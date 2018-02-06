using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DALConnection;
using System.Data;

namespace DALListContact
{
    public class UsersService
    {
        static string requetteInsert = @"insert into users (lastName,firstName,personnage,password,idAddress,idPicture) output inserted.id values (@firstName,@lastName,@personnage,@password,@idAddress,@idPicture)";
        static string requetteCountFriend = @"select count(*) as count from users U inner join usersContactList UCL on U.id=UCL.idUser where U.id=@id";
        static string requetteAddFriend = @"insert into usersContactList (idUser,idFriend,isFriend) output inserted.id values (@idUser,@idFriend,@isFriend)";
        static string requetteUpdateFriend = @"update usersContactList set isFriend=@isFriend where idUser=@idUser and idFriend=@idFriend";
        static string requettedeleteFriend = @"delete from usersContactList where idUser IN (@idUser,@idFriend) and idFriend IN (@idUser,@idFriend)";
        static string requetteDeleteAllRelationUsers = @"delete from usersContactList where idUser =@idUser or idFriend=@idUser";
        static string requetteGetById = @"select * from users where id=@id";
        static string requetteDeleteUser = @"delete from users where id=@id";
        static string requetteGetAllUsers = @" select * from users where id!=@id";
        static string requetteGetAllFriend = @"select * from  users as U inner join usersContactList as UCL on U.id=UCL.idUser where U.id=@id AND UCL.isFriend=1";
        static string requetteGetAllFriendNotConfirmed = @"select * from  users as U inner join usersContactList as UCL on U.id=UCL.idUser where U.id=@id AND UCL.isFriend=0";
        static string requetteGetUsersNotFriend = @"select * from users where id NOT IN (select idFriend from usersContactList where idUser=@id) AND id NOT IN (select idUser from usersContactList where idFriend=@id)";
        static string requetteGetUserRequestFriendRecieved = @"select * from users where id IN (select idFriend from usersContactList where idFriend=@id and isFriend=false)";
        static string requetteGetIdRelation = @"select * from usersContactList where idUser=@idUser AND idFriend=@idFriend and isFriend=true";
        static string requetteSignIn = @"select * from users where personnage=@personnage and password=@password";
        static string VerifyLogin = @" select * from users where personnage=@personnage";

        
        //ok
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
                        users.MyAddress.ID = idGenerated;
                        List<SqlParameter> paramsList = MySqlParameterConverter.ConvertFromUser(users);
                        idGenerated = Connection.Insert(requetteInsert, paramsList);
                    }
                }
            }
            return idGenerated;
        }


        //ok
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

        //ok
        private static int GetCountUsersFriends(int idUser)

        {
            List<SqlParameter> list = new List<SqlParameter>();
            int countFriends;
            list.Add(new SqlParameter("id", idUser));
            DataSet data = Connection.selectQuery(requetteCountFriend, list);
            DataTable table = data.Tables[0];
            DataRowCollection rows = table.Rows;
            countFriends = Convert.ToInt32(rows[0]["count"]);
            return countFriends;
        }

        //ok
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

        //ok
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
        /// retour =1 cette demande d'amitié est supprimé
        /// retour =2 vous n'etes plus amis
        /// </returns>
        // ok
        public static int DeleteFriend(int idUser, int idFriend)
        {
            int ligne1;
            ligne1 = DeleteLineFriend(idUser, idFriend);

            return ligne1;
        }

        //ok
        private static int DeleteLineFriend(int idUser, int idFriend)
        {
            int nbLigne = 0;
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("idUser", idUser));
            list.Add(new SqlParameter("idFriend", idFriend));
            nbLigne = Connection.Delete(requettedeleteFriend, list);
            return nbLigne;
        }

        //OK
        public static int deleteUser(int idUser)
        {
            int nbLigne = 0;
            List<SqlParameter> list = new List<SqlParameter>();
            List<SqlParameter> list1 = new List<SqlParameter>();
            list.Add(new SqlParameter("idUser", idUser));
            list1.Add(new SqlParameter("id", idUser));
            DeleteAllRelationUsers(idUser, list);
            int idAddress = GetById(idUser).MyAddress.ID;
            Connection.Delete(requetteDeleteUser, list1);
            if (idAddress > 0)
                AddressServices.DeleteById(idAddress);
            return nbLigne;
        }

        //ok
        private static int DeleteAllRelationUsers(int idUser, List<SqlParameter> list)
        {
            int nbLigne = 0;
            nbLigne = Connection.Delete(requetteDeleteAllRelationUsers, list);
            return nbLigne;

        }

        //ok
        public static Users GetById(int idUser)
        {
            Users u = new Users { ID = idUser };
            List<SqlParameter> list = MySqlParameterConverter.ConvertFromUser(u);
            DataSet data = Connection.selectQuery(requetteGetById, list);
            DataTable table = data.Tables[0];
            DataRowCollection rows = table.Rows;
            if (rows.Count > 0)
            {
                u = EntitiesConverter.ConvertFromDataRowToUser(rows[0]);
                u.MyPicture = PictureService.getById(u.MyPicture.ID);
                u.MyAddress = AddressServices.GetById(u.MyAddress.ID);
            }
            else
            {
                u = null;
            }
            return u;
        }

        //ok
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
                u1.MyPicture = PictureService.getById(u1.MyPicture.ID);
                u1.MyAddress = AddressServices.GetById(u1.MyAddress.ID);
                allUsers.Add(u1);
            }
            return allUsers;
        }

        //ok
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
        
        //ok
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
                u1.MyPicture = PictureService.getById(u1.MyPicture.ID);
                u1.MyAddress = AddressServices.GetById(u1.MyAddress.ID);
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

        //ok
        public static Users SignIn(string login, string pwd)
        {
            Users u = new Users { Login = login, pwd = pwd };
            List<SqlParameter> list = MySqlParameterConverter.ConvertFromUser(u);
            DataSet data = Connection.selectQuery(requetteSignIn, list);
            DataTable table = data.Tables[0];
            DataRowCollection rows = table.Rows;
            if (rows.Count==1)
            {
                u = GetById(Convert.ToInt32(rows[0]["id"]));
            }
            else
            {
                u = null;
            }


            return u;
        }

        //ok
        public static bool VerifyUserName(string login)
        {
            bool exist = false;
            Users u = new Users { Login = login };
            List<SqlParameter> list = MySqlParameterConverter.ConvertFromUser(u);
            DataSet data = Connection.selectQuery(VerifyLogin, list);
            DataTable table = data.Tables[0];
            DataRowCollection rows = table.Rows;
            int a = rows.Count;
            if (rows.Count == 1)
            {
                exist = true;

            }
            return exist;
        }


        public static List<Users> GetUserRequestFriendRecieved(int idUser)
        {
            Users u = new Users { ID = idUser };
            List<SqlParameter> list = MySqlParameterConverter.ConvertFromUser(u);
            List<Users> allUsers = new List<Users>();
            DataSet data = Connection.selectQuery(requetteGetUserRequestFriendRecieved, list);
            DataTable table = data.Tables[0];
            DataRowCollection rows = table.Rows;
            foreach (DataRow row in rows)
            {
                Users u1 = GetById(Convert.ToInt32(row["idFriend"]));
                allUsers.Add(u1);
            }
            return allUsers;
        }

    }

    //hors classe

}
