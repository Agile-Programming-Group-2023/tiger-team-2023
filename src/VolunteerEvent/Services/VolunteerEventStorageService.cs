// <copyright file="VolunteerEventStorageService.cs" company="Adairsville High School">
//  Copyright © 2023 Adairsville High School. All rights reserved.
//  This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
// </copyright>

namespace VolunteerEvent.Services;

using System.Collections.Concurrent;
using VolunteerScheduler.Models;

/// <summary>
/// The volunteer event storage service.
/// </summary>
public class VolunteerEventStorageService : IVolunteerEventStorageService
{

     private static int index = 0;

    private readonly ConcurrentDictionary<int, VolunteerEvent> volunteerEvents = new ConcurrentDictionary<int, VolunteerEvent>();

    /// <summary>
    /// Adds the.
    /// </summary>
    /// <param name="VolunteerEvent">The volunteer event.</param>
    /// <returns>A VolunteerEvent? .</returns>
    public VolunteerEvent? Add(VolunteerEvent VolunteerEvent)
    {
        Interlocked.Increment(ref index);
        VolunteerEvent.Id = index;
        if (volunteerEvents.TryAdd(VolunteerEvent.Id, VolunteerEvent))
        {
            return VolunteerEvent;
        }

        return null;
    }

    /// <summary>
    /// Deletes the.
    /// </summary>
    /// <param name="VolunteerEvent">The volunteer event.</param>
    /// <returns>A bool.</returns>
    public bool Delete(VolunteerEvent VolunteerEvent) => volunteerEvents.TryRemove(new KeyValuePair<int, VolunteerEvent>(VolunteerEvent.Id, VolunteerEvent));

    /// <summary>
    /// Deletes the all.
    /// </summary>
    public void DeleteAll() => volunteerEvents.Clear();

    /// <summary>
    /// Finds the.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <returns>A VolunteerEvent? .</returns>
    public VolunteerEvent? Find(string title)
    {
        var VolunteerEvent = volunteerEvents.FirstOrDefault(VolunteerEvent => VolunteerEvent.Value.Title == title).Value;
        return VolunteerEvent;
    }

    /// <summary>
    /// Gets the.
    /// </summary>
    /// <returns>A list of volunteerEvents.</returns>
    public IEnumerable<VolunteerEvent> Get() => volunteerEvents.Values.ToList();

    /// <summary>
    /// Gets the.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>A VolunteerEvent? .</returns>
    public VolunteerEvent? Get(int id)
    {
        volunteerEvents.TryGetValue(id, out VolunteerEvent? origVolunteerEvent);
        return origVolunteerEvent;
    }

    /// <summary>
    /// Updates the.
    /// </summary>
    /// <param name="VolunteerEvent">The volunteer event.</param>
    /// <returns>A VolunteerEvent? .</returns>
    public VolunteerEvent? Update(VolunteerEvent VolunteerEvent)
    {
        var origVolunteerEvent = this.Get(VolunteerEvent.Id);
        if (origVolunteerEvent == null)
        {
            return this.Add(VolunteerEvent);
        }

        volunteerEvents.TryUpdate(VolunteerEvent.Id, VolunteerEvent, origVolunteerEvent);
        return VolunteerEvent;
    }
}