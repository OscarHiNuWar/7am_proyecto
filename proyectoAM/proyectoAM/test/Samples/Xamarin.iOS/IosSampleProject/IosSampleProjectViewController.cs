using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.IO;
using MonoTouch.CoreGraphics;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.FixedLayout.Resources;
using MonoTouch.QuickLook;
using Apitron.PDF.Kit.Interactive.Forms;
using Apitron.PDF.Kit.Interactive.Forms.SignatureSettings;
using Apitron.PDF.Kit.Interactive.Forms.Signature;
using Apitron.PDF.Kit.FlowLayout.Content.Controls;
using Apitron.PDF.Kit.Styles.Appearance;
using Apitron.PDF.Kit.FixedLayout.PageProperties;
using Apitron.PDF.Kit.Styles;
using Font = Apitron.PDF.Kit.Styles.Text.Font;
using Apitron.PDF.Kit.FixedLayout.Resources.Fonts;

namespace IosSampleProject
{
	public partial class IosSampleProjectViewController : UIViewController
	{
		public IosSampleProjectViewController () : base ("IosSampleProjectViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.

			// sign up for a touch up event for our button
			btnGenerateFile.TouchUpInside += BtnClickHandler;
		}

		// handles the touch up event for our button
		private void BtnClickHandler(object sender,EventArgs args)
		{
			string outputFile = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "CertificateOfCompletion.pdf");

			ResourceManager resourceManager = new ResourceManager();

			string bundlePath = Path.Combine(NSBundle.MainBundle.BundlePath,"Data");

			// register image resources
			resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("background",Path.Combine (bundlePath, "certificate_template.jpg")));
			resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("signatureImage", Path.Combine(bundlePath,"signatureImage.bmp")));

			// create document and set its background to image
			FlowDocument document = new FlowDocument() { BackgroundImage = new BackgroundImage("background", Length.FromPercentage(100), Length.FromPercentage(100)), BackgroundRepeat = BackgroundRepeat.NoRepeat, Align = Align.Center };

			// set font for all textblocks in document
			document.StyleManager.RegisterStyle("textblock", new Style() { Font = new Font(StandardFonts.CourierBold, 14) });

			// open digital certificate and prepare for signing, add signature field named "signature" to document
			using (Stream certificateStream = File.OpenRead(Path.Combine(bundlePath,"JohnDoe.pfx")))
			{
				document.Fields.Add(new SignatureField("signature")
					{
						ViewSettings =
							new SignatureFieldViewSettings() { 
							Graphic = Graphic.Image, 
							GraphicResourceID = "signatureImage", 
							Description = Apitron.PDF.Kit.Interactive.Forms.SignatureSettings.Description.None },
						Signature =
							Signature.Create(new Pkcs12Store(certificateStream, "password"))
						});
			}

			// fill certificate with data, spacing between elements will be created by using BR elements with custom height
			document.Add(new Br() { Height = 220 });
			document.Add(new TextBlock("issued by Apitron LTD"));
			document.Add(new Br() { Height = 110 });
			document.Add(new TextBlock("Mr. Unnamed User"));
			document.Add(new Br() { Height = 150 });
			document.Add(new TextBlock("Apitron Samples Review Training"));
			document.Add(new Br() { Height = 30 });

			// use this section to create collective left padding 
			Section section = new Section() { Display = Display.Inline, Padding = new Thickness(150, 0, 0, 0) };

			// add signature control linked to existing field
			section.Add(new SignatureControl("signature") { Width = 100, Height = 50 });
			section.Add(new Br() { Height = 35 });
			section.Add(new TextBlock("Chef Instructor"));
			section.Add(new Br() { Height = 45 });
			section.Add(new TextBlock("5.12.2014"));

			document.Add(section);

			// save and open
			using (Stream stream = File.Create(outputFile))
			{
				document.Write(stream, resourceManager, new PageBoundary(Boundaries.A4));
			}

			// launch preview
			QLPreviewController previewController = new QLPreviewController ();
			previewController.DataSource = new MyPreviewDataSource (outputFile);

			this.PresentViewController (previewController, true, null);
		}
	}
}


