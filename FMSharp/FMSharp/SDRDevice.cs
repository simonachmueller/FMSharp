using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSharp
{
    public class SDRDevice
    {
        public string DeviceName { get; private set; }
        public uint DeviceIndex { get; private set; }
        public string DeviceUSBStrings { get; private set; }
        public SDRDevice(string deviceName, uint deviceIndex, string deviceUSBStrings)
        {
            DeviceName = deviceName;
            DeviceIndex = deviceIndex;
            DeviceUSBStrings = deviceUSBStrings;
        }
        public override string ToString()
        {
            return String.Format("ID {0}, {1}, {2}", DeviceIndex, DeviceName, DeviceUSBStrings);
        }
    }
}
