using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.PageProperties;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.FixedLayout.Resources.Fonts;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.FlowLayout.Content.Controls;
using Apitron.PDF.Kit.Interactive.Forms;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.Styles.Appearance;
using Font = Apitron.PDF.Kit.Styles.Text.Font;

namespace QuestionnaireForm
{
    /// <summary>
    /// This class represents a base form which holds questionnaire data.
    /// </summary>
    internal abstract partial class ProductQuestionnaireForm : INotifyPropertyChanged
    {
        #region fields

        private string usagePeriod;
        private string satisfactionLevel;
        private string userCompanyName;
        private string feedback;
        private string selectedProduct;

        #endregion

        #region properties

        public string UsagePeriod
        {
            get { return usagePeriod; }
            set
            {
                usagePeriod = value;

                RaisePropertyChanged("UsagePeriod");
            }
        }

        public string SatisfactionLevel
        {
            get { return satisfactionLevel; }
            set
            {
                satisfactionLevel = value;

                RaisePropertyChanged("SatisfactionLevel");
            }
        }

        public string UserCompanyName
        {
            get { return userCompanyName; }
            set
            {
                userCompanyName = value;
                RaisePropertyChanged("UserCompanyName");
            }
        }

        public string SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                RaisePropertyChanged("SelectedProduct");
            }
        }

        public string Feedback
        {
            get { return feedback; }
            set
            {
                feedback = value;
                RaisePropertyChanged("Feedback");
            }
        }

        public abstract IEnumerable<string> Products { get; }

        public abstract IEnumerable<string> UsagePeriods { get; }

        public abstract IEnumerable<string> SatisfactionLevels { get; }

        #endregion

        #region ctor

        public ProductQuestionnaireForm()
        {
            selectedProduct = string.Empty;
            usagePeriod = string.Empty;
            satisfactionLevel = string.Empty;
            userCompanyName = string.Empty;
            feedback = string.Empty;
        }
      

        #endregion

        #region INotifyPropertyChanged implementation

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion        

        #region members

        public void Reset()
        {
            SatisfactionLevel = string.Empty;
            UserCompanyName = string.Empty;
            SelectedProduct = string.Empty;
            UsagePeriod = string.Empty;
            Feedback = string.Empty;
        }        

        #endregion
    }
}
