using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.PageProperties;
using Apitron.PDF.Kit.FixedLayout.Resources.Fonts;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.Styles.Appearance;
using Font = Apitron.PDF.Kit.Styles.Text.Font;
using Thickness = Apitron.PDF.Kit.Styles.Thickness;
using Style = Apitron.PDF.Kit.Styles.Style;
using TextBlock = Apitron.PDF.Kit.FlowLayout.Content.TextBlock;
using Grid = Apitron.PDF.Kit.FlowLayout.Content.Grid;
using Page = Windows.UI.Xaml.Controls.Page;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowStoreSampleUniversalApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            // start progress
            ShowProgress(true);

            // execute the generation
            StorageFile generatedFile = await PDFGenerator.GeneratePdf();

            ShowProgress(false);

            Launcher.LaunchFileAsync(generatedFile);      
        }

        /// <summary>
        /// Control progress bar state.
        /// </summary>
        /// <param name="start">True if progress should be shown, otherwise false.</param>
        private void ShowProgress(bool start)
        {
            progressBar.Visibility = start ? Visibility.Visible : Visibility.Collapsed;
            progressBar.IsIndeterminate = start;
        }
    }
}
