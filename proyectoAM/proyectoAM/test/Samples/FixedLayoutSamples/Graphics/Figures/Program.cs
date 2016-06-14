namespace Apitron.PDF.Kit.Samples
{
    using System.IO;
    using Apitron.PDF.Kit;
    using Apitron.PDF.Kit.FixedLayout;
    using Apitron.PDF.Kit.FixedLayout.PageProperties;
    using Apitron.PDF.Kit.FixedLayout.Content;
    using Apitron.PDF.Kit.FixedLayout.Resources.ColorSpaces.Device;

    /// <summary>
    /// This sample shows how to draw simple figures.
    /// All drawings within PDF document is being done using methods and properties of <see cref="FixedLayout.Content.Path"/> class.     
    /// You create a set of drawing commands and add the resulting path to the <see cref="Page.Content"/> by filling or stroking it.
    /// If you need to apply clipping, you may create <see cref="ClippedContent"/> object based on path and draw to this clipped content.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            string out_path = @"..\..\..\..\OutputDocuments\Figures.pdf";
            
            // create new PDF file
            using (FileStream fs = new FileStream(out_path, FileMode.Create))
            {
                // this object represents a PDF fixed document
                FixedDocument document = new FixedDocument();
                Page page = new Page(new PageBoundary(Boundaries.A4));

                string rgbID = "CS_RGB";
                document.ResourceManager.RegisterResource(new RgbColorSpace(rgbID));
                page.Content.SetNonStrokingColorSpace(rgbID);

                // draw circle
                page.Content.SetNonStrokingColor(0.33, 0.66, 0.33);
                page.Content.FillPath(FixedLayout.Content.Path.CreateCircle(300, 400, 50, false));

                // draw ellipse 
                page.Content.SetDeviceNonStrokingColor(0.2, 0.15, 0.98);
                page.Content.FillPath(FixedLayout.Content.Path.CreateEllipse(200, 400, 150, 50, false));

                // draw round rect
                page.Content.SetDeviceStrokingColor(0.29, 0.85, 0.18);
                page.Content.StrokePath(FixedLayout.Content.Path.CreateRoundRect(10, 10, 300, 300, 10, 10, 5, 5, false));

                document.Pages.Add(page);
                document.Save(fs);                                
            }

            System.Diagnostics.Process.Start(out_path);
        }
    }
}
