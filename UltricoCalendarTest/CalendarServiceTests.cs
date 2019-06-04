using System;
using System.Collections.Generic;
using Autofac.Extras.Moq;
using UltricoCalendarContracts.Enums;
using UltricoCalendarContracts.Interfaces.Repository;
using UltricoCalendarContracts.Interfaces.Service;
using UltricoCalendarContracts.Models;
using UltricoCalendarService.Repositories;
using UltricoCalendarService.Service;
using Xunit;

namespace UltricoCalendarTest
{
    public class CalendarServiceTests
    {
        private readonly DateTime _eventFromSeriesDateTime = new DateTime(2008, 6, 1, 7, 47, 0);


        private static SingleEventModel TestEventModel()
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

        private EventSeriesModel TestEventSeriesModel()
        {
            return new EventSeriesModel
            {
                Title = "test title",
                Description = "Description",
                Duration = "00:10:00",
                MailAddresses = new List<string>(),
                Start = _eventFromSeriesDateTime,
                RepeatPeriod = RepeatPeriod.Day,
                FinishEnum = FinishEnum.AfterDate,
                FinishDate = _eventFromSeriesDateTime.AddDays(10)
            };
        }

        private EventFromSeriesModel TestEventFromSeriesModel()
        {
            return new EventFromSeriesModel(_eventFromSeriesDateTime)
            {
                Title = "test title",
                Description = "Description",
                Duration = "00:10:00",
                MailAddresses = new List<string>(),
                Start = DateTime.Now
            };
        }

        private static CalendarService CreateServiceForSingleEvents(AutoMock mock)
        {
            mock.Provide<ISingleEventRepository, CalendarRepository>();
            mock.Provide<ISingleEventService, CalendarService>();
            return mock.Create<CalendarService>();
        }

        private static CalendarService CreateServiceForEventSeries(AutoMock mock)
        {
            mock.Provide<IEventSeriesRepository, CalendarRepository>();
            mock.Provide<IEventSeriesService, CalendarService>();
            return mock.Create<CalendarService>();
        }

        private static CalendarService CreateServiceForEventFromSeries(AutoMock mock)
        {
            mock.Provide<IEventSeriesRepository, CalendarRepository>();
            mock.Provide<IEventSeriesService, CalendarService>();
            mock.Provide<IEventFromSeriesRepository, CalendarRepository>();
            mock.Provide<IEventFromSeriesService, CalendarService>();
            return mock.Create<CalendarService>();
        }

        private int AddEditedEventFromSeries(IEventSeriesService service)
        {
            var seriesId = service.AddEventSeries(TestEventSeriesModel());
            return service.ExcludeEventFromSeries(seriesId, TestEventFromSeriesModel());
        }

        [Fact]
        public void TestIfAddsEvent()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForSingleEvents(mock);
                Assert.True(calendarService.AddEvent(TestEventModel()) > 0);
            }
        }

        [Fact]
        public void TestIfAddsEventFromSeries()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForEventSeries(mock);
                var seriesId = calendarService.AddEventSeries(TestEventSeriesModel());
                var id = calendarService.ExcludeEventFromSeries(seriesId, TestEventFromSeriesModel());
                Assert.True(id > 0);
            }
        }

        [Fact]
        public void TestIfAddsEventSeries()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForEventSeries(mock);
                Assert.True(calendarService.AddEventSeries(TestEventSeriesModel()) > 0);
            }
        }

        [Fact]
        public void TestIfDeletedEventFromSeries()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForEventFromSeries(mock);
                var id = AddEditedEventFromSeries(calendarService);
                calendarService.DeleteEventFromSeries(id);
            }
        }

        [Fact]
        public void TestIfDeletesEvent()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForSingleEvents(mock);
                var id = calendarService.AddEvent(TestEventModel());
                Assert.True(calendarService.DeleteEvent(id));
            }
        }


        [Fact]
        public void TestIfDeletesEventSeries()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForEventSeries(mock);
                var id = calendarService.AddEventSeries(TestEventSeriesModel());
                Assert.True(calendarService.DeleteEventSeries(id));
            }
        }


        [Fact]
        public void TestIfEditEventFromSeries()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForEventFromSeries(mock);
                var id = AddEditedEventFromSeries(calendarService);
                var eventModel = (EventFromSeriesModel) calendarService.GetEventFromSeries(id);
                eventModel.Title = "editedTitle";
                calendarService.EditEventFromSeries(id, eventModel);
                var editedEvent = (EventFromSeriesModel) calendarService.GetEventFromSeries(id);
                Assert.Equal(editedEvent.Title, eventModel.Title);
            }
        }

        [Fact]
        public void TestIfEditsEvent()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForSingleEvents(mock);
                var eventModel = TestEventModel();
                var id = calendarService.AddEvent(eventModel);
                eventModel.Title = "editedTitle";
                calendarService.EditEvent(id, eventModel);
                var singleEvent = (SingleEventModel) calendarService.GetEvent(id);
                Assert.Equal(singleEvent.Title, eventModel.Title);
            }
        }

        [Fact]
        public void TestIfEditsEventSeries()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForEventSeries(mock);
                var eventModel = TestEventSeriesModel();
                var id = calendarService.AddEventSeries(eventModel);
                eventModel.Title = "editedTitle";
                calendarService.EditEventSeries(id, eventModel);
                var eventSeries = (EventSeriesModel) calendarService.GetEventSeries(id);
                Assert.Equal(eventSeries.Title, eventModel.Title);
            }
        }

        [Fact]
        public void TestIfGetsCalendarMetadata()
        {
        }

        [Fact]
        public void TestIfGetsEditedEventFromSeries()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForEventFromSeries(mock);
                var id = AddEditedEventFromSeries(calendarService);
                calendarService.GetEventFromSeries(id);
            }
        }

        [Fact]
        public void TestIfGetsEvent()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForSingleEvents(mock);
                var id = calendarService.AddEvent(TestEventModel());
                calendarService.GetEvent(id);
            }
        }

        [Fact]
        public void TestIfGetsEventSeries()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForEventSeries(mock);
                var id = calendarService.AddEventSeries(TestEventSeriesModel());
                calendarService.GetEventSeries(id);
            }
        }
    }
}