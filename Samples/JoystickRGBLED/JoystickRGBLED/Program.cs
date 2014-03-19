using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace JoystickRGBLED
{
    public class Program
    {
        static Joystick.JoystickDriver pad;
        static RGBLED.RGBLEDDriver led;
        static uint color = 0;

        public static void Main()
        {
            pad = new Joystick.JoystickDriver(AnalogChannels.ANALOG_PIN_A0, AnalogChannels.ANALOG_PIN_A1, Pins.GPIO_PIN_D0);
            led = new RGBLED.RGBLEDDriver(PWMChannels.PWM_PIN_D6, PWMChannels.PWM_PIN_D5, PWMChannels.PWM_PIN_D10);

            pad.OnAxisChange += new Joystick.AxisChangedEventHandler(pad_OnAxisChange);
            pad.OnClick += new Joystick.ButtonClickEventHandler(pad_OnClick);
            while (true)
            {
                Thread.Sleep(10);
            }
        }

        static void pad_OnClick(object sender, EventArgs args)
        {
            if (color == uint.MaxValue)
                color = 0;
            else
                color++;
        }

        static void pad_OnAxisChange(object sender, Joystick.AxisChangeEventArgs args)
        {
            var percent = (uint)args.X;
            if ((color % 3) == 0)
                led.B(percent);
            else if ((color % 2) == 0)
                led.G(percent);
            else
                led.R(percent);
        }

    }
}
