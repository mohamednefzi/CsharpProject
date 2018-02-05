using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALListContact
{
    class EntitiesConverter
    {
        internal static Users ConvertFromDataRowToUser(DataRow dataRow)
        {
            Users u = new Users();
            if (dataRow != null)
            {
                u.ID = Convert.ToInt32(dataRow["id"]);
                if (dataRow["firstName"] != DBNull.Value)
                {
                    u.FirstName = Convert.ToString(dataRow["firstName"]);
                }
                if (dataRow["lastName"] != DBNull.Value)
                {
                    u.FirstName = Convert.ToString(dataRow["lastName"]);
                }
                if (dataRow["personnage"] != DBNull.Value)
                {
                    u.FirstName = Convert.ToString(dataRow["personnage"]);
                }
                if (dataRow["password"] != DBNull.Value)
                {
                    u.FirstName = Convert.ToString(dataRow["password"]);
                }
                if (dataRow["idAddress"] != DBNull.Value)
                {
                    u.MyAddress = new Address();
                    u.MyAddress.ID = Convert.ToInt32(dataRow["idAddress"]);
                }
                if (dataRow["idPicture"] != DBNull.Value)
                {
                    u.MyPicture = new Picture();
                    u.MyPicture.ID = Convert.ToInt32(dataRow["idPicture"]);
                }
            }
            return u;
        }

        internal static Picture ConvertFromDataRowToPicture(DataRow dataRow)
        {
            Picture p = new Picture();
            if (dataRow != null)
            {
                p.ID = Convert.ToInt32(dataRow["id"]);
                p.Src = Convert.ToString(dataRow["src"]);
            }
            return p;
        }

        internal static Address ConvertFromDataRowToAddress(DataRow dataRow)
        {
            Address adr = new Address();
            if (dataRow != null)
            {
                adr.ID = Convert.ToInt32(dataRow["id"]);
                if (dataRow["number"] != DBNull.Value) {
                    adr.Number= Convert.ToInt32(dataRow["number"]);
                }
                if (dataRow["street"] != DBNull.Value)
                {
                    adr.Street = Convert.ToString(dataRow["street"]);
                }
                if (dataRow["city"] != DBNull.Value)
                {
                    adr.City = Convert.ToString(dataRow["city"]);
                }
                if (dataRow["province"] != DBNull.Value)
                {
                    adr.Province = Convert.ToString(dataRow["province"]);
                }
                if (dataRow["country"] != DBNull.Value)
                {
                    adr.Country = Convert.ToString(dataRow["country"]);
                }
            }

            return adr;
        }

        internal static Events ConvertFromDataRowToEvents(DataRow dataRow)
        {
            Events e =null;
            if (dataRow != null)
            {
                e = new Events();
                e.ID=Convert.ToInt32(dataRow["id"]);
                e.IdRelation = Convert.ToInt32(dataRow["idRelation"]);
                if (dataRow["description"] != DBNull.Value)
                {
                    e.Description = Convert.ToString(dataRow["description"]);
                }
                if (dataRow["isConfirmed"] != DBNull.Value)
                {
                    e.IsConfirmed = Convert.ToBoolean(dataRow["isConfirmed"]);
                }else
                {
                    e.IsConfirmed = null;
                }
                if(dataRow["date"] != DBNull.Value)
                {
                    e.Date = Convert.ToDateTime(dataRow["date"]);
                }
            }

            return e;
        }

    }
}
