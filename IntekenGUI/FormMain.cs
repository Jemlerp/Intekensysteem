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
using Newtonsoft.Json;
using System.Diagnostics;

namespace IntekenGUI
{
    public partial class FormMain : Form
    {
        public FormMain(string _serkialPort, string _apiAdress, string _username, string _password, bool _startInWindowMode)
        {
            InitializeComponent();
            _Serialport = new SerialPort(_serkialPort, 9600);
            _ApiAddres = _apiAdress;
            _Username = _username;
            _Password = _password;
            _WINDWEDMODUSENABLED = _startInWindowMode;
        }

        bool _NoConnectionMode = false;
        bool _WINDWEDMODUSENABLED = false;

        string _Username;
        string _Password;
        string _ApiAddres;
        SerialPort _Serialport = new SerialPort();
        Timer _TimerCleanUserInfoScreen = new Timer();
        Timer _TimerReloadOverzicht = new Timer();
        private delegate void handelTextDelegate(string read);
        private delegate void updateOverzichtDelegate();

        void enableNoConnectionMode()
        {
            _NoConnectionMode = true;
            panel1.Visible = false;
            this.BackColor = Color.Red;
            _TimerCleanUserInfoScreen.Stop();
            _TimerReloadOverzicht.Stop();
        }

        void disableNoConnectionMode()
        {
            _NoConnectionMode = false;
            panel1.Visible = true;
            this.BackColor = Color.Yellow; //times are outdated
            _TimerCleanUserInfoScreen.Start();
            _TimerReloadOverzicht.Start();
            updateOverzight();
        }

        List<DBDingus.CombUserAfwEntry> _LastRecivedOverzight = new List<DBDingus.CombUserAfwEntry>();

        private NetCom.ServerResponse webbbbrrrrrry(object request)
        {
            return NetCom.WebRequest(request, _Username, _Password, _ApiAddres);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                if (!_WINDWEDMODUSENABLED)
                {
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Maximized;
                }
                _Serialport.Open();
                _Serialport.DataReceived += new SerialDataReceivedEventHandler(readReadFromSerial);
                _TimerCleanUserInfoScreen.Interval = 4200;
                _TimerReloadOverzicht.Interval = 4000;
                _TimerCleanUserInfoScreen.Tick += new EventHandler(ClearUserInfo_Event);
                _TimerReloadOverzicht.Tick += new EventHandler(ReloadEtOverzicht_Event);
                updateOverzight();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        void ClearUserInfo_Event(object nee, object ja)
        {
            _TimerCleanUserInfoScreen.Stop();
            labelInOfUitGetekend.Text = "<In/Uit>";
            labelNaam.Text = "<Naam>";
        }

        void updateOverzight()
        {
            if (!_NoConnectionMode)
            {
                try
                {
                    _TimerReloadOverzicht.Stop();
                    NetCom.ServerRequestOverzightFromOneDate request = new NetCom.ServerRequestOverzightFromOneDate();
                    request.useToday = true;

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
                      // if (MessageBox.Show("Kan Niet Verbinden Met Server", "Ga Naar Alarm Modus?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)==DialogResult.Yes) {
                        enableNoConnectionMode();
                        //} else {
                        //this.BackColor=Color.Yellow;
                        //}
                        _TimerReloadOverzicht.Start();
                        return;
                    }

                    this.BackColor = SystemColors.Control; // all OK color

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
                        ///
                        returnedValue = JsonConvert.DeserializeObject<NetCom.ServerResponseOverzightFromOneDate>(JsonConvert.SerializeObject(response.Response));
                        dataGridView1.DataSource = FormHelp.UserInfoListToDataTableForDataGridDisplay(returnedValue.EtList, returnedValue.SQlDateTime);
                        dataGridView1.Refresh();
                        _LastRecivedOverzight = returnedValue.EtList;

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
                        dataGridView1.Columns[1].Width = 158;
                        dataGridView1.Columns[4].Width = dataGridView1.Width - dataGridView1.Columns[0].Width - dataGridView1.Columns[1].Width - dataGridView1.Columns[2].Width - dataGridView1.Columns[3].Width - 3 - 20;
                        _TimerReloadOverzicht.Start();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    _TimerReloadOverzicht.Start();
                }
            }
            else
            {
                if (FormHelp.CanConnectToServer(_ApiAddres))
                {
                    disableNoConnectionMode();
                    this.BackColor = Color.Yellow;//still outdated
                }
            }
        }

        void ReloadEtOverzicht_Event(object nee, object ja)
        {
            BeginInvoke(new updateOverzichtDelegate(updateOverzight));
        }

        void HandelNfcScan(string _read)
        {
            if (!_NoConnectionMode)
            {
                _TimerCleanUserInfoScreen.Stop();
                NetCom.ServerRequestTekenInOfUit request = new NetCom.ServerRequestTekenInOfUit();
                request.NFCCode = _read;

                NetCom.ServerResponse response;

                try
                {
                    response = webbbbrrrrrry(request);
                }
                catch
                {
                    //if (MessageBox.Show("Kan Niet Verbinden Met Server", "Ga Naar Alarm Modus?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)==DialogResult.Yes) {
                    enableNoConnectionMode();
                    // } else {
                    //     this.BackColor=Color.Yellow;
                    //}
                    return;
                }

                this.BackColor = SystemColors.Control;

                if (response.IsErrorOccurred)
                {
                    MessageBox.Show(response.ErrorInfo.ErrorMessage);
                }
                else
                {
                    NetCom.ServerResponseInteken intekenResponse = JsonConvert.DeserializeObject<NetCom.ServerResponseInteken>(JsonConvert.SerializeObject(response.Response));
                    labelNaam.Text = intekenResponse.TheUserWithEntryInfo.UsE.VoorNaam + " " + intekenResponse.TheUserWithEntryInfo.UsE.AchterNaam;
                    if (intekenResponse.uitekenengeanuleerd) { labelInOfUitGetekend.Text = "Uitekenen Geanuleerd"; }
                    if (intekenResponse.ingetekened) { labelInOfUitGetekend.Text = "Je Bent Nu Ingetekend"; }
                    if (intekenResponse.uitgetekened) { labelInOfUitGetekend.Text = "Je Bent Nu Uitgetekend"; }
                    _TimerCleanUserInfoScreen.Start();
                    updateOverzight();
                }
            }
        }

        void readReadFromSerial(object _ebjec, object _tokdekmak)
        {
            string Read = _Serialport.ReadLine();
            BeginInvoke(new handelTextDelegate(HandelNfcScan), FormHelp.SerialReadToNormal(Read));
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _Serialport.Close();
            _TimerCleanUserInfoScreen.Stop();
            _TimerReloadOverzicht.Stop();
        }

        private void closeProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _TimerCleanUserInfoScreen.Stop();
            _TimerReloadOverzicht.Stop();
            Close();
        }

        private void reconnectSensorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _Serialport.Close();
            }
            catch { }
            try
            {
                _Serialport.Open();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
