using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using Math = System.Math;

namespace RGBLED
{
    public class RGBLEDDriver
    {
        private PWM redPort;
        private PWM greenPort;
        private PWM bluePort;

        public RGBLEDDriver(Microsoft.SPOT.Hardware.Cpu.PWMChannel redPin, Microsoft.SPOT.Hardware.Cpu.PWMChannel greenPin, Microsoft.SPOT.Hardware.Cpu.PWMChannel bluePin)
        {
            // TODO: Complete member initialization
            this.redPort = new PWM(redPin, 1000, 1, PWM.ScaleFactor.Microseconds, false);
            this.greenPort = new PWM(greenPin, 1000, 1, PWM.ScaleFactor.Microseconds, false);
            this.bluePort = new PWM(bluePin, 1000, 1, PWM.ScaleFactor.Microseconds, false);
        }

        void setPort(PWM port, uint scale)
        {
            if (scale == 0)
            {
                port.Stop();
                return;
            }
            port.Duration = (uint)Math.Round((500 * scale) * 0.01);
            port.Start();
        }

        internal void Off()
        {
            setPort(redPort, 0);
            setPort(greenPort, 0);
            setPort(bluePort, 0);
        }

        internal void R(uint p)
        {
            setPort(redPort, p);
        }

        internal void G(uint p)
        {
            setPort(greenPort, p);
        }

        internal void B(uint p)
        {
            setPort(bluePort, p);
        }
    }
}
