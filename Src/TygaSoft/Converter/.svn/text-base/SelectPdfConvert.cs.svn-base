using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TygaSoft.Model;
using SelectPdf;
using System.Web;

namespace TygaSoft.Converter
{
    public class SelectPdfConvert
    {
        public static void HtmlToPdf(HtmlToPdfInfo model)
        {
            var converter = new HtmlToPdf();
            var isCustom = false;
            if (model.Width > 0)
            {
                isCustom = true;
                converter.Options.WebPageWidth = model.Width;
            }
            if (model.Height > 0)
            {
                isCustom = true;
                converter.Options.WebPageHeight = model.Height;
            }
            else converter.Options.WebPageHeight = int.Parse(converter.Options.PdfPageCustomSize.Height.ToString());
            if (model.MarginTop > -1)
            {
                isCustom = true;
                converter.Options.MarginTop = model.MarginTop;
            }
            if (model.MarginRight > -1)
            {
                isCustom = true;
                converter.Options.MarginRight = model.MarginRight;
            }
            if (model.MarginBottom > -1)
            {
                isCustom = true;
                converter.Options.MarginBottom = model.MarginBottom;
            }
            if (model.MarginLeft > -1)
            {
                isCustom = true;
                converter.Options.MarginLeft = model.MarginLeft;
            }
            if (isCustom)
            {
                converter.Options.PdfPageSize = PdfPageSize.Custom;
                converter.Options.PdfPageCustomSize = new System.Drawing.SizeF(model.SizeFWidth > 0 ? model.SizeFWidth : converter.Options.WebPageWidth, model.SizeFHeight > 0 ? model.SizeFHeight : converter.Options.PdfPageCustomSize.Height);
                converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
                converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                converter.Options.WebPageFixedSize = true;
            }

            PdfDocument doc = converter.ConvertHtmlString(model.Html, model.BaseUrl);
            doc.Save(HttpContext.Current.Response, false, string.Format("{0}{1}.pdf", DateTime.Now.ToString("yyMMddHHmm"), (new Random().Next(999)).ToString().PadLeft(3, '0')));
            doc.Close();
        }
    }
}
