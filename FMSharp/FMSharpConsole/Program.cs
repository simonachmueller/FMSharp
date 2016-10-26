using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSharp;

namespace FMSharpConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var devices = RtlSdrDriver.GetConnectedDevices();
            Console.WriteLine("Found {0} devices", devices.Count);
            Console.WriteLine(string.Join(",", devices.Select(x => x.ToString())));

            IRtlSdrDriver rtlDevice = new RtlSdrDriver();
            rtlDevice.OpenDevice();
            rtlDevice.SetFrequency(100000000);
            var samples = rtlDevice.ReadSamplesSync(256);
            Console.WriteLine("Samples: {0}", string.Join(",", samples.Select(x => x.ToString())));
            rtlDevice.CloseDevice();

            Console.WriteLine("Press any key to close the window");
            Console.ReadLine();
        }
    }
}
