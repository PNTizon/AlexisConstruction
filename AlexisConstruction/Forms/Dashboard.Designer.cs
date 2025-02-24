namespace AlexisConstruction.Forms
{
    partial class Dashboard
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
            this.Schedules = new System.Windows.Forms.TabControl();
            this.Schedule = new System.Windows.Forms.TabPage();
            this.dgvSchedule = new System.Windows.Forms.DataGridView();
            this.Transactions = new System.Windows.Forms.TabPage();
            this.dgvReports = new System.Windows.Forms.DataGridView();
            this.Schedules.SuspendLayout();
            this.Schedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).BeginInit();
            this.Transactions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReports)).BeginInit();
            this.SuspendLayout();
            // 
            // Schedules
            // 
            this.Schedules.Controls.Add(this.Schedule);
            this.Schedules.Controls.Add(this.Transactions);
            this.Schedules.Location = new System.Drawing.Point(12, 36);
            this.Schedules.Name = "Schedules";
            this.Schedules.SelectedIndex = 0;
            this.Schedules.Size = new System.Drawing.Size(845, 459);
            this.Schedules.TabIndex = 1;
            // 
            // Schedule
            // 
            this.Schedule.Controls.Add(this.dgvSchedule);
            this.Schedule.Location = new System.Drawing.Point(4, 22);
            this.Schedule.Name = "Schedule";
            this.Schedule.Padding = new System.Windows.Forms.Padding(3);
            this.Schedule.Size = new System.Drawing.Size(837, 433);
            this.Schedule.TabIndex = 0;
            this.Schedule.Text = "WeeklySchedule";
            this.Schedule.UseVisualStyleBackColor = true;
            // 
            // dgvSchedule
            // 
            this.dgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedule.Location = new System.Drawing.Point(30, 34);
            this.dgvSchedule.Name = "dgvSchedule";
            this.dgvSchedule.Size = new System.Drawing.Size(770, 371);
            this.dgvSchedule.TabIndex = 0;
            // 
            // Transactions
            // 
            this.Transactions.Controls.Add(this.dgvReports);
            this.Transactions.Location = new System.Drawing.Point(4, 22);
            this.Transactions.Name = "Transactions";
            this.Transactions.Padding = new System.Windows.Forms.Padding(3);
            this.Transactions.Size = new System.Drawing.Size(837, 433);
            this.Transactions.TabIndex = 1;
            this.Transactions.Text = "Reports";
            this.Transactions.UseVisualStyleBackColor = true;
            // 
            // dgvReports
            // 
            this.dgvReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReports.Location = new System.Drawing.Point(37, 31);
            this.dgvReports.Name = "dgvReports";
            this.dgvReports.Size = new System.Drawing.Size(860, 371);
            this.dgvReports.TabIndex = 1;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 531);
            this.Controls.Add(this.Schedules);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.Schedules.ResumeLayout(false);
            this.Schedule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).EndInit();
            this.Transactions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReports)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Schedules;
        private System.Windows.Forms.TabPage Schedule;
        private System.Windows.Forms.DataGridView dgvSchedule;
        private System.Windows.Forms.TabPage Transactions;
        private System.Windows.Forms.DataGridView dgvReports;
    }
}