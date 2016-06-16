using System;
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
        string compania;
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
            //Parte Superior de la Tabla
            
           
            //Parte Inferior de la Tabla
            tabla.Columns.Add("Cantidad");
            tabla.Columns.Add("Descripcion");
            tabla.Columns.Add("");
            tabla.Columns.Add("Precio");
 
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
            //con.conectame();
            Tbla.DataSource = addColumns();
            addNombre();
            addCompania();
            addTrabajo();
            addCondicionPago();
            addMoneda();
            addDescripcion();
            user = Environment.UserName.ToString();
            DateTime date2 = DateTime.Now; // will give the date for today
            dtVence.Value = date2;
            numnfc += 1;
            txtNCF.Text = codenfc + Convert.ToString(numnfc);
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
                    txtSubtotal.Text = "RD$ " + Convert.ToString(subtotal) + ".00";
                    txtItebis.Text = "RD$ " + Convert.ToString(subitbis) + ".00";
                    txtTotal.Text = "RD$ " + Convert.ToString(total) + ".00";
                }
                else
                {
                    din = "    $ ";
                    txtSubtotal.Text = "$ " + Convert.ToString(subtotal) + ".00";
                    txtItebis.Text = "$ " + Convert.ToString(subitbis) + ".00";
                    txtTotal.Text = "$ " + Convert.ToString(total) + ".00";
                }
            
            moneda = cbMoneda.Text.ToString();
            trabajo = cbTrabajo.SelectedItem.ToString();
            pago = cbPago.SelectedItem.ToString();
            vence = dtVence.Value.ToString().Remove(8);

            tabla.Rows.Add(Convert.ToString(cantidad), cbDescripcion.Text.ToString(), "", din+Convert.ToString(precio) + ".00");
            canttotal = canttotal + Convert.ToInt32(nudCantidad.Text.ToString());
            reset();
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnAgregar.Enabled = true;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            exporta();
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
            var date = DateTime.Now; // will give the date for today
            string dateWithFormat = Convert.ToString(date.Date);
            Document doc = new Document(iTextSharp.text.PageSize.A4, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("C:\\Users\\" + user + " /Documents/Factura " +cbNombre.Text +" "+ date.ToString("dd-MM-yyyy") + ".pdf", FileMode.Create));
            
            doc.Open();

            iTextSharp.text.Image am = iTextSharp.text.Image.GetInstance("logo7am.jpg");
            am.ScalePercent(25f);
            am.SetAbsolutePosition(doc.PageSize.Width - 36f - 72f, doc.PageSize.Height - 36f - 50f);
            doc.Add(am);

            /*if(string.IsNullOrEmpty(cbNombre.Text) ) { Paragraph parag = new Paragraph("FACTURA\nNOMBRE: " + cbNombre.SelectedItem.ToString() + "\n"); doc.Add(parag); }
            else {  Paragraph parag = new Paragraph("FACTURA\n\nNOMBRE: " + cbNombre.Text.ToString() + "\n\n"); doc.Add(parag); }*/


            Paragraph factura = new Paragraph(" FACTURA\n\n"); doc.Add(factura);
            if (string.IsNullOrEmpty(cbCompania.Text)) { Paragraph parag = new Paragraph(" COMPAÑIA: " + cbNombre.SelectedItem.ToString() + "\n\n"); doc.Add(parag); }
            else { Paragraph parag = new Paragraph(" COMPAÑIA: " + cbNombre.Text.ToString() + "\n\n"); doc.Add(parag); }
            Paragraph RNC = new Paragraph(" RNC: " + txtRnc.Text + "\n\n"); doc.Add(RNC);
           /*Paragraph NFC = new Paragraph("NFC: " + txtNCF.Text + "\n\n"); doc.Add(NFC);
            Paragraph tel = new Paragraph("TELÉFONO: " + txtTelefono.Text + "\n\n"); doc.Add(tel);*/
            

            PdfPTable adjust = new PdfPTable(2);
            //adjust.HorizontalAlignment = 1;
            adjust.WidthPercentage = 100f;


            PdfPCell rnce = new PdfPCell( new Phrase("NFC: " + txtNCF.Text + "\n\n\n"));
            rnce.HorizontalAlignment = 0;
            rnce.Colspan = 1;
            rnce.Border = 0;

            var now = DateTime.Now;
            var newdate = now.Date;
            PdfPCell fechahoy = new PdfPCell(new Phrase("Fecha: " + newdate.ToString("dd/MM/yyyy") + "\n"));
            fechahoy.Colspan = 1;
            fechahoy.HorizontalAlignment = 2;
            fechahoy.Border = 0;

            PdfPCell telefono = new PdfPCell(new Phrase("TELEFONO: " + txtTelefono.Text + "\n\n\n"));
            telefono.HorizontalAlignment = 0;
            telefono.Colspan = 1;
            telefono.Border = 0;

            PdfPCell lugar = new PdfPCell(new Phrase("SANTO DOMINGO,\nREPUBLICA DOMINICANA\n"));
            lugar.Colspan = 1;
            lugar.HorizontalAlignment = 2;
            lugar.Border = 0;

            adjust.AddCell(rnce);
            adjust.AddCell(fechahoy);

            adjust.AddCell(telefono);
            adjust.AddCell(lugar);


            doc.Add(adjust);

            Paragraph email = new Paragraph(" EMAIL: " + txtEmail.Text + "\n\n"); doc.Add(email);


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
            var midato = dtVence.Value.ToString("dd/MM/yyyy");
            PdfPCell cvence = new PdfPCell(new Phrase(midato, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.NORMAL)));
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
            float[] widths = new float[] { 15f, 45f, 15f, 25f};
            grantable.SetWidths(widths);
            grantable.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/

            //Add the headers from the DataGridView to the table
            /* for (int j=0;j<Tbla.Columns.Count; j++)
             {
                 grantable.AddCell(new Phrase(Tbla.Columns[j].HeaderText));
             }*/


            //CANTIDDAD
            PdfPCell cant = new PdfPCell(new Phrase("CANTIDAD", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.BOLD)));
            cant.Colspan = 1;
            cant.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            grantable.AddCell(cant);

            //DESCRIPCION
            PdfPCell descripcion = new PdfPCell(new Phrase("DESCRIPCIÒN", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.BOLD)));
            descripcion.Colspan = 1;
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
            PdfPCell precio = new PdfPCell(new Phrase("PRECIO", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.BOLD)));
            precio.BackgroundColor = new iTextSharp.text.BaseColor(149, 160, 150);
            precio.Colspan = 1;
            precio.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            grantable.AddCell(precio);

            //Flag the first row as a header
            grantable.HeaderRows = 1;


            

            for (int k = 0; k < Tbla.Rows.Count; k++)
            {

                PdfPCell cante = new PdfPCell(new Phrase(Tbla[0, k].Value.ToString()));
                cante.HorizontalAlignment = 1;
                PdfPCell descri = new PdfPCell(new Phrase(Tbla[1, k].Value.ToString()));
                descri.HorizontalAlignment = 1;

                PdfPCell vaco =new PdfPCell (new Phrase(Tbla[2, k].Value.ToString()));

                PdfPCell tolt = new PdfPCell(new Phrase(Tbla[3, k].Value.ToString()));
                tolt.BackgroundColor = new iTextSharp.text.BaseColor(149, 160, 150);
                tolt.HorizontalAlignment = 1;

                grantable.AddCell(cante);
                grantable.AddCell(descri);
                grantable.AddCell(vaco);
                grantable.AddCell(tolt);

                if (k > Tbla.Rows.Count)
                {
                    
                    break;
                }
            }

                    //Agregar los rows!
                    /*for (int i = 0; i < Tbla.Rows.Count; i++)
            {
                for (int k = 0; k < Tbla.Columns.Count; k++)
                {
                    

                    if (Tbla[k, i].Value != null)
                    {
                        
                        grantable.AddCell(new Phrase(Tbla[k, i].Value.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.NORMAL)));
                       
                    }

                }
            }
*/
           
            


            //CANTIDAD
            PdfPCell cantidad = new PdfPCell(new Phrase(canttotal.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.BOLD)));
            cantidad.Colspan = 1;
            cantidad.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/


            PdfPTable subt = new PdfPTable(4);
            subt.WidthPercentage = 100f;
            subt.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            
            PdfPCell clear = new PdfPCell(new Phrase(""));
            clear.Border = 0;
            clear.Colspan = 2;
            


            //SUBTOTAL
            PdfPCell subtotal = new PdfPCell(new Phrase("SUB - TOTAL:", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.BOLD)));
            subtotal.Colspan = 1;
            subtotal.HorizontalAlignment = 0; //0=left, 1=center, 2=right*/
            //subtotal.BackgroundColor = new iTextSharp.text.BaseColor(149, 160, 150);
            
            PdfPCell sub = new PdfPCell(new Phrase(txtSubtotal.Text, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.NORMAL)));
            sub.Colspan = 1;
            sub.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            sub.BackgroundColor = new iTextSharp.text.BaseColor(149, 160, 150);

           
            subt.AddCell(clear);
            subt.AddCell(subtotal);
            subt.AddCell(sub);

            PdfPTable ite = new PdfPTable(4);
            ite.WidthPercentage = 100f;
            ite.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/

            //ITBIS
            PdfPCell itbis = new PdfPCell(new Phrase("ITBIS: ", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.BOLD)));
            itbis.Colspan = 1;
            itbis.HorizontalAlignment = 0; //0=left, 1=center, 2=right*/
            //itbis.BackgroundColor = new iTextSharp.text.BaseColor(149, 160, 150);
           
            PdfPCell it = new PdfPCell(new Phrase(txtItebis.Text, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.NORMAL)));
            it.Colspan = 1;
            it.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            it.BackgroundColor = new iTextSharp.text.BaseColor(149, 160, 150);

            ite.AddCell(clear);
            ite.AddCell(itbis);
            ite.AddCell(it);

            


            PdfPTable tol = new PdfPTable(4);
            tol.WidthPercentage = 100f;
            tol.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/

            //TOTAL
            PdfPCell total = new PdfPCell(new Phrase("TOTAL: ", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.BOLD)));
            total.Colspan = 1;
            total.HorizontalAlignment = 0; //0=left, 1=center, 2=right*/
            //total.BackgroundColor = new iTextSharp.text.BaseColor(149, 160, 150);
            
            PdfPCell to = new PdfPCell(new Phrase(txtTotal.Text, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.NORMAL)));
            to.Colspan = 1;
            to.HorizontalAlignment = 1; //0=left, 1=center, 2=right*/
            to.BackgroundColor = new iTextSharp.text.BaseColor(149, 160, 150);

            
            tol.AddCell(clear);
            tol.AddCell(total);
            tol.AddCell(to);

            grantable.AddCell(cantidad);
            grantable.AddCell(clear);
            grantable.AddCell(clear);
            grantable.AddCell(clear);

            Paragraph espacio = new Paragraph("\n\n"); doc.Add(espacio);
 

            //Aca se agrega todo al PDF. El orden es muy importante, so cuidado donde muevan los doc.add!
            doc.Add(pdfTable);
            doc.Add(espacio);
            doc.Add(grantable);
            doc.Add(espacio);
            doc.Add(subt);
            doc.Add(ite);
            doc.Add(tol);
            doc.Add(espacio);

            Paragraph firma = new Paragraph("Factura por: " + txtFirma.Text + "                                                                                             ", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.UNDERLINE)); doc.Add(firma);


            doc.Close();

            MessageBox.Show("Factura Creada en Mis Documentos como: Factura " + cbNombre.Text + " " + date.ToString("dd-MM-yyyy") + ".pdf");
        }
        
    }
    
}
