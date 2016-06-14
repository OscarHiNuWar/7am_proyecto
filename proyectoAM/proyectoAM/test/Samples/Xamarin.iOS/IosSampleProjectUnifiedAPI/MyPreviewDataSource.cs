using System;
using System.IO;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.FixedLayout.Resources;
using QuickLook;
using Foundation;
using UIKit;

namespace IosSampleProjectUnifiedAPI
{
	class MyPreviewDataSource:QLPreviewControllerDataSource
	{
		FilePreviewItem targetItem;

		public MyPreviewDataSource (string filePath)
		{
			if (string.IsNullOrEmpty (filePath))
			{
				throw new ArgumentException ("Target path can't be null or empty");
			}

			targetItem = new FilePreviewItem(Path.GetFileName(filePath),NSUrl.FromFilename (filePath));
		}

		public override nint PreviewItemCount (QLPreviewController controller)
		{
			return 1;
		}

		public override IQLPreviewItem GetPreviewItem (QLPreviewController controller, nint index)
		{
			return targetItem;
		}
	}
}


