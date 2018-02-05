using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;
using DALConnection;

namespace DALListContact
{
    class EventServices
    {
        static string requetteGetEventByIdRelation = @"select * from events where idReltion=@idRelation";

        public static List<Events> getEventsByIdRelation(int idUser, int idFriend)
        {
            List<Events> eventsList = new List<Events>();
            int id = UsersService.GetIdRelation(idUser, idFriend);
            List<SqlParameter> list = MySqlParameterConverter.ConvertFromEvents(new Events { IdRelation = id });
            DataSet data = Connection.selectQuery(requetteGetEventByIdRelation, list);

            return eventsList;
        }

    }
}
