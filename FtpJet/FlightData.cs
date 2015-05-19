

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Configuration file:     "FtpJet\Web.config"
//     Connection String Name: "AW"
//     Connection String:      "Server=.\SQL2014;Database=FlightData;Trusted_Connection=Yes"

// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
// TargetFrameworkVersion = 4.51
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Threading;
using System.Threading.Tasks;
using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

namespace FtpJet
{
    // ************************************************************************
    // Unit of work
    public interface IMyDbContext : IDisposable
    {
        IDbSet<Airport> Airports { get; set; } // Airports
        IDbSet<Route> Routes { get; set; } // Routes
        IDbSet<Sysdiagram> Sysdiagrams { get; set; } // sysdiagrams

        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

    // ************************************************************************
    // Database context
    public class MyDbContext : DbContext, IMyDbContext
    {
        public IDbSet<Airport> Airports { get; set; } // Airports
        public IDbSet<Route> Routes { get; set; } // Routes
        public IDbSet<Sysdiagram> Sysdiagrams { get; set; } // sysdiagrams
        
        static MyDbContext()
        {
            Database.SetInitializer<MyDbContext>(null);
        }

        public MyDbContext()
            : base("Name=AW")
        {
        }

        public MyDbContext(string connectionString) : base(connectionString)
        {
        }

        public MyDbContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AirportConfiguration());
            modelBuilder.Configurations.Add(new RouteConfiguration());
            modelBuilder.Configurations.Add(new SysdiagramConfiguration());
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new AirportConfiguration(schema));
            modelBuilder.Configurations.Add(new RouteConfiguration(schema));
            modelBuilder.Configurations.Add(new SysdiagramConfiguration(schema));
            return modelBuilder;
        }
    }

    // ************************************************************************
    // POCO classes

    // Airports
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.13.0.0")]
    public class Airport
    {
        public int AirportId { get; set; } // AirportId (Primary key)
        public string Name { get; set; } // Name
        public string City { get; set; } // City
        public string Country { get; set; } // Country
        public string FaaIata { get; set; } // FAA_IATA
        public string Icao { get; set; } // ICAO
        public decimal Latitude { get; set; } // Latitude
        public decimal Longitude { get; set; } // Longitude
        public int Altitude { get; set; } // Altitude
        public decimal TimezoneOffset { get; set; } // TimezoneOffset
        public string Dst { get; set; } // DST
        public string Tzdb { get; set; } // tzdb
        public System.Data.Entity.Spatial.DbGeography Location { get; set; } // Location

        // Reverse navigation
        public virtual ICollection<Route> Routes_DestinationAirport { get; set; } // Routes.FK_DestinationAirport
        public virtual ICollection<Route> Routes_SourceAirport { get; set; } // Routes.FK_Routes_SourceAirport
        
        public Airport()
        {
            FaaIata = "";
            Icao = "";
            Routes_DestinationAirport = new List<Route>();
            Routes_SourceAirport = new List<Route>();
        }
    }

    // Routes
    public class Route
    {
        public string Airline { get; set; } // Airline
        public int? AirlineId { get; set; } // AirlineId
        public int? SourceAirport { get; set; } // SourceAirport
        public int? DestinationAirport { get; set; } // DestinationAirport
        public string Codeshare { get; set; } // Codeshare
        public int Stops { get; set; } // Stops
        public string Equipment { get; set; } // Equipment
        public int Id { get; set; } // Id (Primary key)

        // Foreign keys
        public virtual Airport Airport_DestinationAirport { get; set; } // FK_DestinationAirport
        public virtual Airport Airport_SourceAirport { get; set; } // FK_Routes_SourceAirport
        
        public Route()
        {
            Codeshare = " ";
            Stops = 0;
        }
    }

    // sysdiagrams
    public class Sysdiagram
    {
        public string Name { get; set; } // name
        public int PrincipalId { get; set; } // principal_id
        public int DiagramId { get; set; } // diagram_id (Primary key)
        public int? Version { get; set; } // version
        public byte[] Definition { get; set; } // definition
    }


    // ************************************************************************
    // POCO Configuration

    // Airports
    internal class AirportConfiguration : EntityTypeConfiguration<Airport>
    {
        public AirportConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Airports");
            HasKey(x => x.AirportId);

            Property(x => x.AirportId).HasColumnName("AirportId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName("Name").IsRequired().IsUnicode(false).HasMaxLength(255);
            Property(x => x.City).HasColumnName("City").IsRequired().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Country).HasColumnName("Country").IsRequired().IsUnicode(false).HasMaxLength(255);
            Property(x => x.FaaIata).HasColumnName("FAA_IATA").IsRequired().HasMaxLength(50);
            Property(x => x.Icao).HasColumnName("ICAO").IsRequired().IsUnicode(false).HasMaxLength(4);
            Property(x => x.Latitude).HasColumnName("Latitude").IsRequired().HasPrecision(9,6);
            Property(x => x.Longitude).HasColumnName("Longitude").IsRequired().HasPrecision(9,6);
            Property(x => x.Altitude).HasColumnName("Altitude").IsRequired();
            Property(x => x.TimezoneOffset).HasColumnName("TimezoneOffset").IsRequired();
            Property(x => x.Dst).HasColumnName("DST").IsRequired().IsUnicode(false).HasMaxLength(1);
            Property(x => x.Tzdb).HasColumnName("tzdb").IsRequired().IsUnicode(false).HasMaxLength(50);
            Property(x => x.Location).HasColumnName("Location").IsOptional();
        }
    }

    // Routes
    internal class RouteConfiguration : EntityTypeConfiguration<Route>
    {
        public RouteConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Routes");
            HasKey(x => x.Id);

            Property(x => x.Airline).HasColumnName("Airline").IsRequired().IsUnicode(false).HasMaxLength(3);
            Property(x => x.AirlineId).HasColumnName("AirlineId").IsOptional();
            Property(x => x.SourceAirport).HasColumnName("SourceAirport").IsOptional();
            Property(x => x.DestinationAirport).HasColumnName("DestinationAirport").IsOptional();
            Property(x => x.Codeshare).HasColumnName("Codeshare").IsRequired().IsUnicode(false).HasMaxLength(1);
            Property(x => x.Stops).HasColumnName("Stops").IsRequired();
            Property(x => x.Equipment).HasColumnName("Equipment").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Foreign keys
            HasOptional(a => a.Airport_SourceAirport).WithMany(b => b.Routes_SourceAirport).HasForeignKey(c => c.SourceAirport); // FK_Routes_SourceAirport
            HasOptional(a => a.Airport_DestinationAirport).WithMany(b => b.Routes_DestinationAirport).HasForeignKey(c => c.DestinationAirport); // FK_DestinationAirport
        }
    }

    // sysdiagrams
    internal class SysdiagramConfiguration : EntityTypeConfiguration<Sysdiagram>
    {
        public SysdiagramConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".sysdiagrams");
            HasKey(x => x.DiagramId);

            Property(x => x.Name).HasColumnName("name").IsRequired().HasMaxLength(128);
            Property(x => x.PrincipalId).HasColumnName("principal_id").IsRequired();
            Property(x => x.DiagramId).HasColumnName("diagram_id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Version).HasColumnName("version").IsOptional();
            Property(x => x.Definition).HasColumnName("definition").IsOptional();
        }
    }

}

