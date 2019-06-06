using Akka.Actor;
using Autofac;
using UltricoCalendarCommon;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Interfaces.Service;

namespace UltricoCalendarService.Actors
{
    public class EmailActor : ReceiveActor
    {
        private readonly IEmailService _emailService =
            UltricoModule.IoCContainer.Resolve<IEmailService>(); 
        public EmailActor()
        {
            Receive<Commands.SendEmailCommand>(command =>
            {
                var result = _emailService.SendNotificationEmailsFromPeriod(command.From, command.To);
            });
        }
        public static Props Props => Props.Create(() => new QueryActor());

        public static void Create(ActorSystem system)
        {
            system.ActorOf(Props, "email-actor");
        }
    }
}