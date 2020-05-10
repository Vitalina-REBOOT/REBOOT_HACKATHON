namespace HackBotDBEditor
{
    partial class addGroupForm
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
            this.groupDGV = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // groupDGV
            // 
            this.groupDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.groupDGV.Location = new System.Drawing.Point(12, 12);
            this.groupDGV.Name = "groupDGV";
            this.groupDGV.RowHeadersWidth = 51;
            this.groupDGV.RowTemplate.Height = 24;
            this.groupDGV.Size = new System.Drawing.Size(657, 520);
            this.groupDGV.TabIndex = 0;
            this.groupDGV.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.groupDGV_CellDoubleClick);
            // 
            // addGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 544);
            this.Controls.Add(this.groupDGV);
            this.Name = "addGroupForm";
            this.Text = "addGroupForm";
            this.Load += new System.EventHandler(this.addGroupForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupDGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView groupDGV;
    }
}