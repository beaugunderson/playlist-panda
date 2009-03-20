namespace PlaylistPanda
{
    partial class MainForm
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
            this.optionsButton = new System.Windows.Forms.Button();
            this.topTracksListBox = new System.Windows.Forms.ListBox();
            this.matchButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ArtistColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlbumColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TitleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // optionsButton
            // 
            this.optionsButton.Location = new System.Drawing.Point(12, 12);
            this.optionsButton.Name = "optionsButton";
            this.optionsButton.Size = new System.Drawing.Size(75, 23);
            this.optionsButton.TabIndex = 0;
            this.optionsButton.Text = "&Options...";
            this.optionsButton.UseVisualStyleBackColor = true;
            this.optionsButton.Click += new System.EventHandler(this.optionsButton_Click);
            // 
            // topTracksListBox
            // 
            this.topTracksListBox.FormattingEnabled = true;
            this.topTracksListBox.IntegralHeight = false;
            this.topTracksListBox.Location = new System.Drawing.Point(12, 41);
            this.topTracksListBox.Name = "topTracksListBox";
            this.topTracksListBox.Size = new System.Drawing.Size(310, 328);
            this.topTracksListBox.TabIndex = 1;
            // 
            // matchButton
            // 
            this.matchButton.Enabled = false;
            this.matchButton.Location = new System.Drawing.Point(93, 12);
            this.matchButton.Name = "matchButton";
            this.matchButton.Size = new System.Drawing.Size(75, 23);
            this.matchButton.TabIndex = 2;
            this.matchButton.Text = "Match";
            this.matchButton.UseVisualStyleBackColor = true;
            this.matchButton.Click += new System.EventHandler(this.matchButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ArtistColumn,
            this.AlbumColumn,
            this.TitleColumn});
            this.dataGridView1.Location = new System.Drawing.Point(328, 41);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(373, 328);
            this.dataGridView1.TabIndex = 3;
            // 
            // ArtistColumn
            // 
            this.ArtistColumn.HeaderText = "Artist";
            this.ArtistColumn.Name = "ArtistColumn";
            this.ArtistColumn.ReadOnly = true;
            // 
            // AlbumColumn
            // 
            this.AlbumColumn.HeaderText = "Album";
            this.AlbumColumn.Name = "AlbumColumn";
            this.AlbumColumn.ReadOnly = true;
            // 
            // TitleColumn
            // 
            this.TitleColumn.HeaderText = "Title";
            this.TitleColumn.Name = "TitleColumn";
            this.TitleColumn.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 381);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.matchButton);
            this.Controls.Add(this.topTracksListBox);
            this.Controls.Add(this.optionsButton);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button optionsButton;
        private System.Windows.Forms.ListBox topTracksListBox;
        private System.Windows.Forms.Button matchButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArtistColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlbumColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TitleColumn;
    }
}

