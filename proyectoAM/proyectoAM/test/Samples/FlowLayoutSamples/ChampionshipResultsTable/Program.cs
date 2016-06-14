using System.Diagnostics;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.Resources;
using System.IO;
using Apitron.PDF.Kit.FixedLayout.Resources.Fonts;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.Styles.Appearance;
using Font = Apitron.PDF.Kit.Styles.Text.Font;
using Apitron.PDF.Kit.FixedLayout.PageProperties;

namespace ChampionshipResultsTable
{
    /// <summary>
    /// This sample shows how to use <see cref="Grid"/> to create European Footbal Cup winners table. It also demonstrates how to use <see cref="BackgroundImage"/> and <see cref="BackgroundPosition"/> in conjunction with <see cref="BackgroundRepeat"/>
    /// setting to create unique looking columns with team logos inside the cells. Various styling features e.g. id or class matching are used to set properties of grid rows or individual elements.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ResourceManager resourceManager = new ResourceManager();

            // preload logos, each logo is named after the team
            string dataPath = "..\\..\\data";

            foreach (string logoFileName in Directory.EnumerateFiles(dataPath, "*.png"))
            {
                resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image(Path.GetFileNameWithoutExtension(logoFileName), logoFileName));
            }

            // create document and register styles
            FlowDocument document = new FlowDocument() { Margin = new Thickness(18) };

            // common style for the grid with results
            document.StyleManager.RegisterStyle("grid#results", new Style() { InnerBorderColor = RgbColors.LightGray, Align = Align.Center, Font = new Font(StandardFonts.Helvetica, 12) });            
            // set height for each row of the results grid
            document.StyleManager.RegisterStyle("grid#results > gridrow", new Style() { Height = 16 });
            // set header row color
            document.StyleManager.RegisterStyle("grid#results > gridrow#header", new Style() { Background = RgbColors.LightGray });
            // set header text font 
            document.StyleManager.RegisterStyle("textblock.h1", new Style() { Font = new Font(StandardFonts.HelveticaBold, 20) });
            // set winners cell style
            document.StyleManager.RegisterStyle(".Winners", new Style() { Align = Align.Right, BackgroundPosition = BackgroundPosition.LeftCenter, BackgroundRepeat = BackgroundRepeat.NoRepeat, Margin = new Thickness(3, 0, 3, 0) });
            // set runners-up cell style
            document.StyleManager.RegisterStyle(".Runners_up", new Style() { Align = Align.Left, BackgroundPosition = BackgroundPosition.RightCenter, BackgroundRepeat = BackgroundRepeat.NoRepeat, Margin = new Thickness(3, 0, 3, 0) });

            // define the grid
            Grid grid = new Grid(Length.FromPercentage(10), Length.FromPercentage(20), Length.FromPercentage(10), Length.FromPercentage(20), Length.FromPercentage(30), Length.FromPercentage(10)) { Id = "results" };

            // add header row
            grid.Add(new GridRow(new TextBlock("Season"), new TextBlock("Winners"), new TextBlock("Score"), new TextBlock("Runners-up"), new TextBlock("Venue"), new TextBlock("Attendance")) { Id = "header" });

            // read cup data from file and fill the grid, each row is represented as string contaning values separated by semicolon
            using (StreamReader reader = File.OpenText(Path.Combine(dataPath, "data.txt")))
            {
                string rowData = null;

                while (!string.IsNullOrEmpty(rowData = reader.ReadLine()))
                {
                    string[] cells = rowData.Split(';');

                    GridRow newRow = new GridRow();

                    for (int i = 0; i < cells.Length; i++)
                    {
                        TextBlock cell = new TextBlock(cells[i]);

                        // for cols 1 and 3 we set background image by using team name as resource id for the image,
                        // this image will be positioned according to the style that matches cell's class
                        if (i == 1)
                        {
                            cell.BackgroundImage = new BackgroundImage(cells[i]);
                            cell.Class = "Winners";
                        }
                        else if (i == 3)
                        {
                            cell.BackgroundImage = new BackgroundImage(cells[i]);
                            cell.Class = "Runners_up";
                        }

                        newRow.Add(cell);
                    }

                    grid.Add(newRow);
                }
            }

            // add header and grid into the document
            document.Add(new TextBlock("European Cup Winners") { Class = "h1" });
            document.Add(grid);

            string fileName = @"..\..\..\OutputDocuments\ChampionshipResultsTable.pdf";

            // save and open
            using (Stream stream = File.Create(fileName))
            {
                document.Write(stream, resourceManager, new PageBoundary(Boundaries.Ledger));
            }

            Process.Start(fileName);
        }
    }
}
