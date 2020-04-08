using GoldenNumbers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldenNumbers.DAL
{
    public class UserDAL
    {
        GoldenNumberDatabaseEntities db = new GoldenNumberDatabaseEntities();
        #region User
        public List<User> getUsersList()
        {
            List<User> Users;
            GoldenNumberDatabaseEntities db = new GoldenNumberDatabaseEntities();

            Users = db.Users.Where(x => x.IsActive == 1).ToList();


            return Users;
        }

        public User getUserById(int _Id)
        {

            GoldenNumberDatabaseEntities db = new GoldenNumberDatabaseEntities();


            User Users = db.Users.Where(x => x.IsActive == 1).FirstOrDefault(x => x.Id == _Id);


            return Users;
        }

        public bool AddUser(User _User)
        {
            using (db = new GoldenNumberDatabaseEntities())
            {
                db.Users.Add(_User);
                db.SaveChanges();
            }
            return true;
        }

        public bool UpdateUser(User _User)
        {
            using (db = new GoldenNumberDatabaseEntities())
            {
                db.Entry(_User).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return true;
        }

        public void DeleteUser(int _id)
        {
            using (db = new GoldenNumberDatabaseEntities())
            {
                db.Users.Remove(db.Users.FirstOrDefault(x => x.Id == _id));
                db.SaveChanges();
            }
        }
        #endregion
    }
}