using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Stroika.DAL.Entity;

namespace Stroika.DAL
{
    public partial class StroikaDBContext : DbContext
    {
       
        public StroikaDBContext(DbContextOptions<StroikaDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Nationality> Nationalities { get; set; }

        public virtual DbSet<Relationship> Relationships { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<StudentFamily> StudentFamilies { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.Property(e => e.NationalityId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Relationship>(entity =>
            {
                entity.Property(e => e.RelationshipId).ValueGeneratedNever();
            });
             
            base.OnModelCreating(modelBuilder);
        }

        public static void EnsureDBCreated(StroikaDBContext db)
        {
            var isCreated = db.Database.EnsureCreated();
            if (isCreated)
            {
                db.Nationalities.Add(new Stroika.DAL.Entity.Nationality { NationalityId = 1, NationalityName = "Egyptian" });
                db.Nationalities.Add(new Stroika.DAL.Entity.Nationality { NationalityId = 2, NationalityName = "Saudi Arabia" });
                db.Nationalities.Add(new Stroika.DAL.Entity.Nationality { NationalityId = 3, NationalityName = "United Arab Emirates" });
                db.Nationalities.Add(new Stroika.DAL.Entity.Nationality { NationalityId = 4, NationalityName = "Bahrain" });

                db.Relationships.Add(new Stroika.DAL.Entity.Relationship { RelationshipId = 1, RelationshipName = "Father" });
                db.Relationships.Add(new Stroika.DAL.Entity.Relationship { RelationshipId = 2, RelationshipName = "Mother" });
                db.Relationships.Add(new Stroika.DAL.Entity.Relationship { RelationshipId = 3, RelationshipName = "Sibling" });
                db.SaveChanges();

            }
        }
    }
}
