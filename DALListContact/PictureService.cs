using DALConnection;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALListContact
{
    class PictureService
    {

        private static string requetteGetById=@"select from pictures where id=@id";
        internal static Picture getById(int id) {
            Picture p = new Picture { ID = id };
            List<SqlParameter> list = MySqlParameterConverter.ConvertFrompicture(p);
            DataSet data = Connection.selectQuery(requetteGetById, list);
            DataTable table = data.Tables[0];
            DataRowCollection rows = table.Rows;
            p = EntitiesConverter.ConvertFromDataRowToPicture(rows[0]);
            return p;
        }

    }
}
