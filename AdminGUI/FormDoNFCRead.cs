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

namespace AdminGUI
{
    public partial class FormDoNFCRead : Form
    {
        public FormDoNFCRead()
        {
            InitializeComponent();
        }

        public bool GotWorkingPort { get; set; } = false;
        public string Port { get; set; } = "";

        private void FormUsersConnectNFCReader_Load(object sender, EventArgs e)
        {
            buttonRefreshSerialPorts_Click(null, null);
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (FormHelp.testSerialPort((string)listBox1.SelectedItem))
            {
                button1.BackColor = Color.Green;
                GotWorkingPort = true;
            }
            else
            {
                button1.BackColor = Color.Red;
                GotWorkingPort = false;
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            button1_Click(null, null);
            this.Close();
        }
    }
}
