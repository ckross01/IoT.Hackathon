using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using Microsoft.SPOT.Hardware;


namespace LightSensor
{
    public class Program
    {
        public static void Main()
        {


            SecretLabs.NETMF.Hardware.AnalogInput light = new SecretLabs.NETMF.Hardware.AnalogInput(Pins.GPIO_PIN_A0);
            
            // 0 lightest, 10 darkest
            light.SetRange(0, 10);


            while (true)
            {
                var value = light.Read();
                Thread.Sleep(500);
                Debug.Print(value.ToString());
                                                                                                                                                                                                                                  
            }


        }

    }
}
