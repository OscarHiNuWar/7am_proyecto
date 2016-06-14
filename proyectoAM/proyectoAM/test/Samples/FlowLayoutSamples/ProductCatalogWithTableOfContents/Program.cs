using System.Diagnostics;
using System.IO;
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
using Apitron.PDF.Kit;

namespace ProductCatalogWithTableOfContents
{
    /// <summary>
    /// This sample shows how to build product catalog. It parses base folder structure and creates product categories accordingly, thus that one folder corresponds to one category.
    /// Each of the folders contains product images named in special way. Format is as follows [product name];[measure(liter,gramm, kg etc.)];[price].png. So the image filename contains all necessary information to create 
    /// catalogue entry. We use <see cref="Style.ListStyle"/> property to create ordered list and we use <see cref="Style.ListMarker"/> to set the desired numbering style. Links to categories are created by using
    /// <see cref="CrossReference"/> objects assigned to <see cref="ContentElement.Link"/> property of the TOC texblocks. Bookmarks are created using <see cref="ContentElement.Bookmark"/> property, bookmark's placement in bookmarks 
    /// tree is being determined by its nesting level. As an example the root bookmark is created by setting this property for <see cref="FlowDocument"/> object.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string dataFolder = @"..\..\..\data";

            // register images for car and back arrow
            ResourceManager resourceManager = new ResourceManager();

            resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("cart", Path.Combine(dataFolder, "cart.png")));
            resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("backarrow", Path.Combine(dataFolder, "back.png")));

            // create doc
            FlowDocument document = new FlowDocument() { Margin = new Thickness(5) };
            // create root bookmark
            document.Bookmark = new BookmarkEntry("Products");
            // register style for product's measure
            document.StyleManager.RegisterStyle("textblock.measure", new Style() { Font = new Font(StandardFonts.Helvetica, 10), Color = RgbColors.Gray, Width = Length.FromPercentage(50), Align = Align.Left, Display = Display.InlineBlock });
            // register style for product's price
            document.StyleManager.RegisterStyle("textblock.price", new Style() { Font = new Font(StandardFonts.HelveticaBold, 12), Width = Length.FromPercentage(50), Align = Align.Right, Display = Display.InlineBlock });
            // register style for product's label
            document.StyleManager.RegisterStyle("textblock.label", new Style() { Font = new Font(StandardFonts.HelveticaBold, 12), Width = Length.FromPercentage(100), Align = Align.Left, Display = Display.Block, LineHeight = Length.FromPercentage(200) });
            // register style for category header
            document.StyleManager.RegisterStyle("textblock.categoryHeader", new Style() { Font = new Font(StandardFonts.HelveticaBold, 18), LineHeight = Length.FromPercentage(180) });
            // register style for back link arrow
            document.StyleManager.RegisterStyle("image.backlink", new Style() { Margin = new Thickness(5, 0, 5, 0), VerticalAlign = VerticalAlign.Middle });
            // regsiter style for text blocks under the table of contents section
            document.StyleManager.RegisterStyle("section#toc > textblock", new Style() { Display = Display.Block, ListStyle = ListStyle.ListItem, Font = new Font(StandardFonts.HelveticaBold, 18), LineHeight = Length.FromPercentage(150) });
            // register style for TOC
            document.StyleManager.RegisterStyle("section#toc", new Style() { Margin = new Thickness(20, 20, 20, 20), Font = new Font(StandardFonts.HelveticaBold, 25), ListStyle = ListStyle.Ordered, ListMarker = ListMarker.Decimal });
            // style for product info block
            document.StyleManager.RegisterStyle("section.productDescription", new Style() { Width = Length.FromPercentage(25), Height = Length.FromPixels(250), Border = new Border(1), BorderColor = RgbColors.LightGray, Margin = new Thickness(2, 2, 2, 2), Padding = new Thickness(3, 3, 3, 3), Align = Align.Center, Display = Display.InlineBlock });
            // style for product image
            document.StyleManager.RegisterStyle("image.productImage", new Style() { Display = Display.Block, Width = Length.FromPixels(180), Height = Length.FromPixels(180) });

            // create first page content - the cover
            Section firstPage = new Section() { Align = Align.Center, Font = new Font(StandardFonts.HelveticaBold, 40), Margin = new Thickness(0, 350, 0, 0) };
            firstPage.Add(new TextBlock("Products catalog"));
            firstPage.Add(new Br());
            firstPage.Add(new Image("cart"));
            document.Add(firstPage);
            document.Add(new PageBreak());

            // create table of contents
            Section toc = new Section() { Id = "toc" };
            toc.Add(new TextBlock("Select category") { ListStyle = ListStyle.None });
            toc.Add(new Br() { Height = 10 });
            document.Add(toc);
            document.Add(new PageBreak());

            char[] splitChars = new char[] {'_'};

            // create categories and fill with product data
            foreach (string directory in Directory.EnumerateDirectories(Path.Combine(dataFolder, "products")))
            {
                // get product category name
                string[] dirs = directory.Split(Path.DirectorySeparatorChar);
                string dirName = dirs[dirs.Length - 1];

                // add category name into the TOC and set its link pointing to the object with id=[category name]
                toc.Add(new TextBlock(dirName) { Link = new CrossReference(dirName) });

                // create product category section
                Section category = new Section();

                // create link image
                category.Add(new Image("backarrow") { Class = "backlink", Link = new CrossReference("toc") });
                // create category name
                category.Add(new TextBlock(dirName) { Id = dirName, Class = "categoryHeader", Bookmark = new BookmarkEntry(dirName) });
                category.Add(new Br());

                // add products to category
                foreach (string imgFileName in Directory.EnumerateFiles(directory))
                {
                    string productInfo = Path.GetFileNameWithoutExtension(imgFileName);

                    // register product image
                    resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image(productInfo, imgFileName));

                    string[] tags = productInfo.Split(splitChars);

                    Section productDescription = new Section() {Class = "productDescription"};

                    productDescription.Add(new Image(productInfo) { Class = "productImage"});
                    productDescription.Add(new TextBlock(tags[1]) { Class = "measure" });
                    productDescription.Add(new TextBlock(string.Format("$ {0}", tags[2])) { Class = "price" });
                    productDescription.Add(new TextBlock(tags[0]) { Class = "label" });

                    category.Add(productDescription);
                }
                
                // finalize category creation
                document.Add(category);
            }

            string fileName = @"..\..\..\OutputDocuments\ProductCatalogWithTableOfContents.pdf";

            // save and open
            using (Stream stream = File.Create(fileName))
            {
                document.Write(stream, resourceManager, new PageBoundary(Boundaries.A4));
            }

            Process.Start(fileName);
        }
    }
}
