// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CreateQuestionnaireFormSample
{
	[Register ("CreateQuestionnaireFormSampleViewController")]
	partial class CreateQuestionnaireFormSampleViewController
	{
		[Outlet]
		UIKit.UIButton btnClear { get; set; }

		[Outlet]
		UIKit.UIButton btnLoadForm { get; set; }

		[Outlet]
		UIKit.UIButton btnSaveForm { get; set; }

		[Outlet]
		UIKit.UITextField txtCompanyName { get; set; }

		[Outlet]
		UIKit.UITextView txtFeedback { get; set; }

		[Outlet]
		UIKit.UITextField txtProduct { get; set; }

		[Outlet]
		UIKit.UITextField txtSatisfaction { get; set; }

		[Outlet]
		UIKit.UITextField txtUsagePeriod { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnSaveForm != null) {
				btnSaveForm.Dispose ();
				btnSaveForm = null;
			}

			if (btnLoadForm != null) {
				btnLoadForm.Dispose ();
				btnLoadForm = null;
			}

			if (btnClear != null) {
				btnClear.Dispose ();
				btnClear = null;
			}

			if (txtProduct != null) {
				txtProduct.Dispose ();
				txtProduct = null;
			}

			if (txtUsagePeriod != null) {
				txtUsagePeriod.Dispose ();
				txtUsagePeriod = null;
			}

			if (txtSatisfaction != null) {
				txtSatisfaction.Dispose ();
				txtSatisfaction = null;
			}

			if (txtCompanyName != null) {
				txtCompanyName.Dispose ();
				txtCompanyName = null;
			}

			if (txtFeedback != null) {
				txtFeedback.Dispose ();
				txtFeedback = null;
			}
		}
	}
}
