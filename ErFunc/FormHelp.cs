using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ErFunc
{
    public class FormHelp
    {
        public static bool testSerialPort(string port)
        {
            try
            {
                SerialPort porrt = new SerialPort(port, 9600);
                porrt.Open();
                porrt.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static T Requestion<T>(object request, string _UserName, string _Password, string _Address)
        {
            NetCom.ServerResponse response = NetCom.WebRequest(request, _UserName, _Password, _Address);
            if (response.IsErrorOccurred)
            {
                throw new Exception(response.ErrorInfo.ErrorMessage);
            }
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(response.Response));
        }

        public static bool CanConnectToServer(string ip)
        {
            NetCom.ServerRequest request = new NetCom.ServerRequest();
            request.Request = new NetCom.ServerRequestSqlDateTime();
            try
            {
                NetCom.ServerResponse response = NetCom.WebRequest(request, ip);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string SerialReadToNormal(string read)
        {
            string eetEenUi = "";
            char[] nummbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ' };
            foreach (char x in read)
            {
                foreach (char y in nummbers)
                {
                    if (y == x) { eetEenUi += x; break; }
                }
            }
            return eetEenUi.Trim();
        }

        public static DataTable UserInfoListToDataTableForDataGridDisplay(List<DBDingus.CombUserAfwEntry> _UserAndRegEntrys, DateTime _CurrentSQlDateTime)
        {
            DataTable ToReturn = new DataTable();
            ToReturn.Columns.Add("Voornaam");
            ToReturn.Columns.Add("Achternaam");
            ToReturn.Columns.Add("TijdIn");
            ToReturn.Columns.Add("TijdUit");
            ToReturn.Columns.Add("Totaal");
            string Voornaam = "Voornaam";
            string Achternaam = "Achternaam";
            string TijdIn = "TijdIn";
            string TijdUit = "TijdUit";
            string Totaal = "Totaal";
            foreach (var entry in _UserAndRegEntrys)
            {
                DataRow row = ToReturn.NewRow();
                row[Voornaam] = entry.UsE.VoorNaam;
                row[Achternaam] = entry.UsE.AchterNaam;
                if (entry.hasTodayRegEntry)
                {
                    string watAfwezig = "";
                    bool erIsEenAfwezigNotatie = false;
                    if (entry.RegE.IsZiek)
                    {
                        watAfwezig = "Z";
                    }
                    if (entry.RegE.IsFlexiebelverlof)
                    {
                        watAfwezig = "FV";
                    }
                    if (entry.RegE.IsStudieverlof)
                    {
                        watAfwezig = "SF";
                    }
                    if (entry.RegE.IsExcurtie)
                    {
                        watAfwezig = "EX";
                    }
                    if (entry.RegE.IsLaat)
                    {
                        watAfwezig = "Laat : " + entry.RegE.Verwachtetijdvanaanwezighijd.ToString("hh\\:mm\\:ss");
                    }
                    if (entry.RegE.IsToegestaalAfwezig)
                    {
                        watAfwezig = "TgstAfwzg" + " " + entry.RegE.Opmerking;
                    }
                    if (watAfwezig != "")
                    {
                        erIsEenAfwezigNotatie = true;
                        row[TijdIn] = watAfwezig;
                        row[TijdUit] = watAfwezig;
                        row[Totaal] = watAfwezig;
                    }
                    else
                    {
                        if (!entry.RegE.HeeftIngetekend)
                        {
                            row[TijdIn] = entry.RegE.Opmerking;
                            row[TijdUit] = entry.RegE.Opmerking;
                            row[Totaal] = entry.RegE.Opmerking;
                        }
                    }
                    if (entry.RegE.HeeftIngetekend)
                    {
                        row[TijdIn] = entry.RegE.TimeInteken.ToString("hh\\:mm");
                        if (entry.RegE.IsAanwezig)
                        {
                            if (!erIsEenAfwezigNotatie)
                            {
                                row[Totaal] = _CurrentSQlDateTime.TimeOfDay.Subtract(entry.RegE.TimeInteken).ToString("hh\\:mm\\:ss\\.fff");
                            }
                        }
                        else
                        {
                            row[TijdUit] = entry.RegE.TimeUitteken.ToString("hh\\:mm");
                            if (!erIsEenAfwezigNotatie)
                            {
                                row[Totaal] = entry.RegE.TimeUitteken.Subtract(entry.RegE.TimeInteken).ToString("hh\\:mm\\:ss\\.fff");
                            }
                        }
                    }
                }
                ToReturn.Rows.Add(row);
            }
            return ToReturn;
        }
    }
}
