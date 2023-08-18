namespace VolunteerEvent.Services;

using System.Collections.Generic;
using VolunteerScheduler.Models;

/// <summary>
/// The volunteer event storage service.
/// </summary>
public interface IVolunteerEventStorageService
{
    /// <summary>
    /// Adds the.
    /// </summary>
    /// <param name="VolunteerEvent">The volunteer event.</param>
    /// <returns>A nullable VolunteerEvent.</returns>
    VolunteerEvent? Add(VolunteerEvent VolunteerEvent);

    /// <summary>
    /// Deletes the.
    /// </summary>
    /// <param name="VolunteerEvent">The volunteer event.</param>
    /// <returns>A bool.</returns>
    bool Delete(VolunteerEvent VolunteerEvent);

    /// <summary>
    /// Deletes the all.
    /// </summary>
    void DeleteAll();

    /// <summary>
    /// Finds the.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <returns>A nullable VolunteerEvent.</returns>
    VolunteerEvent? Find(string title);

    /// <summary>
    /// Gets the.
    /// </summary>
    /// <returns>A list of volunteerEvents.</returns>
    IEnumerable<VolunteerEvent> Get();

    /// <summary>
    /// Gets the.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>A nullable VolunteerEvent.</returns>
    VolunteerEvent? Get(int id);

    /// <summary>
    /// Updates the.
    /// </summary>
    /// <param name="VolunteerEvent">The volunteer event.</param>
    /// <returns>A nullable VolunteerEvent.</returns>
    VolunteerEvent? Update(VolunteerEvent VolunteerEvent);
}
