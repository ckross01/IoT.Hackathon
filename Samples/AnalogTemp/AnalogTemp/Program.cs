using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace AnalogTemp
{
    public class Program
    {
        private static AnalogInput sensorPin = new AnalogInput(Cpu.AnalogChannel.ANALOG_0, 1, 0, -1);
        
        public static void Main()
        {            
            while (true)
            {
                int adcValue = sensorPin.ReadRaw();
                double temperature = Thermister(adcValue);
                Debug.Print("Temp value: " + temperature.ToString());
                Thread.Sleep(500);
            }
        }

        public static double Thermister(int RawADC)
        {
            double Temp;
            Temp = System.Math.Log(((10240000000 / RawADC) - 10000));
            Temp = 1 / (0.001129148 + (0.000234125 + (0.0000000876741 * Temp * Temp)) * Temp);
            Temp = Temp - 273.15; // Convert Kelvin to Celcius
            return Temp * -1;
        }

    }
}