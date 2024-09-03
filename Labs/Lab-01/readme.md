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
