﻿using System;
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
            light.SetRange(0, 10);

            OutputPort laser = new OutputPort(Pins.GPIO_PIN_D0, false);
            
            

            while (true)
            {
                var value = light.Read();
                Thread.Sleep(500);
                Debug.Print(value.ToString());

                if (value >= 7)
                {
                    laser.Write(true);
                }
                else
                {
                    laser.Write(false);
                }
                                                                                                                                                                                                                                             
            }


        }

    }
}
