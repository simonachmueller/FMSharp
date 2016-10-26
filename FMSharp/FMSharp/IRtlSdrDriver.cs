namespace FMSharp
{
    public interface IRtlSdrDriver
    {
        void OpenDevice(uint deviceIndex);
        void CloseDevice();
        void SetFrequency(uint frequencyInHz);
        void SetSampleRate(uint sampleRate);
    }
}