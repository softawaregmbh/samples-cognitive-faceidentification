using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaceIdentification
{
    public interface IFaceRecognizer
    {
        Task<bool> IsGroupExistingAsync();

        Task CreateGroupAsync();

        Task<IEnumerable<Face>> RecognizeImageAsync(byte[] image);

        Task AddImageToExistingPersonAsync(Guid personId, byte[] image, string userData = null);

        Task<Guid> AddImageAsync(byte[] image, string name, string userData = null);

    }
}
