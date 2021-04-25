﻿// <auto-generated />
using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Domain.Migrations
{
    [DbContext(typeof(DiaDaVacinaContext))]
    [Migration("20210425133341_comit")]
    partial class comit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Domain.Models.DataVacina", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Estado")
                        .HasColumnType("text");

                    b.Property<int>("idade")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("DataVacina");
                });

            modelBuilder.Entity("Domain.Models.Pessoa", b =>
                {
                    b.Property<long>("Telefone")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("Date");

                    b.Property<string>("Estado")
                        .HasColumnType("text");

                    b.Property<string>("Sexo")
                        .HasColumnType("text");

                    b.HasKey("Telefone");

                    b.HasIndex("DataNascimento");

                    b.ToTable("Pessoa");
                });
#pragma warning restore 612, 618
        }
    }
}
