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


