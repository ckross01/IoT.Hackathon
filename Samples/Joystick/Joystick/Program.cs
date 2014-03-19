using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace Joystick
{
    public class Program
    {
        public static void Main()
        {
            // configure driver, VRx to Analog 0, VRy to Analog 1, SW to Digital 0
            var pad = new JoystickDriver(AnalogChannels.ANALOG_PIN_A0, AnalogChannels.ANALOG_PIN_A1, Pins.GPIO_PIN_D0);
            // attach event handlers
            pad.OnClick += new ButtonClickEventHandler(pad_OnClick);
            pad.OnButtonStateChange += new ButtonStateChangedEventHandler(pad_OnButtonStateChange);
            pad.OnAxisChange += new AxisChangedEventHandler(pad_OnAxisChange);


            while (true) {
                Thread.Sleep(500);
            }
        }

        static void pad_OnAxisChange(object sender, AxisChangeEventArgs args)
        {
            Debug.Print("Axis Change");
            Debug.Print("X: " + args.X.ToString());
            Debug.Print("Y: " + args.Y.ToString());
        }

        static void pad_OnButtonStateChange(object sender, ButtonStateChangeEventArgs args)
        {
            Debug.Print("Button State: " + args.IsPressed.ToString());
        }

        static void pad_OnClick(object sender, EventArgs args)
        {
            Debug.Print("Button Clicked");
        }
    }
}
