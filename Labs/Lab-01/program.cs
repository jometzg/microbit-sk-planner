#pragma warning disable SKEXP0050
#pragma warning disable SKEXP0060

using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;  
using System;

// Create the kernel
var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(
    "YOUR_MODEL",
    "YOUR_ENDPOINT",
    "YOUR_KEY");

// uncomment for more debugging - either Loglevel.Info or LogLevel.Trace
// builder.Services.AddLogging(c => 
// {
//     c.AddConsole();
//     c.SetMinimumLevel(LogLevel.Trace);
// });

builder.Plugins.AddFromType<LightsPlugin>();

Kernel kernel = builder.Build();

// Retrieve the chat completion service from the kernel
IChatCompletionService chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

// 2. Enable automatic function calling
OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new() 
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

// Create the chat history and start with some instructions
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

    if (userInput is not null)
    {
        // Add user input
        history.AddUserMessage(userInput);

        // Get the response from the AI with automatic function calling
        var result = await chatCompletionService.GetChatMessageContentAsync(
            history,
            executionSettings: openAIPromptExecutionSettings,
            kernel: kernel);

        // Print the results
        Console.WriteLine("Assistant > " + result);

        // Add the message from the agent to the chat history
        history.AddMessage(result.Role, result.Content ?? string.Empty);
    }
} while (userInput is not null);
