using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.FixedLayout.Resources.Fonts;
using Apitron.PDF.Kit.FixedLayout.Resources.XObjects;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.Styles.Appearance;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apitron.PDF.Kit.FlowLayout;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.PageProperties;

namespace proyectoAM.clases
{
    class pdfCreaton
    {
        /// <summary>
        /// Generates the invoice based on entered data.
        /// </summary>
        /// <param name="stream">Stream to save the resulting pdf into.</param>
        private void GenerateInvoice(Stream stream)
        {
            // base path for images
            string imagesPath = @"..\..\images";
            // create document and register styles
            FlowDocument document = new FlowDocument();
            /* style for products table header, assigned via type + class selectors */
            document.StyleManager.RegisterStyle("gridrow.tableHeader",
            new Style() { Background = RgbColors.LightSlateGray });
            /* style matching all cells in rows with class "centerAlignedCells" set
            and all cells in rows with class "centerAlignedCell" set */
            document.StyleManager.RegisterStyle("gridrow.centerAlignedCells > *, gridrow > *.centerAlignedCell", 
                new Style() { Align = Align.Center, Margin = new Thickness(0) });
            /* style matching all elements in rows with class "leftAlignedCell" set */
            document.StyleManager.RegisterStyle("gridrow > *.leftAlignedCell",
            new Style() { Align = Align.Left, Margin = new Thickness(5, 0, 0, 0) });
            /* default style for any cell in any grid row, assigned via type + child selectors, makes it right
           aligned */
            document.StyleManager.RegisterStyle("gridrow > *",
            new Style() { Align = Align.Right, Margin = new Thickness(0, 0, 5, 0) });
            // create resource manager and register image resources
            ResourceManager resourceManager = new ResourceManager();
            resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("logo",
            Path.Combine(imagesPath, "storeLogo.png"), true)
            { Interpolate = true });
            resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("stamp",
            Path.Combine(imagesPath, "stamp.png"), true)
            { Interpolate = true });
            // construct page header which includes store logo and the text "Invoice"
            document.PageHeader.Margin = new Thickness(0, 40, 0, 20);
            document.PageHeader.Padding = new Thickness(10, 0, 10, 0);
            document.PageHeader.Height = 120;
            document.PageHeader.Background = RgbColors.LightGray;
            document.PageHeader.LineHeight = 60;
           /* document.PageHeader.Add(new Image("logo")
            {
                Height = 50,
                Width = 50,
                VerticalAlign =
           VerticalAlign.Middle
            });*/
            document.PageHeader.Add(new TextBlock("Invoice")
            {
                Display = Display.InlineBlock,
                Align = Align.Right,
                //Font = new Font(StandardFonts.CourierBold, 20),
                Color = RgbColors.Black
            });
            // page content section with padding
            Section pageSection = new Section() { Padding = new Thickness(20) };
            // add company info section
            pageSection.AddItems(
                //OJO OSCAR!!! TRAER LOS DATOS DE FORM1 PARA ACA (O SEA, TODOS LOS TEXTOS FUERA DEL DATAGRIDVIEW
            CreateInfoSubsections(new string[] { }));
                //OJO OSCAR!!!
            // add horizontal line for visual separation
            pageSection.Add(new Hr() { Padding = new Thickness(0, 20, 0, 20) });
            // add products grid
            pageSection.Add(CreateProductsGrid());
            // add new line after grid
            pageSection.Add(new Br { Height = 20 });
            // insert empty padding section and stamp image
            pageSection.Add(new Section() { Width = 250, Display = Display.InlineBlock });
            pageSection.Add(new Image("stamp"));
            // add page section into document
            document.Add(pageSection);
            // save document to stream
            document.Write(stream, resourceManager, new PageBoundary(Boundaries.A4));
        }

        private IEnumerable<ContentElement> CreateInfoSubsections(string[] v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates several info sections side by side based on given list of strings.
        /// </summary>
        /// <returns>
        /// List of created sections with textual information.
        /// </returns>
        private IList<Section> CreateInfoSubsections(string[] info)
        {
            List<Section> createdSections = new List<Section>();
            double width = 100.0 / info.Length;
            for (int i = 0; i < info.Length; i++)
            {
                Section section = new Section()
                {
                    Width = Length.FromPercentage(width),
                    Display = Display.InlineBlock
                };
                using (StringReader reader = new StringReader(info[i]))
                {
                    string currentLine = null;
                    while ((currentLine = reader.ReadLine()) != null)
                    {
                        section.Add(new TextBlock(currentLine));
                        section.Add(new Br());
                    }
                }
                createdSections.Add(section);
            }
            return createdSections;
        }

        /// <summary>
        /// Creates products grid.
        /// </summary>
        private Grid CreateProductsGrid()
        {
            // create grid content element and its define columnts
            Grid productsGrid = new Grid(20, Length.Auto, 30, 50, 55, 60);
            // add header row
            productsGrid.Add(new GridRow(new TextBlock("#"), new TextBlock("Product"), new TextBlock("Qty."),
            new TextBlock("Price"), new TextBlock("Disc.(%)"), new TextBlock("Total"))
            {
                Class = "tableHeader centerAlignedCells"
            });
            Decimal invoiceTotal = 0;
            // enumerate the list of products and create grid rows
            foreach (ProductEntry product in products)
            {
                TextBlock pos = new TextBlock(product.Pos.ToString()) { Class = "centerAlignedCell" };
                TextBlock description = new TextBlock(product.Description) { Class = "leftAlignedCell" };
                TextBlock qty = new TextBlock(product.Qty.ToString()) { Class = "centerAlignedCell" };
                TextBlock price = new TextBlock(product.Price.ToString(CultureInfo.InvariantCulture));
                TextBlock discount = new TextBlock(product.Discount.ToString(CultureInfo.InvariantCulture));
                TextBlock total = new TextBlock(product.Total.ToString(CultureInfo.InvariantCulture));
                productsGrid.Add(new GridRow(pos, description, qty, price, discount, total));
                invoiceTotal += product.Total;
            }
            // append "total" row
            productsGrid.Add(new GridRow(new TextBlock("Total(USD)") { ColSpan = 4 },
            new TextBlock(invoiceTotal.ToString(CultureInfo.InvariantCulture)) { ColSpan = 2 }));
            return productsGrid;
        }

    }
}
