using System.Diagnostics;
using System.IO;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.PageProperties;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.FixedLayout.Resources.Fonts;
using Apitron.PDF.Kit.FlowLayout.Content.Controls;
using Apitron.PDF.Kit.Interactive.Actions;
using Apitron.PDF.Kit.Interactive.Forms;
using Apitron.PDF.Kit.Styles;
using Font = Apitron.PDF.Kit.Styles.Text.Font;

namespace FillFormUsingJavascriptAction
{
    /// <summary>
    /// This sample shows how modify form field value using <see cref="JavaScriptAction"/> assigned to a <see cref="PushButton"/>.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // create resource manager for all referenced resources
            ResourceManager resourceManager = new ResourceManager();

            // create document
            FlowDocument document = new FlowDocument() { Margin = new Thickness(5) };

            // add fields for text and button
            document.Fields.Add(new TextField("testField", "this text will be changed when you click on a button"));
            document.Fields.Add(new PushbuttonField("button", "Change text"));

            // add controls linked to document fields, so they will be shown on a page
            document.Add(new TextBox("testField") { Font = new Font(StandardFonts.Helvetica, 16) });
            // add javascript action to a button which changes text of the text field
            document.Add(new PushButton("button", new JavaScriptAction(string.Format("var f = this.getField(\"testField\"); f.value=\"{0}\";", "created using Apitron PDF Kit"))) { Width = 100, Height = 20 });

            string fileName = @"..\..\..\OutputDocuments\FillFormUsingJavascriptAction.pdf";

            // save and open
            using (Stream stream = File.Create(fileName))
            {

                document.Write(stream, resourceManager, new PageBoundary(Boundaries.A4));
            }

            Process.Start(fileName);
        }
    }
}
