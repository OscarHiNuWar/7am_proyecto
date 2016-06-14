using System.Diagnostics;
using System.IO;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.PageProperties;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.FixedLayout.Resources.Fonts;
using Apitron.PDF.Kit.FlowLayout;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.Styles.Appearance;
using Font = Apitron.PDF.Kit.Styles.Text.Font;

namespace AnnualSalesReport
{
    /// <summary>
    /// This sample demonstrates how to use <see cref="Grid"/> element for reports creation, it creates a simple sales report for a tool shop that shows sales per month statistics.
    /// It also shows how to use <see cref="Style"/> objects and various selectors in order to get the report customized.
    /// </summary>        
    class Program
    {       
        static void Main(string[] args)
        {
            // create list of instruments for report data generation
            string[] listOfInstruments = new string[]{"Electric Hand Drill","AWL","Back Saw","Ball Peen Hammer","Batt. Powered Hand Drill","Carpenter’s Square","C-Clamp","Chuck Key","Drill Press Chuck","Combination Square","Combination Pliers","Coping Saw","Diagonal Cutters","Drill Press","Wooden Mallet","Forester Bit","Hack Saw","Hand Saw","Hole Cutter","Linesman Pliers","Mitre Saw","Multi Spur Bit","Needle Nose Pliers","Parallel Clamp","Phillips Screwdriver","Pipe Wrench","Robertson Screwdriver","Slip Joint Pliers","Slot Screwdriver","Spade Drill Bit","Spring Clamp","Tri-Square","Twisted Drill Bits","Utility Knife","Vise Grips","Wire Cutters"};
            
            ResourceManager resourceManager = new ResourceManager();

            // create flow document and set its margin
            FlowDocument document = new FlowDocument();
            document.Margin = new Thickness(5, 5, 5, 5);

            // style for row header contaning months
            document.StyleManager.RegisterStyle("gridrow#gridHeaderMonths", new Style() { Font = new Font(StandardFonts.HelveticaBold, 14) });

            // style for row header containing inner column names
            document.StyleManager.RegisterStyle("gridrow#gridHeaderDataColumnNames", new Style() { Font = new Font(StandardFonts.HelveticaBold, 12) });

            // style for final row, sontaining monthly sales amount
            document.StyleManager.RegisterStyle("gridrow#subtotalsRow", new Style() { Font = new Font(StandardFonts.HelveticaBold, 20) });

            // style for report grid
            document.StyleManager.RegisterStyle("grid#reportgrid", new Style() { Align = Align.Center });

            // style for textblocks containing low sales values, indicated by class "lowsales"
            document.StyleManager.RegisterStyle("gridrow>textblock.lowsales", new Style() { Color = RgbColors.Red });

            // create main grid with 25 cols
            Grid grid = new Grid(150, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto, Length.Auto) { Id = "reportGrid" };

            // create first row, it will contain month names
            GridRow gridHeaderMonths = new GridRow(ContentElement.Empty) {Id = "gridHeaderMonths"};
            
            // define data column names row
            GridRow gridHeaderDataColumnNames = new GridRow(new TextBlock("Product")) {Id = "gridHeaderDataColumnNames"};

            for (int i =1;i<13;i++)
            {
                // add month name
                gridHeaderMonths.Add(new TextBlock(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i)){ColSpan = 2});

                // add data columns for each month
                gridHeaderDataColumnNames.Add(  new TextBlock("Items sold"));
                gridHeaderDataColumnNames.Add(  new TextBlock("Subtotal"));
            }

            // add header rows to grid
            grid.Add(gridHeaderMonths);
            grid.Add(gridHeaderDataColumnNames);
          
            // indicates low sales amount, items having value below this indicator will have its class set to "lowsales"
            double lowSalesIndicator = 100;

            // will contain monthly sales amounts
            double[] monthlySales = new double[12];

            // fill the table with data
            for (int instrumentIndex = 1; instrumentIndex < 4; instrumentIndex++)
            {
                for (int i = 0; i < listOfInstruments.Length; i++)
                {
                    GridRow newRow = new GridRow(new TextBlock(string.Format("{0}#{1}", listOfInstruments[i], instrumentIndex)));

                    for (int j = 0; j < 12; j++)
                    {
                        int countSold = (i + j) * 10;
                        double subTotal = countSold * 3.5;

                        TextBlock colCountSold = new TextBlock(countSold.ToString());
                        TextBlock colSubTotal = new TextBlock(subTotal.ToString());

                        // mark low sales cells with corresponding class, so they will be highlighted 
                        if (subTotal < lowSalesIndicator)
                        {
                            colSubTotal.Class = "lowSales";
                        }

                        newRow.Add(colCountSold);
                        newRow.Add(colSubTotal);

                        monthlySales[j] += subTotal;
                    }

                    grid.Add(newRow);
                }
            }

            // create subtotals row
            GridRow subtotalsRow = new GridRow(new TextBlock("Subtotals")) { Id = "subtotalsRow" };

            for (int i = 0; i < 12; i++)
            {
                subtotalsRow.Add(new TextBlock(string.Format("$ {0}", monthlySales[i])) { ColSpan = 2 });
            }

            grid.Add(subtotalsRow);

            // add grid to doc
            document.Add(grid);

            string fileName = @"..\..\..\OutputDocuments\annualSalesReport.pdf";

            // write the file to stream
            using (Stream outputStream = File.Create(fileName))
            {
                document.Write(outputStream,resourceManager, new PageBoundary(new Boundary(0, 0, Boundaries.A2.Height, Boundaries.A2.Width)));
            }

            // open for preview
            Process.Start(fileName);
        }
    }
}
