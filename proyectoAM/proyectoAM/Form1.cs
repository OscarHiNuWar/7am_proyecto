﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using proyectoAM.clases;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace proyectoAM
{
    public partial class Form1 : Form
    {
        facturacion con = new facturacion();
        double total; //Aca va el precio total de la suma de todos los precios del DataGridView
        double subtotal=0;
        double itbis = 0.18;
        double subitbis=0;
        double precio=0;
        int cantidad=0;
        int canttotal = 0;
        string nombre; //Aca va el dato "nombre" de la tabla cliente
        string codenfc = "A0100100115";
        int numnfc = 0;
        string nfc; //Aca va el dato "NFC" de la tabla cliente
        string rnc; //Aca va el dato "RNC" de la tabla cliente
        string telefono;
        string email;
       // string compania;
        string moneda;
        string trabajo;
        string pago;
        string vence; //Este dato puede variar a uno que funcione con DataTimePicker
        DataTable tabla;
        //addFactura agrego = new addFactura();
        // 
        string user;
        int id=0;
        

        DataTable addColumns()
        {
            tabla = new DataTable();
<<<<<<< HEAD
            //Parte Inferior de la Tabla
=======
            //tabla.Columns.Add("ID");
            //tabla.Columns.Add("Moneda");
>>>>>>> origin/master
            tabla.Columns.Add("Cantidad");
            tabla.Columns.Add("Descripcion");
            tabla.Columns.Add("");
            tabla.Columns.Add("Precio");
<<<<<<< HEAD
 
=======
            //tabla.Columns.Add("Condiciones de Pago");
            // tabla.Columns.Add("Vence");


            /*tabla.Columns.Add("Descripcion");*/
>>>>>>> origin/master
            return tabla;
        }

        void addNombre()
        {
            cbNombre.Items.Add("7AM");
            cbNombre.Items.Add("Coca-cola");
            cbNombre.Items.Add("Pepsi");
            cbNombre.Items.Add("Infotep");
        }

        void addCompania()
        {
            cbCompania.Items.Add("7AM");
            cbCompania.Items.Add("Coca-cola");
            cbCompania.Items.Add("Pepsi");
            cbCompania.Items.Add("Infotep");
        }

        void addTrabajo()
        {
            cbTrabajo.Items.Add("Desarrollo de Pagina Web");
            cbTrabajo.Items.Add("Continuacion de Red Social");
            cbTrabajo.Items.Add("Posicionamiento");
            //cbTrabajo.Items.Add("Infotep");
        }

        void addCondicionPago()
        {
            cbPago.Items.Add("Efectivo");
            cbPago.Items.Add("Cheque");
        }

        void addMoneda()
        {
            cbMoneda.Items.Add("Pesos Dominicanos");
            cbMoneda.Items.Add("Dolares");
        }

        void addDescripcion()
        {
            cbDescripcion.Items.Add("Realización pagina web");
            cbDescripcion.Items.Add("Diseño de línea grafica");
            cbDescripcion.Items.Add("Diseño de logotipo");
            cbDescripcion.Items.Add("Servicio y Mantenimiento Página Web");
            cbDescripcion.Items.Add("Administración de Redes Sociales");
            cbDescripcion.Items.Add("Contrato mantenimiento de pagina");
        }

        void reset()
        {
            txtPrecio.Text = null;
            nudCantidad.Value = 1;
            cbDescripcion.Text = null;
            //btnAgregar.Enabled = false;
        }

        public Form1()
        {
            InitializeComponent();
            btnEliminar.Enabled = false;
            //btnExportar.Enabled = false;
            //DataTable tabla = agrego.muestra();
            //Programc pdf = new Programc();
<<<<<<< HEAD
            //con.conectame();
=======
>>>>>>> origin/master
            Tbla.DataSource = addColumns();
            addNombre();
            addCompania();
            addTrabajo();
            addCondicionPago();
            addMoneda();
            addDescripcion();
            user = Environment.UserName.ToString();
<<<<<<< HEAD
            DateTime date2 = DateTime.Now; // will give the date for today
            dtVence.Value = date2;
            numnfc += 1;
            txtNCF.Text = codenfc + Convert.ToString(numnfc);
=======
            //Tbla.Columns[0].Visible = false;
>>>>>>> origin/master
        }



        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtPrecio.Text == "") { MessageBox.Show("Favor de poner un precio"); }
            else if (cbNombre.Text == "") { MessageBox.Show("Favor de poner un cliente"); }
            else if (cbMoneda.Text == "") { MessageBox.Show("Favor de poner una moneda"); }
            else if (cbTrabajo.Text == "") { MessageBox.Show("Favor de poner un Trabajo"); }
            else if (cbPago.Text == "") { MessageBox.Show("Favor de poner una condicion de pago"); }
            else if (cbDescripcion.Text == "") { MessageBox.Show("Favor de poner un articulo"); }

            else {

                if(cbNombre.SelectedItem.ToString() != "") { 
                nombre = cbNombre.SelectedItem.ToString();
                } else { nombre = cbNombre.Text; }
                nfc = txtNCF.Text;
                rnc = txtRnc.Text;
                email = txtEmail.Text;
                telefono = txtTelefono.Text;

               /* if (con.agregaruser(new string[] { nombre, nfc, rnc, email, telefono }))
                     {
                         
                     }*/


            btnExportar.Enabled = true;
            id = id + 1;
            
            cantidad = Convert.ToInt32(nudCantidad.Text.ToString());
            precio = double.Parse(txtPrecio.Text);

            
            subtotal = (precio * cantidad) + subtotal;
            subitbis = (subtotal * itbis) + subitbis;
            total = (subtotal + subitbis);
                string din;
                if (cbMoneda.Text == "Pesos Dominicanos")
                {
                    din = "RD$ ";
                    // txtSubtotal.Text = "RD$ " + Convert.ToString(subtotal) + ".00";
                    txtSubtotal.Text = "RD$" + Convert.ToString(string.Format("{0:0.00}", subtotal));
                    txtItebis.Text = "RD$" + Convert.ToString(string.Format("{0:0.00}",subitbis));
                    txtTotal.Text = "RD$" + Convert.ToString(string.Format("{0:0.00}",total));
                }
                else
                {
                    din = "    $ ";
                    txtSubtotal.Text = "$ " + Convert.ToString(string.Format("{0:0.00}",subtotal));
                    txtItebis.Text = "$ " + Convert.ToString(string.Format("{0:0.00}", subitbis));
                    txtTotal.Text = "$ " + Convert.ToString(string.Format("{0:0.00}", total));
                }
            
<<<<<<< HEAD
=======

            txtSubtotal.Text = Convert.ToString(subtotal);
            txtItebis.Text = Convert.ToString(subitbis);
            txtTotal.Text = Convert.ToString(total);
>>>>>>> origin/master
            moneda = cbMoneda.Text.ToString();
            trabajo = cbTrabajo.SelectedItem.ToString();
            pago = cbPago.SelectedItem.ToString();
            vence = dtVence.Value.ToString().Remove(8);

<<<<<<< HEAD
            tabla.Rows.Add(Convert.ToString(cantidad), cbDescripcion.Text.ToString(), "", din+Convert.ToString(precio) + ".00");
            canttotal = canttotal + Convert.ToInt32(nudCantidad.Text.ToString());
=======
            tabla.Rows.Add(  Convert.ToString(cantidad),  cbTrabajo.Text.ToString(), Convert.ToString(precio));

>>>>>>> origin/master
            reset();
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnAgregar.Enabled = true;
        }



        private void button1_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            exporta();
            ///

=======
            Document doc = new Document(iTextSharp.text.PageSize.A4, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("C:\\Users\\" + user + " /Documents/Oscar.pdf", FileMode.Create));
            doc.Open();

            iTextSharp.text.Image am = iTextSharp.text.Image.GetInstance("logo7am.jpg");
            am.ScalePercent(25f);
            am.SetAbsolutePosition(doc.PageSize.Width - 36f - 72f, doc.PageSize.Height - 36f - 50f);
            doc.Add(am);

            if(string.IsNullOrEmpty(cbNombre.Text) ) { Paragraph parag = new Paragraph("FACTURA\nNOMBRE: " + cbNombre.SelectedItem.ToString() + "\n"); doc.Add(parag); }
            else {  Paragraph parag = new Paragraph("FACTURA\n\nNOMBRE: " + cbNombre.Text.ToString() + "\n\n"); doc.Add(parag); }

            Paragraph RNC = new Paragraph("RNC: " + txtRnc.Text+ "\n\n"); doc.Add(RNC);
            Paragraph NFC = new Paragraph("NFC: " + txtNCF.Text + "\n\n"); doc.Add(NFC);
            Paragraph tel = new Paragraph("TELÉFONO: "+txtTelefono.Text+ "\n\n"); doc.Add(tel);
            Paragraph email = new Paragraph("EMAIL: "+txtEmail.Text+ "\n\n"); doc.Add(email);

            if (string.IsNullOrEmpty(cbCompania.Text)) { Paragraph parag = new Paragraph("COMPAÑIA: " + cbCompania.SelectedItem.ToString() + "\n\n");  doc.Add(parag); }
            else { Paragraph parag = new Paragraph("COMPAÑIA: " + cbCompania.Text.ToString() + "\n\n"); doc.Add(parag); }

            PdfPTable pdfTable = new PdfPTable(4);
            pdfTable.HorizontalAlignment = 1;
            pdfTable.WidthPercentage = 100f;
           

            //ACA VA MONEDA
            PdfPCell moneda = new PdfPCell(new Phrase("MONEDA", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.BOLD)));
            moneda.Colspan = 1;
            moneda.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            PdfPCell cmoneda = new PdfPCell(new Phrase(cbMoneda.SelectedItem.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.NORMAL)));
            cmoneda.Colspan = 1;
            cmoneda.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/


            //ACA VA TRABAJO
            PdfPCell trabajo = new PdfPCell(new Phrase("TRABAJO", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.BOLD)));
            trabajo.Colspan = 1;
            trabajo.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            PdfPCell ctrabajo = new PdfPCell(new Phrase(cbTrabajo.SelectedItem.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.NORMAL)));
            ctrabajo.Colspan = 1;
            ctrabajo.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/


            //ACA VA CONDICIONES
            PdfPCell condicion = new PdfPCell(new Phrase("CONDICIONES DE PAGO", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.BOLD)));
            condicion.Colspan = 1;
            condicion.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            PdfPCell cpago = new PdfPCell(new Phrase(cbPago.SelectedItem.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.NORMAL)));
            cpago.Colspan = 1;
            cpago.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/


            //ACA VENCIMIENTO
            PdfPCell vence = new PdfPCell(new Phrase("FECHA DE VENCIMIENTO", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.BOLD)));
            vence.Colspan = 1;
            vence.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            PdfPCell cvence = new PdfPCell(new Phrase(dtVence.Text.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.NORMAL)));
            cvence.Colspan = 1;
            cvence.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/


            pdfTable.AddCell(moneda);
            pdfTable.AddCell(trabajo);
            pdfTable.AddCell(condicion);
            pdfTable.AddCell(vence);
            pdfTable.AddCell(cmoneda);
            pdfTable.AddCell(ctrabajo);
            pdfTable.AddCell(cpago);
            pdfTable.AddCell(cvence);

            //GRAN TABLA!!!

            PdfPTable grantable = new PdfPTable(Tbla.Columns.Count);
            grantable.WidthPercentage = 100f;
            

            //Add the headers from the DataGridView to the table
            for (int j=0;j<Tbla.Columns.Count; j++)
            {
                grantable.AddCell(new Phrase(Tbla.Columns[j].HeaderText));
            }

            //Flag the first row as a header
            grantable.HeaderRows = 1;

            //Agregar los rows!
            for (int i=0; i<Tbla.Rows.Count; i++)
            {
                for (int k=0; k < Tbla.Columns.Count; k++)
                {
                    if(Tbla[k,i].Value != null)
                    {
                        grantable.AddCell(new Phrase(Tbla[k, i].Value.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.NORMAL)));
                    }
                }
            }

            doc.Add(pdfTable);
            Paragraph espacio = new Paragraph("\n\n"); doc.Add(espacio);
            doc.Add(grantable);
            doc.Add(espacio);


            //SUBTOTAL
           /* PdfPCell subtotal = new PdfPCell(new Phrase("SUB - TOTAL:", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.NORMAL)));
            subtotal.Colspan = 1;
            subtotal.HorizontalAlignment = 0; //0=left, 1=center, 2=right*/
           /* PdfPCell sub = new PdfPCell(new Phrase(txtSubtotal.Text, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.NORMAL)));
            sub.Colspan = 1;
            sub.HorizontalAlignment = 0; //0=left, 1=center, 2=right*/

            //ITBIS
           /* PdfPCell itbis = new PdfPCell(new Phrase("ITBIS: ", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.NORMAL)));
            subtotal.Colspan = 1;
            subtotal.HorizontalAlignment = 0; //0=left, 1=center, 2=right*/
           /* PdfPCell it = new PdfPCell(new Phrase(txtItebis.Text, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.NORMAL)));
            it.Colspan = 1;
            it.HorizontalAlignment = 0; //0=left, 1=center, 2=right*/

            //ITBIS
           /* PdfPCell total = new PdfPCell(new Phrase("TOTAL: ", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.NORMAL)));
            subtotal.Colspan = 1;
            subtotal.HorizontalAlignment = 0; //0=left, 1=center, 2=right*/
           /* PdfPCell to = new PdfPCell(new Phrase(txtTotal.Text, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.NORMAL)));
            to.Colspan = 1;
            to.HorizontalAlignment = 0; //0=left, 1=center, 2=right*/

            Paragraph subtotal = new Paragraph("SUB-TOTAL: " + txtSubtotal.Text + "\n"); doc.Add(subtotal);
            Paragraph itbis = new Paragraph("ITBIS: " + txtItebis.Text + "\n"); doc.Add(itbis);
            Paragraph total = new Paragraph("TOTAL: " + txtTotal.Text + "\n"); doc.Add(total);
            /*doc.Add(subtotal);
            doc.Add(itbis);
            doc.Add(total);

            doc.Add(sub);
            doc.Add(it);
            doc.Add(to);*/

            doc.Close();
        
            MessageBox.Show("Factura exportada");
            

>>>>>>> origin/master
        }

        
        private void txtPrecio_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
      (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
      (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void Tbla_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          /*  btnEliminar.Visible = true;
            btnEliminar.Enabled = true;*/
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Estas seguro de eliminar esta Tabla", "Eliminar Feriado", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (DataGridViewRow item in this.Tbla.SelectedRows)
                {
                    Tbla.Rows.RemoveAt(item.Index);
                }
            }
            btnEliminar.Enabled = false;
        }

        private void Tbla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEliminar.Visible = true;
            btnEliminar.Enabled = true;
        }

        public void exporta()
        {

            Paragraph espacio = new Paragraph("\n\n");
            Paragraph espacio2 = new Paragraph("\n");

            var color = new BaseColor(0, 0, 0);


            //EL FONT!
            BaseFont trebutchet = BaseFont.CreateFont("../../Resources/Font/Trebuchet MS.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            BaseFont trebutchetbold = BaseFont.CreateFont("../../Resources/Font/Trebuchet MS Bold.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            //Letra de Titulo
            iTextSharp.text.Font titulo = new iTextSharp.text.Font(trebutchetbold, 16, iTextSharp.text.Font.BOLD);

            //Letra del texto general
            iTextSharp.text.Font texto = new iTextSharp.text.Font(trebutchet, 8, iTextSharp.text.Font.NORMAL);

            //Letra del texto Cliente
            iTextSharp.text.Font cliente = new iTextSharp.text.Font(trebutchetbold, 11, iTextSharp.text.Font.BOLD);

            //Letra del texto Totales
            iTextSharp.text.Font numero = new iTextSharp.text.Font(trebutchet, 9, iTextSharp.text.Font.NORMAL);

            //Letra del texto tablatitulo
            iTextSharp.text.Font tablatitulo = new iTextSharp.text.Font(trebutchetbold, 7, iTextSharp.text.Font.BOLD);

            //Letra del texto Firma
            iTextSharp.text.Font textfirma = new iTextSharp.text.Font(trebutchet, 11, iTextSharp.text.Font.UNDERLINE);

           
            

            var date = DateTime.Now; // will give the date for today
            string dateWithFormat = Convert.ToString(date.Date);
            Document doc = new Document(iTextSharp.text.PageSize.A4, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("C:\\Users\\" + user + " /Documents/Factura " +cbNombre.Text +" "+ date.ToString("dd-MM-yyyy") + ".pdf", FileMode.Create));
            
            doc.Open();

            iTextSharp.text.Image am = iTextSharp.text.Image.GetInstance("logo7am2.jpg");
            am.ScalePercent(25f);
            am.SetAbsolutePosition(doc.PageSize.Width - 36f - 122f, doc.PageSize.Height - 36f - 110f);
            doc.Add(am);

            /*if(string.IsNullOrEmpty(cbNombre.Text) ) { Paragraph parag = new Paragraph("FACTURA\nNOMBRE: " + cbNombre.SelectedItem.ToString() + "\n"); doc.Add(parag); }
            else {  Paragraph parag = new Paragraph("FACTURA\n\nNOMBRE: " + cbNombre.Text.ToString() + "\n\n"); doc.Add(parag); }*/
            /*Paragraph NFC = new Paragraph("NFC: " + txtNCF.Text + "\n\n"); doc.Add(NFC);
             Paragraph tel = new Paragraph("TELÉFONO: " + txtTelefono.Text + "\n\n"); doc.Add(tel);*/


            PdfPTable adjust = new PdfPTable(2);
            //adjust.HorizontalAlignment = 1;
            adjust.WidthPercentage = 85f;

            PdfPCell factura = new PdfPCell(new Phrase("\nFACTURA\n\n", titulo));
            factura.HorizontalAlignment = 0;
            factura.Colspan = 1;
            factura.Border = 0;
            

            PdfPCell rnc = new PdfPCell(new Phrase("RNC: \n" + txtRnc.Text + "\n\n", texto));
            rnc.HorizontalAlignment =0;
            rnc.Colspan = 1;
            rnc.Border = 0;

            PdfPCell empty = new PdfPCell(new Phrase("\n"));
            empty.Colspan=1;
            empty.Border = 0;

            PdfPCell rnce = new PdfPCell( new Phrase("NFC: " + "" + txtNCF.Text + "\n\n\n", texto));
            rnce.HorizontalAlignment = 0;
            rnce.Colspan = 1;
            rnce.Border = 0;

            var now = DateTime.Now;
            var newdate = now.Date;
            PdfPCell fechahoy = new PdfPCell(new Phrase("Fecha: " + newdate.ToString("dd/MM/yyyy") + "\n\n", texto));
            fechahoy.Colspan = 1;
            fechahoy.HorizontalAlignment = 2;
            fechahoy.Border = 0;

            PdfPCell telefono = new PdfPCell(new Phrase("TELEFONO: \n" + "" + txtTelefono.Text + "\n\n", texto));
            telefono.HorizontalAlignment = 0;
            telefono.Colspan = 1;
            telefono.Border = 0;

            PdfPCell lugar = new PdfPCell(new Phrase("SANTO DOMINGO,\nREPUBLICA DOMINICANA\n", texto));
            lugar.Colspan = 1;
            lugar.HorizontalAlignment = 2;
            lugar.Border = 0;

            adjust.AddCell(empty); adjust.AddCell(empty);
            adjust.AddCell(empty); adjust.AddCell(empty);
            adjust.AddCell(factura);adjust.AddCell(empty);
            adjust.AddCell(rnc); adjust.AddCell(empty);
            adjust.AddCell(rnce);
            adjust.AddCell(fechahoy);
            adjust.AddCell(telefono);
            adjust.AddCell(lugar);

            
            PdfPCell email = new PdfPCell(new Phrase("EMAIL:\n"+ txtEmail.Text + "\n\n", texto)); email.Border = 0; email.HorizontalAlignment = 0; email.Colspan = 2; adjust.AddCell(email);
            if (string.IsNullOrEmpty(cbCompania.Text)) { PdfPCell parag = new PdfPCell(new Phrase( ("CLIENTE: "  + cbNombre.SelectedItem.ToString()), cliente)); parag.Border = 0; parag.HorizontalAlignment = 0; parag.Colspan = 2; adjust.AddCell(parag); }
            else { PdfPCell parag = new PdfPCell(new Phrase ("CLIENTE: " + cbNombre.Text.ToString(), cliente)); parag.Border = 0; parag.HorizontalAlignment = 0; parag.Colspan = 2; adjust.AddCell(parag); }
            doc.Add(adjust);

            PdfPTable pdfTable = new PdfPTable(4);
            pdfTable.HorizontalAlignment = 1;
            pdfTable.WidthPercentage = 85f;
    
            //ACA VA MONEDA
            PdfPCell moneda = new PdfPCell(new Phrase("MONEDA", tablatitulo));
            moneda.Colspan = 1;
            moneda.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            moneda.Padding = 5;
            PdfPCell cmoneda = new PdfPCell(new Phrase(cbMoneda.SelectedItem.ToString(), texto));
            cmoneda.Colspan = 1;
            cmoneda.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            cmoneda.PaddingTop = 5;
            cmoneda.PaddingBottom = 8;


            //ACA VA TRABAJO
            PdfPCell trabajo = new PdfPCell(new Phrase("TRABAJO", tablatitulo));
            trabajo.Colspan = 1;
            trabajo.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            trabajo.Padding = 5;
            PdfPCell ctrabajo = new PdfPCell(new Phrase(cbTrabajo.SelectedItem.ToString(), texto));
            ctrabajo.Colspan = 1;
            ctrabajo.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            ctrabajo.Padding = 5;


            //ACA VA CONDICIONES
            PdfPCell condicion = new PdfPCell(new Phrase("CONDICIONES DE PAGO", tablatitulo));
            condicion.Colspan = 1;
            condicion.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            condicion.Padding = 5;
            PdfPCell cpago = new PdfPCell(new Phrase(cbPago.SelectedItem.ToString(), texto));
            cpago.Colspan = 1;
            cpago.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            cpago.Padding = 5;


            //ACA VENCIMIENTO
            PdfPCell vence = new PdfPCell(new Phrase("FECHA DE VENCIMIENTO", tablatitulo));
            vence.Colspan = 1;
            vence.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            vence.Padding = 5;
            var midato = dtVence.Value.ToString("dd/MM/yyyy");
            PdfPCell cvence = new PdfPCell(new Phrase(midato, texto));
            cvence.Colspan = 1;
            cvence.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            cvence.Padding = 5;


            pdfTable.AddCell(moneda);
            pdfTable.AddCell(trabajo);
            pdfTable.AddCell(condicion);
            pdfTable.AddCell(vence);
            pdfTable.AddCell(cmoneda);
            pdfTable.AddCell(ctrabajo);
            pdfTable.AddCell(cpago);
            pdfTable.AddCell(cvence);

            //GRAN TABLA!!!
            PdfPTable grantable = new PdfPTable(Tbla.Columns.Count);
            grantable.WidthPercentage = 85f;
            float[] widths = new float[] { 25f, 42f, 10f, 28f};
            grantable.SetWidths(widths);
            grantable.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/


            //Add the headers from the DataGridView to the table
            /* for (int j=0;j<Tbla.Columns.Count; j++)
             {
                 grantable.AddCell(new Phrase(Tbla.Columns[j].HeaderText));
             }*/

            //CANTIDDAD
            PdfPCell cant = new PdfPCell(new Phrase("CANTIDAD", tablatitulo));
            cant.Colspan = 1;
            cant.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            cant.Padding = 5;
            grantable.AddCell(cant);

            //DESCRIPCION
            PdfPCell descripcion = new PdfPCell(new Phrase("DESCRIPCIÒN", tablatitulo));
            descripcion.Colspan = 1;
            descripcion.Padding = 5;
            //descripcion.Width = 50;
            descripcion.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            grantable.AddCell(descripcion);

            //VACIO
            var FontColour = new BaseColor(255, 255, 255);
            var MyFont = FontFactory.GetFont("Times New Roman", 11, FontColour);
            PdfPCell vacio = new PdfPCell(new Phrase("B", MyFont));
            vacio.Colspan = 1;
            vacio.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            grantable.AddCell(vacio);

            //PRECIO
            PdfPCell precio = new PdfPCell(new Phrase("PRECIO", tablatitulo));
            precio.BackgroundColor = new iTextSharp.text.BaseColor(219, 229, 241);
            precio.Colspan = 1;
            precio.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            precio.Padding = 5;
            grantable.AddCell(precio);

            //Flag the first row as a header
            grantable.HeaderRows = 1;

            for (int k = 0; k < Tbla.Rows.Count; k++)
            {

                PdfPCell cante = new PdfPCell(new Phrase(Tbla[0, k].Value.ToString(), texto));
                cante.HorizontalAlignment = 0;
                cante.PaddingTop = 5;
                cante.PaddingBottom = 8;
                cante.PaddingLeft = 20;
                //cante.BorderWidth = 1f;
                // cante.BorderWidthBottom = 0; 

                PdfPCell descri = new PdfPCell(new Phrase(Tbla[1, k].Value.ToString(), texto));
                descri.HorizontalAlignment = 0;
                descri.PaddingTop = 5;
                descri.PaddingBottom = 8;
                descri.PaddingLeft = 20;
                //descri.BorderWidth = 1f;
                // descri.BorderWidthBottom = 0;

                PdfPCell vaco =new PdfPCell (new Phrase(Tbla[2, k].Value.ToString()));
                //vaco.BorderWidth = 1f;
               // vaco.BorderWidthBottom = 0;


                PdfPCell tolt = new PdfPCell(new Phrase(Tbla[3, k].Value.ToString(), texto));
                tolt.BackgroundColor = new iTextSharp.text.BaseColor(219, 229, 241);
                tolt.HorizontalAlignment = 0;
                tolt.PaddingTop = 5;
                tolt.PaddingBottom = 8;
                tolt.PaddingLeft=20;
                //tolt.BorderWidth = 1;
                //tolt.BorderWidth = 1f;
                //tolt.BorderWidthBottom = 0;

                /* PdfPCell empty = new PdfPCell(new Phrase(""));
                 empty.BackgroundColor = new iTextSharp.text.BaseColor(149, 160, 150);
                 empty.HorizontalAlignment = 1;
                 ///tolt.BorderWidth = 1;
                 empty.BorderWidthBottom = 0;
                 empty.BorderWidthLeft = 0;
                 empty.BorderWidthRight = 0;
                 empty.BorderWidthTop = 1f;*/


                /* if (k == Tbla.Rows.Count-1)
                 {

                     cante.BorderWidthTop = 1;
                     descri.BorderWidthTop = 1;
                     vaco.BorderWidthTop = 1;
                     tolt.BorderWidthTop = 1;

                 }*/

                grantable.AddCell(cante);
                grantable.AddCell(descri);
                grantable.AddCell(vaco);
                grantable.AddCell(tolt);

                

                if (k > Tbla.Rows.Count)
                {
                   // grantable.AddCell(empty);
                    break;
                }
            }

                    //Agregar los rows!
                   

            //CANTIDAD
            PdfPCell cantidad = new PdfPCell(new Phrase(canttotal.ToString(), tablatitulo));
            cantidad.Colspan = 1;
            cantidad.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/


            PdfPTable subt = new PdfPTable(4);
            subt.WidthPercentage = 85f;
            subt.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            float[] widths2 = new float[] { 70f, 0f, 15f, 15f };
            subt.SetWidths(widths2);
            

            PdfPCell clear = new PdfPCell(new Phrase(""));
            clear.Border = 0;
            clear.Colspan = 1;
            

            //SUBTOTAL
            PdfPCell subtotal = new PdfPCell(new Phrase("SUB - TOTAL:", tablatitulo));
            subtotal.Colspan = 1;
            subtotal.HorizontalAlignment = 0; //0=left, 1=center, 2=right*/
            subtotal.PaddingTop = 5;
            subtotal.PaddingBottom = 6;
            subtotal.PaddingLeft = 10;
            //subtotal.BackgroundColor = new iTextSharp.text.BaseColor(149, 160, 150);

            PdfPCell sub = new PdfPCell(new Phrase(txtSubtotal.Text, numero));
            sub.Colspan = 1;
            sub.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            sub.BackgroundColor = new iTextSharp.text.BaseColor(219, 229, 241);
            sub.PaddingTop = 5;
            sub.PaddingBottom = 6;
            sub.PaddingLeft = 10;


            subt.AddCell(clear);
            subt.AddCell(clear);
            subt.AddCell(subtotal);
            subt.AddCell(sub);

            PdfPTable ite = new PdfPTable(4);
            ite.WidthPercentage = 85f;
            ite.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            float[] widths3 = new float[] { 70f, 0f, 15f, 15f };
            ite.SetWidths(widths3);
            //ite.PaddingTop = 5;

            //ITBIS
            PdfPCell itbis = new PdfPCell(new Phrase("ITBIS: ", tablatitulo));
            itbis.Colspan = 1;
            itbis.HorizontalAlignment = 0; //0=left, 1=center, 2=right*/
            //itbis.BackgroundColor = new iTextSharp.text.BaseColor(149, 160, 150);
            itbis.PaddingTop = 5;
            itbis.PaddingBottom = 6;
            itbis.PaddingLeft = 10;


            PdfPCell it = new PdfPCell(new Phrase(txtItebis.Text, numero));
            it.Colspan = 1;
            it.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            it.BackgroundColor = new iTextSharp.text.BaseColor(219, 229, 241);
            it.PaddingTop = 5;
            it.PaddingBottom = 6;
            it.PaddingLeft = 10;

            ite.AddCell(clear);
            ite.AddCell(clear);
            ite.AddCell(itbis);
            ite.AddCell(it);


            PdfPTable tol = new PdfPTable(4);
            tol.WidthPercentage = 85f;
            tol.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            float[] widths4 = new float[] { 70f, 0f, 15f, 15f };
            tol.SetWidths(widths4);
            

            //TOTAL
            PdfPCell total = new PdfPCell(new Phrase("TOTAL: ", tablatitulo));
            total.Colspan = 1;
            total.HorizontalAlignment = 0; //0=left, 1=center, 2=right*/
            //total.BackgroundColor = new iTextSharp.text.BaseColor(149, 160, 150);
            total.PaddingTop = 5;
            total.PaddingBottom = 6;
            total.PaddingLeft = 10;

            PdfPCell to = new PdfPCell(new Phrase(txtTotal.Text, numero));
            to.Colspan = 1;
            to.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            to.BackgroundColor = new iTextSharp.text.BaseColor(219, 229, 241);
            to.PaddingTop = 5;
            to.PaddingBottom = 6;
            to.PaddingLeft = 10;


            tol.AddCell(clear);
            tol.AddCell(clear);
            tol.AddCell(total);
            tol.AddCell(to);

           /* grantable.AddCell(cantidad);
            grantable.AddCell(clear);
            grantable.AddCell(clear);
            grantable.AddCell(clear);*/


            //Aca se agrega todo al PDF. El orden es muy importante, so cuidado donde muevan los doc.add!
            doc.Add(espacio2);
            doc.Add(pdfTable);
            doc.Add(espacio2);
            doc.Add(grantable);
            doc.Add(espacio);
            doc.Add(subt);
            doc.Add(ite);
            doc.Add(tol);
            doc.Add(espacio);

            PdfPTable firm = new PdfPTable(2);
            firm.WidthPercentage = 85f;
            PdfPCell prefirma = new PdfPCell(new Phrase("Factura por: "));
            PdfPCell firma = new PdfPCell(new Phrase(txtFirma.Text, textfirma));
            float[] widths5 = new float[] { 15f,85f };
            firm.SetWidths(widths5);
            prefirma.Border = 0; firma.Border = 0;
            firm.AddCell(prefirma);firm.AddCell(firma);
            doc.Add(firm);



            doc.Close();

            MessageBox.Show("Factura Creada en Mis Documentos como: Factura " + cbNombre.Text + " " + date.ToString("dd-MM-yyyy") + ".pdf");
        }
        
    }
    
}
