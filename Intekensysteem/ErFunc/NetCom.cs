using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErFunc
{
    public class NetCom
    {
        public static ServerResponse WebRequest(ServerRequest _request, string _apiAddres)
        {
            using (HttpClient client = new HttpClient())
            {
                string teststststsst = JsonConvert.SerializeObject(_request);
                HttpResponseMessage response = client.PostAsync(_apiAddres, new StringContent(JsonConvert.SerializeObject(_request), Encoding.UTF8, "application/json")).Result;
                Task<string> result = response.Content.ReadAsStringAsync();
                //string banana = JsonConvert.DeserializeObject<string>(result.Result);
                return JsonConvert.DeserializeObject<ServerResponse>(result.Result);
            }
        }

        /* bleh
        private static ServerResponse WebRequest(ServerRequest _Request, string _APIAddres) {
            using (HttpClient httpClient = new HttpClient()) {
                httpClient.DefaultRequestHeaders.Add("X-Accept", "application/Json");                
                Task<HttpResponseMessage> response = httpClient.PostAsJsonAsync(_APIAddres, _Request);
                response.Wait();
                Task<string> result = response.Result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ServerResponse>(result.Result);
            }
        }
        */

        public static ServerResponse WebRequest(object request, string _Username, string _Password, string _ApiAddres)
        {
            ServerRequest reques = new ServerRequest();
            reques.UserName = _Username;
            reques.Password = _Password;
            reques.Request = request;
            return WebRequest(reques, _ApiAddres);
        }

        public class ServerRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public object Request { get; set; }
        }

        public class ServerResponse
        {
            public bool IsErrorOccurred { get; set; } = false;
            public ERRORINFO ErrorInfo { get; set; } = new ERRORINFO();
            public object Response { get; set; }
        }

        public class ERRORINFO
        {
            public string ErrorMessage { get; set; }
        }

        public interface IKnow
        {
            WhatIsThisEnum WatIsDit { get; }
        }

        public enum WhatIsThisEnum
        {
            RSqlServerDateTime,
            RInteken,
            ROneDateRegiOverzight,
            RMultiDateRegiOverzight,
            RChangeRegTable,

            GetUserTable, ChangeUserTable,
            GetModTable, ChangeModTable,
            GetAcountsTable, ChangeAcountTable,
            GetIsSchoolDagTable, ChangeIsSchoolDagTable
        }

        public class ServerRequestSqlDateTime : IKnow
        {
            public WhatIsThisEnum WatIsDit { get { return WhatIsThisEnum.RSqlServerDateTime; } }
        }

        public class serverResponseSqlDateTime
        {
            public DateTime SqlDateTime { get; set; }
        }


        public class ServerRequestOverzightFromMultipleDates : IKnow
        {
            public WhatIsThisEnum WatIsDit { get { return WhatIsThisEnum.RMultiDateRegiOverzight; } }
            public DateTime FromAndWithThisDate { get; set; }
            public DateTime TotEnMetDezeDatum { get; set; }
            public bool getForExUsers { get; set; }
        }

        public class ServerResponseOverzightFromMultipleDates
        {
            //baylife
            public List<ServerResponseOverzightFromMultipleDatesSubType> allesDatJeNodigHebt { get; set; } = new List<ServerResponseOverzightFromMultipleDatesSubType>();
        }

        public class ServerResponseOverzightFromMultipleDatesSubType
        {
            public ServerResponseOverzightFromOneDate OverZichtFromThisDate { get; set; } = new ServerResponseOverzightFromOneDate();
            public DateTime DateOfOverzight { get; set; }
        }


        public class ServerRequestTekenInOfUit : IKnow
        {
            public WhatIsThisEnum WatIsDit { get { return WhatIsThisEnum.RInteken; } }
            public string NFCCode { get; set; }
            public bool DateIsToday { get; set; } = true;
            public DateTime Date { get; set; }
        }

        public class ServerResponseInteken
        {
            public DBDingus.CombUserAfwEntry TheUserWithEntryInfo { get; set; } = new DBDingus.CombUserAfwEntry();
            public bool ingetekened { get; set; }
            public bool uitgetekened { get; set; }
            public bool uitekenengeanuleerd { get; set; }
        }


        public class ServerRequestOverzightFromOneDate : IKnow
        {
            public WhatIsThisEnum WatIsDit { get { return WhatIsThisEnum.ROneDateRegiOverzight; } }
            public bool useToday { get; set; } = false;
            public bool alsoReturnExUsers { get; set; } = false;
            public DateTime dateToGetOverzightFrom { get; set; }
        }

        public class ServerResponseOverzightFromOneDate
        {
            public List<DBDingus.CombUserAfwEntry> EtList { get; set; } = new List<DBDingus.CombUserAfwEntry>();
            public DateTime SQlDateTime { get; set; }
        }


        public class ServerRequestChangeRegistratieTable : IKnow
        {
            public WhatIsThisEnum WatIsDit { get { return WhatIsThisEnum.RChangeRegTable; } }
            public bool isNieuwEntry { get; set; } = false; //als true ignore DBDingus.RegistratieTableTableEntry.ID
            public bool newEntryDateIsToday { get; set; } = true;
            public DBDingus.RegistratieTableTableEntry deEntry { get; set; } = new DBDingus.RegistratieTableTableEntry();
        }

        public class ServerResponseChangeRegistratieTable
        {
            //public DBDingus.RegistratieTableTableEntry deEntry { get; set; } // voor DBDingus.RegistratieTableTableEntry.ID als het nietuw as //SCOPE_IDENTITY() wwerrk nie
        }

        //       newR

        //user
        public class ServerRequestGetUserTable : IKnow
        {
            public WhatIsThisEnum WatIsDit { get { return WhatIsThisEnum.GetUserTable; } }
            public bool aleenDieNogOpSchoolZitten { get; set; } = true;
        }

        public class ServerResponseGetUserTable
        {
            public List<DBDingus.UserTableTableEntry> deEntrys { get; set; } = new List<DBDingus.UserTableTableEntry>();
        }


        public class ServerRequestChangeUserTable : IKnow
        {
            public WhatIsThisEnum WatIsDit { get { return WhatIsThisEnum.ChangeUserTable; } }
            public bool IsNewUser { get; set; } = false;
            public bool DeleteEntry { get; set; } = false;
            public DBDingus.UserTableTableEntry deEntry { get; set; }
        }

        public class ServerResponseChangeUserTable
        {
            public bool OK { get; set; } = true;
        }


        //mod
        public class ServerRequestGetModTable : IKnow
        {
            public WhatIsThisEnum WatIsDit { get { return WhatIsThisEnum.GetModTable; } }
            public DateTime AllesWaarvanErEffectIsTussenDeze { get; set; }
            public DateTime EnDezeDatums { get; set; }
        }

        public class ServerResponseGetModTable
        {
            public List<DBDingus.ModifierTableEntry> deEntrys { get; set; } = new List<DBDingus.ModifierTableEntry>();
        }


        public class ServerRequestChangeModTable : IKnow
        {
            public WhatIsThisEnum WatIsDit { get { return WhatIsThisEnum.ChangeModTable; } }
            public bool IsNewEntry { get; set; } = false;
            public bool DeleteEntry { get; set; } = false;
            public DBDingus.ModifierTableEntry deEntry { get; set; } = new DBDingus.ModifierTableEntry();
        }

        public class ServerResponseChangeModTable
        {
            public bool OK { get; set; } = true;
        }


        //acount
        public class ServerRequestGetAcountsTable : IKnow
        {
            public WhatIsThisEnum WatIsDit { get { return WhatIsThisEnum.GetAcountsTable; } }
        }

        public class ServerResponseGetAcountTable
        {
            public List<DBDingus.AcountTableEntry> deEntrys { get; set; } = new List<DBDingus.AcountTableEntry>();
        }
        

        public class ServerRequestChangeAcountTable : IKnow
        {
            public WhatIsThisEnum WatIsDit { get { return WhatIsThisEnum.ChangeAcountTable; } }
            public bool IsNewEntry { get; set; } = false;
            public bool DeleteEntry { get; set; } = false;
            public DBDingus.AcountTableEntry deEntry { get; set; }
        }

        public class ServerResponseChangeAcountTable
        {
            public bool OK { get; set; } = true; // why?
        }


        //isSchoolDag
        public class ServerRequestGetIsSchoolDagTable : IKnow
        {
            public WhatIsThisEnum WatIsDit { get { return WhatIsThisEnum.GetIsSchoolDagTable; } }
            public DateTime year { get; set; }
            public bool byBetween { get; set; } = false;
            public DateTime dateFromAndWith { get; set; } = new DateTime();
            public DateTime dateTotEnMet { get; set; } = new DateTime();
        }

        public class ServerResponseGetIsSchoolDagTable
        {
            public List<DBDingus.IsSchoolDagTableEntry> DagenDateErSchoolIs { get; set; } = new List<DBDingus.IsSchoolDagTableEntry>();
        }


        public class ServerRequestChangeIsSchoolDagTable : IKnow
        {
            public WhatIsThisEnum WatIsDit { get { return WhatIsThisEnum.ChangeIsSchoolDagTable; } }
            //List<DateTime> toRemoveFromDB { get; set; } = new List<DateTime>(); // als er nog meer data makelijk bij gezet wilt worden
            public List<DBDingus.IsSchoolDagTableEntry> deleteList = new List<DBDingus.IsSchoolDagTableEntry>(); // only id is used to delete
            public List<DateTime> toAddToDB { get; set; } = new List<DateTime>();
        }

        public class ServerResponseChangeIsSchoolDagTable
        {

        }

    }
}
