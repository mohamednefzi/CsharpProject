using Entities;

using System.Collections.Generic;
using System.Data.SqlClient;

using DALConnection;
using System.Data;

namespace DALListContact
{
    class AddressServices
    {
        static private string requetteInsert = @" insert into adress (number,street,city,province,country) output inserted.id(@number,@street,@city,@province,@country)";
        private static string requetteDelete = @"delete from adress where id =@id";
        private static string requetteGetById = @"select from adress where id=@id";

        internal static long Insert(Address address)
        {
            long idGenerated = -1;
            if (address != null)
            {
                List<SqlParameter> paramsList = MySqlParameterConverter.ConvertFromAdress(address);
                idGenerated = Connection.Insert(requetteInsert, paramsList);
            }
            return idGenerated;
        }

        internal static int DeleteById(int IdAddress)
        {
            int nbLigne = 0;
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("id", IdAddress));
            nbLigne = Connection.Delete(requetteDelete, list);
            return nbLigne;
        }

        internal static Address GetById(int id)
        {
            Address adr = new Address { ID = id };
            List<SqlParameter> list = new List<SqlParameter>();
            DataSet data = Connection.selectQuery(requetteGetById, list);
            DataTable table = data.Tables[0];
            DataRowCollection rows = table.Rows;
            if (rows.Count > 0)
                adr = EntitiesConverter.ConvertFromDataRowToAddress(rows[0]);
            else
            {
                adr = null;
            }
            return adr;
        }
    }
}
