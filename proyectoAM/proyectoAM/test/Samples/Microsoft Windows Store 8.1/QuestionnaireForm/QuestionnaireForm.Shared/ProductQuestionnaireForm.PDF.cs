using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.PageProperties;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.FixedLayout.Resources.Fonts;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.FlowLayout.Content.Controls;
using Apitron.PDF.Kit.Interactive.Forms;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.Styles.Appearance;
using Font = Apitron.PDF.Kit.Styles.Text.Font;

namespace QuestionnaireForm
{
    /// <summary>
    /// This part of implementation contais PDF related code.
    /// </summary>
    internal partial class ProductQuestionnaireForm
    {
        #region PDF save/restore

        /// <summary>
        /// Saves current form data to local storage.
        /// </summary>
        public async Task<bool> SaveToLocalStorage(string fileName)
        {
            try
            {
                FlowDocument pdfDocument = CreatePDFDocument();

                StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,CreationCollisionOption.ReplaceExisting);

                using (Stream outputStream = await file.OpenStreamForWriteAsync())
                {
                    pdfDocument.Write(outputStream, new ResourceManager(), new PageBoundary(Boundaries.A4));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Load form data from PDF file using given filename.
        /// </summary>
        public static async Task<ProductQuestionnaireForm> LoadFromLocalStorage(string fileName)
        {
            try
            {               
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);

                using (Stream inputStream = await file.OpenStreamForReadAsync())
                {
                    // use fixed document for reading form data
                    FixedDocument pdfDocument = new FixedDocument(inputStream);

                    // create and initialize form object instance
                    ProductQuestionnaireForm form = new ApitronProductQuestionnaireForm();
                    form.SelectedProduct = ((TextField)pdfDocument.AcroForm["SelectedProduct"]).Text;
                    form.UsagePeriod = ((TextField)pdfDocument.AcroForm["UsagePeriod"]).Text;
                    form.SatisfactionLevel = ((TextField)pdfDocument.AcroForm["SatisfactionLevel"]).Text;
                    form.UserCompanyName = ((TextField)pdfDocument.AcroForm["UserCompanyName"]).Text;
                    form.Feedback = ((TextField)pdfDocument.AcroForm["Feedback"]).Text;

                    return form;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return null;
        }

        /// <summary>
        /// Creates FlowDocument that represents PDF form.
        /// </summary>
        private FlowDocument CreatePDFDocument()
        {
            FlowDocument document = new FlowDocument(){Margin = new Apitron.PDF.Kit.Styles.Thickness(10)};

            // add form fields and set text values based on form data
            document.Fields.Add(new TextField("SelectedProduct", SelectedProduct){IsReadOnly = true});
            document.Fields.Add(new TextField("UsagePeriod", UsagePeriod) { IsReadOnly = true });
            document.Fields.Add(new TextField("SatisfactionLevel", SatisfactionLevel) { IsReadOnly = true });
            document.Fields.Add(new TextField("UserCompanyName", UserCompanyName) { IsReadOnly = true });
            document.Fields.Add(new TextField("Feedback", Feedback) { IsReadOnly = true, IsMultiline = true});

            // register styles defining controls appearance
            document.StyleManager.RegisterStyle(".formHeader",new Apitron.PDF.Kit.Styles.Style(){Display = Display.Block, Font = new Font(StandardFonts.HelveticaBold, 20)});
            document.StyleManager.RegisterStyle("TextBlock, TextBox", new Apitron.PDF.Kit.Styles.Style() { Font = new Font(StandardFonts.Helvetica, 14)});
            document.StyleManager.RegisterStyle("TextBlock + TextBox", new Apitron.PDF.Kit.Styles.Style() { BorderColor = RgbColors.Black, Border = new Border(1), Height = 20, Background = RgbColors.LightGray});
            document.StyleManager.RegisterStyle("Br", new Apitron.PDF.Kit.Styles.Style() { Height = 10});

            // add document content elements
            document.Add(new TextBlock("Product questionnaire form"){Class = "formHeader"});
            document.Add(new TextBlock("Generated on " + DateTime.Now.ToString("dd/MM/yyyy HH:mm")));
            document.Add(new Hr(){Height = 2, Margin = new Thickness(0,5,0,5)});
            document.Add(new TextBlock("Selected product:"));
            document.Add(new TextBox("SelectedProduct"));
            document.Add(new Br());

            document.Add(new TextBlock("Usage period:"));
            document.Add(new TextBox("UsagePeriod"));
            document.Add(new Br());

            document.Add(new TextBlock("SatisfactionLevel:"));
            document.Add(new TextBox("SatisfactionLevel"));
            document.Add(new Br());

            document.Add(new TextBlock("User company name:"));
            document.Add(new TextBox("UserCompanyName"));
            document.Add(new Br());

            document.Add(new TextBlock("Feedback:"));
            document.Add(new TextBox("Feedback"){Height = 100});

            return document;
        }
     
        #endregion
    }
}
