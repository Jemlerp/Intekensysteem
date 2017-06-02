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
using System.IO.Ports;

namespace AdminGUI
{
    public partial class FormModUsers : Form
    {
        public FormModUsers(string _username, string _password, string _address)
        {
            InitializeComponent();
            _UserName = _username;
            _Password = _password;
            _Address = _address;
        }

        string _Password = "";
        string _Address = "";
        string _UserName = "";
        string _JustReadNFCIDJustValue = "";
        private delegate void handelTextDelegate(string read);

        void setAllz(string NFCID)
        {
            _JustReadNFCIDJustValue = NFCID;
            textBoxNewNfcid.Text = NFCID;
            textBoxUpdateNFCID.Text = NFCID;
        }

        List<DBDingus.UserTableTableEntry> alleDeEntrys;
        SerialPort _Serialport;

        private T webr<T>(object request)
        {
            return FormHelp.Requestion<T>(request, _UserName, _Password, _Address);
        }

        void readReadFromSerial(object _ebjec, object _tokdekmak)
        {
            string Read = _Serialport.ReadLine();
            BeginInvoke(new handelTextDelegate(setAllz), FormHelp.SerialReadToNormal(Read));
        }


        private void FormModUsers_Load(object sender, EventArgs e)
        {
            dateTimePickerNewDateJoined.Format = DateTimePickerFormat.Custom;
            dateTimePickerNewDateJoined.CustomFormat = "dd/MM/yyyy";
            dateTimePickerNewDateLeft.Format = DateTimePickerFormat.Custom;
            dateTimePickerNewDateLeft.CustomFormat = "dd/MM/yyyy";
            dateTimePickerUpdateDateJoined.Format = DateTimePickerFormat.Custom;
            dateTimePickerUpdateDateJoined.CustomFormat = "dd/MM/yyyy";
            dateTimePickerUpdateDateLeft.Format = DateTimePickerFormat.Custom;
            dateTimePickerUpdateDateLeft.CustomFormat = "dd/MM/yyyy";

            refreshList();

        }

        private void connectRederToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDoNFCRead form = new FormDoNFCRead();
            form.ShowDialog();
            if (form.GotWorkingPort)
            {
                try
                {
                    _Serialport = new SerialPort(form.Port, 9600);
                    _Serialport.DataReceived += new SerialDataReceivedEventHandler(readReadFromSerial);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {

                    DBDingus.UserTableTableEntry deEntry = alleDeEntrys.Where(entr => entr.ID == Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())).FirstOrDefault();

                    textBoxUpdateID.Text = deEntry.ID.ToString();
                    textBoxUpdateVoornaam.Text = deEntry.VoorNaam;
                    textBoxUpdateAchternaam.Text = deEntry.AchterNaam;
                    textBoxUpdateNFCID.Text = deEntry.NFCID;

                    dateTimePickerUpdateDateJoined.Value = deEntry.DateJoined;

                    checkBoxUpdateZitNogOpSchool.Checked = deEntry.IsActiveUser;
                    if (!deEntry.IsActiveUser)
                    {
                        dateTimePickerUpdateDateLeft.Value = deEntry.DateLeft;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "dit had niet moeten gebeuren");
                }
            }
        }

        void refreshList()
        {
            refreshList("");
        }

        public void refreshList(string searth)
        {
            try
            {
                alleDeEntrys = webr<NetCom.ServerResponseGetUserTable>(new NetCom.ServerRequestGetUserTable()).deEntrys;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            DataTable dieWeSet = new DataTable();

            string ID = "ID";
            string Vnaam = "Voornaam";
            string aNaam = "Achternaam";
            string nfcid = "NFCID";
            string zitnogopschool = "ZitOpSchool";
            string dateJoined = "DateJoined";
            string dateLeft = "DateLeft";

            dieWeSet.Columns.Add(ID);
            dieWeSet.Columns.Add(Vnaam);
            dieWeSet.Columns.Add(aNaam);
            dieWeSet.Columns.Add(nfcid);
            dieWeSet.Columns.Add(zitnogopschool);
            dieWeSet.Columns.Add(dateJoined);
            dieWeSet.Columns.Add(dateLeft);

            foreach (var i in alleDeEntrys.Where(vnaam => vnaam.VoorNaam.Contains(searth) || vnaam.AchterNaam.Contains(searth)))
            {
                DataRow row = dieWeSet.NewRow();
                row[ID] = i.ID;
                row[Vnaam] = i.VoorNaam;
                row[aNaam] = i.AchterNaam;
                row[nfcid] = i.NFCID;
                row[zitnogopschool] = i.IsActiveUser.ToString();
                row[dateJoined] = i.DateJoined.Date.ToString("yyyy-MM-dd");
                if (!i.IsActiveUser)
                {
                    row[dateLeft] = i.DateLeft.Date.ToString("yyyy-MM-dd");
                }
                else
                {
                    row[dateLeft] = "NULL";
                }
                dieWeSet.Rows.Add(row);
            }

            dataGridView1.DataSource = dieWeSet;
            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        private void push(bool esNieuw)
        {
            push(esNieuw, false);
        }

        private void push(bool isNew, bool delete)
        {
            try
            {
                NetCom.ServerRequestChangeUserTable request = new NetCom.ServerRequestChangeUserTable();
                DBDingus.UserTableTableEntry deEntry = new DBDingus.UserTableTableEntry();

                if (delete)
                {
                    request.DeleteEntry = true;
                    deEntry.ID = Convert.ToInt32(textBoxUpdateID.Text);
                }
                else
                {
                    request.DeleteEntry = false;
                    request.IsNewUser = isNew;

                    if (isNew)
                    {

                        deEntry.VoorNaam = textBoxNewvoornaam.Text;
                        deEntry.AchterNaam = textBoxNewAchternaam.Text;
                        deEntry.NFCID = textBoxNewNfcid.Text;
                        deEntry.DateJoined = dateTimePickerNewDateJoined.Value;

                        deEntry.IsActiveUser = checkBoxNewZitNogOpSchool.Checked;
                        if (deEntry.IsActiveUser)
                        {
                            deEntry.DateLeft = dateTimePickerNewDateLeft.Value;
                        }

                    }
                    else
                    {

                        deEntry.ID = Convert.ToInt32(textBoxUpdateID.Text);

                        deEntry.VoorNaam = textBoxUpdateVoornaam.Text;
                        deEntry.AchterNaam = textBoxUpdateAchternaam.Text;
                        deEntry.NFCID = textBoxUpdateNFCID.Text;
                        deEntry.DateJoined = dateTimePickerUpdateDateJoined.Value;

                        deEntry.IsActiveUser = checkBoxUpdateZitNogOpSchool.Checked;
                        if (deEntry.IsActiveUser)
                        {
                            deEntry.DateLeft = dateTimePickerUpdateDateLeft.Value;
                        }
                    }
                }

                request.deEntry = deEntry;

                NetCom.ServerResponseChangeUserTable response = webr<NetCom.ServerResponseChangeUserTable>(request);

                if (response.OK == false)
                {
                    if (isNew)
                    {
                        throw new Exception("buttonNew~~ response !OK");
                    }
                    else
                    {
                        throw new Exception("buttonUpdate~~ response !OK");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            refreshList();
        }

        private void checkBoxUpdateZitNogOpSchool_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerUpdateDateLeft.Enabled = !checkBoxUpdateZitNogOpSchool.Checked;
        }

        private void checkBoxNewZitNogOpSchool_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerNewDateLeft.Enabled = checkBoxNewZitNogOpSchool.Checked;
        }

        private void buttonUopdate_Click(object sender, EventArgs e)
        {
            push(false);
        }

        private void buttonSaveNew_Click(object sender, EventArgs e)
        {
            push(true);
        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Weet Je Het Zeker?", "DELETE USER", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (MessageBox.Show("Weet Je Het Echt Heel Zeker?", "DELETE USER", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK)
                {
                    push(false, true);
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                refreshList(textBox1.Text);
            }
            else
            {
                refreshList("");
            }
        }
    }
}
