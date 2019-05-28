using Akka.Actor;
using Autofac;
using UltricoCalendarCommon;
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
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CalendarActor>().PropertiesAutowired().AsSelf();
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