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
                StringBuilder manufact = new StringBuilder(256);
                StringBuilder product = new StringBuilder(256);
                StringBuilder serial = new StringBuilder(256);
                RtlSdrWrapper.rtlsdr_get_device_usb_strings(devIndex, manufact, product, serial);
                string usbStrings;
                usbStrings = string.Format("{0}, {1}, SN: {2}", manufact, product, serial);
                devices.Add(new SDRDevice(RtlSdrWrapper.rtlsdr_get_device_name(devIndex), devIndex, usbStrings));
            }
            return devices.AsReadOnly();
        }
    }
}
