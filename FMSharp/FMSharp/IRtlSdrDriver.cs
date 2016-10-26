namespace FMSharp
{
    public interface IRtlSdrDriver
    {
        void OpenDevice(uint deviceIndex = 0);
        void CloseDevice();
        void SetFrequency(uint frequencyInHz);
        void SetSampleRate(uint sampleRate);
        byte[] ReadSamplesSync(int sampleCount);
    }
}