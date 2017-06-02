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

namespace AdminGUI
{
    public partial class FormModModifiers : Form
    {
        public FormModModifiers(string _username, string _password, string _address)
        {
            InitializeComponent();
            _UserName = _username;
            _Password = _password;
            _Address = _address;
        }

        string _Password = "";
        string _Address = "";
        string _UserName = "";

        List<DBDingus.UserTableTableEntry> selectedUsers = new List<DBDingus.UserTableTableEntry>();
        List<DBDingus.UserTableTableEntry> alleDeUserEntrys = new List<DBDingus.UserTableTableEntry>();
        List<DBDingus.ModifierTableEntry> alleDeModifierEntrys = new List<DBDingus.ModifierTableEntry>();
        int curModEntryID = 0;

        private T webr<T>(object request)
        {
            return FormHelp.Requestion<T>(request, _UserName, _Password, _Address);
        }

        private void FormModModifiers_Load(object sender, EventArgs e)
        {
            dateTimePickerHoursToAdd.ShowUpDown = true;
            dateTimePickerHoursToAdd.CustomFormat = "HH:mm:ss";
            dateTimePickerHoursToAdd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;

            LoadAlleDeEntrys();
            LoadDataTablesFromAlleDeEntrys();
        }

        private void push(bool isNew, bool delete)
        { // push to server
            NetCom.ServerRequestChangeModTable request = new NetCom.ServerRequestChangeModTable();
            DBDingus.ModifierTableEntry d = new DBDingus.ModifierTableEntry();
            if (delete)
            {
                request.DeleteEntry = true;
                d.ID = curModEntryID;
            }
            else
            {
                request.DeleteEntry = false;
                if (isNew)
                {
                    request.IsNewEntry = true;
                }
                else
                {
                    d.ID = curModEntryID;
                    request.IsNewEntry = false;
                }
                //fill D
                d.HoursToAdd = dateTimePickerHoursToAdd.Value.TimeOfDay;
                d.DateTotEnMet = dateTimePickerDatumTotEnMet.Value;
                d.DateVanafEnMet = dateTimePickerVanafEnMet.Value;
                d.omschrijveing = textBoxOpmerking.Text;
                d.isExurtie = checkBoxIsExcurtie.Checked;
                d.isStudieVerlof = checkBoxIsStudiVerlof.Checked;
                d.isFlexibelverlofoeorfsjklcghiur = checkBoxIsFlexiebelverlof.Checked;
                d.DaysOfEffect[0] = checkBoxMo.Checked;
                d.DaysOfEffect[1] = checkBoxDi.Checked;
                d.DaysOfEffect[2] = checkBoxWo.Checked;
                d.DaysOfEffect[3] = checkBoxDo.Checked;
                d.DaysOfEffect[4] = checkBoxVr.Checked;
                d.DaysOfEffect[5] = checkBoxZa.Checked;
                d.DaysOfEffect[6] = checkBoxZo.Checked;

                d.UserIDs = selectedUsers.Select(i => i.ID).ToList();
            }
            request.deEntry = d; // request.deEntry in the firstplcae
            NetCom.ServerResponseChangeModTable response = webr<NetCom.ServerResponseChangeModTable>(request);
            if (response.OK)
            {

            }
            else
            {
                MessageBox.Show("niet ok");
            }
            ClearInterface();
            LoadAlleDeEntrys();
            LoadDataTablesFromAlleDeEntrys();
        }

        private void ClearInterface()
        {
            curModEntryID = 0;
            buttonUpdate.Enabled = false;
            dateTimePickerDatumTotEnMet.Value = new DateTime(2000, 3, 19);
            dateTimePickerVanafEnMet.Value = new DateTime(2000, 3, 19);
            dateTimePickerHoursToAdd.Value = new DateTime(2000, 3, 19, 4, 20, 59);
            textBoxOpmerking.Text = "";
            checkBoxMo.Checked = false;
            checkBoxDi.Checked = false;
            checkBoxWo.Checked = false;
            checkBoxDo.Checked = false;
            checkBoxVr.Checked = false;
            checkBoxZa.Checked = false;
            checkBoxZo.Checked = false;
            checkBoxIsExcurtie.Checked = false;
            checkBoxIsFlexiebelverlof.Checked = false;
            checkBoxIsStudiVerlof.Checked = false;
            selectedUsers.Clear();
            LoadSelectedUsersFromSelectedUsers();
        }

        private void LoadAlleDeEntrys()
        {
            alleDeModifierEntrys = webr<NetCom.ServerResponseGetModTable>(new NetCom.ServerRequestGetModTable()).deEntrys;
            alleDeUserEntrys = webr<NetCom.ServerResponseGetUserTable>(new NetCom.ServerRequestGetUserTable()).deEntrys;
        }

        private void LoadDataTablesFromAlleDeEntrys()
        {

            //modifier list
            DataTable d2 = new DataTable();
            string d2ID = "ID";
            string d2DateVan = "DateVan";
            string d2DateTot = "DateTot";
            string d2DaysEffect = "Days Active";
            string d2UserIds = "Users";
            string d2HoursToAdd = "Hour Mod";
            string d2omschrijving = "Omschrijving";
            d2.Columns.Add(d2ID);
            d2.Columns.Add(d2DateVan);
            d2.Columns.Add(d2DateTot);
            d2.Columns.Add(d2DaysEffect);
            d2.Columns.Add(d2UserIds);
            d2.Columns.Add(d2HoursToAdd);
            d2.Columns.Add(d2omschrijving);
            foreach (var i in alleDeModifierEntrys)
            {
                DataRow row = d2.NewRow();
                row[d2ID] = i.ID.ToString();
                row[d2DateVan] = i.DateVanafEnMet.ToString();
                row[d2DateTot] = i.DateTotEnMet.ToString();
                row[d2DaysEffect] = $" {i.DaysOfEffect[0].ToString()} {i.DaysOfEffect[0].ToString()} {i.DaysOfEffect[0].ToString()} {i.DaysOfEffect[0].ToString()} ";
                row[d2HoursToAdd] = i.HoursToAdd.ToString();
                row[d2omschrijving] = i.omschrijveing;
                d2.Rows.Add(row);
            }
            dataGridViewModEntrys.DataSource = d2;
            dataGridViewModEntrys.Update();
            dataGridViewModEntrys.Refresh();

            //user list
            DataTable d1 = new DataTable();
            string d1ID = "ID";
            string d1Vnaam = "Vnaam";
            string d1Anaam = "Anaam";
            string d1Joindate = "JoinDate";
            string d1Leavedate = "LeaveDate";
            d1.Columns.Add(d1ID);
            d1.Columns.Add(d1Vnaam);
            d1.Columns.Add(d1Anaam);
            d1.Columns.Add(d1Joindate);
            d1.Columns.Add(d1Leavedate);
            foreach (var i in alleDeUserEntrys)
            {
                DataRow row = d1.NewRow();
                row[d1ID] = i.ID;
                row[d1Vnaam] = i.VoorNaam;
                row[d1Anaam] = i.AchterNaam;
                row[d1Joindate] = i.DateJoined.ToString();
                try
                {
                    row[d1Leavedate] = i.DateLeft.ToString();
                }
                catch
                {
                    row[d1Leavedate] = "Null";
                }
                d1.Rows.Add(row);
            }
            dataGridViewUsers.DataSource = d1;
            dataGridViewUsers.Update();
            dataGridViewUsers.Refresh();

        }

        private void LoadSelectedUsersFromSelectedUsers()
        {
            //copy pasta
            DataTable d1 = new DataTable();
            string d1ID = "ID";
            string d1Vnaam = "Vnaam";
            string d1Anaam = "Anaam";
            string d1Joindate = "JoinDate";
            string d1Leavedate = "LeaveDate";
            d1.Columns.Add(d1ID);
            d1.Columns.Add(d1Vnaam);
            d1.Columns.Add(d1Anaam);
            d1.Columns.Add(d1Joindate);
            d1.Columns.Add(d1Leavedate);
            foreach (var i in selectedUsers)
            {
                DataRow row = d1.NewRow();
                row[d1ID] = i.ID;
                row[d1Vnaam] = i.VoorNaam;
                row[d1Anaam] = i.AchterNaam;
                row[d1Joindate] = i.DateJoined.ToString();
                try
                {
                    row[d1Leavedate] = i.DateLeft.ToString();
                }
                catch
                {
                    row[d1Leavedate] = "Null";
                }
                d1.Rows.Add(row);
            }
            dataGridViewSelecetedUsers.DataSource = d1;
            dataGridViewSelecetedUsers.Update();
            dataGridViewSelecetedUsers.Refresh();
        }

        private void dataGridViewModEntrys_SelectionChanged(object sender, EventArgs e)
        {  // update interface
            if (dataGridViewModEntrys.SelectedRows.Count > 0)
            {
                DBDingus.ModifierTableEntry entry = alleDeModifierEntrys.FirstOrDefault(a => a.ID == Convert.ToInt32(dataGridViewModEntrys.SelectedRows[0].Cells[0].Value.ToString()));
                if (entry != null)
                {
                    buttonUpdate.Enabled = true;
                    curModEntryID = entry.ID;
                    selectedUsers = alleDeUserEntrys.Where(x => entry.UserIDs.Any(i => i == x.ID)).ToList();
                    LoadSelectedUsersFromSelectedUsers();
                    dateTimePickerDatumTotEnMet.Value = entry.DateTotEnMet;
                    dateTimePickerVanafEnMet.Value = entry.DateVanafEnMet;
                    dateTimePickerHoursToAdd.Value = Convert.ToDateTime(entry.HoursToAdd.ToString());
                    textBoxOpmerking.Text = entry.omschrijveing;
                    checkBoxMo.Checked = entry.DaysOfEffect[0];
                    checkBoxDi.Checked = entry.DaysOfEffect[1];
                    checkBoxWo.Checked = entry.DaysOfEffect[2];
                    checkBoxDo.Checked = entry.DaysOfEffect[3];
                    checkBoxVr.Checked = entry.DaysOfEffect[4];
                    checkBoxZa.Checked = entry.DaysOfEffect[5];
                    checkBoxZo.Checked = entry.DaysOfEffect[6];
                    checkBoxIsExcurtie.Checked = entry.isExurtie;
                    checkBoxIsFlexiebelverlof.Checked = entry.isFlexibelverlofoeorfsjklcghiur;
                    checkBoxIsStudiVerlof.Checked = entry.isStudieVerlof;
                }
            }
        }

        private void dataGridViewUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // add selection to selected data grid

            //object refrence not set to an inctance of an object????? selectedUsers was nog null.....
            int id = Convert.ToInt32(dataGridViewUsers.Rows[e.RowIndex].Cells[0].Value.ToString());

            bool jah = true;
            foreach (var i in selectedUsers)
            {
                if (i.ID == id) { jah = false; }
            }
            if (jah)
            {
                selectedUsers.Add(alleDeUserEntrys.FirstOrDefault(a => a.ID == id));
            }

            /*
            int id = Convert.ToInt32(dataGridViewUsers.Rows[e.RowIndex].Cells[0].Value.ToString());
            foreach(var x in alleDeUserEntrys) {
                if(x.ID == id) {
                    bool toevoegenjah = true;
                    foreach(var y in selectedUsers) {
                        if(y.ID == id) { toevoegenjah = false;}
                    }
                    if (toevoegenjah) {
                        selectedUsers.Add(x);
                    }
                }
            }
            */

            LoadSelectedUsersFromSelectedUsers();
        }

        private void dataGridViewSelecetedUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // remove from seleced list
            selectedUsers.RemoveAll(i => i.ID == Convert.ToInt32(dataGridViewSelecetedUsers.Rows[e.RowIndex].Cells[0].Value.ToString()));
            LoadSelectedUsersFromSelectedUsers();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            push(false, true);
        }

        private void buttonSaveNew_Click(object sender, EventArgs e)
        {
            push(true, false);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            push(false, false);
        }
    }
}
