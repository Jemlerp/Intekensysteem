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
    public partial class FormModAcounts : Form
    {
        public FormModAcounts(string _username, string _password, string _address)
        {
            InitializeComponent();
            _UserName = _username;
            _Password = _password;
            _Address = _address;
        }

        string _Password = "";
        string _Address = "";
        string _UserName = "";

        List<DBDingus.AcountTableEntry> alleDeEntrys;


        private T webr<T>(object request)
        {
            return FormHelp.Requestion<T>(request, _UserName, _Password, _Address);
        }

        private void FormModAcounts_Load(object sender, EventArgs e)
        {
            refreshList();
        }

        public void refreshList()
        {
            try
            {
                alleDeEntrys = webr<NetCom.ServerResponseGetAcountTable>(new NetCom.ServerRequestGetAcountsTable()).deEntrys;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            DataTable dieWeSet = new DataTable();

            dieWeSet.Columns.Add("ID");
            dieWeSet.Columns.Add("Naam");
            dieWeSet.Columns.Add("InlogNaam");
            //dieWeSet.Columns.Add("InlogWachtwoord");
            dieWeSet.Columns.Add("AansprBevoeg");
            dieWeSet.Columns.Add("AdminBevoeg");

            string ID = "ID";
            string Naam = "Naam";
            string InlogNaam = "InlogNaam";
            //string InlogWachtwoord = "";
            string AansprBevoeg = "AansprBevoeg";
            string AdminBevoeg = "AdminBevoeg";

            foreach (var i in alleDeEntrys)
            {
                DataRow row = dieWeSet.NewRow();
                row[ID] = i.ID;
                row[Naam] = i.Naam;
                row[InlogNaam] = i.inlogNaam;
                row[AansprBevoeg] = i.aanspreekpuntBevoegdhijd;
                row[AdminBevoeg] = i.adminBevoegdhijd;
                dieWeSet.Rows.Add(row);
            }

            dataGridView1.DataSource = dieWeSet;
            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        private void push(bool atarashi)
        {
            push(atarashi, false);
        }

        private void push(bool isNew, bool delete)
        {
            try
            {
                NetCom.ServerRequestChangeAcountTable request = new NetCom.ServerRequestChangeAcountTable();
                DBDingus.AcountTableEntry deEntry = new DBDingus.AcountTableEntry();

                if (delete)
                {
                    deEntry.ID = deEntry.ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    request.DeleteEntry = true;
                }
                else
                {

                    request.IsNewEntry = isNew;

                    if (isNew)
                    {
                        deEntry.Naam = textBoxNewNaam.Text;
                        deEntry.inlogNaam = textBoxNewInlogNaam.Text;
                        deEntry.inlogWachtwoord = textBoxNewInlogWachtwoord.Text;
                        deEntry.aanspreekpuntBevoegdhijd = Convert.ToInt32(textBoxNewaansprelvl.Text);
                        deEntry.adminBevoegdhijd = Convert.ToInt32(textBoxNewAdminlbvl.Text);
                    }
                    else
                    {
                        deEntry.ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        deEntry.Naam = textBoxUpdateNaam.Text;
                        deEntry.inlogNaam = textBoxUpdateInlogNaam.Text;
                        if(textBoxUpdateInlogWachtwoord .Text != "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")
                        {
                            deEntry.inlogWachtwoord = textBoxUpdateInlogWachtwoord.Text;
                        }
                        else
                        {
                            deEntry.inlogWachtwoord = alleDeEntrys.First(x => x.ID == deEntry.ID).inlogWachtwoord; // not changed moet een bool zijn en pw moet niet gestuurd worden.... 
                        }
                        deEntry.aanspreekpuntBevoegdhijd = Convert.ToInt32(textBoxUpdateAnsprlvl.Text);
                        deEntry.adminBevoegdhijd = Convert.ToInt32(textBoxUpdateAdminlvl.Text);

                    }
                }

                request.deEntry = deEntry;

                NetCom.ServerResponseChangeAcountTable response = webr<NetCom.ServerResponseChangeAcountTable>(request);

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
            }
            refreshList();
        }

        private void buttonUopdate_Click(object sender, EventArgs e)
        {
            push(false);
        }

        private void buttonSaveNew_Click(object sender, EventArgs e)
        {
            push(true);
            textBoxNewInlogNaam.Text = "";
            textBoxNewInlogWachtwoord.Text = "";
            textBoxNewNaam.Text = "";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    textBoxUpdateID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    textBoxUpdateNaam.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    textBoxUpdateInlogNaam.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    textBoxUpdateAnsprlvl.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                    textBoxUpdateAdminlvl.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();

                    textBoxUpdateInlogWachtwoord.Text = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"; // get pw on save~update
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "dit had niet moeten gebeuren");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            push(false, true);
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            refreshList();
        }
    }
}
