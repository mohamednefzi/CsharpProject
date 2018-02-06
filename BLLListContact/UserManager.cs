using DALListContact;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLLListContact
{
    public class UserManager
    {
        public static long insertUser(Users user)
        {
            return UsersService.InsertUser(user);
        }

        public static Users signIn(string login, string password)
        {
            return UsersService.SignIn(login, password);
        }

        public static Boolean UsernameExist(String login)
        {
            return UsersService.VerifyUserName(login);
        }

        public static Users GetUserById(int id)
        {
            return UsersService.GetById(id);
        }


        public static List<Users> getAllUserSaufCurrentUser(int idUser)
        {
            return UsersService.getAll(idUser);
        }


        public static int AddUserToFriend(int idUser, int idFriend)
        {
            return UsersService.AddFriend(idUser, idFriend);
        }

        public static List<Users> GetAllFriendByUser(int idUser)
        {
            return UsersService.getAllFriend(idUser);
        }
    }
}
