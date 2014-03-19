using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace Button
{
    public delegate void ButtonClickEventHandler(object sender, EventArgs args);
    public delegate void ButtonStateEventHandler(object sender, ButtonStateEventArgs args);
    public class ButtonStateEventArgs
    {
        public bool IsPressed { get; internal set; }

        public ButtonStateEventArgs(bool pressed)
        {
            IsPressed = pressed;
        }
    }

    public class ButtonDriver
    {
        private const int debounce = 1500;
        private InterruptPort buttonPort;
        private DateTime updated;

        public event ButtonClickEventHandler OnClick;
        public event ButtonStateEventHandler OnButtonStateChanged;

        public bool IsPressed { get; internal set; }

        public ButtonDriver(Cpu.Pin buttonPin)
        {
            buttonPort = new InterruptPort(buttonPin, true, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth);
            buttonPort.OnInterrupt += new NativeEventHandler(buttonPort_OnInterrupt);
        }

        void buttonPort_OnInterrupt(uint data1, uint data2, DateTime time)
        {
            if (time.Subtract(updated).Ticks > debounce)
            {
                updated = time;
                IsPressed = ((data2 % 2) == 0);

                if (OnButtonStateChanged != null)
                    OnButtonStateChanged(this, new ButtonStateEventArgs(IsPressed));

                if (!IsPressed && OnClick != null)
                    OnClick(this, EventArgs.Empty);
            }
        }
    }
}

