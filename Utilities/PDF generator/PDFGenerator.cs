using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BookingApp.Utilities.PDF_generator
{
    public static class PDFGenerator
    {
        [Obsolete]
        public static void Generate(DocumentData data, string filePath)
        {
            // Configure the QuestPDF license
            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);

                    // Set the background image to cover the entire page
                    page.Background()
                        .Image("../../../WPF/Resources/Images/guide_background.png", ImageScaling.Resize);

                    page.Header()
                        .Element(container => Header(container, data));

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Element(container => Content(container, data));

                    page.Footer()
                        .AlignCenter()
                        .Element(container => Footer(container, data));
                });
            });

            document.GeneratePdf(filePath);
        }

        private static void Header(IContainer container, DocumentData data)
        {
            container.Row(row =>
            {
                row.RelativeColumn(1)
                    .AlignLeft()
                    .Image(data.ApplicationLogoPath, ImageScaling.FitWidth);

                row.RelativeColumn(3)
                    .AlignCenter()
                    .Column(column =>
                    {
                        column.Item().Text(data.ApplicationName).FontSize(24).Bold().FontColor(Colors.Black);
                        column.Item().Text("Tour Statistics").FontSize(18).FontColor(Colors.Black);
                        column.Item().Text("This report presents detailed statistics on the tours conducted by our guides, showcasing insights into the age distribution of attendees and the popularity of various tours over time. The visualizations provide a clear representation of the demographics of the tourists, divided into three age groups: below 18 years, between 18 and 50 years, and above 50 years. Additionally, the report highlights the most visited tours, giving a comprehensive overview of the tours' performance over the years.")
                            .FontSize(12).FontColor(Colors.Grey.Medium);
                    });
            });
        }

        private static void Content(IContainer container, DocumentData data)
        {
            container.Column(column =>
            {
                column.Spacing(20);

                column.Item().Text($"Tour Name: {data.TourName}").FontSize(16).Bold().FontColor(Colors.Black);

                column.Item().Row(row =>
                {
                    // Adjust the column widths to shift the pie chart a bit to the left and make the legend wider
                    row.RelativeColumn(2)
                        .Image(data.PieChart, ImageScaling.FitWidth);

                    row.RelativeColumn(1)  // Increasing the width for the legend
                        .Column(legendColumn =>
                        {
                            legendColumn.Spacing(5);
                            legendColumn.Item().Text("Legend:").Bold().FontColor(Colors.Black).FontSize(14);

                            double total = data.Below18 + data.Between18And50 + data.Above50;
                            legendColumn.Item().Text($"Total Participants: {total}").Bold().FontColor(Colors.Black).FontSize(12);  // Adding total participants
                            legendColumn.Item().Text($"Below 18 years - {data.Below18} persons - {Math.Round((data.Below18 / total) * 100, 2)}%").FontColor(Colors.Black).FontSize(12);
                            legendColumn.Item().Text($"18 to 50 years - {data.Between18And50} persons - {Math.Round((data.Between18And50 / total) * 100, 2)}%").FontColor(Colors.Black).FontSize(12);
                            legendColumn.Item().Text($"Above 50 years - {data.Above50} persons - {Math.Round((data.Above50 / total) * 100, 2)}%").FontColor(Colors.Black).FontSize(12);
                        });
                });

                column.Item().Column(column =>
                {
                    column.Item().Text("Most Visited Tours").Bold().FontSize(18).FontColor(Colors.Black);
                    column.Item().Text($"All Time: {data.MostVisitedTours.AllTime}").FontColor(Colors.Black);
                    column.Item().Text($"In 2024: {data.MostVisitedTours.Year2024}").FontColor(Colors.Black);
                    column.Item().Text($"In 2023: {data.MostVisitedTours.Year2023}").FontColor(Colors.Black);
                });
            });
        }



        private static void Footer(IContainer container, DocumentData data)
        {
            var aliceBlueColor = Colors.Black;

            container.Row(row =>
            {
                // Left side: Contact Info
                row.RelativeColumn(1).Column(leftColumn =>
                {
                    leftColumn.Item().Text($"Email: {data.ContactEmail}").FontSize(12).FontColor(aliceBlueColor);
                    leftColumn.Item().Text($"Phone: {data.ContactPhone}").FontSize(12).FontColor(aliceBlueColor);
                });

                // Right side: Guide Info and Date
                row.RelativeColumn(1).AlignRight().Column(rightColumn =>
                {
                    rightColumn.Item().Text($"Published By: {data.GuideName}").FontSize(12).FontColor(aliceBlueColor);
                    rightColumn.Item().Text($"Generated on: {data.GeneratedTime:g}").FontSize(12).FontColor(aliceBlueColor);
                });
            });
        }


    }
}
