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
        
        public DbSet<EventSeries<FinishAfterDate>> DateEventSeries { get; set; }
        
        public DbSet<EventSeries<FinishAfterOccurs>> CountEventSeries { get; set; }
        
        public DbSet<EventSeries<NeverFinish>> NeverFinishEventSeries { get; set; }
        
        public DbSet<EditedSeriesEvent> EditedSeriesEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapSingleEvents(modelBuilder);
            MapDateEventSeries(modelBuilder);
            MapCountEventSeries(modelBuilder);
            MapNeverFinishEventSeries(modelBuilder);
            MapEditedSeriesEvents(modelBuilder);
        }

        private void MapEditedSeriesEvents(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<EditedSeriesEvent>();
            // TODO FINISH IT
        }

        private void MapSingleEvents(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<SingleEvent>();
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).IsRequired();
            entity.Property(x => x.Start).IsRequired();
            entity.Property(x => x.Duration).IsRequired();
        }
        
        private void MapDateEventSeries(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<EventSeries<FinishAfterDate>>();
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).IsRequired();
            entity.Property(x => x.Start).IsRequired();
            entity.Property(x => x.Duration).IsRequired();
            entity.Property(x => x.RepeatEvery).IsRequired();
            // TODO Add serialization here
            entity.Property(x => x.Finish).IsRequired();
        }

        private void MapCountEventSeries(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<EventSeries<FinishAfterOccurs>>();
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).IsRequired();
            entity.Property(x => x.Start).IsRequired();
            entity.Property(x => x.Duration).IsRequired();
            entity.Property(x => x.RepeatEvery).IsRequired();
            // TODO Add serialization here
            entity.Property(x => x.Finish).IsRequired();
        }

        private void MapNeverFinishEventSeries(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<EventSeries<NeverFinish>>();
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).IsRequired();
            entity.Property(x => x.Start).IsRequired();
            entity.Property(x => x.Duration).IsRequired();
            entity.Property(x => x.RepeatEvery).IsRequired();
            entity.Property(x => x.Finish).IsRequired();
        }
        
    }
}