using System.IO;
using System.Windows;
using System.Windows.Controls;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.FixedLayout.Resources.Fonts;
using Apitron.PDF.Kit.Styles;
using Font = Apitron.PDF.Kit.Styles.Text.Font;
using Style = Apitron.PDF.Kit.Styles.Style;
using TextBlock = Apitron.PDF.Kit.FlowLayout.Content.TextBlock;
using Thickness = Apitron.PDF.Kit.Styles.Thickness;

namespace CreatePDFSample
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSaveToPDFClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "*.pdf|*.pdf";

            if (saveFile.ShowDialog() == true)
            {
                // open stream for writing
                using (Stream outputStream = saveFile.OpenFile())
                {                    
                    ResourceManager resourceManager = new ResourceManager();
                    // create flow document
                    FlowDocument doc = new FlowDocument() {Margin = new Thickness(10)};
                    // register style for text block
                    doc.StyleManager.RegisterStyle("textblock",new Style(){Font = new Font(StandardFonts.HelveticaBold, 18), Color = RgbColors.DarkBlue});
                    // add the text block with our text
                    doc.Add(new TextBlock(TextBox.Text));
                    // save document
                    doc.Write(outputStream,resourceManager );
                }
            }
        }
    }
}
