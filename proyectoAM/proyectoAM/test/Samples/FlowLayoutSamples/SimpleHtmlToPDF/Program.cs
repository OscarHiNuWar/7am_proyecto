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
using Apitron.PDF.Kit.Styles.Text;
using Font = Apitron.PDF.Kit.Styles.Text.Font;

namespace SimpleHtmlToPDF
{
    internal class Program
    {
        /// <summary>
        /// This sample shows how to create PDF document based on simple HTML styles it uses <see cref="ContentElement.FromMarkup"/> method to parse textual markup into a set of  content elements.
        /// See its description for more details.
        /// </summary>        
        private static void Main(string[] args)
        {
            using (FileStream fs = new FileStream(@"..\..\..\OutputDocuments\SimpleHtmlToPDF.pdf", FileMode.Create))
            {
                // create document
                FlowDocument document = new FlowDocument();
                document.PageHeader.Background = RgbColors.Black;
                document.Padding = new Thickness(10, 10, 10, 10);
                document.Background = RgbColors.Silver;


                // registering resources
                ResourceManager resourceManager = new ResourceManager();
                resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("myPicture", @"..\..\..\data\wp-header-logo.png"));
                resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.Fonts.Font("WebSymbols",  @"..\..\..\data\websymbols-regular-webfont.ttf"));
                resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("pic1", @"..\..\..\data\viet.jpg"));
                resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("pic2", @"..\..\..\data\bali.jpg"));
                resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("pic3", @"..\..\..\data\bolg.jpg"));

                document.StyleManager.RegisterStyle(".symbols", new Style { Font = new Font("WebSymbols", 14), Color = RgbColors.Blue, Padding = new Thickness(10,20,0,20)});
                document.StyleManager.RegisterStyle(".round", new Style   { Margin = new Thickness(20, 20, 20, 20), Display = Display.InlineBlock, Float = Float.Left, Width = Length.FromPoints(100), Height = Length.FromPoints(100), Border = new Border(1), BorderColor = RgbColors.Blue, BorderRadius = Length.FromPoints(50) });
                document.StyleManager.RegisterStyle(".bodySection", new Style { Width = Length.FromPercentage(33.33), Height = Length.FromPercentage(32.33), Display = Display.InlineBlock, Margin = new Thickness(4, 6, 0, 6), BorderRadius = 5 });
                
                // HTML styles, each style-class representing HTML tag.
                document.StyleManager.RegisterStyle(".h0", new Style { Font = new Font(StandardFonts.HelveticaOblique, 30), Display = Display.InlineBlock, Color = RgbColors.Orange, TextRenderingMode = TextRenderingMode.Fill, Width = Length.FromPercentage(30), Float = Float.Left});
                document.StyleManager.RegisterStyle(".h1", new Style { Font = new Font(StandardFonts.HelveticaBold, 24), Display = Display.Block, Color = RgbColors.White });
                document.StyleManager.RegisterStyle(".h2", new Style { Font = new Font(StandardFonts.HelveticaBold, 22), Display = Display.Block, Color = RgbColors.White, TextRenderingMode = TextRenderingMode.Stroke });
                document.StyleManager.RegisterStyle(".h3", new Style { Font = new Font(StandardFonts.HelveticaBold, 20), Display = Display.Block, Color = RgbColors.White, Padding = new Thickness(0,8,0,0)});
                document.StyleManager.RegisterStyle(".center", new Style { Align = Align.Center, VerticalAlign = VerticalAlign.Middle });
                
                document.StyleManager.RegisterStyle(".b", new Style      { Font = new Font(StandardFonts.HelveticaBold, 14) });     
                document.StyleManager.RegisterStyle(".i", new Style      { Font = new Font(StandardFonts.HelveticaOblique, 14), Color = RgbColors.Red});
                document.StyleManager.RegisterStyle(".bi", new Style     { Font = new Font(StandardFonts.HelveticaBoldOblique, 10) });
                document.StyleManager.RegisterStyle(".u", new Style      { TextDecoration = new TextDecoration(TextDecorationOptions.Underline) });
                document.StyleManager.RegisterStyle(".strike", new Style { TextDecoration = new TextDecoration(TextDecorationOptions.LineThrough) });
                document.StyleManager.RegisterStyle(".sup", new Style    { ScriptLevel = ScriptLevel.Superscript, Color = RgbColors.Orange, Font = new Font(StandardFonts.Helvetica, 10), Display = Display.InlineBlock, Width = 10});
                document.StyleManager.RegisterStyle(".pre", new Style    { Align = Align.Justify, TextIndent = 3, Font = new Font(StandardFonts.Helvetica, 13), LineHeight = 15, Padding = new Thickness(2, 2, 2, 2)});
                document.StyleManager.RegisterStyle(".button", new Style { Align = Align.Center, VerticalAlign = VerticalAlign.Bottom, Font = new Font(StandardFonts.Helvetica, 14), Margin = new Thickness(120,40,0,0), Padding = new Thickness(5,5,5,5), Border = new Border(0.5), Background = RgbColors.LightCyan, BorderColor = RgbColors.Cyan, BorderRadius = 3});
               
                document.StyleManager.RegisterStyle(".a", new Style { Color = RgbColors.Blue, TextDecoration = new TextDecoration(TextDecorationOptions.Underline) });
                document.StyleManager.RegisterStyle(".li",new Style {ListStyle = ListStyle.ListItem, ListMarker = ListMarker.Circle, ListMarkerPadding = new Thickness(5, 2, 2, 2)});

                // add section based on HTML 
                Section body = new Section{Padding = new Thickness(10, 10, 10, 10), BorderTop = null, BorderRadius = 12, Background = RgbColors.Black, Height = Length.FromPercentage(100)};
                body.Add(new Section(ContentElement.FromMarkup("<h1><img width='303pt' height='53pt' src='myPicture'/><center>Simple HTML to PDF sample.</center></h1>")));
                body.Add(new Section(ContentElement.FromMarkup("<p><round><img src='pic1'/></round><h2><u>Vietnam</u></h2><p>Astonishingly exotic and utterly compelling, <b>Vietnam</b> is a country of breathtaking natural beauty with an incredible heritage that quickly becomes addictive.</p><br/><h3>Activities in Vietnam</h3><li>Cu Chi Tunnels and Cao Dai Temple Full-Day Tour.</li><li>Explore bustling Ho Chi Minh City (formerly known as Saigon).</li><li>Experience the vibrant city lights of Ho Chi Minh City when you join the Bonsai for a memorable.</li>" +
                                                               "<round><img src='pic2'/></round><h2><u>Bali</u></h2><p>The mere mention of Bali evokes thoughts of a paradise. It's more than a place; it's a <strike>boredom</strike> mood, an aspiration, a tropical state of mind.</p><h3>Bali's essence</h3><p>Yes, Bali has beaches, surfing, diving, and resorts great and small, but it's the essence of Bali – and the Balinese – that makes it so much more than just a <i>fun-in-the-sun</i> retreat. It is possible to take the cliché of the smiling Balinese too far but, in reality, the inhabitants of this small island are indeed a generous, genuinely warm people.</p>" +
                                                               "<br/><round><img src='pic3'/></round><h2><u>Bulgaria</u></h2><p>Bulgaria may be best known for its long, sandy Black Sea beaches, but  you’ll also find treasure-filled cities, landscapes ideal for hiking,  skiing, cycling, climbing and wildlife watching, churches and  monasteries...</p><br/></p>")) { Background = RgbColors.Aqua, Height = Length.FromPercentage(56), Width = Length.FromPercentage(100), BorderRadius = 12, Padding = new Thickness(15, 15, 15, 5) });
                // add section with 3 columns
                Section table  = new Section { Display = Display.Inline, Height = Length.FromPercentage(100) };
                Section left   = new Section { Class = "bodySection", Background = RgbColors.Pink};
                Section center = new Section { Class = "bodySection", Background = RgbColors.Yellow };
                Section right  = new Section { Class = "bodySection", Background = RgbColors.LightGreen };
                left.Add  (new Section(ContentElement.FromMarkup("<h2><center>Travel</center></h2><pre><a>Buzzing</a> with cultural activities - museums, monuments, parks and gardens, theatre..</pre><br/><pre><a>Travel n tourism information.</a> Contact official government tourist information centres around the world</pre><br/><button>More...</button>")));
                center.Add(new Section(ContentElement.FromMarkup("<h2><center>Booking</center></h2><pre>Provides an online <b>reservation</b> service for accommodation and tourism industries, with travel information for a range of destinations in:</pre><li>Europe.</li><li>America.</li><li>Australia.</li>")));
                right.Add (new Section(ContentElement.FromMarkup("<h2><center>Weather</center></h2><h0>+27<sup>o</sup></h0><pre>We provide Tourist Board Information for all the major destinations in the world. Learn about Sydney's weather, climate and average temperatures. Find seasonal activities and events on offer throughout the year. Experience four seasons in one day when you visit Melbourne and surrounds.</pre>")));
                
                // combine sections into the final document
                table.Add(left);
                table.Add(center);
                table.Add(right);
                body.Add(table);
                document.Add(body);
                
                // add page footer
                document.PageFooter.Add(new Section(new TextBlock("Follow us:"), new TextBlock("t"){Class="symbols", Link = new LinkUri("www.apitron.com")}, new TextBlock("f"){Class="symbols"}, new TextBlock("l"){Class="symbols"}){Align = Align.Right});
                document.Write(fs, resourceManager, new PageBoundary(Boundaries.A4));
            }
            Process.Start(@"..\..\..\OutputDocuments\SimpleHtmlToPDF.pdf");
        }
    }
}
