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

        public static List<Users> GetAllNotConfirmedFriend(int idUser)
        {
            return UsersService.getAllFriendNotConfirmed(idUser);
        }

        public static int DeleteFriend(int idUser, int idFriend)
        {
            return UsersService.DeleteFriend(idUser, idFriend);
        }

        public static int deleteUser(int idUser)
        {
            return UsersService.deleteUser(idUser);
        }

        public static List<Users> GetAllUserNotFriendByIdUser(int idUser)
        {
            return UsersService.getAllUserNotFriends(idUser);
        }
    }
}
