﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceIdentification.UI.Universal.Camera
{
    public interface ICamera
    {
        Task<byte[]> GetPhotoAsync();
    }
}
