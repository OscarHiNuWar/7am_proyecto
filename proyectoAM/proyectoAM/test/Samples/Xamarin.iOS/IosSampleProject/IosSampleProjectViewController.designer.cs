// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace IosSampleProject
{
	[Register ("IosSampleProjectViewController")]
	partial class IosSampleProjectViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnGenerateFile { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnGenerateFile != null) {
				btnGenerateFile.Dispose ();
				btnGenerateFile = null;
			}
		}
	}
}
