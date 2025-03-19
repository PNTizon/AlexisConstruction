namespace AlexisConstruction.Forms
{
    partial class BookingManagement
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.nudHoursRendered = new System.Windows.Forms.NumericUpDown();
            this.cmbServices = new System.Windows.Forms.ComboBox();
            this.dgvServices = new System.Windows.Forms.DataGridView();
            this.ServiceID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoursRendered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HourlyRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sHOWCLIENTSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet2 = new AlexisConstruction.DataSet.DataSet2();
            this.bILLINGSTATEMENTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.cmbClients = new System.Windows.Forms.ComboBox();
            this.dtpBookingDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.txtCash = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtChange = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnPay = new System.Windows.Forms.Button();
            this.txtServiceID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.panel17 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.sHOWCLIENTSTableAdapter = new AlexisConstruction.DataSet.DataSet2TableAdapters.SHOWCLIENTSTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.nudHoursRendered)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sHOWCLIENTSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bILLINGSTATEMENTBindingSource)).BeginInit();
            this.panel17.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(171, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 16);
            this.label3.TabIndex = 274;
            this.label3.Text = "Services :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(480, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 16);
            this.label2.TabIndex = 273;
            this.label2.Text = "Hours Rendered :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(153)))), ((int)(((byte)(78)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(344, 160);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 26);
            this.button1.TabIndex = 272;
            this.button1.Text = "Book";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnBook_Click);
            // 
            // nudHoursRendered
            // 
            this.nudHoursRendered.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudHoursRendered.Location = new System.Drawing.Point(596, 74);
            this.nudHoursRendered.Name = "nudHoursRendered";
            this.nudHoursRendered.Size = new System.Drawing.Size(199, 22);
            this.nudHoursRendered.TabIndex = 271;
            this.nudHoursRendered.ValueChanged += new System.EventHandler(this.nudHoursRendered_ValueChanged);
            // 
            // cmbServices
            // 
            this.cmbServices.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbServices.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbServices.DropDownHeight = 90;
            this.cmbServices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbServices.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbServices.FormattingEnabled = true;
            this.cmbServices.IntegralHeight = false;
            this.cmbServices.Location = new System.Drawing.Point(243, 74);
            this.cmbServices.Name = "cmbServices";
            this.cmbServices.Size = new System.Drawing.Size(199, 24);
            this.cmbServices.TabIndex = 270;
            this.cmbServices.SelectedIndexChanged += new System.EventHandler(this.cmbServices_SelectedIndexChanged);
            // 
            // dgvServices
            // 
            this.dgvServices.AllowUserToAddRows = false;
            this.dgvServices.AllowUserToDeleteRows = false;
            this.dgvServices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvServices.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvServices.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(245)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvServices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ServiceID,
            this.ServiceName,
            this.HoursRendered,
            this.HourlyRate,
            this.Amount});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvServices.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvServices.EnableHeadersVisualStyles = false;
            this.dgvServices.GridColor = System.Drawing.Color.Black;
            this.dgvServices.Location = new System.Drawing.Point(37, 214);
            this.dgvServices.Name = "dgvServices";
            this.dgvServices.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvServices.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvServices.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvServices.Size = new System.Drawing.Size(923, 314);
            this.dgvServices.TabIndex = 269;
            // 
            // ServiceID
            // 
            this.ServiceID.HeaderText = "Service ID";
            this.ServiceID.Name = "ServiceID";
            this.ServiceID.ReadOnly = true;
            // 
            // ServiceName
            // 
            this.ServiceName.HeaderText = "Service Name";
            this.ServiceName.Name = "ServiceName";
            this.ServiceName.ReadOnly = true;
            // 
            // HoursRendered
            // 
            this.HoursRendered.HeaderText = "Hours Rendered";
            this.HoursRendered.Name = "HoursRendered";
            this.HoursRendered.ReadOnly = true;
            // 
            // HourlyRate
            // 
            this.HourlyRate.HeaderText = "Hourly Rate";
            this.HourlyRate.Name = "HourlyRate";
            this.HourlyRate.ReadOnly = true;
            // 
            // Amount
            // 
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            // 
            // sHOWCLIENTSBindingSource
            // 
            this.sHOWCLIENTSBindingSource.DataMember = "SHOWCLIENTS";
            this.sHOWCLIENTSBindingSource.DataSource = this.dataSet2;
            // 
            // dataSet2
            // 
            this.dataSet2.DataSetName = "DataSet2";
            this.dataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bILLINGSTATEMENTBindingSource
            // 
            this.bILLINGSTATEMENTBindingSource.DataMember = "BILLINGSTATEMENT";
            this.bILLINGSTATEMENTBindingSource.DataSource = this.dataSet2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(184, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 268;
            this.label1.Text = "Clients :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbClients
            // 
            this.cmbClients.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbClients.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbClients.DropDownHeight = 90;
            this.cmbClients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbClients.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbClients.FormattingEnabled = true;
            this.cmbClients.IntegralHeight = false;
            this.cmbClients.Location = new System.Drawing.Point(243, 38);
            this.cmbClients.Name = "cmbClients";
            this.cmbClients.Size = new System.Drawing.Size(199, 24);
            this.cmbClients.TabIndex = 267;
            this.cmbClients.SelectedIndexChanged += new System.EventHandler(this.cmbClients_SelectedIndexChanged);
            // 
            // dtpBookingDate
            // 
            this.dtpBookingDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBookingDate.Location = new System.Drawing.Point(596, 36);
            this.dtpBookingDate.Name = "dtpBookingDate";
            this.dtpBookingDate.Size = new System.Drawing.Size(199, 22);
            this.dtpBookingDate.TabIndex = 266;
            this.dtpBookingDate.ValueChanged += new System.EventHandler(this.dtpBookingDate_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(482, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 16);
            this.label4.TabIndex = 265;
            this.label4.Text = "Scheduled Date :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(153)))), ((int)(((byte)(78)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(605, 160);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 26);
            this.button2.TabIndex = 275;
            this.button2.Text = "Add";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnAddService_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(755, 543);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 16);
            this.label5.TabIndex = 278;
            this.label5.Text = "Total Amount : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTotalAmount.Location = new System.Drawing.Point(853, 543);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(0, 18);
            this.lblTotalAmount.TabIndex = 277;
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCash
            // 
            this.txtCash.Location = new System.Drawing.Point(243, 107);
            this.txtCash.Multiline = true;
            this.txtCash.Name = "txtCash";
            this.txtCash.Size = new System.Drawing.Size(199, 24);
            this.txtCash.TabIndex = 280;
            this.txtCash.TextChanged += new System.EventHandler(this.txtCash_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(190, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 16);
            this.label6.TabIndex = 281;
            this.label6.Text = "Cash :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtChange
            // 
            this.txtChange.Location = new System.Drawing.Point(596, 107);
            this.txtChange.Multiline = true;
            this.txtChange.Name = "txtChange";
            this.txtChange.Size = new System.Drawing.Size(199, 24);
            this.txtChange.TabIndex = 282;
            this.txtChange.TextChanged += new System.EventHandler(this.txtChange_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(533, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 16);
            this.label7.TabIndex = 283;
            this.label7.Text = "Change :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPay
            // 
            this.btnPay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(153)))), ((int)(((byte)(78)))));
            this.btnPay.FlatAppearance.BorderSize = 0;
            this.btnPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPay.ForeColor = System.Drawing.Color.White;
            this.btnPay.Location = new System.Drawing.Point(476, 160);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(80, 26);
            this.btnPay.TabIndex = 284;
            this.btnPay.Text = "Pay";
            this.btnPay.UseVisualStyleBackColor = false;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // txtServiceID
            // 
            this.txtServiceID.BackColor = System.Drawing.Color.White;
            this.txtServiceID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtServiceID.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServiceID.ForeColor = System.Drawing.SystemColors.Control;
            this.txtServiceID.Location = new System.Drawing.Point(297, 66);
            this.txtServiceID.Name = "txtServiceID";
            this.txtServiceID.Size = new System.Drawing.Size(100, 3);
            this.txtServiceID.TabIndex = 285;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.ForeColor = System.Drawing.SystemColors.Control;
            this.txtName.Location = new System.Drawing.Point(294, 29);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 3);
            this.txtName.TabIndex = 286;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DimGray;
            this.panel5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.panel5.Location = new System.Drawing.Point(243, 74);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1, 24);
            this.panel5.TabIndex = 287;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.DimGray;
            this.panel14.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.panel14.Location = new System.Drawing.Point(243, 74);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(199, 1);
            this.panel14.TabIndex = 292;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.DimGray;
            this.panel15.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.panel15.Location = new System.Drawing.Point(441, 74);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(1, 24);
            this.panel15.TabIndex = 293;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.DimGray;
            this.panel16.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.panel16.Location = new System.Drawing.Point(441, 38);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(1, 24);
            this.panel16.TabIndex = 288;
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.DimGray;
            this.panel17.Controls.Add(this.panel3);
            this.panel17.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.panel17.Location = new System.Drawing.Point(243, 38);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(1, 24);
            this.panel17.TabIndex = 294;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DimGray;
            this.panel3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.panel3.Location = new System.Drawing.Point(0, 23);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(199, 1);
            this.panel3.TabIndex = 293;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Location = new System.Drawing.Point(243, 97);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(199, 1);
            this.panel1.TabIndex = 295;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DimGray;
            this.panel2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.panel2.Location = new System.Drawing.Point(243, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(199, 1);
            this.panel2.TabIndex = 296;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DimGray;
            this.panel4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.panel4.Location = new System.Drawing.Point(243, 61);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(199, 1);
            this.panel4.TabIndex = 297;
            // 
            // sHOWCLIENTSTableAdapter
            // 
            this.sHOWCLIENTSTableAdapter.ClearBeforeFill = true;
            // 
            // BookingManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 578);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel17);
            this.Controls.Add(this.panel16);
            this.Controls.Add(this.panel15);
            this.Controls.Add(this.panel14);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtServiceID);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtChange);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCash);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.nudHoursRendered);
            this.Controls.Add(this.cmbServices);
            this.Controls.Add(this.dgvServices);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbClients);
            this.Controls.Add(this.dtpBookingDate);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BookingManagement";
            this.Text = "BookingManagement";
            this.Load += new System.EventHandler(this.BookingManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudHoursRendered)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sHOWCLIENTSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bILLINGSTATEMENTBindingSource)).EndInit();
            this.panel17.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown nudHoursRendered;
        private System.Windows.Forms.ComboBox cmbServices;
        private System.Windows.Forms.DataGridView dgvServices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbClients;
        private System.Windows.Forms.DateTimePicker dtpBookingDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.TextBox txtCash;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtChange;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.TextBox txtServiceID;
        private System.Windows.Forms.TextBox txtName;
        private DataSet.DataSet2 dataSet2;
        private System.Windows.Forms.BindingSource sHOWCLIENTSBindingSource;
        private DataSet.DataSet2TableAdapters.SHOWCLIENTSTableAdapter sHOWCLIENTSTableAdapter;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.BindingSource bILLINGSTATEMENTBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoursRendered;
        private System.Windows.Forms.DataGridViewTextBoxColumn HourlyRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
    }
}