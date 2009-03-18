using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;
using PlaylistPanda.Properties;

namespace PlaylistPanda
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            usernameTextBox.Text = Settings.Default.LastFmUserName;
            passwordTextBox.Text = Settings.Default.LastFmPassword;

            proxyUriTextBox.Text = Settings.Default.ProxyUri;
            proxyUsernameTextBox.Text = Settings.Default.ProxyUsername;
            proxyPasswordTextBox.Text = Settings.Default.ProxyPassword;

            proxyEnabledCheckBox.Checked = Settings.Default.ProxyEnabled;

            if (Settings.Default.Locations != null)
            {
                foreach (string location in Settings.Default.Locations)
                {
                    locationsListBox.Items.Add(location);
                }
            }

            toggleProxyTextBoxes();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Settings.Default.LastFmUserName = usernameTextBox.Text;
            Settings.Default.LastFmPassword = passwordTextBox.Text;

            Settings.Default.ProxyUri = proxyUriTextBox.Text;
            Settings.Default.ProxyUsername = proxyUsernameTextBox.Text;
            Settings.Default.ProxyPassword = proxyPasswordTextBox.Text;

            Settings.Default.ProxyEnabled = proxyEnabledCheckBox.Checked;

            StringCollection locations = new StringCollection();

            foreach (object item in locationsListBox.Items)
            {
                locations.Add(item.ToString());
            }

            Settings.Default.Locations = locations;

            Settings.Default.Save();

            DialogResult = DialogResult.OK;

            Close();
        }

        public static string ObjectToString(object o)
        {
            return o.ToString();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }

        private void proxyEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            toggleProxyTextBoxes();
        }

        private void toggleProxyTextBoxes()
        {
            proxyUriTextBox.Enabled = proxyEnabledCheckBox.Checked;
            proxyUsernameTextBox.Enabled = proxyEnabledCheckBox.Checked;
            proxyPasswordTextBox.Enabled = proxyEnabledCheckBox.Checked;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.MyComputer
            };

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                locationsListBox.Items.Add(folderBrowserDialog.SelectedPath);
            }
        }
    }
}