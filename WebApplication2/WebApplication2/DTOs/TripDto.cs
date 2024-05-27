namespace WebApplication2.DTOs;

public record TripDto(string Name, string Description, DateTime DateFrom, DateTime DateTo, int MaxPeople, List<CountryDto> Countires, List<ClientDto> Clients);