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
using Font = Apitron.PDF.Kit.Styles.Text.Font;

namespace EmployeeCard
{
    /// <summary>
    /// This sample demonstrates how to create a simple employee card document.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // create resource manager and register image resource
            ResourceManager resourceManager = new ResourceManager();

            resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("photo", System.IO.Path.Combine("..\\..\\..\\data", "johndoe.png")));

            // create flow document
            FlowDocument document = new FlowDocument();
            document.Margin=new Thickness(Length.FromInches(0.25));

            // create style for personal photo
            document.StyleManager.RegisterStyle("image#employeePhoto", new Style(){Float = Float.Left, Border = new Border(2), BorderColor = RgbColors.Black, Margin = new Thickness(0,0,5,5)});
            // create style for header
            document.StyleManager.RegisterStyle("textblock.header", new Style() { Display = Display.InlineBlock, Align = Align.Center, Margin = new Thickness(0, 0, 0, 5) }); 
            // create style for data labels
            document.StyleManager.RegisterStyle("textblock.label",new Style(){Font = new Font(StandardFonts.HelveticaBold, 14), Margin = new Thickness(0,0,5,0)});
            // create style for text data
            document.StyleManager.RegisterStyle("textblock.data",new Style(){Font = new Font(StandardFonts.Helvetica, 14)});
            // create style for br element that goes after textblock
            document.StyleManager.RegisterStyle("textblock+br", new Style() { Height = 3});

            // fill the document
            document.Add(new TextBlock("Employee Card #123"){Class = "header label"});            

            document.Add(new Image("photo"){Id = "employeePhoto"});

            document.Add(new TextBlock("Name:"){Class = "label"});
            document.Add(new TextBlock("John") { Class = "data" });
            document.Add(new Br());

            document.Add(new TextBlock("Second name:"){Class = "label"});
            document.Add(new TextBlock("Doe") { Class = "data" });
            document.Add(new Br());

            document.Add(new TextBlock("Date of birth:"){Class = "label"});
            document.Add(new TextBlock("July 7, 1981") { Class = "data" });
            document.Add(new Br());

            document.Add(new TextBlock("SSN:"){Class = "label"});
            document.Add(new TextBlock("12341-123123-13215") { Class = "data" });
            document.Add(new Br());

            document.Add(new TextBlock("Position:"){Class = "label"});
            document.Add(new TextBlock("Sales manager I") { Class = "data" });
            document.Add(new Br());

            document.Add(new TextBlock("Compensation:") { Class = "label" });
            document.Add(new TextBlock("$ 72000 per year") { Class = "data" });
            document.Add(new Br());

            document.Add(new TextBlock("Work records:"){Class = "label"});
            document.Add(new TextBlock("Joined the company as junior sales manager in 2008." +
                                       " Demonstrated good results during his first 2 years and was promoted to Sales Manager I by request of CEO." +
                                       " Completed GBA, CBJ and ARC company trainings. He has been rated as 'Worker of the Year' in 2010, 2012 and 2013. Last annual review score 93 of 100. ") { Class = "data" });
            document.Add(new Br());

            string fileName = @"..\..\..\OutputDocuments\EmployeeCard.pdf";

            // save and open
            using (Stream stream = File.Create(fileName))
            {
                document.Write(stream, resourceManager, new PageBoundary(Boundaries.A4));
            }

            Process.Start(fileName);
        }
    }
}
