﻿using DALListContact;
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



    }
}