namespace ffmpegClipper
{
    public interface IClipperListener
    {
        void OnStart();
        void OnInterrupt();
        void OnRunnerComplete();
    }
}
