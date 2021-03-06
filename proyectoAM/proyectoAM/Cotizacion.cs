﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using proyectoAM.clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyectoAM
{
    public partial class Cotizacion : Form
    {
        //facturacion con = new facturacion();
        double total; //Aca va el precio total de la suma de todos los precios del DataGridView
        double subtotal = 0;
        double itbis = 0.18;
        double subitbis = 0;
        double precio = 0;
        int cantidad = 0;
        int canttotal = 0;
        string nombre; //Aca va el dato "nombre" de la tabla cliente
       
       
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
        int id = 0;
        addCliente cli = new addCliente();
        MySqlDataReader reader;
        MySqlConnection cn;
        MySqlCommand cmd;
        addFactura fact = new addFactura();
        addItem itm = new addItem();
        double esub, eitbis, pretotal;
        string din, dat;
        DateTime date2;

       

        public void conecta() { cn = conDB.conecta(); cn.Open(); }

        DataTable addColumns()
        {
            tabla = new DataTable();
            //Parte Inferior de la Tabla
            tabla.Columns.Add("Cantidad");
            tabla.Columns.Add("Descripcion");
            tabla.Columns.Add("");
            tabla.Columns.Add("Precio");
            tabla.Columns.Add("Precio-SinMoneda");

            return tabla;
        }

        void addNombre()
        {
            try
            {
                conecta();
                string sql = "SELECT nombre FROM cliente";
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cbNombre.Items.Add(reader.GetString("nombre"));
                }
                cn.Close();
            }
            catch { }
        }

  
        void addCondicionPago()
        {
           /* cbPago.Items.Add("Efectivo");
            cbPago.Items.Add("Cheque");*/
        }

        void addMoneda()
        {
            cbMoneda.Items.Add("RD$");
            cbMoneda.Items.Add("US$");
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
            txtidcli.Text = "";
            //btnAgregar.Enabled = false;
        }


        public Cotizacion()
        {
            InitializeComponent();
            btnEliminar.Enabled = false;
            Tbla.DataSource = addColumns();
            addNombre();
            addCondicionPago();
            addMoneda();
            addDescripcion();
            user = Environment.UserName.ToString();
            DateTime date2 = DateTime.Now; // will give the date for today
            dtVence.Value = date2;
            Tbla.Columns[4].Visible = false;
           // dat = string.Format("{0:dd/M/yyyy}", date2);
        }




        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtPrecio.Text == "") { MessageBox.Show("Favor de poner un precio"); }
            else if (cbNombre.Text == "") { MessageBox.Show("Favor de poner un cliente"); }
            else if (cbMoneda.Text == "") { MessageBox.Show("Favor de poner una moneda"); }
            
            //else if (cbPago.Text == "") { MessageBox.Show("Favor de poner una condicion de pago"); }
            else if (cbDescripcion.Text == "") { MessageBox.Show("Favor de poner un articulo"); }

            else
            {

                if (cbNombre.SelectedItem == "MONEDA" || cbNombre.SelectedItem == null)
                {
                    nombre = cbNombre.Text;
                }
                else { nombre = cbNombre.SelectedItem.ToString(); }
                
                string rnc = txtRnc.Text;

                vence = dtVence.Value.ToString().Remove(8);
               


                btnExportar.Enabled = true;
                id = id + 1;

                cantidad = Convert.ToInt32(nudCantidad.Text.ToString());
                precio = double.Parse(txtPrecio.Text);


                subtotal = (precio * cantidad) + subtotal;
                subitbis = (subtotal * itbis);
                total = (subtotal + subitbis);
                
                if (cbMoneda.Text == "RD$")
                {
                    din = "RD$";
                    // txtSubtotal.Text = "RD$ " + Convert.ToString(subtotal) + ".00";
                    txtSubtotal.Text = Convert.ToString(string.Format("{0:N}", subtotal));
                    txtItebis.Text = Convert.ToString(string.Format("{0:N}", subitbis));
                    txtTotal.Text = Convert.ToString(string.Format("{0:N}", total));
                }
                else
                {
                    din = "US$";
                    txtSubtotal.Text = Convert.ToString(string.Format("{0:N}", subtotal));
                    txtItebis.Text = Convert.ToString(string.Format("{0:N}", subitbis));
                    txtTotal.Text = Convert.ToString(string.Format("{0:N}", total));
                }

                moneda = cbMoneda.Text.ToString();
                trabajo = "--";
                //pago = cbPago.SelectedItem.ToString();
                vence = dtVence.Value.ToString("dd/M/yy");

                tabla.Rows.Add(Convert.ToString(cantidad), cbDescripcion.Text.ToString(), din, /*Convert.ToString(string.Format("{0:C}", precio)),*/ precio.ToString("N2"), precio);
                canttotal = canttotal + Convert.ToInt32(nudCantidad.Text.ToString());
                
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Estas seguro de eliminar esta Tabla", "Eliminar Feriado", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (DataGridViewRow item in this.Tbla.SelectedRows)
                {
                    Tbla.Rows.RemoveAt(item.Index);
                }
                eliminacant();
            }
            btnEliminar.Enabled = false;

        }

        public void guarda()
        {
            try
            {
                conecta();
            }
            catch { }


            foreach (DataGridViewRow rows in Tbla.Rows)
            {
                itm.agregarItems(new string[] { txtidcli.Text, rows.Cells[0].Value.ToString(), rows.Cells[1].Value.ToString(), rows.Cells[3].Value.ToString(), vence });
            }

            if (fact.agregarCotizacion(new string[] { txtidcli.Text, "", vence }))
            {
                // MessageBox.Show("Agregado a Base de datos");
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (Tbla.RowCount == 0) { MessageBox.Show("La tabla esta vacia. Por favor agregar datos."); }
            else
            {
                exporta();
                if (MessageBox.Show("¿Desea guardar esta Factura?", "Save", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    guarda();
                    MessageBox.Show("Factura Guardada.");
                }
                reset();
            }
            //exporta();
        }

        public void eliminacant()
        {


            esub = Convert.ToInt32(subtotal) - esub;
            subtotal = esub;
            subitbis = subitbis - eitbis;
            // subitbis = (subtotal * 0.18) + esub;
            total = total - pretotal;
            // pretotal = total - pretotal;
            txtSubtotal.Text = din + esub.ToString();
            txtItebis.Text = din + subitbis.ToString();
            txtTotal.Text = din + total.ToString();
        }


        public void exporta()
        {

            Paragraph espacio = new Paragraph("\n\n");
            Paragraph espacio2 = new Paragraph("\n");

            var color = new BaseColor(0, 0, 0);


            //EL FONT!
            BaseFont trebutchet = BaseFont.CreateFont("Resources/Font/Trebuchet_MS.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            BaseFont trebutchetbold = BaseFont.CreateFont("Resources/Font/Trebuchet_MS_Bold.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            //Letra de Titulo
            iTextSharp.text.Font titulo = new iTextSharp.text.Font(trebutchetbold, 16, iTextSharp.text.Font.BOLD);

            //Letra del texto general
            iTextSharp.text.Font texto = new iTextSharp.text.Font(trebutchet, 8, iTextSharp.text.Font.NORMAL);

            //Letra del texto general negrita
            iTextSharp.text.Font texton = new iTextSharp.text.Font(trebutchet, 8, iTextSharp.text.Font.BOLD);

            //Letra del texto Cliente
            iTextSharp.text.Font cliente = new iTextSharp.text.Font(trebutchetbold, 11, iTextSharp.text.Font.BOLD);

            //Letra del texto Totales
            iTextSharp.text.Font numero = new iTextSharp.text.Font(trebutchet, 9, iTextSharp.text.Font.NORMAL);

            //Letra del texto tablatitulo
            iTextSharp.text.Font tablatitulo = new iTextSharp.text.Font(trebutchetbold, 7, iTextSharp.text.Font.BOLD);

            //Letra del texto Firma
            iTextSharp.text.Font textfirma = new iTextSharp.text.Font(trebutchet, 11, iTextSharp.text.Font.NORMAL);

            //Letra del texto Nota
            iTextSharp.text.Font notafinal = new iTextSharp.text.Font(trebutchet, 9, iTextSharp.text.Font.NORMAL);


            var date = DateTime.Now; // will give the date for today
            string dateWithFormat = Convert.ToString(date.Date);
            Document doc = new Document(iTextSharp.text.PageSize.A4, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("C:\\Users\\" + user + " /Documents/Cotización " + cbNombre.Text + " " + date.ToString("dd-MM-yyyy") + ".pdf", FileMode.Create));

            doc.Open();

            iTextSharp.text.Image am = iTextSharp.text.Image.GetInstance("Resources/logo7am2.jpg");
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

            PdfPCell factura = new PdfPCell(new Phrase("Cotización\n\n", titulo));
            factura.HorizontalAlignment = 0;
            factura.Colspan = 1;
            factura.Border = 0;

           /* txtRnc.Font = new System.Drawing.Font(txtRnc.Font, FontStyle.Regular);
            PdfPCell rnc = new PdfPCell(new Phrase("RNC: " + txtRnc.Text + "\n\n", texton));

            rnc.HorizontalAlignment = 0;
            rnc.Colspan = 1;
            rnc.Border = 0;*/

            PdfPCell empty = new PdfPCell(new Phrase("\n"));
            empty.Colspan = 1;
            empty.Border = 0;

           /* PdfPCell rnce = new PdfPCell(new Phrase("NFC: " + txtNCF.Text + "\n\n", texton));
            rnce.HorizontalAlignment = 0;
            rnce.Colspan = 1;
            rnce.Border = 0;*/

            var now = DateTime.Now;
            var newdate = now.Date;
            PdfPCell fechahoy = new PdfPCell(new Phrase("Fecha: " + newdate.ToString("dd/MM/yyyy") + "\n\n", texton));
            fechahoy.Colspan = 1;
            fechahoy.HorizontalAlignment = 2;
            fechahoy.Border = 0;

            PdfPCell telefono = new PdfPCell(new Phrase("TELEFONO: \n 809 - 535 - 1613\n\n", texton));
            telefono.HorizontalAlignment = 0;
            telefono.Colspan = 1;
            telefono.Border = 0;

            PdfPCell lugar = new PdfPCell(new Phrase("SANTO DOMINGO,\nREPUBLICA DOMINICANA\n", texton));
            lugar.Colspan = 1;
            lugar.HorizontalAlignment = 2;
            lugar.Border = 0;

            adjust.AddCell(empty); adjust.AddCell(empty);
            adjust.AddCell(empty); adjust.AddCell(empty);
            adjust.AddCell(factura); adjust.AddCell(empty);
            /*adjust.AddCell(rnc); adjust.AddCell(empty);
            adjust.AddCell(rnce);*/
            adjust.AddCell(telefono);  adjust.AddCell(empty);
            /*adjust.AddCell(empty); adjust.AddCell(empty);
            adjust.AddCell(empty); adjust.AddCell(empty); */
             


            PdfPCell email = new PdfPCell(new Phrase("EMAIL:\n info@agencia7am.com \n\n", texton)); email.Border = 0; email.HorizontalAlignment = 0; email.Colspan = 1; adjust.AddCell(email); adjust.AddCell(empty);  
            if (string.IsNullOrEmpty(cbNombre.Text)) { PdfPCell parag = new PdfPCell(new Phrase(("CLIENTE: " + cbNombre.SelectedItem.ToString() + " " + txtbuscar.Text), cliente)); parag.Border = 0; parag.HorizontalAlignment = 0; parag.Colspan = 1; adjust.AddCell(parag); }
            else { PdfPCell parag = new PdfPCell(new Phrase("CLIENTE: " + cbNombre.Text.ToString() + " " + txtbuscar.Text, cliente)); parag.Border = 0; parag.HorizontalAlignment = 0; parag.Colspan = 1; adjust.AddCell(parag); }
            adjust.AddCell(fechahoy);
            doc.Add(adjust);

            PdfPTable pdfTable = new PdfPTable(5);
            pdfTable.HorizontalAlignment = 1;
            float[] widthsa = new float[] { 25f, 25f, 25f, 25f, 0f };
            pdfTable.SetWidths(widthsa);
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
            PdfPCell ctrabajo = new PdfPCell(new Phrase("--", texto));
            ctrabajo.Colspan = 1;
            ctrabajo.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            ctrabajo.Padding = 5;


            //ACA VA CONDICIONES
            PdfPCell condicion = new PdfPCell(new Phrase("CONDICIONES DE PAGO", tablatitulo));
            condicion.Colspan = 1;
            condicion.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            condicion.Padding = 5;
            PdfPCell cpago = new PdfPCell(new Phrase("", texto));
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
            pdfTable.AddCell("");
            pdfTable.AddCell(cmoneda);
            pdfTable.AddCell(ctrabajo);
            pdfTable.AddCell(cpago);
            pdfTable.AddCell(cvence);
            pdfTable.AddCell("");

            //GRAN TABLA!!!
            PdfPTable grantable = new PdfPTable(Tbla.Columns.Count);
            grantable.WidthPercentage = 85f;
            float[] widths = new float[] { 10f, 54f, 20f, 16f, 0f };
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
            PdfPCell descripcion = new PdfPCell(new Phrase("DESCRIPCIÓN", tablatitulo));
            descripcion.Colspan = 1;
            descripcion.Padding = 5;

            descripcion.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
                                                 //descripcion.BorderWidth = 1f;
                                                 //descripcion.BorderWidthRight = 0;

            grantable.AddCell(descripcion);

            //VACIO
            var FontColour = new BaseColor(255, 255, 255);
            var MyFont = FontFactory.GetFont("Times New Roman", 11, FontColour);
            PdfPCell vacio = new PdfPCell(new Phrase("B", MyFont));
            vacio.Colspan = 1;
            // vacio.BorderWidthLeft = 0;
            vacio.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            grantable.AddCell(vacio);

            //PRECIO
            PdfPCell precio = new PdfPCell(new Phrase("PRECIO", tablatitulo));
            precio.BackgroundColor = new iTextSharp.text.BaseColor(219, 229, 241);
            precio.Colspan = 1;
            precio.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            precio.Padding = 5;
            grantable.AddCell(precio);

            PdfPCell va = new PdfPCell(new Phrase(""));
            grantable.AddCell(va);

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

                PdfPCell vaco = new PdfPCell(new Phrase(""));
                //vaco.BorderWidth = 1f;
                // vaco.BorderWidthBottom = 0;


                PdfPCell tolt = new PdfPCell(new Phrase(Tbla[2,k].Value.ToString() + Tbla[3, k].Value.ToString(), texto));
                tolt.BackgroundColor = new iTextSharp.text.BaseColor(219, 229, 241);
                tolt.HorizontalAlignment = 0;
                tolt.PaddingTop = 5;
                tolt.PaddingBottom = 8;
                tolt.PaddingLeft = 20;

                PdfPCell v = new PdfPCell(new Phrase(""));

                grantable.AddCell(cante);
                grantable.AddCell(descri);
                grantable.AddCell(vaco);
                grantable.AddCell(tolt);
                grantable.AddCell(v);


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
            float[] widths2 = new float[] { 70f, 0f, 23f, 17f };
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



            PdfPCell sub = new PdfPCell(new Phrase(cbMoneda.Text + txtSubtotal.Text, numero));
            sub.Colspan = 1;
            sub.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            sub.BackgroundColor = new iTextSharp.text.BaseColor(219, 229, 241);
            sub.PaddingTop = 5;
            sub.PaddingBottom = 6;
            sub.PaddingLeft = 6;


            subt.AddCell(clear);
            subt.AddCell(clear);
            subt.AddCell(subtotal);
            subt.AddCell(sub);

            PdfPTable ite = new PdfPTable(4);
            ite.WidthPercentage = 85f;
            ite.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            float[] widths3 = new float[] { 70f, 0f, 23f, 17f };
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


            PdfPCell it = new PdfPCell(new Phrase(cbMoneda.Text + txtItebis.Text, numero));
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
            float[] widths4 = new float[] { 70f, 0f, 23f, 17f };
            tol.SetWidths(widths4);


            //TOTAL
            PdfPCell total = new PdfPCell(new Phrase("TOTAL: ", tablatitulo));
            total.Colspan = 1;
            total.HorizontalAlignment = 0; //0=left, 1=center, 2=right*/
            //total.BackgroundColor = new iTextSharp.text.BaseColor(149, 160, 150);
            total.PaddingTop = 5;
            total.PaddingBottom = 6;
            total.PaddingLeft = 10;

            PdfPCell to = new PdfPCell(new Phrase(cbMoneda.Text + txtTotal.Text, numero));
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


            //Aca se agrega todo al PDF. El orden es muy importante, so cuidado donde muevan los doc.add!
            doc.Add(espacio2);
            doc.Add(pdfTable);
            doc.Add(espacio2);
            doc.Add(grantable);
            doc.Add(espacio2);
            doc.Add(subt);
            doc.Add(ite);
            doc.Add(tol);
            doc.Add(espacio2);

            PdfPTable div = new PdfPTable(1);
            div.WidthPercentage = 85f;
            div.DefaultCell.Border = 0;

            PdfPCell linea = new PdfPCell(new Phrase(""));
            linea.Border = 0;
            linea.BorderWidthBottom = .5f;
            div.AddCell(linea);
            doc.Add(div);
            doc.Add(espacio2);

            PdfPTable firm = new PdfPTable(2);
            firm.WidthPercentage = 85f;
            firm.DefaultCell.BorderWidth = 0;
            PdfPCell prefirma = new PdfPCell(new Phrase("Firma por: "));
            PdfPCell firma = new PdfPCell(new Phrase("", textfirma));
            float[] widths5 = new float[] { 15f, 85f };
            firm.SetWidths(widths5);
            prefirma.Border = 0; firma.Border = 0; firma.BorderWidthBottom = .5f;
            firm.AddCell(prefirma); firm.AddCell(firma);

            

            doc.Add(firm);
            doc.Add(espacio2);
            
            doc.Add(espacio2);
            //doc.Add(end);






            doc.Close();

            MessageBox.Show("Cotización Creada en Mis Documentos como: Cotización " + cbNombre.Text + " " + date.ToString("dd-MM-yyyy") + ".pdf");
        }

        private void Tbla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEliminar.Visible = true;
            btnEliminar.Enabled = true;
            double preesub = double.Parse(Tbla.CurrentRow.Cells[3].Value.ToString());

            esub = preesub;

            eitbis = esub * 0.18;

            pretotal = esub + eitbis;
        }

        private void cbNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                nombre = cbNombre.Text;
                conecta();
                string sql = "SELECT id, rnc FROM cliente WHERE nombre='" + nombre + "';";
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    txtRnc.Text = (reader["rnc"].ToString());
                    txtidcli.Text = reader["id"].ToString();
                    //txtidcli.Text = sql;
                }
                cn.Close();
            }
            catch (MySqlException ex) { MessageBox.Show("Error en: " + ex); }
        }

        private void txtrnccom_TextChanged(object sender, EventArgs e)
        {
            buscar(txtbuscar.Text);
        }

        public void buscar(string nombre)
        {
            try
            {

                cn.Open();
                string sql = "SELECT nombre FROM `cliente` WHERE nombre like '%" + nombre + "%'";
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                reader.Read();
                cbNombre.Text = reader["nombre"].ToString();
                reader.Close();
                cn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error en: " + ex);
            }
        }
    }
}
