﻿using FaceIdentification.UI.Universal.Camera;
using FaceIdentification.UI.Universal.ViewModels;
using FaceIdentification.CognitiveServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FaceIdentification.UI.Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainViewModel viewModel;
        private IFaceRecognizer faceRecognizer;

        public MainPage()
        {
            this.InitializeComponent();

            this.viewModel = new MainViewModel(new UniversalCamera());
            this.DataContext = this.viewModel;
        }
    }
}
