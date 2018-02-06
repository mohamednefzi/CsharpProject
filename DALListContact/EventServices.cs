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
        static string requetteGetEventByIdRelation = @"select * from events where idRelation=@idRelation";
        static string requetteConfirmEvents = @"update events set isConfirmed=1 where id=@id";
        static string requetteDeleteEvents = @"delete from events where id=@id";
        static string requetteInsertEvents = @"insert into events (description,idRelation,date) output inserted.id values(@description,@idRelation,@date)";
        static string requetteDeleteByIdUser = @"delete from events where idRelation=@id";
        
        public static List<Events> getEventsByIdRelation(int idUser, int idFriend)
        {
            List<Events> eventsList = new List<Events>();
            int id = UsersService.GetIdRelation(idUser, idFriend);
            List<SqlParameter> list = MySqlParameterConverter.ConvertFromEvents(new Events { IdRelation = id, Date = DateTime.Now });
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
            nbLignes = Connection.Update(requetteConfirmEvents, list);
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

        internal static int DeleteByIdUser(int idUser)
        {
            int nbLignes = 0;
            List<int> listId = new List<int>();
            listId = UsersService.GetIdRelationByIdUser(idUser);
            foreach (int item in listId)
            {
                List<SqlParameter> list = new List<SqlParameter>();
                list.Add(new SqlParameter("id", item));
                nbLignes += Connection.Delete(requetteDeleteByIdUser, list);
            }

            return nbLignes;
        }

        internal static int DeleteByIdRelation(int idUser, int idFriend)
        {
            int nbLignes = 0;

            int id1 = UsersService.GetIdRelation(idUser, idFriend);
            int id2 = UsersService.GetIdRelation(idUser, idFriend);
            if (id1 > 0)
            {
                List<SqlParameter> list = new List<SqlParameter>();
                list.Add(new SqlParameter("id", id1));
                nbLignes += Connection.Delete(requetteDeleteByIdUser, list);
            }
            if (id1 > 0)
            {
                List<SqlParameter> list1 = new List<SqlParameter>();
                list1.Add(new SqlParameter("id", id2));
                nbLignes += Connection.Delete(requetteDeleteByIdUser, list1);
            }
            return nbLignes;
        }

        public static int Insert(int idUser, int idFriend, string desc)
        {
            int idGenerated = -1;
            int id = UsersService.GetIdRelation(idUser, idFriend);
            if (id > 0)
            {
                Events e = new Events { IdRelation = id, Description = desc, Date = DateTime.Now };
                List<SqlParameter> list = new List<SqlParameter>();
                list = MySqlParameterConverter.ConvertFromEvents(e);
                idGenerated = Connection.Insert(requetteInsertEvents, list);
            }
            return idGenerated;
        }
    }
}
