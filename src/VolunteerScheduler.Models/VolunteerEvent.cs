// <copyright file="VolunteerEvent.cs" company="Adairsville High School">
//  Copyright © 2023 Adairsville High School. All rights reserved.
//  This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
//  </copyright>

namespace VolunteerScheduler.Models;

using Ical.Net;

/// <summary>
/// The volunteer event.
/// </summary>
public class VolunteerEvent
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the event.
    /// </summary>
    public Calendar? Event { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; set; }
}
