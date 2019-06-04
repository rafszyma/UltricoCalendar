using Akka.Actor;
using Autofac;
using UltricoCalendarCommon;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Interfaces.Service;

namespace UltricoCalendarService.Actors
{
    public class QueryActor : ReceiveActor
    {
        private readonly IEventFromSeriesService _eventFromSeriesService =
            UltricoModule.IoCContainer.Resolve<IEventFromSeriesService>();

        private readonly IEventSeriesService _eventSeriesService =
            UltricoModule.IoCContainer.Resolve<IEventSeriesService>();

        private readonly IMetadataService _metadataService = UltricoModule.IoCContainer.Resolve<IMetadataService>();

        private readonly ISingleEventService _singleEventService =
            UltricoModule.IoCContainer.Resolve<ISingleEventService>();

        public QueryActor()
        {
            Receive<Queries.GetEventMetadata>(query =>
            {
                var result = _metadataService.GetMetadata(query.From, query.To);
                Context.Sender.Tell(result);
            });

            Receive<Queries.EventQueries<SingleEvent>.Get>(query =>
            {
                var result = _singleEventService.GetEvent(query.Id);
                Context.Sender.Tell(result);
            });

            Receive<Queries.EventQueries<EventSeries>.Get>(query =>
            {
                var result = _eventSeriesService.GetEventSeries(query.Id);
                Context.Sender.Tell(result);
            });

            Receive<Queries.EventQueries<EventFromSeries>.Get>(query =>
            {
                var result = _eventFromSeriesService.GetEventFromSeries(query.Id);
                Context.Sender.Tell(result);
            });
        }

        private static Props Props => Props.Create(() => new QueryActor());

        public static void Create(ActorSystem system)
        {
            system.ActorOf(Props, "query-actor");
        }
    }
}