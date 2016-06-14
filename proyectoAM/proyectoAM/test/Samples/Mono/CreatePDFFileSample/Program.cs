using System;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FlowLayout.Content;
using System.IO;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.Styles.Text;
using System.Diagnostics;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.FixedLayout.Resources.Fonts;
using Font = Apitron.PDF.Kit.Styles.Text.Font;
using Apitron.PDF.Kit.Styles.Appearance;
using Apitron.PDF.Kit.FlowLayout;

namespace CreatePDFFileSample
{
	class MainClass
	{
		public static void Main (string[] args)
		{			
			// create flow layout PDF document and resource manager
			ResourceManager resourceManager = new ResourceManager ();
			FlowDocument pdfDocument = new FlowDocument () {Margin = new Thickness(10), Align = Align.Justify};

			// register styles
			pdfDocument.StyleManager.RegisterStyle ("textblock",new Style()
				{
					Font = new Font(StandardFonts.Helvetica, 20),
					Color = RgbColors.Black,
				});

			pdfDocument.StyleManager.RegisterStyle ("textblock.a", new Style()
				{
					Color = RgbColors.Blue	
				});

			// add simple text blok element
			pdfDocument.Add (new TextBlock ("This PDF file was created using Apitron PDF Kit for .NET component. " +
				"It's a cross-platform library that can be used to manipulate PDF in Windows, " +
				"Mac, iOS, Android and Mono apps."));

			// add text containing the block acting as an interactive link
			pdfDocument.AddItems(ContentElement.FromMarkup("Visit our <a href='www.apitron.com'>website</a> " +
				"if you want to learn more about our PDF components."));

			string outputFileName = "document.pdf";

			// save the PDF file
			using (Stream outputStream = File.Create (outputFileName)) 
			{							
				pdfDocument.Write (outputStream, resourceManager);
			}

			// open file with default system viewer
			Process.Start (outputFileName);
		}
	}
}
