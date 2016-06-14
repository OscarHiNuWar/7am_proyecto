using System;
using System.Drawing;

using Foundation;
using UIKit;
using QuickLook;

namespace CreateQuestionnaireFormSample
{
	public partial class CreateQuestionnaireFormSampleViewController : UIViewController
	{
		private ProductQuestionnaireForm currentForm;

		public CreateQuestionnaireFormSampleViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.


			// create current form and wire up button click handlers
			currentForm = new ApitronProductQuestionnaireForm ();

			btnSaveForm.TouchUpInside += OnSaveForm;
			btnLoadForm.TouchUpInside += OnLoadForm;
			btnClear.TouchUpInside += OnClearForm;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		public void OnSaveForm(object sender, EventArgs e)
		{
			string outFile = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"form.pdf");

			// pull data from the controls
			currentForm.SelectedProduct = txtProduct.Text;
			currentForm.UsagePeriod = txtUsagePeriod.Text;
			currentForm.SatisfactionLevel = txtSatisfaction.Text;
			currentForm.UserCompanyName = txtCompanyName.Text;
			currentForm.Feedback = txtFeedback.Text;

			// save and provide the preview option
			if( currentForm.Save (outFile))
			{
				DisplayMessage ("PDF form was succesfully saved.", new string[]{"Open preview"},
					(index)=> {
						if(index==1)
						{
							// launch preview
							QLPreviewController previewController = new QLPreviewController ();
							previewController.DataSource = new MyPreviewDataSource (outFile);
							this.PresentViewController (previewController, true, null);
						}
					}
				);
			}
			else 
			{
				DisplayMessage ("App failed to save PDF form.");
			}
		}

		public void OnLoadForm(object sender, EventArgs e)
		{
			string inputFile = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"form.pdf");

			// load PDF form
			if( ( currentForm = ProductQuestionnaireForm.Load (inputFile))!=null)
			{
				txtProduct.Text = currentForm.SelectedProduct;
				txtUsagePeriod.Text = currentForm.UsagePeriod;
				txtSatisfaction.Text = currentForm.SatisfactionLevel;
				txtCompanyName.Text = currentForm.UserCompanyName ;
				txtFeedback.Text = currentForm.Feedback;

				DisplayMessage ("PDF form was succesfully loaded.");
			}
			else
			{
				DisplayMessage ("PDF form loading failed. Created new form.");

				currentForm = new ApitronProductQuestionnaireForm ();

				OnClearForm (this, EventArgs.Empty);
			}
		}

		public void OnClearForm(object sender, EventArgs e)
		{
			if(currentForm!=null)
			{
				currentForm.Reset ();

				txtProduct.Text = string.Empty;
				txtUsagePeriod.Text = string.Empty;
				txtSatisfaction.Text = string.Empty;
				txtCompanyName.Text = string.Empty;
				txtFeedback.Text = string.Empty;
			}
		}

		/// <summary>
		/// Displays the message box with additional options if needed.
		/// </summary>
		private void DisplayMessage(string message, string[] buttonNames=null, ProcessButtonClickAtIndex handler=null)
		{
			if (buttonNames == null) 
			{
				new UIAlertView ("Apitron sample", message, null, "Ok", null).Show ();
			} 
			else 
			{
				new UIAlertView ("Apitron sample", message, new CustomAlertViewDelegate(handler) , "Ok", buttonNames).Show ();
			}
				
		}

		#endregion
	}
}

