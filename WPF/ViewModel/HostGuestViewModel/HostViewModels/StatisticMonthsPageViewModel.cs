using BookingApp.Application.Services.RateServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.ComponentModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using BookingApp.WPF.View.HostPages;
using System.Windows.Navigation;
using BookingApp.WPF.View.Guest.GuestTools;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class StatisticMonthsPageViewModel: INotifyPropertyChanged
    {

        private string selectedYear;
        public string SelectedYear
        {
            set
            {
                if (selectedYear != value)
                {

                    selectedYear = value;
                    OnPropertyChanged("SelectedYear");
                }
            }
            get { return selectedYear; }
        }
    public AccommodationViewModel AccommodationViewModel { get; set; }

    public SeriesCollection SeriesCollection { get; set; }

    public SeriesCollection SeriesCollectionCancel { get; set; }

    public SeriesCollection SeriesCollectionRecommendation { get; set; }

    public int Year;
    public NavigationService NavService { get; set; }
    public string[] Months { get; set; }

    public string[] MonthsC { get; set; }

    public string[] MonthsR { get; set; }

    public string[] MonthsD { get; set; }

    public string[] AllMonths { get; set; }

     public string MostBusyMonth { get; set; }

    public Func<int, string> NumOfReservations { get; set; }

    public Func<int, string> NumOfCancellations { get; set; }

    public Func<int, string> NumOfRecommendation { get; set; }

    public MyICommand SelectionChangedCommand { get; set; }

    public AccommodationReservationService AccommodationReservationService { get; set; }

    public ReservationCancellationService ReservationCancellationService { get; set; }

    public DelayRequestService DelayRequestService { get; set; }

    public RenovationRecommendationService RenovationRecommendationService { get; set; }

    public User User { get; set; }

    public MyICommand GeneratePdf {  get; set; }
    public StatisticMonthsPageViewModel(User user, AccommodationViewModel acc, string yearString, NavigationService navService)
    {
        AccommodationViewModel = acc;
        AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
        ReservationCancellationService = new ReservationCancellationService(Injector.Injector.CreateInstance<IReservationCancellationRepository>());
        DelayRequestService = new DelayRequestService(Injector.Injector.CreateInstance<IDelayRequestRepository>());
        RenovationRecommendationService = new RenovationRecommendationService(Injector.Injector.CreateInstance<IRenovationRecommendationRepository>());
        User = user;
        NavService = navService;
        Year = Convert.ToInt32(yearString);
        SeriesCollection = new SeriesCollection();
        SeriesCollectionCancel = new SeriesCollection();
        SeriesCollectionRecommendation = new SeriesCollection();
        AddYChart();
        SelectedYear = yearString;
        Months = AccommodationReservationService.GetAllMonthsForAcc(acc.Id, Year).Select(i => GetMonthName(i)).ToArray();
        MonthsD = DelayRequestService.GetAllMonthsForAcc(acc.Id, Year).Select(i => GetMonthName(i)).ToArray();
        MonthsR = RenovationRecommendationService.GetAllMonthsForAcc(acc.Id, Year).Select(i => GetMonthName(i)).ToArray();
        MonthsC = ReservationCancellationService.GetAllMonthsForAcc(acc.Id, Year).Select(i => GetMonthName(i)).ToArray();
        AllMonths = MonthsC.Concat(MonthsD).ToArray();
        NumOfReservations = value => value.ToString("N");
        NumOfCancellations = value => value.ToString("N");
        NumOfRecommendation = value => value.ToString("N");
        MostBusyMonth = GetMonthName(AccommodationReservationService.GetMostBusyMonth(acc.Id, Year));
        GeneratePdf = new MyICommand(GeneratePDF);
        SelectionChangedCommand = new MyICommand(NavigatePage);
        }

    private void AddYChart()
    {
        SeriesCollection.Add(new LineSeries
        { 
            Title = "Number of reservations",
            Values = new ChartValues<int>(AccommodationReservationService.GetAllReservationsForMonths(AccommodationViewModel.Id, Year))
        });

        SeriesCollectionCancel.Add(new ColumnSeries
        {
            Title = "Number of reservation's delay",
            Values = new ChartValues<int>(DelayRequestService.GetAllDelaysForMonths(AccommodationViewModel.Id, Year))
        });

        SeriesCollectionCancel.Add(new ColumnSeries
        {
            Title = "Number of cancelling reservations",
            Values = new ChartValues<int>(ReservationCancellationService.GetAllCancellationsForMonths(AccommodationViewModel.Id, Year))
        });

        SeriesCollectionRecommendation.Add(new ColumnSeries
        {
            Title = "Number of renovation recommendation",
            Values = new ChartValues<int>(RenovationRecommendationService.GetAllRecommendationForMonths(AccommodationViewModel.Id, Year))
        });



    }
        public string GetMonthName(int monthNumber)
        {
            if (monthNumber < 1 || monthNumber > 12)
            {
                return "Invalid Month";
            }

            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            return cultureInfo.DateTimeFormat.GetMonthName(monthNumber);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NavigatePage()
        {

            if (SelectedYear != "All")
            {
                StatisticMonthsPage page = new StatisticMonthsPage(User, AccommodationViewModel, SelectedYear, NavService);
                this.NavService.Navigate(page);
            }
            else
            {
                StatisticYearsPage page = new StatisticYearsPage(User, AccommodationViewModel, NavService);
                this.NavService.Navigate(page);
            }

        }

        public void GeneratePDF()
        {
            string directoryPath;
            int index = 0;
            int maxNumber = 0;


            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Save PDF File";
                saveFileDialog.FileName = "StatisticMonth_1.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    directoryPath = Path.GetDirectoryName(saveFileDialog.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);

                    var pdfFiles = Directory.GetFiles(directoryPath, $"{fileName}_*.pdf");

                    Regex regex = new Regex($@"{fileName}_(\d+).pdf");
                    if (pdfFiles.Any())
                    {
                        maxNumber = pdfFiles
                            .Select(Path.GetFileNameWithoutExtension)
                            .Select(name => int.TryParse(regex.Match(name).Groups[1].Value, out var number) ? number : 0)
                            .Max();
                    }
                    index = ++maxNumber;

                    string filePath = Path.Combine(directoryPath, $"{fileName}_{index}.pdf");
                    Document pdfDoc = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(filePath, FileMode.Create));
                    string backgroundImagePath = "../../../WPF/Resources/Images/pink8.jpg";
                    writer.PageEvent = new BackgroundImageHandler(backgroundImagePath);
                    pdfDoc.Open();
                    GeneratePdfContent(pdfDoc);
                    pdfDoc.Close();
                    
                }
                else
                {

                    return;
                }
            }
        }

        public void GeneratePdfContent(Document pdfDoc)
        {

            string imagePath = "../../../WPF/Resources/Images/logo.png";
            Image img = Image.GetInstance(imagePath);
            img.ScaleToFit(110f, 110f);


            float imageHeight = img.ScaledHeight;
            float imageWidth = img.ScaledWidth;
            float imageX = 25;
            float imageY = pdfDoc.PageSize.Height - imageHeight - 30;
            img.SetAbsolutePosition(imageX, imageY);
            pdfDoc.Add(img);


            string accommodationName = AccommodationViewModel.Name;
            string titleText = $"{accommodationName} – all months statistic";


            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 22);
            iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph(titleText, titleFont);


            title.IndentationLeft = imageX + imageWidth + 10;
            title.SpacingAfter = 18f;

            pdfDoc.Add(title);

            string accommodationCity = AccommodationViewModel.City;
            string accommodationCountry = AccommodationViewModel.Country;
            string accommodationType = AccommodationViewModel.Type.ToString().ToLower();
            string accommodationMax = AccommodationViewModel.MaxGuestNumber.ToString();
            string accommodationCancel = AccommodationViewModel.ReservationDaysLimit.ToString();
            string paragraphText = "" + $"{accommodationName} is located in " + $"{accommodationCity}, "
                + $"{accommodationCountry} and is one of the most famous " + $"{accommodationType}s. " +
                $"It can accommodate " + $"{accommodationMax} people and it is possible to cancel the reservation" +
                $" even " + $"{accommodationCancel} day before the start of the reservation.";
            Font paragraphFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(paragraphText, paragraphFont);
            paragraph.Alignment = Element.ALIGN_LEFT;


            paragraph.IndentationLeft = title.IndentationLeft;


            pdfDoc.Add(paragraph);

            AddTableToPdf(pdfDoc);
            string yearString = MostBusyMonth.ToString();
            string additionalParagraphText = "The highest occupation of the " + $"{accommodationName} was in " + $"{yearString}.";
            iTextSharp.text.Paragraph additionalParagraph = new iTextSharp.text.Paragraph(additionalParagraphText, paragraphFont);
            additionalParagraph.Alignment = Element.ALIGN_LEFT;
            additionalParagraph.SpacingBefore = 20f;
            additionalParagraph.IndentationLeft = 53f;
            pdfDoc.Add(additionalParagraph);

        }

        public void AddTableToPdf(Document pdfDoc)
        {
            PdfPTable table = new PdfPTable(5);
            table.SpacingBefore = 40f;


            table.AddCell(new PdfPCell());
            int num = 1;


            PdfPCell cell = new PdfPCell(new Phrase("Number of reservations"));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = new BaseColor(132, 205, 245);
            cell.BorderColor = BaseColor.BLACK;
            cell.Padding = 8;
            cell.MinimumHeight = 30;
            cell.Phrase.Font.Size = 11;
            table.AddCell(cell);

            PdfPCell cell1 = new PdfPCell(new Phrase("Number of reservation’s delay"));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell1.BackgroundColor = new BaseColor(132, 205, 245);
            cell1.BorderColor = BaseColor.BLACK;
            cell1.Padding = 8;
            cell1.MinimumHeight = 30;
            cell1.Phrase.Font.Size = 11;
            table.AddCell(cell1);

            PdfPCell cell2 = new PdfPCell(new Phrase("Number of cancelling reservations"));
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell2.BackgroundColor = new BaseColor(132, 205, 245);
            cell2.BorderColor = BaseColor.BLACK;
            cell2.Padding = 8;
            cell2.MinimumHeight = 30;
            cell2.Phrase.Font.Size = 11;
            table.AddCell(cell2);

            PdfPCell cell3 = new PdfPCell(new Phrase("Number of renovation recommendation"));
            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell3.BackgroundColor = new BaseColor(132, 205, 245);
            cell3.BorderColor = BaseColor.BLACK;
            cell3.Padding = 8;
            cell3.MinimumHeight = 30;
            cell3.Phrase.Font.Size = 11;
            table.AddCell(cell3);

            for (int row = 0; row < 12; row++)
            {

                PdfPCell rowTitleCell = new PdfPCell(new Phrase(GetMonthName(num)));
                rowTitleCell.HorizontalAlignment = Element.ALIGN_CENTER;
                rowTitleCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                rowTitleCell.BackgroundColor = new BaseColor(132, 205, 245);
                rowTitleCell.BorderColor = BaseColor.BLACK;
                rowTitleCell.Padding = 8;
                rowTitleCell.MinimumHeight = 30;
                rowTitleCell.Phrase.Font.Size = 11;
                table.AddCell(rowTitleCell);


                for (int col = 0; col < 4; col++)
                {

                    int value = getForSpecificRowAndColumn(num, col);
                    PdfPCell valueCell = new PdfPCell(new Phrase(value.ToString()));
                    valueCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    valueCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    valueCell.BackgroundColor = BaseColor.WHITE;
                    valueCell.BorderColor = BaseColor.BLACK;
                    valueCell.Padding = 8;
                    valueCell.MinimumHeight = 30;
                    valueCell.Phrase.Font.Size = 11;
                    table.AddCell(valueCell);
                }
                num++;
            }
            pdfDoc.Add(table);


        }


        public int getForSpecificRowAndColumn(int num, int column)
        {
            int value;
            if (column == 0)
                value = AccommodationReservationService.GetNumOfReservationsByMonth(AccommodationViewModel.Id, num, Year);
            else if (column == 1)
                value = DelayRequestService.GetNumOfDelaysByMonth(AccommodationViewModel.Id, num, Year);
            else if (column == 2)
                value = ReservationCancellationService.GetNumOfCancellationsByMonth(AccommodationViewModel.Id, num, Year);
            else
                value = RenovationRecommendationService.GetNumOfRecommendationsByMonth(AccommodationViewModel.Id, num, Year);

            return value;
        }
    }




}

