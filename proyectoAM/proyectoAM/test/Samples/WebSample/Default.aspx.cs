using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.Content;
using Apitron.PDF.Kit.Interactive.Actions;
using Apitron.PDF.Kit.Interactive.Annotations;
using Apitron.PDF.Kit.Interactive.Forms;

namespace WebSample
{
    public partial class Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // output path
            string out_path = Server.MapPath("~\\App_Code\\Documents\\testfile.pdf");

            // create new file or memory stream
            using (FileStream fs = new FileStream(out_path, FileMode.Create))
            {
                // this object represents a PDF fixed document
                FixedDocument document = new FixedDocument();
                Apitron.PDF.Kit.FixedLayout.Page page = new Apitron.PDF.Kit.FixedLayout.Page();

                // add page
                document.Pages.Add(new Page());

                // add some text content
                TextObject text = new TextObject("Helvetica", 38);
                text.Translate(10, 550);
                text.SetTextRenderingMode(RenderingMode.FillText);
                text.AppendText("Very simple PDF creation process");

                // append text and image
                document.Pages[0].Content.AppendText(text);

                document.Save(fs);
            }
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.WriteFile(Server.MapPath("~\\App_Code\\Documents\\testfile.pdf"));
            Response.Flush();
            Response.End();
       }
   }
}
