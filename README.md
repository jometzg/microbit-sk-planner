# Semantic kernel planner example with virtual lights and Micro:bit

The repository is a starting point for a workshop to demonstrate the power of Semantic Kernel [Planners](https://learn.microsoft.com/en-us/semantic-kernel/concepts/planning?pivots=programming-language-csharp), especially in controlling physical things.

## Project Structure
This project is a simple semantic kernel console app set up as a planner, with 2 plugins:
1. Light plugin. This simulates a 5x5 grid of lights that can each be separately addressed and have 10 stages of brightness.
2. Microbit plugin. This interfaces directly to a BBC [Micro:bit](https://microbit.org/) via serial over USB. The micobit has the same array of physical LEDs, so the same experiments can be targeted at the virtual lights or to the micro:bit LEDs.

So, the console app implements a chat interface over the console. This could be refactored to be a web API, but this is left to others to implement.

## Labs
[Lab 01 - basic light control](./Labs/Lab-01/readme.md)

[Lab 02 - doing more with lights](./Labs/Lab-02/readme.md)

[Lab 03 - using a Micro:bit](./Labs/Lab-03/readme.md)

[Lab 04 - more with a Micro:bit](./Labs/Lab-04/readme.md)


## Lab Ideas
here are some potential lab ideas that will be fleshed out later:

1. Controlling individual lights at a pixel level using X, Y coordinates and brightness
2. Displaying the state of the virtual light array
3. Light up a row or column
4. Light up a shape like a square or circle
5. clear the lights to zero
6. Find out the state of a pixel
7. Flash the lights
8. Brighten the lights (adding to the brightness)
9. Render a more complex shape
10. Render a character or letter using the character's name e.g. "M"
11. Any of the above, but targerting the microbit
12. Copy what's on the virtual lights to the microbit
13. Display scrolling text on the microbit
14. Get the current temperature from the microbit
15. Draw shapes on the microbit
16. Display a scrolling word or phrase on the microbit
17. Make a sound on the microbit speaker

The general idea will be to initially have fewer plug-in methods and introduce new ones as needed for new use cases that may be difficult or not possible with fewer plugin methods. 

This will allow the semantic kernel planner to make different choices on how to call the plug-in methods for each lab.

