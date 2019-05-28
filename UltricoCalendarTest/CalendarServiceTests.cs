using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Extras.Moq;
using UltricoCalendarCommon;
using UltricoCalendarContracts.Interfaces.Repository;
using UltricoCalendarContracts.Interfaces.Service;
using UltricoCalendarContracts.Models;
using UltricoCalendarService.Repositories;
using UltricoCalendarService.Service;
using Xunit;

namespace UltricoCalendarTest
{
    public class CalendarServiceTests {
        
        [Fact]
        public void TestIfAddsEvent()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Provide<ISingleEventRepository, CalendarRepository>();
                mock.Provide<ISingleEventService, CalendarService>();

                var calendarService = mock.Create<CalendarService>();
                calendarService.AddEvent(TestEventModel());
            }
        }
        
        [Fact]public void TestIfGetsEvent(){}
        [Fact]public void TestIfDeletesEvent(){}
        [Fact]public void TestIfEditsEvent(){}
        
        [Fact]public void TestIfAddsEventSeries(){}
        [Fact]public void TestIfGetsEventSeries(){}
        [Fact]public void TestIfDeletesEventSeries(){}
        [Fact]public void TestIfEditsEventSeries(){}
        
        [Fact]public void TestIfDeletedEventFromSeries(){}
        [Fact]public void TestIfEditEventFromSeries(){}
        [Fact]public void TestIfGetsEditedEventFromSeries(){}
        [Fact]public void TestIfGetsCalendarMetadata(){}

        private SingleEventModel TestEventModel()
        {
            return new SingleEventModel
            {
                Title = "test title",
                Description = "Description",
                Duration = "00:10:00",
                MailAddresses = new List<string>(),
                Start = DateTime.Now
            };
        }
    }
}
