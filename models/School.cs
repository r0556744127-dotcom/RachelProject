using Microsoft.EntityFrameworkCore;
using Repositories.InterFaces;
using Repositories.models;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeFirst.models
{
    //Scaffold-DbContext "Server=sql;Database=School_Racheli;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
    public class School:DbContext, IContext
    {
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Stuff> Stuff { get; set; }

        public DbSet<Submission> Submissions { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<User> LessonCategory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=School_Racheli;trusted_connection=true;TrustServerCertificate=true");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // כאן את אומרת לו להפריד לטבלאות שונות ב-SQL
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Student>().ToTable("Students");
        modelBuilder.Entity<Stuff>().ToTable("Stuffs");

        // כאן גם שמנו קודם את הפתרון לבעיית ה-Cascade אם הוספת אותו
        base.OnModelCreating(modelBuilder);
    }
        public Task SaveAsync()
        {
            return SaveChangesAsync(); // מחזיר Task אמיתי מ־EF Core
        }


    }
}
