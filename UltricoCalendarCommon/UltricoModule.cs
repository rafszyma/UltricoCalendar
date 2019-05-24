using System;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Autofac;

namespace UltricoCalendarCommon
{
    public abstract class UltricoModule : Module
    {
        private static readonly AutoResetEvent WaitHandle = new AutoResetEvent(false);
        
        public abstract void MigrateDatabase(IContainer container);

        public abstract void CreateActors(ActorSystem system);
        
        public void SystemShutdown(ActorSystem system)
        {
            Console.CancelKeyPress += (o, e) =>
            {
                CoordinatedShutdown.Get(system).Run().Wait(TimeSpan.FromSeconds(30));
                WaitHandle.Set();
            };
            WaitHandle.WaitOne();
        }
        
        
    }
}