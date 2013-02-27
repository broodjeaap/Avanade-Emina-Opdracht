using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Emina.Models
{
    public class EminaContext : DbContext
    {
        public DbSet<Enquete> Enquetes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<PossibleAnswer> PossibleAnswers { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Answer>().
            /*
            modelBuilder.Entity<Answer>()
                .HasRequired(a => a.Question)
                .WithRequiredDependent()
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Answer>()
                .HasRequired(a => a.Enquete)
                .WithRequiredDependent()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Answer>()
                .HasRequired(a => a.PossibleAnswer)
                .WithRequiredDependent()
                .WillCascadeOnDelete(false);
            */
        }
    }
}