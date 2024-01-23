﻿using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Data;
using ServiceManagerApi.Dtos.HoursEntries;

namespace ServiceManagerApi.Controllers.Esms;

[Route("api/[controller]")]
[ApiController]
public class HoursEntryController : BaeApiController<HoursEntryController>
{
  private readonly EnpDBContext _context;

  public HoursEntryController(EnpDBContext context)
  {
    _context = context;
  }


  [HttpGet("tenant/{tenantId}")]
  [ProducesResponseType(typeof(HoursEntry), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public Task<List<HoursEntry>> GetHoursEntries(string tenantId)
  {
    var hoursEntries = _context.HoursEntries
        // .Where(hoursEntry => hoursEntry.TenantId == tenantId && hoursEntry.EntrySource == "Normal Reading")
        // .OrderByDescending(entry => entry.Date) // Order by the Date property in descending order
        // .Take(1) // Take the first (latest) entry
        // .ToListAsync();
        .Where(leav => leav.TenantId == tenantId && leav.EntrySource == "Normal Reading")
        .GroupBy(entry => entry.FleetId)
        .Select(group => group.OrderByDescending(entry => entry.Date)
            .ThenByDescending(i => i.Id).First())
        .ToListAsync();
    return hoursEntries;
  }
    [HttpGet("HoursEntryByTenantId/{tenantId}")]
    //[ProducesResponseType(typeof(HoursEntry), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<List<HoursEntry>> GetAllHours(string tenantId)
    {
        DateTime sixMonthsAgo = DateTime.UtcNow.AddMonths(-6);

        var hoursEntries = _context.HoursEntries
            .Where(leav =>
                leav.TenantId == tenantId &&
                leav.EntrySource == "Normal Reading" &&
                leav.Date >= sixMonthsAgo 
            )
            .ToListAsync();

        return hoursEntries;
    }

    //pulling all hoursEntries

    [HttpGet("IndividualHoursEntry/{tenantId}/{fleetId}")]
    [ProducesResponseType(typeof(List<HoursEntry>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<HoursEntry>>> GetAllHoursEntries(string tenantId, string fleetId)
    {
        var hoursEntries = await _context.HoursEntries
            .Where(leav => leav.TenantId == tenantId && leav.EntrySource == "Normal Reading" && leav.FleetId == fleetId)
            .ToListAsync();

        if (hoursEntries == null || hoursEntries.Count == 0)
        {
            return NotFound(); // Return a 404 response if no entries are found
        }

        return Ok(hoursEntries); // Return a 200 OK response with the list of entries
    }


    // get by equipmentId
    [HttpGet("{equipmentId}/tenant/{tenantId}/{readingSource}")]
  [ProducesResponseType(typeof(HoursEntry), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetById(string equipmentId, string tenantId, string readingSource)
  {
    var hoursEntries = await _context.HoursEntries
        .Where(entry => entry.FleetId == equipmentId.ToString() && entry.TenantId == tenantId &&
                        entry.EntrySource == readingSource)
        .OrderByDescending(entry => entry.Date)
        .Take(1)
        .ToListAsync(); // Order by the Date property in descending order

    if (hoursEntries == null) return NotFound();
    return Ok(hoursEntries);
  }

  //get all pm hours entries
  [HttpGet("tenant/{tenantId}/allpmreadings")]
  [ProducesResponseType(typeof(HoursEntry), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public Task<List<HoursEntry>> GetHoursAllPmEntries(string tenantId)
  {
    var hoursEntries = _context.HoursEntries
        // .Where(hoursEntry => hoursEntry.TenantId == tenantId && hoursEntry.EntrySource == "PM Reading")
        // .OrderByDescending(entry => entry.Date) // Order by the Date property in descending order
        // .Take(1) // Take the first (latest) entry
        // .ToListAsync();
        .Where(leav => leav.TenantId == tenantId && leav.EntrySource == "PM Reading")
        .GroupBy(entry => entry.FleetId)
        .Select(group => group.OrderByDescending(entry => entry.Date)
            .ThenByDescending(i => i.Id).First())
        .ToListAsync();
    return hoursEntries;
  }


  // post hourEntry
  [HttpPost]
  [ProducesResponseType(typeof(HoursEntry), StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> Create(HoursEntriesPostDto hoursEntriesPostDto)
  {
    var hourEntry = _mapper.Map<HoursEntry>(hoursEntriesPostDto);

    _context.HoursEntries.Add(hourEntry);
    try
    {
      await _context.SaveChangesAsync();
      return Ok(hourEntry);
    }
    catch (DbUpdateException)
    {
      if (HourEntryExists(hourEntry.Id))
        return Conflict();
      else
        throw;
    }
  }

  // updates hourEntry
  [HttpPut("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Put(int id, HoursEntriesPutDto hoursEntriesPutDto)
  {
    var hourEntry = _mapper.Map<HoursEntry>(hoursEntriesPutDto);

    if (id != hourEntry.Id) return BadRequest();

    _context.Entry(hourEntry).State = EntityState.Modified;

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!HourEntryExists(id))
        return NotFound();
      else
        throw;
    }

    return NoContent();
  }


  // patch hourEntry
  [HttpPatch("{id:int}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<HoursEntry> patchHourEntry)
  {
    var hourEntry = await _context.HoursEntries.FindAsync(id);

    if (hourEntry == null) return BadRequest();

    patchHourEntry.ApplyTo(hourEntry, ModelState);

    await _context.SaveChangesAsync();

    return Ok(hourEntry);
  }


  // delete hourEntry
  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Delete(int id)
  {
    var groupToDelete = await _context.HoursEntries.FindAsync(id);
    if (groupToDelete == null) return NotFound();
    _context.HoursEntries.Remove(groupToDelete);
    await _context.SaveChangesAsync();
    return NoContent();
  }

  private bool HourEntryExists(int id)
  {
    return _context.HoursEntries.Any(e => e.Id == id);
  }
}