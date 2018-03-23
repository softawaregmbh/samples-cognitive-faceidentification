using FaceIdentification.CognitiveServices;
using FaceIdentification.UI.Universal.Camera;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FaceIdentification.UI.Universal.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string subscriptionKey;
        private AzureRegions azureRegion;
        private IEnumerable<AzureRegions> availableAzureRegions;

        private string personGroup;

        private CreateGroupViewModel createGroupViewModel;
        private AddPersonViewModel addPersonViewModel;
        private AddFaceViewModel addFaceViewModel;
        private RecognizeViewModel recognizeViewModel;

        private IFaceRecognizer faceRecognizer;

        private bool isInitialized;

        public MainViewModel(ICamera camera)
        {
#if DEBUG
            this.SubscriptionKey = "b05b96b84fbb4e72a070603cc0b9471d";
            this.AzureRegion = AzureRegions.Westeurope;
            this.PersonGroup = "demo";
#endif

            this.IsInitialized = false;

            this.AvailableAzureRegions = Enum.GetValues(typeof(AzureRegions)).Cast<AzureRegions>();
            this.LoginCommand = new RelayCommand(() =>
            {
                this.faceRecognizer = new FaceRecognizer(this.SubscriptionKey, this.AzureRegion, this.PersonGroup);
                this.CreateGroupViewModel = new CreateGroupViewModel(this.faceRecognizer);
                this.RecognizeViewModel = new RecognizeViewModel(camera, this.faceRecognizer);
                this.AddFaceViewModel = new AddFaceViewModel(camera, this.faceRecognizer);
                this.addPersonViewModel = new AddPersonViewModel(camera, this.faceRecognizer);

                this.IsInitialized = true;
            });
        }

        public AzureRegions AzureRegion
        {
            get { return azureRegion; }
            set { this.Set(ref azureRegion, value); }
        }

        public string PersonGroup
        {
            get { return personGroup; }
            set { this.Set(ref personGroup, value); }
        }

        public string SubscriptionKey
        {
            get { return subscriptionKey; }
            set { this.Set(ref subscriptionKey, value);  }
        }

        public IEnumerable<AzureRegions> AvailableAzureRegions
        {
            get { return availableAzureRegions; }
            set { this.Set(ref availableAzureRegions, value); }
        }

        public CreateGroupViewModel CreateGroupViewModel
        {
            get { return createGroupViewModel; }
            set { this.Set(ref createGroupViewModel, value); }
        }
       
        public RecognizeViewModel RecognizeViewModel
        {
            get { return recognizeViewModel; }
            set { this.Set(ref recognizeViewModel, value); }
        }
        
        public AddPersonViewModel MyProperty
        {
            get { return addPersonViewModel; }
            set { this.Set(ref addPersonViewModel, value); }
        }
        
        public AddFaceViewModel AddFaceViewModel
        {
            get { return addFaceViewModel; }
            set { this.Set(ref addFaceViewModel, value); }
        }
        
        public bool IsInitialized
        {
            get { return isInitialized; }
            set { this.Set(ref isInitialized, value); }
        }

        public ICommand LoginCommand { get; set; }
    }
}
