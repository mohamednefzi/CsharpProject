using DALListContact;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLLListContact
{
    public class EventsManager
    {
        public static int AddEvent(int idUser, int idFriend, String description)
        {
            return EventServices.Insert(idUser, idFriend, description);
        }

        public static List<Events> GetAllFriendsEvents(int idUser, int idFriend)
        {
            return EventServices.getEventsByIdRelation(idUser, idFriend);
        }

        public static int DeleteEvents(int idEvent)
        {
            return EventServices.DeleteEvents(idEvent);
        }


        public static int ConfirmEvents(int idEvent)
        {
            return EventServices.ConfirmEvents(idEvent);
        }
    }
}
