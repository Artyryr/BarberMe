﻿// <auto-generated />
using System;
using BarberMe.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BarberMe.Migrations.ApplicationDb
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BarberMe.Models.Barber", b =>
                {
                    b.Property<int>("BarberId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BarbershopId");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Facebook");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("Instagram");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PhotoLink");

                    b.Property<string>("Telephone")
                        .IsRequired();

                    b.HasKey("BarberId");

                    b.HasIndex("BarbershopId");

                    b.ToTable("Barbers");
                });

            modelBuilder.Entity("BarberMe.Models.Barbershop", b =>
                {
                    b.Property<int>("BarbershopId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("BarbershopUserId")
                        .IsRequired();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Facebook");

                    b.Property<string>("Geoposition");

                    b.Property<string>("Instagram");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PhotoLink");

                    b.Property<string>("Telephone")
                        .IsRequired();

                    b.HasKey("BarbershopId");

                    b.ToTable("Barbershops");
                });

            modelBuilder.Entity("BarberMe.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BarberId");

                    b.Property<int?>("BarbershopId");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<int?>("PaymentId");

                    b.Property<double>("Price");

                    b.Property<int?>("ScheduleId");

                    b.Property<int?>("ServiceId");

                    b.Property<string>("Telephone")
                        .IsRequired();

                    b.HasKey("OrderId");

                    b.HasIndex("BarberId");

                    b.HasIndex("BarbershopId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("ScheduleId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BarberMe.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CVV")
                        .IsRequired();

                    b.Property<string>("CardNumber")
                        .IsRequired();

                    b.Property<string>("CardOwner")
                        .IsRequired();

                    b.Property<DateTime>("ExpiryDate");

                    b.HasKey("PaymentId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("BarberMe.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BarberId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int>("Rating");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("ReviewId");

                    b.HasIndex("BarberId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("BarberMe.Models.Schedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Availability");

                    b.Property<int>("BarberId");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("Time");

                    b.HasKey("ScheduleId");

                    b.HasIndex("BarberId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("BarberMe.Models.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BarbershopId");

                    b.Property<string>("ServiceDescription");

                    b.Property<int>("ServiceDuration");

                    b.Property<string>("ServiceName")
                        .IsRequired();

                    b.Property<double>("ServicePrice");

                    b.Property<int>("ServiceTypeId");

                    b.HasKey("ServiceId");

                    b.HasIndex("BarbershopId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("BarberMe.Models.ServiceType", b =>
                {
                    b.Property<int>("ServiceTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("ServiceTypeName")
                        .IsRequired();

                    b.HasKey("ServiceTypeId");

                    b.ToTable("ServiceTypes");
                });

            modelBuilder.Entity("BarberMe.Models.Barber", b =>
                {
                    b.HasOne("BarberMe.Models.Barbershop")
                        .WithMany("Barbers")
                        .HasForeignKey("BarbershopId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BarberMe.Models.Order", b =>
                {
                    b.HasOne("BarberMe.Models.Barber", "Barber")
                        .WithMany()
                        .HasForeignKey("BarberId");

                    b.HasOne("BarberMe.Models.Barbershop", "Barbershop")
                        .WithMany()
                        .HasForeignKey("BarbershopId");

                    b.HasOne("BarberMe.Models.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId");

                    b.HasOne("BarberMe.Models.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId");

                    b.HasOne("BarberMe.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId");
                });

            modelBuilder.Entity("BarberMe.Models.Review", b =>
                {
                    b.HasOne("BarberMe.Models.Barber")
                        .WithMany("Reviews")
                        .HasForeignKey("BarberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BarberMe.Models.Schedule", b =>
                {
                    b.HasOne("BarberMe.Models.Barber")
                        .WithMany("Schedule")
                        .HasForeignKey("BarberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BarberMe.Models.Service", b =>
                {
                    b.HasOne("BarberMe.Models.Barbershop")
                        .WithMany("Services")
                        .HasForeignKey("BarbershopId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
