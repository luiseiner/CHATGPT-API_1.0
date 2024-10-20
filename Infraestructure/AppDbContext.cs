using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;


namespace Infraestructure
{
    public class ContextFactura : DbContext
    {

        //public ContextFactura(DbContextOptions options) : base(options)
        //{
        //}

        public ContextFactura() : base() { }


        public DbSet<Category> Categories { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Alternative> Alternatives { get; set; }
        public DbSet<Pront> Pronts { get; set; }
        public DbSet<UserResponse> UserResponses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ChatGptResponse> ChatGptResponses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            //var connectionString = Convert.ToString( Configuration.GetSection("ConnectionStrings")["Conexion"]);

            var connectionString = "Data Source=SQL5106.site4now.net;Initial Catalog=db_aada3d_prueba;User Id=db_aada3d_prueba_admin;Password=torrico...123T; TrustServerCertificate=True;";

            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(config.SecondsTimeOutBD));
                optionsBuilder.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(120));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             // Cambia la restricción de eliminación aquí

            modelBuilder.Entity<UserResponse>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserResponses)
                .HasForeignKey(ur => ur.UserID)
                .OnDelete(DeleteBehavior.Restrict); // Cambia la restricción de eliminación aquí

            // Configuración para UserResponse
            modelBuilder.Entity<UserResponse>()
                .HasOne(ur => ur.Question)
                .WithMany(q => q.UserResponses)
                .HasForeignKey(ur => ur.QuestionID)
                .OnDelete(DeleteBehavior.Restrict); // Cambia la restricción de eliminación aquí

            modelBuilder.Entity<UserResponse>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserResponses)
                .HasForeignKey(ur => ur.UserID)
                .OnDelete(DeleteBehavior.Restrict); // Cambia la restricción de eliminación aquí

            // Configuración para Question
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Category)
                .WithMany(c => c.Questions)
                .HasForeignKey(q => q.CategoryID)
                .OnDelete(DeleteBehavior.Restrict); // Cambia la restricción de eliminación aquí

            // Cambia la restricción de eliminación aquí

            // Configuración para Alternative
            modelBuilder.Entity<Alternative>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Alternatives)
                .HasForeignKey(a => a.QuestionID)
                .OnDelete(DeleteBehavior.Restrict); // Cambia la restricción de eliminación aquí

        }
    }

}

