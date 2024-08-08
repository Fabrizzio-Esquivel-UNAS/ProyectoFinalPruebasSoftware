﻿// <auto-generated />
using System;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240803222101_Entidades")]
    partial class Entidades
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Calendario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("AccessTokenExpiration")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("EventType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventsPageToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("SchedulingUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserUri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Calendarios");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Cita", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AsesorUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AsesoradoUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("DesarrolloAsesor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DesarrolloAsesorado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<string>("EventoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AsesorUserId");

                    b.HasIndex("AsesoradoUserId");

                    b.HasIndex("EventoId")
                        .IsUnique();

                    b.ToTable("Citas");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Contrato", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateOnly?>("FechaFinal")
                        .HasColumnType("date");

                    b.Property<DateOnly>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<Guid>("SolicitudId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SolicitudId");

                    b.ToTable("Contratos");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Escuela", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("FacultadId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("FacultadId");

                    b.ToTable("Escuelas");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ef858bce-40b4-4445-aa8a-87c6edf80e65"),
                            Deleted = false,
                            FacultadId = new Guid("6bc517b3-d8b3-47b2-b81d-e30f5a615129"),
                            Nombre = "Admin Escuela"
                        });
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Facultad", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Facultades");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6bc517b3-d8b3-47b2-b81d-e30f5a615129"),
                            Deleted = false,
                            Nombre = "Admin Facultad"
                        });
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.GrupoInvestigacion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("GruposInvestigacion");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8167563c-5ecb-4778-bab9-f603f35df52d"),
                            Deleted = false,
                            Nombre = "Admin Grupo de Investigacion"
                        });
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.HistorialCoordinador", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GrupoInvestigacionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GrupoInvestigacionId");

                    b.HasIndex("UserId");

                    b.ToTable("HistorialCoordinadores");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.LineaInvestigacion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("FacultadId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GrupoInvestigacionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("FacultadId");

                    b.HasIndex("GrupoInvestigacionId");

                    b.ToTable("LineasInvestigacion");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8167563c-5ecb-4778-bac8-f603f35df55f"),
                            Deleted = false,
                            FacultadId = new Guid("6bc517b3-d8b3-47b2-b81d-e30f5a615129"),
                            GrupoInvestigacionId = new Guid("8167563c-5ecb-4778-bab9-f603f35df52d"),
                            Nombre = "Admin Linea de Investigacion"
                        });
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Solicitud", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AsesorUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AsesoradoUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("FechaCreacion")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("FechaRespuesta")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Mensaje")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumeroTesis")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AsesorUserId");

                    b.HasIndex("AsesoradoUserId");

                    b.ToTable("Solicitudes");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Tenants");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b542bf25-134c-47a2-a0df-84ed14d03c4a"),
                            Deleted = false,
                            Name = "Admin Tenant"
                        });
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AsesoriaEstado")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<Guid?>("EscuelaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("LastLoggedinDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("LineaInvestigacionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Notificaciones")
                        .HasColumnType("tinyint");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Preferencias")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("EscuelaId");

                    b.HasIndex("LineaInvestigacionId");

                    b.HasIndex("TenantId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000010"),
                            AsesoriaEstado = 0,
                            Codigo = "0123456789",
                            Deleted = false,
                            Email = "admin@email.com",
                            EscuelaId = new Guid("ef858bce-40b4-4445-aa8a-87c6edf80e65"),
                            FirstName = "Admin",
                            LastName = "User",
                            LineaInvestigacionId = new Guid("8167563c-5ecb-4778-bac8-f603f35df55f"),
                            Notificaciones = (byte)1,
                            Password = "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2",
                            Preferencias = "",
                            Role = 0,
                            Status = 0,
                            Telefono = "123456789",
                            TenantId = new Guid("b542bf25-134c-47a2-a0df-84ed14d03c4a")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            AsesoriaEstado = 0,
                            Codigo = "0123456789",
                            Deleted = false,
                            Email = "coordinador@email.com",
                            EscuelaId = new Guid("ef858bce-40b4-4445-aa8a-87c6edf80e65"),
                            FirstName = "Coordinador",
                            LastName = "User",
                            LineaInvestigacionId = new Guid("8167563c-5ecb-4778-bac8-f603f35df55f"),
                            Notificaciones = (byte)1,
                            Password = "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2",
                            Preferencias = "",
                            Role = 1,
                            Status = 0,
                            Telefono = "123456789",
                            TenantId = new Guid("b542bf25-134c-47a2-a0df-84ed14d03c4a")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            AsesoriaEstado = 0,
                            Codigo = "0123456789",
                            Deleted = false,
                            Email = "asesor@email.com",
                            EscuelaId = new Guid("ef858bce-40b4-4445-aa8a-87c6edf80e65"),
                            FirstName = "Asesor",
                            LastName = "User",
                            LineaInvestigacionId = new Guid("8167563c-5ecb-4778-bac8-f603f35df55f"),
                            Notificaciones = (byte)1,
                            Password = "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2",
                            Preferencias = "",
                            Role = 2,
                            Status = 0,
                            Telefono = "123456789",
                            TenantId = new Guid("b542bf25-134c-47a2-a0df-84ed14d03c4a")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000003"),
                            AsesoriaEstado = 0,
                            Codigo = "0123456789",
                            Deleted = false,
                            Email = "asesorado@email.com",
                            EscuelaId = new Guid("ef858bce-40b4-4445-aa8a-87c6edf80e65"),
                            FirstName = "Asesorado",
                            LastName = "User",
                            LineaInvestigacionId = new Guid("8167563c-5ecb-4778-bac8-f603f35df55f"),
                            Notificaciones = (byte)1,
                            Password = "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2",
                            Preferencias = "",
                            Role = 3,
                            Status = 0,
                            Telefono = "123456789",
                            TenantId = new Guid("b542bf25-134c-47a2-a0df-84ed14d03c4a")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000004"),
                            AsesoriaEstado = 0,
                            Codigo = "0123456789",
                            Deleted = false,
                            Email = "user@email.com",
                            EscuelaId = new Guid("ef858bce-40b4-4445-aa8a-87c6edf80e65"),
                            FirstName = "User",
                            LastName = "User",
                            LineaInvestigacionId = new Guid("8167563c-5ecb-4778-bac8-f603f35df55f"),
                            Notificaciones = (byte)1,
                            Password = "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2",
                            Preferencias = "",
                            Role = 4,
                            Status = 0,
                            Telefono = "123456789",
                            TenantId = new Guid("b542bf25-134c-47a2-a0df-84ed14d03c4a")
                        });
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Calendario", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.User", "User")
                        .WithOne("Calendario")
                        .HasForeignKey("CleanArchitecture.Domain.Entities.Calendario", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Cita", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.User", "AsesorUser")
                        .WithMany()
                        .HasForeignKey("AsesorUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CleanArchitecture.Domain.Entities.User", "AsesoradoUser")
                        .WithMany()
                        .HasForeignKey("AsesoradoUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AsesorUser");

                    b.Navigation("AsesoradoUser");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Contrato", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Solicitud", "Solicitud")
                        .WithMany()
                        .HasForeignKey("SolicitudId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Solicitud");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Escuela", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Facultad", "Facultad")
                        .WithMany("Escuelas")
                        .HasForeignKey("FacultadId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Facultad");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.HistorialCoordinador", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.GrupoInvestigacion", "GrupoInvestigacion")
                        .WithMany("HistorialCoordinadores")
                        .HasForeignKey("GrupoInvestigacionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CleanArchitecture.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("GrupoInvestigacion");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.LineaInvestigacion", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Facultad", "Facultad")
                        .WithMany("LineasInvestigacion")
                        .HasForeignKey("FacultadId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CleanArchitecture.Domain.Entities.GrupoInvestigacion", "GrupoInvestigacion")
                        .WithMany("LineasInvestigacion")
                        .HasForeignKey("GrupoInvestigacionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Facultad");

                    b.Navigation("GrupoInvestigacion");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Solicitud", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.User", "AsesorUser")
                        .WithMany()
                        .HasForeignKey("AsesorUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CleanArchitecture.Domain.Entities.User", "AsesoradoUser")
                        .WithMany()
                        .HasForeignKey("AsesoradoUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AsesorUser");

                    b.Navigation("AsesoradoUser");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.User", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Escuela", "Escuela")
                        .WithMany("Users")
                        .HasForeignKey("EscuelaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CleanArchitecture.Domain.Entities.LineaInvestigacion", "LineaInvestigacion")
                        .WithMany()
                        .HasForeignKey("LineaInvestigacionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CleanArchitecture.Domain.Entities.Tenant", "Tenant")
                        .WithMany("Users")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Escuela");

                    b.Navigation("LineaInvestigacion");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Escuela", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Facultad", b =>
                {
                    b.Navigation("Escuelas");

                    b.Navigation("LineasInvestigacion");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.GrupoInvestigacion", b =>
                {
                    b.Navigation("HistorialCoordinadores");

                    b.Navigation("LineasInvestigacion");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Tenant", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.User", b =>
                {
                    b.Navigation("Calendario");
                });
#pragma warning restore 612, 618
        }
    }
}
