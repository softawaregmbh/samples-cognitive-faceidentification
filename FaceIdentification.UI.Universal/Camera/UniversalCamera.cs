using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;

namespace FaceIdentification.UI.Universal.Camera
{
    public class UniversalCamera : ICamera
    {
        public async Task<byte[]> GetPhotoAsync()
        {
            var cameraUI = new CameraCaptureUI();
            var photo = await cameraUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
            using (var stream = await photo.OpenStreamForReadAsync())
            {
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }            
        }
    }
}
