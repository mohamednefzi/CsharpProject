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


    }
}
