using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.View.Guest.GuestTools
{
    class BackgroundImageHandler : PdfPageEventHelper
    {
        private Image _backgroundImage;

        public BackgroundImageHandler(string imagePath)
        {
            _backgroundImage = Image.GetInstance(imagePath);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfContentByte canvas = writer.DirectContentUnder;
            _backgroundImage.SetAbsolutePosition(0, 0);
            _backgroundImage.ScaleToFit(document.PageSize.Width, document.PageSize.Height);
            canvas.AddImage(_backgroundImage);
        }
    }
}
