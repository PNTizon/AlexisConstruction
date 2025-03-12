namespace AlexisConstruction.Forms
{
    partial class Mainform
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.BillingStatement = new System.Windows.Forms.Button();
            this.PaymentManagerbtn = new System.Windows.Forms.Button();
            this.InventoryManagerbtn = new System.Windows.Forms.Button();
            this.ServiceManagerbtn = new System.Windows.Forms.Button();
            this.ClientManagerbtn = new System.Windows.Forms.Button();
            this.BookingManagerbtn = new System.Windows.Forms.Button();
            this.Dashboardbtn = new System.Windows.Forms.Button();
            this.paneldash = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BillingStatement);
            this.panel1.Controls.Add(this.PaymentManagerbtn);
            this.panel1.Controls.Add(this.InventoryManagerbtn);
            this.panel1.Controls.Add(this.ServiceManagerbtn);
            this.panel1.Controls.Add(this.ClientManagerbtn);
            this.panel1.Controls.Add(this.BookingManagerbtn);
            this.panel1.Controls.Add(this.Dashboardbtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(154, 532);
            this.panel1.TabIndex = 10;
            // 
            // BillingStatement
            // 
            this.BillingStatement.Location = new System.Drawing.Point(41, 423);
            this.BillingStatement.Name = "BillingStatement";
            this.BillingStatement.Size = new System.Drawing.Size(75, 23);
            this.BillingStatement.TabIndex = 12;
            this.BillingStatement.Text = "Billings";
            this.BillingStatement.UseVisualStyleBackColor = true;
            this.BillingStatement.Click += new System.EventHandler(this.Form1_Click);
            // 
            // PaymentManagerbtn
            // 
            this.PaymentManagerbtn.Location = new System.Drawing.Point(41, 371);
            this.PaymentManagerbtn.Name = "PaymentManagerbtn";
            this.PaymentManagerbtn.Size = new System.Drawing.Size(75, 23);
            this.PaymentManagerbtn.TabIndex = 11;
            this.PaymentManagerbtn.Text = "Payment Transaction";
            this.PaymentManagerbtn.UseVisualStyleBackColor = true;
            this.PaymentManagerbtn.Click += new System.EventHandler(this.Form1_Click);
            // 
            // InventoryManagerbtn
            // 
            this.InventoryManagerbtn.Location = new System.Drawing.Point(41, 318);
            this.InventoryManagerbtn.Name = "InventoryManagerbtn";
            this.InventoryManagerbtn.Size = new System.Drawing.Size(75, 23);
            this.InventoryManagerbtn.TabIndex = 10;
            this.InventoryManagerbtn.Text = "Inventory";
            this.InventoryManagerbtn.UseVisualStyleBackColor = true;
            this.InventoryManagerbtn.Click += new System.EventHandler(this.Form1_Click);
            // 
            // ServiceManagerbtn
            // 
            this.ServiceManagerbtn.Location = new System.Drawing.Point(41, 265);
            this.ServiceManagerbtn.Name = "ServiceManagerbtn";
            this.ServiceManagerbtn.Size = new System.Drawing.Size(75, 23);
            this.ServiceManagerbtn.TabIndex = 9;
            this.ServiceManagerbtn.Text = "Services";
            this.ServiceManagerbtn.UseVisualStyleBackColor = true;
            this.ServiceManagerbtn.Click += new System.EventHandler(this.Form1_Click);
            // 
            // ClientManagerbtn
            // 
            this.ClientManagerbtn.Location = new System.Drawing.Point(41, 201);
            this.ClientManagerbtn.Name = "ClientManagerbtn";
            this.ClientManagerbtn.Size = new System.Drawing.Size(75, 23);
            this.ClientManagerbtn.TabIndex = 8;
            this.ClientManagerbtn.Text = "Clients";
            this.ClientManagerbtn.UseVisualStyleBackColor = true;
            this.ClientManagerbtn.Click += new System.EventHandler(this.Form1_Click);
            // 
            // BookingManagerbtn
            // 
            this.BookingManagerbtn.Location = new System.Drawing.Point(41, 146);
            this.BookingManagerbtn.Name = "BookingManagerbtn";
            this.BookingManagerbtn.Size = new System.Drawing.Size(75, 23);
            this.BookingManagerbtn.TabIndex = 7;
            this.BookingManagerbtn.Text = "Bookings";
            this.BookingManagerbtn.UseVisualStyleBackColor = true;
            this.BookingManagerbtn.Click += new System.EventHandler(this.Form1_Click);
            // 
            // Dashboardbtn
            // 
            this.Dashboardbtn.Location = new System.Drawing.Point(41, 85);
            this.Dashboardbtn.Name = "Dashboardbtn";
            this.Dashboardbtn.Size = new System.Drawing.Size(75, 23);
            this.Dashboardbtn.TabIndex = 6;
            this.Dashboardbtn.Text = "Dashboard";
            this.Dashboardbtn.UseVisualStyleBackColor = true;
            this.Dashboardbtn.Click += new System.EventHandler(this.Form1_Click);
            // 
            // paneldash
            // 
            this.paneldash.Location = new System.Drawing.Point(155, 1);
            this.paneldash.Name = "paneldash";
            this.paneldash.Size = new System.Drawing.Size(869, 531);
            this.paneldash.TabIndex = 9;
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 532);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.paneldash);
            this.Name = "Mainform";
            this.Load += new System.EventHandler(this.Mainform_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button InventoryManagerbtn;
        private System.Windows.Forms.Button ServiceManagerbtn;
        private System.Windows.Forms.Button ClientManagerbtn;
        private System.Windows.Forms.Button BookingManagerbtn;
        private System.Windows.Forms.Button Dashboardbtn;
        private System.Windows.Forms.Panel paneldash;
        private System.Windows.Forms.Button PaymentManagerbtn;
        private System.Windows.Forms.Button BillingStatement;
    }
}