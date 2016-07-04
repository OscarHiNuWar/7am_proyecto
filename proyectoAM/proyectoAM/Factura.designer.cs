namespace proyectoAM
{
    partial class Factura
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbNombre = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRnc = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dtVence = new System.Windows.Forms.DateTimePicker();
            this.Tbla = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.cbMoneda = new System.Windows.Forms.ComboBox();
            this.btnExportar = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtItebis = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtNCF = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbDescripcion = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtsubsin = new System.Windows.Forms.TextBox();
            this.txtitbsin = new System.Windows.Forms.TextBox();
            this.txttolsin = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ckSinCompro = new System.Windows.Forms.CheckBox();
            this.cbcompro = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtidcli = new System.Windows.Forms.TextBox();
            this.txtTipoTrabajo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Tbla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbNombre
            // 
            this.cbNombre.FormattingEnabled = true;
            this.cbNombre.Location = new System.Drawing.Point(19, 22);
            this.cbNombre.Name = "cbNombre";
            this.cbNombre.Size = new System.Drawing.Size(152, 21);
            this.cbNombre.TabIndex = 1;
            this.cbNombre.Text = "CLIENTE";
            this.cbNombre.SelectedIndexChanged += new System.EventHandler(this.cbNombre_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(289, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "RNC";
            // 
            // txtRnc
            // 
            this.txtRnc.Location = new System.Drawing.Point(177, 23);
            this.txtRnc.Name = "txtRnc";
            this.txtRnc.ReadOnly = true;
            this.txtRnc.Size = new System.Drawing.Size(106, 20);
            this.txtRnc.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "VENCE";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // dtVence
            // 
            this.dtVence.Location = new System.Drawing.Point(13, 31);
            this.dtVence.Name = "dtVence";
            this.dtVence.Size = new System.Drawing.Size(201, 20);
            this.dtVence.TabIndex = 10;
            this.dtVence.Value = new System.DateTime(2016, 6, 9, 0, 0, 0, 0);
            this.dtVence.ValueChanged += new System.EventHandler(this.dtVence_ValueChanged);
            // 
            // Tbla
            // 
            this.Tbla.AllowUserToAddRows = false;
            this.Tbla.AllowUserToDeleteRows = false;
            this.Tbla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Tbla.DefaultCellStyle = dataGridViewCellStyle1;
            this.Tbla.Location = new System.Drawing.Point(13, 278);
            this.Tbla.Name = "Tbla";
            this.Tbla.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.Tbla.Size = new System.Drawing.Size(751, 179);
            this.Tbla.TabIndex = 18;
            this.Tbla.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tbla_CellContentDoubleClick);
            this.Tbla.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tbla_CellDoubleClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "SUB-TOTAL";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Location = new System.Drawing.Point(87, 28);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(128, 20);
            this.txtSubtotal.TabIndex = 20;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(6, 16);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 13;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(87, 16);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 22;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Visible = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(197, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "PRECIO";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(254, 47);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(126, 20);
            this.txtPrecio.TabIndex = 12;
            this.txtPrecio.TextChanged += new System.EventHandler(this.txtPrecio_TextChanged);
            this.txtPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecio_KeyPress_1);
            // 
            // cbMoneda
            // 
            this.cbMoneda.FormattingEnabled = true;
            this.cbMoneda.Location = new System.Drawing.Point(101, 46);
            this.cbMoneda.Name = "cbMoneda";
            this.cbMoneda.Size = new System.Drawing.Size(86, 21);
            this.cbMoneda.TabIndex = 7;
            this.cbMoneda.Tag = "";
            this.cbMoneda.Text = "MONEDA";
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(169, 16);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(75, 23);
            this.btnExportar.TabIndex = 14;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.button1_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(221, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "ITBIS";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(377, 31);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "TOTAL";
            // 
            // txtItebis
            // 
            this.txtItebis.Location = new System.Drawing.Point(262, 28);
            this.txtItebis.Name = "txtItebis";
            this.txtItebis.ReadOnly = true;
            this.txtItebis.Size = new System.Drawing.Size(109, 20);
            this.txtItebis.TabIndex = 29;
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(425, 28);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(110, 20);
            this.txtTotal.TabIndex = 30;
            // 
            // txtNCF
            // 
            this.txtNCF.Location = new System.Drawing.Point(101, 17);
            this.txtNCF.Name = "txtNCF";
            this.txtNCF.ReadOnly = true;
            this.txtNCF.Size = new System.Drawing.Size(149, 20);
            this.txtNCF.TabIndex = 2;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(416, 53);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 13);
            this.label15.TabIndex = 33;
            this.label15.Text = "QTY";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // nudCantidad
            // 
            this.nudCantidad.Location = new System.Drawing.Point(451, 51);
            this.nudCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(52, 20);
            this.nudCantidad.TabIndex = 13;
            this.nudCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCantidad.ValueChanged += new System.EventHandler(this.nudCantidad_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbNombre);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtRnc);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(752, 57);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(384, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(201, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(340, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Buscar:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbDescripcion);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.nudCantidad);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.cbMoneda);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtPrecio);
            this.groupBox2.Location = new System.Drawing.Point(12, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(521, 86);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            // 
            // cbDescripcion
            // 
            this.cbDescripcion.FormattingEnabled = true;
            this.cbDescripcion.Location = new System.Drawing.Point(101, 19);
            this.cbDescripcion.Name = "cbDescripcion";
            this.cbDescripcion.Size = new System.Drawing.Size(403, 21);
            this.cbDescripcion.TabIndex = 11;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(15, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(80, 13);
            this.label16.TabIndex = 34;
            this.label16.Text = "DESCRIPCIÒN";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtSubtotal);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtTotal);
            this.groupBox3.Controls.Add(this.txtItebis);
            this.groupBox3.Location = new System.Drawing.Point(16, 458);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(748, 71);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            // 
            // txtsubsin
            // 
            this.txtsubsin.Location = new System.Drawing.Point(15, 544);
            this.txtsubsin.Name = "txtsubsin";
            this.txtsubsin.Size = new System.Drawing.Size(100, 20);
            this.txtsubsin.TabIndex = 39;
            // 
            // txtitbsin
            // 
            this.txtitbsin.Location = new System.Drawing.Point(121, 544);
            this.txtitbsin.Name = "txtitbsin";
            this.txtitbsin.Size = new System.Drawing.Size(100, 20);
            this.txtitbsin.TabIndex = 40;
            // 
            // txttolsin
            // 
            this.txttolsin.Location = new System.Drawing.Point(227, 544);
            this.txttolsin.Name = "txttolsin";
            this.txttolsin.Size = new System.Drawing.Size(100, 20);
            this.txttolsin.TabIndex = 41;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.dtVence);
            this.groupBox4.Location = new System.Drawing.Point(539, 75);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(225, 85);
            this.groupBox4.TabIndex = 42;
            this.groupBox4.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtTipoTrabajo);
            this.groupBox5.Controls.Add(this.ckSinCompro);
            this.groupBox5.Controls.Add(this.cbcompro);
            this.groupBox5.Controls.Add(this.txtNCF);
            this.groupBox5.Location = new System.Drawing.Point(12, 172);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(752, 46);
            this.groupBox5.TabIndex = 43;
            this.groupBox5.TabStop = false;
            // 
            // ckSinCompro
            // 
            this.ckSinCompro.AutoSize = true;
            this.ckSinCompro.Location = new System.Drawing.Point(257, 19);
            this.ckSinCompro.Name = "ckSinCompro";
            this.ckSinCompro.Size = new System.Drawing.Size(107, 17);
            this.ckSinCompro.TabIndex = 33;
            this.ckSinCompro.Text = "Sin Comprobante";
            this.ckSinCompro.UseVisualStyleBackColor = true;
            this.ckSinCompro.CheckedChanged += new System.EventHandler(this.ckSinCompro_CheckedChanged);
            // 
            // cbcompro
            // 
            this.cbcompro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcompro.FormattingEnabled = true;
            this.cbcompro.Location = new System.Drawing.Point(18, 16);
            this.cbcompro.Name = "cbcompro";
            this.cbcompro.Size = new System.Drawing.Size(77, 21);
            this.cbcompro.TabIndex = 32;
            this.cbcompro.SelectedIndexChanged += new System.EventHandler(this.cbcompro_SelectedIndexChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnAgregar);
            this.groupBox6.Controls.Add(this.btnEliminar);
            this.groupBox6.Controls.Add(this.btnExportar);
            this.groupBox6.Location = new System.Drawing.Point(12, 224);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(752, 48);
            this.groupBox6.TabIndex = 44;
            this.groupBox6.TabStop = false;
            // 
            // txtidcli
            // 
            this.txtidcli.Location = new System.Drawing.Point(335, 544);
            this.txtidcli.Name = "txtidcli";
            this.txtidcli.Size = new System.Drawing.Size(100, 20);
            this.txtidcli.TabIndex = 45;
            // 
            // txtTipoTrabajo
            // 
            this.txtTipoTrabajo.Location = new System.Drawing.Point(540, 17);
            this.txtTipoTrabajo.Name = "txtTipoTrabajo";
            this.txtTipoTrabajo.Size = new System.Drawing.Size(201, 20);
            this.txtTipoTrabajo.TabIndex = 34;
            this.txtTipoTrabajo.Text = "Tipo de Trabajo";
            // 
            // Factura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(782, 540);
            this.Controls.Add(this.txtidcli);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txttolsin);
            this.Controls.Add(this.txtitbsin);
            this.Controls.Add(this.txtsubsin);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Tbla);
            this.Name = "Factura";
            this.Text = "Factura";
            this.Load += new System.EventHandler(this.Factura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tbla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRnc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtVence;
        private System.Windows.Forms.DataGridView Tbla;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.ComboBox cbMoneda;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtItebis;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtNCF;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbDescripcion;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtsubsin;
        private System.Windows.Forms.TextBox txtitbsin;
        private System.Windows.Forms.TextBox txttolsin;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtidcli;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbcompro;
        private System.Windows.Forms.CheckBox ckSinCompro;
        private System.Windows.Forms.TextBox txtTipoTrabajo;
    }
}

