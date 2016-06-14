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

namespace IosSampleProject
{
	class MyPreviewDataSource:QLPreviewControllerDataSource
	{
		FilePreviewItem targetItem;

		public MyPreviewDataSource (string filePath)
		{
			if (string.IsNullOrEmpty (filePath))
				throw new ArgumentException ("Target path can't be null or empty");

			targetItem = new FilePreviewItem(Path.GetFileName(filePath),NSUrl.FromFilename (filePath));
		}

		public override int PreviewItemCount (QLPreviewController controller)
		{
			return 1;
		}

		public override QLPreviewItem GetPreviewItem (QLPreviewController controller, int index)
		{
			return targetItem;
		}
	}

}


