# Lab 02 - Doing more with lights

This lab extends the previous lab, so you can decide whether to run it from here or just extend the lab you previously built.

All this lab does is a little more with the lights!

## Prerequisites
1. Lab-01 prerequities
2. A working lab-01 preferably

## In this lab
So far, we can display the matrix of lights and set a specific pixel, but there is no means to get the brightness of a specific pixel.

The tasks for this lab are:
1. to add a method for getting the value of the pixel.
2. set a pixel to on or off
3. get the status of a pixel in terms of whether it is on or off
4. clear the whole display

Some hints: 
1. the method to set the value of a pixel has most of what is needed to get the value
2. is on an off really necessary? Would brightness just do?
3. clearing the display has some of the characteristics of either the orginal initialisation of the lights or the display method

## Some More Thoughts
So far we have been looking at the plugin having methods for doing what you want to each of the lights in the matrix.

Semantic kernel planners can do a little more.

Some further experiments to try:
1. Comment out the pixel on and off methods and ask to turn a pixel on or off? What happens?
2. What about commenting out the clear the whole display method from above? Can you ask the display to be cleared still? And what happens?

This then opens up the possibility to do even more interesting things. Some things to try:
1. Ask it to draw line giving its start and end positions
2. Ask it to draw a vertical or horizontal line
3. Ask it to draw a circle
4. How about a symbol or a letter?

```
User > draw a circle and show it
Brightness set!
Brightness set!
Brightness set!
Brightness set!
 .  .  .  .  . 
 .  .  5  .  .
 .  5  .  5  .
 .  .  5  .  .
 .  .  .  .  .

Assistant > The circle has been drawn and displayed on the matrix.
User > make the circle bigger
Brightness set!
Brightness set!
Brightness set!
Brightness set!
Brightness set!
Brightness set!
Brightness set!
Brightness set!
 .  .  5  .  . 
 .  5  5  5  .
 5  5  .  5  5
 .  5  5  5  .
 .  .  5  .  .

Assistant > The circle has been enlarged and is now displayed on the matrix.
User > clear the display and then draw a vertical line on the right side of the display
Brightness set!
Brightness set!
Brightness set!
Brightness set!
Brightness set!
Brightness set!
Brightness set!
Brightness set!
Brightness set!
Brightness set!
Brightness set!
Brightness set!
Brightness set!
Brightness set!
Brightness set!
Brightness set!
Brightness set!
 .  .  .  .  9 
 .  .  .  .  9
 .  .  .  .  9
 .  .  .  .  9
 .  .  .  .  9

Assistant > The display has been cleared and a vertical line has been drawn on the right side of the matrix.
```

## Troubleshooting
In this lab, the semantic kernel planner may call the LLM multiple times. If your rate limit for your model deployment is too low, then this may cause the model to return errors (too busy or 429s). If this is the case, then increase the token limit for the model.

Remember, you can get more debug with:
```
builder.Services.AddLogging(c => 
{
    c.AddConsole();
    c.SetMinimumLevel(LogLevel.Trace);
});
```

# Summary
Even with minimal methods on the plugin, the semantic kernel planner can do more complicated things. How well it does them all depends on how the original prompt is setup and how you ask. So some experimentation can yield better results.


Next up the Micro:bit.
