using System.Threading.Tasks;
using Akka.Actor;
using Autofac;

namespace UltricoCalendarCommon
{
    public abstract class UltricoModule : Module
    {
        public abstract void MigrateDatabase(IContainer container);

        public abstract void CreateActors(ActorSystem system);
    }
}