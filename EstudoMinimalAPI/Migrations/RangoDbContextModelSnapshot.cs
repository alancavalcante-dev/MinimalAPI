﻿// <auto-generated />
using EstudoMinimalAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EstudoMinimalAPI.Migrations
{
    [DbContext(typeof(RangoDbContext))]
    partial class RangoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("EstudoMinimalAPI.Entities.Ingrediente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ingredientes");
                });

            modelBuilder.Entity("EstudoMinimalAPI.Entities.Rango", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Rangos");
                });

            modelBuilder.Entity("IngredienteRango", b =>
                {
                    b.Property<int>("IngredientesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RangosId")
                        .HasColumnType("INTEGER");

                    b.HasKey("IngredientesId", "RangosId");

                    b.HasIndex("RangosId");

                    b.ToTable("IngredienteRango");
                });

            modelBuilder.Entity("IngredienteRango", b =>
                {
                    b.HasOne("EstudoMinimalAPI.Entities.Ingrediente", null)
                        .WithMany()
                        .HasForeignKey("IngredientesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EstudoMinimalAPI.Entities.Rango", null)
                        .WithMany()
                        .HasForeignKey("RangosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
