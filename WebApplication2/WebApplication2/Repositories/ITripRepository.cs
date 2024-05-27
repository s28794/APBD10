using WebApplication2.DTOs;
using WebApplication2.Enums;

namespace WebApplication2.Repositories;

public interface ITripRepository
{
    Task<IEnumerable<TripDto>> GetTrips();
    Task<Errors> DeleteClient(int idClient);
    Task<Errors> AssignClientToTrip(ClientInputDTO clientInputDto);
}