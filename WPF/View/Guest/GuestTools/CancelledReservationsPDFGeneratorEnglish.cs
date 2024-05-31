using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model.Features;
using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.Model.Reservations;

namespace BookingApp.WPF.View.Guest.GuestTools
{
    public class CancelledReservationsPDFGeneratorEnglish
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public User User { get; set; }
        public string SavedPath { get; set; }
        public ReservationCancellationService ReservationCancellationService { get; set; }
     

        public AccommodationService AccommodationService { get; set; }
        public CancelledReservationsPDFGeneratorEnglish(User user, DateTime startDate, DateTime endDate)
        {
            User = user;
            StartDate = startDate;
            EndDate = endDate;
            ReservationCancellationService = new ReservationCancellationService(Injector.Injector.CreateInstance<IReservationCancellationRepository>());
            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());

           
        }
        public void MakeCancelledReservationsReport()
        {
            string directoryPath = "../../../Resources/PDFs/GuestPDFs";
            int index = 0;
            int maxIndex = 0;

            if (!Directory.Exists(directoryPath))
            {

                Directory.CreateDirectory(directoryPath);
            }

            var pdfFiles = Directory.GetFiles(directoryPath, "CancelledReservations_*.pdf");

            if (pdfFiles.Any())
            {
                maxIndex = pdfFiles
                    .Select(Path.GetFileNameWithoutExtension)
                    .Select(name => int.TryParse(name.Substring("CancelledReservations_".Length), out var number) ? number : 0)
                    .Max();
            }

            index = ++maxIndex;



            string filePath = Path.Combine(directoryPath, "CancelledReservations_" + index + ".pdf");
            Document pdfDoc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(filePath, FileMode.Create));
            string backgroundImagePath = "../../../WPF/Resources/Images/reportBackgroundGuest.png";
            writer.PageEvent = new BackgroundImageHandler(backgroundImagePath);
            pdfDoc.Open();
            GeneratePdfContent(pdfDoc);
            pdfDoc.Close();
            SavedPath = "Resources/PDFs/GuestResources/CancelledReservations_" + index + ".pdf";

        }

        private void GeneratePdfContent(Document pdfDoc)
        {

            string imagePath = "../../../WPF/Resources/Images/logo.png";
            Image img = Image.GetInstance(imagePath);
            img.ScaleToFit(110f, 110f);

            img.SetAbsolutePosition(25, pdfDoc.PageSize.Height - img.ScaledHeight);


            pdfDoc.Add(img);

            System.Drawing.Color drawingColor = System.Drawing.Color.FromArgb(73, 44, 130);
            BaseColor fontColor = new BaseColor(drawingColor.R, drawingColor.G, drawingColor.B);
            Font headingFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 22, fontColor);
            Font subHeadingFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, fontColor);

            Paragraph heading = new Paragraph("Cancelled reservations", headingFont)
            {
                Alignment = Element.ALIGN_CENTER
            };

            string dateRange = StartDate.ToString("MM/dd/yyyy") + " -> " + EndDate.ToString("MM/dd/yyyy");

            Paragraph timePeriodHeading = new Paragraph("Time period: " + dateRange, subHeadingFont)
            {
                Alignment = Element.ALIGN_CENTER
            };
            Paragraph userHeading = new Paragraph("User: " + User.Username, subHeadingFont)
            {
                Alignment = Element.ALIGN_CENTER
            };



            pdfDoc.Add(heading);
            pdfDoc.Add(timePeriodHeading);
            pdfDoc.Add(userHeading);
            pdfDoc.Add(new Paragraph(" "));
            pdfDoc.Add(new Paragraph(" "));

            CreateTable(pdfDoc);

            pdfDoc.Add(new Paragraph(" "));
            pdfDoc.Add(new Paragraph(" "));

            Paragraph thankYou = new Paragraph("Thank you for using our application!", subHeadingFont)
            {
                Alignment = Element.ALIGN_CENTER
            };

            pdfDoc.Add(thankYou);


        }

        private void CreateTable(Document pdfDoc)
        {
            BaseColor headerCellColor = new BaseColor(119, 71, 214);
            BaseColor cellColor = new BaseColor(227, 250, 255, 191);

            PdfPTable table = new PdfPTable(5)
            {
                WidthPercentage = 95,
            };

            AddCellToHeader(table, "", headerCellColor);
            AddCellToHeader(table, "Accommodation", headerCellColor);
            AddCellToHeader(table, "Location", headerCellColor);
            AddCellToHeader(table, "Start date", headerCellColor);
            AddCellToHeader(table, "End date", headerCellColor);

            List<ReservationCancellation> reservations = ReservationCancellationService.GetCancelledReservationsReport(User, StartDate, EndDate);
            int i = 0;
            foreach (ReservationCancellation reservation in reservations)
            {
                i++;
                AddCellToBody(table, i.ToString(), cellColor);
                Accommodation acc = AccommodationService.GetById(reservation.AccommodationId);
                string accommodationName = acc.Name;
                string location = acc.City + ", " + acc.Country;
                AddCellToBody(table, accommodationName, cellColor);
                AddCellToBody(table, location, cellColor);
                AddCellToBody(table, reservation.StartDate.ToString("MM/dd/yyyy"), cellColor);
                AddCellToBody(table, reservation.EndDate.ToString("MM/dd/yyyy"), cellColor);


            }
            pdfDoc.Add(table);
        }

        private void AddCellToBody(PdfPTable table, string v, BaseColor cellColor)
        {
            Font font = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);
            PdfPCell cell = new PdfPCell(new Phrase(v, font))
            {
                BackgroundColor = cellColor,
                BorderColor = BaseColor.WHITE,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            };
            table.AddCell(cell);
        }

        private void AddCellToHeader(PdfPTable table, string v, BaseColor headerCellColor)
        {
            Font font = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.WHITE);
            PdfPCell cell = new PdfPCell(new Phrase(v, font))
            {
                BackgroundColor = headerCellColor,
                BorderColor = BaseColor.WHITE,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5

            };

            table.AddCell(cell);
        }
    }
}
