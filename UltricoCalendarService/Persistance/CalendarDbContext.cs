using System;
using Microsoft.EntityFrameworkCore;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Entities;

namespace UltricoCalendarService.Persistance
{
    public class CalendarDbContext : DbContext
    { 
        public CalendarDbContext(DbContextOptions<CalendarDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<SingleEvent> SingleEvents { get; set; }
        
        public DbSet<EventSeries> EventSeries { get; set; }
        
        public DbSet<EditedSeriesEvent> EditedSeriesEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapSingleEvents(modelBuilder);
            MapEventSeries(modelBuilder);
            MapEditedSeriesEvents(modelBuilder);
        }

        private void MapEditedSeriesEvents(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<EditedSeriesEvent>();
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).IsRequired();
            entity.Property(x => x.Start).IsRequired();
            entity.Property(x => x.Duration).IsRequired();
            entity.Property(x => x.EventSeriesId).IsRequired();
        }

        private void MapSingleEvents(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<SingleEvent>();
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).IsRequired();
            entity.Property(x => x.Start).IsRequired();
            entity.Property(x => x.Duration).IsRequired();
        }
        
        private void MapEventSeries(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<EventSeries>();
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).IsRequired();
            entity.Property(x => x.Start).IsRequired();
            entity.Property(x => x.Duration).IsRequired();
            entity.Property(x => x.RepeatEvery).IsRequired();
            entity.HasMany(x => x.EditedEvents).WithOne(x => x.EventSeries);
            entity.Property(x => x.Finish).IsRequired();
        }
    }
}