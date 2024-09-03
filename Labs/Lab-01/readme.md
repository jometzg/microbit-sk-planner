# Lab One - Getting Started
This lab is based upon the idea in the Semantic Kernel [Planning](https://learn.microsoft.com/en-us/semantic-kernel/concepts/planning?pivots=programming-language-csharp) and is about controlling a virtual set of lights. 

In this lab these lights are in a 5x5 matrix and can have a brightness of 0 to 9.

## Prerequisites
This lab requires:
1. An Azure OpenAI service deployed
2. A model deployment - preferably GPT-4o
3. Visual Studio code
4. .NET 8.0 [SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Steps
1. Clone this repo locally (or use a CodeSpaces)
```
git clone https://github.com/jometzg/microbit-sk-planner.git
```
2. In the VS Code terminal, open up a command prompt and go to the folder *lab-01*
3. In the command prompt:
```
dotnet restore 
```
4. if there are any build issues resolve these. This may require the use of *dotnet add package XXXXX*
5. In the file *program.cs*, update the credentials in the *AddAzureOpenAIChatCompletion* call to be those of your own Azure OpenAI instance.
6. In the terminal, run:
```
dotnet build
```
7. If these are build issues, these need to be resolved.
8. The application is a console one, so in the terminal:
```
dotnet run
```
9. You should then get a user prompt *user > * where you can enter your query to the chatbot.
10. You can try a simple command "show lights"


## Debugging
1. If you can't get a successful build, then it is likely that either packages have not been added or that an incorrect version of a package may not be present. Present packages can be seen in the *.csproj* file and these could be stripped out and redownloaded with a "dotnet add package XXXXX"
2. If you get an exception when sending the first chat, check your endpoint, key and model deployment names are correct.
3. You can add extra debugging to the application:
```
builder.Services.AddLogging(c => 
{
    c.AddConsole();
    c.SetMinimumLevel(LogLevel.Info);
});
```
or *LogLevel.Trace*

## Sample Run
```
PS C:\dev\sk-learn\micro-lights> dotnet run  
User > show the lights
 .  .  .  .  . 
 .  .  .  .  .
 .  .  .  .  .
 .  .  .  .  .
 .  .  .  .  .

Assistant > Currently, all the lights are off. Here's the display:

```
 .  .  .  .  .
 .  .  .  .  .
 .  .  .  .  .
 .  .  .  .  .
 .  .  .  .  .
```
Each dot represents a light that is turned off.
User > set light 0, 1 to 5
Brightness set!
Assistant > The brightness of the light at position (0, 1) has been set to 5.
User > show the lights
 .  5  .  .  . 
 .  .  .  .  .
 .  .  .  .  .
 .  .  .  .  .
 .  .  .  .  .

Assistant > Here's the updated display with the light at position (0, 1) set to a brightness of 5:

```
 .  5  .  .  .
 .  .  .  .  .
 .  .  .  .  .
 .  .  .  .  .
 .  .  .  .  .
```
User >
```
As can be seen, the app can draw the lights when asked and set the value of a single light.

## Understanding the application
Being a planner, application, this has a main planning loop that controls the flow of the application.
```
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
```
This allows the semantic kernal to use chat history to build up the context to the Azure OpenAI model. This also allows the planner to call prompts and plugins where needed, without you explicitly having to call these as a developer. 

It can also do more than one call to the LLM if it needs to to achieve what you have asked for.

This is the power of the planner.

In order to do this, the prompts and plugins need to be registered
```
builder.Plugins.AddFromType<LightsPlugin>();
```
The plugin is a class with one or more methods that are *decorated* with a description that semantic kernal inspects. The underlying mechanism is OpenAI automatic function calling.

The lights in this demo are represented as a 2 dimensional array - to give each light a coordinate:

```
const int COLUMNS = 5;
const int ROWS = 5;
LightModel[,] _lights = new LightModel[ROWS,COLUMNS];
```
At its most basic level, the plugin only needs a few methods:

1. display the light matrix
```
[KernelFunction("show_all_lights")]
[Description("display the light brighness")]
public string ShowAllTheLights( Kernel kernel)
{
    var sb = new StringBuilder();
    for (int row = 0; row < ROWS; row++)
    {
        for(int column = 0; column < COLUMNS; column++)
        {
            if(_lights[row, column].Brightness > 0)
                sb.Append($" {_lights[row, column].Brightness} ");   
            else
                sb.Append(" . ");
        }
        sb.AppendLine();
    }
    Console.WriteLine(sb.ToString());
    return sb.ToString();
}
```

2. Set the brightness of a pixel in the light matrix
```
[KernelFunction("set_light_brightness")]
[Description("sets a light brightness by its ID.")]
public void SetLightBrightness(
    Kernel kernel,
    int rowid,
    int columnid,
    int brightness
)
{
    // Add logic to send an email using the recipientEmails, subject, and body
    // For now, we'll just print out a success message to the console
    _lights[rowid, columnid].Brightness = brightness;
    Console.WriteLine("Brightness set!");
}
```
With this pair of methods, you can set any pixel value and display the matrix of values.

