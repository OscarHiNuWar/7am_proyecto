using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.PageProperties;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.FixedLayout.Resources.Fonts;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.Styles.Appearance;
using Font = Apitron.PDF.Kit.Styles.Text.Font;
using Image = Apitron.PDF.Kit.FlowLayout.Content.Image;

namespace InvoiceGenerator
{
    public partial class MainForm : Form
    {
        #region Fields

        private BindingList<ProductEntry> products;

        private IList<string> productNames;

        #endregion

        #region ctor

        public MainForm()
        {
            InitializeComponent();

            // products lists to be used as datasource in data grid view combobox
            productNames = new List<string>
                           {
                               "Angle grinder",
                               "Circular saw",
                               "Cutting blades, pack 10 pcs.",
                               "Electric drill 1KWt",
                               "Laser level with accesories",
                               "Non-contact voltage tester",
                               "Paint sprayer",
                               "Spanners set 24 pcs.",
                               "Stainless steel pull saw",
                               "Stud finder"
                           };

            // data source for data grid view
            products = new BindingList<ProductEntry>();
            products.AddingNew += products_AddingNew;
            products.AllowNew = true;

            // init view columns and set datasource
            InitGridViewColumns();
            dataGridView.DataSource = products;
        }

        #endregion

        #region UI creation and initialization

        private void products_AddingNew(object sender, AddingNewEventArgs e)
        {
            // init new row, increment position and set default quantity
            e.NewObject = new HomeToolProduct() {Pos = products.Count + 1, Qty = 1};
        }

        /// <summary>
        /// Creates data grid view columns.
        /// </summary>
        private void InitGridViewColumns()
        {
            dataGridView.AutoGenerateColumns = false;

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn() {DataPropertyName = "Pos", HeaderText = "#", Width = 25});
            dataGridView.Columns.Add(new DataGridViewComboBoxColumn(){DataPropertyName = "Description",HeaderText = "Description",DataSource = productNames,Width = 180});
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Qty", HeaderText = "Qty.", Width = 40 });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Price", HeaderText = "Price", Width = 50 });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Discount", HeaderText = "Discount(%)", Width = 50 });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Total", HeaderText = "Total", ReadOnly = true, Width = 55 });
        }

        /// <summary>
        /// Shows file save dialog and proceeds with invoice saving.
        /// </summary>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "*.pdf | *.pdf";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream stream = File.Create(saveDialog.FileName))
                {
                    GenerateInvoice(stream);
                }
            }
        }

        #endregion

        #region PDF generation

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
                new Style() {Background = RgbColors.LightSlateGray});

            /* style matching all cells in rows with class "centerAlignedCells" set 
                and all cells in rows with class "centerAlignedCell" set */
            document.StyleManager.RegisterStyle("gridrow.centerAlignedCells > *, gridrow > *.centerAlignedCell",
                new Style() {Align = Align.Center, Margin = new Thickness(0)});

            /* style matching all elements in rows with class "leftAlignedCell" set */
            document.StyleManager.RegisterStyle("gridrow > *.leftAlignedCell",
                new Style() {Align = Align.Left, Margin = new Thickness(5, 0, 0, 0)});

            /* default style for any cell in any grid row, assigned via type + child selectors, makes it right aligned */
            document.StyleManager.RegisterStyle("gridrow > *",
                new Style() {Align = Align.Right, Margin = new Thickness(0, 0, 5, 0)});

            // create resource manager and register image resources
            ResourceManager resourceManager = new ResourceManager();
            resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("logo",
                Path.Combine(imagesPath, "storeLogo.png"), true) {Interpolate = true});
            resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("stamp",
                Path.Combine(imagesPath, "stamp.png"), true) {Interpolate = true});

            // construct page header which includes store logo and the text "Invoice"
            document.PageHeader.Margin = new Thickness(0, 40, 0, 20);
            document.PageHeader.Padding = new Thickness(10, 0, 10, 0);
            document.PageHeader.Height = 120;
            document.PageHeader.Background = RgbColors.LightGray;
            document.PageHeader.LineHeight = 60;
            document.PageHeader.Add(new Image("logo") {Height = 50, Width = 50, VerticalAlign = VerticalAlign.Middle});
            document.PageHeader.Add(new TextBlock("Invoice")
                                    {
                                        Display = Display.InlineBlock,
                                        Align = Align.Right,
                                        Font = new Font(StandardFonts.CourierBold, 20),
                                        Color = RgbColors.Black
                                    });

            // page content section with padding
            Section pageSection = new Section() {Padding = new Thickness(20)};

            // add company info section
            pageSection.AddItems(
                CreateInfoSubsections(new string[] {txtCompany.Text, "Bill to:\r\n" + txtCustomerInfo.Text}));

            // add horizontal line for visual separation
            pageSection.Add(new Hr() {Padding = new Thickness(0, 20, 0, 20)});

            // add products grid        
            pageSection.Add(CreateProductsGrid());

            // add new line after grid
            pageSection.Add(new Br {Height = 20});

            // insert empty padding section and stamp image
            pageSection.Add(new Section() {Width = 250, Display = Display.InlineBlock});
            pageSection.Add(new Image("stamp"));

            // add page section into document
            document.Add(pageSection);
            // save document to stream
            document.Write(stream, resourceManager, new PageBoundary(Boundaries.A4));
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

            double width = 100.0/info.Length;

            for (int i = 0; i < info.Length; i++)
            {
                Section section = new Section() {Width = Length.FromPercentage(width), Display = Display.InlineBlock};

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
                                 Class =
                                     "tableHeader centerAlignedCells"
                             });

            Decimal invoiceTotal = 0;

            // enumerate the list of products and create grid rows
            foreach (ProductEntry product in products)
            {
                TextBlock pos = new TextBlock(product.Pos.ToString()) {Class = "centerAlignedCell"};
                TextBlock description = new TextBlock(product.Description) {Class = "leftAlignedCell"};
                TextBlock qty = new TextBlock(product.Qty.ToString()) {Class = "centerAlignedCell"};
                TextBlock price = new TextBlock(product.Price.ToString(CultureInfo.InvariantCulture));
                TextBlock discount = new TextBlock(product.Discount.ToString(CultureInfo.InvariantCulture));
                TextBlock total = new TextBlock(product.Total.ToString(CultureInfo.InvariantCulture));

                productsGrid.Add(new GridRow(pos, description, qty, price, discount, total));
                invoiceTotal += product.Total;
            }

            // append "total" row
            productsGrid.Add(new GridRow(new TextBlock("Total(USD)") {ColSpan = 4},
                new TextBlock(invoiceTotal.ToString(CultureInfo.InvariantCulture)) {ColSpan = 2}));
            return productsGrid;
        }

        #endregion      
    }
}
