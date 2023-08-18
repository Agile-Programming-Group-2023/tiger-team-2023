// <copyright file="VolunteerEventControllerTest.cs" company="Adairsville High School">
//  Copyright © 2023 Adairsville High School. All rights reserved.
//  This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
//  </copyright>

namespace VolunteerEventTests;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using VolunteerEvent.Controllers;
using VolunteerEvent.Services;
using VolunteerScheduler.Models;

public class VolunteerEventControllerTest
{
    [Fact]
    public void CreateInstance()
    {
        ILogger<VolunteerEventController> logger = new NullLogger<VolunteerEventController>();
        IVolunteerEventStorageService volunteerEventStorageService = new Mock<IVolunteerEventStorageService>().Object;

        VolunteerEventController sut = new VolunteerEventController(logger, volunteerEventStorageService);
        Assert.NotNull(sut);
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public void CreateInstanceWithBadParameters(bool createLogger, bool createStorage)
    {
        ILogger<VolunteerEventController> logger = createLogger ? new NullLogger<VolunteerEventController>() : null;
        IVolunteerEventStorageService volunteerEventStorageService = createStorage ? new Mock<IVolunteerEventStorageService>().Object : null;

        Assert.Throws<ArgumentNullException>(() => new VolunteerEventController(logger, volunteerEventStorageService));
    }

    [Fact]
    public void Get()
     {
        ILogger<VolunteerEventController> logger = new NullLogger<VolunteerEventController>();
        Mock<IVolunteerEventStorageService> volunteerEventStorageService = new Mock<IVolunteerEventStorageService>();

        List<VolunteerEvent> data = new List<VolunteerEvent>();

        VolunteerEvent expectedEvent = new VolunteerEvent
        {
            Id = 1,
            Title = "Unit test",
            Description = "Unit test",
            Event = Ical.Net.Calendar.Load("BEGIN:VCALENDAR\r\nPRODID:-//github.com/rianjs/ical.net//NONSGML ical.net 4.0//EN\r\nVERSION:2.0\r\nBEGIN:VEVENT\r\nDTSTAMP:20230814T150012Z\r\nDTSTART:20230814T150012\r\nRRULE:FREQ=WEEKLY;UNTIL=99991231T235959;BYDAY=SU\r\nSEQUENCE:0\r\nUID:bbc861aa-f3f1-449c-8657-9839b4dd59c4\r\nEND:VEVENT\r\nEND:VCALENDAR\r\n")
        };

        data.Add(expectedEvent);

        volunteerEventStorageService.Setup(e => e.Get()).Returns(data);

        VolunteerEventController sut = new VolunteerEventController(logger, volunteerEventStorageService.Object);
        Assert.NotNull(sut);

        sut.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext { HttpContext = new DefaultHttpContext() };

        var result = sut.Get();
        Assert.NotNull(result);

        var okResult = result as OkObjectResult;

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);

        var datResult = okResult?.Value as IEnumerable<VolunteerEvent>;

        Assert.NotNull(datResult);
        Assert.Single(datResult);

        VolunteerEvent actualEvent = datResult.FirstOrDefault();
        Assert.NotNull(actualEvent);

        Assert.Equal(expectedEvent.Id, actualEvent.Id);
        Assert.Equal(expectedEvent.Title, actualEvent.Title);
        Assert.Equal(expectedEvent.Event, actualEvent.Event);
    }

    [Fact]
    public void Post()
    {
        ILogger<VolunteerEventController> logger = new NullLogger<VolunteerEventController>();
        Mock<IVolunteerEventStorageService> volunteerEventStorageService = new Mock<IVolunteerEventStorageService>();

        VolunteerEvent expectedEvent = new VolunteerEvent
        {
            Id = 1,
            Title = "Unit test",
            Description = "Unit test",
            Event = Ical.Net.Calendar.Load("BEGIN:VCALENDAR\r\nPRODID:-//github.com/rianjs/ical.net//NONSGML ical.net 4.0//EN\r\nVERSION:2.0\r\nBEGIN:VEVENT\r\nDTSTAMP:20230814T150012Z\r\nDTSTART:20230814T150012\r\nRRULE:FREQ=WEEKLY;UNTIL=99991231T235959;BYDAY=SU\r\nSEQUENCE:0\r\nUID:bbc861aa-f3f1-449c-8657-9839b4dd59c4\r\nEND:VEVENT\r\nEND:VCALENDAR\r\n")
        };

        volunteerEventStorageService.Setup(e => e.Add(It.Is<VolunteerEvent>( @event => @event.Id == 1 ))).Returns(expectedEvent);

        VolunteerEventController sut = new VolunteerEventController(logger, volunteerEventStorageService.Object);
        Assert.NotNull(sut);

        sut.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext { HttpContext = new DefaultHttpContext() };

        var result = sut.Post(expectedEvent);
        Assert.NotNull(result);

        var createdResult = result as CreatedResult;

        Assert.NotNull(createdResult);
        Assert.Equal(201, createdResult.StatusCode);

        var actualEvent = createdResult?.Value as VolunteerEvent;

        Assert.NotNull(actualEvent);
        Assert.Equal(expectedEvent.Id, actualEvent.Id);
        Assert.Equal(expectedEvent.Title, actualEvent.Title);
        Assert.Equal(expectedEvent.Event, actualEvent.Event);
    }

    [Fact]
    public void PostWithNullEvent()
    {
        ILogger<VolunteerEventController> logger = new NullLogger<VolunteerEventController>();
        Mock<IVolunteerEventStorageService> volunteerEventStorageService = new Mock<IVolunteerEventStorageService>();

        VolunteerEventController sut = new VolunteerEventController(logger, volunteerEventStorageService.Object);
        Assert.NotNull(sut);

        sut.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext { HttpContext = new DefaultHttpContext() };

        var result = sut.Post(null);
        Assert.NotNull(result);

        var badRequestResult = result as BadRequestObjectResult;

        Assert.NotNull(badRequestResult);
        Assert.Equal(400, badRequestResult.StatusCode);
    }
}
