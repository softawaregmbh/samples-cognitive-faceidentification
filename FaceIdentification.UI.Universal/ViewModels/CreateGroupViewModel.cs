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
    public class CreateGroupViewModel : ViewModelBase
    {
        private IFaceRecognizer faceRecognizer;

        public CreateGroupViewModel(IFaceRecognizer faceRecognizer)
        {
            this.faceRecognizer = faceRecognizer;

            this.CreateGroupCommand = new RelayCommand(async () => await this.faceRecognizer.CreateGroupAsync());
        }

        public ICommand CreateGroupCommand;        
    }
}
