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

builder.Plugins.AddFromType<LightsPlugin>();
builder.Plugins.AddFromType<MicrobitPlugin>();
Kernel kernel = builder.Build();

// Retrieve the chat completion service from the kernel
IChatCompletionService chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

// 2. Enable automatic function calling
OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new() 
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

// Create the chat history
ChatHistory history = new ChatHistory("""
    You are a friendly assistant who likes to follow the rules. You have a matrix of 5 rows and columns each of which can have a brightness of 9 .
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
