using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.FlowLayout.Navigation;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.Styles.Text;
using Apitron.PDF.Kit.FixedLayout.Resources.Fonts;
using Apitron.PDF.Kit.Styles.Appearance;
using Font = Apitron.PDF.Kit.Styles.Text.Font;
using Display = Apitron.PDF.Kit.Styles.Appearance.Display;
using Apitron.PDF.Kit.FixedLayout.PageProperties;
using Apitron.PDF.Kit.FixedLayout;
using Android.Content.Res;

namespace ProductCatalogWithTableOfContents
{
	[Activity (Label = "ProductCatalogWithTableOfContents", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += new EventHandler(OnButtonClick);
		}

		/// <summary>
		/// Handles the button click event. This handler creates PDF file with products catalog for imaginary store.
		/// The output file will have table of contents generated with links pointing to product subcatalogs and back.
		/// Bookmarks will be generated as well. Output will be written to an external folder external_media/Apitron
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		private void OnButtonClick(Object sender, EventArgs args )
		{
			// extract resources
			string dataFolder = BaseContext.CacheDir.AbsolutePath;

			ExtractAssets ("data", dataFolder);

			// register images for car and back arrow
			ResourceManager resourceManager = new ResourceManager();

			resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("cart", Path.Combine(dataFolder, "cart.png")));
			resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("backarrow", Path.Combine(dataFolder, "back.png")));

			// create doc
			FlowDocument document = new FlowDocument() { Margin = new Thickness(5) };
			// create root bookmark
			document.Bookmark = new BookmarkEntry("Products");
			// register style for product's measure
			document.StyleManager.RegisterStyle("textblock.measure", new Style() { Font = new Font(StandardFonts.Helvetica, 10), Color = RgbColors.Gray, Width = Length.FromPercentage(50), Align = Align.Left, Display = Display.InlineBlock });
			// register style for product's price
			document.StyleManager.RegisterStyle("textblock.price", new Style() { Font = new Font(StandardFonts.HelveticaBold, 12), Width = Length.FromPercentage(50), Align = Align.Right, Display = Display.InlineBlock });
			// register style for product's label
			document.StyleManager.RegisterStyle("textblock.label", new Style() { Font = new Font(StandardFonts.HelveticaBold, 12), Width = Length.FromPercentage(100), Align = Align.Left, Display = Display.Block, LineHeight = Length.FromPercentage(200) });
			// register style for category header
			document.StyleManager.RegisterStyle("textblock.categoryHeader", new Style() { Font = new Font(StandardFonts.HelveticaBold, 18), LineHeight = Length.FromPercentage(180) });
			// register style for back link arrow
			document.StyleManager.RegisterStyle("image.backlink", new Style() { Margin = new Thickness(5, 0, 5, 0), VerticalAlign = VerticalAlign.Middle });
			// regsiter style for text blocks under the table of contents section
			document.StyleManager.RegisterStyle("section#toc > textblock", new Style() { Display = Display.Block, ListStyle = ListStyle.ListItem, Font = new Font(StandardFonts.HelveticaBold, 18), LineHeight = Length.FromPercentage(150) });
			// register style for TOC
			document.StyleManager.RegisterStyle("section#toc", new Style() { Margin = new Thickness(20, 20, 20, 20), Font = new Font(StandardFonts.HelveticaBold, 25), ListStyle = ListStyle.Ordered, ListMarker = ListMarker.Decimal });
			// style for product info block
			document.StyleManager.RegisterStyle("section.productDescription", new Style() { Width = Length.FromPercentage(25), Height = Length.FromPixels(250), Border = new Border(1), BorderColor = RgbColors.LightGray, Margin = new Thickness(2, 2, 2, 2), Padding = new Thickness(3, 3, 3, 3), Align = Align.Center, Display = Display.InlineBlock });
			// style for product image
			document.StyleManager.RegisterStyle("image.productImage", new Style() { Display = Display.Block, Width = Length.FromPixels(180), Height = Length.FromPixels(180) });

			// create first page content - the cover
			Section firstPage = new Section() { Align = Align.Center, Font = new Font(StandardFonts.HelveticaBold, 40), Margin = new Thickness(0, 350, 0, 0) };
			firstPage.Add(new TextBlock("Products catalog"));
			firstPage.Add(new Br());
			firstPage.Add(new Image("cart"));
			document.Add(firstPage);
			document.Add(new PageBreak());

			// create table of contents
			Section toc = new Section() { Id = "toc" };
			toc.Add(new TextBlock("Select category") { ListStyle = ListStyle.None });
			toc.Add(new Br() { Height = 10 });
			document.Add(toc);
			document.Add(new PageBreak());

			// create categories and fill with product data
			foreach (string directory in Directory.EnumerateDirectories(Path.Combine(dataFolder, "products")))
			{
				// get product category name
				string[] dirs = directory.Split(Path.DirectorySeparatorChar);
				string dirName = dirs[dirs.Length - 1];

				// add category name into the TOC and set its link pointing to the object with id=[category name]
				toc.Add(new TextBlock(dirName) { Link = new CrossReference(dirName) });

				// create product category section
				Section category = new Section();

				// create link image
				category.Add(new Image("backarrow") { Class = "backlink", Link = new CrossReference("toc") });
				// create category name
				category.Add(new TextBlock(dirName) { Id = dirName, Class = "categoryHeader", Bookmark = new BookmarkEntry(dirName) });
				category.Add(new Br());

				// add products to category
				foreach (string imgFileName in Directory.EnumerateFiles(directory))
				{
					string productInfo = Path.GetFileNameWithoutExtension(imgFileName);

					// register product image
					resourceManager.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image(productInfo, imgFileName));

					string[] tags = productInfo.Split(new char[] { '_' });

					Section productDescription = new Section() {Class = "productDescription"};

					productDescription.Add(new Image(productInfo) { Class = "productImage"});
					productDescription.Add(new TextBlock(tags[1]) { Class = "measure" });
					productDescription.Add(new TextBlock(string.Format("$ {0}", tags[2])) { Class = "price" });
					productDescription.Add(new TextBlock(tags[0]) { Class = "label" });

					category.Add(productDescription);
				}

				// finalize category creation
				document.Add(category);
			}

			// create outdir if it doesn't exist
			string outDirPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath,"Apitron");

			if (!Directory.Exists (outDirPath)) 
			{
				Directory.CreateDirectory (outDirPath);
			}

			string fileName = Path.Combine(outDirPath,"productCatalogWithToc.pdf");
		
			// write the file to stream
			using (Stream outputStream = File.Create(fileName))
			{
				document.Write(outputStream,resourceManager, new PageBoundary(Boundaries.A4));
			}
				
			// try to open the file using installed viewers
			Intent intent = new Intent (Intent.ActionView);
			intent.SetDataAndType (Android.Net.Uri.FromFile (new Java.IO.File (fileName)),"application/pdf");
			intent.SetFlags (ActivityFlags.NoHistory);

			StartActivity(intent);
		}	
			
		#region Helpers

		///<summary>
		/// Extracts assets subdirectory from app's package to the cache folder, so they could be referenced later by their file paths.
		/// This temporary solution allows creation of resources requiring file paths to be passed to ctor.
		///</summary>
		void ExtractAssets(string basePath, string baseOutPath)
		{
			string []  assets = Assets.List (basePath);

			foreach (string asset in assets)
			{
				string assetPath = string.Format("{0}/{1}", basePath, asset);

				string [] innerAssets = Assets.List(assetPath);

				if(innerAssets.Length==0)
				{
					using (Stream streamIn = Assets.Open(assetPath), streamOut = File.Create(Path.Combine(baseOutPath,asset)))
					{
						streamIn.CopyTo (streamOut);
					}
				}
				else
				{
					string newBasePath = Path.Combine (baseOutPath, asset);

					Directory.CreateDirectory(newBasePath);

					ExtractAssets (assetPath, newBasePath);
				}
			}
		}

		#endregion
	}
}
