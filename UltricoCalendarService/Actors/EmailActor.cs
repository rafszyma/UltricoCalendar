using System;
using Akka.Actor;
using Autofac;
using UltricoCalendarCommon;
using UltricoCalendarContracts;
using UltricoCalendarContracts.Interfaces.Service;

namespace UltricoCalendarService.Actors
{
    public class EmailActor : ReceiveActor
    {
        private readonly IEmailNotificationService _emailNotificationService =
            UltricoModule.IoCContainer.Resolve<IEmailNotificationService>(); 
        
        public EmailActor()
        {
            Receive<Commands.SendEmailCommand>(command =>
            {
                _emailNotificationService.SendNotificationEmailsFromPeriod(DateTime.Now);
            });
        }

        private static Props Props => Props.Create(() => new EmailActor());

        public static IActorRef Create(ActorSystem system)
        {
            return system.ActorOf(Props, "email-actor");
        }
    }
}