namespace ffmpegClipper
{
    public interface IClipperListener
    {
        void OnStartCapture();
        void OnStopCapture();
    }
}
