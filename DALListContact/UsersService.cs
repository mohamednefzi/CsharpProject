using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALConnection;
using System.Data;

namespace DALListContact
{
    public class UsersService
    {
        static string requetteInsert = @" insert into users ('lastName','firstName','personnage','password') output inserted.id (@firstName,@lastName,@personnage,@password,@idAddress,@idPicture)";
        static string requetteCountFriend = @"select count(*) as count from users where 'id'=@id";
        static string requetteAddFriend = @"insert into usersContactList ('idUser','idFriend','isFriend') output inserted.id (@idUser,@idFriend,@isFriend)";
        static string requetteUpdateFriend = @"update usersContactList set isFriend=@isFriend where idUser=@idUser and idFriend=@idFriend";
        static string requettedeleteFriend = @" delete from usersContactList where idUser IN (@idUser,@idFriend) and idFriend IN (@idUser,@idFriend)";
        static string requetteDeleteAllRelationUsers = @"delete from usersContactList where idUser =@idUser or idFriend=@idUser";
        static string requetteGetById = @" select * from users where id=@id";
        static string requetteDeleteUser = @"delete from users where id=@id";
        static string requetteGetAllUsers = @" select * from users where id!=@id";
        static string requetteGetAllFriend = @"select * from  users as U inner join usersContactList as UCL on U.id= UCL.idUser where U.id=@id AND UCL.isFriend=true";
        static string requetteGetAllFriendNotConfirmed = @"select * from  users as U inner join usersContactList as UCL on U.id= UCL.idUser where U.id=@id AND UCL.isFriend=false";
        static string requetteGetUsersNotFriend = @"select * from users where id NOT IN (select idFriend from usersContactList where idUser=@id) AND id NOT IN (select idUser from usersContactList where idFriend=@id)";
        static string requetteGetUserRequestFriendRecieved = @"select * from users where id IN (select idFriend from usersContactList where idUser=@id and isFriend=false)";
        static string requetteGetIdRelation = @"select * from usersContactList where idUser=@idUser AND idFriend=@idFriend and isFriend=true";
        static string requetteSignIn = @"select * from users where personnage=@personnage and password=@password";
        static string VerifyLogin = @" select * from users where personnage=@personnage";

        public static int InsertUser(Users users)
        {
            int idGenerated = -1;
            if (users != null)
            {
                if (users.MyAddress != null)
                {
                    idGenerated = AddressServices.Insert(users.MyAddress);
                    if (idGenerated != -1)
                    {
                        List<SqlParameter> paramsList = MySqlParameterConverter.ConvertFromUser(users);
                        idGenerated = Connection.Insert(requetteInsert, paramsList);
                    }
                }
            }
            return idGenerated;
        }

        public static int AddFriend(int idUser, int idFriend)
        {
            int idGenerated = -1;

            if (GetCountUsersFriends(idUser) >= 5)
            {
                idGenerated = -3;
            }
            else
            {
                List<SqlParameter> list = new List<SqlParameter>();
                list.Add(new SqlParameter("idUser", idUser));
                list.Add(new SqlParameter("idFriend", idFriend));
                list.Add(new SqlParameter("isFriend", false));
                idGenerated = Connection.Insert(requetteAddFriend, list);
            }

            return idGenerated;
        }

        private static int GetCountUsersFriends(int idUser)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            int countFriends;
            list.Add(new SqlParameter("id", idUser));
            DataSet data = Connection.selectQuery(requetteCountFriend, list);
            DataTable table = data.Tables[0];
            DataRowCollection rows = table.Rows;
            countFriends = Convert.ToInt32(rows[0]["id"]);
            return countFriends;
        }

        public static int ConfirmNewFriend(int idUser, int idFriend)
        {
            int idGenerated = -1;
            if (GetCountUsersFriends(idUser) >= 5)
            {
                idGenerated = -3;
            }
            else
            {
                AddFriend(idUser, idFriend);
                Confirm(idUser, idFriend);
                Confirm(idFriend, idUser);
            }

            return idGenerated;
        }

        private static int Confirm(int idUser, int idFriend)
        {
            int nbLigne = 0;
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("idUser", idUser));
            list.Add(new SqlParameter("idFriend", idFriend));
            list.Add(new SqlParameter("isFriend", true));
            nbLigne = Connection.Update(requetteUpdateFriend, list);
            return nbLigne;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="idFriend"></param>
        /// <returns>
        /// retour =0 aucune relation d'amitié
        /// retour =1 vous avez annulé votre demande d'amis
        /// retour =2 vous avez refusé la demande d'amis
        /// rtour =3 relation d'amitié annulé
        /// </returns>
        public static int DeleteFriend(int idUser, int idFriend)
        {

            int ligne1, ligne2;
            ligne1 = DeleteLineFriend(idUser, idFriend);
            ligne2 = DeleteLineFriend(idFriend, idUser);

            return ligne1 + ligne2 * 2;

        }

        private static int DeleteLineFriend(int idUser, int idFriend)
        {
            int nbLigne = 0;
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("idUser", idUser));
            list.Add(new SqlParameter("idFriend", idFriend));
            nbLigne = Connection.Delete(requettedeleteFriend, list);

            return nbLigne;
        }

        public static int deleteUser(int idUser)
        {
            int nbLigne = 0;
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("idUser", idUser));
            DeleteAllRelationUsers(idUser, list);
            int idAddress = GetById(idUser).MyAddress.ID;
            Connection.Delete(requetteDeleteUser, list);
            AddressServices.DeleteById(idAddress);
            return nbLigne;
        }

        private static int DeleteAllRelationUsers(int idUser, List<SqlParameter> list)
        {
            int nbLigne = 0;
            nbLigne = Connection.Delete(requetteDeleteAllRelationUsers, list);
            return nbLigne;

        }

        public static Users GetById(int idUser)
        {
            Users u = new Users { ID = idUser };
            List<SqlParameter> list = MySqlParameterConverter.ConvertFromUser(u);
            DataSet data = Connection.selectQuery(requetteGetById, list);
            DataTable table = data.Tables[0];
            DataRowCollection rows = table.Rows;
            u = EntitiesConverter.ConvertFromDataRowToUser(rows[0]);
            u.MyPicture = PictureService.getById(u.MyPicture.ID);
            u.MyAddress = AddressServices.GetById(u.MyAddress.ID);
            return u;
        }

        public static List<Users> getAll(int idUser)
        {
            List<Users> allUsers = new List<Users>();
            Users u = new Users { ID = idUser };
            List<SqlParameter> list = MySqlParameterConverter.ConvertFromUser(u);
            DataSet data = Connection.selectQuery(requetteGetAllUsers, list);
            DataTable table = data.Tables[0];
            DataRowCollection rows = table.Rows;
            foreach (DataRow row in rows)
            {
                Users u1 = new Users();
                u1 = EntitiesConverter.ConvertFromDataRowToUser(row);
                u1.MyPicture = PictureService.getById(u.MyPicture.ID);
                u1.MyAddress = AddressServices.GetById(u.MyAddress.ID);
                allUsers.Add(u1);
            }
            return allUsers;
        }

        public static List<Users> getAllFriend(int idUser)
        {
            Users u = new Users { ID = idUser };
            List<SqlParameter> list = MySqlParameterConverter.ConvertFromUser(u);
            List<Users> allUsers = new List<Users>();
            DataSet data = Connection.selectQuery(requetteGetAllFriend, list);
            DataTable table = data.Tables[0];
            DataRowCollection rows = table.Rows;
            foreach (DataRow row in rows)
            {
                Users u1 = GetById(Convert.ToInt32(row["idFriend"]));
                allUsers.Add(u1);
            }
            return allUsers;
        }

        public static List<Users> getAllFriendNotConfirmed(int idUser)
        {
            Users u = new Users { ID = idUser };
            List<SqlParameter> list = MySqlParameterConverter.ConvertFromUser(u);
            List<Users> allUsers = new List<Users>();
            DataSet data = Connection.selectQuery(requetteGetAllFriendNotConfirmed, list);
            DataTable table = data.Tables[0];
            DataRowCollection rows = table.Rows;
            foreach (DataRow row in rows)
            {
                Users u1 = GetById(Convert.ToInt32(row["idFriend"]));
                allUsers.Add(u1);
            }
            return allUsers;
        }

        public static List<Users> getAllUserNotFriends(int idUser)
        {
            Users u = new Users { ID = idUser };
            List<SqlParameter> list = MySqlParameterConverter.ConvertFromUser(u);
            List<Users> allUsers = new List<Users>();
            DataSet data = Connection.selectQuery(requetteGetUsersNotFriend, list);
            DataTable table = data.Tables[0];
            DataRowCollection rows = table.Rows;
            foreach (DataRow row in rows)
            {
                Users u1 = new Users();
                u1 = EntitiesConverter.ConvertFromDataRowToUser(row);
                u1.MyPicture = PictureService.getById(u.MyPicture.ID);
                u1.MyAddress = AddressServices.GetById(u.MyAddress.ID);
                allUsers.Add(u1);
            }
            return allUsers;
        }

        internal static int GetIdRelation(int idUser, int idFriend)
        {
            int id = -1;
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("idUser", idUser));
            list.Add(new SqlParameter("idFriend", idFriend));
            DataSet data = Connection.selectQuery(requetteGetIdRelation, list);
            DataTable table = data.Tables[0];
            DataRowCollection rows = table.Rows;
            if (rows != null)
            {
                id = Convert.ToInt32(rows[0]["id"]);
            }
            return id;
        }

        public static Users SignIn(string login, string pwd)
        {
            Users u = new Users { Login = login, pwd = pwd };
            List<SqlParameter> list = MySqlParameterConverter.ConvertFromUser(u);
            DataSet data = Connection.selectQuery(requetteSignIn, list);
            DataTable table = data.Tables[0];
            DataRowCollection rows = table.Rows;
            if (rows != null)
            {
                u = GetById(Convert.ToInt32(rows[0]["id"]));
            }
            else
            {
                u = null;
            }


            return u;
        }


        public static bool VerifyUser(string login)
        {
            bool exist = false;
            Users u = new Users { Login = login };
            List<SqlParameter> list = MySqlParameterConverter.ConvertFromUser(u);
            DataSet data = Connection.selectQuery(VerifyLogin, list);
            DataTable table = data.Tables[0];
            DataRowCollection rows = table.Rows;
            if (rows != null)
            {
                exist = true;

            }
            return exist;
        }


    }

    //hors classe

}
