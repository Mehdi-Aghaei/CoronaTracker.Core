﻿// <auto-generated />
using CoronaTracker.Core.Brokers.Storages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoronaTracker.Core.Migrations
{
    [DbContext(typeof(StorageBroker))]
    partial class StorageBrokerModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CoronaTracker.Core.Models.Countries.Country", b =>
                {
                    b.Property<string>("CountryName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Cases")
                        .HasColumnType("int");

                    b.Property<int>("CasesPerOneMillion")
                        .HasColumnType("int");

                    b.Property<string>("Continent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Deaths")
                        .HasColumnType("int");

                    b.Property<int>("DeathsPerOneMillion")
                        .HasColumnType("int");

                    b.Property<string>("Iso3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Population")
                        .HasColumnType("int");

                    b.Property<int>("Recovered")
                        .HasColumnType("int");

                    b.Property<float>("RecoveredPerOneMillion")
                        .HasColumnType("real");

                    b.Property<int>("TodayCases")
                        .HasColumnType("int");

                    b.Property<int>("TodayDeaths")
                        .HasColumnType("int");

                    b.Property<int>("TodayRecovered")
                        .HasColumnType("int");

                    b.Property<float>("criticalPerOneMillion")
                        .HasColumnType("real");

                    b.HasKey("CountryName");

                    b.ToTable("Countries");
                });
#pragma warning restore 612, 618
        }
    }
}