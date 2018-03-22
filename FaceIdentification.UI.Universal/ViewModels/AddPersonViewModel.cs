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
    public class AddPersonViewModel : ViewModelBase
    {
        private ICamera camera;
        private IFaceRecognizer faceRecognizer;
        private string name;

        public AddPersonViewModel(ICamera camera, IFaceRecognizer faceRecognizer)
        {
            this.camera = camera;

            this.faceRecognizer = faceRecognizer;

            this.AddPersonCommand = new RelayCommand(async () =>
            {
                var photo = await this.camera.GetPhotoAsync();
                await this.faceRecognizer.AddImageAsync(photo, this.Name);
            });
        }

        public string Name
        {
            get { return name; }
            set { this.Set(ref name, value); }
        }

        public ICommand AddPersonCommand { get; private set; }
    }
}
