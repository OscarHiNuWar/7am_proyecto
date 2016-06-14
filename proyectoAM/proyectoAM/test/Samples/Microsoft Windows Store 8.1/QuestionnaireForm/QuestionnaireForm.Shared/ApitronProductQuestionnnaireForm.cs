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
    /// This class represents an Apitron specific implementation of the questionnaire form.
    /// </summary>
    internal sealed class ApitronProductQuestionnaireForm:ProductQuestionnaireForm
    {
        #region fields

        private IList<string> products;
        private IList<string> usagePeriods;
        private IList<string> satisfactionLevels;

        #endregion

        #region properties

        public override IEnumerable<string> Products
        {
            get { return products; }
        }

        public override IEnumerable<string> UsagePeriods
        {
            get { return usagePeriods; }
        }

        public override IEnumerable<string> SatisfactionLevels
        {
            get { return satisfactionLevels; }
        }

        #endregion

        #region ctor

        public ApitronProductQuestionnaireForm()
        {
            products = new List<string>();
            products.Add("Apitron PDF Kit for .NET");
            products.Add("Apitron PDF Rasterizer for .NET");
            products.Add("Apitron PDF Controls for .NET");
            products.Add("Apitron PDF Kit for Xamarin");
            products.Add("Apitron PDF Rasterizer for Xamarin");

            usagePeriods = new List<string>();
            usagePeriods.Add("Less than a month");
            usagePeriods.Add("Three Months");
            usagePeriods.Add("Six months");
            usagePeriods.Add("One year or more");

            satisfactionLevels = new List<string>();
            satisfactionLevels.Add("Low");
            satisfactionLevels.Add("Average");
            satisfactionLevels.Add("High");
        }

        #endregion      
    }
}
