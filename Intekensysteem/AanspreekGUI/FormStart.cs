using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ErFunc;

namespace AanspreekGUI
{
    public partial class FormStart : Form
    {
        public FormStart()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            //test server connection/login gegevens
            try
            {
                NetCom.ServerResponse response = NetCom.WebRequest(new NetCom.ServerRequestSqlDateTime(), textBoxUserName.Text, textBoxPassword.Text, textBoxApiAddres.Text);
                if (response.IsErrorOccurred)
                {
                    if (MessageBox.Show(response.ErrorInfo.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop) == DialogResult.OK)
                    {
                        //buttonStart_Click(null, null); nee
                    }
                }
            }
            catch
            {
                if (MessageBox.Show("Kan Niet Met Server Verbinden", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop) == DialogResult.Retry)
                {
                    buttonStart_Click(null, null);
                }
                else
                {
                    return;
                }
                return;
            }

            //do
            try
            {
                FormMain form = new FormMain(textBoxPassword.Text, textBoxUserName.Text, textBoxApiAddres.Text, checkBox1.Checked);
                Visible = false;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "dit had niet moeten gebeuren...");
            }
        }
    }
}
