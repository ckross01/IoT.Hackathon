using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Button;
using System.Threading;

namespace RotaryEncoder
{
    public delegate void CounterResetEventHandler(object sender, EventArgs args);
    public delegate void CounterChangedEventHandler(object sender, CounterChangedEventArgs args);
    public class CounterChangedEventArgs
    {
        public int Count { get; internal set; }

        public CounterChangedEventArgs(int count)
        {
            Count = count;
        }
    }

    public class RotaryEncoderDriver
    {
        const int debounce = 3000;

        ButtonDriver button;
        InterruptPort aPort;
        InputPort bPort;
        DateTime updated;

        public event ButtonStateEventHandler OnButtonStateChanged;
        public event ButtonClickEventHandler OnClick;
        public event CounterChangedEventHandler OnCounterChanged;
        public event CounterResetEventHandler OnCounterReset;
        
        public bool IsPressed { get; internal set; }
        public int Counter { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        public int ResetValue { get; set; }

        public RotaryEncoderDriver(Cpu.Pin aPin, Cpu.Pin bPin, Cpu.Pin buttonPin)
        {
            MaxValue = int.MaxValue;
            MinValue = int.MinValue;

            button = new ButtonDriver(buttonPin);
            button.OnButtonStateChanged += new ButtonStateEventHandler(button_OnButtonStateChanged);

            aPort = new InterruptPort(aPin, false, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth);
            bPort = new InputPort(bPin, false, Port.ResistorMode.PullUp);
            aPort.OnInterrupt += new NativeEventHandler(aPort_OnInterrupt);
        }

        void aPort_OnInterrupt(uint port, uint state, DateTime time)
        {
            if (time.Subtract(updated).Ticks > debounce)
            {
                updated = time;
                if (Counter >= MaxValue || Counter <= MinValue)
                {
                    Counter = ResetValue;
                    if (OnCounterReset != null)
                        OnCounterReset(this, EventArgs.Empty);
                }

                var bstate = bPort.Read();
                if (state == 0)
                {
                    Counter += (bstate ? -1 : 1);
                }
                else
                {
                    Counter += (bstate ? 1 : -1);
                }
                if (OnCounterChanged != null)
                    OnCounterChanged(this, new CounterChangedEventArgs(Counter));
            }
        }

        void button_OnButtonStateChanged(object sender, ButtonStateEventArgs args)
        {
            IsPressed = args.IsPressed;
            if (OnButtonStateChanged != null)
                OnButtonStateChanged(this, args);
            if (!IsPressed && OnClick != null)
                OnClick(this, EventArgs.Empty);
        }
    }
}
