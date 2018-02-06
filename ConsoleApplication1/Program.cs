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
             //Users u = new Users { FirstName = "test", LastName = "Nefzi", Login = "test", pwd = "momo", MyAddress = new Address { Number = 12, City = "city", Country = "canada", Province = "quebec", Street = "street" },MyPicture= new Picture {ID=1 } };
            // Users u = new Users { ID=2,FirstName = "hatem", LastName = "chaaben", Login = "hatem", pwd = "chaaben", MyAddress = new Address {ID=2, Number = 12, City = "city4", Country = "canada32", Province = "quebec234", Street = "street" },MyPicture= new Picture {ID=2 } };

            //int a= UsersService.InsertUser(u);
            //  int a1 = UsersService.InsertUser(u1);
            //Users u = UsersService.GetById(8);
            //bool u = UsersService.VerifyUserName("hatem");
            //int c = UsersService.AddFriend(14, 8);
            //UsersService.ConfirmNewFriend(8, 2);
            //  int u = UsersService.deleteUser(13);
            //Console.WriteLine(u);
            //List<Users> us = UsersService.getAll(2);
            // List<Users> us = UsersService.getAllFriend(8);
             List<Users> us = UsersService.getAllFriendNotConfirmed(14);
            //List<Users> us = UsersService.getAllUserNotFriends(8);
            //List<Users> us = UsersService.GetUserRequestFriendRecieved(8);
            foreach (Users b  in us)
            {
                Console.WriteLine(b.ID + b.LastName);
            }
            

           // UsersService.Update(u);
            /* List<Events> l = EventServices.getEventsByIdRelation(2, 8);
              foreach (Events b in l)
              {
                  Console.WriteLine(b.ID + b.Description);
              }*/
            //  EventServices.ConfirmEvents(1);
            //EventServices.DeleteEvents(5);

        }
    }
}
