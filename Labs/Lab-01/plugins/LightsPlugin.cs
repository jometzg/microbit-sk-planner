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

   
    
    [KernelFunction("show_all_lights")]
    [Description("displays the matrix of the lights with each lights brightness")]
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
}
