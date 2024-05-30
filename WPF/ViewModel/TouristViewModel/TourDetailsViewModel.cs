using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.View.TouristWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight.Messaging;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;
using Syncfusion.Pdf;

namespace BookingApp.WPF.ViewModel.GuideTouristViewModel
{
    public class TourDetailsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Checkpoint> CheckpointWithColors { get; set; }

        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(param => CloseWindow());
                }
                return _closeCommand;
            }
        }

        private void CloseWindow()
        {
            Messenger.Default.Send(new CloseWindowMessage());
        }

        private ICommand _exportToPDFCommand;
        public ICommand ExportToPDFCommand
        {
            get
            {
                if (_exportToPDFCommand == null)
                {
                    _exportToPDFCommand = new RelayCommand(param => ExportToPDF());
                }
                return _exportToPDFCommand;
            }
        }

        [Obsolete]
        private void ExportToPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var directoryPath = "../../../Resources/PDFs";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var pdfFiles = Directory.GetFiles(directoryPath, "TourDetails_*.pdf");
            int maxNumber = 0;
            if (pdfFiles.Any())
            {
                maxNumber = pdfFiles
                    .Select(Path.GetFileNameWithoutExtension)
                    .Select(name => int.TryParse(name.Substring("TourDetails_".Length), out var number) ? number : 0)
                    .Max();
            }

            String defPath = "../../../WPF/";
            Document
                .Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(QuestPDF.Helpers.Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(20));

                        page.Header().Stack(stack =>
                        {
                            stack.Item().Row(row =>
                            {
                                row.ConstantColumn(150).AlignLeft().Container().Height(80).Image("../../../WPF/Resources/Images/logo.png", ImageScaling.FitArea);
                                row.RelativeColumn().AlignLeft().Text("Tour Details")
                                    .SemiBold().FontSize(36).FontColor(QuestPDF.Helpers.Colors.Blue.Medium);
                            });
                            stack.Item().Row(row =>
                            {
                                row.ConstantColumn(200).AlignLeft().Stack(stack =>
                                {
                                    stack.Item().Text("Date created: " + DateTime.Now.ToString("yyyy-MM-dd")).FontSize(14);
                                    stack.Item().Text("User: " + Username).FontSize(14);
                                });
                            });
                        });


                        //"../../../WPF/Resources/Images/logo.png"
                        page.Content()
                            .PaddingVertical(1, Unit.Centimetre)
                            .Column(x =>
                            {
                                x.Spacing(20);

                                x.Item().Row(row =>
                                {
                                    row.ConstantColumn(20).PaddingLeft(20);
                                    row.RelativeColumn().Stack(stack =>
                                    {
                                        stack.Spacing(10);

                                        stack.Item().Row(innerRow =>
                                        {
                                            innerRow.ConstantColumn(120).Text("Tour Name:").FontSize(18);
                                            innerRow.RelativeColumn().Text(SelectedTour.Name).FontSize(18);
                                        });
                                        stack.Item().Row(innerRow =>
                                        {
                                            innerRow.ConstantColumn(120).Text("Country:").FontSize(18);
                                            innerRow.RelativeColumn().Text(SelectedTour.Country).FontSize(18);
                                        });
                                        stack.Item().Row(innerRow =>
                                        {
                                            innerRow.ConstantColumn(120).Text("City:").FontSize(18);
                                            innerRow.RelativeColumn().Text(SelectedTour.City).FontSize(18);
                                        });
                                        stack.Item().Row(innerRow =>
                                        {
                                            innerRow.ConstantColumn(120).Text("Language:").FontSize(18);
                                            innerRow.RelativeColumn().Text(SelectedTour.Language).FontSize(18);
                                        });
                                        stack.Item().Row(innerRow =>
                                        {
                                            innerRow.ConstantColumn(120).Text("Date:").FontSize(18);
                                            innerRow.RelativeColumn().Text(SelectedTour.Date).FontSize(18);
                                        });
                                        stack.Item().Row(innerRow =>
                                        {
                                            innerRow.ConstantColumn(120).Text("Status:").FontSize(18);
                                            innerRow.RelativeColumn().Text(SelectedTour.Status).FontSize(18);
                                        });
                                        stack.Item().PaddingTop(20).Text("Key Points: ");
                                        foreach (var checkpoint in CheckpointWithColors)
                                        {
                                            stack.Item().PaddingLeft(80).Text(checkpoint.Name).FontSize(16);
                                        }
                                    });
                                });
                                if (SelectedTour.Pictures != null || !SelectedTour.Pictures.Contains("no_image.jpg"))
                                {
                                    int imageCount = 0;
                                    x.Item().Row(row =>
                                    {
                                        row.Spacing(20);
                                        foreach (var picture in SelectedTour.Pictures)
                                        {
                                            if (imageCount >= 2) break;
                                            String p = picture.Replace("../../", "../../../WPF/");
                                            row.RelativeColumn().Container().Height(180).Width(200).Image(p.Replace("\\", "/"), ImageScaling.FitArea);
                                            imageCount++;
                                        }
                                    });
                                }
                            });


                    });
                })
                .GeneratePdf($"{directoryPath}\\TourDetails_{maxNumber + 1}.pdf");
            Messenger.Default.Send(new NotificationMessage($"A PDF is created on path: Resources/PDFs/TourDetails_{maxNumber + 1}.pdf"));
        }

        private TourViewModel _selectedTour;
        public TourViewModel SelectedTour
        {
            get
            {
                return _selectedTour;
            }
            set
            {
                if (value != _selectedTour)
                {
                    _selectedTour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if(_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        private Visibility _pdfPanel;
        public Visibility PdfPanel
        {
            get
            {
                return _pdfPanel;
            }
            set
            {
                if (_pdfPanel != value)
                {
                    _pdfPanel = value;
                    OnPropertyChanged(nameof(PdfPanel));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void TourDetailsWindowInitialization(bool IsMyTour, string username)
        {
            Username = username;
            InitializePdfPanel(IsMyTour);

            InitializeCheckpoints();

            InitializePictures();
        }
        private void InitializePictures()
        {
            var imagePlaceholder = "../../Resources/Images/no_image.jpg";
            if (SelectedTour.Pictures != null)
            {
                for (int i = 0; i < SelectedTour.Pictures.Count; i++)
                {
                    if (!SelectedTour.Pictures[i].StartsWith("../../"))
                        SelectedTour.Pictures[i] = "../../" + SelectedTour.Pictures[i];
                }
            }
            else
            {
                SelectedTour.Pictures = new List<string> { imagePlaceholder };
            }
        }
        private void InitializePdfPanel(bool IsMyTour)
        {
            PdfPanel = Visibility.Collapsed;
            PdfPanel = IsMyTour ? Visibility.Visible : PdfPanel;
        }
        private void InitializeCheckpoints()
        {
            SolidColorBrush activeColor = new SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString("#4F4A09"));
            SolidColorBrush inactiveColor = Brushes.LightGray;
            CheckpointWithColors.Clear();
            foreach (var checkpoint in SelectedTour.Checkpoints)
            {
                CheckpointWithColors.Add(new Checkpoint { Name = checkpoint, IndicatorColor = inactiveColor });
            }

            int checkpointIndex = SelectedTour.CurrentCheckpoint;
            for (int i = 0; i < SelectedTour.Checkpoints.Count; i++)
            {
                if (i == checkpointIndex)
                {
                    CheckpointWithColors[i].IndicatorColor = activeColor;
                }
                else
                {
                    CheckpointWithColors[i].IndicatorColor = inactiveColor;
                }
            }
        }

        public TourDetailsViewModel()
        {
            CheckpointWithColors = new ObservableCollection<Checkpoint>();
        }

        public class Checkpoint : INotifyPropertyChanged
        {
            private string name;
            public string Name
            {
                get { return name; }
                set { name = value; NotifyPropertyChanged(nameof(Name)); }
            }

            private Brush indicatorColor;
            public Brush IndicatorColor
            {
                get { return indicatorColor; }
                set { indicatorColor = value; NotifyPropertyChanged(nameof(IndicatorColor)); }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
