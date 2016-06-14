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
using Apitron.PDF.Kit.Styles.Text;
using Font = Apitron.PDF.Kit.Styles.Text.Font;

namespace ContractWithDigitalSignatures
{
    /// <summary>
    /// This sample shows how to sign PDF document using several signatures. Take a look how  <see cref="Align.Justify"/> is used to create justified text for contract body.
    /// Signatures are added using <see cref="SignatureControl"/> and <see cref="SignatureField"/>. Another important thing to note is <see cref="Style.LineHeight"/> property usage 
    /// for the correct placement of the signature control and accompanying text label having different height.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string dataPath = "..\\..\\..\\data";

            ResourceManager resourceManager = new ResourceManager();

            // register signature images
            resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("signatureImageJoe", Path.Combine(dataPath, "signatureImage.png")));
            resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("signatureImageJane", Path.Combine(dataPath, "signatureImageJane.png")));

            // create document and set its margin
            FlowDocument document = new FlowDocument() { Margin = new Thickness(20, Length.FromPercentage(35), 20, 20) };

            // open digital certificates and create signature fields
            using (Stream certificateStream1 = File.OpenRead(Path.Combine(dataPath, "johndoe.pfx")), certificateStream2 = File.OpenRead(Path.Combine(dataPath, "janeDoe.pfx")))
            {
                // add lender signature field
                document.Fields.Add(new SignatureField("lenderSignature")
                {
                    // adjust view settings to make signature image viewable without any signature properties
                    ViewSettings =
                        new SignatureFieldViewSettings() { Graphic = Graphic.Image, GraphicResourceID = "signatureImageJoe", Description = Description.None },
                    Signature =
                        Signature.Create(new Pkcs12Store(certificateStream1, "password"))
                });

                // add borrower signature field
                document.Fields.Add(new SignatureField("borrowerSignature")
                {
                    ViewSettings =
                        new SignatureFieldViewSettings() { Graphic = Graphic.Image, GraphicResourceID = "signatureImageJane", Description = Description.None },
                    Signature =
                        Signature.Create(new Pkcs12Store(certificateStream2, "password"))
                });
            }

            // register style for contract header
            document.StyleManager.RegisterStyle("#header", new Style() { Display = Display.Block, Align = Align.Center, Font = new Font(StandardFonts.CourierBold, 25) });
            // style for the conditions text
            document.StyleManager.RegisterStyle("#body", new Style() { TextIndent = 10, Font = new Font(StandardFonts.Courier, 14), Align = Align.Justify, Display = Display.InlineBlock });
            // style for accompanying name and signature labels
            document.StyleManager.RegisterStyle(".name", new Style() { Font = new Font(StandardFonts.Courier, 14), TextDecoration = new TextDecoration(TextDecorationOptions.Underline) });            
            document.StyleManager.RegisterStyle(".container", new Style() { Width = Length.FromPercentage(50), Display = Display.InlineBlock, LineHeight = 30 });
            // style for signature control
            document.StyleManager.RegisterStyle("SignatureControl", new Style() { Width = Length.FromPixels(100), Height = Length.FromPixels(50), Display = Display.InlineBlock });

            // add contract header
            document.Add(new TextBlock("PERSONAL LOAN CONTRACT") { Id = "header" });
            document.Add(new Br() { Height = 20 });
            // place conditions
            document.Add(new TextBlock("This contract (\"Contract\") is an agreement between Mrs. Jane Doe, henceforth known as \"Borrower,\" and Mr. John Doe, henceforth known as \"Lender\"." +
            "Borrower wishes to borrow $100, known as \"Loan\", from Lender. Loan will be returned to Borrower on	1.1.2016." +
            "Conditions for this Loan are as follows: Borrower will begin repayment of the Loan on 1.1.2015." +
            "The final date for repayment will be 1.1.2016. Failure to repay the Loan within 14 days of 1.1.2016 will result in the matter being addressed in court." +
            "Borrower and Lender agree to the conditions above, and digitally sign this contract using their personal digital certificates on the 7 day of December, 2014.") { Id = "body" });

            document.Add(new Br() { Height = 20 });

            // create section to wrap name, signature label and signature itself and make divide page space 50/50
            // between this and next section
            document.Add(new Section(new TextBlock("Borrower name: Jane Doe") { Class = "name" }, new Br(),
                                     new TextBlock("Borrower signature:") { Class = "name" },
                                     new SignatureControl("borrowerSignature")) { Class = "container" });


            document.Add(new Section(new TextBlock("Lender name: John Doe") { Class = "name" }, new Br(),
                                     new TextBlock("Lender signature:") { Class = "name" },
                                     new SignatureControl("lenderSignature")) { Class = "container" });


            string fileName = @"..\..\..\OutputDocuments\ContractWithDigitalSignatures.pdf";

            // save and open
            using (Stream stream = File.Create(fileName))
            {
                document.Write(stream, resourceManager, new PageBoundary(Boundaries.A4));
            }

            Process.Start(fileName);
        }
    }
}
