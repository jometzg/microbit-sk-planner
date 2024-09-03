using System.ComponentModel;
using Microsoft.SemanticKernel;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System;

public class MicrobitPlugin
{
    SerialPort serialPort;

    public MicrobitPlugin()
    {
        serialPort = new SerialPort("COM3", 115200);
        serialPort.Open();
    }

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

    [KernelFunction("set_microbit_command")]
    [Description("send a command to the microbit using REPL.")]
    public async Task  SetLightBrightness(
        Kernel kernel,
        string command
    )
    {
        SendCommand(serialPort, command);
    }
    
    private void SendCommand(SerialPort serialPort, string command)  
    {  
        //send crt-c to stop any running program
        serialPort.Write(new byte[] { 0x03 }, 0, 1);
        // Wait for the micro:bit to stop any running program
        System.Threading.Thread.Sleep(1000); // Adjust the delay as needed

        // Send the command to the micro:bit  
        serialPort.WriteLine(command); 
        // send a carriage return to execute the command 
        serialPort.Write(new byte[] { 0x0d }, 0, 1);
        // Wait for the micro:bit to finish executing the command  
        System.Threading.Thread.Sleep(10); // Adjust the delay as needed  
  
        // Read the response from the micro:bit  
        var response = serialPort.ReadExisting();  
  
        // Print the response to the console  
        Console.WriteLine(response);  
    }  
}
