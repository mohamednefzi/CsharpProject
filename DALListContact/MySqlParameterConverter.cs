using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALListContact
{
    class MySqlParameterConverter
    {
        public static List<SqlParameter> ConvertFromUser(Users user)
        {
            List<SqlParameter> myParamsSql = new List<SqlParameter>();

            if (user != null)
            {
                if ( user.ID != 0)
                {
                    myParamsSql.Add(new SqlParameter("id", user.ID));
                }
                if (user.FirstName != null)
                {
                    myParamsSql.Add(new SqlParameter("firstName", user.FirstName));
                }
                if (user.LastName != null)
                {
                    myParamsSql.Add(new SqlParameter("lastName", user.LastName));
                }
                if (user.Login != null)
                {
                    myParamsSql.Add(new SqlParameter("personnage", user.Login));
                }
                if (user.pwd != null)
                {
                    myParamsSql.Add(new SqlParameter("password", user.pwd));
                }
                if (user.MyAddress != null) {
                    myParamsSql.Add(new SqlParameter("idAddress", user.MyAddress.ID));
                }
                if (user.MyPicture != null)
                {
                    myParamsSql.Add(new SqlParameter("idAddress", user.MyPicture.ID));
                }
            }

            return myParamsSql;
        }

        public static List<SqlParameter> ConvertFromAdress(Address address)
        {

            List<SqlParameter> myParamsSql = new List<SqlParameter>();
            if (address != null)
            {
                if (address.ID != 0)
                {
                    myParamsSql.Add(new SqlParameter("id", address.ID));
                }
                if (address.Number != 0)
                {
                    myParamsSql.Add(new SqlParameter("number", address.Number));
                }
                if (address.Street != null)
                {
                    myParamsSql.Add(new SqlParameter("street", address.Street));
                }
                if (address.City != null)
                {
                    myParamsSql.Add(new SqlParameter("city", address.City));
                }
                if (address.Province != null)
                {
                    myParamsSql.Add(new SqlParameter("province", address.Province));
                }
                if (address.Country != null)
                {
                    myParamsSql.Add(new SqlParameter("country", address.Country));
                }

            }
            return myParamsSql;
        }
        
        public static List<SqlParameter> ConvertFrompicture(Picture pic)
        {

            List<SqlParameter> myParamsSql = new List<SqlParameter>();
            if (pic.ID != 0)
            {
                myParamsSql.Add(new SqlParameter("id", pic.ID));
            }
            if (pic.Src != null)
            {
                myParamsSql.Add(new SqlParameter("src", pic.Src));
            }

            return myParamsSql;
        }

        public static List<SqlParameter> ConvertFromEvents(Events events)
        {
            List<SqlParameter> myParamsSql = new List<SqlParameter>();

            if ( events.ID != 0)
            {
                myParamsSql.Add(new SqlParameter("id", events.ID));
            }
            if ( events.IdRelation != 0)
            {
                myParamsSql.Add(new SqlParameter("IdRelation", events.IdRelation));
            }
            if (events.Description != null)
            {
                myParamsSql.Add(new SqlParameter("desc", events.Description));
            }
            if (events.Date != null)
            {
                myParamsSql.Add(new SqlParameter("date", events.Date));
            }
            if (events.IsConfirmed != null)
            {
                myParamsSql.Add(new SqlParameter("isConfirmed", events.IsConfirmed)); k
            }

            return myParamsSql;
        }
    }
}
