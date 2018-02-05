using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALListContact;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
          //  Users u = new Users { FirstName = "Mohamed", LastName = "Nefzi", Login = "momo", pwd = "momo", MyAddress = new Address { Number = 12, City = "city", Country = "canada", Province = "quebec", Street = "street" },MyPicture= new Picture {ID=1 } };
           //int a= UsersService.InsertUser(u);
           
            foreach(Users b  in UsersService.getAll(1))
            {
                Console.WriteLine(b.ID + b.LastName);
            }
        }
    }
}
