﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ErFunc;
using System.Diagnostics;
using Newtonsoft.Json;

namespace AanspreekGUI
{
    public partial class FormMain : Form
    {
        public FormMain(string _password, string _username, string _apiadrres, bool _startInWindow)
        {
            InitializeComponent();
            _Password = _password;
            _Fullscreen = !_startInWindow;
            _Username = _username;
            _ApiAddres = _apiadrres;
        }

        bool _ALLOWCLEARINPUTSONRELOAD = true;
        private delegate void updateOverzichtDelegate();
        List<DBDingus.CombUserAfwEntry> _LastRecivedOverzight = new List<DBDingus.CombUserAfwEntry>(); // to compair with new one en als current selected veranderd is input clearen
        bool _LastRecivedOverzightDateIsToday = true;
        DateTime _LastRecivedOverzightDate = new DateTime();
        DBDingus.CombUserAfwEntry _CurrentlySelectedUser = new DBDingus.CombUserAfwEntry();
        bool _NoConnectionMode = false;
        Timer _TimerReloadOverzicht = new Timer();
        Timer _TimerShowWhenNextReloadOverzichtHappens = new Timer();

        bool _Fullscreen;
        string _Password = "";
        string _Username = "";
        string _ApiAddres = "";

        void enableNoConnectionMode()
        {
            _NoConnectionMode = true;
            panel2.Visible = false;
            this.BackColor = Color.Red;
            _TimerReloadOverzicht.Stop();
        }

        void disableNoConnectionMode()
        {
            _NoConnectionMode = false;
            panel2.Visible = true;
            this.BackColor = Color.Yellow; //times are outdated
            panel2.BackColor = Color.Yellow;
            _TimerReloadOverzicht.Start();
            ReloadOverzight();
        }

        void disableEditControls()
        {
            buttonTekenIn.Enabled = false;
            buttonTekenUit.Enabled = false;
            buttonSave.Enabled = false;
            dateTimePickerTijdIn.Enabled = false;
            dateTimePickerTimeUit.Enabled = false;
            comboBoxRedenAfwezig.Enabled = false;
            textBoxOpmerking.Text = "";
            textBoxOpmerking.Enabled = false;
            checkBoxHeefAfwezigReden.Checked = false;
            dateTimePickerVerwachteTijdVanAankomst.Enabled = false;
            buttonClearInEnUitTeken.Enabled = false;
        }

        private void FormStart_Load(object sender, EventArgs e)
        {
            if (_Fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }

            _TimerReloadOverzicht.Interval = 4000;
            _TimerReloadOverzicht.Tick += new EventHandler(ReloadOverzight_Event);

            _TimerShowWhenNextReloadOverzichtHappens.Interval = 1000;
            _TimerShowWhenNextReloadOverzichtHappens.Tick += new EventHandler(UpdateNextRealoadOverzightTimer);

            dateTimePickerTijdIn.ShowUpDown = true;
            dateTimePickerTijdIn.CustomFormat = "HH:mm:ss";
            dateTimePickerTijdIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;

            dateTimePickerTimeUit.ShowUpDown = true;
            dateTimePickerTimeUit.CustomFormat = "HH:mm:ss";
            dateTimePickerTimeUit.Format = System.Windows.Forms.DateTimePickerFormat.Custom;

            dateTimePickerVerwachteTijdVanAankomst.ShowUpDown = true;
            dateTimePickerVerwachteTijdVanAankomst.CustomFormat = "HH:mm:ss";
            dateTimePickerVerwachteTijdVanAankomst.Format = System.Windows.Forms.DateTimePickerFormat.Custom;

            comboBoxRedenAfwezig.SelectedItem = comboBoxRedenAfwezig.Items[1];
            ReloadOverzight();
        }

        private void ReloadOverzight_Event(object ja, object nee)
        {
            try
            {
                BeginInvoke(new updateOverzichtDelegate(ReloadOverzight));
            }
            catch
            {
            }
        }

        private void tekenInOfUit(object sender, EventArgs e)
        {
            if (!_NoConnectionMode)
            {
                NetCom.ServerRequestTekenInOfUit request = new NetCom.ServerRequestTekenInOfUit();
                request.NFCCode = _CurrentlySelectedUser.UsE.NFCID;
                NetCom.ServerResponse response;

                if (!checkBoxSeUserTodayAsDate.Checked)
                {
                    request.DateIsToday = false;
                    request.Date = dateTimePickerSeDateToListTo.Value.Date;
                }

                try
                {
                    response = webbbbrrrrrry(request);
                }
                catch
                {
                    enableNoConnectionMode();
                    return;
                }

                if (response.IsErrorOccurred)
                {
                    MessageBox.Show(response.ErrorInfo.ErrorMessage);
                }
                else
                {
                    ReloadOverzight();
                    dataGridView1_SelectionChanged(null, null);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReloadOverzight();
        }

        private void ReloadOverzight()
        {
            _TimerShowWhenNextReloadOverzichtHappens.Stop();
            ReloadOverzight(true);
            labelTimeToNextUpdate.Text = (_TimerReloadOverzicht.Interval / 1000).ToString();
            _TimerShowWhenNextReloadOverzichtHappens.Start();
        }

        private void ReloadOverzight(bool reloadFromServer)
        {
            if (!_NoConnectionMode)
            {
                _TimerReloadOverzicht.Stop();
                NetCom.ServerRequestOverzightFromOneDate request = new NetCom.ServerRequestOverzightFromOneDate();
                if (checkBoxSeUserTodayAsDate.Checked)
                {
                    request.useToday = true;
                }
                else
                {
                    request.useToday = false;
                    request.dateToGetOverzightFrom = dateTimePickerSeDateToListTo.Value.Date;
                    _LastRecivedOverzightDate = dateTimePickerSeDateToListTo.Value.Date;
                }
                _LastRecivedOverzightDateIsToday = checkBoxSeUserTodayAsDate.Checked;
                if (checkBoxSeShowExUsers.Checked)
                {
                    request.alsoReturnExUsers = true;
                }
                NetCom.ServerResponse response;
                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    response = webbbbrrrrrry(request);
                    label2.Text = sw.ElapsedMilliseconds.ToString();
                }
                catch
                { // als server down is (als school in brand staat...)
                  //   if (MessageBox.Show("Kan Niet Verbinden Met Server", "Ga Naar Alarm Modus?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                    enableNoConnectionMode();
                    //   } else {
                    //       this.BackColor = Color.Yellow;//times are outdated
                    //       panel2.BackColor = Color.Yellow;
                    //   }
                    _TimerReloadOverzicht.Start();
                    return;
                }
                NetCom.ServerResponseOverzightFromOneDate returnedValue;
                if (response.IsErrorOccurred)
                {
                    throw new Exception(response.ErrorInfo.ErrorMessage);
                }
                else
                {
                    //--
                    int _SelectedRowNummber = 0;
                    if (dataGridView1.CurrentCell != null)
                    {
                        _SelectedRowNummber = dataGridView1.CurrentCell.RowIndex;
                    }
                    ListSortDirection _oldSortOrder;
                    DataGridViewColumn _oldSortCol;
                    _oldSortOrder = dataGridView1.SortOrder == SortOrder.Ascending ?
                     ListSortDirection.Ascending : ListSortDirection.Descending;
                    _oldSortCol = dataGridView1.SortedColumn;
                    ///--
                    if (reloadFromServer)
                    {
                        returnedValue = JsonConvert.DeserializeObject<NetCom.ServerResponseOverzightFromOneDate>(JsonConvert.SerializeObject(response.Response));

                        _ALLOWCLEARINPUTSONRELOAD = false;

                        if (_LastRecivedOverzight != null && _CurrentlySelectedUser.UsE != null && _CurrentlySelectedUser.RegE != null && ((_LastRecivedOverzightDateIsToday == checkBoxSeUserTodayAsDate.Checked && _LastRecivedOverzightDateIsToday == true) || _LastRecivedOverzightDate == dateTimePickerSeDateToListTo.Value.Date))
                        {
                            #region try 2015/2016?
                            /*
                            try
                            {
                                foreach (var entry in returnedValue.EtList)
                                {
                                    if (entry.UsE.ID == _CurrentlySelectedUser.UsE.ID)
                                    {
                                        if (entry.hasTodayRegEntry == _CurrentlySelectedUser.hasTodayRegEntry)
                                        {
                                            if (entry.RegE.ID == _CurrentlySelectedUser.RegE.ID &&
                                                entry.RegE.HeeftIngetekend == _CurrentlySelectedUser.RegE.HeeftIngetekend &&
                                                entry.RegE.IsAanwezig == _CurrentlySelectedUser.RegE.IsAanwezig &&
                                                entry.RegE.IsExcurtie == _CurrentlySelectedUser.RegE.IsExcurtie &&
                                                entry.RegE.IsFlexiebelverlof == _CurrentlySelectedUser.RegE.IsStudieverlof &&
                                                entry.RegE.IsStudieverlof == _CurrentlySelectedUser.RegE.IsStudieverlof &&
                                                entry.RegE.IsLaat == _CurrentlySelectedUser.RegE.IsLaat &&
                                                entry.RegE.IsZiek == _CurrentlySelectedUser.RegE.IsZiek &&
                                                entry.RegE.Opmerking == _CurrentlySelectedUser.RegE.Opmerking)
                                            {
                                                if (entry.RegE.HeeftIngetekend)
                                                {
                                                    if (entry.RegE.TimeInteken == _CurrentlySelectedUser.RegE.TimeInteken)
                                                    {
                                                        if (entry.RegE.IsAanwezig == false)
                                                        {
                                                            if (entry.RegE.TimeUitteken == _CurrentlySelectedUser.RegE.TimeUitteken)
                                                            {
                                                                if (entry.RegE.IsLaat)
                                                                {
                                                                    if (entry.RegE.Verwachtetijdvanaanwezighijd == _CurrentlySelectedUser.RegE.Verwachtetijdvanaanwezighijd)
                                                                    {
                                                                        _ALLOWCLEARINPUTSONRELOAD = false;
                                                                    }
                                                                    break;
                                                                }
                                                                _ALLOWCLEARINPUTSONRELOAD = false;
                                                            }
                                                            break;
                                                        }
                                                        _ALLOWCLEARINPUTSONRELOAD = false;
                                                    }
                                                    break;
                                                }
                                                _ALLOWCLEARINPUTSONRELOAD = false;
                                            }
                                            break;
                                        }
                                        break;
                                    }
                                }
                            }
                            catch (Exception ex) { string kanker = ex.Message; _ALLOWCLEARINPUTSONRELOAD = true; }
                            */
                            #endregion


                            #region try 2017
                            try
                            {
                                DBDingus.CombUserAfwEntry entry = returnedValue.EtList.First(x => x.UsE.ID == _CurrentlySelectedUser.UsE.ID);
                                if (entry.RegE != null)
                                {
                                    if (entry.RegE.ID == _CurrentlySelectedUser.RegE.ID &&
                                                    entry.RegE.HeeftIngetekend == _CurrentlySelectedUser.RegE.HeeftIngetekend &&
                                                    entry.RegE.IsAanwezig == _CurrentlySelectedUser.RegE.IsAanwezig &&
                                                    entry.RegE.IsExcurtie == _CurrentlySelectedUser.RegE.IsExcurtie &&
                                                    entry.RegE.IsFlexiebelverlof == _CurrentlySelectedUser.RegE.IsStudieverlof &&
                                                    entry.RegE.IsStudieverlof == _CurrentlySelectedUser.RegE.IsStudieverlof &&
                                                    entry.RegE.IsLaat == _CurrentlySelectedUser.RegE.IsLaat &&
                                                    entry.RegE.IsZiek == _CurrentlySelectedUser.RegE.IsZiek &&
                                                    entry.RegE.Opmerking == _CurrentlySelectedUser.RegE.Opmerking)
                                    {
                                        _ALLOWCLEARINPUTSONRELOAD = false;
                                    }
                                    else
                                    {
                                        _ALLOWCLEARINPUTSONRELOAD = true;
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                _ALLOWCLEARINPUTSONRELOAD = true;
                            }
                            #endregion
                        }

                        _LastRecivedOverzight = returnedValue.EtList;

                    }
                    else
                    {
                        returnedValue = new NetCom.ServerResponseOverzightFromOneDate();
                        returnedValue.EtList = _LastRecivedOverzight;
                    }
                    if (textBoxZoekOp.Text.Trim() == "")
                    {
                        dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
                        dataGridView1.DataSource = FormHelp.UserInfoListToDataTableForDataGridDisplay(returnedValue.EtList, returnedValue.SQlDateTime);
                        dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
                        dataGridView1.Refresh();
                    }
                    else
                    { //zoek op
                        List<DBDingus.CombUserAfwEntry> sortedList = new List<DBDingus.CombUserAfwEntry>();
                        foreach (DBDingus.CombUserAfwEntry perso in returnedValue.EtList)
                        {
                            if (perso.UsE.VoorNaam.Contains(textBoxZoekOp.Text.Trim()) || perso.UsE.AchterNaam.Contains(textBoxZoekOp.Text.Trim()))
                            {
                                sortedList.Add(perso);
                            }
                        }
                        dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
                        dataGridView1.DataSource = FormHelp.UserInfoListToDataTableForDataGridDisplay(sortedList, returnedValue.SQlDateTime);
                        dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
                        dataGridView1.Refresh();
                    }
                    this.BackColor = SystemColors.Control; // times are uptodate
                    panel2.BackColor = SystemColors.Control;
                    //--
                    if (_oldSortCol != null)
                    {
                        DataGridViewColumn newCol = dataGridView1.Columns[_oldSortCol.Name];
                        dataGridView1.Sort(newCol, _oldSortOrder);
                    }
                    try
                    {// voor als row[x] er niet (meer) is
                        if (dataGridView1.CurrentCell != null)
                        {
                            dataGridView1.CurrentCell = dataGridView1[1, _SelectedRowNummber];
                        }
                    }
                    catch
                    {
                        dataGridView1.ClearSelection();
                    }
                    ///--
                    dataGridView1.Columns[0].Width = 130;
                    dataGridView1.Columns[1].Width = 140;
                    dataGridView1.Columns[2].Width = 130;
                    dataGridView1.Columns[3].Width = 130;
                    dataGridView1.Columns[4].Width = dataGridView1.Width - dataGridView1.Columns[0].Width - dataGridView1.Columns[1].Width - dataGridView1.Columns[2].Width - dataGridView1.Columns[3].Width - 3 - 20;
                    _TimerReloadOverzicht.Start();
                }
            }

            else
            {
                if (FormHelp.CanConnectToServer(_ApiAddres))
                {
                    disableNoConnectionMode();
                    this.BackColor = Color.Yellow;//still outdated
                    panel2.BackColor = Color.Yellow;//why?
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (_CurrentlySelectedUser.UsE == null) { return; }
            if (!_NoConnectionMode)
            {
                NetCom.ServerRequestChangeRegistratieTable request = new NetCom.ServerRequestChangeRegistratieTable();
                //set all control values back in errr object
                if (_CurrentlySelectedUser.HasTodayRegEntry)
                {
                    request.isNieuwEntry = false;
                    request.deEntry = _CurrentlySelectedUser.RegE;
                }
                else
                {
                    request.isNieuwEntry = true;
                    request.deEntry = new DBDingus.RegistratieTableTableEntry();
                    request.deEntry.IDOfUserRelated = _CurrentlySelectedUser.UsE.ID;
                    request.newEntryDateIsToday = checkBoxSeUserTodayAsDate.Checked;
                    if (!checkBoxSeUserTodayAsDate.Checked)
                    {
                        request.deEntry.Date = dateTimePickerSeDateToListTo.Value.Date;
                    }
                }
                request.deEntry.Opmerking = textBoxOpmerking.Text;
                if (dateTimePickerTijdIn.Enabled)
                {
                    request.deEntry.TimeInteken = dateTimePickerTijdIn.Value.TimeOfDay;
                }
                if (dateTimePickerTimeUit.Enabled)
                {
                    request.deEntry.TimeUitteken = dateTimePickerTimeUit.Value.TimeOfDay;
                }
                request.deEntry.IsLaat = false;
                request.deEntry.IsZiek = false;
                request.deEntry.IsStudieverlof = false;
                request.deEntry.IsFlexiebelverlof = false;
                request.deEntry.IsExcurtie = false;
                request.deEntry.IsToegestaalAfwezig = false;
                if (checkBoxHeefAfwezigReden.Checked)
                {
                    switch (comboBoxRedenAfwezig.SelectedItem.ToString())
                    {
                        case "Laat":
                            request.deEntry.IsLaat = true;
                            request.deEntry.Verwachtetijdvanaanwezighijd = dateTimePickerVerwachteTijdVanAankomst.Value.TimeOfDay;
                            break;
                        case "Ziek":
                            request.deEntry.IsZiek = true;
                            break;
                        case "StudieVerlof":
                            request.deEntry.IsStudieverlof = true;
                            break;
                        case "FlexibelVerlof":
                            request.deEntry.IsFlexiebelverlof = true;
                            break;
                        case "Excursie":
                            request.deEntry.IsExcurtie = true;
                            break;
                        case "Toegestaan Afwezig":
                            request.deEntry.IsToegestaalAfwezig = true;
                            break;
                    }
                }
                //put in trycatch for noodmodus
                NetCom.ServerResponse response;

                try
                {
                    response = webbbbrrrrrry(request);
                }
                catch
                {
                    // if (MessageBox.Show("Kan Niet Verbinden Met Server", "Ga Naar Alarm Modus?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                    enableNoConnectionMode();
                    // }
                    return;
                }

                if (response.IsErrorOccurred)
                {
                    MessageBox.Show(response.ErrorInfo.ErrorMessage);
                }
                ReloadOverzight();

            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _TimerReloadOverzicht.Stop();
            _TimerShowWhenNextReloadOverzichtHappens.Stop();
        }

        private void textBoxZoekOp_TextChanged(object sender, EventArgs e)
        {
            ReloadOverzight(false);
        }

        private void buttonClearInEnUitTeken_Click(object sender, EventArgs e)
        {
            if (!_NoConnectionMode)
            {
                DialogResult dialogResult = MessageBox.Show("Verwijder In En Uit Tijden", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (dialogResult == DialogResult.Yes)
                {
                    NetCom.ServerRequestChangeRegistratieTable request = new NetCom.ServerRequestChangeRegistratieTable();
                    request.isNieuwEntry = false;
                    request.deEntry = _CurrentlySelectedUser.RegE;
                    request.deEntry.HeeftIngetekend = false;
                    request.deEntry.IsAanwezig = false;
                    request.deEntry.TimeInteken = new TimeSpan();
                    request.deEntry.TimeUitteken = new TimeSpan();
                    NetCom.ServerResponse response;

                    try
                    {
                        response = webbbbrrrrrry(request);
                    }
                    catch
                    {
                        //if (MessageBox.Show("Kan Niet Verbinden Met Server", "Ga Naar Alarm Modus?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                        enableNoConnectionMode();
                        // }
                        return;
                    }

                    if (response.IsErrorOccurred)
                    {
                        MessageBox.Show(response.ErrorInfo.ErrorMessage);
                    }
                    ReloadOverzight();
                }
                else if (dialogResult == DialogResult.No)
                {

                }

            }
        }

        private void UpdateNextRealoadOverzightTimer(object nee, object ja)
        {
            labelTimeToNextUpdate.Text = (Convert.ToInt32(labelTimeToNextUpdate.Text) - 1).ToString();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0) { return; }
                DBDingus.CombUserAfwEntry selectedUserData = new DBDingus.CombUserAfwEntry();
                string _voorNaam = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string _achterNaam = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                bool found = false;
                foreach (DBDingus.CombUserAfwEntry entry in _LastRecivedOverzight)
                {
                    if (entry.UsE.VoorNaam == _voorNaam && entry.UsE.AchterNaam == _achterNaam) { selectedUserData = entry; found = true; break; }
                }
                if (!found) { MessageBox.Show("Cant Find Selected User"); return; } //bleh            
                _CurrentlySelectedUser = selectedUserData;

                //quick fix````````````````````````````````````````````````````````````````
                buttonTekenIn.Enabled = false;
                buttonTekenUit.Enabled = false;
                if (selectedUserData.HasTodayRegEntry)
                {
                    if (selectedUserData.RegE.HeeftIngetekend)
                    {
                        buttonClearInEnUitTeken.Enabled = true;
                        if (selectedUserData.RegE.IsAanwezig)
                        {
                            buttonTekenUit.Enabled = true;
                        }
                        else
                        {
                            buttonTekenIn.Enabled = true; // anulleer uitteken
                        }
                    }
                    else
                    {
                        buttonTekenIn.Enabled = true;
                    }
                }
                else
                {
                    buttonTekenIn.Enabled = true;
                }

                ///\quick fix ````````````````````````````````````````````````````````

                if (_ALLOWCLEARINPUTSONRELOAD)
                {

                    disableEditControls();

                    labelVoorNaam.Text = selectedUserData.UsE.VoorNaam;
                    labelAchterNaam.Text = selectedUserData.UsE.AchterNaam;

                    buttonSave.Enabled = true;
                    textBoxOpmerking.Enabled = true;
                    //enable buttons / set values

                    if (selectedUserData.HasTodayRegEntry)
                    {
                        buttonSave.Enabled = true;
                        textBoxOpmerking.Text = selectedUserData.RegE.Opmerking;
                        textBoxOpmerking.Enabled = true;

                        //select item in dropdown
                        bool erIsEenAfwezigNotatie = true;
                        if (selectedUserData.RegE.IsZiek)
                        {
                            comboBoxRedenAfwezig.SelectedItem = comboBoxRedenAfwezig.Items[1];
                        }
                        else
                        if (selectedUserData.RegE.IsFlexiebelverlof)
                        {
                            comboBoxRedenAfwezig.SelectedItem = comboBoxRedenAfwezig.Items[3];
                        }
                        else
                        if (selectedUserData.RegE.IsStudieverlof)
                        {
                            comboBoxRedenAfwezig.SelectedItem = comboBoxRedenAfwezig.Items[2];
                        }
                        else
                        if (selectedUserData.RegE.IsExcurtie)
                        {
                            comboBoxRedenAfwezig.SelectedItem = comboBoxRedenAfwezig.Items[4];
                        }
                        else
                        if (selectedUserData.RegE.IsLaat)
                        {
                            comboBoxRedenAfwezig.SelectedItem = comboBoxRedenAfwezig.Items[0];
                            dateTimePickerVerwachteTijdVanAankomst.Value = Convert.ToDateTime(selectedUserData.RegE.Verwachtetijdvanaanwezighijd.ToString("hh\\:mm\\:ss"));
                        }
                        else
                        if (selectedUserData.RegE.IsToegestaalAfwezig)
                        {
                            comboBoxRedenAfwezig.SelectedItem = comboBoxRedenAfwezig.Items[5];
                        }
                        else
                        {
                            erIsEenAfwezigNotatie = false;
                        }

                        if (erIsEenAfwezigNotatie)
                        {
                            checkBoxHeefAfwezigReden.Checked = true;
                        }

                        if (selectedUserData.RegE.HeeftIngetekend)
                        {
                            buttonClearInEnUitTeken.Enabled = true;
                            dateTimePickerTijdIn.Enabled = true;
                            dateTimePickerTijdIn.Value = Convert.ToDateTime(selectedUserData.RegE.TimeInteken.ToString("hh\\:mm\\:ss"));
                            if (selectedUserData.RegE.IsAanwezig)
                            {
                                buttonTekenUit.Enabled = true;
                            }
                            else
                            {
                                dateTimePickerTimeUit.Enabled = true;
                                dateTimePickerTimeUit.Value = Convert.ToDateTime(selectedUserData.RegE.TimeUitteken.ToString("hh\\:mm\\:ss"));
                                buttonTekenIn.Enabled = true; // anulleer uitteken
                            }
                        }
                        else
                        {
                            buttonTekenIn.Enabled = true;
                        }
                    }
                    else
                    {
                        buttonTekenIn.Enabled = true;
                    }
                }
                _ALLOWCLEARINPUTSONRELOAD = true;
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }
        }

        private void closeProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _TimerReloadOverzicht.Stop();
            _TimerReloadOverzicht.Stop();
            Close();
        }

        private void checkBoxHeefAfwezigReden_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHeefAfwezigReden.Checked)
            {
                comboBoxRedenAfwezig.Enabled = true;
            }
            else
            {
                comboBoxRedenAfwezig.Enabled = false;
            }
            comboBoxRedenAfwezig_SelectedIndexChanged(comboBoxRedenAfwezig, new EventArgs());
        }

        private void checkBoxSeUserTodayAsDate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSeUserTodayAsDate.Checked)
            {
                dateTimePickerSeDateToListTo.Enabled = false;
            }
            else
            {
                dateTimePickerSeDateToListTo.Enabled = true;
            }
            ReloadOverzight();
        }

        private void comboBoxRedenAfwezig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRedenAfwezig.Text == "Laat" && comboBoxRedenAfwezig.Enabled == true)
            {
                dateTimePickerVerwachteTijdVanAankomst.Enabled = true;
            }
            else
            {
                dateTimePickerVerwachteTijdVanAankomst.Enabled = false;
            }
        }

        private void dateTimePickerSeDateToListTo_ValueChanged(object sender, EventArgs e)
        {
            ReloadOverzight();
        }

        private NetCom.ServerResponse webbbbrrrrrry(object request)
        {
            return NetCom.WebRequest(request, _Username, _Password, _ApiAddres);
        }
    }
}
