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
