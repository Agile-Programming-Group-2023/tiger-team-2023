// <copyright file="CalendarConverter.cs" company="Adairsville High School">
//  Copyright © 2023 Adairsville High School. All rights reserved.
//  This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
// </copyright>

namespace VolunteerScheduler.Models.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Ical.Net;
using Ical.Net.Serialization;

/// <summary>
/// The calendar converter.
/// </summary>
public class CalendarConverter : JsonConverter<Calendar>
{
    /// <summary>
    /// Reads the Calendar data type.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">The options.</param>
    /// <returns>A Calendar? .</returns>
    public override Calendar? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Calendar.Load<Calendar>(reader.GetString()).FirstOrDefault();
    }

    /// <summary>
    /// Writes the Calendar data type.
    /// </summary>
    /// <param name="writer">The writer.</param>
    /// <param name="value">The value.</param>
    /// <param name="options">The options.</param>
    public override void Write(Utf8JsonWriter writer, Calendar value, JsonSerializerOptions options)
    {
        CalendarSerializer ser = new CalendarSerializer();
        writer.WriteStringValue(ser.SerializeToString(value));
    }
}
