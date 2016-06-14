using System;
using UIKit;

namespace CreateQuestionnaireFormSample
{
	/// <summary>
	/// Process button click event given the index of the button in <see cref="UIAlertView"/>.
	/// </summary>
	delegate void ProcessButtonClickAtIndex(nint index);

	/// <summary>
	/// Custom alert view delegate.
	/// </summary>
	class CustomAlertViewDelegate:UIAlertViewDelegate
	{
		ProcessButtonClickAtIndex buttonHanlder;

		public CustomAlertViewDelegate (ProcessButtonClickAtIndex buttonHandler)
		{
			if (buttonHandler == null)
			{
				throw new ArgumentNullException ();
			}

			this.buttonHanlder = buttonHandler;
		}

		public override void Clicked (UIAlertView alertview, nint buttonIndex)
		{
			buttonHanlder (buttonIndex);
		}
	}
}

