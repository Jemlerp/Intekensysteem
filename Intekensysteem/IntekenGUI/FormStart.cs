using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ErFunc;

namespace IntekenGUI
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
                FormMain form = new FormMain((string)listBox1.SelectedItem, textBoxApiAddres.Text, textBoxUserName.Text, textBoxPassword.Text, checkBoxStartWindowed.Checked);
                Visible = false;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private bool testSerialPort(string port)
        {
            return FormHelp.testSerialPort(port);
        }

        private void buttonRefreshSerialPorts_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string[] comlist = SerialPort.GetPortNames();
            foreach (string com in comlist)
            {
                listBox1.Items.Add(com);
            }
            if (listBox1.Items.Count > 0) { listBox1.SelectedItem = listBox1.Items[0]; }

        }

        private void FormStart_Load(object sender, EventArgs e)
        {
            buttonRefreshSerialPorts_Click(null, null);
        }
    }
}
