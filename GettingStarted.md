# Getting Started

## Required Software

The following packages are required and should be installed in the order shown
below.

1. [Microsoft Visual C# Express 2010](http://www.visualstudio.com/downloads/download-visual-studio-vs#d-csharp-2010-express)
2. [.NET Micro Framework SDK v4.2](http://www.netduino.com/downloads/MicroFrameworkSDK_NETMF42_QFE2.msi)
3. [Netduino SDK v4.2.2.0 (32-bit)](http://www.netduino.com/downloads/netduinosdk_32bit_NETMF42.exe) or [Netduino SDK v4.2.2.0 (64-bit)](http://www.netduino.com/downloads/netduinosdk_64bit_NETMF42.exe)
4. [Github for windows](http://windows.github.com)


## Provided Hardware

- [Netduino Plus 2 board](http://www.netduino.com/netduinoplus2/specs.htm)
- Assortment of sensors

## Running Code

In the [Getting Started guide](http://www.netduino.com/downloads/gettingstarted.pdf) on the Netduino site they go over creating the 'blinky' app to get some code running on your board.  Spend a few minutes in the Code and Run sections of this document to validate your configuration.

## Verify Internet Connectivity

1. Clone the [neudesic/IoT.Hackathon](https://github.com/neudesic/IoT.Hackathon) repository on Github.
Easiest way to do this is go to the repository above and click `Clone in Desktop` button.
2. Open the `Utilities\PrintIp\PrintIp.sln` and run it on your Netduino
3. Observe the IP Address in the Output window in Visual Studio

## It's broken...
Most likely its not.  These boards are built for hacking.  I bricked 3 times
in an hour before finding the bug causing it.

Before you toss your board, grab one of the coordinators.  Bricking your device
can be part of the hacking process.  We have been there and will be more than
happy to share the knowledge of unbricking.

## Connecting Sensors

##### A word on LEDs
LEDs are like children in a candy store.  They will try to consume every bit of
current it can.  A current limiting resistor may reduce the brightness, but it
will also protect the LED and the Netduino from becoming smokers.

### Analog Sensors
Analog sensors must be wired up to analog ports in order to function.  This is
common for sensors that use voltage levels to give a variable state over a
single pin.

In the Joystick sample, the X and Y Axis provides values ranging from 0 to 100
to show the percentage of travel along that axis.

### Digital Sensors
Digital sensors come in three variants.  
1. Simple on/off, true/false, 0/1 states.
The Button sample is shows this type of digital sensor.
2. Pulse Width Modulation(PWM), RGBLED sample uses this to control the level
of brightness for each portion of Red/Green/Blue.  With PWM channels, you set
the frequency of the pulses and the hardware layer will take care of repeating
the pattern until you change it.
3. Serial, Bing/Google is your friend.  Look for a driver someone else has
written.  Even a driver written for Arduino, or another micro-controller can
be helpful in bit-banging your Netduino version.

## Additional Resources

- [Netduino](www.netduino.com)
- [.NET Micro Framework (v4.1) MSDN](http://msdn.microsoft.com/en-us/library/ee436350.aspx)
- [Getting Started with Netduino: Safari Books Online](http://search.safaribooksonline.com/book/hardware/netduino/9781449317799)
