using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Moq;
using Serilog;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Enums;
using UltricoCalendarContracts.Interfaces.Repository;
using UltricoCalendarContracts.Interfaces.Service;
using UltricoCalendarContracts.Models;
using UltricoCalendarService.Repositories;
using UltricoCalendarService.Service;
using Xunit;
using Xunit.Abstractions;

namespace UltricoCalendarTest
{
    public class CalendarServiceTests
    {
        private readonly DateTime _startDateTime = new DateTime(2008, 6, 1, 7, 47, 0);

        private readonly DateTime _eventFromSeriesDateTime = new DateTime(2008, 6, 1, 7, 47, 0).AddHours(1);

        private const int SeriesCount = 10;

        private readonly ITestOutputHelper _output;

        public CalendarServiceTests(ITestOutputHelper output)
                 {
            _output = output;
        }


        private SingleEventModel TestEventModel()
        {
            return new SingleEventModel
            {
                Title = "test title",
                Description = "Description",
                Duration = "00:10:00",
                MailAddresses = new List<string>(),
                Start = _startDateTime
            };
        }

        private EventSeriesModel TestFinishAfterDateEventSeriesModel()
        {
            return new EventSeriesModel
            {
                Title = "test title",
                Description = "Description",
                Duration = "00:10:00",
                MailAddresses = new List<string>(),
                Start = _startDateTime,
                RepeatPeriod = RepeatPeriod.Day,
                FinishEnum = FinishEnum.AfterDate,
                FinishDate = _startDateTime.AddDays(SeriesCount)
            };
        }
        
        private EventSeriesModel TestNeverEndingEventSeriesModel()
        {
            return new EventSeriesModel
            {
                Title = "test title",
                Description = "Description",
                Duration = "00:10:00",
                MailAddresses = new List<string>(),
                Start = _startDateTime,
                RepeatPeriod = RepeatPeriod.Day,
                FinishEnum = FinishEnum.NeverFinish,
            };
        }
        
        private EventSeriesModel TestFinishAfterOccursEventSeriesModel()
        {
            return new EventSeriesModel
            {
                Title = "test title",
                Description = "Description",
                Duration = "00:10:00",
                MailAddresses = new List<string>(),
                Start = _startDateTime,
                RepeatPeriod = RepeatPeriod.Day,
                FinishEnum = FinishEnum.AfterOccurs,
                OccursAmount = SeriesCount
            };
        }
        
        private EventSeriesModel TestNeverEndingFullDayEventSeriesModel()
        {
            return new EventSeriesModel
            {
                Title = "test title",
                Description = "Description",
                Duration = "00:00:00",
                MailAddresses = new List<string>(),
                Start = _startDateTime,
                RepeatPeriod = RepeatPeriod.Day,
                FinishEnum = FinishEnum.NeverFinish,
            };
        }

        private EventFromSeriesModel TestEventFromSeriesModel()
        {
            return new EventFromSeriesModel(_startDateTime)
            {
                Title = "test title",
                Description = "Description",
                Duration = "00:10:00",
                MailAddresses = new List<string>(),
                Start = _eventFromSeriesDateTime,
                OldStartDate = _startDateTime
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
        
        private static CalendarService CreateServiceForMetadata(AutoMock mock)
        {
            mock.Provide<IEventSeriesRepository, CalendarRepository>();
            mock.Provide<IEventSeriesService, CalendarService>();
            mock.Provide<IEventFromSeriesRepository, CalendarRepository>();
            mock.Provide<IEventFromSeriesService, CalendarService>();
            mock.Provide<ISingleEventRepository, CalendarRepository>();
            mock.Provide<ISingleEventService, CalendarService>();
            return mock.Create<CalendarService>();
        }


        private int AddEditedEventFromSeries(IEventSeriesService service)
        {
            var seriesId = service.AddEventSeries(TestNeverEndingEventSeriesModel());
            return service.ExcludeEventFromSeries(seriesId, TestEventFromSeriesModel());
        }

        private void ValidateEventSeries(CalendarService calendarService, BaseEventModel eventModel, int minusDays, int plusDays, int expectedCount)
        {
            var id = calendarService.AddEventSeries(eventModel);
            Assert.True(id > 0);
            var metadata = calendarService.GetMetadata(_startDateTime.AddDays(-minusDays), _startDateTime.AddDays(plusDays)).ToList();
            var eventMetadata = metadata.Single(x => x.Id == id);
            Assert.True(eventMetadata.Start.Count() == expectedCount);
            Assert.True(eventMetadata.Start.First() == _startDateTime);
            Assert.True(eventMetadata.Title.Equals(eventModel.Title));
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
                var seriesId = calendarService.AddEventSeries(TestNeverEndingEventSeriesModel());
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
                Assert.True(calendarService.AddEventSeries(TestNeverEndingEventSeriesModel()) > 0);
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
                var id = calendarService.AddEventSeries(TestNeverEndingEventSeriesModel());
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
                var eventModel = TestNeverEndingEventSeriesModel();
                var id = calendarService.AddEventSeries(eventModel);
                eventModel.Title = "editedTitle";
                calendarService.EditEventSeries(id, eventModel);
                var eventSeries = (EventSeriesModel) calendarService.GetEventSeries(id);
                Assert.Equal(eventSeries.Title, eventModel.Title);
            }
        }

        [Fact]
        public void TestIfGetsCalendarMetadataForSingleEvent()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForMetadata(mock);
                var eventModel = TestEventModel();
                var id = calendarService.AddEvent(eventModel);
                Assert.True(id > 0);
                var metadata = calendarService.GetMetadata(_startDateTime.AddDays(-1), _startDateTime.AddDays(1)).ToList();
                var eventMetadata = metadata.First(x => x.Id == id && x.Start.Count() == 1);
                Assert.True(eventMetadata.Start.First() == _startDateTime);
                Assert.True(eventMetadata.Title.Equals(eventModel.Title));
            }
        }
        
        [Fact]
        public void TestIfGetsCalendarMetadataForNeverEndingEventSeries()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForMetadata(mock);
                var eventModel = TestNeverEndingEventSeriesModel();
                ValidateEventSeries(calendarService, eventModel, 1, SeriesCount, SeriesCount);
            }
        }
        
        [Fact]
        public void TestIfGetsCalendarMetadataForNeverEndingFullDayEventSeriesModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForMetadata(mock);
                var eventModel = TestNeverEndingFullDayEventSeriesModel();
                ValidateEventSeries(calendarService, eventModel, 1, SeriesCount, SeriesCount);
            }
        }

        [Fact]
        public void TestIfGetsCalendarMetadataForFinishAfterDateEventSeriesModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForMetadata(mock);
                var eventModel = TestFinishAfterDateEventSeriesModel();
                ValidateEventSeries(calendarService, eventModel, 1, SeriesCount * 2, SeriesCount);
            }
        }
        
        [Fact]
        public void TestIfGetsCalendarMetadataForFinishAfterOccursEventSeriesModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForMetadata(mock);
                var eventModel = TestFinishAfterOccursEventSeriesModel();
                ValidateEventSeries(calendarService, eventModel, 1, SeriesCount * 2, SeriesCount);
            }
        }

        [Fact]
        public void TestIfGetsCalendarMetadataForEventFromSeries()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForMetadata(mock);
                var eventModel = TestNeverEndingEventSeriesModel();
                var seriesId = calendarService.AddEventSeries(eventModel);
                Assert.True(seriesId > 0);
                var eventFromSeriesId = calendarService.ExcludeEventFromSeries(seriesId, TestEventFromSeriesModel());
                Assert.True(eventFromSeriesId > 0);
                var metadata = calendarService.GetMetadata(_startDateTime.AddDays(-1), _startDateTime.AddDays(SeriesCount)).ToList();
                var eventSeriesMetadata = metadata.Single(x => x.Id == seriesId && x.Start.Count() > 1);
                var eventFromSeriesMetadata = metadata.Single(x => x.Id == eventFromSeriesId && x.Start.Count() == 1);
                Assert.True(eventSeriesMetadata.Start.Count() == SeriesCount - 1);
                Assert.False(eventSeriesMetadata.Start.Contains(eventFromSeriesMetadata.Start.First()));
            }
        }
        
        [Fact]
        public void TestIfGetsCalendarMetadataForDeletedOccurence()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var calendarService = CreateServiceForMetadata(mock);
                var eventModel = TestNeverEndingEventSeriesModel();
                var seriesId = calendarService.AddEventSeries(eventModel);
                Assert.True(seriesId > 0);
                calendarService.DeleteEventOccurenceFromSeries(seriesId, _startDateTime);
                var metadata = calendarService.GetMetadata(_startDateTime.AddDays(-1), _startDateTime.AddDays(SeriesCount)).ToList();
                var eventSeriesMetadata = metadata.Single(x => x.Id == seriesId && x.Start.Count() > 1);
                Assert.True(eventSeriesMetadata.Start.Count() == SeriesCount - 1);
                Assert.False(eventSeriesMetadata.Start.Contains(_startDateTime));
            }
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
                var id = calendarService.AddEventSeries(TestNeverEndingEventSeriesModel());
                calendarService.GetEventSeries(id);
            }
        }
        
        [Fact]public void TestIfDontDeleteEventIfNotExist(){}
        [Fact]public void TestIfDontEditEventIfNotExist(){}
        [Fact]public void TestIfDontGetEventIfNotExist(){}
        [Fact]public void TestIfDontDeleteEventSeriesIfNotExist(){}
        [Fact]public void TestIfDontEditEventSeriesIfNotExist(){}
        [Fact]public void TestIfDontGetEventSeriesIfNotExist(){}
        [Fact]public void TestIfDontDeleteEventFromSeriesIfNotExist(){}
        [Fact]public void TestIfDontEditEventFromSeriesIfNotExist(){}
        [Fact]public void TestIfDontGetEventFromSeriesIfNotExist(){}
        [Fact]public void TestIfDontDeleteOccurenceIfTimeDoesntMatch(){}
        [Fact]public void TestIfDontAddEventFromSeriesIfTimeDoesntMatch(){}
    }
}