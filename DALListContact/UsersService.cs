using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALListContact
{
    public class UsersService
    {
        string insert = @" insert into users ('lastName','firstName','personnage','password') output inserted.id ('');
        public static int insertUser(Users users)
        {
            int idGenerated = -1;
            List<SqlParameter> paramsList = MySqlParameterConverter.ConvertFromUser(users); 
            if (users != null) {
            }
            return idGenerated;

        }
    }
}
