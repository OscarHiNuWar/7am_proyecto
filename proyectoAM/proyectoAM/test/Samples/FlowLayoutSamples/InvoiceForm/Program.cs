using System;
using System.Collections.Generic;
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
using Apitron.PDF.Kit.Interactive.Actions;
using Apitron.PDF.Kit.Interactive.Annotations;
using Apitron.PDF.Kit.Interactive.Forms;
using Apitron.PDF.Kit.Styles;
using Apitron.PDF.Kit.Styles.Appearance;
using Font = Apitron.PDF.Kit.Styles.Text.Font;

namespace InvoiceForm
{
    internal class Program
    {
        /// <summary>
        /// This sample shows how to modify form field values.
        /// </summary>        
        private static void Main(string[] args)
        {
           
            using (FileStream fs = new FileStream(@"..\..\..\OutputDocuments\Invoice.pdf", FileMode.Create))
            {
                FlowDocument document = new FlowDocument();

                // register resource logo image
                ResourceManager rm = new ResourceManager();
                rm.RegisterResource(new Apitron.PDF.Kit.FixedLayout.Resources.XObjects.Image("logo", @"..\..\..\data\logo.png"));

                // add document fields
                document.Fields.Add(new TextField("date", DateTime.Now.ToString("MM-dd-yyyy")));
                document.Fields.Add(new TextField("number",""));
                document.Fields.Add(new TextField("Id",""));
                document.Fields.Add(new TextField("CustomerName", ""));
                document.Fields.Add(new TextField("CompanyName",""));
                document.Fields.Add(new TextField("Address","") { IsMultiline = true });
                document.Fields.Add(new TextField("Comment","") { IsMultiline = true });
                document.Fields.Add(new TextField("subtotal","") { Text = "0.00", QuaddingJustification = QuaddingJustification.Centered });
                document.Fields.Add(new TextField("qty","") { Text = "1", QuaddingJustification = QuaddingJustification.Centered });
                document.Fields.Add(new TextField("unitPrice", "") { QuaddingJustification = QuaddingJustification.Centered });
                document.Fields.Add(new TextField("tax","") { Text = "6.25", QuaddingJustification = QuaddingJustification.Centered });
                document.Fields.Add(new TextField("comission", "") { Text = "0.00", QuaddingJustification = QuaddingJustification.Centered });
                document.Fields.Add(new TextField("calculated", "") { Text = "0.00", QuaddingJustification = QuaddingJustification.Centered });
                document.Fields.Add(new PushbuttonField("button","") { TextColor = new double[] {0.2,0.4,0.9}, FontSize = 12, Caption ="Calculate", ViewColor = new double[]{0.5,0.4,0.6}, QuaddingJustification = QuaddingJustification.Centered });

                // define different styles
                document.StyleManager.RegisterStyle("grid", new Style { BorderRadius = 12, InnerBorderColor = RgbColors.Black, InnerBorder = new Border(0.5) });
                document.StyleManager.RegisterStyle("textbox", new Style { Display = Display.InlineBlock, BorderColor = RgbColors.Black,  BorderBottom = new Border(0.5), Font = new Font(StandardFonts.Helvetica, 15), Align = Align.Center});
                document.StyleManager.RegisterStyle(".h3", new Style { Align = Align.Center, Font = new Font(StandardFonts.TimesBoldItalic, 25) });
                document.StyleManager.RegisterStyle(".header", new Style { Background = RgbColors.DeepSkyBlue, Align = Align.Center, Color = RgbColors.White, Font = new Font(StandardFonts.Courier, 16) });
                document.StyleManager.RegisterStyle(".fieldLabel", new Style { Display = Display.InlineBlock, Width = Length.FromPercentage(35), Align = Align.Right, Padding = new Thickness(0, 0, 5, 0) });
                document.StyleManager.RegisterStyle(".sum", new Style { Align = Align.Right, Border = null, Font = new Font("ArialBold", 12), Margin = new Thickness(5, 5, 10, 5) });
                document.StyleManager.RegisterStyle("#address", new Style { Padding = new Thickness(10, 0, 5, 10) });
                document.StyleManager.RegisterStyle("#address > textblock", new Style { LineHeight = 14, Display = Display.Block, Align = Align.Left });
                document.StyleManager.RegisterStyle("#footer", new Style { Align = Align.Center, Font = new Font(StandardFonts.TimesRoman, 20) });
                document.StyleManager.RegisterStyle("#footer > textblock", new Style { LineHeight = 25, Display = Display.Block, Align = Align.Center });
                


                // add company's logo
                Section name = new Section { Display = Display.InlineBlock, Align = Align.Left, Width = Length.FromPercentage(63) };
                name.Add(new Image("logo") { Height = Length.FromPixels(76), Width = Length.FromPixels(140) });
                document.PageHeader.Add(name); 
                
                // identification info
                Section date = new Section { Display = Display.InlineBlock, Width = Length.FromPercentage(37)};
                date.Add(new TextBlock("INVOICE") { Color = RgbColors.DeepSkyBlue, Padding = new Thickness(40,0,0,0), Font = new Font(StandardFonts.Courier, 26) });
                date.Add(new Br{Height = 3});
                date.Add(new TextBlock("Date: ") { Class="fieldLabel" }); 
                date.Add(new TextBox("date"));
                date.Add(new Br{Height = 3});
                date.Add(new TextBlock("Invoice #") { Class="fieldLabel" });
                date.Add(new TextBox("number"));
                date.Add(new Br{Height = 3});
                date.Add(new TextBlock("Customer ID:") { Class="fieldLabel" });
                date.Add(new TextBox("Id"));
                document.PageHeader.Add(date);

                // company info
                Section address = new Section{Id = "address"};
                address.Add(new TextBlock("Contoso LLC."));  
                address.Add(new TextBlock("36th Street,"));  
                address.Add(new TextBlock("Redmond,"));      
                address.Add(new TextBlock("WA 98052-7329,"));
                address.Add(new TextBlock("USA"));           
                address.Add(new TextBlock("Tel: +1 (206) 905-9580"));
                address.Add(new TextBlock("Fax: +1 (206) 905-9581"));
                document.Add(address);
                
                // customer info
                Section billTo = new Section{Display = Display.Block, Background = RgbColors.DeepSkyBlue, Color = RgbColors.White, Margin = new Thickness(10, 0, 5, 0), Width = Length.FromPercentage(50)};
                billTo.Add(new TextBlock("BILL TO") { Padding = new Thickness(5, 0, 0, 0), Font = new Font(StandardFonts.Courier, 16) });
                document.Add(billTo);
                Section details = new Section {Display = Display.InlineBlock, Align = Align.Right, Margin = new Thickness(0, 0, 5, 0), Width = Length.FromPercentage(50) };
                details.Add(new TextBlock("Customer name: ") { Class = "fieldLabel" }); 
                details.Add(new TextBox("CustomerName") );
                details.Add(new Br { Height = 3 });
                details.Add(new TextBlock("Company :") { Class = "fieldLabel" }); 
                details.Add(new TextBox("CompanyName"));
                details.Add(new Br { Height = 3 });
                details.Add(new TextBlock("Address :") { Class = "fieldLabel"}); 
                details.Add(new TextBox("Address") {Height = 60}); 
                document.Add(details);
                
                // footer
                Section footer = new Section{Id="footer"};
                footer.Add(new TextBlock("If you have any questions regarding this invoice, please contact"));
                footer.Add(new TextBlock("support@apitron.com"));
                footer.Add(new TextBlock("Thank you for your Business!"){Font = new Font(StandardFonts.TimesBoldItalic, 25)});
                document.PageFooter.Add(footer);
                
                // products table
                Grid table = new Grid(Length.FromPercentage(6),Length.FromPercentage(60), Length.FromPercentage(20),Length.FromPercentage(14)) { Margin = new Thickness(40, 20, 40, 20) };
                GridRow header = new GridRow { Class = "header" };
                header.Add(new TextBlock("#"));
                header.Add(new TextBlock("Description"));
                header.Add(new TextBlock("Qty"));
                header.Add(new TextBlock("Price"));
                table.Add(header);

                // add products values 
                Dictionary<string, string> products = new Dictionary<string, string>();
                products.Add("1", "Small grocery bag");
                products.Add("2", "Medium grocery bag");
                products.Add("3", "Large grocery bag");
                
                // create fiels
                ChoiceField productName = new ChoiceField("productName", "product", products, ChoiceFieldType.ComboBox);
                document.Fields.Add(productName);
                Choice product = new Choice("productName"){Font= new Font(StandardFonts.Courier, 14)};
               
                // add row
                GridRow row = new GridRow();
                row.Add(new TextBlock("1") { Align = Align.Center, Padding = new Thickness(0, 3, 2, 0) });
                row.Add(product);
                row.Add(new TextBox("qty"));
                row.Add(new TextBox("unitPrice"));
                table.Add(row);

                // generate empty rows
                for (int i = 2; i < 5; i++)
                {
                    table.Add(new GridRow(new TextBlock(i.ToString()) { Align = Align.Center, Padding = new Thickness(0, 3, 2, 0) }, ContentElement.Empty, ContentElement.Empty, ContentElement.Empty));
                }
                
                // comment field
                Section common = new Section { RowSpan = 7, ColSpan = 2 };
                Section comments = new Section { Display = Display.Inline, Background = RgbColors.DeepSkyBlue, Color = RgbColors.White };
                comments.Add(new TextBlock("OTHER COMMENTS") { Font = new Font(StandardFonts.Courier, 16), Padding = new Thickness(10,2,10,2) });
                Section comment = new Section { Margin = new Thickness(10, 0, 5, 0) };
                comment.Add(new TextBox("Comment") { Height = Length.FromPercentage(100), Width = Length.FromPercentage(100) });
                common.Add(comments);
                common.Add(comment);

                // total counts
                table.Add(new GridRow{common, new TextBlock("Subtotal $") 	{ Class ="sum" }, new TextBox("subtotal")});
                table.Add(new GridRow(new TextBlock("Taxable $")        	{ Class ="sum" }, new TextBox("subtotal")));
                table.Add(new GridRow(new TextBlock("Tax rate %")       	{ Class ="sum" }, new TextBox("tax")));
                table.Add(new GridRow(new TextBlock("Tax due $")        	{ Class ="sum" }));
                table.Add(new GridRow(new TextBlock("Other $")          	{ Class ="sum" }, new TextBox("comission")));
                table.Add(new GridRow(new TextBlock("Total (in USD) ")  	{ Class ="sum" }, new TextBox("calculated")));
                document.Add(table);

                // add javascript action to a button which calculates values for total fields
                document.Add(new Section(new PushButton("button", new JavaScriptAction("app.beep(); var product = this.getField(\"productName\");  var unitPrice = this.getField(\"unitPrice\"); var sub = this.getField(\"subtotal\");  var all = this.getField(\"calculated\"); var rate = this.getField(\"tax\"); var qty = this.getField(\"qty\"); var mult1 = qty.value * 125.0;  var mult2 = qty.value * 415.0;  var mult3 = qty.value * 785.0; if(product.value == 1) { unitPrice.value = mult1 }; if(product.value == 2) { unitPrice.value = mult2 }; if(product.value == 3) { unitPrice.value = mult3 }; sub.value = unitPrice.value; all.value = (sub.value - ((sub.value * rate.value) /100)).toFixed(2);")) { Align = Align.Right, Font = new Font(StandardFonts.Helvetica, 12), Background = RgbColors.Orange, Color = RgbColors.DeepSkyBlue, BorderRadius = 12, Width = 100, Height = 20 }) { Align = Align.Right, Margin = new Thickness(0, 0, 20, 0) });
                document.Write(fs, rm, new PageBoundary(Boundaries.A4));
            }
            Process.Start(@"..\..\..\OutputDocuments\Invoice.pdf");
        }
    }
}
