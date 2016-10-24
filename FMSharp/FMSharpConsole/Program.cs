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
            var devices = RtlSdr.GetConnectedDevices();
            Console.WriteLine("Found {0} devices", devices.Count);
            Console.WriteLine(string.Join(",", devices.Select(x => x.ToString())));

            var t = new RtlSdr(0);

            Console.ReadLine();
        }
    }
}
