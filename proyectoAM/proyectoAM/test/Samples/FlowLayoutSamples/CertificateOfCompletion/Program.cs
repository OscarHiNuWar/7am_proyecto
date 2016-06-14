using System.Diagnostics;
using System.IO;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.PageProperties;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.FixedLayout.Resources.Fonts;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.FlowLayout.Content.Controls;
using Apitron.PDF.Kit.Interactive.Forms;
using Apitron.PDF.Kit.Interactive.Forms.Signature;
using Apitron.PDF.Kit.Interactive.Forms.SignatureSettings;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.Styles.Appearance;
using Font = Apitron.PDF.Kit.Styles.Text.Font;

namespace CertificateOfCompletion
{
    /// <summary>
    /// This sample demonstrates creation of a basic certificate of completion. It also uses <see cref="SignatureField"/> and <see cref="SignatureControl"/> to digitally sign the resulting
    /// document and show image as signature representation. It also sets document background to image using <see cref="BackgroundImage"/> and also sets custom height to <see cref="Br"/> element.
    /// </summary>
    class Program
    {        
        static void Main(string[] args)
        {
            ResourceManager resourceManager = new ResourceManager();

            // register image resources
            resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("background", "..\\..\\..\\data\\certificate_template.png"));
            resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("signatureImage", "..\\..\\..\\data\\signatureImage.png"));

            // create document and set its background to image
            FlowDocument document = new FlowDocument() { BackgroundImage = new BackgroundImage("background", Length.FromPercentage(100), Length.FromPercentage(100)), BackgroundRepeat = BackgroundRepeat.NoRepeat, Align = Align.Center };

            // set font for all textblocks in document
            document.StyleManager.RegisterStyle("textblock", new Style() { Font = new Font(StandardFonts.CourierBold, 14) });

            // open digital certificate and prepare for signing, add signature field named "signature" to document
            using (Stream certificateStream = File.OpenRead("..\\..\\..\\data\\johndoe.pfx"))
            {
                document.Fields.Add(new SignatureField("signature")
                {
                    ViewSettings =
                        new SignatureFieldViewSettings() { Graphic = Graphic.Image, GraphicResourceID = "signatureImage", Description = Description.None },
                    Signature =
                        Signature.Create(new Pkcs12Store(certificateStream, "password"))
                });
            }

            // fill certificate with data, spacing between elements will be created by using BR elements with custom height
            document.Add(new Br() { Height = 220 });
            document.Add(new TextBlock("issued by Apitron LTD"));
            document.Add(new Br() { Height = 110 });
            document.Add(new TextBlock("Mr. Unnamed User"));
            document.Add(new Br() { Height = 150 });
            document.Add(new TextBlock("Apitron Samples Review Training"));
            document.Add(new Br() { Height = 30 });
            
            // use this section to create collective left padding 
            Section section = new Section() { Display = Display.Inline, Padding = new Thickness(150, 0, 0, 0) };

            // add signature control linked to existing field
            section.Add(new SignatureControl("signature") { Width = 100, Height = 50 });
            section.Add(new Br() { Height = 35 });
            section.Add(new TextBlock("Chef Instructor"));
            section.Add(new Br() { Height = 45 });
            section.Add(new TextBlock("5.12.2014"));

            document.Add(section);

            string fileName = @"..\..\..\OutputDocuments\CertificateOfCompletion.pdf";

            // save and open
            using (Stream stream = File.Create(fileName))
            {
                document.Write(stream, resourceManager, new PageBoundary(Boundaries.A4));
            }

            Process.Start(fileName);
        }
    }
}
