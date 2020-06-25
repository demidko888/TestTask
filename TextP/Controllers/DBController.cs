using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TextP.Models;

namespace TextP.Controllers
{
    public static class DBController
    {        
        public static void WriteAllDB(DbSet<Word> words)
        {
            Console.WriteLine("Current status of db: ");
            foreach (var e in words)
                e.Write();
            Console.WriteLine("-------------------------------");
        }

        public static void AddOrUpdateEntity(DBWords db,Word entity)
        {
            var dbEntity = db.Words.Where(w => w.Name == entity.Name).FirstOrDefault();
            if (dbEntity != null)
                dbEntity.Frequency += entity.Frequency;
            else
                db.Add(entity);
            db.SaveChanges();
        }

        public static void AddOrUpdateEntity(DBWords db, List<Word> entities)
        {
            foreach(var entity in entities)
            {
                var dbEntity = db.Words.Where(w => w.Name == entity.Name).FirstOrDefault();
                if(dbEntity==null)
                    dbEntity = db.Words.Local.Where(w => w.Name == entity.Name).FirstOrDefault();
                if (dbEntity != null)
                    dbEntity.Frequency += entity.Frequency;
                else
                    db.Add(entity);
            }
            db.SaveChanges();
        }

        public static void DeleteDB(DBWords db)
        {
            //db.Words.RemoveRange(db.Words);
            db.Database.EnsureDeleted();
        }
    }
}
