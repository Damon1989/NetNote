﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetNote.Models;

namespace NetNote.Migrations
{
    [DbContext(typeof(NoteContext))]
    partial class NoteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NetNote.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Attachment");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("Create");

                    b.Property<string>("Password");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("NetNote.Models.NoteType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("NoteTypes");
                });

            modelBuilder.Entity("NetNote.Models.Note", b =>
                {
                    b.HasOne("NetNote.Models.NoteType", "Type")
                        .WithMany("Notes")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
