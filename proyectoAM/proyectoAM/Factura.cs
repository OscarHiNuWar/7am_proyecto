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
using MySql.Data.MySqlClient;

namespace proyectoAM
{
    public partial class Factura : Form
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
       // string codenfc = "A0100100115";
        int numnfc = 0;
        string nfc; //Aca va el dato "NFC" de la tabla cliente
        string rnc; //Aca va el dato "RNC" de la tabla cliente
        //string telefono = "809-535-1613";
        //string email = "info@agencia7am.com";
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
        addCliente cli = new addCliente();
        MySqlDataReader reader;
        MySqlConnection cn;
        MySqlCommand cmd;
        addFactura fact = new addFactura();
<<<<<<< HEAD
        comprobante com = new comprobante();
        addItem itm = new addItem();
        double esub, eitbis, pretotal;
        string din;
        int nf;
        int fin;
        string tipo, valo,ini; 
=======
        addItem itm = new addItem();
        double esub, eitbis, pretotal;

>>>>>>> origin/master



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
            /*  tabla.Columns.Add("Sub-Total");
             tabla.Columns.Add("ITBIS");
             tabla.Columns.Add("Total");*/

            return tabla;
        }

        void addNombre()
        {
            /*cbNombre.Items.Add("7AM");
            cbNombre.Items.Add("Coca-cola");
            cbNombre.Items.Add("Pepsi");
            cbNombre.Items.Add("Infotep");*/


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

        void addCompania()
        {
            /*cbCompania.Items.Add("7AM");
            cbCompania.Items.Add("Coca-cola");
            cbCompania.Items.Add("Pepsi");
            cbCompania.Items.Add("Infotep");*/
        }

        void addTrabajo()
        {
            /*cbTrabajo.Items.Add("Desarrollo de Pagina Web");
            cbTrabajo.Items.Add("Continuacion de Red Social");
            cbTrabajo.Items.Add("Posicionamiento");*/
            //cbTrabajo.Items.Add("Infotep");
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

        void addcompro()
        {
            cbcompro.Items.Add("Negocio");
            cbcompro.Items.Add("Gubernamental");
        }

        void reset()
        {
            txtPrecio.Text = null;
            nudCantidad.Value = 1;
            cbDescripcion.Text = null;
<<<<<<< HEAD
            
=======
            txtidcli.Text = "";
>>>>>>> origin/master
            //btnAgregar.Enabled = false;
        }

        public Factura()
        {
            InitializeComponent();
            btnEliminar.Enabled = false;
            //DataTable tabla = agrego.muestra();
            Tbla.DataSource = addColumns();
            addNombre();
            addCompania();
            addTrabajo();
            addcompro();
           // addCondicionPago();
            addMoneda();
            addDescripcion();
            user = Environment.UserName.ToString();
            DateTime date2 = DateTime.Now; // will give the date for today
            dtVence.Value = date2;
            numnfc += 1;
            //txtNCF.Text = codenfc + Convert.ToString(numnfc);
<<<<<<< HEAD
            Tbla.Columns[4].Visible = false;
            connfc();
            

=======
            
>>>>>>> origin/master
        }

        public void connfc()
        {
            try { 

            id = com.traerid();
            if(id == com.traerid())
            { id++;
            }
                tipo = com.traertipo();
            ini = com.traerinicio();
            valo = com.traervalor();
            txtNCF.Text = com.traeractual();
            nf = int.Parse(txtNCF.Text);
            fin = com.traermax();
            int prefin = fin - nf;
                nf++;
				
            if(fin >= prefin)
            {
                MessageBox.Show("Quedan "+prefin+" NFCs");
            }
            if(nf == fin || nf>fin)
            {
                MessageBox.Show("No quedan NFCs. No puede facturar.");
                btnAgregar.Enabled = false;
            }
            txtNCF.Text = com.traervalor()+(nf).ToString();
            } catch(Exception ex)
            {
                MessageBox.Show("Nesesita un comprobante.");
                btnAgregar.Visible = false;
            }
           

        }
		
		public void guberna(){
			try { 
            id = com.traerid();
            if(id == com.traerid())
            { id++;
            }
            ini = com.traerinicio();
            valo = com.traervalor();
            txtNCF.Text = com.traeractual();
            nf = int.Parse(txtNCF.Text);
            fin = com.traermax();
            int prefin = fin - nf;
                nf++;
				
            if(fin >= prefin)
            {
                MessageBox.Show("Quedan "+prefin+" NFCs");
            }
            if(nf == fin || nf>fin)
            {
                MessageBox.Show("No quedan NFCs. No puede facturar.");
                btnAgregar.Enabled = false;
            }
            txtNCF.Text = com.traervalor()+(nf).ToString();
            } catch(Exception ex)
            {
                MessageBox.Show("Nesesita un comprobante.");
                
            }
		
		}

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            if (txtPrecio.Text == "") { MessageBox.Show("Favor de poner un precio"); }
            else if (cbNombre.Text == "") { MessageBox.Show("Favor de poner un cliente"); }
            else if (cbMoneda.Text == "") { MessageBox.Show("Favor de poner una moneda"); }
            else if (cbMoneda.SelectedItem == null) { MessageBox.Show("Favor de poner una moneda"); }
            // else if (cbTrabajo.Text == "") { MessageBox.Show("Favor de poner un Trabajo"); }
<<<<<<< HEAD
           /* else if (cbPago.Text == "") { MessageBox.Show("Favor de poner una condicion de pago"); }
            else if (cbPago.Text == "Condición de Pago") { MessageBox.Show("Favor de poner una condicion de pago"); }*/
=======
            else if (cbPago.Text == "") { MessageBox.Show("Favor de poner una condicion de pago"); }
            else if (cbPago.Text == "Condición de Pago") { MessageBox.Show("Favor de poner una condicion de pago"); }
>>>>>>> origin/master
            else if (cbDescripcion.Text == "") { MessageBox.Show("Favor de poner un articulo"); }

            else {



                if (cbNombre.SelectedItem == "MONEDA" || cbNombre.SelectedItem == null) { 
                nombre = cbNombre.Text;
                } else {  nombre = cbNombre.SelectedItem.ToString();}
                nfc = txtNCF.Text;
                rnc = txtRnc.Text;
                
                vence = dtVence.Value.ToString().Remove(8);

                

               

            btnExportar.Enabled = true;
            id = id + 1;
            
            cantidad = Convert.ToInt32(nudCantidad.Text.ToString());
            precio = double.Parse(txtPrecio.Text);

            
            subtotal = (precio * cantidad) + subtotal;
           // subitbis = (subtotal * 0.18) + subitbis;
            subitbis = (subtotal * 0.18);
            total = (subtotal + subitbis);
                
<<<<<<< HEAD
               
                if (cbMoneda.Text == "RD$")
                {
                    din = "RD$";
                    // txtSubtotal.Text = "RD$ " + Convert.ToString(subtotal) + ".00";
                    txtSubtotal.Text =  Convert.ToString(string.Format("{0:N}", subtotal));
                    txtItebis.Text =  Convert.ToString(string.Format("{0:N}", subitbis));
                    txtTotal.Text =  Convert.ToString(string.Format("{0:N}", total));
                }
                else
                {
                    din = "US$";
                    txtSubtotal.Text =  Convert.ToString(string.Format("{0:N}", subtotal));
                    txtItebis.Text =  Convert.ToString(string.Format("{0:N}", subitbis));
                    txtTotal.Text =  Convert.ToString(string.Format("{0:N}", total));
=======
               string din;
                if (cbMoneda.Text == "Pesos Dominicanos")
                {
                    din = "RD$";
                    // txtSubtotal.Text = "RD$ " + Convert.ToString(subtotal) + ".00";
                    txtSubtotal.Text = "" + Convert.ToString(string.Format("{0:C}", subtotal));
                    txtItebis.Text = "" + Convert.ToString(string.Format("{0:C}", subitbis));
                    txtTotal.Text = "" + Convert.ToString(string.Format("{0:C}", total));
                }
                else
                {
                    din = "$";
                    txtSubtotal.Text = " " + Convert.ToString(string.Format("{0:C}", subtotal));
                    txtItebis.Text = " " + Convert.ToString(string.Format("{0:C}", subitbis));
                    txtTotal.Text = " " + Convert.ToString(string.Format("{0:C}", total));
>>>>>>> origin/master
                }
            
            moneda = cbMoneda.Text.ToString();
            trabajo = "--";
<<<<<<< HEAD
            //pago = cbPago.SelectedItem.ToString();
            

            tabla.Rows.Add(Convert.ToString(cantidad), cbDescripcion.Text.ToString(), din, Convert.ToString(string.Format("{0:C}", precio)), precio);
            canttotal = canttotal + Convert.ToInt32(nudCantidad.Text.ToString());
                reset();
=======
            pago = cbPago.SelectedItem.ToString();
            

            tabla.Rows.Add(Convert.ToString(cantidad), cbDescripcion.Text.ToString(), "", Convert.ToString(string.Format("{0:C}", precio)), precio);
            canttotal = canttotal + Convert.ToInt32(nudCantidad.Text.ToString());
                //reset();
>>>>>>> origin/master
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnAgregar.Enabled = true;
        }

        public void guarda()
        {
            try
            {
                conecta();
            }
            catch { }
<<<<<<< HEAD


            foreach (DataGridViewRow rows in Tbla.Rows)
            {
                itm.agregarItems(new string[] { txtidcli.Text, rows.Cells[0].Value.ToString(), rows.Cells[1].Value.ToString(), rows.Cells[3].Value.ToString(), vence });
            }
=======
>>>>>>> origin/master

            if (fact.agregarFactura(new string[] { txtidcli.Text, "", vence }))
            {
                // MessageBox.Show("Agregado a Base de datos");
            }
            com.agregarActu(new string[] { tipo, valo, ini, nf.ToString("D6"), fin.ToString("D6")});
        }

            foreach (DataGridViewRow rows in Tbla.Rows)
            {
                itm.agregarItems(new string[] { txtidcli.Text, rows.Cells[0].Value.ToString(), rows.Cells[1].Value.ToString(), rows.Cells[3].Value.ToString(), vence });
            }

            if (fact.agregarFactura(new string[] { txtidcli.Text, cbPago.SelectedItem.ToString(), vence }))
            {
                // MessageBox.Show("Agregado a Base de datos");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Tbla.RowCount == 0) { MessageBox.Show("La tabla esta vacia. Por favor agregar datos."); }
            else{ 
            exporta();
           if( MessageBox.Show("¿Desea guardar esta Factura?", "Save", MessageBoxButtons.YesNo) == DialogResult.Yes){
                guarda();
                MessageBox.Show("Factura Guardada.");
            }
            reset();
            }
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

        public void eliminacant()
        {
            

            esub = Convert.ToInt32(subtotal) - esub;
<<<<<<< HEAD
            subtotal = esub;
            subitbis = subitbis - eitbis;
            // subitbis = (subtotal * 0.18) + esub;
            total = total - pretotal;
           // pretotal = total - pretotal;
            txtSubtotal.Text =din+ esub.ToString();
            txtItebis.Text = din + subitbis.ToString();
            txtTotal.Text = din + total.ToString();
=======
            subtotal = esub - subtotal;
            subitbis = subitbis - eitbis;
            total = total - pretotal;
           // pretotal = total - pretotal;
            txtSubtotal.Text = esub.ToString();
            txtItebis.Text = eitbis.ToString();
            txtTotal.Text = pretotal.ToString();
>>>>>>> origin/master
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

        private void Tbla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
<<<<<<< HEAD
            if (Tbla.RowCount == 0) { }
            else { 
            btnEliminar.Visible = true;
             btnEliminar.Enabled = true;
=======
           /*  btnEliminar.Visible = true;
             btnEliminar.Enabled = true;*/
>>>>>>> origin/master
            double preesub = double.Parse(Tbla.CurrentRow.Cells[4].Value.ToString());

            

            esub = preesub;
            
            eitbis = esub * 0.18;

            pretotal = esub+eitbis;
<<<<<<< HEAD
            }
=======
>>>>>>> origin/master

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Factura_Load(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void nudCantidad_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtVence_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            buscar(textBox1.Text);
        }

<<<<<<< HEAD
        private void ckSinCompro_CheckedChanged(object sender, EventArgs e)
        {
            if(ckSinCompro.Checked == true)
            {
                txtNCF.Text = "";
                cbcompro.Items.Clear();
            }
        }

        private void cbcompro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbcompro.SelectedItem == "Negocio")
            {

            }
            else if(cbcompro.SelectedItem == "Negocio")
            {

            }
        }

=======
>>>>>>> origin/master
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
            iTextSharp.text.Font notafinal = new iTextSharp.text.Font(trebutchet, 9, iTextSharp.text.Font.BOLD);


            var date = DateTime.Now; // will give the date for today
            string dateWithFormat = Convert.ToString(date.Date);
            Document doc = new Document(iTextSharp.text.PageSize.A4, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("C:\\Users\\" + user + " /Documents/Factura " +cbNombre.Text +" "+ date.ToString("dd-MM-yyyy") + ".pdf", FileMode.Create));
            
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

            PdfPCell factura = new PdfPCell(new Phrase("\nFACTURA\n\n", titulo));
            factura.HorizontalAlignment = 0;
            factura.Colspan = 1;
            factura.Border = 0;

            txtRnc.Font = new System.Drawing.Font(txtRnc.Font, FontStyle.Regular);
            PdfPCell rnc = new PdfPCell(new Phrase("RNC: " + txtRnc.Text + "\n\n", texton));
            
            rnc.HorizontalAlignment =0;
            rnc.Colspan = 1;
            rnc.Border = 0;

            PdfPCell empty = new PdfPCell(new Phrase("\n"));
            empty.Colspan=1;
            empty.Border = 0;

            string ne = txtNCF.Text;
            
            PdfPCell rnce = new PdfPCell( new Phrase("NFC: " + valo + nf.ToString("D6") + "\n\n", texton));
            rnce.HorizontalAlignment = 0;
            rnce.Colspan = 1;
            rnce.Border = 0;

            var now = DateTime.Now;
            var newdate = now.Date;
            PdfPCell fechahoy = new PdfPCell(new Phrase("Fecha: " + newdate.ToString("dd/MM/yyyy") + "\n\n", texton));
            fechahoy.Colspan = 1;
            fechahoy.HorizontalAlignment = 2;
            fechahoy.Border = 0;

            PdfPCell telefono = new PdfPCell(new Phrase("TELEFONO: \n 809-535-1613\n\n", texton));
            telefono.HorizontalAlignment = 0;
            telefono.Colspan = 1;
            telefono.Border = 0;

            PdfPCell lugar = new PdfPCell(new Phrase("SANTO DOMINGO,\nREPUBLICA DOMINICANA\n", texton));
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

            
            PdfPCell email = new PdfPCell(new Phrase("EMAIL:\n info@agencia7am.com \n\n", texton)); email.Border = 0; email.HorizontalAlignment = 0; email.Colspan = 2; adjust.AddCell(email);
            if (string.IsNullOrEmpty(cbNombre.Text)) { PdfPCell parag = new PdfPCell(new Phrase( ("CLIENTE: "  + cbNombre.SelectedItem.ToString() + " " + txtRnc.Text), cliente)); parag.Border = 0; parag.HorizontalAlignment = 0; parag.Colspan = 2; adjust.AddCell(parag); }
            else { PdfPCell parag = new PdfPCell(new Phrase ("CLIENTE: " + cbNombre.Text.ToString() + " " +txtRnc.Text , cliente)); parag.Border = 0; parag.HorizontalAlignment = 0; parag.Colspan = 2; adjust.AddCell(parag); }
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
            float[] widths = new float[] { 10f, 54f, 20f, 16f, 0f};
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

                PdfPCell vaco =new PdfPCell (new Phrase(""));
                //vaco.BorderWidth = 1f;
               // vaco.BorderWidthBottom = 0;


                PdfPCell tolt = new PdfPCell(new Phrase(Tbla[3, k].Value.ToString(), texto));
                tolt.BackgroundColor = new iTextSharp.text.BaseColor(219, 229, 241);
                tolt.HorizontalAlignment = 0;
                tolt.PaddingTop = 5;
                tolt.PaddingBottom = 8;
                tolt.PaddingLeft=20;


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



            PdfPCell sub = new PdfPCell(new Phrase(din + txtSubtotal.Text, numero));
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


            PdfPCell it = new PdfPCell(new Phrase(din + txtItebis.Text, numero));
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

            PdfPCell to = new PdfPCell(new Phrase(din + txtTotal.Text, numero));
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

            PdfPCell linea = new PdfPCell(new Phrase(""));
            linea.Border = 0;
            //linea.BorderWidthBottom = .5f;
            //div.AddCell(linea);
            //doc.Add(div);
            

            PdfPTable firm = new PdfPTable(2);
            firm.WidthPercentage = 85f;
            PdfPCell prefirma = new PdfPCell(new Phrase("Facturado por: ", textfirma));
<<<<<<< HEAD
            PdfPCell firma = new PdfPCell(new Phrase("", textfirma));
=======
            PdfPCell firma = new PdfPCell(new Phrase("Madelyn", textfirma));
>>>>>>> origin/master
            float[] widths5 = new float[] { 20f,80f };
            firm.SetWidths(widths5);
            prefirma.Border = 0; firma.Border = 0; firma.BorderWidthBottom = .5f;
            firm.AddCell(prefirma);firm.AddCell(firma);


            PdfPTable clien = new PdfPTable(2);
            clien.WidthPercentage = 85f;
            float[] widths7 = new float[] { 21f, 79f };
            clien.SetWidths(widths7);
            PdfPCell preclient = new PdfPCell(new Phrase("Firma del Cliente: ", textfirma));
            preclient.HorizontalAlignment = 0;
            PdfPCell client = new PdfPCell(new Phrase("\n"));
            client.HorizontalAlignment = 0;
            preclient.Border = 0;
            
            client.Border = 0;
            client.BorderWidthBottom = .5f;

            PdfPTable end = new PdfPTable(1);
            end.WidthPercentage = 85f;
            PdfPCell nota = new PdfPCell(new Phrase("NOTA:\nFavor de realizar los pagos en cheques a nombre de 7AM AGENCIA MULTIMEDIA SRL.", notafinal));
            nota.Border = 0;


            //clien.AddCell(preclient); clien.AddCell(client);
            end.AddCell(nota);

            doc.Add(end);
            doc.Add(espacio2);
            doc.Add(firm);
            doc.Add(espacio2);
            //doc.Add(clien);
            //doc.Add(espacio2);

            doc.Close();

            MessageBox.Show("Factura Creada en Mis Documentos como: Factura " + cbNombre.Text + " " + date.ToString("dd-MM-yyyy") + ".pdf");
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
    }
    
}
