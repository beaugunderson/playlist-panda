namespace PlaylistPanda
{
    partial class OptionsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.proxyPasswordLabel = new System.Windows.Forms.Label();
            this.proxyUsernameLabel = new System.Windows.Forms.Label();
            this.proxyUrlLabel = new System.Windows.Forms.Label();
            this.proxyEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.proxyPasswordTextBox = new System.Windows.Forms.TextBox();
            this.proxyUsernameTextBox = new System.Windows.Forms.TextBox();
            this.proxyUriTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.locationsListBox = new System.Windows.Forms.ListBox();
            this.removeButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.passwordTextBox);
            this.groupBox1.Controls.Add(this.usernameTextBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 134);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Last.FM";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Username";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordTextBox.Location = new System.Drawing.Point(80, 45);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(111, 20);
            this.passwordTextBox.TabIndex = 1;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameTextBox.Location = new System.Drawing.Point(80, 19);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(111, 20);
            this.usernameTextBox.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox2, 2);
            this.groupBox2.Controls.Add(this.addButton);
            this.groupBox2.Controls.Add(this.removeButton);
            this.groupBox2.Controls.Add(this.locationsListBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 124);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File Locations";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.proxyPasswordLabel);
            this.groupBox4.Controls.Add(this.proxyUsernameLabel);
            this.groupBox4.Controls.Add(this.proxyUrlLabel);
            this.groupBox4.Controls.Add(this.proxyEnabledCheckBox);
            this.groupBox4.Controls.Add(this.proxyPasswordTextBox);
            this.groupBox4.Controls.Add(this.proxyUsernameTextBox);
            this.groupBox4.Controls.Add(this.proxyUriTextBox);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(206, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(197, 134);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Proxy";
            // 
            // proxyPasswordLabel
            // 
            this.proxyPasswordLabel.AutoSize = true;
            this.proxyPasswordLabel.Location = new System.Drawing.Point(6, 97);
            this.proxyPasswordLabel.Name = "proxyPasswordLabel";
            this.proxyPasswordLabel.Size = new System.Drawing.Size(53, 13);
            this.proxyPasswordLabel.TabIndex = 10;
            this.proxyPasswordLabel.Text = "Password";
            // 
            // proxyUsernameLabel
            // 
            this.proxyUsernameLabel.AutoSize = true;
            this.proxyUsernameLabel.Location = new System.Drawing.Point(6, 71);
            this.proxyUsernameLabel.Name = "proxyUsernameLabel";
            this.proxyUsernameLabel.Size = new System.Drawing.Size(55, 13);
            this.proxyUsernameLabel.TabIndex = 10;
            this.proxyUsernameLabel.Text = "Username";
            // 
            // proxyUrlLabel
            // 
            this.proxyUrlLabel.AutoSize = true;
            this.proxyUrlLabel.Location = new System.Drawing.Point(6, 45);
            this.proxyUrlLabel.Name = "proxyUrlLabel";
            this.proxyUrlLabel.Size = new System.Drawing.Size(29, 13);
            this.proxyUrlLabel.TabIndex = 10;
            this.proxyUrlLabel.Text = "URL";
            // 
            // proxyEnabledCheckBox
            // 
            this.proxyEnabledCheckBox.AutoSize = true;
            this.proxyEnabledCheckBox.Location = new System.Drawing.Point(6, 19);
            this.proxyEnabledCheckBox.Name = "proxyEnabledCheckBox";
            this.proxyEnabledCheckBox.Size = new System.Drawing.Size(93, 17);
            this.proxyEnabledCheckBox.TabIndex = 9;
            this.proxyEnabledCheckBox.Text = "Proxy enabled";
            this.proxyEnabledCheckBox.UseVisualStyleBackColor = true;
            this.proxyEnabledCheckBox.CheckedChanged += new System.EventHandler(this.proxyEnabledCheckBox_CheckedChanged);
            // 
            // proxyPasswordTextBox
            // 
            this.proxyPasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.proxyPasswordTextBox.Location = new System.Drawing.Point(91, 94);
            this.proxyPasswordTextBox.Name = "proxyPasswordTextBox";
            this.proxyPasswordTextBox.Size = new System.Drawing.Size(100, 20);
            this.proxyPasswordTextBox.TabIndex = 2;
            this.proxyPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // proxyUsernameTextBox
            // 
            this.proxyUsernameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.proxyUsernameTextBox.Location = new System.Drawing.Point(91, 68);
            this.proxyUsernameTextBox.Name = "proxyUsernameTextBox";
            this.proxyUsernameTextBox.Size = new System.Drawing.Size(100, 20);
            this.proxyUsernameTextBox.TabIndex = 1;
            // 
            // proxyUriTextBox
            // 
            this.proxyUriTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.proxyUriTextBox.Location = new System.Drawing.Point(91, 42);
            this.proxyUriTextBox.Name = "proxyUriTextBox";
            this.proxyUriTextBox.Size = new System.Drawing.Size(100, 20);
            this.proxyUriTextBox.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(406, 270);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(340, 288);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(259, 288);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // locationsListBox
            // 
            this.locationsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.locationsListBox.FormattingEnabled = true;
            this.locationsListBox.IntegralHeight = false;
            this.locationsListBox.Location = new System.Drawing.Point(6, 19);
            this.locationsListBox.Name = "locationsListBox";
            this.locationsListBox.Size = new System.Drawing.Size(388, 70);
            this.locationsListBox.TabIndex = 0;
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.Location = new System.Drawing.Point(319, 95);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 1;
            this.removeButton.Text = "&Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(238, 95);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "&Add...";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(430, 323);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(438, 350);
            this.Name = "OptionsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.TextBox proxyPasswordTextBox;
        private System.Windows.Forms.TextBox proxyUsernameTextBox;
        private System.Windows.Forms.TextBox proxyUriTextBox;
        private System.Windows.Forms.Label proxyPasswordLabel;
        private System.Windows.Forms.Label proxyUsernameLabel;
        private System.Windows.Forms.Label proxyUrlLabel;
        private System.Windows.Forms.CheckBox proxyEnabledCheckBox;
        private System.Windows.Forms.ListBox locationsListBox;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button addButton;
    }
}