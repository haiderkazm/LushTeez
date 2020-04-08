using GoldenNumbers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldenNumbers.DAL
{
    public class NumberDAL
    {
        GoldenNumberDatabaseEntities db; 

        #region Number
        public List<Number> getNumbersList()
        {
            List<Number> Numbers;
            GoldenNumberDatabaseEntities db = new GoldenNumberDatabaseEntities();

            Numbers = db.Numbers.Where(x => x.IsActive == 1).ToList();


            return Numbers;
        }

        public Number getNumberById(int _Id)
        {

            GoldenNumberDatabaseEntities db = new GoldenNumberDatabaseEntities();

           
            Number Numbers = db.Numbers.Where(x => x.IsActive == 1).FirstOrDefault(x => x.Id == _Id);


            return Numbers;
        }

        public bool AddNumber(Number _NUmber)
        {
            using (db = new GoldenNumberDatabaseEntities())
            {
                db.Numbers.Add(_NUmber);
                db.SaveChanges();
            }
            return true;
        }

        public bool UpdateNumber(Number _Number)
        {
            using (db = new GoldenNumberDatabaseEntities())
            {
                db.Entry(_Number).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return true;
        }

        public void DeleteNumber(int _id)
        {
            using (db = new GoldenNumberDatabaseEntities())
            {
                db.Numbers.Remove(db.Numbers.FirstOrDefault(x => x.Id == _id));
                db.SaveChanges();
            }
        }
        #endregion
    }
}