using FaceIdentification.UI.Universal.Camera;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FaceIdentification.UI.Universal.ViewModels
{
    public class AddFaceViewModel : ViewModelBase
    {
        private ICamera camera;
        private IFaceRecognizer faceRecognizer;
        private Guid selectedPerson;

        public AddFaceViewModel(ICamera camera, IFaceRecognizer faceRecognizer)
        {
            this.camera = camera;

            this.faceRecognizer = faceRecognizer;

            this.AddPhotoCommand = new RelayCommand(async () =>
            {
                var photo = await this.camera.GetPhotoAsync();
                await this.faceRecognizer.AddImageToExistingPersonAsync(this.SelectedPerson, photo);
            });
        }

        public Guid SelectedPerson
        {
            get { return selectedPerson; }
            set { this.Set(ref selectedPerson, value); }
        }

        public ICommand AddPhotoCommand { get; private set; }
    }
}
