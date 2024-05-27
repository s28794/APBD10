using WebApplication2.DTOs;
using WebApplication2.Enums;

namespace WebApplication2.Services;

public interface ITripService
{
    Task<IEnumerable<TripDto>> GetTrips();
    Task<Errors> DeleteClient(int idClient);
    Task<Errors> AssignClientToTrip(ClientInputDTO clientInputDto);
}