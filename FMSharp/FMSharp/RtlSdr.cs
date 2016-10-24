using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSharp
{
    public class RtlSdr : IDisposable
    {
        private IntPtr device;
        public RtlSdr(uint deviceIndex)
        {
            device = new IntPtr();
            if (RtlSdrWrapper.rtlsdr_open(out device, deviceIndex) != 0)
                throw new Exception("Unable to open device");
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                RtlSdrWrapper.rtlsdr_close(device);
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);            
        }
        #endregion
    }
}
