﻿// <auto-generated />
using System;
using Infraestructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infraestructure.Migrations
{
    [DbContext(typeof(ContextFactura))]
    partial class ContextFacturaModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Alternative", b =>
                {
                    b.Property<int>("AlternativeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlternativeID"));

                    b.Property<string>("CreationDate")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("QuestionID")
                        .HasColumnType("int");

                    b.Property<string>("TextEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextEs")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("TranslationChatGptEn")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("TranslationChatGptEs")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("AlternativeID");

                    b.HasIndex("QuestionID");

                    b.ToTable("Alternatives");
                });

            modelBuilder.Entity("Domain.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("CreationDate")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEs")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Prompt")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.ChatGptResponse", b =>
                {
                    b.Property<int>("ResponseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResponseID"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Request")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Response")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ResponseID");

                    b.ToTable("ChatGptResponses");
                });

            modelBuilder.Entity("Domain.Language", b =>
                {
                    b.Property<int>("LanguageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LanguageID"));

                    b.Property<string>("CreationDate")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("LanguageID");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Domain.Pront", b =>
                {
                    b.Property<int>("ProntID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProntID"));

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatePront")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ProntID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("UserID");

                    b.ToTable("Pronts");
                });

            modelBuilder.Entity("Domain.Question", b =>
                {
                    b.Property<int>("QuestionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionID"));

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("CreationDate")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("QuestionTypeID")
                        .HasColumnType("int");

                    b.Property<string>("TextEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextEs")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("QuestionID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("QuestionTypeID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Domain.QuestionType", b =>
                {
                    b.Property<int>("QuestionTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionTypeID"));

                    b.Property<string>("CreationDate")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TypeEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeEs")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("QuestionTypeID");

                    b.ToTable("QuestionTypes");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("CreationDate")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<int>("LanguageID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("LanguageID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.UserResponse", b =>
                {
                    b.Property<int>("UserResponseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserResponseID"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("QuestionID")
                        .HasColumnType("int");

                    b.Property<string>("ResponseEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponseEs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("UserResponseID");

                    b.HasIndex("QuestionID");

                    b.HasIndex("UserID");

                    b.ToTable("UserResponses");
                });

            modelBuilder.Entity("Domain.Alternative", b =>
                {
                    b.HasOne("Domain.Question", "Question")
                        .WithMany("Alternatives")
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Domain.Pront", b =>
                {
                    b.HasOne("Domain.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Question", b =>
                {
                    b.HasOne("Domain.Category", "Category")
                        .WithMany("Questions")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.QuestionType", "QuestionType")
                        .WithMany()
                        .HasForeignKey("QuestionTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("QuestionType");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.HasOne("Domain.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("Domain.UserResponse", b =>
                {
                    b.HasOne("Domain.Question", "Question")
                        .WithMany("UserResponses")
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.User", "User")
                        .WithMany("UserResponses")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Category", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Domain.Question", b =>
                {
                    b.Navigation("Alternatives");

                    b.Navigation("UserResponses");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Navigation("UserResponses");
                });
#pragma warning restore 612, 618
        }
    }
}
