using System.Diagnostics;
using System.IO;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.PageProperties;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.FixedLayout.Resources.Fonts;
using Apitron.PDF.Kit.FixedLayout.Resources.Patterns;
using Apitron.PDF.Kit.FixedLayout.Resources.XObjects;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.Styles.Appearance;
using Apitron.PDF.Kit.Styles.Text;
using Font = Apitron.PDF.Kit.Styles.Text.Font;

namespace DocumentWithPatternBackground
{
    /// <summary>
    /// This sample shows how to use patterns for filling elements background. They can be defined using colored tiling pattern objects
    /// and set as current background or foreground color. Patterns can contain everything that can be defined using Fixed Layout API.        
    /// The code below uses tiling pattern containing an image to fill document background. 
    /// Read more about patterns in Apitron PDF Kit in Action, section 3.3.2.
    /// </summary>
    internal class Program
    {       
        private static void Main(string[] args)
        {
            using (FileStream fs = new FileStream(@"..\..\..\OutputDocuments\DocumentWithPatternBackground.pdf", FileMode.Create))
            {
                // create documents resource manager and register pattern image
                ResourceManager resourceManager = new ResourceManager();
                resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("snowflakes", "../../../data/snowflakes.png",true));
                
                // create colored tiling pattern, draw image inside and register it.
                TilingPattern pattern = new TilingPattern("myPattern",new Boundary(0,0,100,100),100,100,true);
                pattern.Content.AppendImage("snowflakes",0,0,100,100);

                resourceManager.RegisterResource(pattern);

                // create document
                FlowDocument document = new FlowDocument(){Padding = new Thickness(10)};

                // set color, defined as PATTERN color as a background color for document
                document.Background = new Color(PredefinedColorSpaces.Pattern,"myPattern");
               
                // add sample text block
                document.Add(new TextBlock("Sample for background patterns"){Color = RgbColors.Red});

                // generate document
                document.Write(fs, resourceManager, new PageBoundary(Boundaries.A4));
            }
            Process.Start(@"..\..\..\OutputDocuments\DocumentWithPatternBackground.pdf");
        }
    }
}
