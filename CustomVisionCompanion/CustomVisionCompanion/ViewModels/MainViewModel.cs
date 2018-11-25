using CustomVisionCompanion.Common;
using CustomVisionCompanion.Services;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.CustomVisionEngine;
using Plugin.CustomVisionEngine.Models;
using GalaSoft.MvvmLight.Command;

namespace CustomVisionCompanion.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMediaService mediaService;

        private IEnumerable<string> predictions;

        private IEnumerable<string> carPredictions;

        private const string  panamera = "Porsche Panamera";
        private const string nineOneOne = "Porsche 911 Carrera";
        private const string sevenOneEight = "Porsche 718 Boxter";
        private const string cayenne = "cayenne";
        private const string notPorsche = "not porsche";


        public IEnumerable<string> CarPredictions
        {
            get => carPredictions;
            set => Set(ref carPredictions, value);
        }

        public ViewModelCar Car
        {
            get;
            set;
        }

        public IEnumerable<string> Predictions
        {
            get => predictions;
            set => Set(ref predictions, value);
        }

        private bool isOffline = true;
        public bool IsOffline
        {
            get => isOffline;
            set => Set(ref isOffline, value);
        }

        private string imagePath;
        public string ImagePath
        {
            get => imagePath;
            set => Set(ref imagePath, value);
        }

        public AutoRelayCommand TakePhotoCommand { get; private set; }

        public AutoRelayCommand PickPhotoCommand { get; private set; }

        public AutoRelayCommand SettingsCommand { get; private set; }

        public MainViewModel(IMediaService mediaService)
        {
            this.mediaService = mediaService;

            CreateCommands();
        }

        private void CreateCommands()
        {
            TakePhotoCommand = new AutoRelayCommand(async () => await AnalyzePhotoAsync(() => mediaService.TakePhotoAsync()));
            PickPhotoCommand = new AutoRelayCommand(async () => await AnalyzePhotoAsync(() => mediaService.PickPhotoAsync()));
            SettingsCommand = new AutoRelayCommand(() => NavigationService.NavigateTo(Constants.SettingsPage));
        }

        private async Task AnalyzePhotoAsync(Func<Task<MediaFile>> action)
        {
            IsBusy = true;

            try
            {
                var file = await action.Invoke();
                if (file != null)
                {
                    // Clean up previous results.
                    Predictions = null;
                    ImagePath = file.Path;

                    // Check whether to use the online or offline version of the prediction model.
                    IEnumerable<Recognition> predictionsRecognized = null;
                    IEnumerable<Recognition> predictionsCarRecognized = null;
                    if (isOffline)
                    {
                        var classifier = CrossOfflineClassifier.Current;
                        predictionsRecognized = await classifier.RecognizeAsync(file.GetStream(), file.Path);
                        var predictionsRec = predictionsRecognized.ToArray();
                        if (predictionsRec[0].Tag == "cars" && predictionsRec[0].Probability > 0.7)
                        {
                            var carClassifier = CrossOfflineCarClassifier.Current;
                            predictionsCarRecognized = await carClassifier.RecognizeAsync(file.GetStream(), file.Path);
                            CarPredictions = predictionsCarRecognized.Select(p => $"{p.Tag}: {p.Probability:P1}");
                            var tag = predictionsCarRecognized.Where(t => t.Probability > 0.7)
                                .OrderByDescending(prp => prp.Probability)
                                .FirstOrDefault().ToString();
                            switch (tag)
                            {
                                case panamera:
                                    Car = new ViewModelCar()
                                    {
                                        Accelaration = "5.7 s",
                                        Power= "243 kW/330 hp",
                                        Rpm = "6,000 r/min	",
                                        TopSpeed = "264 km/h",
                                        Price = "90.655,00 €"
                                    };
                                    break;
                                case nineOneOne:
                                    Car = new ViewModelCar()
                                    {
                                        Accelaration = "4.6 s",
                                        Power = "272 kW/370 PS",
                                        Rpm = "6,500 r/min	",
                                        TopSpeed = "295 km/h",
                                        Price = "114.880,00 €"
                                    };
                                    break;
                                case sevenOneEight:
                                    Car = new ViewModelCar()
                                    {
                                        Accelaration = "4.9 s",
                                        Power = "220 kW/300 hp",
                                        Rpm = "6,500 r/min",
                                        TopSpeed = "275 km/h",
                                        Price = "58.900,00 €"
                                    };
                                    break;
                                case cayenne:
                                    Car = new ViewModelCar()
                                    {
                                        Accelaration = "6.2 s",
                                        Power = "250 kW/340 PS",
                                        Rpm = "6,500 r/min",
                                        TopSpeed = "245 km/h",
                                        Price = "74.828,00 €"
                                    };
                                    break;

                                default:
                                    break;
                            }
                        }
                        else
                        {
                            CarPredictions = Enumerable.Empty<string>();
                        }

                    }
                    else
                    {
                        var classifier = CrossOnlineClassifier.Current;
                        predictionsRecognized = await classifier.RecognizeAsync(SettingsService.PredictionKey, Guid.Parse(SettingsService.ProjectId), file.GetStream(), null);
                    }

                    Predictions = predictionsRecognized.Select(p => $"{p.Tag}: {p.Probability:P1}");


                    file.Dispose();
                }
            }
            catch (Exception ex)
            {
                await DialogService.AlertAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
