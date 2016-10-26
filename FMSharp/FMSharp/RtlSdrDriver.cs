using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMSharp
{
    public class RtlSdrDriver : IDisposable, IRtlSdrDriver
    {
        private IntPtr device;
        private const uint DefaultFrequency = 100000000;
        private const int DefaultSamplerate = 2048000;

        public RtlSdrDriver()
        {
            device = new IntPtr();
        }

        public void OpenDevice(uint deviceIndex = 0)
        {
            if (RtlSdrWrapper.rtlsdr_open(out device, deviceIndex) != 0)
                throw new Exception("Unable to open device");
            if (RtlSdrWrapper.rtlsdr_set_center_freq(device, DefaultFrequency) != 0)
                throw new Exception("Unable to set default frequency");
            if (RtlSdrWrapper.rtlsdr_set_sample_rate(device, DefaultSamplerate) != 0)
                throw new Exception("Unable to set default samplerate");
            if (RtlSdrWrapper.rtlsdr_set_tuner_gain_mode(device, 0) != 0)
                throw new Exception("Unable to set auto gain mode");
            if (RtlSdrWrapper.rtlsdr_reset_buffer(device) != 0)
                throw new Exception("Unable to reset buffer");
        }

        public void CloseDevice()
        {
            if (RtlSdrWrapper.rtlsdr_close(device) != 0)
                throw new Exception("Unable to close device");
        }

        public void SetFrequency(uint frequencyInHz)
        {
            if (RtlSdrWrapper.rtlsdr_set_center_freq(device, frequencyInHz) != 0)
                throw new Exception("Unable to set frequency");
        }

        public void SetSampleRate(uint sampleRate)
        {
            if (RtlSdrWrapper.rtlsdr_set_sample_rate(device, sampleRate) != 0)
                throw new Exception("Unable to set samplerate");
        }

        public byte[] ReadSamplesSync(int sampleCount)
        {
            return ReadSamplesSyncInternal(sampleCount);
        }

        private unsafe byte[] ReadSamplesSyncInternal(int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            fixed (byte* p = buffer)
            {
                IntPtr ptr = (IntPtr)p;
                int nRead = 0;
                RtlSdrWrapper.rtlsdr_read_sync(device, ptr, bufferSize, out nRead);
                if (nRead != bufferSize)
                    throw new Exception("rtlsdr_read_sync");
            }
            return buffer;
        }

        public static IReadOnlyCollection<SDRDeviceDescription> GetConnectedDevices()
        {
            var deviceCount = RtlSdrWrapper.rtlsdr_get_device_count();
            List<SDRDeviceDescription> devices = new List<SDRDeviceDescription>((int)deviceCount);
            for (uint devIndex = 0; devIndex < deviceCount; devIndex++)
            {
                StringBuilder manufact = new StringBuilder(256);
                StringBuilder product = new StringBuilder(256);
                StringBuilder serial = new StringBuilder(256);
                RtlSdrWrapper.rtlsdr_get_device_usb_strings(devIndex, manufact, product, serial);
                string usbStrings;
                usbStrings = string.Format("{0}, {1}, SN: {2}", manufact, product, serial);
                devices.Add(new SDRDeviceDescription(RtlSdrWrapper.rtlsdr_get_device_name(devIndex), devIndex, usbStrings));
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
