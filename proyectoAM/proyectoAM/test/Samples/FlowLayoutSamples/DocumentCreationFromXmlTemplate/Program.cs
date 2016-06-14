using System.Diagnostics;
using System.IO;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.PageProperties;
using Apitron.PDF.Kit.Interactive.Forms;
using Apitron.PDF.Kit.FixedLayout.Resources;

namespace DocumentCreationFromXmlTemplate
{
    /// <summary>
    /// This samples demonstrates how to load document from predefined XML template using <see cref="FlowDocument.LoadFromXml"/>. 
    /// It also shows how one can alter loaded document by changing its <see cref="FlowDocument.Fields"/> thus producing a new customized document on demand.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ResourceManager resourceManager = new ResourceManager();
            
            FlowDocument document;

            // load document from XML template
            using (Stream inputStream = File.OpenRead(Path.Combine("..\\..\\..\\data", "form.xml")))
            {
                document = FlowDocument.LoadFromXml(inputStream, resourceManager);
            }
            
            // set new values for its fields
            ((TextField)document.Fields["applicantName"]).Text = "John Doe";
            ((TextField)document.Fields["applicantPosition"]).Text = "software developer";
            ((TextField)document.Fields["dateFrom"]).Text = "1.1.2014";
            ((TextField)document.Fields["dateTo"]).Text = "1.6.2014";
            ((TextField)document.Fields["issuerName"]).Text = "Mrs. Jane Doe";
            ((TextField)document.Fields["issuerPosition"]).Text = "Head of HR dept.";
            ((TextField)document.Fields["issueDate"]).Text = "1.1.2015";

            string fileName = @"..\..\..\OutputDocuments\DocumentCreationFromXmlTemplate.pdf";

            // save and open
            using (Stream stream = File.Create(fileName))
            {
                document.Write(stream, resourceManager, new PageBoundary(Boundaries.A4));
            }

            Process.Start(fileName);
        }
    }
}
