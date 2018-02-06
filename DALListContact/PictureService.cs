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

        private static string requetteGetById=@"select * from pictures where id=@id";
        private static string requetteInsert = @"insert into pictures (src) output inserted.id values(@src)";
        internal static Picture getById(int id) {
            Picture p = new Picture { ID = id };
            List<SqlParameter> list = MySqlParameterConverter.ConvertFrompicture(p);
            DataSet data = Connection.selectQuery(requetteGetById, list);
            DataTable table = data.Tables[0];
            DataRowCollection rows = table.Rows;
            p = EntitiesConverter.ConvertFromDataRowToPicture(rows[0]);
            return p;
        }
        internal static int Insert(Picture pic)
        {
            int idGenarted = -1;
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("src", pic.Src));
            idGenarted= Connection.Insert(requetteInsert, list);
            return idGenarted;
        }

    }
}
