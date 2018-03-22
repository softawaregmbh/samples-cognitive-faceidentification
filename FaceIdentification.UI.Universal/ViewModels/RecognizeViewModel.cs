using FaceIdentification.UI.Universal.Camera;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FaceIdentification.UI.Universal.ViewModels
{
    public class RecognizeViewModel : ViewModelBase
    {
        private ICamera camera;
        private IFaceRecognizer faceRecognizer;

        public RecognizeViewModel(ICamera camera, IFaceRecognizer faceRecognizer)
        {
            this.camera = camera;

            this.faceRecognizer = faceRecognizer;

            this.TakePhotoCommand = new RelayCommand(async () =>
            {
                var photo = await this.camera.GetPhotoAsync();
                var faces = await this.faceRecognizer.RecognizeImageAsync(photo);

                this.RecognizedFaces.Clear();
                foreach (var face in faces)
                {
                    this.RecognizedFaces.Add(face);
                }
            });

            this.RecognizedFaces = new ObservableCollection<Face>();
        }
        
        private ObservableCollection<Face> recognizedFaces;

        public ObservableCollection<Face> RecognizedFaces
        {
            get { return recognizedFaces; }
            set { this.Set(ref recognizedFaces, value); }
        }

        public ICommand TakePhotoCommand { get; private set; }
    }
}
