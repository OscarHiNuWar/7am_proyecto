using System.Diagnostics;
using System.IO;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.PageProperties;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.Styles.Appearance;
using Apitron.PDF.Kit.Styles.Text;

namespace Booklet
{
    /// <summary>
    /// This sample shows how to use <see cref="FlowDocument.NewPage"/> event and override styles for producing dynamically generated content.
    /// Note that only elements which initialization takes place on a page being generated can be affected using this technique.
    /// It also shows how to use <see cref="Hr"/> and <see cref="PageCount"/> elements. Advanced selectors usage is demonstrated as well, see <see cref="StyleManager.RegisterStyle"/> for details on selectors.
    /// The resulting booklet will have 2 pages and page numbers will be positioned according to the page order.
    /// </summary>
    class Program
    {      
        static void Main(string[] args)
        {
            ResourceManager resourceManager = new ResourceManager();

            FlowDocument document = new FlowDocument() { Margin = new Thickness(5) };

            // add style for footer section with page number and page count
            document.StyleManager.RegisterStyle("section#footercontent", new Style() { Align = Align.Left, Margin = new Thickness(0, 5, 0, 0) });

            // add style for the section with dashed border around that defines the placeholder for booklet content
            document.StyleManager.RegisterStyle("section.bookletPage", new Style()
            {
                Border = new Border(1, new float[] { 5, 5 }, 0),
                BorderColor = RgbColors.Gray,
                Width = Length.FromPercentage(100),
                Height = Length.FromPercentage(99),
                Align = Align.Center,
                VerticalAlign = VerticalAlign.Middle,
                Padding = new Thickness(0, Length.FromPercentage(45), 0, 0)
            });

            // define the font for textblocks inside the booklet section
            document.StyleManager.RegisterStyle("section.bookletPage > textblock", new Style() { Font = new Font("Tahoma", 18) });

            // make sure that page number will be positioned on the right if the page is even
            // page numbers in booklet are assumed to be 1,2,..etc.
            document.NewPage += (eventArgs) =>
            {
                // alter style for pages 2,4.. etc.
                if ((eventArgs.Context.CurrentPage & 0x1) > 0)
                {
                    eventArgs.OverridingStyleManager.RegisterStyle("section#footercontent", new Style() { Align = Align.Right });
                }
            };

            // add labels and page break
            document.Add(new Section(new TextBlock("Here goes the content for the first page")) { Class = "bookletPage" });
            document.Add(new PageBreak());
            document.Add(new Section(new TextBlock("Here goes the content for the second page")) { Class = "bookletPage" });

            // add content to page footer
            document.PageFooter.Add(new Hr());

            // define dynamic content generation for the page count textbox using lambda
            document.PageFooter.Add(new Section(new TextBlock((context) => string.Format("Page {0} of&nbsp;", context.CurrentPage + 1)), new PageCount(2)) { Id = "footerContent" });
            string fileName = @"..\..\..\OutputDocuments\Booklet.pdf";

            // save and open
            using (Stream stream = File.Create(fileName))
            {
                document.Write(stream, resourceManager, new PageBoundary(Boundaries.A4));
            }

            Process.Start(fileName);
        }
    }
}
