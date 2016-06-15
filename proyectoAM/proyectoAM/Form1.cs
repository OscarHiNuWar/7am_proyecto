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
        double total; //Aca va el precio total de la suma de todos los precios del DataGridView
        double subtotal=0;
        double itbis = 0.18;
        double subitbis=0;
        double precio=0;
        int cantidad=0;
        string nombre; //Aca va el dato "nombre" de la tabla cliente
        string rnc; //Aca va el dato "RNC" de la tabla cliente
        string telefono;
        string email;
        string compania;
        string moneda;
        string trabajo;
        string pago;
        string vence; //Este dato puede variar a uno que funcione con DataTimePicker
        DataTable tabla;
        addFactura agrego = new addFactura();
        // 
        string user;
        int id=0;
        

        DataTable addColumns()
        {
            tabla = new DataTable();
            //tabla.Columns.Add("ID");
            //tabla.Columns.Add("Moneda");
            tabla.Columns.Add("Cantidad");
            tabla.Columns.Add("Descripcion");
            tabla.Columns.Add("");
            tabla.Columns.Add("Precio");
            //tabla.Columns.Add("Condiciones de Pago");
            // tabla.Columns.Add("Vence");


            /*tabla.Columns.Add("Descripcion");*/
            return tabla;
        }

        void addNombre()
        {
            cbNombre.Items.Add("Oscar");
            cbNombre.Items.Add("Jairo");
            cbNombre.Items.Add("Kelly");
            cbNombre.Items.Add("Daniel");
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
            cbMoneda.Items.Add("RD$");
            cbMoneda.Items.Add("$");
        }

        void reset()
        {
            txtPrecio.Text = null;
            //btnAgregar.Enabled = false;
        }

        public Form1()
        {
            InitializeComponent();
            btnEliminar.Enabled = false;
            //btnExportar.Enabled = false;
            //DataTable tabla = agrego.muestra();
            //Programc pdf = new Programc();
            Tbla.DataSource = addColumns();
            addNombre();
            addCompania();
            addTrabajo();
            addCondicionPago();
            addMoneda();
            user = Environment.UserName.ToString();
            //Tbla.Columns[0].Visible = false;
        }



        private void btnAgregar_Click(object sender, EventArgs e)
        {
            /* if (agrego.agregar(new string[] { nombre, rnc, telefono,email,compania,moneda,trabajo,pago,vence }))
             {
                 MessageBox.Show("Factura Guardada");
             }*/
            btnExportar.Enabled = true;
            id = id + 1;
            
            cantidad = Convert.ToInt32(nudCantidad.Text.ToString());
            precio = double.Parse(txtPrecio.Text);

            
            subtotal = (precio * cantidad) + subtotal;
            subitbis = (subtotal * itbis) + subitbis;
            total = (subtotal + subitbis);
            

            txtSubtotal.Text = Convert.ToString(subtotal);
            txtItebis.Text = Convert.ToString(subitbis);
            txtTotal.Text = Convert.ToString(total);
            moneda = cbMoneda.Text.ToString();
            trabajo = cbTrabajo.SelectedItem.ToString();
            pago = cbPago.SelectedItem.ToString();
            vence = dtVence.Value.ToString().Remove(8);

            tabla.Rows.Add(  Convert.ToString(cantidad),  cbTrabajo.Text.ToString(), Convert.ToString(precio));

            reset();

        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            btnAgregar.Enabled = true;
        }



        private void button1_Click(object sender, EventArgs e)
        {
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

            btnEliminar.Visible = true;
            btnEliminar.Enabled = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Estas seguro de eliminar este Dia feriado", "Eliminar Feriado", MessageBoxButtons.YesNo) == DialogResult.Yes)
            { 
                foreach (DataGridViewRow item in this.Tbla.SelectedRows)
            {
                Tbla.Rows.RemoveAt(item.Index);
            }
            }
        }


        /* private void Tbla_DoubleClick(object sender, EventArgs e)
         {
             string id = Tbla.CurrentRow.Cells[0].Value.ToString();

             btnExportar.Enabled = true;
         }*/
    }
    
}
