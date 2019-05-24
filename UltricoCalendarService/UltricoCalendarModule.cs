using Akka.Actor;
using Autofac;
using UltricoCalendarCommon;
using UltricoCalendarService.Actors;

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
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CalendarActor>().PropertiesAutowired().AsSelf();
            base.Load(builder);
        }
    }
}