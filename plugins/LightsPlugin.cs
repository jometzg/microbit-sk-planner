using System.ComponentModel;
using Microsoft.SemanticKernel;
using System.Text;
using System.Threading.Tasks;
using System;

public class LightsPlugin
{
    const int COLUMNS = 5;
    const int ROWS = 5;

    LightModel[,] _lights = new LightModel[ROWS,COLUMNS];

    public LightsPlugin()
    {
        for (int row = 0; row < ROWS; row++)
        {
            for(int column = 0; column < COLUMNS; column++)
            {
                _lights[row, column] = new LightModel
                {
                    IsOn = false,
                    Brightness = 0
                };
            }
        }
    }

    [KernelFunction("get_light_brightness")]
    [Description("gets a light brightness by its row and column ID.")]
    public async Task<int> GetLightBrightness(
        Kernel kernel,
        int rowid,
        int columnid
    )
    {
        // Add logic to send an email using the recipientEmails, subject, and body
        // For now, we'll just print out a success message to the console
        var brightness = _lights[rowid, columnid].Brightness;
        Console.WriteLine(brightness);
        return brightness;
    }

    [KernelFunction("set_light_brightness")]
    [Description("sets a light brightness by its ID.")]
    public async Task SetLightBrightness(
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

    [KernelFunction("turn_on_light")]
    [Description("turns on a light by its ID.")]
    public async Task TurnOnLight(
        Kernel kernel,
        int rowid,
        int columnid
    )
    {
        // Add logic to send an email using the recipientEmails, subject, and body
        // For now, we'll just print out a success message to the console
        _lights[rowid, columnid].IsOn = true;
        Console.WriteLine("Light turned on!");
    }

    [KernelFunction("get_the_light_onoff_status")]
    [Description("gets the on/off status of a light by its ID.")]
    public async Task<bool> GetTheLightOnOffStatus(
        Kernel kernel,
        int rowid,
        int columnid
    )
    {
        // Add logic to send an email using the recipientEmails, subject, and body
        // For now, we'll just print out a success message to the console
        var isOn = _lights[rowid, columnid].IsOn;
        Console.WriteLine(isOn);
        return isOn;
    }
    
    [KernelFunction("show_all_lights")]
    public async Task<string> ShowAllTheLights( Kernel kernel)
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
}