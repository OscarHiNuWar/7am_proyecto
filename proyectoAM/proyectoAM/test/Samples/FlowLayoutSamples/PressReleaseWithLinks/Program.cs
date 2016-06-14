using System.Diagnostics;
using System.IO;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.PageProperties;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.FixedLayout.Resources.Fonts;
using Apitron.PDF.Kit.FlowLayout;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.FlowLayout.Navigation;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.Styles.Appearance;
using Font = Apitron.PDF.Kit.Styles.Text.Font;

namespace PressReleaseWithLinks
{
    /// <summary>
    /// This sample shows how to use external web links assigned to content elements via <see cref="ContentElement.Link"/> property and <see cref="LinkUri"/>.
    /// These links can be assigned to almost any content element. It's also possible to assign a link to other content element within the document by using <see cref="CrossReference"/> 
    /// and when user clicks on this element it will be navigated to linked element's location. This sample also uses <see cref="Style.TextIndent"/> for producing indented paragraphs 
    /// and <see cref="Display.InlineBlock"/> style in order to enable <see cref="Align.Justify"/> setting for press-release body text. 
    /// Take a look how the header text is being vertically centered using the <see cref="Style.LineHeight"/> set to pageheader's height and  vertical align set to <see cref="VerticalAlign.Middle"/>.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // create resource manager and document
            ResourceManager resourceManager = new ResourceManager();

            FlowDocument document = new FlowDocument() { };

            // register style for page header
            document.StyleManager.RegisterStyle("#pageHeader", new Style() { Font = new Font(StandardFonts.TimesBold, 20), Color = RgbColors.White, Background = RgbColors.Black, Align = Align.Center, Height = 40, LineHeight = 40 });
            // register style for page area
            document.StyleManager.RegisterStyle("section.page", new Style() { Margin = new Thickness(30, 0, 30, 30) });
            // register style for release text area
            document.StyleManager.RegisterStyle("section.releasebody", new Style() { Font = new Font(StandardFonts.TimesRoman, 14) });
            // register style for contacts text
            document.StyleManager.RegisterStyle("textblock.header", new Style() { Font = new Font(StandardFonts.TimesBold, 14), Display = Display.Block });
            // register style for links
            document.StyleManager.RegisterStyle("textblock.link", new Style() { Color = RgbColors.Blue });
            // register style for text paragraphs of release text
            document.StyleManager.RegisterStyle("textblock.paragraph", new Style() { Display = Display.InlineBlock, Align = Align.Justify, TextIndent = 20 });

            // setup the page header, each added page will have it
            document.PageHeader.Id = "pageHeader";
            document.PageHeader.Add(new TextBlock("PRESS RELEASE") { VerticalAlign = VerticalAlign.Middle });

            // add page section
            Section page = new Section() { Class = "page" };
            page.Add(new Br() { Height = 30 });
            // add contacts, some of the entries will have links assigned via Link property
            page.Add(new TextBlock("Jan 1, 2015") { Class = "header" });
            page.Add(new TextBlock("Apitron releases a new PDF creation product") { Class = "header" });
            page.Add(new Br());
            page.Add(new TextBlock("Company website:&nbsp;") { Class = "header", Display = Display.Inline });
            page.Add(new TextBlock("www.apitron.com") { Class = "header link", Link = new LinkUri("http://www.apitron.com"), Display = Display.InlineBlock });
            page.Add(new Br());
            page.Add(new TextBlock("Email:") { Class = "header", Link = new LinkUri("mailto:sales@apitron.com"), Display = Display.Inline });
            page.Add(new TextBlock("sales@apitron.com") { Class = "header link", Link = new LinkUri("mailto:sales@apitron.com"), Display = Display.Inline });
            page.Add(new Br());
            page.Add(new TextBlock("Phone: +1 (206) 905 9580 ") { Class = "header" });
            page.Add(new Br() { Height = 50 });

            // add release text section (body)
            Section sectionBody = new Section() { Class = "releasebody" };

            sectionBody.Add(new TextBlock("Apitron, a software development company specialied in PDF and .NET has announced today that their new product - Apitron PDF Kit for .NET is out and available for download.") { Class = "paragraph" });
            sectionBody.Add(new Br() { Height = 10 });
            sectionBody.Add(new TextBlock("This product could be used to create dynamically generated PDF documents with complex layouts and various content. Both, flow layout and fixed layout APIs are provided and can be used together to create stunning reports, " +
                                   "bills, manuals, catalogues, CVs, booklets, brochures etc. Well-designed API lessens the development time and makes your company more effective. " +
                                   "Digitally signed documents are supported natively and it's even possible to produce document with multiple signatures applied. CSS-like styling makes document creation process an unbelievably easy task.") { Class = "paragraph" });
            sectionBody.Add(new Br() { Height = 10 });
            sectionBody.Add(new TextBlock("Combined with Apitron PDF Rasterizer for .NET a new product makes Apitron's toolchain completely unbeatable on .NET PDF components market.") { Class = "paragraph" });
            sectionBody.Add(new TextBlock("Click"));
            sectionBody.Add(new TextBlock("&nbsp;here&nbsp;") { Link = new LinkUri("http://apitron.com/Product/pdf-kit"), Class = "link" });
            sectionBody.Add(new TextBlock("to view Apitron PDF Kit for .NET product page."));

            // add body section to main page section
            page.Add(sectionBody);

            // add page section to document
            document.Add(page);

            string fileName = @"..\..\..\OutputDocuments\PressReleaseWithLinks.pdf";

            // save and open
            using (Stream stream = File.Create(fileName))
            {
                document.Write(stream, resourceManager, new PageBoundary(Boundaries.A4));
            }

            Process.Start(fileName);
        }
    }
}
