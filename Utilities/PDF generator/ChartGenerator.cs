using System.Collections.Generic;
using System.IO;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;

namespace BookingApp.Utilities.PDF_generator
{
    public static class ChartGenerator
    {
        public static byte[] GeneratePieChart(int below18, int between18And50, int above50)
        {
            var totalParticipants = below18 + between18And50 + above50;

            var model = new PlotModel
            {
                Title = "Visitors Age Distribution",
                TitleFontSize = 24, // Slightly reduce the font size if it's too dominant
                TextColor = OxyColors.Black,
                PlotAreaBorderColor = OxyColors.Transparent,
                Background = OxyColors.Transparent,
                TitlePadding = 24, // Increase title padding to give more space
                Padding = new OxyThickness(50) // Add padding around the plot area to avoid clipping
            };

            var pieSeries = new PieSeries
            {
                StrokeThickness = 2.0,
                InsideLabelPosition = 0.5, // Adjust label position within the slices
                AngleSpan = 360,
                StartAngle = 0,
                TextColor = OxyColors.Black,
                FontSize = 16, // Adjust font size to fit better
                Diameter = 1, // Reduce diameter to fit within the plot area
                InnerDiameter = 0.2
            };

            pieSeries.Slices.Add(new PieSlice("Below 18", below18) { IsExploded = true, Fill = OxyColor.FromRgb(255, 20, 147) }); // Deep Pink
            pieSeries.Slices.Add(new PieSlice("18 to 50", between18And50) { Fill = OxyColor.FromRgb(30, 144, 255) }); // Dodger Blue
            pieSeries.Slices.Add(new PieSlice("Above 50", above50) { Fill = OxyColor.FromRgb(255, 165, 0) }); // Orange

            model.Series.Add(pieSeries);

            using (var stream = new MemoryStream())
            {
                var exporter = new PngExporter { Width = 800, Height = 600 };
                exporter.Export(model, stream);
                return stream.ToArray();
            }
        }
    }
}
