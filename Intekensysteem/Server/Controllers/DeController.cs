using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ErFunc;
using System.Net;

namespace Server.Controllers
{
    public class HetController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Index()
        {
            DBDingus.AcountTableEntry sysUser = new DBDingus.AcountTableEntry();

            NetCom.ServerRequestOverzightFromOneDate request = new NetCom.ServerRequestOverzightFromOneDate();
            request.useToday = true;
            request.alsoReturnExUsers = false;

            NetCom.ServerResponseOverzightFromOneDate resp = FuncsVController.overzight(sysUser, request);

            DateTime lkNu = FuncsVController.GetDateTimeFromSqlDatabase();

            List<string> DaiNiBan = new List<string>();

            string DaiIkan = lkNu.ToString() + "\r\n \n";

            foreach (var x in resp.EtList)
            {
                string toAdd = "";
                toAdd = x.UsE.VoorNaam + "  " + x.UsE.AchterNaam;
                if (x.hasTodayRegEntry)
                {
                    if (x.RegE.HeeftIngetekend)
                    {
                        toAdd += "   In:" + x.RegE.TimeInteken.ToString("hh\\:mm\\:ss");
                        if (x.RegE.IsAanwezig)
                        {
                            toAdd += "   Uit:Aanwezig   Totaal:" + lkNu.Subtract(x.RegE.TimeInteken).ToString("hh\\:mm\\:ss\\.fff");
                        }
                        else
                        {
                            toAdd += $"   Uit:{x.RegE.TimeUitteken.ToString("hh\\:mm\\:ss")}   Totaal:" + x.RegE.TimeUitteken.Subtract(x.RegE.TimeInteken).ToString("hh\\:mm\\:ss\\.fff");
                        }
                    }
                }
                DaiNiBan.Add(toAdd + "\r\n \n");
            }

            foreach (var x in DaiNiBan.OrderBy(x => x.Length))
            {
                DaiIkan += x;
            }

            HttpResponseMessage toReturn = new HttpResponseMessage(HttpStatusCode.OK);
            toReturn.Content = new StringContent(DaiIkan, System.Text.Encoding.UTF8, "text/plain");
            return toReturn;
        }

        private T Deserialise<T>(string _toDeserialie)
        {
            return JsonConvert.DeserializeObject<T>(_toDeserialie);
        }

        private string Serilalise(object _toSerialise)
        {
            return JsonConvert.SerializeObject(_toSerialise);
        }

        private DBDingus.AcountTableEntry GetUser(string _username, string _password)
        {
            return default(DBDingus.AcountTableEntry);
        }

        [HttpPost]
        public HttpResponseMessage tokidokiaru([FromBody]NetCom.ServerRequest _request)
        {
            NetCom.ServerResponse toReturn = new NetCom.ServerResponse();
            toReturn.IsErrorOccurred = false;
            try
            {
                DBDingus.AcountTableEntry usingUser = GetUser(_request.UserName, _request.Password);
                string param = Serilalise(_request.Request);
                JObject baylife = JObject.Parse(param);
                switch ((NetCom.WhatIsThisEnum)Enum.Parse(typeof(NetCom.WhatIsThisEnum), (string)baylife["WatIsDit"]))
                {
                    case NetCom.WhatIsThisEnum.RSqlServerDateTime:
                        toReturn.Response = FuncsVController.GetDateTimeFromSqlDatabase();
                        break;
                    case NetCom.WhatIsThisEnum.RInteken:
                        toReturn.Response = FuncsVController.inteken(usingUser, Deserialise<NetCom.ServerRequestTekenInOfUit>(param));
                        break;
                    case NetCom.WhatIsThisEnum.ROneDateRegiOverzight:
                        toReturn.Response = FuncsVController.overzight(usingUser, Deserialise<NetCom.ServerRequestOverzightFromOneDate>(param));
                        break;
                    case NetCom.WhatIsThisEnum.RChangeRegTable:
                        toReturn.Response = FuncsVController.ChangeRegistatieTable(usingUser, Deserialise<NetCom.ServerRequestChangeRegistratieTable>(param));
                        break;
                    case NetCom.WhatIsThisEnum.RMultiDateRegiOverzight:
                        toReturn.Response = FuncsVController.alleDagOverzightenVanTussenTweDatums(usingUser, Deserialise<NetCom.ServerRequestOverzightFromMultipleDates>(param));
                        break;
                    //newr
                    case NetCom.WhatIsThisEnum.GetUserTable:
                        toReturn.Response = FuncsVController.GetUserTable(usingUser, Deserialise<NetCom.ServerRequestGetUserTable>(param));
                        break;
                    case NetCom.WhatIsThisEnum.ChangeUserTable:
                        toReturn.Response = FuncsVController.ChangeUserTable(usingUser, Deserialise<NetCom.ServerRequestChangeUserTable>(param));
                        break;
                    case NetCom.WhatIsThisEnum.GetModTable:
                        toReturn.Response = FuncsVController.GetModtable(usingUser, Deserialise<NetCom.ServerRequestGetModTable>(param));
                        break;
                    case NetCom.WhatIsThisEnum.ChangeModTable:
                        toReturn.Response = FuncsVController.ChangeModtable(usingUser, Deserialise<NetCom.ServerRequestChangeModTable>(param));
                        break;
                    case NetCom.WhatIsThisEnum.GetAcountsTable:
                        toReturn.Response = FuncsVController.GetAcountTable(usingUser, Deserialise<NetCom.ServerRequestGetAcountsTable>(param));
                        break;
                    case NetCom.WhatIsThisEnum.ChangeAcountTable:
                        toReturn.Response = FuncsVController.ChangeAcountTable(usingUser, Deserialise<NetCom.ServerRequestChangeAcountTable>(param));
                        break;
                    default: throw new Exception("bla bla bla lala la lal aaaaa noob");
                }
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(toReturn), System.Text.Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                toReturn.IsErrorOccurred = true;
                toReturn.ErrorInfo.ErrorMessage = ex.Message;
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(toReturn), System.Text.Encoding.UTF8, "application/json");
                return response;
            }
        }
    }
}
