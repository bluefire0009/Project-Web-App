﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebCalendaar.Models;

#nullable disable

namespace WebCalendaar.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("WebCalendaar.Models.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AdminId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Admin");

                    b.HasData(
                        new
                        {
                            AdminId = 1,
                            Email = "admin1@example.com",
                            Password = "^�H��(qQ��o��)'s`=\rj���*�rB�",
                            UserName = "admin1"
                        },
                        new
                        {
                            AdminId = 2,
                            Email = "admin2@example.com",
                            Password = "\\N@6��G��Ae=j_��a%0�QU��\\",
                            UserName = "admin2"
                        },
                        new
                        {
                            AdminId = 3,
                            Email = "admin3@example.com",
                            Password = "�j\\��f������x�s+2��D�o���",
                            UserName = "admin3"
                        },
                        new
                        {
                            AdminId = 4,
                            Email = "admin4@example.com",
                            Password = "�].��g��Պ��t��?��^�T��`aǳ",
                            UserName = "admin4"
                        },
                        new
                        {
                            AdminId = 5,
                            Email = "admin5@example.com",
                            Password = "E�=���:�-����gd����bF��80]�",
                            UserName = "admin5"
                        });
                });

            modelBuilder.Entity("WebCalendaar.Models.Attendance", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("AttendanceDate")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "AttendanceDate");

                    b.ToTable("Attendance");
                });

            modelBuilder.Entity("WebCalendaar.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AdminApproval")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("EventDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("EventId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("WebCalendaar.Models.Event_Attendance", b =>
                {
                    b.Property<int>("Event_AttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("DatePlaced")
                        .HasColumnType("TEXT");

                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Feedback")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Rating")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Event_AttendanceId");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("Event_Attendance");
                });

            modelBuilder.Entity("WebCalendaar.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AttendanceIds")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RecuringDays")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("WebCalendaar.Models.Attendance", b =>
                {
                    b.HasOne("WebCalendaar.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebCalendaar.Models.Event_Attendance", b =>
                {
                    b.HasOne("WebCalendaar.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebCalendaar.Models.User", "User")
                        .WithMany("Event_Attendances")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebCalendaar.Models.User", b =>
                {
                    b.Navigation("Event_Attendances");
                });
#pragma warning restore 612, 618
        }
    }
}
