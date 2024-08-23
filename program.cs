#pragma warning disable SKEXP0050
#pragma warning disable SKEXP0060

using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;


// Create the kernel
var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(
    "MODEL",
    "ENDPOINT",
    "KEY");

// add logging, if needed
builder.Services.AddLogging(c => 
{
    c.AddConsole();
    c.SetMinimumLevel(LogLevel.Trace);
});

// add the lights and microbit plugins
builder.Plugins.AddFromType<LightsPlugin>();
builder.Plugins.AddFromType<MicrobitPlugin>();
Kernel kernel = builder.Build();

// Retrieve the chat completion service from the kernel
IChatCompletionService chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

// Enable automatic function calling
OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new() 
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

// Create the chat history, sort of system prompt (need to check)
ChatHistory history = new ChatHistory("""
    You have a matrix of 5 rows and columns each of which can have a brightness of 0 to 9.
    Rows make up the horizontal axis. The top row has coordinates 0,1 to 4, 0.
    Columns represent the vertical axis. The left most column is 0,0 to 0, 4 and the right column is 4,0 to 4,4.
    """);


string? userInput;
do {
    // Collect user input
    Console.Write("User > ");
    userInput = Console.ReadLine();

    // Add user input
    history.AddUserMessage(userInput);

    // 3. Get the response from the AI with automatic function calling
    var result = await chatCompletionService.GetChatMessageContentAsync(
        history,
        executionSettings: openAIPromptExecutionSettings,
        kernel: kernel);

    // Print the results
    Console.WriteLine("Assistant > " + result);

    // Add the message from the agent to the chat history
    history.AddMessage(result.Role, result.Content ?? string.Empty);
} while (userInput is not null);
