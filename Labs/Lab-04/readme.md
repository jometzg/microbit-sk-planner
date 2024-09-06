# Lab 04 - more general commands with the Microbit

In this lab, we will look to see how we can improve the Microbit plugin to make it more functional.

## Prerequisites
1. Lab-03 running

## Steps
As it currently stands, the Microbit plugin can set a specific pixel using the following method:
```
 [KernelFunction("set_microbit_light_brightness")]
 [Description("sets the brightness of a microbit pixel by its row and column ID.")]
 public async Task<int>  SetLightBrightness(
      Kernel kernel,
      int rowid,
      int columnid,
      int brightness
  )
  {
      SendCommand(serialPort, $"display.set_pixel({rowid},{columnid},{brightness})");
      return brightness;
  }
```

We could devise a more generic method with the following signature:
```
[KernelFunction("set_microbit_command")]
[Description("send a command to the microbit using REPL.")]
```
Complete the function definition and rebuild the application.

## In this lab
The Microbit has a number of single-line commands that can do much more interesting things than with a single Pixel at a time.

You can try:
1. Get it to Display one letter after another
2. Get it to display a scrolling word or sentance
3. Make a sound
4. Ask it for the current temperature

## Extra Ideas
The REPL interface, which is the command-line serial interface most easily accepts single line commands, these can even be sent in a sequence.

So, if you want to display a line on the microbit, there are 2 potential approaches:
1. send each of the pixel requests one by one that correspond to the line
2. Use a loop and in the loop, set a pixel value using the loop index

The LLM may chose either of these approaches as Microbit commands appear to be in the gpt-4o training set. So, it could choose either of these approaches. If the loop one does not work, as the bot to send the requests *one by one* or *pixel by pixel*. That often will fix the issue.

Another enhancement to the plugin would be to make it more easy to accept multi-statement requests - like for loops in one go. It is worth a try. Python being space away does a loop like this:
```
for i in range (5):
   # set the pixel in column 0, row i to 9
   display.set_pixel(0, i, 9)
```

so the serial interface could be enhanced to accept the above and send it in a way that the loop code gets sent once and executed.
