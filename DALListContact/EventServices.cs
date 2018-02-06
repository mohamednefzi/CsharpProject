using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;
using DALConnection;
using System.Data;

namespace DALListContact
{
   public class EventServices
    {
        static string requetteGetEventByIdRelation = @"select * from events where idReltion=@idRelation";
        static string requetteConfirmEvents = @"update events set isConfirmed=true where id=@id";
        static string requetteDeleteEvents = @"delete from events where id=@id";
        static string requetteInsertEvents = @"insert into events (description,idRelation,date) output inserted.id values(@description,@idRelation,@date)";

        public static List<Events> getEventsByIdRelation(int idUser, int idFriend)
        {
            List<Events> eventsList = new List<Events>();
            int id = UsersService.GetIdRelation(idUser, idFriend);
            List<SqlParameter> list = MySqlParameterConverter.ConvertFromEvents(new Events { IdRelation = id });
            DataSet data = Connection.selectQuery(requetteGetEventByIdRelation, list);
            DataTable table = data.Tables[0];
            DataRowCollection rows = table.Rows;
            foreach (DataRow row in rows)
            {
                eventsList.Add(EntitiesConverter.ConvertFromDataRowToEvents(row));
            }
            return eventsList;
        }

        public static int ConfirmEvents(int idEvent)
        {
            int nbLignes = -1;
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("id", idEvent));
            nbLignes=  Connection.Update(requetteConfirmEvents, list);
            return nbLignes;
        }

        public static int DeleteEvents(int idEvents)
        {
            int nbLignes = -1;
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("id", idEvents));
            nbLignes = Connection.Delete(requetteDeleteEvents, list);
            return nbLignes;
        }
        
        public static int Insert(int idUser,int idFriend,string desc)
        {
            int idGenerated = -1;
            int id=UsersService.GetIdRelation(idUser, idFriend);
            if (id > 0)
            {
                Events e = new Events { IdRelation = id, Description = desc,Date=DateTime.Now };
                List<SqlParameter> list = new List<SqlParameter>();
                list = MySqlParameterConverter.ConvertFromEvents(e);
                idGenerated= Connection.Insert(requetteInsertEvents, list);
            }
            return idGenerated;
        }
    }
}
