namespace IPPhone
{
    partial class Administrator
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pushCallRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateInfoNumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatePartitionNumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView_Administrator = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Administrator)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.optionToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(639, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.newToolStripMenuItem.Text = "New...";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pushCallRecordToolStripMenuItem,
            this.updateInfoNumberToolStripMenuItem,
            this.updatePartitionNumberToolStripMenuItem});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionToolStripMenuItem.Text = "Option";
            // 
            // pushCallRecordToolStripMenuItem
            // 
            this.pushCallRecordToolStripMenuItem.Name = "pushCallRecordToolStripMenuItem";
            this.pushCallRecordToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.pushCallRecordToolStripMenuItem.Text = "Push Call Record...";
            this.pushCallRecordToolStripMenuItem.Click += new System.EventHandler(this.pushCallRecordToolStripMenuItem_Click);
            // 
            // updateInfoNumberToolStripMenuItem
            // 
            this.updateInfoNumberToolStripMenuItem.Name = "updateInfoNumberToolStripMenuItem";
            this.updateInfoNumberToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.updateInfoNumberToolStripMenuItem.Text = "Update Info Number";
            this.updateInfoNumberToolStripMenuItem.Click += new System.EventHandler(this.updateInfoNumberToolStripMenuItem_Click);
            // 
            // updatePartitionNumberToolStripMenuItem
            // 
            this.updatePartitionNumberToolStripMenuItem.Name = "updatePartitionNumberToolStripMenuItem";
            this.updatePartitionNumberToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.updatePartitionNumberToolStripMenuItem.Text = "Update Partition Number";
            this.updatePartitionNumberToolStripMenuItem.Click += new System.EventHandler(this.updatePartitionNumberToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // dataGridView_Administrator
            // 
            this.dataGridView_Administrator.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Administrator.Location = new System.Drawing.Point(29, 43);
            this.dataGridView_Administrator.Name = "dataGridView_Administrator";
            this.dataGridView_Administrator.Size = new System.Drawing.Size(572, 219);
            this.dataGridView_Administrator.TabIndex = 5;
            this.dataGridView_Administrator.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Administrator_CellContentClick);
            // 
            // Administrator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 350);
            this.Controls.Add(this.dataGridView_Administrator);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Administrator";
            this.Text = "Administrator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Administrator)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pushCallRecordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateInfoNumberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updatePartitionNumberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView_Administrator;
    }
}

