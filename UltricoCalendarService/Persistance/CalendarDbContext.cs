using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UltricoCalendarCommon;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Extensions;

namespace UltricoCalendarService.Persistance
{
    public class CalendarDbContext : DbContext
    { 
        public CalendarDbContext(DbContextOptions<CalendarDbContext> options)
            : base(options)
        {
        }
        
        public CalendarDbContext() : base(GenerateOptions())
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
            entity.Property(x => x.Duration).HasConversion(d => d.ToJson(), d => EventDuration.FromJson(d))
                .IsRequired();
            entity.Property(x => x.MailAddresses)
                .HasConversion(ma => ListMapper.ToJson(ma), ma => ListMapper.FromJson(ma));
            entity.Property(x => x.EventSeriesId).IsRequired();
        }

        private void MapSingleEvents(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<SingleEvent>();
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).IsRequired();
            entity.Property(x => x.Start).IsRequired();
            entity.Property(x => x.Duration).HasConversion(d => d.ToJson(), d => EventDuration.FromJson(d)).IsRequired();
            entity.Property(x => x.MailAddresses)
                .HasConversion(ma => ListMapper.ToJson(ma), ma => ListMapper.FromJson(ma));
        }
        
        private void MapEventSeries(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<EventSeries>();
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).IsRequired();
            entity.Property(x => x.Start).IsRequired();
            entity.Property(x => x.Duration).HasConversion(d => d.ToJson(), d => EventDuration.FromJson(d)).IsRequired();
            entity.Property(x => x.RepeatPeriod).IsRequired();
            entity.HasMany(x => x.EditedEvents).WithOne(x => x.EventSeries);
            entity.Property(x => x.Finish).HasConversion(v => v.ToString(), v => FinishClass.FromJson(v)).IsRequired();
            entity.Property(x => x.MailAddresses)
                .HasConversion(ma => ListMapper.ToJson(ma), ma => ListMapper.FromJson(ma));
            entity.Property(x => x.DeletedOccurrences)
                .HasConversion(deleted => ListMapper.ToJsonDateTime(deleted), deleted => ListMapper.FromJsonDateTime(deleted));
        }
        
        private static DbContextOptions<CalendarDbContext> GenerateOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CalendarDbContext>();
            return optionsBuilder.UseInMemoryDatabase("myDb").UseLazyLoadingProxies().Options;
        }
    }
}