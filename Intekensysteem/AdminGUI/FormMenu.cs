using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminGUI
{
    public partial class FormMenu : Form
    {
        public FormMenu(DateTime _dateToday, string _username, string _password, string _address)
        {
            InitializeComponent();
            _Username = _username;
            _Password = _password;
            _DateToday = _dateToday;
            _Address = _address;
        }

        DateTime _DateToday;
        string _Password = "";
        string _Address = "";
        string _Username = "";

        void staret(Form x)
        {
            //try {
            x.ShowDialog();
            //} catch (Exception ex) {
            //    MessageBox.Show(ex.Message, "Eroer");
            //}
        }

        private void Acounts_Click(object sender, EventArgs e)
        {
            staret(new FormModAcounts(_Username, _Password, _Address));
        }

        private void Users_Click(object sender, EventArgs e)
        {
            staret(new FormModUsers(_Username, _Password, _Address));
        }

        private void uitzon_Click(object sender, EventArgs e)
        {
            staret(new FormModModifiers(_Username, _Password, _Address));
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
