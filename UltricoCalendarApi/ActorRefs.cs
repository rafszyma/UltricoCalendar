using Akka.Actor;

namespace UltricoCalendarApi
{
    public static class ActorRefs
    {
        public static IActorRef CalendarActor = Nobody.Instance;
    }
}