using System.Diagnostics;
using System.IO;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.PageProperties;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.Styles.Appearance;
using Font = Apitron.PDF.Kit.Styles.Text.Font;


namespace MultilingualDocument
{
    /// <summary>
    /// This sample shows how to create PDF document containing text written using various languages.
    /// It uses system fonts, so if any of the text entries is rendered wrong it means that the required font is missing.
    /// Simply substitute a font containing glyphs for selected language, do this by either registering font file via <see cref="ResourceManager.RegisterResource"/>
    /// or by providing its name(if it's a system font).
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ResourceManager resourceManager = new ResourceManager();

            // create document
            FlowDocument document = new FlowDocument() { Margin = new Thickness(5) };

            // register font styles for various textblock classes
            document.StyleManager.RegisterStyle("textblock", new Style() { Display = Display.Block, Font = new Font("Arial", 16) });
            document.StyleManager.RegisterStyle("textblock.chinese", new Style() { Font = new Font("Microsoft YaHei", 16) });
            document.StyleManager.RegisterStyle("textblock.japanese", new Style() { Font = new Font("MS Gothic", 16) });
            document.StyleManager.RegisterStyle("textblock.hindi", new Style() { Font = new Font("Mangal", 16) });

            // add textblocks
            document.Add(new TextBlock("Chinese simplified: 利用创建 Apitron PDF KIT") { Class = "chinese" });
            document.Add(new TextBlock("Czech: vytvořeno pomocí Apitron PDF KIT"));
            document.Add(new TextBlock("English: created using Apitron PDF KIT"));
            document.Add(new TextBlock("Finnish: luotu käyttäen Apitron PDF KIT"));
            document.Add(new TextBlock("Hindi: का उपयोग कर बनाया Apitron PDF KIT") { Class = "hindi" });
            document.Add(new TextBlock("Japanese: 使用して作成 Apitron PDF KIT") { Class = "japanese" });
            document.Add(new TextBlock("Turkish:  kullanılarak oluşturulan Apitron PDF KIT"));
            document.Add(new TextBlock("Russian: создано с помощью Apitron PDF KIT"));            

            string fileName = @"..\..\..\OutputDocuments\MultilingualDocument.pdf";

            // save and open
            using (Stream stream = File.Create(fileName))
            {
                document.Write(stream, resourceManager, new PageBoundary(Boundaries.A4));
            }

            Process.Start(fileName);
        }
    }
}
