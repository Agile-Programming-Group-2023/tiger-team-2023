// <copyright file="VolunteerEventController.cs" company="Adairsville High School">
//  Copyright © 2023 Adairsville High School. All rights reserved.
//  This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
// </copyright>

namespace VolunteerEvent.Controllers;

using Microsoft.AspNetCore.Mvc;
using VolunteerEvent.Services;
using VolunteerScheduler.Models;

/// <summary>
/// The volunteer event controller.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class VolunteerEventController : ControllerBase
{
    private readonly ILogger<VolunteerEventController> logger;
    private readonly IVolunteerEventStorageService volunteerEventStorageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="VolunteerEventController"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="volunteerEventStorageService">The volunteer event storage service.</param>
    public VolunteerEventController(ILogger<VolunteerEventController> logger, IVolunteerEventStorageService volunteerEventStorageService)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.volunteerEventStorageService = volunteerEventStorageService ?? throw new ArgumentNullException(nameof(volunteerEventStorageService));
    }

    /// <summary>
    /// Gets the VolunteerEvents. This will get all the volunteer events that are stored.
    /// </summary>
    /// <returns>An IActionResult.</returns>
    [HttpGet(Name = "GetVolunteerEvents")]
    public IActionResult Get()
    {
        logger.LogInformation("Getting the volunteer events.");
        return Ok(this.volunteerEventStorageService.Get());
    }

    /// <summary>
    /// Posts the VolunteerEvent; which will create a new record and store it.
    /// </summary>
    /// <param name="event">The event.</param>
    /// <returns>An IActionResult.</returns>
    [HttpPost(Name = "AddVolunteerEvent")]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Post([FromBody] VolunteerEvent @event)
    {
        if (@event == null)
        {
            logger.LogInformation("Tried to send me an empty event.");
            return BadRequest("event can't be null");
        }

        // Since we want to add, lets force this to zero for now. Bit of a hack and we should
        //  be checking to see if we already have this event.
        @event.Id = 0;
        this.volunteerEventStorageService.Add(@event);

        return Created(string.Empty, @event);
    }
}
