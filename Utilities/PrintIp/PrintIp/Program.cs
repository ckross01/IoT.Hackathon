using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace PrintIp
{
    public class Program
    {
        public static void Main()
        {
            Microsoft.SPOT.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += new Microsoft.SPOT.Net.NetworkInformation.NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);
            Microsoft.SPOT.Net.NetworkInformation.NetworkChange.NetworkAddressChanged += new Microsoft.SPOT.Net.NetworkInformation.NetworkAddressChangedEventHandler(NetworkChange_NetworkAddressChanged);
            PrintIpAddresses();
            while (true) { };
        }

        static void NetworkChange_NetworkAvailabilityChanged(object sender, Microsoft.SPOT.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
            foreach(var iface in Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                iface.EnableDhcp();
                iface.ReleaseDhcpLease();
                iface.RenewDhcpLease();
            }
        }

        static void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
        {
            PrintIpAddresses();
        }

        static void PrintIpAddresses()
        {
            var ifaces = Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();

            for(var i = 0; i < ifaces.Length; i++)
            {
                Debug.Print("IF" + i.ToString() + ": " + ifaces[i].IPAddress);
            }
        }

    }
}
