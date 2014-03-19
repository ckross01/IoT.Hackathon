using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace Button
{
    public class Program
    {
        static OutputPort led;

        public static void Main()
        {
            led = new OutputPort(Pins.ONBOARD_LED, false);
            ButtonDriver button = new ButtonDriver(Pins.GPIO_PIN_D0);
            button.OnButtonStateChanged += new ButtonStateEventHandler(button_OnButtonStateChanged);

            while (true)
            {
                Thread.Sleep(100);
            }
        }

        static void button_OnButtonStateChanged(object sender, ButtonStateEventArgs args)
        {
            led.Write(args.IsPressed);
        }

    }
}
