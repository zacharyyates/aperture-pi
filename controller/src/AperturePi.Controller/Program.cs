using System;
using OpenCvSharp;
using System.Threading.Tasks;
using System.Threading;

namespace AperturePi.Controller
{
    class Program
    {
        // TODO -- setup that handles sigterm
        // https://dejanstojanovic.net/aspnet/2018/june/clean-service-stop-on-linux-with-net-core-21/

        static async Task Main(string[] args)
        {
            // simple test of OpenCV video stream capture

            // this should capture from the default webcam? at least that's what someone said online
            var capture = new VideoCapture(0);

            using (var output = Console.OpenStandardOutput())
            using (var window = new Window("capture"))
            using (var frame = new Mat()) {
                while (true) {
                    // gets a frame from the video
                    capture.Read(frame);
                    if (frame.Empty())
                        break;

                    // show in a desktop window for debugging
                    window.ShowImage(frame);
                    // pipe the output to vlc for RTSP serving
                    frame.WriteToStream(output);

                    Cv2.WaitKey(5);
                }
            }
        }
    }
}
