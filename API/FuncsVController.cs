using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using ErFunc;

namespace API
{
    public class FuncsVController
    {
        public static DBDingus _DBDingus = new DBDingus();

        public static DateTime GetDateTimeFromSqlDatabase()
        {
            return FuncsVSQL.GetDateTimeFromSQLServer();
        }

        public static IEnumerable<DateTime> ElkeDatumTussenTweDatums(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public static NetCom.ServerResponseInteken inteken(DBDingus.AcountTableEntry _userAcount, NetCom.ServerRequestTekenInOfUit _request)
        {
            NetCom.ServerResponseInteken toReturn = new NetCom.ServerResponseInteken();
            SqlCommand command;

            //get userID of scan
            command = new SqlCommand();
            command.Parameters.AddWithValue("@nfcode", _request.NFCCode);
            command.CommandText = $"select * from {DBDingus.UserTableNames.UserTableName} where {DBDingus.UserTableNames.NFCID} = @nfcode";
            List<DBDingus.UserTableTableEntry> foundUsers = FuncsVSQL.GetListUTFromReader(command); //_DBDingus.GetListUTFromReader(FuncsVSQL.SQLQuery(command));
            if (foundUsers.Count > 0)
            {
                toReturn.TheUserWithEntryInfo.UsE = foundUsers[0];
            }
            else
            {
                throw new Exception("Card Unknown");
            }

            //inteken/uiteken
            command = new SqlCommand();
            command.Parameters.AddWithValue("@userid", toReturn.TheUserWithEntryInfo.UsE.ID);
            command.CommandText = $"select * from {DBDingus.RegistratieTableNames.RegistratieTableName} where {DBDingus.RegistratieTableNames.IDOfUserRelated} = @userid and {DBDingus.RegistratieTableNames.Date} = cast(getdate() as date)";
            List<DBDingus.RegistratieTableTableEntry> _existingRegEntry = FuncsVSQL.GetListRTFromReader(command); //_DBDingus.GetListRTFromReader(FuncsVSQL.SQLQuery(command));
            DBDingus.RegistratieTableTableEntry existingRegEntry;
            command = new SqlCommand();
            if (_existingRegEntry.Count > 0)
            {
                // edit
                existingRegEntry = _existingRegEntry[0];
                existingRegEntry.IsLaat = false;
                existingRegEntry.Verwachtetijdvanaanwezighijd = new TimeSpan();

                command.Parameters.AddWithValue("@id", existingRegEntry.ID);
                if (existingRegEntry.HeeftIngetekend)
                {
                    if (existingRegEntry.IsAanwezig)
                    {
                        //update teken uit
                        toReturn.uitgetekened = true;
                        command.CommandText = $"update {DBDingus.RegistratieTableNames.RegistratieTableName} set {DBDingus.RegistratieTableNames.TimeUitteken} = cast(getdate() as time), {DBDingus.RegistratieTableNames.IsAanwezig} = 0 ,{DBDingus.RegistratieTableNames.IsLaat} = 0 where {DBDingus.RegistratieTableNames.Date} = cast(getdate() as date) and {DBDingus.RegistratieTableNames.IDOfUserRelated} = {toReturn.TheUserWithEntryInfo.UsE.ID}";
                    }
                    else
                    {
                        //update anuleer uitteken
                        toReturn.uitekenengeanuleerd = true;
                        command.CommandText = $"update {DBDingus.RegistratieTableNames.RegistratieTableName} set {DBDingus.RegistratieTableNames.IsAanwezig} = 1, {DBDingus.RegistratieTableNames.IsLaat} = 0  where {DBDingus.RegistratieTableNames.Date} = cast(getdate() as date) and {DBDingus.RegistratieTableNames.IDOfUserRelated} = {toReturn.TheUserWithEntryInfo.UsE.ID}";
                    }
                }
                else
                {
                    //update inteken
                    toReturn.ingetekened = true;
                    command.CommandText = $"update {DBDingus.RegistratieTableNames.RegistratieTableName} set {DBDingus.RegistratieTableNames.HeeftIngetekend} = 1, {DBDingus.RegistratieTableNames.TimeInteken} = cast(getdate() as time), {DBDingus.RegistratieTableNames.IsAanwezig} = 1, {DBDingus.RegistratieTableNames.IsLaat} = 0  where {DBDingus.RegistratieTableNames.Date} = cast(getdate() as date) and {DBDingus.RegistratieTableNames.IDOfUserRelated} = {toReturn.TheUserWithEntryInfo.UsE.ID}";
                }
            }
            else
            {
                //new
                //inteken
                command.Parameters.AddWithValue("@relatedUserId", foundUsers[0].ID);
                toReturn.ingetekened = true;
                command.CommandText = $"insert into {DBDingus.RegistratieTableNames.RegistratieTableName} ({DBDingus.RegistratieTableNames.IDOfUserRelated},{DBDingus.RegistratieTableNames.Date},{DBDingus.RegistratieTableNames.TimeInteken},{DBDingus.RegistratieTableNames.HeeftIngetekend},{DBDingus.RegistratieTableNames.IsAanwezig},{DBDingus.RegistratieTableNames.IsZiek},{DBDingus.RegistratieTableNames.IsFlexibelverlof},{DBDingus.RegistratieTableNames.IsStudieverlof},{DBDingus.RegistratieTableNames.IsExcursie},{DBDingus.RegistratieTableNames.IsLaat},{DBDingus.RegistratieTableNames.IsToegestaanAfwezig},{DBDingus.RegistratieTableNames.Opmerking},{DBDingus.RegistratieTableNames.Verwachtetijdvanaanwezighijd}) values (@relatedUserId, cast(getdate() as date), cast(getdate() as time), 1,1,0,0,0,0,0,0,'','')";
            }
            FuncsVSQL.SQLNonQuery(command);
            command = new SqlCommand();
            command.Parameters.AddWithValue("@userid", toReturn.TheUserWithEntryInfo.UsE.ID);
            command.CommandText = $"select * from {DBDingus.RegistratieTableNames.RegistratieTableName} where {DBDingus.RegistratieTableNames.IDOfUserRelated} = @userid and {DBDingus.RegistratieTableNames.Date} = cast(getdate() as date)";
            List<DBDingus.RegistratieTableTableEntry> endResult = FuncsVSQL.GetListRTFromReader(command); //_DBDingus.GetListRTFromReader(FuncsVSQL.SQLQuery(command));
            toReturn.TheUserWithEntryInfo.hasTodayRegEntry = true;
            toReturn.TheUserWithEntryInfo.RegE = endResult[0];
            return toReturn;
        }

        public static NetCom.ServerResponseOverzightFromOneDate overzight(DBDingus.AcountTableEntry _userAcount, NetCom.ServerRequestOverzightFromOneDate _request)
        {
            NetCom.ServerResponseOverzightFromOneDate toReturn = new NetCom.ServerResponseOverzightFromOneDate();
            SqlCommand command = new SqlCommand();
            command.CommandText = $"select * from {DBDingus.UserTableNames.UserTableName}";
            if (!_request.alsoReturnExUsers)
            {
                command.CommandText += $" where {DBDingus.UserTableNames.IsActiveUser} = 1";
            }
            List<DBDingus.UserTableTableEntry> userEntrys = FuncsVSQL.GetListUTFromReader(command); //_DBDingus.GetListUTFromReader(FuncsVSQL.SQLQuery(command));
            List<DBDingus.RegistratieTableTableEntry> regEntrys = new List<DBDingus.RegistratieTableTableEntry>();

            command = new SqlCommand();
            command.CommandText = $"select * from {DBDingus.RegistratieTableNames.RegistratieTableName} where {DBDingus.RegistratieTableNames.Date}";
            if (_request.useToday)
            {
                command.CommandText += " = cast(getdate() as date)";
            }
            else
            {
                command.CommandText += $" = cast('{_request.dateToGetOverzightFrom.Date.ToString("yyyy-MM-dd")}' as date)";
            }

            regEntrys = FuncsVSQL.GetListRTFromReader(command); //_DBDingus.GetListRTFromReader(FuncsVSQL.SQLQuery(command));
            foreach (var User in userEntrys)
            {
                if (User.IsActiveUser)
                {
                    DBDingus.CombUserAfwEntry toPutInList = new DBDingus.CombUserAfwEntry();
                    toPutInList.UsE = User;
                    foreach (var Entry in regEntrys)
                    {
                        if (Entry.IDOfUserRelated == User.ID)
                        {
                            toPutInList.hasTodayRegEntry = true;
                            toPutInList.RegE = Entry;
                            break;
                        }
                    }
                    toReturn.EtList.Add(toPutInList);
                }
            }
            toReturn.SQlDateTime = GetDateTimeFromSqlDatabase();
            return toReturn;
        }

        public static NetCom.ServerResponseOverzightFromMultipleDates alleDagOverzightenVanTussenTweDatums(DBDingus.AcountTableEntry _userAcount, NetCom.ServerRequestOverzightFromMultipleDates _request)
        {
            NetCom.ServerResponseOverzightFromMultipleDates toReturn = new NetCom.ServerResponseOverzightFromMultipleDates();

            foreach (DateTime dag in ElkeDatumTussenTweDatums(_request.FromAndWithThisDate, _request.TotEnMetDezeDatum))
            {

                NetCom.ServerResponseOverzightFromMultipleDatesSubType toAddToList = new NetCom.ServerResponseOverzightFromMultipleDatesSubType();
                toAddToList.DateOfOverzight = dag;

                NetCom.ServerRequestOverzightFromOneDate moreRequest = new NetCom.ServerRequestOverzightFromOneDate();
                moreRequest.alsoReturnExUsers = _request.getForExUsers;
                moreRequest.useToday = false;
                moreRequest.dateToGetOverzightFrom = dag;

                toAddToList.OverZichtFromThisDate = overzight(_userAcount, moreRequest);

                toReturn.allesDatJeNodigHebt.Add(toAddToList);
            }
            return toReturn;
        }

        public static NetCom.ServerResponseChangeRegistratieTable ChangeRegistatieTable(DBDingus.AcountTableEntry _MasterRightsEntry, NetCom.ServerRequestChangeRegistratieTable _Request)
        {
            NetCom.ServerResponseChangeRegistratieTable _toReturn = new NetCom.ServerResponseChangeRegistratieTable();
            SqlCommand _commamd = new SqlCommand();
            _commamd.Parameters.AddWithValue("@andered", _Request.deEntry.Opmerking);
            _commamd.Parameters.AddWithValue("@verwachtetijdvana", _Request.deEntry.Verwachtetijdvanaanwezighijd);

            if (_Request.isNieuwEntry)
            {
                if (_Request.newEntryDateIsToday)
                {
                    _commamd.CommandText = $"insert into {DBDingus.RegistratieTableNames.RegistratieTableName}( {DBDingus.RegistratieTableNames.IDOfUserRelated}, {DBDingus.RegistratieTableNames.Date}, {DBDingus.RegistratieTableNames.TimeInteken}, {DBDingus.RegistratieTableNames.TimeUitteken}, {DBDingus.RegistratieTableNames.HeeftIngetekend}, {DBDingus.RegistratieTableNames.IsAanwezig}, {DBDingus.RegistratieTableNames.IsZiek}, {DBDingus.RegistratieTableNames.IsFlexibelverlof}, {DBDingus.RegistratieTableNames.IsStudieverlof}, {DBDingus.RegistratieTableNames.IsExcursie}, {DBDingus.RegistratieTableNames.IsLaat}, {DBDingus.RegistratieTableNames.IsToegestaanAfwezig}, {DBDingus.RegistratieTableNames.Opmerking}, {DBDingus.RegistratieTableNames.Verwachtetijdvanaanwezighijd}) values ( {_Request.deEntry.IDOfUserRelated}, cast('{_Request.deEntry.Date.ToString("yyyy\\/MM\\/dd")}' as date), cast('{_Request.deEntry.TimeInteken}' as time), cast('{_Request.deEntry.TimeUitteken}' as time), cast('{_Request.deEntry.HeeftIngetekend}' as bit), cast('{_Request.deEntry.IsAanwezig}' as bit),  cast('{_Request.deEntry.IsZiek}' as bit), cast('{_Request.deEntry.IsFlexiebelverlof}' as bit), cast('{_Request.deEntry.IsStudieverlof}' as bit),  cast('{_Request.deEntry.IsExcurtie}' as bit),  cast('{_Request.deEntry.IsLaat}' as bit), cast('{_Request.deEntry.IsToegestaalAfwezig}' as bit, @andered, {_Request.deEntry.Verwachtetijdvanaanwezighijd})";

                    //_commamd.CommandText = $"insert into {DBDingus.RegistratieTableNames.RegistratieTableName} ({DBDingus.RegistratieTableNames.IDOfUserRelated}, {DBDingus.RegistratieTableNames.Date}, {DBDingus.RegistratieTableNames.TimeInteken}, {DBDingus.RegistratieTableNames.TimeUitteken}, {DBDingus.RegistratieTableNames.HeeftIngetekend}, {DBDingus.RegistratieTableNames.IsAanwezig}, {DBDingus.RegistratieTableNames.IsZiek}, {DBDingus.RegistratieTableNames.IsFlexibelverlof}, {DBDingus.RegistratieTableNames.IsStudieverlof}, {DBDingus.RegistratieTableNames.IsExcursie},{DBDingus.RegistratieTableNames.IsLaat}, {DBDingus.RegistratieTableNames.IsToegestaanAfwezig}, {DBDingus.RegistratieTableNames.Opmerking}, {DBDingus.RegistratieTableNames.Verwachtetijdvanaanwezighijd}) values ({_Request.deEntry.IDOfUserRelated}, cast(getdate() as date), cast('{_Request.deEntry.TimeInteken}' as time), cast('{_Request.deEntry.TimeUitteken}' as time), cast('{_Request.deEntry.HeeftIngetekend}' as bit), cast('{_Request.deEntry.IsAanwezig}' as bit), cast('{_Request.deEntry.IsZiek}' as bit),cast('{_Request.deEntry.IsFlexiebelverlof}' as bit), cast('{_Request.deEntry.IsStudieverlof}' as bit), cast('{_Request.deEntry.IsExcurtie}' as bit), cast('{_Request.deEntry.IsLaat}' as bit), cast('{_Request.deEntry.IsToegestaalAfwezig}' as bit), @andered, cast(@verwachtetijdvana as time))";
                }
                else
                {
                    //_commamd.CommandText = $"insert into {DBDingus.RegistratieTableNames.RegistratieTableName} ({DBDingus.RegistratieTableNames.IDOfUserRelated}, {DBDingus.RegistratieTableNames.Date}, {DBDingus.RegistratieTableNames.TimeInteken}, {DBDingus.RegistratieTableNames.TimeUitteken}, {DBDingus.RegistratieTableNames.HeeftIngetekend}, {DBDingus.RegistratieTableNames.IsAanwezig}, {DBDingus.RegistratieTableNames.IsZiek}, {DBDingus.RegistratieTableNames.IsFlexibelverlof}, {DBDingus.RegistratieTableNames.IsStudieverlof}, {DBDingus.RegistratieTableNames.IsExcursie},{DBDingus.RegistratieTableNames.IsLaat},{DBDingus.RegistratieTableNames.IsToegestaanAfwezig}, {DBDingus.RegistratieTableNames.Opmerking}, {DBDingus.RegistratieTableNames.Verwachtetijdvanaanwezighijd}) values ({_Request.deEntry.IDOfUserRelated}, cast('{_Request.deEntry.Date.ToString("yyyy\\/MM\\/dd")}' as date), cast('{_Request.deEntry.TimeInteken}' as time), cast('{_Request.deEntry.TimeUitteken}' as time), cast('{_Request.deEntry.HeeftIngetekend}' as bit), cast('{_Request.deEntry.IsAanwezig}' as bit), cast('{_Request.deEntry.IsZiek}' as bit),cast('{_Request.deEntry.IsFlexiebelverlof}' as bit), cast('{_Request.deEntry.IsStudieverlof}' as bit), cast('{_Request.deEntry.IsExcurtie}' as bit), cast('{_Request.deEntry.IsLaat}' as bit),cast('{_Request.deEntry.IsToegestaalAfwezig}' as bit), @andered, cast(@verwachtetijdvana as time))";

                    _commamd.CommandText = $"insert into {DBDingus.RegistratieTableNames.RegistratieTableName}( {DBDingus.RegistratieTableNames.IDOfUserRelated}, {DBDingus.RegistratieTableNames.Date}, {DBDingus.RegistratieTableNames.TimeInteken}, {DBDingus.RegistratieTableNames.TimeUitteken}, {DBDingus.RegistratieTableNames.HeeftIngetekend}, {DBDingus.RegistratieTableNames.IsAanwezig}, {DBDingus.RegistratieTableNames.IsZiek}, {DBDingus.RegistratieTableNames.IsFlexibelverlof}, {DBDingus.RegistratieTableNames.IsStudieverlof}, {DBDingus.RegistratieTableNames.IsExcursie}, {DBDingus.RegistratieTableNames.IsLaat}, {DBDingus.RegistratieTableNames.IsToegestaanAfwezig}, {DBDingus.RegistratieTableNames.Opmerking}, {DBDingus.RegistratieTableNames.Verwachtetijdvanaanwezighijd}) values ( {_Request.deEntry.IDOfUserRelated}, cast(getdate() as date), cast('{_Request.deEntry.TimeInteken}' as time), cast('{_Request.deEntry.TimeUitteken}' as time), cast('{_Request.deEntry.HeeftIngetekend}' as bit), cast('{_Request.deEntry.IsAanwezig}' as bit),  cast('{_Request.deEntry.IsZiek}' as bit), cast('{_Request.deEntry.IsFlexiebelverlof}' as bit), cast('{_Request.deEntry.IsStudieverlof}' as bit),  cast('{_Request.deEntry.IsExcurtie}' as bit),  cast('{_Request.deEntry.IsLaat}' as bit), cast('{_Request.deEntry.IsToegestaalAfwezig}' as bit), @andered,cast(@verwachtetijdvana as time) )";

                }

                if (FuncsVSQL.SQLNonQuery(_commamd) > 0)
                {
                    //_toReturn.deEntry=_Request.deEntry;
                    // _toReturn.deEntry.ID=(int)SqlDingusEnUserRechten.SQLQuery("select SCOPE_IDENTITY() as [yui]").Rows[0]["yui"];
                }
                else
                {
                    throw new Exception("SQL CHANGED_0 ERROR AT: " + _commamd.CommandText);
                }
            }
            else
            {

                _commamd.CommandText = $@"update {DBDingus.RegistratieTableNames.RegistratieTableName} set {DBDingus.RegistratieTableNames.IDOfUserRelated} = {_Request.deEntry.IDOfUserRelated}, {DBDingus.RegistratieTableNames.Date} = cast('{_Request.deEntry.Date.ToString("yyyy\\/MM\\/dd")}' as date), {DBDingus.RegistratieTableNames.TimeInteken} = cast('{_Request.deEntry.TimeInteken}' as time), {DBDingus.RegistratieTableNames.TimeUitteken} = cast('{_Request.deEntry.TimeUitteken}' as time), {DBDingus.RegistratieTableNames.IsAanwezig} = cast('{_Request.deEntry.IsAanwezig}' as bit), {DBDingus.RegistratieTableNames.HeeftIngetekend} = cast('{_Request.deEntry.HeeftIngetekend}' as bit), {DBDingus.RegistratieTableNames.IsZiek} = cast('{_Request.deEntry.IsZiek}' as bit), {DBDingus.RegistratieTableNames.IsFlexibelverlof} = cast('{_Request.deEntry.IsFlexiebelverlof}' as bit), {DBDingus.RegistratieTableNames.IsStudieverlof} = cast('{_Request.deEntry.IsStudieverlof}' as bit), {DBDingus.RegistratieTableNames.IsExcursie} = cast('{_Request.deEntry.IsExcurtie}' as bit), {DBDingus.RegistratieTableNames.IsLaat} = cast('{_Request.deEntry.IsLaat}' as bit), {DBDingus.RegistratieTableNames.IsToegestaanAfwezig} = cast('{_Request.deEntry.IsToegestaalAfwezig}' as bit), {DBDingus.RegistratieTableNames.Opmerking} = @andered, {DBDingus.RegistratieTableNames.Verwachtetijdvanaanwezighijd} = cast(@verwachtetijdvana as time) where {DBDingus.RegistratieTableNames.ID} = {_Request.deEntry.ID}";

                if (FuncsVSQL.SQLNonQuery(_commamd) > 0)
                {
                    //_toReturn.deEntry=_Request.deEntry;
                }
                else
                {
                    throw new Exception("SQL CHANGED_0 ERROR AT: " + _commamd.CommandText);
                }
            }
            return _toReturn;
        }

        //user
        public static NetCom.ServerResponseGetUserTable GetUserTable(DBDingus.AcountTableEntry _MasterRightsEnty, NetCom.ServerRequestGetUserTable _request)
        {
            NetCom.ServerResponseGetUserTable toReturn = new NetCom.ServerResponseGetUserTable();
            SqlCommand command = new SqlCommand();
            if (_request.aleenDieNogOpSchoolZitten)
            {
                command.CommandText = $"select * from {DBDingus.UserTableNames.UserTableName} where ZitNogOpSchool = 1";
            }
            else
            {
                command.CommandText = $"select * from {DBDingus.UserTableNames.UserTableName}";
            }
            toReturn.deEntrys = FuncsVSQL.GetListUTFromReader(command);
            return toReturn;
        }

        public static NetCom.ServerResponseChangeUserTable ChangeUserTable(DBDingus.AcountTableEntry _MasterRightsEnty, NetCom.ServerRequestChangeUserTable _request)
        {
            NetCom.ServerResponseChangeUserTable toReturn = new NetCom.ServerResponseChangeUserTable();
            SqlCommand command = new SqlCommand();

            if (!_request.IsNewUser || _request.DeleteEntry)
            {
                command.Parameters.AddWithValue("@ID", _request.deEntry.ID);
            }

            if (_request.DeleteEntry)
            {
                command.CommandText = $"delete from {DBDingus.UserTableNames.UserTableName} where {DBDingus.UserTableNames.ID} = @ID";
            }
            else
            {

                bool baylife = false;

                command.Parameters.AddWithValue("@Voornaam", _request.deEntry.VoorNaam);
                command.Parameters.AddWithValue("@Achternaam", _request.deEntry.AchterNaam);
                command.Parameters.AddWithValue("@NFCID", _request.deEntry.NFCID);
                command.Parameters.AddWithValue("@DateJoined", _request.deEntry.DateJoined);
                command.Parameters.AddWithValue("@IsActive", _request.deEntry.IsActiveUser);

                try
                {

                    command.Parameters.AddWithValue("@DateLeft", _request.deEntry.DateLeft);

                }
                catch { baylife = true; }

                if (_request.IsNewUser)
                {
                    if (baylife)
                    {
                        command.CommandText = $"insert into {DBDingus.UserTableNames.UserTableName} ({DBDingus.UserTableNames.VoorNaam}, {DBDingus.UserTableNames.AchterNaam}, {DBDingus.UserTableNames.NFCID}, {DBDingus.UserTableNames.DateJoined}, {DBDingus.UserTableNames.IsActiveUser}, {DBDingus.UserTableNames.DateLeft} ) values (@Voornaam, @Achternaam, @NFCID, cast(@DateJoined as date), cast(@IsActive as bit), cast(@DateLeft as date))";
                    }
                    else
                    {
                        command.CommandText = $"insert into {DBDingus.UserTableNames.UserTableName} ({DBDingus.UserTableNames.VoorNaam}, {DBDingus.UserTableNames.AchterNaam}, {DBDingus.UserTableNames.NFCID}, {DBDingus.UserTableNames.DateJoined}, {DBDingus.UserTableNames.IsActiveUser}) values (@Voornaam, @Achternaam, @NFCID, cast(@DateJoined as date), cast(@IsActive as bit))";
                    }
                }
                else
                {
                    if (baylife)
                    {
                        command.CommandText = $"update {DBDingus.UserTableNames.UserTableName} set {DBDingus.UserTableNames.VoorNaam} = @Voornaam, {DBDingus.UserTableNames.AchterNaam} = @Achternaam, {DBDingus.UserTableNames.NFCID} = @NFCID, {DBDingus.UserTableNames.DateJoined} = cast(@DateJoined as date), {DBDingus.UserTableNames.IsActiveUser} = cast(@IsActive as bit), {DBDingus.UserTableNames.DateLeft} = cast(@DateLeft as date) where {DBDingus.UserTableNames.ID} = @ID";
                    }
                    else
                    {
                        command.CommandText = $"update {DBDingus.UserTableNames.UserTableName} set {DBDingus.UserTableNames.VoorNaam} = @Voornaam, {DBDingus.UserTableNames.AchterNaam} = @Achternaam, {DBDingus.UserTableNames.NFCID} = @NFCID, {DBDingus.UserTableNames.DateJoined} = cast(@DateJoined as date), {DBDingus.UserTableNames.IsActiveUser} = cast(@IsActive as bit) where {DBDingus.UserTableNames.ID} = @ID";
                    }
                }
            }
            if (FuncsVSQL.SQLNonQuery(command) != 1)
            {
                toReturn.OK = false;
            }
            return toReturn;
        }

        //mod
        public static NetCom.ServerResponseGetModTable GetModtable(DBDingus.AcountTableEntry _MasterRightsEnty, NetCom.ServerRequestGetModTable _request)
        {
            NetCom.ServerResponseGetModTable toReturn = new NetCom.ServerResponseGetModTable();
            toReturn.deEntrys = FuncsVSQL.GetListMTFromReader($"select * from {DBDingus.ModifierTableNames.ModifierTableName}");
            return toReturn;
        }

        public static NetCom.ServerResponseChangeModTable ChangeModtable(DBDingus.AcountTableEntry _MasterRightsEnty, NetCom.ServerRequestChangeModTable _request)
        {
            NetCom.ServerResponseChangeModTable toReturn = new NetCom.ServerResponseChangeModTable();
            SqlCommand command = new SqlCommand();

            if (!_request.IsNewEntry || _request.DeleteEntry)
            {
                command.Parameters.AddWithValue("@ID", _request.deEntry.ID);
            }

            if (_request.DeleteEntry)
            {
                command.CommandText = $"delete from {DBDingus.ModifierTableNames.ModifierTableName} where {DBDingus.ModifierTableNames.ID} = @ID";
            }
            else
            {
                command.Parameters.AddWithValue("@dateVan", _request.deEntry.DateVanafEnMet);
                command.Parameters.AddWithValue("@dateTot", _request.deEntry.DateTotEnMet);
                command.Parameters.AddWithValue("@daysOfEffect", JsonConvert.SerializeObject(_request.deEntry.DaysOfEffect));
                command.Parameters.AddWithValue("@users", JsonConvert.SerializeObject(_request.deEntry.UserIDs));
                command.Parameters.AddWithValue("@hoursToAdd", _request.deEntry.HoursToAdd);
                command.Parameters.AddWithValue("@omschrij", _request.deEntry.omschrijveing);
                command.Parameters.AddWithValue("@isStudiev", _request.deEntry.isStudieVerlof);
                command.Parameters.AddWithValue("@isExcur", _request.deEntry.isExurtie);
                command.Parameters.AddWithValue("@isFlexy", _request.deEntry.isFlexibelverlofoeorfsjklcghiur);
                if (_request.IsNewEntry)
                {
                    command.CommandText = $"insert into {DBDingus.ModifierTableNames.ModifierTableName} ({DBDingus.ModifierTableNames.DateVanafEnMet}, {DBDingus.ModifierTableNames.DateTotEnMet}, {DBDingus.ModifierTableNames.DaysOfEffect}, {DBDingus.ModifierTableNames.UserIDs}, {DBDingus.ModifierTableNames.HoursToAdd}, {DBDingus.ModifierTableNames.Omschrijving}, {DBDingus.ModifierTableNames.isStudiever}, {DBDingus.ModifierTableNames.isExur}, {DBDingus.ModifierTableNames.isflexy}) values (cast(@dateVan as date), cast(@dateTot as date), @daysOfEffect, @users, @hoursToAdd, @omschrij, @isStudiev, @isExcur, @isFlexy)";
                }
                else
                {
                    command.CommandText = $"update {DBDingus.ModifierTableNames.ModifierTableName} set {DBDingus.ModifierTableNames.DateVanafEnMet} = cast(@dateVan as date), {DBDingus.ModifierTableNames.DateTotEnMet} = @dateTot, {DBDingus.ModifierTableNames.DaysOfEffect} = @daysOfEffect, {DBDingus.ModifierTableNames.UserIDs} = @users, {DBDingus.ModifierTableNames.HoursToAdd} = @hoursToAdd,  {DBDingus.ModifierTableNames.Omschrijving} = @omschrij, {DBDingus.ModifierTableNames.isStudiever} = @isStudiev, {DBDingus.ModifierTableNames.isExur} = @isStudiev, {DBDingus.ModifierTableNames.isflexy} = @isFlexy where {DBDingus.ModifierTableNames.ID} = @ID";
                }
            }
            if (FuncsVSQL.SQLNonQuery(command) != 1)
            {
                toReturn.OK = false;
            }
            return toReturn;
        }

        //acount
        public static NetCom.ServerResponseGetAcountTable GetAcountTable(DBDingus.AcountTableEntry _MasterRightsEnty, NetCom.ServerRequestGetAcountsTable _request)
        {
            NetCom.ServerResponseGetAcountTable toReturn = new NetCom.ServerResponseGetAcountTable();
            toReturn.deEntrys = FuncsVSQL.GetListATFromReader($"select * from {DBDingus.AcountsTableNames.AcountsTableName}");
            return toReturn;
        }

        public static NetCom.ServerResponseChangeAcountTable ChangeAcountTable(DBDingus.AcountTableEntry _MasterRightsEnty, NetCom.ServerRequestChangeAcountTable _request)
        {
            NetCom.ServerResponseChangeAcountTable toReturn = new NetCom.ServerResponseChangeAcountTable();
            SqlCommand command = new SqlCommand();

            if (!_request.IsNewEntry || _request.DeleteEntry)
            {
                command.Parameters.AddWithValue("@ID", _request.deEntry.ID);

            }

            if (_request.DeleteEntry)
            {
                command.CommandText = $"delete from {DBDingus.AcountsTableNames.AcountsTableName} where {DBDingus.AcountsTableNames.ID} = @ID";

            }
            else
            {
                command.Parameters.AddWithValue("@aansprBevoeg", _request.deEntry.aanspreekpuntBevoegdhijd);
                command.Parameters.AddWithValue("@adminBevoeg", _request.deEntry.adminBevoegdhijd);
                command.Parameters.AddWithValue("@inlogNaam", _request.deEntry.inlogNaam);
                command.Parameters.AddWithValue("@pw", _request.deEntry.inlogWachtwoord);
                command.Parameters.AddWithValue("@naam", _request.deEntry.Naam);

                if (_request.IsNewEntry)
                {
                    command.CommandText = $"insert into {DBDingus.AcountsTableNames.AcountsTableName} ({DBDingus.AcountsTableNames.Naam}, {DBDingus.AcountsTableNames.inlogNaam}, {DBDingus.AcountsTableNames.inlogWachtwoord}, {DBDingus.AcountsTableNames.aanspreekpuntBevoegthijdLvl}, {DBDingus.AcountsTableNames.adminBevoegdhijd}) values (@naam, @inlogNaam, @pw, @aansprBevoeg, @adminBevoeg)";

                }
                else
                {
                    command.CommandText = $"update {DBDingus.AcountsTableNames.AcountsTableName} set {DBDingus.AcountsTableNames.Naam}= @naam, {DBDingus.AcountsTableNames.inlogNaam} = @inlogNaam, {DBDingus.AcountsTableNames.inlogWachtwoord} = @pw, {DBDingus.AcountsTableNames.aanspreekpuntBevoegthijdLvl} = @aansprBevoeg, {DBDingus.AcountsTableNames.adminBevoegdhijd} = @adminBevoeg where {DBDingus.AcountsTableNames.ID} = @ID";

                }
            }

            if (FuncsVSQL.SQLNonQuery(command) != 1)
            {
                toReturn.OK = false;
            }

            return toReturn;
        }
    }
}