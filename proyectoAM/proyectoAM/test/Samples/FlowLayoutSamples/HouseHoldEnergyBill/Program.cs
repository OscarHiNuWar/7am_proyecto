
using System.Diagnostics;
using System.IO;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.PageProperties;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.FixedLayout.Resources.Fonts;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.Styles.Appearance;
using Apitron.PDF.Kit.Styles.Text;
using Font = Apitron.PDF.Kit.Styles.Text.Font;


namespace HouseHoldEnergyBill
{
    /// <summary>
    /// This sample demonstrates <see cref="Grid"/> element usage for creation of simple infographic. It shows how to center grid cell content (see rows style definition), how to define elements dimensions using pixels,
    /// how to set <see cref="BackgroundImage"/> for document and <see cref="Style"/> matching using id selectors.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // create resource manager and register image resource
            ResourceManager resourceManager = new ResourceManager();

            resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("background", System.IO.Path.Combine("..\\..\\..\\data", "house.png")));

            // create document
            FlowDocument document = new FlowDocument() { BackgroundImage = new BackgroundImage("background"), Align = Align.Center };

            // register style for document header element
            document.StyleManager.RegisterStyle("textblock#header", new Style() { Font = new Font(StandardFonts.TimesBold, 20), Padding = new Thickness(0, 20, 0, 65) });
            // register style for data grid
            document.StyleManager.RegisterStyle("grid#data", new Style() { Width = Length.FromPixels(380), Align = Align.Center, InnerBorder = Border.None });
            // register style for the first row
            document.StyleManager.RegisterStyle("#row1", new Style() { Height = Length.FromPixels(205), LineHeight = Length.FromPixels(205) });
            // register style for the second row
            document.StyleManager.RegisterStyle("#row2", new Style() { Height = Length.FromPixels(255), LineHeight = Length.FromPixels(255) });
            // register style for text elements in both rows
            document.StyleManager.RegisterStyle("#row1>textblock, #row2>textblock ", new Style() { VerticalAlign = VerticalAlign.Middle, Color = RgbColors.White, TextDecoration = new TextDecoration(TextDecorationOptions.Underline) });

            // add document header
            document.Add(new TextBlock("Household Energy Bill 2014") { Id = "header" });

            // add data grid, it will be mapped to house image on document background
            Grid grid = new Grid(Length.FromPercentage(48), Length.FromPercentage(6), Length.FromPercentage(46)) { Id = "data" };

            // add rows with data
            grid.Add(new GridRow(new TextBlock("Electricity $ 100") { ColSpan = 2 }, new TextBlock("Gas $ 300")) { Id = "row1" });
            grid.Add(new GridRow(new TextBlock("Trash $ 50"), new TextBlock("Water $ 200") { ColSpan = 2 }) { Id = "row2" });

            // add grid to document
            document.Add(grid);

            string fileName = @"..\..\..\OutputDocuments\HouseHoldEnergyBill.pdf";

            // save and open
            using (Stream stream = File.Create(fileName))
            {
                document.Write(stream, resourceManager, new PageBoundary(new Boundary(435, 480)));
            }

            Process.Start(fileName);
        }
    }
}
