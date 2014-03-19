using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System.Threading;
using Math = System.Math;
using Button;

namespace Joystick
{
    // axis changed handler, we just wrap both x and y into a single handler
    // most likley there will be slight variances to the other axis as you move.
    public delegate void AxisChangedEventHandler(object sender, AxisChangeEventArgs args);
    public class AxisChangeEventArgs
    {
        public double X { get; internal set; }
        public double Y { get; internal set; }

        public AxisChangeEventArgs(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    // the actual driver
    public class JoystickDriver
    {
        private AnalogInput xPort;
        private AnalogInput yPort;
        private ButtonDriver button;

        public event ButtonClickEventHandler OnClick;
        public event ButtonStateEventHandler OnButtonStateChange;
        public event AxisChangedEventHandler OnAxisChange;
        private Thread thread;

        public bool ButtonState { get; internal set; }
        public double XAxis { get; internal set; }
        public double YAxis { get; internal set; }

        public JoystickDriver(Microsoft.SPOT.Hardware.Cpu.AnalogChannel xPort, Microsoft.SPOT.Hardware.Cpu.AnalogChannel yPort, Microsoft.SPOT.Hardware.Cpu.Pin buttonPort)
        {
            this.xPort = new AnalogInput(xPort);
            this.yPort = new AnalogInput(yPort);
            this.button = new ButtonDriver(buttonPort);
            this.button.OnButtonStateChanged += new ButtonStateEventHandler(button_OnButtonStateChanged);
            this.button.OnClick += new ButtonClickEventHandler(button_OnClick);

            // Analog ports can't interrupt, so we spin up a thread to handle reading
            // the values and triggering events
            this.thread = new Thread(this.Sampler);
            this.thread.Start();
        }

        void button_OnClick(object sender, EventArgs args)
        {
            if (OnClick != null)
                OnClick(this, args);
        }

        void button_OnButtonStateChanged(object sender, ButtonStateEventArgs args)
        {
            if (OnButtonStateChange != null)
                OnButtonStateChange(this, args);
        }

        void Sampler()
        {
            const double variance = 1;
            // driver loop to constantly read the axis data
            // we make an effort to slow down the noise
            // from the sensor
            while (true)
            {
                var x = (int)Math.Round(this.xPort.Read() * 100);
                var y = (int)Math.Round(this.yPort.Read() * 100);
                if (Math.Abs((XAxis - x)) > variance || Math.Abs((YAxis - y)) > variance)
                {
                    // store the values in case the user is just sampling the driver in
                    // thier main loop
                    XAxis = x;
                    YAxis = y;
                    // fire the event if we are attached
                    if (OnAxisChange != null)
                        OnAxisChange(this, new AxisChangeEventArgs(XAxis, YAxis));
                }
            }
        }
    }
}
