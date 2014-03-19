using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace RGBLED
{
    public class Program
    {
        public static void Main()
        {
            var led = new RGBLEDDriver(PWMChannels.PWM_PIN_D6, PWMChannels.PWM_PIN_D5, PWMChannels.PWM_PIN_D10);

            while (true)
            {
                led.Off();
                led.R(100);
                Thread.Sleep(1000);
                led.Off();
                led.G(100);
                Thread.Sleep(1000);
                led.Off();
                led.B(100);
                Thread.Sleep(1000);

                // color mixing
                for (var r = 0; r <= 100; r+= 10)
                {
                    for (var g = 0; g <= 100; g += 10)
                    {
                        for (var b = 0; b <= 100; b += 10)
                        {
                            led.R((uint)r);
                            led.B((uint)b);
                            led.G((uint)g);
                            Thread.Sleep(250);
                        }
                    }
                }
            }


        }

    }
}
