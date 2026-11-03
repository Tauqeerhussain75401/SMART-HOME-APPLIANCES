using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge;
using AForge.Video ;
using AForge.Video .DirectShow ;
namespace Classes
{
    
    class Camera
    {
        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice  FinalFrame;
        void Load()
        {
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo  Device in CaptureDevice )
            {
                
            }
            FinalFrame = new VideoCaptureDevice(); 
        }
        void Click()
        {
            FinalFrame = new VideoCaptureDevice(CaptureDevice[0].MonikerString  );
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.Start();  
        }

        void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            
        }
    }
}
