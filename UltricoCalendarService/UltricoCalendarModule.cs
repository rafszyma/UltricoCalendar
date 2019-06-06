using System;
using Akka.Actor;
using Autofac;
using UltricoCalendarCommon;
using UltricoCalendarCommon.Settings;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Interfaces.Repository;
using UltricoCalendarContracts.Interfaces.Service;
using UltricoCalendarService.Actors;
using UltricoCalendarService.Repositories;
using UltricoCalendarService.Service;

namespace UltricoCalendarService
{
    public class UltricoCalendarModule : UltricoModule
    {
        public override void MigrateDatabase(IContainer container)
        {
        }

        public override void CreateActors(ActorSystem system)
        {
            CalendarActor.Create(system);
            QueryActor.Create(system);
            
            // TODO take FromMinutes value from config
            system.Scheduler.ScheduleTellRepeatedly(TimeSpan.Zero, TimeSpan.FromMinutes(1), EmailActor.Create(system), new Commands.SendEmailCommand(), ActorRefs.NoSender);
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CalendarServiceSettings>().PropertiesAutowired().SingleInstance().AsSelf();
            builder.RegisterType<CalendarActor>().PropertiesAutowired().AsSelf();
            builder.RegisterType<QueryActor>().PropertiesAutowired().AsSelf();
            builder.RegisterType<EmailActor>().PropertiesAutowired().AsSelf();

            builder.RegisterType<EmailNotificationService>().As<IEmailNotificationService>().PropertiesAutowired();
            builder.RegisterType<CalendarService>().As<ISingleEventService>().PropertiesAutowired();
            builder.RegisterType<CalendarService>().As<IEventSeriesService>().PropertiesAutowired();
            builder.RegisterType<CalendarService>().As<IEventFromSeriesService>().PropertiesAutowired();
            builder.RegisterType<CalendarService>().As<IMetadataService>().PropertiesAutowired();

            builder.RegisterType<CalendarRepository>().As<ISingleEventRepository>().PropertiesAutowired();
            builder.RegisterType<CalendarRepository>().As<IEventSeriesRepository>().PropertiesAutowired();
            builder.RegisterType<CalendarRepository>().As<IEventFromSeriesRepository>().PropertiesAutowired();

            base.Load(builder);
        }
    }
}