using ErFunc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErFunc;
using System.Windows.Forms;

namespace AdminGUI
{
    public partial class FormSetSchoolDagen : Form
    {
        public FormSetSchoolDagen(string _username, string _password, string _address)
        {
            InitializeComponent();
            _UserName = _username;
            _Password = _password;
            _Address = _address;
        }

        string _Password = "";
        string _Address = "";
        string _UserName = "";

        List<DBDingus.IsSchoolDagTableEntry> fromServerEntrys = new List<DBDingus.IsSchoolDagTableEntry>();

        void ReloadAlleDeButtons (Point startLoc)
        {
            //clear all buttons from panles 
            //for every month \\ panel[month].contols.add \\ set colors on thte buttons based on hte fromServerEntrys


        }

        private T webr<T>(object request)
        {
            return FormHelp.Requestion<T>(request, _UserName, _Password, _Address);
        }

        void push()
        {
            try
            {
                NetCom.ServerRequestChangeIsSchoolDagTable request = new NetCom.ServerRequestChangeIsSchoolDagTable();
                foreach (Button x in Controls.OfType<Button>())
                {
                    if (x.Name[0] == 'c')
                    {
                        DateTime deateTime = new DateTime(DateTime.Now.Year, 1, 1).AddDays(Convert.ToInt32(x.Name.Split(';')[1]) - 1);

                        if (fromServerEntrys.Any(y => y.Date == deateTime))
                        {
                            request.deleteList.Add(fromServerEntrys.First(z => z.Date == deateTime));
                        }
                        else
                        {
                            request.toAddToDB.Add(deateTime);
                        }
                    }
                }

                NetCom.ServerResponseChangeIsSchoolDagTable response = webr<NetCom.ServerResponseChangeIsSchoolDagTable>(request);
                

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void buttonPressed(object sender, EventArgs e)
        {
            Button buttt = sender as Button;
            buttt.BackColor = (buttt.BackColor == Color.Red) ? Color.Plum : Color.Red;
        }
    }
}
