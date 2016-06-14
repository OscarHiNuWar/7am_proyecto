using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace CreateQuestionnaireFormSample
{
    using System.IO;

    using Android.Graphics;
    using Android.Provider;


	[Activity(Label = "CreateQuestionnaireFormSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {		
		#region Fields

		ToastLength toastDelay = ToastLength.Short;

		Spinner spinnerProduct;
		Spinner spinnerUsagePeriod;
		Spinner spinnerSatisfactionLevel;

		EditText txtCompanyName;
		EditText txtFeedback;

		ProductQuestionnaireForm currentForm;

		#endregion

        #region UI init

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			currentForm = new ApitronProductQuestionnaireForm ();

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// initialize UI
			Button btnSave = FindViewById<Button> (Resource.Id.btnSaveForm);
			btnSave.Click += new EventHandler (OnSaveClick);

			Button btnLoad = FindViewById<Button> (Resource.Id.btnLoadForm);
			btnLoad.Click += new EventHandler (OnLoadClick);

			Button btnClear = FindViewById<Button> (Resource.Id.btnClear);
			btnClear.Click += new EventHandler (OnClearClick);

			spinnerProduct = FindViewById<Spinner> (Resource.Id.spinnerProduct);
			spinnerUsagePeriod = FindViewById<Spinner> (Resource.Id.spinnerUsagePeriod);
			spinnerSatisfactionLevel = FindViewById<Spinner> (Resource.Id.spinnerSatisfactionLevel);

			txtCompanyName = FindViewById<EditText> (Resource.Id.txtCompanyName);
			txtFeedback = FindViewById<EditText> (Resource.Id.txtFeedback);

			spinnerProduct.Adapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleSpinnerItem, currentForm.Products);
			spinnerUsagePeriod.Adapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleSpinnerItem, currentForm.UsagePeriods);
			spinnerSatisfactionLevel.Adapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleSpinnerItem, currentForm.SatisfactionLevels);
		}

		#endregion

        #region Controls event handlers

		private void OnSaveClick (object sender, EventArgs e)
		{			
			currentForm.SelectedProduct = spinnerProduct.SelectedItem.ToString ();
			currentForm.UsagePeriod = spinnerUsagePeriod.SelectedItem.ToString ();
			currentForm.SatisfactionLevel = spinnerSatisfactionLevel.SelectedItem.ToString ();

			currentForm.UserCompanyName = txtCompanyName.Text;
			currentForm.Feedback = txtFeedback.Text;

			if (currentForm.Save (System.IO.Path.Combine (GetExternalFilesDir (null).AbsolutePath, "myform.pdf"))) {
				Notify("Form was successfully saved.");
			} else {
				Notify("Form save failed");
			}
		}

		private void OnLoadClick (object sender, EventArgs e)
		{	
			// load form and set controls data
			currentForm = ApitronProductQuestionnaireForm.Load (System.IO.Path.Combine (GetExternalFilesDir (null).AbsolutePath, "myform.pdf"));

			if (currentForm != null) {
				spinnerProduct.SetSelection (((ArrayAdapter<string>)spinnerProduct.Adapter).GetPosition (currentForm.SelectedProduct));
				spinnerUsagePeriod.SetSelection (((ArrayAdapter<string>)spinnerUsagePeriod.Adapter).GetPosition (currentForm.UsagePeriod));
				spinnerSatisfactionLevel.SetSelection (((ArrayAdapter<string>)spinnerSatisfactionLevel.Adapter).GetPosition (currentForm.SatisfactionLevel));

				txtCompanyName.Text = currentForm.UserCompanyName;
				txtFeedback.Text = currentForm.Feedback;	

				Notify ("Form was successfully loaded.");
			} else 
			{
				Notify("Form load failed");
				OnClearClick (this, EventArgs.Empty);
			}
		}
			
		private void OnClearClick (object sender, EventArgs e)
		{
			spinnerProduct.SetSelection (0);
			spinnerUsagePeriod.SetSelection (0);
			spinnerSatisfactionLevel.SetSelection (0);

			txtCompanyName.Text = string.Empty;
			txtFeedback.Text = string.Empty;
		}


		void Notify (string text)
		{
			Toast.MakeText (this, text, toastDelay).Show();
		}

		#endregion				
    }
}

