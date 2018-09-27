using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXing.Common;
using ZXing;

namespace TygaSoft.Converter
{
    public class ZxingHelper
    {
        public static void CreateBarcode(string toFile, BarcodeInfo model)
        {
            EncodingOptions options = null;
            options = new EncodingOptions
            {
                Width = model.Width,
                Height = model.Height,
                Margin = model.Margin
            };

            var writer = new BarcodeWriter();
            writer.Options = options;
            writer.Format = (BarcodeFormat)Enum.Parse(typeof(BarcodeFormat), model.BarcodeFormat);
            var bitmap = writer.Write(model.Barcode);

            bitmap.Save(toFile);
        }
    }
}
