using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Users
    {
        public Users()
        {
            MyPicture = new Picture();
            MyAddress = new Address();
        }

        public Users(int iD, string lastName, string firstName, string login, string pwd, Address myAddress, Picture myPicture, List<Users> usersFriend)
        {
            ID = iD;
            LastName = lastName;
            FirstName = firstName;
            Login = login;
            this.pwd = pwd;
            MyAddress = myAddress;
            MyPicture = myPicture;
            UsersFriend = usersFriend;
        }

        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Login { get; set; }
        public string pwd { get; set; }
        public Address MyAddress { get; set; }
        public Picture MyPicture { get; set; }
        public List<Users> UsersFriend { get; set; }

    }
}
