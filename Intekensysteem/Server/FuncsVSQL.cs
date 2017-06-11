using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using ErFunc;

namespace Server
{
    public static class FuncsVSQL
    {
        public static string _ConnectionString =
            "Data Source=DESKTOP-O2RJSDK;" +
            "Initial Catalog=IntekenSysteem;" +
            "User id=sa;" +
            "Password=kanker;";
        //public static string _ConnectionString = "Server=DESKTOP-V3MSLRN\SQLEXPRESS; Database=IntekenSysteem; User Id=sa; password=kanker;";
        //public static string _ConnectionString = "Server=SHISHIDOU-PC\\SQLEXPRESS; Database=IntekenSysteem; User Id=sa; password=kanker;";

        public static int SQLNonQuery(string _command)
        {
            SqlCommand command = new SqlCommand()
            {
                CommandText = _command
            };
            return SQLNonQuery(command);
        }

        public static int SQLNonQuery(SqlCommand _command)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
            {
                try
                {
                    _command.Connection = sqlConnection;
                    sqlConnection.Open();
                    return _command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"ERROR @ SQL Command: {_command.CommandText} | Message: {ex.Message}");
                }
            }
        }

        /* illegal
        public static SqlDataReader SQLQuery(string _command)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = _command;
            return SQLQuery(command);
        }

        public static SqlDataReader SQLQuery(SqlCommand _command)
        {
            SqlConnection sqlConnection = new SqlConnection(_ConnectionString);
            try
            {
                _command.Connection = sqlConnection;
                sqlConnection.Open();
                return _command.ExecuteReader();
                //close? dispose?
            }
            catch (Exception ex)
            {
                sqlConnection.Dispose();
                throw new Exception($"ERROR @ SQL Command: {_command.CommandText} | Message: {ex.Message}");
            }
        }

    */

        //datatable afwezig in .netcore...... switched to .net but this works just fine

        private static T ReadFromReader<T>(IDataRecord _record, string _readProp)
        {
            if (_record.IsDBNull(_record.GetOrdinal(_readProp))) { return default(T); }
            return (T)_record.GetValue(_record.GetOrdinal(_readProp));
        }

        private static DateTime ReadDateTimeFromReader(IDataRecord _record, string _readProp)
        {
            if (_record.IsDBNull(_record.GetOrdinal(_readProp))) { return new DateTime(); }
            return DateTime.Parse(_record.GetValue(_record.GetOrdinal(_readProp)).ToString());
        }


        public static DateTime GetDateTimeFromSQLServer()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "select getdate() jikan";
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        return ((IDataRecord)reader).GetDateTime(reader.GetOrdinal("jikan"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR @ SQL Command: select getdate() jikan | Message: {ex.Message}");
            }
            throw new Exception("dit kan niet~~ FuncdVSQSL.GetDateTimeFromSQlServer()");
        }


        public static List<DBDingus.AcountTableEntry> GetListATFromReader(string _command)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = _command;
            return GetListATFromReader(command);
        }

        public static List<DBDingus.AcountTableEntry> GetListATFromReader(SqlCommand _command)
        {
            List<DBDingus.AcountTableEntry> toReturn = new List<DBDingus.AcountTableEntry>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    _command.Connection = connection;
                    connection.Open();
                    SqlDataReader _reader = _command.ExecuteReader();

                    List<string> fields = new List<string>();
                    for (int i = 0; i < _reader.FieldCount; i++)
                    {
                        fields.Add(_reader.GetName(i));
                    }
                    while (_reader.Read())
                    {
                        DBDingus.AcountTableEntry entry = new DBDingus.AcountTableEntry();
                        if (fields.Contains(DBDingus.AcountsTableNames.ID)) { entry.ID = ReadFromReader<Int32>((IDataRecord)_reader, DBDingus.AcountsTableNames.ID); }
                        if (fields.Contains(DBDingus.AcountsTableNames.Naam)) { entry.Naam = ReadFromReader<string>((IDataRecord)_reader, DBDingus.AcountsTableNames.Naam); }
                        if (fields.Contains(DBDingus.AcountsTableNames.inlogNaam)) { entry.inlogNaam = ReadFromReader<string>((IDataRecord)_reader, DBDingus.AcountsTableNames.inlogNaam); }
                        if (fields.Contains(DBDingus.AcountsTableNames.inlogWachtwoord)) { entry.inlogWachtwoord = ReadFromReader<string>((IDataRecord)_reader, DBDingus.AcountsTableNames.inlogWachtwoord); }
                        if (fields.Contains(DBDingus.AcountsTableNames.aanspreekpuntBevoegthijdLvl)) { entry.aanspreekpuntBevoegdhijd = ReadFromReader<Int32>((IDataRecord)_reader, DBDingus.AcountsTableNames.aanspreekpuntBevoegthijdLvl); }
                        if (fields.Contains(DBDingus.AcountsTableNames.adminBevoegdhijd)) { entry.adminBevoegdhijd = ReadFromReader<Int32>((IDataRecord)_reader, DBDingus.AcountsTableNames.adminBevoegdhijd); }
                        toReturn.Add(entry);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"(ATFromReader)ERROR @ SQL Command: {_command.CommandText} | Message: {ex.Message}");
            }
            return toReturn;
        }


        public static List<DBDingus.UserTableTableEntry> GetListUTFromReader(string _command)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = _command;
            return GetListUTFromReader(command);
        }

        public static List<DBDingus.UserTableTableEntry> GetListUTFromReader(SqlCommand _command)
        {
            List<DBDingus.UserTableTableEntry> toReturn = new List<DBDingus.UserTableTableEntry>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    _command.Connection = connection;
                    connection.Open();
                    SqlDataReader _reader = _command.ExecuteReader();

                    List<string> fields = new List<string>();
                    for (int i = 0; i < _reader.FieldCount; i++)
                    {
                        fields.Add(_reader.GetName(i));
                    }
                    while (_reader.Read())
                    {
                        DBDingus.UserTableTableEntry entry = new DBDingus.UserTableTableEntry();
                        if (fields.Contains(DBDingus.UserTableNames.ID)) { entry.ID = ReadFromReader<int>((IDataRecord)_reader, DBDingus.UserTableNames.ID); }
                        if (fields.Contains(DBDingus.UserTableNames.VoorNaam)) { entry.VoorNaam = ReadFromReader<string>((IDataRecord)_reader, DBDingus.UserTableNames.VoorNaam); }
                        if (fields.Contains(DBDingus.UserTableNames.AchterNaam)) { entry.AchterNaam = ReadFromReader<string>((IDataRecord)_reader, DBDingus.UserTableNames.AchterNaam); }
                        if (fields.Contains(DBDingus.UserTableNames.NFCID)) { entry.NFCID = ReadFromReader<string>((IDataRecord)_reader, DBDingus.UserTableNames.NFCID); }
                        if (fields.Contains(DBDingus.UserTableNames.DateJoined)) { entry.DateJoined = ReadDateTimeFromReader((IDataRecord)_reader, DBDingus.UserTableNames.DateJoined); }
                        if (fields.Contains(DBDingus.UserTableNames.DateLeft)) { entry.DateLeft = ReadDateTimeFromReader((IDataRecord)_reader, DBDingus.UserTableNames.DateLeft); }
                        if (fields.Contains(DBDingus.UserTableNames.IsActiveUser)) { entry.IsActiveUser = ReadFromReader<bool>((IDataRecord)_reader, DBDingus.UserTableNames.IsActiveUser); }
                        toReturn.Add(entry);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"(UTFromReader)ERROR @ SQL Command: {_command.CommandText} | Message: {ex.Message}");
            }

            return toReturn;
        }


        public static List<DBDingus.RegistratieTableTableEntry> GetListRTFromReader(string _command)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = _command;
            return GetListRTFromReader(command);
        }

        public static List<DBDingus.RegistratieTableTableEntry> GetListRTFromReader(SqlCommand _command)
        {
            List<DBDingus.RegistratieTableTableEntry> toReturn = new List<DBDingus.RegistratieTableTableEntry>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    _command.Connection = connection;
                    connection.Open();
                    SqlDataReader _reader = _command.ExecuteReader();

                    List<string> fields = new List<string>();
                    for (int i = 0; i < _reader.FieldCount; i++)
                    {
                        fields.Add(_reader.GetName(i));
                    }
                    while (_reader.Read())
                    {
                        DBDingus.RegistratieTableTableEntry entry = new DBDingus.RegistratieTableTableEntry();

                        if (fields.Contains(DBDingus.RegistratieTableNames.ID))
                        {
                            entry.ID = ReadFromReader<int>((IDataRecord)_reader, DBDingus.RegistratieTableNames.ID);
                        }
                        if (fields.Contains(DBDingus.RegistratieTableNames.IDOfUserRelated))
                        {
                            entry.IDOfUserRelated = ReadFromReader<int>((IDataRecord)_reader, DBDingus.RegistratieTableNames.IDOfUserRelated);
                        }
                        if (fields.Contains(DBDingus.RegistratieTableNames.Date))
                        {
                            entry.Date = ReadDateTimeFromReader((IDataRecord)_reader, DBDingus.RegistratieTableNames.Date);
                        }
                        if (fields.Contains(DBDingus.RegistratieTableNames.TimeInteken))
                        {
                            entry.TimeInteken = ReadDateTimeFromReader((IDataRecord)_reader, DBDingus.RegistratieTableNames.TimeInteken).TimeOfDay;
                        }
                        if (fields.Contains(DBDingus.RegistratieTableNames.TimeUitteken))
                        {
                            entry.TimeUitteken = ReadDateTimeFromReader((IDataRecord)_reader, DBDingus.RegistratieTableNames.TimeUitteken).TimeOfDay;
                        }
                        if (fields.Contains(DBDingus.RegistratieTableNames.HeeftIngetekend))
                        {
                            entry.HeeftIngetekend = ReadFromReader<bool>((IDataRecord)_reader, DBDingus.RegistratieTableNames.HeeftIngetekend);
                        }
                        if (fields.Contains(DBDingus.RegistratieTableNames.IsAanwezig))
                        {
                            entry.IsAanwezig = ReadFromReader<bool>((IDataRecord)_reader, DBDingus.RegistratieTableNames.IsAanwezig);
                        }
                        if (fields.Contains(DBDingus.RegistratieTableNames.IsZiek))
                        {
                            entry.IsZiek = ReadFromReader<bool>((IDataRecord)_reader, DBDingus.RegistratieTableNames.IsZiek);
                        }
                        if (fields.Contains(DBDingus.RegistratieTableNames.IsFlexibelverlof))
                        {
                            entry.IsFlexiebelverlof = ReadFromReader<bool>((IDataRecord)_reader, DBDingus.RegistratieTableNames.IsFlexibelverlof);
                        }
                        if (fields.Contains(DBDingus.RegistratieTableNames.IsStudieverlof))
                        {
                            entry.IsStudieverlof = ReadFromReader<bool>((IDataRecord)_reader, DBDingus.RegistratieTableNames.IsStudieverlof);
                        }
                        if (fields.Contains(DBDingus.RegistratieTableNames.IsExcursie))
                        {
                            entry.IsExcurtie = ReadFromReader<bool>((IDataRecord)_reader, DBDingus.RegistratieTableNames.IsExcursie);
                        }
                        if (fields.Contains(DBDingus.RegistratieTableNames.IsLaat))
                        {
                            entry.IsLaat = ReadFromReader<bool>((IDataRecord)_reader, DBDingus.RegistratieTableNames.IsLaat);
                        }
                        if (fields.Contains(DBDingus.RegistratieTableNames.IsToegestaanAfwezig))
                        {
                            entry.IsToegestaalAfwezig = ReadFromReader<bool>((IDataRecord)_reader, DBDingus.RegistratieTableNames.IsToegestaanAfwezig);
                        }
                        if (fields.Contains(DBDingus.RegistratieTableNames.Opmerking))
                        {
                            entry.Opmerking = ReadFromReader<string>((IDataRecord)_reader, DBDingus.RegistratieTableNames.Opmerking);
                        }
                        if (fields.Contains(DBDingus.RegistratieTableNames.Verwachtetijdvanaanwezighijd))
                        {
                            entry.Verwachtetijdvanaanwezighijd = ReadDateTimeFromReader((IDataRecord)_reader, DBDingus.RegistratieTableNames.Verwachtetijdvanaanwezighijd).TimeOfDay;
                        }
                        toReturn.Add(entry);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"(RTFromReader)ERROR @ SQL Command: {_command.CommandText} | Message: {ex.Message}");
            }

            return toReturn;
        }


        public static List<DBDingus.ModifierTableEntry> GetListMTFromReader(string _command)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = _command;
            return GetListMTFromReader(command);
        }

        public static List<DBDingus.ModifierTableEntry> GetListMTFromReader(SqlCommand _command)
        {
            List<DBDingus.ModifierTableEntry> toReturn = new List<DBDingus.ModifierTableEntry>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    _command.Connection = connection;
                    connection.Open();
                    SqlDataReader _reader = _command.ExecuteReader();

                    List<string> fields = new List<string>();
                    for (int i = 0; i < _reader.FieldCount; i++)
                    {
                        fields.Add(_reader.GetName(i));
                    }
                    while (_reader.Read())
                    {
                        DBDingus.ModifierTableEntry entry = new DBDingus.ModifierTableEntry();
                        if (fields.Contains(DBDingus.ModifierTableNames.ID)) { entry.ID = ReadFromReader<Int32>((IDataRecord)_reader, DBDingus.ModifierTableNames.ID); }
                        if (fields.Contains(DBDingus.ModifierTableNames.DateTotEnMet)) { entry.DateTotEnMet = ReadDateTimeFromReader((IDataRecord)_reader, DBDingus.ModifierTableNames.DateTotEnMet); }
                        if (fields.Contains(DBDingus.ModifierTableNames.DateVanafEnMet)) { entry.DateVanafEnMet = ReadDateTimeFromReader((IDataRecord)_reader, DBDingus.ModifierTableNames.DateVanafEnMet); }
                        if (fields.Contains(DBDingus.ModifierTableNames.UserIDs)) { entry.UserIDs = JsonConvert.DeserializeObject<List<int>>(ReadFromReader<string>((IDataRecord)_reader, DBDingus.ModifierTableNames.UserIDs)); }
                        if (fields.Contains(DBDingus.ModifierTableNames.DaysOfEffect)) { entry.DaysOfEffect = JsonConvert.DeserializeObject<bool[]>(ReadFromReader<string>((IDataRecord)_reader, DBDingus.ModifierTableNames.DaysOfEffect)); }
                        if (fields.Contains(DBDingus.ModifierTableNames.HoursToAdd)) { entry.HoursToAdd = ReadDateTimeFromReader((IDataRecord)_reader, DBDingus.ModifierTableNames.HoursToAdd).TimeOfDay; }
                        if (fields.Contains(DBDingus.ModifierTableNames.Omschrijving)) { entry.omschrijveing = ReadFromReader<string>((IDataRecord)_reader, DBDingus.ModifierTableNames.Omschrijving); }
                        if (fields.Contains(DBDingus.ModifierTableNames.isStudiever)) { entry.isStudieVerlof = ReadFromReader<bool>((IDataRecord)_reader, DBDingus.ModifierTableNames.isStudiever); }
                        if (fields.Contains(DBDingus.ModifierTableNames.isExur)) { entry.isExurtie = ReadFromReader<bool>((IDataRecord)_reader, DBDingus.ModifierTableNames.isExur); }
                        if (fields.Contains(DBDingus.ModifierTableNames.isflexy)) { entry.isFlexibelverlofoeorfsjklcghiur = ReadFromReader<bool>((IDataRecord)_reader, DBDingus.ModifierTableNames.isflexy); }
                        toReturn.Add(entry);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"(MTFromReader)ERROR @ SQL Command: {_command.CommandText} | Message: {ex.Message}");
            }
            return toReturn;
        }


        public static List<DBDingus.IsSchoolDagTableEntry> GetLisISDFromReader(string _command)
        {
            return GetLisISDFromReader(new SqlCommand { CommandText = _command });
        }

        public static List<DBDingus.IsSchoolDagTableEntry> GetLisISDFromReader(SqlCommand _command)
        {
            List<DBDingus.IsSchoolDagTableEntry> toReturn = new List<DBDingus.IsSchoolDagTableEntry>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    _command.Connection = connection;
                    connection.Open();
                    SqlDataReader _reader = _command.ExecuteReader();

                    List<string> fields = new List<string>();
                    for (int i = 0; i < _reader.FieldCount; i++)
                    {
                        fields.Add(_reader.GetName(i));
                    }
                    while (_reader.Read())
                    {
                        DBDingus.IsSchoolDagTableEntry entry = new DBDingus.IsSchoolDagTableEntry();
                        if (fields.Contains(DBDingus.IsSchoolDagTableNames.ID)) { entry.ID = ReadFromReader<int>((IDataRecord)_reader, DBDingus.IsSchoolDagTableNames.ID); }
                        if (fields.Contains(DBDingus.IsSchoolDagTableNames.Date)) { entry.Date = ReadDateTimeFromReader((IDataRecord)_reader, DBDingus.IsSchoolDagTableNames.Date); }
                        toReturn.Add(entry);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"(ISDFromReader)ERROR @ SQL Command: {_command.CommandText} | Message: {ex.Message}");
            }
            return toReturn;
        }

    }
}