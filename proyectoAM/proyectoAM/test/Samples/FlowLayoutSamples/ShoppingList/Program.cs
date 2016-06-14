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

namespace ShoppingList
{
    internal class Program
    {
        /// <summary>
        /// This sample shows how to create lists with different numeration styles.
        /// Lists are being created by setting a style property <see cref="ListStyle"/> of a <see cref="Section"/> element to <see cref="ListStyle.Ordered"/> or <see cref="ListStyle.Unordered"/>
        /// thus producing the numerated or non-numerated lists accordingly. An element becomes a part of the list once it gets its <see cref="ListStyle"/> assigned to <see cref="ListStyle.ListItem"/>.
        /// Numbering styles or markers are being set using <see cref="ListMarker"/>. For setting counter base you may use <see cref="ListCounter"/> property.
        /// It's also possible to use custom markers(images etc.), see <see cref="ListMarker.FromResourceId"/> method.
        /// Nested lists can be created by inserting one list(section) into another.
        /// </summary>        
        private static void Main(string[] args)
        {
            using (FileStream fs = new FileStream(@"..\..\..\OutputDocuments\ShoppingList.pdf", FileMode.Create))
            {
                // create document
                FlowDocument document = new FlowDocument();
                document.Padding = new Thickness(10, 10, 10, 10);
                document.Background = RgbColors.Beige;

                // registering resources
                ResourceManager resourceManager = new ResourceManager();
                resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("cart", @"..\..\..\data\cart.png"));
                resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("star", @"..\..\..\data\star.png"));

                Section title = new Section();
                title.Add(new TextBlock("GROCERY") { Align = Align.Right, Color = RgbColors.DarkRed, Display = Display.InlineBlock, Width = Length.FromPercentage(65), Font = new Font(StandardFonts.HelveticaBold, 28), TextRenderingMode = TextRenderingMode.Stroke });
                title.Add(new Image("cart") { Height = Length.FromPoints(32), Width = Length.FromPoints(32), Align = Align.Left, Display = Display.InlineBlock });
                document.Add(title);

                Section shoppingList = new Section{Align = Align.Center, BorderRadius = 12, Background = RgbColors.DarkBlue, Width = Length.FromPercentage(100)};
                shoppingList.Add(new TextBlock("SHOPPING LIST"){ Color = RgbColors.Wheat, Font = new Font(StandardFonts.HelveticaBold, 20)});

                Section products = new Section  { Height = Length.FromPercentage(80), Margin = new Thickness(10,0,10,0),Font = new Font(StandardFonts.TimesBold, 18)};
                Section leftList = new Section  { ListStyle = ListStyle.Ordered, Display = Display.InlineBlock, Width = Length.FromPercentage(49), Background = RgbColors.Bisque, Padding = new Thickness(0, 2, 0, 1), Margin = new Thickness(0, 0, 5, 0) };
                Section rightList = new Section { Display = Display.InlineBlock, Width = Length.FromPercentage(49), Background = RgbColors.Bisque, Padding = new Thickness(0, 2, 0, 1), Margin = new Thickness(0, 0, 5, 0) };


                document.StyleManager.RegisterStyle("section#breadAndDesserts", new Style { ListStyle = ListStyle.Unordered, ListMarker = ListMarker.Circle });
                document.StyleManager.RegisterStyle("section#breadAndDesserts > textBlock", new Style { ListStyle = ListStyle.ListItem, Padding = new Thickness(4,0,0,0) });
                document.StyleManager.RegisterStyle("section#freshDrinks", new Style { ListStyle = ListStyle.Ordered, ListMarker = ListMarker.Decimal });
                document.StyleManager.RegisterStyle("section#freshDrinks > textBlock", new Style { ListStyle = ListStyle.ListItem, Padding = new Thickness(4,0,0,0) });
                document.StyleManager.RegisterStyle("section#meatChicken", new Style { ListStyle = ListStyle.Ordered, ListMarker = ListMarker.FromResourceId("star") });
                document.StyleManager.RegisterStyle("section#meatChicken > textBlock", new Style { ListStyle = ListStyle.ListItem, Padding = new Thickness(4,0,0,0) });
                document.StyleManager.RegisterStyle("section#beerDrinks", new Style { ListStyle = ListStyle.Unordered, ListMarker = ListMarker.None });
                document.StyleManager.RegisterStyle("section#beerDrinks > textBlock", new Style { ListStyle = ListStyle.ListItem, Padding = new Thickness(4,0,0,0)});
                document.StyleManager.RegisterStyle("section#fruitsVegetables", new Style { ListStyle = ListStyle.Ordered, ListMarker = ListMarker.Triangle });
                document.StyleManager.RegisterStyle("section#fruitsVegetables > textBlock", new Style { ListStyle = ListStyle.ListItem, Padding = new Thickness(4,0,0,0) });
                document.StyleManager.RegisterStyle("section#householdGoods", new Style { ListStyle = ListStyle.Ordered, ListMarker = ListMarker.UpperLatin, ListCounter = new ListCounter(0) });
                document.StyleManager.RegisterStyle("section#householdGoods > textBlock", new Style { ListStyle = ListStyle.ListItem, Padding = new Thickness(4,0,0,0)});
                document.StyleManager.RegisterStyle("section#dairy", new Style { ListStyle = ListStyle.Ordered, ListMarker = ListMarker.Decimal });
                document.StyleManager.RegisterStyle("section#dairy > textBlock", new Style { ListStyle = ListStyle.ListItem, Padding = new Thickness(4,0,0,0)});
                document.StyleManager.RegisterStyle(".header", new Style { Display = Display.Inline, Background = RgbColors.RosyBrown });


                Section breadAndDesserts = new Section { Id = "breadAndDesserts" };
                breadAndDesserts.Add(new TextBlock("apple cake"));
                breadAndDesserts.Add(new TextBlock("dark bread 600gr"));
                breadAndDesserts.Add(new TextBlock("loaf"));
                breadAndDesserts.Add(new TextBlock("bagels"));
                breadAndDesserts.Add(new TextBlock("cookies"));
                breadAndDesserts.Add(new TextBlock("tart"));

                Section freshDrinks = new Section { Id = "freshDrinks" };
                freshDrinks.Add(new TextBlock("Monster energy"));
                freshDrinks.Add(new TextBlock("Pepsi"));
                freshDrinks.Add(new TextBlock("Cola"));

                Section meatChicken = new Section { Id = "meatChicken" };
                meatChicken.Add(new TextBlock("forcemeat 500gr") );
                meatChicken.Add(new TextBlock("hamburger"));
                meatChicken.Add(new TextBlock("sausage"));
                meatChicken.Add(new TextBlock("pork meat"));
                meatChicken.Add(new TextBlock("ham"));

                Section beerDrinks = new Section { Id = "beerDrinks" };
                beerDrinks.Add(new TextBlock("Amstel"));
                beerDrinks.Add(new TextBlock("Courvoisier VSOP"));

                Section dairy = new Section { Id = "dairy" };
                dairy.Add(new TextBlock("milk 2L"));
                dairy.Add(new TextBlock("cheese"));
                dairy.Add(new TextBlock("yogurt"));
                dairy.Add(new TextBlock("margarine"));

                Section fruitsVegetables = new Section { Id = "fruitsVegetables" };
                fruitsVegetables.Add(new TextBlock("broccoli"));
                fruitsVegetables.Add(new TextBlock("cauliflower"));
                fruitsVegetables.Add(new TextBlock("garlik"));
                fruitsVegetables.Add(new TextBlock("mango"));
                fruitsVegetables.Add(new TextBlock("stuffed cabbadge"));
                fruitsVegetables.Add(new TextBlock("potato"));

                Section householdGoods = new Section { Id = "householdGoods" };
                householdGoods.Add(new TextBlock("Ariel soap"));
                householdGoods.Add(new TextBlock("shampoo"));
                householdGoods.Add(new TextBlock("batteries AA"));
                householdGoods.Add(new TextBlock("mop"));
                householdGoods.Add(new TextBlock("brush"));

                leftList.Add(new Section(new TextBlock("Bread & desserts")) { Class="header" });
                leftList.Add(breadAndDesserts);

                leftList.Add(new Section(new TextBlock("Beer and strong drinks")) { Class="header" });
                leftList.Add(beerDrinks);

                leftList.Add(new Section(new TextBlock("Fresh drinks")) { Class = "header", ListStyle = ListStyle.ListItem, ListMarker = ListMarker.Decimal, ListMarkerPadding = new Thickness(2, 0, 0, 0) });
                leftList.Add(freshDrinks);
                
                leftList.Add(new Section(new TextBlock("Dairy")) { Class = "header", ListStyle = ListStyle.ListItem, ListMarker = ListMarker.Decimal, ListMarkerPadding = new Thickness(2, 0, 0, 0) });
                leftList.Add(dairy);

                rightList.Add(new Section(new TextBlock("Fruits & vegetables")) { Class = "header" });
                rightList.Add(fruitsVegetables);

                rightList.Add(new Section(new TextBlock("Meat & chicken")) { Class = "header" });
                rightList.Add(meatChicken);
                

                rightList.Add(new Section(new TextBlock("Household Goods")) { Class = "header" });
                rightList.Add(householdGoods);

                products.Add(leftList);
                products.Add(rightList);
                shoppingList.Add(products);
                shoppingList.Add(new TextBlock("You can do it!"){ Color = RgbColors.Wheat, Font = new Font(StandardFonts.HelveticaBold, 20) });
                document.Add(shoppingList);

                document.Write(fs, resourceManager, new PageBoundary(Boundaries.A5));
            }
            Process.Start(@"..\..\..\OutputDocuments\ShoppingList.pdf");
        }
    }
}
