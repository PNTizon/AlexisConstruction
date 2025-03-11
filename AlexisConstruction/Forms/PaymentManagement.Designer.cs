namespace AlexisConstruction.Forms
{
    partial class PaymentManagement
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
            this.btnPaid = new System.Windows.Forms.Button();
            this.dgvBilling = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBilling)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPaid
            // 
            this.btnPaid.Location = new System.Drawing.Point(716, 483);
            this.btnPaid.Name = "btnPaid";
            this.btnPaid.Size = new System.Drawing.Size(75, 23);
            this.btnPaid.TabIndex = 5;
            this.btnPaid.Text = "Paid";
            this.btnPaid.UseVisualStyleBackColor = true;
            this.btnPaid.Click += new System.EventHandler(this.btnPaid_Click);
            // 
            // dgvBilling
            // 
            this.dgvBilling.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBilling.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBilling.Location = new System.Drawing.Point(37, 40);
            this.dgvBilling.Name = "dgvBilling";
            this.dgvBilling.Size = new System.Drawing.Size(793, 425);
            this.dgvBilling.TabIndex = 4;
            this.dgvBilling.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBilling_CellClick);
            // 
            // PaymentManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 531);
            this.Controls.Add(this.btnPaid);
            this.Controls.Add(this.dgvBilling);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PaymentManagement";
            this.Text = "PaymentManagement";
            this.Load += new System.EventHandler(this.PaymentManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBilling)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnPaid;
        private System.Windows.Forms.DataGridView dgvBilling;
    }
}