using System.Diagnostics;
using System.IO;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.PageProperties;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.Styles.Appearance;

namespace BarcodeGeneration
{
    /// <summary>
    /// This sample shows how to generate CODE39 barcode using free ttf font.
    /// </summary>        
    class Program
    {
        static void Main(string[] args)
        {
            ResourceManager resourceManager = new ResourceManager();

            // register barcode font
            resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.Fonts.Font("barcodeFont", "..\\..\\fonts\\free3of9.ttf"));

            // register image resource
            resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("barcodeImage", "..\\..\\images\\apitron_code39.png"));

            FlowDocument document = new FlowDocument();
            document.Margin = new Thickness(5);

            // register style for barcode element, setting display.block on element means it will create a line break before and after.
            document.StyleManager.RegisterStyle(".barcode", new Style() { Font = new Apitron.PDF.Kit.Styles.Text.Font("barcodeFont", 50), Margin = new Thickness(0, 10, 0, 20), Display = Display.Block });

            // generate barcode
            document.Add(new TextBlock("The barcode below contains text APITRON"));
            document.Add(new TextBlock("*APITRON*") { Class = "barcode" });

            // add reference pre-generated barcode as image
            document.Add(new TextBlock("The barcode image below contains text APITRON"));
            document.Add(new Image("barcodeImage") { Class = "barcode" });

            string fileName = @"..\..\..\OutputDocuments\BarcodeGeneration.pdf";

            // save and open
            using(Stream stream = File.Create(fileName))
            {
                document.Write(stream,resourceManager, new PageBoundary(Boundaries.A4));
            }

            Process.Start(fileName);
        }
    }
}
