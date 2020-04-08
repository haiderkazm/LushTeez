using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldenNumbers.Helping_Classes
{
    public class SessionDTO
    {
        public string getEmail()
        {
            SessionDTO ndto = (SessionDTO)HttpContext.Current.Session["Session"];
            return ndto?.Email;
        }

        public int getId()
        {
            SessionDTO ndto = (SessionDTO)HttpContext.Current.Session["Session"];
            return ((ndto != null) ? ndto.Id : -1);
        }

        public string getName()
        {
            SessionDTO ndto = (SessionDTO)HttpContext.Current.Session["Session"];
            return ndto?.Name;
        }

        public int getRole()
        {
            SessionDTO ndto = (SessionDTO)HttpContext.Current.Session["Session"];
            return ((ndto != null) ? ndto.Role : -1);
        }

        public string Name { get; set; }

        public int Id { get; set; }

        public int Role { get; set; }

        public string Email { get; set; }
    }
}