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

namespace BookingApp.WPF.View.Guest.GuestTools
{
    public class CreatedReservationsPDFGeneratorSerbian
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public User User { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }

        public CreatedReservationsPDFGeneratorSerbian(User user, DateTime startDate, DateTime endDate)
        {
            User = user;
            StartDate = startDate;
            EndDate = endDate;
            AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
        }
        public void MakeCreatedReservationsReport()
        {
            string directoryPath = "../../../Resources/PDFs/GuestPDFs";
            int index = 0;
            int maxIndex = 0;

            if (!Directory.Exists(directoryPath))
            {

                Directory.CreateDirectory(directoryPath);
            }

            var pdfFiles = Directory.GetFiles(directoryPath, "NapravljeneRezervacije_*.pdf");

            if (pdfFiles.Any())
            {
                maxIndex = pdfFiles
                    .Select(Path.GetFileNameWithoutExtension)
                    .Select(name => int.TryParse(name.Substring("NapravljeneRezervacije_".Length), out var number) ? number : 0)
                    .Max();
            }

            index = ++maxIndex;



            string filePath = Path.Combine(directoryPath, "NapravljeneRezervacije_" + index + ".pdf");
            Document pdfDoc = new Document();
            PdfWriter.GetInstance(pdfDoc, new FileStream(filePath, FileMode.Create));
            pdfDoc.Open();
            GeneratePdfContent(pdfDoc);
            pdfDoc.Close();

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

            Paragraph heading = new Paragraph("Napravljene rezervacije", headingFont)
            {
                Alignment = Element.ALIGN_CENTER
            };

            string dateRange = StartDate.ToString("dd/MM/yyyy") + " -> " + EndDate.ToString("dd/MM/yyyy");

            Paragraph timePeriodHeading = new Paragraph("Vremenski period: " + dateRange, subHeadingFont)
            {
                Alignment = Element.ALIGN_CENTER
            };
            Paragraph userHeading = new Paragraph("Korisnik: " + User.Username, subHeadingFont)
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

            Paragraph thankYou = new Paragraph("Hvala Vam što koristite našu aplikaciju!", subHeadingFont)
            {
                Alignment = Element.ALIGN_CENTER
            };

            pdfDoc.Add(thankYou);


        }

        private void CreateTable(Document pdfDoc)
        {
            BaseColor headerCellColor = new BaseColor(119, 71, 214);
            BaseColor cellColor = new BaseColor(227, 250, 255);

            PdfPTable table = new PdfPTable(5)
            {
                WidthPercentage = 95,
            };

            AddCellToHeader(table, "", headerCellColor);
            AddCellToHeader(table, "Smeštaj", headerCellColor);
            AddCellToHeader(table, "Lokacija", headerCellColor);
            AddCellToHeader(table, "Pocetni datum", headerCellColor);
            AddCellToHeader(table, "Krajnji datum", headerCellColor);

            List<AccommodationReservationViewModel> reservations = AccommodationReservationService.GetCreatedReservationsReport(User, StartDate, EndDate);
            int i = 0;
            foreach (AccommodationReservationViewModel reservation in reservations)
            {
                i++;
                AddCellToBody(table, i.ToString(), cellColor);
                AddCellToBody(table, reservation.AccommodationName, cellColor);
                AddCellToBody(table, reservation.Location, cellColor);
                AddCellToBody(table, reservation.StartDate.ToString("dd/MM/yyyy"), cellColor);
                AddCellToBody(table, reservation.EndDate.ToString("dd/MM/yyyy"), cellColor);


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
