using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.Extraction;
using Apitron.PDF.Kit.FixedLayout;

namespace ExtractTextSample
{
    public partial class MainPage : UserControl
    {
        private FixedDocument document;
        private int pageIndex = -1;
        private object syncRoot = new object();

        public MainPage()
        {
            InitializeComponent();
        }

        private void OpenButton_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "*.pdf|*.pdf";

            if (openFileDialog.ShowDialog() == true)
            {
                Stream stream = openFileDialog.File.OpenRead();
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                // open document and render its first page
                if (this.document != null)
                {
                    this.document.Dispose();
                }
                this.document = new FixedDocument(new MemoryStream(buffer, false));
                StringBuilder sb = new StringBuilder();
                this.pageIndex = 0;
                ExtractPage();
            }
        }

        private void ExtractPage(int shift = 0)
        {
            lock (this.syncRoot)
            {
                if (this.document != null)
                {
                    pageIndex += shift;
                    pageIndex = Math.Max(0, Math.Min(pageIndex, this.document.Pages.Count));
                    if (pageIndex < this.document.Pages.Count)
                    {
                        Page page = this.document.Pages[pageIndex];
                        string extracted = page.ExtractText(TextExtractionOptions.FormattedText, true);
                        this.PageImage.Text = extracted;
                    }
                }
            }
        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            this.ExtractPage(-1);
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            this.ExtractPage(+1);
        }
    }


}
