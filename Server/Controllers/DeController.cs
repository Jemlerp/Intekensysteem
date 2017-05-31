using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ErFunc;

namespace Server.Controllers
{
    public class DeController : System.Web.Mvc.Controller
    {
        public class HomeController : System.Web.Mvc.Controller
        {
            public string Index()
            {
                return "Ja";
            }
        }
    }

    public class ValuesController : ApiController
    {
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
        public NetCom.ServerResponse tokidokiaru([FromBody]NetCom.ServerRequest _request)
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
                }
                return toReturn;
                throw new Exception("ahodashi");
            }
            catch (Exception ex)
            {
                toReturn.IsErrorOccurred = true;
                toReturn.ErrorInfo.ErrorMessage = ex.Message;
                return toReturn;
            }
        }
    }
}
