using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace RotaryEncoder
{
    public class Program
    {
        private static RotaryEncoderDriver encoder;

        public static void Main()
        {
            encoder = new RotaryEncoderDriver(Pins.GPIO_PIN_D12, Pins.GPIO_PIN_D13, Pins.GPIO_PIN_D0);
            encoder.OnButtonStateChanged += new Button.ButtonStateEventHandler(encoder_OnButtonStateChanged);
            encoder.OnCounterChanged += new CounterChangedEventHandler(encoder_OnCounterChanged);
            encoder.OnCounterReset += new CounterResetEventHandler(encoder_OnCounterReset);
            encoder.MaxValue = 100;
            encoder.MinValue = -100;

            while (true)
            {
                Thread.Sleep(100);
            }

        }

        static void encoder_OnCounterReset(object sender, EventArgs args)
        {
            Debug.Print("Counter Reset");
        }

        static void encoder_OnCounterChanged(object sender, CounterChangedEventArgs args)
        {
            Debug.Print("Count: " + args.Count.ToString());
        }

        static void encoder_OnButtonStateChanged(object sender, Button.ButtonStateEventArgs args)
        {
            Debug.Print("Pressed " + args.IsPressed);
        }

    }
}
