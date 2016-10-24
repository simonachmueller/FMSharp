using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSharp
{
    public class RtlSdr
    {
        public RtlSdr()
        {

        }
        public static IReadOnlyCollection<SDRDevice> GetConnectedDevices()
        {
            var deviceCount = RtlSdrWrapper.rtlsdr_get_device_count();
            List<SDRDevice> devices = new List<SDRDevice>((int)deviceCount);
            for (uint devIndex = 0; devIndex < deviceCount; devIndex++)
            {
                devices.Add(new SDRDevice(RtlSdrWrapper.rtlsdr_get_device_name(devIndex), devIndex));
            }
            return devices.AsReadOnly();
        }
    }
}
