using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace RotaryEncoderRGBLED
{
    public class Program
    {
        delegate void Color(uint state);

        static RotaryEncoder.RotaryEncoderDriver encoder;
        static RGBLED.RGBLEDDriver led;
        static uint color = 0;
        static Color[] colors;
        static uint[] states;

        public static void Main()
        {
            encoder = new RotaryEncoder.RotaryEncoderDriver(Pins.GPIO_PIN_D12, Pins.GPIO_PIN_D13, Pins.GPIO_PIN_D0);
            led = new RGBLED.RGBLEDDriver(PWMChannels.PWM_PIN_D6, PWMChannels.PWM_PIN_D5, PWMChannels.PWM_PIN_D10);

            encoder.OnClick += new Button.ButtonClickEventHandler(encoder_OnClick);
            encoder.OnCounterChanged += new RotaryEncoder.CounterChangedEventHandler(encoder_OnCounterChanged);
            encoder.OnCounterReset += new RotaryEncoder.CounterResetEventHandler(encoder_OnCounterReset);
            encoder.MaxValue = 100;
            encoder.MinValue = 0;

            colors= new Color[]{
                led.R,
                led.G,
                led.B
            };
            states = new uint[]{0, 0, 0};
            led.Off();
            
            while (true)
            {
                Thread.Sleep(10);
            }
        }

        static void updateColor(uint v)
        {
            states[color] = (uint)encoder.Counter;
            colors[color](states[color]);
        }

        static void  encoder_OnCounterReset(object sender, EventArgs args)
        {
 	        updateColor((uint)encoder.Counter);
        }

        static void encoder_OnClick(object sender, EventArgs args)
        {
            color++;

            if(color == colors.Length)
                color = 0;
            encoder.Counter = (int)states[color];
        }

        static void encoder_OnCounterChanged(object sender, RotaryEncoder.CounterChangedEventArgs args)
        {
            updateColor((uint)args.Count);
        }

    }
}
