using System;
using System.Drawing;
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
	/// <summary>
	///  Represents a preview item implementation for <see cref="MyPreviewDataSource"/>
	/// </summary>
	public sealed class FilePreviewItem :QLPreviewItem
	{
		#region fields

		string title;
		NSUrl targetUrl;

		#endregion

		#region properties

		public override string ItemTitle {
			get {
				return title;
			}
		}

		public override NSUrl ItemUrl {
			get {
				return targetUrl;
			}
		}

		#endregion


		#region ctor

		public FilePreviewItem (string title, NSUrl targetUrl)
		{
			this.targetUrl = targetUrl;
			this.title = title;
		}

		#endregion
	}
}


