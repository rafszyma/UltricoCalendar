using Akka.Actor;

namespace UltricoCalendarService.Actors
{
    public class QueryActor : ReceiveActor
    {
        public static Props Props => Props.Create(() => new CalendarActor());
        
        public QueryActor()
        {
        }
        
        public static void Create(ActorSystem system)
        {
            system.ActorOf(Props,"query-actor");
        }
    }
}