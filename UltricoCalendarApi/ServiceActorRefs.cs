using Akka.Actor;

namespace UltricoCalendarApi
{
    public static class ServiceActorRefs
    {
        public static IActorRef CalendarServiceActor;
        
        public static IActorRef CalendarQueryActor;
    }
}