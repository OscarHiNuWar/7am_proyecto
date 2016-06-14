using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace QuestionnaireForm
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            CurrentForm = new ApitronProductQuestionnaireForm() { UserCompanyName = "Company", Feedback = "Feedback", SatisfactionLevel = "High", SelectedProduct = "Apitron PDF Kit for .NET", UsagePeriod = "One year or more" };
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }        

        private async void saveFormDataClick(object sender, RoutedEventArgs e)
        {
            var form = CurrentForm;

            if (form != null)
            {
                MessageDialog dialog = null;

                dialog = await form.SaveToLocalStorage("form.pdf")
                    ? new MessageDialog("Form was successfully saved!")
                    : new MessageDialog("There was an error while saving the form.");

                await dialog.ShowAsync();
            }       
        }

        private ProductQuestionnaireForm CurrentForm
        {
            get
            {
                return this.DataContext as ProductQuestionnaireForm;                
            }
            set
            {
                this.DataContext = value;
            }
        }

        private void btnClearForm_Click(object sender, RoutedEventArgs e)
        {
            var form = CurrentForm;

            if (form != null)
            {
                form.Reset();
            }
        }

        private async void btnLoadFormData_Click(object sender, RoutedEventArgs e)
        {
            CurrentForm =  await ProductQuestionnaireForm.LoadFromLocalStorage("form.pdf");

            if (CurrentForm == null)
            {
                await new MessageDialog("There was an error while loading the form. Empty form was loaded.").ShowAsync();

                CurrentForm = new ApitronProductQuestionnaireForm();
            }
        }   
    }
}
