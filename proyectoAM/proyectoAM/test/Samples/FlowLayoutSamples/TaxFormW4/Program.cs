using System.Diagnostics;
using System.IO;
using Apitron.PDF.Kit;
using Apitron.PDF.Kit.FixedLayout;
using Apitron.PDF.Kit.FixedLayout.PageProperties;
using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.FixedLayout.Resources.Fonts;
using Apitron.PDF.Kit.FlowLayout;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.FlowLayout.Content.Controls;
using Apitron.PDF.Kit.Interactive.Annotations;
using Apitron.PDF.Kit.Interactive.Forms;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.Styles.Appearance;
using Apitron.PDF.Kit.Styles.Text;
using Font = Apitron.PDF.Kit.Styles.Text.Font;

namespace TaxFormW4
{
    internal class Program
    {
        /// <summary>
        /// This sample shows how to create W4 tax form with fields and styles.
        /// </summary>        
        private static void Main(string[] args)
        {
            string[] introductionTextLeftColumn =
                {
                    "Purpose.",
                    "Complete Form W-4 so that your employer can withhold the correct federal income tax from your pay. Consider completing a new Form W-4 each year and when your personal or financial situation changes.", 
                    "Exemption from withholding.",
                    "If you are exempt, complete only lines 1, 2, 3, 4, and 7 and sign the form to validate it. Your exemption for 2015 expires February 17, 2015. See Pub. 505, Tax Withholding and Estimated Tax.", 
                    "Note.",
                    "If another person can claim you as a dependent on his or her tax return, you cannot claim exemption from withholding if your income exceeds $1,000 and includes more than $350 of unearned income (for example, interest and dividends).", 
                    "Exceptions.",
                    "An employee may be able to claim exemption from withholding even if the employee is a dependent, if the employee:",
                    "Is age 65 or older,", "Is blind, or",
                    "Will claim adjustments to income; tax credits; or itemized deductions, on his or her tax return."
                };

            string[] introductionTextCentralColumn =
                {
                    "The exceptions do not apply to supplemental wages greater than $1,000,000.", "Basic instructions.",
                    "If you are not exempt, complete the Personal Allowances Worksheet below. The worksheets on page 2 further adjust your withholding allowances based on itemized deductions, certain credits, adjustments to income, or two-earners/multiple jobs situations.",
                    "Complete all worksheets that apply. However, you may claim fewer (or zero) allowances. For regular wages, withholding must be based on allowances you claimed and may not be a flat amount or percentage of wages.", 
                    "Head of household.",
                    "Generally, you can claim head of household filing status on your tax return only if you are unmarried and pay more than 50% of the costs of keeping up a home for yourself and your dependent(s) or other qualifying individuals. See Pub. 501, Exemptions, Standard Deduction, and Filing Information, for information.", 
                    "Tax credits.",
                    "You can take projected tax credits into account in figuring your allowable number of withholding allowances. Credits for child or dependent care expenses and the child tax credit may be claimed using the Personal Allowances Worksheet below. See Pub. 505 for information on converting your other credits into withholding allowances."
                };
            string[] introductionTextRightColumn =
                {
                    "Nonwage income.",
                    "If you have a large amount of nonwage income, such as interest or dividends, consider making estimated tax payments using Form 1040-ES, Estimated Tax for Individuals. Otherwise, you may owe additional tax. If you have pension or annuity iincome, see Pub. 505 to find out if you should adjust your withholding on Form W-4 or W-4P.", 
                    "Two earners or multiple jobs.",
                    "If you have a working spouse or more than one job, figure the total number of allowances you are entitled to claim on all jobs using worksheets from only one Form W-4. Your withholding usually will be most accurate when all allowances are claimed on the Form W-4 for the highest paying job and zero allowances are claimed on the others. See Pub. 505 for details.", 
                    "Nonresident alien.",
                    "If you are a nonresident alien, see Notice 1392, Supplemental Form W-4 Instructions for Nonresident Aliens, before completing this form.", 
                    "Check your withholding.",
                    "After your Form W-4 takes effect, use Pub. 505 to see how the amount you are having withheld compares to your projected total tax for 2015. See Pub. 505, especially if your earnings exceed $130,000 (Single) or $180,000 (Married).", 
                    "Future developments.",
                    "Information about any future developments affecting Form W-4 (such as legislation enacted after we release it) will be posted at", 
                    "www.irs.gov/w4."
                };
            
            using (FileStream fs = new FileStream(@"..\..\..\OutputDocuments\W4Form.pdf", FileMode.Create))
            {
                FlowDocument document = new FlowDocument();

                // document styles
                document.BackgroundRepeat = BackgroundRepeat.NoRepeat;
                document.BackgroundPosition = BackgroundPosition.LeftTop;

                document.StyleManager.RegisterStyle(".header", new Style { Font = new Font("Franklin Gothic Demi", 19), BorderBottom = new Border(2), Display = Display.Block, Width = Length.FromPercentage(100), LineHeight = 19, VerticalAlign = VerticalAlign.Top });
                document.StyleManager.RegisterStyle(".page", new Style { Margin = new Thickness(Length.FromInches(0.48), Length.FromInches(0.48), Length.FromInches(0.48), 0), Width = Length.FromPercentage(100), Height = Length.FromPercentage(100) });
                document.StyleManager.RegisterStyle(".introSection", new Style { Width = Length.FromPercentage(31.5), Display = Display.InlineBlock, Padding = new Thickness(0, 2, 0, 0) });

                document.StyleManager.RegisterStyle(".intro", new Style { LineHeight = Length.FromPoints(7) });
                document.StyleManager.RegisterStyle(".introSection > br", new Style { Height = 3 });
                document.StyleManager.RegisterStyle(".introListItem", new Style { ListStyle = ListStyle.ListItem, ListMarker = ListMarker.Circle, ListMarkerPadding = new Thickness(2), Font = new Font(StandardFonts.Helvetica, 7), LineHeight = 7, Padding = new Thickness(0, 0, 0, 5) });

                document.StyleManager.RegisterStyle(".allowancesRowStart", new Style { Width = Length.FromPercentage(10), Display = Display.InlineBlock });
                document.StyleManager.RegisterStyle(".allowanceField", new Style { BorderColor = RgbColors.Black, BorderBottom = new Border(1) });
                document.StyleManager.RegisterStyle(".inlineList", new Style { ListStyle = ListStyle.Unordered, ListMarker = ListMarker.Circle, Display = Display.InlineBlock, ListMarkerPadding = new Thickness(0, 2, 2, 2) });
                document.StyleManager.RegisterStyle(".inlineList > *", new Style { ListStyle = ListStyle.ListItem });
                document.StyleManager.RegisterStyle(".dashedBottomBorder", new Style { BorderColor = RgbColors.Black, BorderBottom = new Border(0.5, new float[] { 2, 2, 2, 2, 2, 2, 4, 2 }, 0) });

                // font styles
                document.StyleManager.RegisterStyle(".textHBO7", new Style { Font = new Font(StandardFonts.HelveticaBoldOblique, 7) });
                document.StyleManager.RegisterStyle(".textHB7", new Style { Font = new Font(StandardFonts.HelveticaBold, 7) });
                document.StyleManager.RegisterStyle(".textHB8", new Style { Font = new Font(StandardFonts.HelveticaBold, 8) });
                document.StyleManager.RegisterStyle(".textHB9", new Style { Font = new Font(StandardFonts.HelveticaBold, 9) });
                document.StyleManager.RegisterStyle(".textHB10", new Style { Font = new Font(StandardFonts.HelveticaBold, 10) });
                document.StyleManager.RegisterStyle(".textHB14", new Style { Font = new Font(StandardFonts.HelveticaBold, 14) });
                document.StyleManager.RegisterStyle(".textHB21", new Style { Font = new Font(StandardFonts.HelveticaBold, 21) });
                document.StyleManager.RegisterStyle(".textHB24", new Style { Font = new Font(StandardFonts.HelveticaBold, 24) });
                document.StyleManager.RegisterStyle(".textH6", new Style { Font = new Font(StandardFonts.Helvetica, 6) });
                document.StyleManager.RegisterStyle(".textH7", new Style { Font = new Font(StandardFonts.Helvetica, 7) });
                document.StyleManager.RegisterStyle(".textH8", new Style { Font = new Font(StandardFonts.Helvetica, 8) });
                document.StyleManager.RegisterStyle(".textH10", new Style { Font = new Font(StandardFonts.Helvetica, 10) });
                document.StyleManager.RegisterStyle(".textTR6", new Style { Font = new Font(StandardFonts.TimesRoman, 6) });
                document.StyleManager.RegisterStyle(".textTR8", new Style { Font = new Font("TimesNewRoman", 8) });
                document.StyleManager.RegisterStyle(".textTR28", new Style { Font = new Font(StandardFonts.TimesRoman, 28) });
                document.StyleManager.RegisterStyle(".textTR53", new Style { Font = new Font(StandardFonts.TimesRoman, 53) });
                document.StyleManager.RegisterStyle(".textFGD11", new Style { Font = new Font("Franklin Gothic Demi", 11) });


                document.StyleManager.RegisterStyle(".certHeaderPart", new Style { Display = Display.InlineBlock, Height = 40, BorderBottom = Border.Solid, BorderRight = Border.Solid, BorderColor = RgbColors.Black });
                document.StyleManager.RegisterStyle(".certEmployeeDataRow", new Style { Display = Display.InlineBlock, Height = 24, BorderBottom = new Border(0.5), BorderRight = new Border(0.5), BorderColor = RgbColors.Black });
                document.StyleManager.RegisterStyle(".certEmployerDataRow", new Style { Display = Display.InlineBlock, Height = 24, BorderBottom = new Border(1), BorderRight = new Border(0.5), BorderColor = RgbColors.Black });
                document.StyleManager.RegisterStyle(".certEmployerDataRowSmall", new Style { Display = Display.InlineBlock, Height = 20, BorderBottom = new Border(0.5), BorderRight = new Border(0.5), BorderColor = RgbColors.Black });
                document.StyleManager.RegisterStyle(".certCheckbox", new Style { Display = Display.InlineBlock, Width = 9, Height = 9, VerticalAlign = VerticalAlign.Middle });

                document.StyleManager.RegisterStyle(".thinBorderLeftBottom", new Style { BorderColor = RgbColors.Black, BorderBottom = new Border(0.5), BorderLeft = new Border(0.5) });
                document.StyleManager.RegisterStyle(".thinBorderBottom", new Style { BorderColor = RgbColors.Black, BorderBottom = new Border(0.5) });
                document.StyleManager.RegisterStyle(".thinBorderTop", new Style { BorderColor = RgbColors.Black, BorderTop = new Border(0.5) });
                document.StyleManager.RegisterStyle(".thinBorderLeft", new Style { BorderColor = RgbColors.Black, BorderLeft = new Border(0.5) });

                Section page1 = new Section { Class = "page" };

                // 3 intro sections
                page1.Add(new Hr());

                // intro top left
                Section introPart1 = new Section { Class = "introSection", ListStyle = ListStyle.Unordered };
                introPart1.Add(new TextBlock("Form W-4 (2015)") { Class = "header", BorderBottom = new Border(2), BorderColor = RgbColors.Black });
                introPart1.Add(new Br());
                introPart1.Add(new TextBlock(introductionTextLeftColumn[0]) { Class = "intro textHB7" });
                introPart1.Add(new TextBlock(introductionTextLeftColumn[1]) { Class = "intro textH7" });
                introPart1.Add(new Br());
                introPart1.Add(new TextBlock(introductionTextLeftColumn[2]) { Class = "intro textHB7" });
                introPart1.Add(new TextBlock(introductionTextLeftColumn[3]) { Class = "intro textH7" });
                introPart1.Add(new Br());
                introPart1.Add(new TextBlock(introductionTextLeftColumn[4]) { Class = "intro textHB7" });
                introPart1.Add(new TextBlock(introductionTextLeftColumn[5]) { Class = "intro textH7" });
                introPart1.Add(new Br());
                introPart1.Add(new TextBlock(introductionTextLeftColumn[6]) { Class = "intro textHBO7", TextIndent = 7 });
                introPart1.Add(new TextBlock(introductionTextLeftColumn[7]) { Class = "intro textH7" });
                introPart1.Add(new Br());
                introPart1.Add(new TextBlock(introductionTextLeftColumn[8]) { Class = "introListItem" });
                introPart1.Add(new TextBlock(introductionTextLeftColumn[9]) { Class = "introListItem" });
                introPart1.Add(new TextBlock(introductionTextLeftColumn[10]) { Class = "introListItem" });
                page1.Add(introPart1);
                page1.Add(new Section { Display = Display.InlineBlock, Width = Length.FromPercentage(3) });


                // intro top center
                Section introPart2 = new Section { Class = "introSection", Width = Length.FromPercentage(31) };
                introPart2.Add(new TextBlock(introductionTextCentralColumn[0]) { Class = "intro textH7" });
                introPart2.Add(new Br());
                introPart2.Add(new TextBlock(introductionTextCentralColumn[1]) { Class = "intro textHB7" });
                introPart2.Add(new TextBlock(introductionTextCentralColumn[2]) { Class = "intro textH7" });
                introPart2.Add(new Br());
                introPart2.Add(new TextBlock(introductionTextCentralColumn[3]) { Class = "intro textH7" });
                introPart2.Add(new Br());
                introPart2.Add(new TextBlock(introductionTextCentralColumn[4]) { Class = "intro textHB7" });
                introPart2.Add(new TextBlock(introductionTextCentralColumn[5]) { Class = "intro textH7" });
                introPart2.Add(new Br());
                introPart2.Add(new TextBlock(introductionTextCentralColumn[6]) { Class = "intro textHB7" });
                introPart2.Add(new TextBlock(introductionTextCentralColumn[7]) { Class = "intro textH7" });
                page1.Add(introPart2);
                page1.Add(new Section { Display = Display.InlineBlock, Width = Length.FromPercentage(3.5) });


                // intro top right
                Section introPart3 = new Section { Class = "introSection", Width = Length.FromPercentage(31) };
                introPart3.Add(new TextBlock(introductionTextRightColumn[0]) { Class = "intro textHB7" });
                introPart3.Add(new TextBlock(introductionTextRightColumn[1]) { Class = "intro textH7" });
                introPart3.Add(new Br());
                introPart3.Add(new TextBlock(introductionTextRightColumn[2]) { Class = "intro textHB7" });
                introPart3.Add(new TextBlock(introductionTextRightColumn[3]) { Class = "intro textH7" });
                introPart3.Add(new Br());
                introPart3.Add(new TextBlock(introductionTextRightColumn[4]) { Class = "intro textHB7" });
                introPart3.Add(new TextBlock(introductionTextRightColumn[5]) { Class = "intro textH7" });
                introPart3.Add(new Br());
                introPart3.Add(new TextBlock(introductionTextRightColumn[6]) { Class = "intro textHB7" });
                introPart3.Add(new TextBlock(introductionTextRightColumn[7]) { Class = "intro textH7" });
                introPart3.Add(new Br());
                introPart3.Add(new TextBlock(introductionTextRightColumn[8]) { Class = "intro textHB7" });
                introPart3.Add(new TextBlock(introductionTextRightColumn[9]) { Class = "intro textH7" });
                introPart3.Add(new Br { Height = 0 });
                introPart3.Add(new TextBlock(introductionTextRightColumn[10]) { Class = "intro textH7" });
                page1.Add(introPart3);

                // add subheader - Personal Allowances worksheet
                page1.Add(new Hr());

                Section subHeader = new Section { Align = Align.Center };
                subHeader.Add(new TextBlock("Personal Allowances Worksheet") { Class = "textHB10" });
                subHeader.Add(new TextBlock("(Keep for your records.)") { Class = "textH10" });

                page1.Add(subHeader);

                page1.Add(new Hr { Height = 0.5 });

                // set of fields
                document.Fields.Add(new TextField("rowA", ""));
                document.Fields.Add(new TextField("rowB", ""));
                document.Fields.Add(new TextField("rowC", ""));
                document.Fields.Add(new TextField("rowD", ""));
                document.Fields.Add(new TextField("rowE", ""));
                document.Fields.Add(new TextField("rowF", ""));
                document.Fields.Add(new TextField("rowG", ""));
                document.Fields.Add(new TextField("rowH", ""));
                document.Fields.Add(new TextField("employeeName", ""));
                document.Fields.Add(new TextField("employeeLastname", ""));
                document.Fields.Add(new TextField("employeeSSN", ""));
                document.Fields.Add(new TextField("employeeAddress", ""));
                document.Fields.Add(new RadioButtonField("maritalStatus"));
                document.Fields.Add(new TextField("employeeAddress2", ""));
                document.Fields.Add(new CheckBoxField("ssnCardStatus", ""));
                document.Fields.Add(new TextField("certRow5", ""));
                document.Fields.Add(new TextField("certRow6", ""));
                document.Fields.Add(new TextField("certRow7", ""));
                document.Fields.Add(new TextField("employerName", ""));
                document.Fields.Add(new TextField("officeCode", ""));
                document.Fields.Add(new TextField("employerEIN", ""));
                document.Fields.Add(new TextField("worksheetRow1", ""));
                document.Fields.Add(new TextField("worksheetRow2", ""));
                document.Fields.Add(new TextField("worksheetRow3", ""));
                document.Fields.Add(new TextField("worksheetRow4", ""));
                document.Fields.Add(new TextField("worksheetRow5", ""));
                document.Fields.Add(new TextField("worksheetRow6", ""));
                document.Fields.Add(new TextField("worksheetRow7", ""));
                document.Fields.Add(new TextField("worksheetRow8", ""));
                document.Fields.Add(new TextField("worksheetRow9", ""));
                document.Fields.Add(new TextField("worksheetRow10", ""));
                document.Fields.Add(new TextField("worksheetRow11", ""));
                document.Fields.Add(new TextField("worksheetRow12", ""));
                document.Fields.Add(new TextField("worksheetRow13", ""));
                document.Fields.Add(new TextField("worksheetRow14", ""));
                document.Fields.Add(new TextField("worksheetRow15", ""));
                document.Fields.Add(new TextField("worksheetRow16", ""));
                document.Fields.Add(new TextField("worksheetRow17", ""));
                document.Fields.Add(new TextField("worksheetRow18", ""));
                document.Fields.Add(new TextField("worksheetRow19", ""));

                string[] allowanceA = { "Enter “1” for yourself if no one else can claim you as a dependent . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . ." };
                string[] allowanceB = { "Enter “1” if", "{", "You are single and have only one job; or", "You are married, have only one job, and your spouse does not work; or", "Your wages from a second job or your spouse’s wages (or the total of both) are $1,500 or less.", "}", ". . . . . . . . . . . . . . . . . . . . . . . . . . ." };
                string[] allowanceC = { "Enter “1” for your spouse. But, you may choose to enter “-0-” if you are married and have either a working spouse or more than one job. (Entering “-0-” may help you avoid having too little tax withheld.) . . . . . . . . . . . . . . . . . . . . . . . . . . . ." };
                string[] allowanceD = { "Enter number of dependents (other than your spouse or yourself) you will claim on your tax return . . . . . . . . . . . . . . . . . . . . . . . . . . . ." };
                string[] allowanceE = { "Enter “1” if you will file as head of household on your tax return (see conditions under Head of household above) . . . . . . . . . . . . . . . ." };
                string[] allowanceF = { "Enter “1” if you have at least $2,000 of child or dependent care expenses for which you plan to claim a credit  . . . . . . . . . . . . . . . . . . . .(Note. Do not include child support payments. See Pub. 503, Child and Dependent Care Expenses, for details.)" };
                string[] allowanceG = { "Child Tax Credit (including additional child tax credit). See Pub. 972, Child Tax Credit, for more information.", "If your total income will be less than $65,000 ($95,000 if married), enter “2” for each eligible child; then less “1” if you have three to six eligible children or less “2” if you have seven or more eligible children.", "If your total income will be between $65,000 and $84,000 ($95,000 and $119,000 if married), enter “1” for each eligible child . . . . . . ." };
                string[] allowanceH = { "Add lines A through G and enter total here.(Note.This may be different from the number of exemptions you claim on your tax return.)", "►" };
                string[] allowanceI = { "For accuracy, complete all worksheets that apply.", "{", "If you plan to itemize or claim adjustments to income and want to reduce your withholding, see the Deductions and Adjustments Worksheet on page 2.", "If you are single and have more than one job or are married and you and your spouse both work and the combined earnings from all jobs exceed $50,000 ($20,000 if married), see the Two-Earners/Multiple Jobs Worksheet on page 2 to avoid having too little tax withheld.", "If neither of the above situations applies, stop here and enter the number from line H on line 5 of Form W-4 below." };

                Grid allowances = new Grid(Length.FromPercentage(4), Length.FromPercentage(88), Length.FromPercentage(2.5), Length.FromPercentage(5.5)) { InnerBorderColor = RgbColors.Transparent, Id = "grid" };

                // Row A
                allowances.Add(
                    new GridRow(Length.FromPoints(10), new TextBlock("A") { Class = "textHB9" },
                        new TextBlock(allowanceA[0]) { Class = "textH8" },
                        new TextBlock("A") { Class = "textHB9" },
                        new TextBox("rowA") { Class = "textH8 allowanceField" }));

                // Row B
                Section rowB = new Section { Class = "textH8", Display = Display.InlineBlock, Height = 36, VerticalAlign = VerticalAlign.Middle };
                rowB.Add(new TextBlock(allowanceB[0]));
                rowB.Add(new TextBlock(allowanceB[1]) { Class = "textTR28", VerticalAlign = VerticalAlign.Bottom, Padding = new Thickness(0, 0, 0, 3) });

                Section rowBlist = new Section { Class = "inlineList", Display = Display.InlineBlock, LineHeight = 12, Width = Length.FromPercentage(74) };
                rowBlist.Add(new TextBlock(allowanceB[2]));
                rowBlist.Add(new TextBlock(allowanceB[3]));
                rowBlist.Add(new TextBlock(allowanceB[4]));

                rowB.Add(rowBlist);
                rowB.Add(new TextBlock(allowanceB[5]) { Class = "textTR28", VerticalAlign = VerticalAlign.Bottom, Padding = new Thickness(0, 0, 0, 4) });
                rowB.Add(new TextBlock(allowanceB[6]));

                Section rowBField = new Section();
                rowBField.Add(new TextBox("rowB") { Class = "textH8 allowanceField", VerticalAlign = VerticalAlign.Middle, Display = Display.InlineBlock, Height = 12 });

                allowances.Add(
                   new GridRow(Length.FromPoints(36), new TextBlock("B") { Class = "textHB9", VerticalAlign = VerticalAlign.Middle },
                       rowB,
                       new TextBlock("B") { Class = "textHB9", VerticalAlign = VerticalAlign.Middle },
                       rowBField) { LineHeight = 36 });

                // Row C                                   
                Section rowCField = new Section { LineHeight = 22 };
                rowCField.Add(new TextBox("rowC") { Class = "textH8 allowanceField", VerticalAlign = VerticalAlign.Bottom, Display = Display.InlineBlock, Height = 10 });

                Section rowCMarker = new Section { LineHeight = 22 };
                rowCMarker.Add(new TextBlock("C") { Class = "textHB9", VerticalAlign = VerticalAlign.Bottom, Display = Display.InlineBlock, LineHeight = 10 });

                allowances.Add(
                    new GridRow(Length.FromPoints(23), new TextBlock("C") { Class = "textHB9" },
                        new TextBlock(allowanceC[0]) { Class = "textH8" },
                        rowCMarker,
                        rowCField) { LineHeight = 10 });

                // Row D
                allowances.Add(
                   new GridRow(Length.FromPoints(10), new TextBlock("D") { Class = "textHB9" },
                       new TextBlock(allowanceD[0]) { Class = "textH8" },
                       new TextBlock("D") { Class = "textHB9" },
                       new TextBox("rowD") { Class = "textH8 allowanceField" }));

                // Row E
                allowances.Add(
                   new GridRow(Length.FromPoints(12), new TextBlock("E") { Class = "textHB9" },
                       new TextBlock(allowanceE[0]) { Class = "textH8" },
                       new TextBlock("E") { Class = "textHB9" },
                       new TextBox("rowE") { Class = "textH8 allowanceField" }));

                // Row F                                   
                Section rowFField = new Section();
                rowFField.Add(new TextBox("rowF") { Class = "textH8 allowanceField", VerticalAlign = VerticalAlign.Top, Display = Display.InlineBlock, Height = 11 });

                allowances.Add(
                    new GridRow(Length.FromPoints(22),
                        new TextBlock("F") { Class = "textHB9" },
                        new TextBlock(allowanceF[0]) { Class = "textH8" },
                        new TextBlock("F") { Class = "textHB9", VerticalAlign = VerticalAlign.Top },
                        rowFField) { LineHeight = 11 });

                // Row G
                Section rowGField = new Section { LineHeight = 48 };
                rowGField.Add(new TextBox("rowG") { Class = "textH8 allowanceField", VerticalAlign = VerticalAlign.Bottom, Display = Display.InlineBlock, Height = 12 });

                Section rowGMarker = new Section { LineHeight = 48 };
                rowGMarker.Add(new TextBlock("G") { Class = "textHB9", VerticalAlign = VerticalAlign.Bottom, Display = Display.InlineBlock, LineHeight = 12 });

                Section rowGDescription = new Section { Class = "inlineList textH8" };

                rowGDescription.Add(new TextBlock(allowanceG[0]) { ListStyle = ListStyle.None });
                rowGDescription.Add(new TextBlock(allowanceG[1]));
                rowGDescription.Add(new TextBlock(allowanceG[2]));

                allowances.Add(new GridRow(Length.FromPoints(48), new TextBlock("G") { Class = "textHB9" }, rowGDescription, rowGMarker, rowGField) { LineHeight = 12 });

                // Row H
                Section rowHDescription = new Section();
                rowHDescription.Add(new TextBlock(allowanceH[0]) { Class = "textH8" });
                rowHDescription.Add(new TextBlock(allowanceH[1]) { Class = "textTR6" });
                allowances.Add(new GridRow(Length.FromPoints(12), new TextBlock("H") { Class = "textHB9" }, rowHDescription, new TextBlock("H") { Class = "textHB9" }, new TextBox("rowH") { Class = "textH8 allowanceField" }) { LineHeight = 12 });


                // Row I
                Section rowIDescription = new Section { ColSpan = 3 };

                TextBlock rowIDescriptionHeader = new TextBlock(allowanceI[0]) { Display = Display.InlineBlock, Width = 50, Height = 40, VerticalAlign = VerticalAlign.Middle, Class = "textH8", LineHeight = 10 };
                rowIDescription.Add(rowIDescriptionHeader);
                rowIDescription.Add(new TextBlock(allowanceI[1]) { Class = "textTR53", VerticalAlign = VerticalAlign.Bottom, Margin = new Thickness(0, 0, 0, 7) });

                Section rowIOptionsList = new Section { Class = "inlineList textH8", LineHeight = 9, Id = "rowIoptions" };
                rowIOptionsList.Add(new TextBlock(allowanceI[2]) { Padding = new Thickness(0, 0, 0, 1) });
                rowIOptionsList.Add(new TextBlock(allowanceI[3]) { Padding = new Thickness(0, 0, 0, 1) });
                rowIOptionsList.Add(new TextBlock(allowanceI[4]));

                rowIDescription.Add(rowIOptionsList);

                allowances.Add(new GridRow(Length.FromPoints(55), ContentElement.Empty, rowIDescription) { LineHeight = 55 });

                page1.Add(allowances);
                page1.Add(new Hr());

                // starting the content at/below the tear-off line
                Section tearOffLine = new Section { Width = Length.FromPercentage(100), Height = 22, LineHeight = 24 };
                tearOffLine.Add(new Section { Class = "dashedBottomBorder", BorderColor = RgbColors.Black, Width = Length.FromPercentage(20), Height = 13, Display = Display.InlineBlock, Margin = new Thickness(0, 0, 2, 0), Float = Float.Left });
                tearOffLine.Add(new TextBlock("Separate here and give Form W-4 to your employer. Keep the top part for your records.") { Class = "textHB8", VerticalAlign = VerticalAlign.Bottom });
                tearOffLine.Add(new Section { Class = "dashedBottomBorder", BorderColor = RgbColors.Black, Width = Length.FromPercentage(23), Height = 13, Display = Display.InlineBlock, Float = Float.Right });

                page1.Add(tearOffLine);

                // Cert header

                // top left part
                Grid certHeaderLeft = new Grid(22, 58) { Class = "certHeaderPart", Width = Length.FromPercentage(15), InnerBorder = Border.None };

                GridRow certHeaderLeftTopRow = new GridRow { Height = 23, LineHeight = 24 };
                Section certHeaderLeftTopCell = new Section();
                certHeaderLeftTopCell.Add(new TextBlock("Form") { Class = "textH6", VerticalAlign = VerticalAlign.Bottom, LineHeight = 12 });
                certHeaderLeftTopRow.Add(certHeaderLeftTopCell);
                certHeaderLeftTopRow.Add(new TextBlock("W-4") { Class = "textHB24", VerticalAlign = VerticalAlign.Bottom });
                certHeaderLeft.Add(certHeaderLeftTopRow);
                certHeaderLeft.Add(new GridRow(new TextBlock("Department of the Treasury Internal Revenue Service") { Class = "textH6", ColSpan = 2 }));

                // middle part
                Section certHeaderMiddle = new Section { Class = "certHeaderPart", Width = Length.FromPercentage(70), Align = Align.Center };
                certHeaderMiddle.Add(new TextBlock("Employee's Withholding Allowance Certificate") { Class = "textHB14" });
                certHeaderMiddle.Add(new Br { Height = 2 });
                certHeaderMiddle.Add(new TextBlock("►") { Class = "textTR6" });
                certHeaderMiddle.Add(new TextBlock("Whether you are entitled to claim a certain number of allowances or exemption from withholding is") { Class = "textHB7" });
                certHeaderMiddle.Add(new Br());
                certHeaderMiddle.Add(new TextBlock("subject to review by the IRS. Your employer may be required to send a copy of this form to the IRS.") { Class = "textHB7" });

                // right part
                Section certHeaderRight = new Section { Class = "certHeaderPart", Width = Length.FromPercentage(15), BorderRight = Border.None, Align = Align.Center, Padding = new Thickness(0, 3, 0, 0) };
                certHeaderRight.Add(new TextBlock("OMB No. 1545-0074") { Class = "textH7" });
                certHeaderRight.Add(new Br());
                certHeaderRight.Add(new TextBlock("20") { Class = "textHB21", TextRenderingMode = TextRenderingMode.Stroke });
                certHeaderRight.Add(new TextBlock("15") { Class = "textHB21" });

                page1.Add(certHeaderLeft);
                page1.Add(certHeaderMiddle);
                page1.Add(certHeaderRight);

                // employee data table

                string[] employee = { "1", "Your first name and middle initial", "Lastname", "2", "Your social security number", "Home address (number and street or rural route)", "3", "Single", "Married", "Married, but withhold at higher Single rate.", "Note. If married, but legally separated, or spouse is a nonresident alien, check the “Single” box.", "City or town, state, and ZIP code", "4", "If your last name differs from that shown on your social security card,", "check here. You must call 1-800-772-1213 for a replacement card.", "►" };
                // name
                Section employeeName = new Section { Class = "certEmployeeDataRow", Width = Length.FromPercentage(33.5) };
                employeeName.Add(new TextBlock(employee[0]) { Class = "textHB7", Padding = new Thickness(12, 0, 15, 0) });
                employeeName.Add(new TextBlock(employee[1]) { Class = "textH7" });
                employeeName.Add(new Br());
                employeeName.Add(new TextBox("employeeName") { Class = "textH7" });
                page1.Add(employeeName);

                // lastname
                Section employeeLastname = new Section { Class = "certEmployeeDataRow", Width = Length.FromPercentage(41) };
                employeeLastname.Add(new TextBlock(employee[2]) { Class = "textH7", Padding = new Thickness(6, 0, 0, 0) });
                employeeLastname.Add(new Br());
                employeeLastname.Add(new TextBox("employeeLastname") { Class = "textH7" });
                page1.Add(employeeLastname);

                // ssn
                Section employeeSSN = new Section { Class = "certEmployeeDataRow", Width = Length.FromPercentage(25.5), BorderRight = Border.None };
                employeeSSN.Add(new TextBlock(employee[3]) { Class = "textHB7", Padding = new Thickness(6, 0, 10, 0) });
                employeeSSN.Add(new TextBlock(employee[4]) { Class = "textHB7" });
                employeeSSN.Add(new Br());
                employeeSSN.Add(new TextBox("employeeSSN") { Class = "textH7" });
                page1.Add(employeeSSN);

                // address
                Section employeeAddress = new Section { Class = "certEmployeeDataRow", Width = Length.FromPercentage(51) };
                employeeAddress.Add(new TextBlock(employee[5]) { Class = "textH7", Padding = new Thickness(30, 0, 0, 0) });
                employeeAddress.Add(new Br());
                employeeAddress.Add(new TextBox("employeeAddress") { Class = "textH7" });
                page1.Add(employeeAddress);

                // marriage status
                Section employeeStatus = new Section { Class = "certEmployeeDataRow", Width = Length.FromPercentage(49), BorderRight = Border.None, LineHeight = 12 };
                employeeStatus.Add(new TextBlock(employee[6]) { Class = "textHB7", Padding = new Thickness(4, 0, 5, 0) });
                employeeStatus.Add(new RadioButton("maritalStatus", "", "certSingle", CheckBoxMark.Check) { Class = "certCheckbox" });
                employeeStatus.Add(new TextBlock(employee[7]) { Class = "textH7", Padding = new Thickness(4, 0, 10, 0) });
                employeeStatus.Add(new RadioButton("maritalStatus", "", "certMarried", CheckBoxMark.Check) { Class = "certCheckbox" });
                employeeStatus.Add(new TextBlock(employee[8]) { Class = "textH7", Padding = new Thickness(4, 0, 5, 0) });
                employeeStatus.Add(new RadioButton("maritalStatus", "", "certMarriedWithholdSingle", CheckBoxMark.Check) { Class = "certCheckbox" });
                employeeStatus.Add(new TextBlock(employee[9]) { Class = "textH7", Padding = new Thickness(4, 0, 5, 0) });
                employeeStatus.Add(new Br());
                employeeStatus.Add(new TextBlock(employee[10]) { Class = "textH6", Padding = new Thickness(4, 0, 0, 0) });
                page1.Add(employeeStatus);

                // address2
                Section employeeAddress2 = new Section { Class = "certEmployeeDataRow", Width = Length.FromPercentage(51) };
                employeeAddress2.Add(new TextBlock(employee[11]) { Class = "textH7", Padding = new Thickness(30, 0, 0, 0) });
                employeeAddress2.Add(new Br());
                employeeAddress2.Add(new TextBox("employeeAddress2") { Class = "textH7" });
                page1.Add(employeeAddress2);

                // Note
                Section employeeSSNCardStatus = new Section { Class = "certEmployeeDataRow", Width = Length.FromPercentage(49), BorderRight = Border.None, LineHeight = 11 };
                employeeSSNCardStatus.Add(new TextBlock(employee[12]) { Class = "textHB7", Padding = new Thickness(4, 0, 6, 0) });
                employeeSSNCardStatus.Add(new TextBlock(employee[13]) { Class = "textHB7" });
                employeeSSNCardStatus.Add(new Br());
                employeeSSNCardStatus.Add(new TextBlock(employee[14]) { Class = "textHB7", Padding = new Thickness(13, 0, 2, 0) });
                employeeSSNCardStatus.Add(new TextBlock(employee[15]) { Class = "textTR8", Padding = new Thickness(0, 0, 2, 0) });
                employeeSSNCardStatus.Add(new CheckBox("ssnCardStatus", CheckBoxMark.Check) { Class = "certCheckbox" });
                page1.Add(employeeSSNCardStatus);

                // allowance cert rows 5-7
                string[] certRow = { "5", "Total number of allowances you are claiming (from line H above or from the applicable worksheet on page 2)", "6", "Additional amount, if any, you want withheld from each paycheck . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .", "$", "I claim exemption from withholding for 2015, and I certify that I meet both of the following conditions for exemption.", "Last year I had a right to a refund of all federal income tax withheld because I had no tax liability, and", "This year I expect a refund of all federal income tax withheld because I expect to have no tax liability.", "7", "If you meet both conditions, write “Exempt” here . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .", "►", "Under penalties of perjury, I declare that I have examined this certificate and, to the best of my knowledge and belief, it is true, correct, and complete." };

                Grid certGridRows57 = new Grid(Length.FromPoints(30), Length.Auto, Length.FromPoints(15), Length.FromPoints(10), Length.FromPoints(56)) { InnerBorder = Border.None };

                certGridRows57.Add(new GridRow(12, new TextBlock(certRow[0]) { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) },
                                                   new TextBlock(certRow[1]) { Class = "textH8" },
                                                   new TextBlock(certRow[0]) { Class = "textHB9 thinBorderLeftBottom", Padding = new Thickness(5, 0, 0, 0) },
                                                   new TextBox("certRow5") { Class = "textHB9 thinBorderLeftBottom", ColSpan = 2 }));
                //row 6
                certGridRows57.Add(new GridRow(12, new TextBlock(certRow[2]) { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) },
                                                   new TextBlock(certRow[3]) { Class = "textH8" },
                                                   new TextBlock(certRow[2]) { Class = "textHB9 thinBorderLeftBottom", Padding = new Thickness(5, 0, 0, 0) },
                                                   new TextBlock(certRow[4]) { Class = "textHB9 thinBorderLeftBottom", Padding = new Thickness(2, 0, 0, 0) },
                                                   new TextBox("certRow6") { Class = "textHB9 thinBorderLeftBottom" }));

                //row 7
                Section itemsListCertRow7 = new Section { Class = "textH8 inlineList", ColSpan = 2 };
                itemsListCertRow7.Add(new TextBlock(certRow[5]) { ListStyle = ListStyle.None });
                itemsListCertRow7.Add(new TextBlock(certRow[6]));
                itemsListCertRow7.Add(new TextBlock(certRow[7]));

                certGridRows57.Add(new GridRow(36, new TextBlock(certRow[8]) { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) },
                                                   itemsListCertRow7,
                                                   new Section { Background = RgbColors.LightGray, Class = "thinBorderLeft", ColSpan = 2, Margin = new Thickness(0, 0.5, 0, 0) }) { LineHeight = 12 });

                // row 7-2
                Section itemsListCertRow7part2 = new Section { Id = "certRow7", Class = "textH8 thinBorderBottom", ColSpan = 5, Padding = new Thickness(30, 0, 0, 0) };
                itemsListCertRow7part2.Add(new TextBlock(certRow[9]));
                itemsListCertRow7part2.Add(new TextBlock(certRow[10]) { Class = "textTR8" });
                itemsListCertRow7part2.Add(new Section(new TextBlock(certRow[8]) { Padding = new Thickness(5, 0, 5, 0) },
                                                       new TextBox("certRow7") { Class = "textHB9 thinBorderLeft", Display = Display.InlineBlock }) { Class = "textHB9 thinBorderLeft thinBorderTop", Display = Display.InlineBlock });

                certGridRows57.Add(new GridRow(11, itemsListCertRow7part2) { LineHeight = 11 });

                //row 8
                certGridRows57.Add(new GridRow(11, new TextBlock(certRow[11]) { ColSpan = 5, Class = "textH8" }));
                page1.Add(certGridRows57);

                // signature part
                page1.Add(new Br { Height = 5 });

                // employer header 1
                Section employerheader1 = new Section { Class = "certEmployerDataRowSmall", Width = Length.FromPercentage(74.5), BorderRight = Border.None, LineHeight = 9 };
                employerheader1.Add(new TextBlock("Employee’s signature") { Class = "textHB8" });
                employerheader1.Add(new Br());
                employerheader1.Add(new TextBlock("(This form is not valid unless you sign it.)") { Class = "textH8" });
                employerheader1.Add(new TextBlock("►") { Class = "textTR8" });
                page1.Add(employerheader1);

                // employer header 2
                Section employerheader2 = new Section { Class = "certEmployerDataRowSmall", Width = Length.FromPercentage(25.5), BorderRight = Border.None, LineHeight = 9 };
                employerheader2.Add(new Br { Height = 9 });
                employerheader2.Add(new TextBlock("Date") { Class = "textHB8", Padding = new Thickness(4, 0, 0, 0) });
                employerheader2.Add(new TextBlock("►") { Class = "textTR8" });
                page1.Add(employerheader2);


                // employer name
                Section employerName = new Section { Class = "certEmployerDataRow", Width = Length.FromPercentage(61) };
                employerName.Add(new TextBlock("8") { Class = "textHB7", Padding = new Thickness(12, 0, 12, 0) });
                employerName.Add(new TextBlock("Employer’s name and address (Employer: Complete lines 8 and 10 only if sending to the IRS.)") { Class = "textH7" });
                employerName.Add(new Br());
                employerName.Add(new TextBox("employerName") { Class = "textH7" });
                page1.Add(employerName);

                // office
                Section employerOffice = new Section { Class = "certEmployerDataRow", Width = Length.FromPercentage(13.5) };
                employerOffice.Add(new TextBlock("9") { Class = "textHB7", Padding = new Thickness(2, 0, 2, 0) });
                employerOffice.Add(new TextBlock("Office code(optional)") { Class = "textH6" });
                employerOffice.Add(new TextBox("officeCode") { Class = "textH7" });
                page1.Add(employerOffice);

                // EIN
                Section employerEIN = new Section { Class = "certEmployerDataRow", Width = Length.FromPercentage(25.5), BorderRight = Border.None };
                employerEIN.Add(new TextBlock("10") { Class = "textHB7", Padding = new Thickness(6, 0, 10, 0) });
                employerEIN.Add(new TextBlock("Employer identification number (EIN)") { Class = "textH6" });
                employerEIN.Add(new Br());
                employerEIN.Add(new TextBox("employerEIN") { Class = "textH7" });
                page1.Add(employerEIN);

                // footer
                Grid gridFooter = new Grid(Length.FromPercentage(61), Length.FromPercentage(13.5), Length.FromPercentage(25.5)) { InnerBorder = Border.None, LineHeight = 12 };
                Section rightFooterCell = new Section(new TextBlock("Form") { Class = "textH7" }, new TextBlock("W-4") { Class = "textHB10" }, new TextBlock("(2015)") { Class = "textH7" }) { Align = Align.Right };

                gridFooter.Add(new GridRow(new TextBlock("For Privacy Act and Paperwork Reduction Act Notice, see page 2.") { Class = "textHB8" }, new TextBlock("Cat. No. 10220Q") { Align = Align.Center, Class = "textH7" }, rightFooterCell));
                page1.Add(gridFooter);
                document.Add(page1);

                Section page2 = new Section { Class = "page" };
                page2.Add(new Section(new TextBlock("Form W-4 (2015)") { Class = "textH7", Display = Display.InlineBlock, Align = Align.Left, Width = Length.FromPercentage(15) }, new TextBlock(ctx => string.Format("Page {0}", ctx.CurrentPage + 1)) { Class = "textHB8", Display = Display.InlineBlock, Align = Align.Right }));

                /* Deductions and Adjustments Worksheet */
                document.StyleManager.RegisterStyle(".tableHeader", new Style { Color = RgbColors.Black, Align = Align.Center });
                document.StyleManager.RegisterStyle(".tableH3", new Style { Color = RgbColors.Black, Align = Align.Justify, Padding = new Thickness(2, 2, 2, 2), VerticalAlign = VerticalAlign.Bottom });
                document.StyleManager.RegisterStyle("gridrow", new Style { Font = new Font(StandardFonts.Helvetica, 8) });
                document.StyleManager.RegisterStyle("grid#worksheet > gridrow > textblock", new Style { Font = new Font(StandardFonts.Helvetica, 7), LineHeight = 7 });
                document.StyleManager.RegisterStyle(".clean", new Style { InnerBorder = null, InnerBorderColor = RgbColors.Transparent, Padding = new Thickness(0, 0, 8, 0) });

                string[] worksheetTable = { "Deductions and Adjustments Worksheet", 
                                            " Note.", "Use this worksheet only if you plan to itemize deductions or claim certain credits or adjustments to income.", 
                                            "Enter an estimate of your 2015 itemized deductions. These include qualifying home mortgage interest, charitable contributions, state and local taxes, medical expenses in excess of 10% (7.5% if either you or your spouse was born before January 2, 1951) of your income, and miscellaneous deductions. For 2015, you may have to reduce your itemized deductions if your income is over $309,900 and you are married filing jointly or are a qualifying widow(er); $284,050 if you are head of household; $258,250 if you are single and not head of household or a qualifying widow(er); or $154,950 if you are married filing separately. See Pub. 505 for details", 
                                            "Enter:", "{", "$12,600 if married filing jointly or qualifying widow(er)", "$9,250 if head of household", "$6,300 if single or married filing separately", "}", ".........", "Subtract line 2 from line 1. If zero or less, enter “-0-”", "Enter an estimate of your 2015 adjustments to income and any additional standard deduction (see Pub. 505)", 
                                            "Add lines 3 and 4 and enter the total. (Include any amount for credits from the Converting Credits to Withholding Allowances for 2015 Form W-4 worksheet in Pub. 505.)", 
                                            "Enter an estimate of your 2015 nonwage income (such as dividends or interest)", 
                                            "Subtract line 6 from line 5. If zero or less, enter “-0-”", 
                                            "Divide the amount on line 7 by $4,000 and enter the result here. Drop any fraction", 
                                            "Enter the number from the Personal Allowances Worksheet, line H, page 1", 
                                            "Add lines 8 and 9 and enter the total here. If you plan to use the Two-Earners/Multiple Jobs Worksheet, also enter this total on line 1 below. Otherwise, stop here and enter this total on Form W-4, line 5, page 1" };

                Section nestedList = new Section { Class = "inlineList", Display = Display.InlineBlock, ListMarker = ListMarker.None, LineHeight = 12, Width = Length.FromPercentage(52), Padding = new Thickness(12, 0, 12, 0) };
                nestedList.Add(new TextBlock(worksheetTable[6]));
                nestedList.Add(new TextBlock(worksheetTable[7]));
                nestedList.Add(new TextBlock(worksheetTable[8]));

                Section nested = new Section { Class = "textH8", Display = Display.InlineBlock, Height = 36, VerticalAlign = VerticalAlign.Middle };
                nested.Add(new TextBlock(worksheetTable[4]));
                nested.Add(new TextBlock(worksheetTable[5]) { Class = "textTR28", VerticalAlign = VerticalAlign.Top, Padding = new Thickness(0, 0, 0, 3) });
                nested.Add(nestedList);
                nested.Add(new TextBlock(worksheetTable[9]) { Class = "textTR28", VerticalAlign = VerticalAlign.Top, Padding = new Thickness(0, 0, 0, 3) });
                nested.Add(new TextBlock(worksheetTable[10]) { CharacterSpacing = new CharacterSpacing(10) });

                Grid deductions = new Grid(Length.FromPercentage(5), Length.FromPercentage(78), Length.FromPercentage(5), Length.FromPercentage(12)) { Class = "clean" };
                deductions.Add(new GridRow { new TextBlock(worksheetTable[0]) { Class = "tableHeader textFGD11", ColSpan = 4 } });
                deductions.Add(new GridRow { new TextBlock(worksheetTable[1]) { Class = "textHB8" }, new TextBlock(worksheetTable[2]), ContentElement.Empty, ContentElement.Empty });
                deductions.Add(new GridRow { new TextBlock("1") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new TextBlock(worksheetTable[3]) { Align = Align.Justify, LineHeight = 8 }, new TextBlock("1") { Class = "textHB9", Padding = new Thickness(10, 40, 0, 0) }, new Section(new TextBlock("$") { Display = Display.InlineBlock, Width = Length.FromPercentage(10) }, new TextBox("worksheetRow1") { Display = Display.InlineBlock, Class = "thinBorderBottom" }) { Class = "textHB9", Padding = new Thickness(0, 40, 0, 0) } });
                deductions.Add(new GridRow { new TextBlock("2") { Class = "textHB9", Padding = new Thickness(10, 12, 0, 0) }, nested, new TextBlock("2") { Class = "textHB9", Padding = new Thickness(10, 15, 0, 0) }, new Section(new TextBlock("$") { Display = Display.Inline, Width = Length.FromPercentage(10) }, new TextBox("worksheetRow2") { Display = Display.InlineBlock, Class = "thinBorderBottom" }) { Class = "textHB9", Padding = new Thickness(0, 15, 0, 0) } });
                deductions.Add(new GridRow { new TextBlock("3") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBlock(worksheetTable[11]), new TextBlock(worksheetTable[10]) { CharacterSpacing = new CharacterSpacing(12) }), new TextBlock("3") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBlock("$") { Display = Display.InlineBlock, Width = Length.FromPercentage(10) }, new TextBox("worksheetRow3") { Display = Display.InlineBlock, Class = "thinBorderBottom" }) { Class = "textHB9" } });
                deductions.Add(new GridRow { new TextBlock("4") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new TextBlock(worksheetTable[12]), new TextBlock("4") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBlock("$") { Display = Display.InlineBlock, Width = Length.FromPercentage(10) }, new TextBox("worksheetRow4") { Display = Display.InlineBlock, Class = "thinBorderBottom" }) { Class = "textHB9" } });
                deductions.Add(new GridRow { new TextBlock("5") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBlock(worksheetTable[13]), new TextBlock(worksheetTable[10]) { CharacterSpacing = new CharacterSpacing(12) }), new TextBlock("5") { Class = "textHB9", Padding = new Thickness(10, 10, 0, 0) }, new Section(new TextBlock("$") { Display = Display.InlineBlock, Width = Length.FromPercentage(10) }, new TextBox("worksheetRow5") { Display = Display.InlineBlock, Class = "thinBorderBottom" }) { Class = "textHB9", Padding = new Thickness(0, 10, 0, 0) } });
                deductions.Add(new GridRow { new TextBlock("6") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBlock(worksheetTable[14]), new TextBlock(worksheetTable[10]) { CharacterSpacing = new CharacterSpacing(12) }), new TextBlock("6") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBlock("$") { Display = Display.InlineBlock, Width = Length.FromPercentage(10) }, new TextBox("worksheetRow6") { Display = Display.InlineBlock, Class = "thinBorderBottom" }) { Class = "textHB9" } });
                deductions.Add(new GridRow { new TextBlock("7") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBlock(worksheetTable[15]), new TextBlock(worksheetTable[10]) { CharacterSpacing = new CharacterSpacing(12) }), new TextBlock("7") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBlock("$") { Display = Display.InlineBlock, Width = Length.FromPercentage(10) }, new TextBox("worksheetRow7") { Display = Display.InlineBlock, Class = "thinBorderBottom" }) { Class = "textHB9" } });
                deductions.Add(new GridRow { new TextBlock("8") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBlock(worksheetTable[16]), new TextBlock(worksheetTable[10]) { CharacterSpacing = new CharacterSpacing(12) }), new TextBlock("8") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBox("worksheetRow8") { Display = Display.InlineBlock, Class = "thinBorderBottom" }) { Class = "textHB9" } });
                deductions.Add(new GridRow { new TextBlock("9") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new TextBlock(worksheetTable[17]), new TextBlock("9") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBox("worksheetRow9") { Display = Display.InlineBlock, Class = "thinBorderBottom" }) { Class = "textHB9" } });
                deductions.Add(new GridRow { new TextBlock("10") { Class = "textHB9", Padding = new Thickness(6, 0, 0, 0) }, new TextBlock(worksheetTable[18]), new TextBlock("10") { Class = "textHB9", Padding = new Thickness(6, 10, 0, 0) }, new Section(new TextBox("worksheetRow10") { Display = Display.InlineBlock, Class = "thinBorderBottom" }) { Class = "textHB9", Padding = new Thickness(0, 10, 0, 0) } });

                /*Multiple Jobs Worksheet*/
                string[] multijobs =
                    {
                        "Two-Earners/Multiple Jobs Worksheet (See Two earners or multiple jobs on page 1.)",
                        "Note.",
                        "Use this worksheet only if the instructions under line H on page 1 direct you here.",
                        "Enter the number from line H, page 1 (or from line 10 above if you used the Deductions and Adjustments Worksheet)",
                        "Find the number in Table 1 below that applies to the LOWEST paying job and enter it here. However, if you are married filing jointly and wages from the highest paying job are $65,000 or less, do not enter more than “3” .  .  .  .  .  .  .",
                        "If line 1 is more than or equal to line 2, subtract line 2 from line 1. Enter the result here (if zero, enter “-0-”) and on Form W-4, line 5, page 1. Do not use the rest of this worksheet",
                        "If line 1 is less than line 2, enter “-0-” on Form W-4, line 5, page 1. Complete lines 4 through 9 below to figure the additional withholding amount necessary to avoid a year-end tax bill.",
                        "Enter the number from line 2 of this worksheet",
                        "Enter the number from line 1 of this worksheet",
                        "Subtract line 5 from line 4",
                        "Find the amount in Table 2 below that applies to the HIGHEST paying job and enter it here",
                        "Multiply line 7 by line 6 and enter the result here. This is the additional annual withholding needed",
                        "Divide line 8 by the number of pay periods remaining in 2015. For example, divide by 25 if you are paid every two weeks and you complete this form on a date in January when there are 25 pay periods remaining in 2015. Enter the result here and on Form W-4, line 6, page 1. This is the additional amount to be withheld from each paycheck","........","...................","......","...."
                    };

                Grid multijob = new Grid(Length.FromPercentage(5), Length.FromPercentage(78), Length.FromPercentage(5), Length.FromPercentage(12)) { Id = "multijob", Class = "clean" };
                multijob.Add(new GridRow { new TextBlock(multijobs[0]) { Class = "tableHeader textFGD11", ColSpan = 4 } });
                multijob.Add(new GridRow { new TextBlock(multijobs[1]) { Class = "textHB8" }, new TextBlock(multijobs[2]), ContentElement.Empty, ContentElement.Empty });

                multijob.Add(new GridRow { new TextBlock("1") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new TextBlock(multijobs[3]), new TextBlock("1") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBox("worksheetRow11") { Display = Display.InlineBlock, Class = "thinBorderBottom" }) { Class = "textHB9" } });
                multijob.Add(new GridRow { new TextBlock("2") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new TextBlock(multijobs[4]), new TextBlock("2") { Class = "textHB9", Padding = new Thickness(10, 10, 0, 0) }, new Section(new TextBox("worksheetRow12") { Display = Display.InlineBlock, Class = "thinBorderBottom", Padding = new Thickness(0, 10, 0, 0) }) { Class = "textHB9" } });
                multijob.Add(new GridRow { new TextBlock("3") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBlock(multijobs[5]), new TextBlock(worksheetTable[10]) { Display = Display.Inline, CharacterSpacing = new CharacterSpacing(12) }), new TextBlock("3") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBox("worksheetRow13") { Display = Display.InlineBlock, Class = "thinBorderBottom" }) { Class = "textHB9" } });
                multijob.Add(new GridRow { new TextBlock(multijobs[1]) { Class = "textHB8" }, new TextBlock(multijobs[6]), ContentElement.Empty, ContentElement.Empty });

                multijob.Add(new GridRow { new TextBlock("4") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBlock(multijobs[7]) { Padding = new Thickness(0, 0, 20, 0) }, new TextBlock(multijobs[13]) { CharacterSpacing = new CharacterSpacing(15) }, new TextBlock("4") { Class = "textHB9", Padding = new Thickness(10, 0, 10, 0) }, new TextBox("worksheetRow14") { Display = Display.InlineBlock, Class = "textHB9 thinBorderBottom" }), ContentElement.Empty, ContentElement.Empty });
                multijob.Add(new GridRow { new TextBlock("5") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBlock(multijobs[8]) { Padding = new Thickness(0, 0, 20, 0) }, new TextBlock(multijobs[13]) { CharacterSpacing = new CharacterSpacing(15) }, new TextBlock("5") { Class = "textHB9", Padding = new Thickness(10, 0, 10, 0) }, new TextBox("worksheetRow15") { Display = Display.InlineBlock, Class = "textHB9 thinBorderBottom" }), ContentElement.Empty, ContentElement.Empty });

                multijob.Add(new GridRow { new TextBlock("6") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBlock(multijobs[9]), new TextBlock(multijobs[14]) { CharacterSpacing = new CharacterSpacing(15) }), new TextBlock("6") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBox("worksheetRow16") { Display = Display.InlineBlock, Class = "thinBorderBottom" }) { Class = "textHB9" } });
                multijob.Add(new GridRow { new TextBlock("7") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBlock(multijobs[10]), new TextBlock(multijobs[15]) { CharacterSpacing = new CharacterSpacing(15) }), new TextBlock("7") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBlock("$") { Display = Display.InlineBlock, Width = Length.FromPercentage(10) }, new TextBox("worksheetRow17") { Display = Display.InlineBlock, Class = "thinBorderBottom" }) { Class = "textHB9" } });
                multijob.Add(new GridRow { new TextBlock("8") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBlock(multijobs[11]), new TextBlock(multijobs[16]) { CharacterSpacing = new CharacterSpacing(15) }), new TextBlock("8") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new Section(new TextBlock("$") { Display = Display.InlineBlock, Width = Length.FromPercentage(10) }, new TextBox("worksheetRow18") { Display = Display.InlineBlock, Class = "thinBorderBottom" }) { Class = "textHB9" } });
                multijob.Add(new GridRow { new TextBlock("9") { Class = "textHB9", Padding = new Thickness(10, 0, 0, 0) }, new TextBlock(multijobs[12]), new TextBlock("9") { Class = "textHB9", Padding = new Thickness(10, 20, 0, 0) }, new Section(new TextBlock("$") { Display = Display.InlineBlock, Width = Length.FromPercentage(10) }, new TextBox("worksheetRow19") { Display = Display.InlineBlock, Class = "thinBorderBottom" }) { Class = "textHB9", Padding = new Thickness(0, 20, 0, 0) } });

                Section nestedSection = new Section { Border = new Border(0.5), BorderColor = RgbColors.Black };
                nestedSection.Add(deductions);
                page2.Add(nestedSection);

                Section nestedSection2 = new Section { Border = new Border(0.5), BorderTop = new Border(0), BorderColor = RgbColors.Black };
                nestedSection2.Add(multijob);
                page2.Add(nestedSection2);

                string[] worksheets = { "Table 1", "Table 2", "Married Filing Jointly", "All Others", "If wages from LOWEST paying job are—", "If wages from HIGHEST paying job are—", "Enter on line 2 above", "Enter on line 7 above" };

                Grid worksheet = new Grid(Length.FromPercentage(15), Length.FromPercentage(10), Length.FromPercentage(15), Length.FromPercentage(10), Length.FromPercentage(15), Length.FromPercentage(10), Length.FromPercentage(15), Length.FromPercentage(10));
                worksheet.Add(new GridRow { new TextBlock(worksheets[0]) { Class = "tableHeader textHB10", ColSpan = 4 }, new TextBlock(worksheets[1]) { Class = "tableHeader textHB10", ColSpan = 4 } });
                worksheet.Add(new GridRow { new TextBlock(worksheets[2]) { Class = "tableHeader textHB8", ColSpan = 2 }, new TextBlock(worksheets[3]) { Class = "tableHeader textHB8", ColSpan = 2 }, new TextBlock(worksheets[2]) { Class = "tableHeader textHB8", ColSpan = 2 }, new TextBlock(worksheets[3]) { Class = "tableHeader textHB8", ColSpan = 2 } });
                worksheet.Add(new GridRow
                {
                    new TextBlock(worksheets[4]) {Class = "tableH3 textH6"},
                    new TextBlock(worksheets[6]) {Class = "tableH3 textH6"},
                    new TextBlock(worksheets[4]) {Class = "tableH3 textH6"},
                    new TextBlock(worksheets[6]) {Class = "tableH3 textH6"},
                    new TextBlock(worksheets[5]) {Class = "tableH3 textH6"},
                    new TextBlock(worksheets[7]) {Class = "tableH3 textH6"},
                    new TextBlock(worksheets[5]) {Class = "tableH3 textH6"},
                    new TextBlock(worksheets[7]) {Class = "tableH3 textH6"},
                });

                // First column
                Grid numbers = new Grid(Length.FromPercentage(50), Length.FromPercentage(50)) { Class = "clean", Align = Align.Right, Id = "worksheet" };
                numbers.Add(new GridRow(new TextBlock("$0 -"), new TextBlock("$6,000")));
                numbers.Add(new GridRow(new TextBlock("6,001 -"), new TextBlock("13,000")));
                numbers.Add(new GridRow(new TextBlock("13,001 -"), new TextBlock("24,000")));
                numbers.Add(new GridRow(new TextBlock("24,001 -"), new TextBlock("26,000")));
                numbers.Add(new GridRow(new TextBlock("26,001 -"), new TextBlock("34,000")));
                numbers.Add(new GridRow(new TextBlock("34,001 -"), new TextBlock("44,000")));
                numbers.Add(new GridRow(new TextBlock("44,001 -"), new TextBlock("50,000")));
                numbers.Add(new GridRow(new TextBlock("50,001 -"), new TextBlock("65,000")));
                numbers.Add(new GridRow(new TextBlock("65,001 -"), new TextBlock("75,000")));
                numbers.Add(new GridRow(new TextBlock("75,001 -"), new TextBlock("80,000")));
                numbers.Add(new GridRow(new TextBlock("80,001 -"), new TextBlock("100,000")));
                numbers.Add(new GridRow(new TextBlock("100,001 -"), new TextBlock("115,000")));
                numbers.Add(new GridRow(new TextBlock("115,001 -"), new TextBlock("130,000")));
                numbers.Add(new GridRow(new TextBlock("130,001 -"), new TextBlock("140,000")));
                numbers.Add(new GridRow(new TextBlock("140,001 -"), new TextBlock("150,000")));
                numbers.Add(new GridRow(new TextBlock("150,001 a"), new TextBlock("nd over") { Align = Align.Left }));

                // 2nd column
                Grid numbers2 = new Grid(Length.FromPercentage(100)) { Class = "clean", Align = Align.Center, Id = "worksheet" };
                numbers2.Add(new GridRow(new TextBlock("0")));
                numbers2.Add(new GridRow(new TextBlock("1")));
                numbers2.Add(new GridRow(new TextBlock("2")));
                numbers2.Add(new GridRow(new TextBlock("3")));
                numbers2.Add(new GridRow(new TextBlock("4")));
                numbers2.Add(new GridRow(new TextBlock("5")));
                numbers2.Add(new GridRow(new TextBlock("6")));
                numbers2.Add(new GridRow(new TextBlock("7")));
                numbers2.Add(new GridRow(new TextBlock("8")));
                numbers2.Add(new GridRow(new TextBlock("9")));
                numbers2.Add(new GridRow(new TextBlock("10")));
                numbers2.Add(new GridRow(new TextBlock("11")));
                numbers2.Add(new GridRow(new TextBlock("12")));
                numbers2.Add(new GridRow(new TextBlock("13")));
                numbers2.Add(new GridRow(new TextBlock("14")));
                numbers2.Add(new GridRow(new TextBlock("15")));

                // 3rd column
                Grid numbers3 = new Grid(Length.FromPercentage(60), Length.FromPercentage(40)) { Class = "clean", Align = Align.Right, Id = "worksheet" };
                numbers3.Add(new GridRow(new TextBlock("$0 -"), new TextBlock("$8,000")));
                numbers3.Add(new GridRow(new TextBlock("8,001 -"), new TextBlock("17,000")));
                numbers3.Add(new GridRow(new TextBlock("17,001 -"), new TextBlock("26,000")));
                numbers3.Add(new GridRow(new TextBlock("26,001 -"), new TextBlock("34,000")));
                numbers3.Add(new GridRow(new TextBlock("34,001 -"), new TextBlock("44,000")));
                numbers3.Add(new GridRow(new TextBlock("44,001 -"), new TextBlock("75,000")));
                numbers3.Add(new GridRow(new TextBlock("75,001 -"), new TextBlock("85,000")));
                numbers3.Add(new GridRow(new TextBlock("85,001 -"), new TextBlock("110,000")));
                numbers3.Add(new GridRow(new TextBlock("110,001 -"), new TextBlock("125,000")));
                numbers3.Add(new GridRow(new TextBlock("125,001 -"), new TextBlock("140,000")));
                numbers3.Add(new GridRow(new TextBlock("140,001 a"), new TextBlock("nd over") { Align = Align.Left }));

                // 4th column
                Grid numbers4 = new Grid(Length.FromPercentage(100)) { Class = "clean", Align = Align.Center, Id = "worksheet" };
                numbers4.Add(new GridRow(new TextBlock("0")));
                numbers4.Add(new GridRow(new TextBlock("1")));
                numbers4.Add(new GridRow(new TextBlock("2")));
                numbers4.Add(new GridRow(new TextBlock("3")));
                numbers4.Add(new GridRow(new TextBlock("4")));
                numbers4.Add(new GridRow(new TextBlock("5")));
                numbers4.Add(new GridRow(new TextBlock("6")));
                numbers4.Add(new GridRow(new TextBlock("7")));
                numbers4.Add(new GridRow(new TextBlock("8")));
                numbers4.Add(new GridRow(new TextBlock("9")));
                numbers4.Add(new GridRow(new TextBlock("10")));

                // 5th column
                Grid numbers5 = new Grid(Length.FromPercentage(60), Length.FromPercentage(40)) { Class = "clean", Align = Align.Right, Id = "worksheet" };
                numbers5.Add(new GridRow(new TextBlock("$0 -"), new TextBlock("$75,000")));
                numbers5.Add(new GridRow(new TextBlock("75,001 -"), new TextBlock("135,000")));
                numbers5.Add(new GridRow(new TextBlock("135,001 -"), new TextBlock("205,000")));
                numbers5.Add(new GridRow(new TextBlock("205,001 -"), new TextBlock("360,000")));
                numbers5.Add(new GridRow(new TextBlock("360,001 -"), new TextBlock("405,000")));
                numbers5.Add(new GridRow(new TextBlock("405,001 a"), new TextBlock("nd over") { Align = Align.Left }));

                // 6th column
                Grid numbers6 = new Grid(Length.FromPercentage(100)) { Class = "clean", Align = Align.Center, Id = "worksheet" };
                numbers6.Add(new GridRow(new TextBlock("$600")));
                numbers6.Add(new GridRow(new TextBlock("1,000")));
                numbers6.Add(new GridRow(new TextBlock("1,120")));
                numbers6.Add(new GridRow(new TextBlock("1,320")));
                numbers6.Add(new GridRow(new TextBlock("1,400")));
                numbers6.Add(new GridRow(new TextBlock("1,580")));

                // 7th column
                Grid numbers7 = new Grid(Length.FromPercentage(60), Length.FromPercentage(40)) { Class = "clean", Align = Align.Right, Id = "worksheet" };
                numbers7.Add(new GridRow(new TextBlock("$0 -"), new TextBlock("$38,000")));
                numbers7.Add(new GridRow(new TextBlock("38,001 -"), new TextBlock("83,000")));
                numbers7.Add(new GridRow(new TextBlock("83,001 -"), new TextBlock("180,000")));
                numbers7.Add(new GridRow(new TextBlock("180,001 -"), new TextBlock("395,000")));
                numbers7.Add(new GridRow(new TextBlock("395,001 a"), new TextBlock("nd over") { Align = Align.Left }));

                // 8th column
                Grid numbers8 = new Grid(Length.FromPercentage(100)) { Class = "clean", Align = Align.Center, Id = "worksheet" };
                numbers8.Add(new GridRow(new TextBlock("$600")));
                numbers8.Add(new GridRow(new TextBlock("1,000")));
                numbers8.Add(new GridRow(new TextBlock("1,120")));
                numbers8.Add(new GridRow(new TextBlock("1,320")));
                numbers8.Add(new GridRow(new TextBlock("1,580")));

                worksheet.Add(new GridRow { numbers, numbers2, numbers3, numbers4, numbers5, numbers6, numbers7, numbers8 });
                page2.Add(worksheet);

                /* privacy act */
                string[] privacyActInfo = { "Privacy Act and Paperwork Reduction Act Notice.", " We ask for the information on this form to carry out the Internal Revenue laws of the United States. Internal Revenue Code sections 3402(f)(2) and 6109 and their regulations require you to provide this information; your employer uses it to determine your federal income tax withholding. Failure to provide a properly completed form will result in your being treated as a single person who claims no withholding allowances; providing fraudulent information may subject you to penalties. Routine uses of this information include giving it to the Department of Justice for civil and criminal litigation; to cities, states, the District of Columbia, and U.S. commonwealths and possessions for use in administering their tax laws; and to the Department of Health and Human Services for use in the National Directory of New Hires. We may also disclose this information to other countries under a tax treaty, to federal and state agencies to enforce federal nontax criminal laws, or to federal law enforcement and intelligence agencies to combat terrorism.", "You are not required to provide the information requested on a form that is subject to the Paperwork Reduction Act unless the form displays a valid OMB control number. Books or records relating to a form or its instructions must be retained as long as their contents may become material in the administration of any Internal Revenue law. Generally, tax returns and return information are confidential, as required by Code section 6103.", "The average time and expenses required to complete and file this form will vary depending on individual circumstances. For estimated averages, see the instructions for your income tax return.", "If you have suggestions for making this form simpler, we would be happy to hear from you. See the instructions for your income tax return." };

                // privacy act left
                Section privacyActL = new Section { Class = "introSection", Width = Length.FromPercentage(48) };
                privacyActL.Add(new TextBlock(privacyActInfo[0]) { Class = "intro textHB7" });
                privacyActL.Add(new TextBlock(privacyActInfo[1]) { Class = "intro textH7" });
                page2.Add(privacyActL);
                page2.Add(new Section { Display = Display.InlineBlock, Width = Length.FromPercentage(4) });

                // privacy act right
                Section privacyActR = new Section { Class = "introSection", Width = Length.FromPercentage(48) };
                privacyActR.Add(new TextBlock(privacyActInfo[2]) { Class = "intro textH7", TextIndent = 7 });
                privacyActR.Add(new Br { Height = 7 });
                privacyActR.Add(new TextBlock(privacyActInfo[3]) { Class = "intro textH7", TextIndent = 7 });
                privacyActR.Add(new Br { Height = 7 });
                privacyActR.Add(new TextBlock(privacyActInfo[4]) { Class = "intro textH7", TextIndent = 7 });
                page2.Add(privacyActR);
                page2.Add(new Section { Display = Display.InlineBlock, Width = Length.FromPercentage(4) });

                document.Add(page2);
                document.Write(fs, new ResourceManager(), new PageBoundary(new Boundary(612, 792)));
            }
            Process.Start(@"..\..\..\OutputDocuments\W4Form.pdf");
        }
    }
}
