﻿using Microsoft.EntityFrameworkCore;
using WebApplication2.Enums;
using WebApplication2.Entities;
using WebApplication2.DTOs;

namespace WebApplication2.Repositories;

public class TripRepository : ITripRepository
{
    public async Task<IEnumerable<TripDto>> GetTrips()
    {
        ApbdContext apbdContext = new();

        List<TripDto> trips = await apbdContext.Trips
            .OrderByDescending(t => t.DateFrom)
            .Select(x => new TripDto
            (
                x.Name, 
                x.Description, 
                x.DateFrom, 
                x.DateTo, 
                x.MaxPeople,
                x.IdCountries.Select(c => new CountryDto
                (
                    c.Name
                )).ToList(),
                x.ClientTrips.Select(ct => new ClientDto
                (
                    ct.IdClientNavigation.FirstName,
                    ct.IdClientNavigation.LastName
                )).ToList()
            ))
            .ToListAsync();

        return trips;
    }

    public async Task<Errors> DeleteClient(int idClient)
    {
        ApbdContext apbdContext = new();
        
        var client = apbdContext.Clients.Find(idClient);
        if (client == null)
            return Errors.NotFound;

        if (apbdContext.ClientTrips.Any(ct => ct.IdClient == idClient))
            return Errors.BadRequest;

        apbdContext.Clients.Remove(client);
        apbdContext.SaveChanges();

        return Errors.Good;
    }

    public async Task<Errors> AssignClientToTrip(ClientInputDTO clientInputDto)
    {
        ApbdContext apbdContext = new();
        
        var existingClient = apbdContext.Clients.FirstOrDefault(c => c.Pesel == clientInputDto.Pesel);
        if (existingClient != null)
            return Errors.BadRequest;

        var trip = apbdContext.Trips.Find(clientInputDto.IdTrip);
        if (trip == null)
            return Errors.NotFound;

        if (trip.DateFrom <= DateTime.Now)
            return Errors.BadRequest;

        var client = new Client
        {
            FirstName = clientInputDto.FirstName,
            LastName = clientInputDto.LastName,
            Email = clientInputDto.Email,
            Telephone = clientInputDto.Telephone,
            Pesel = clientInputDto.Pesel
        };

        var clientTrip = new ClientTrip
        {
            IdClient = existingClient.IdClient,
            IdTrip = clientInputDto.IdTrip,
            RegisteredAt = DateTime.Now,
            PaymentDate = clientInputDto.PaymentDate
        };

        apbdContext.ClientTrips.Add(clientTrip);
        apbdContext.SaveChanges();

        return Errors.Good;
    }
}