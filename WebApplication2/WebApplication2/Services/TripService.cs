using WebApplication2.Enums;
using WebApplication2.DTOs;
using WebApplication2.Repositories;

namespace WebApplication2.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;

    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }
    public async Task<IEnumerable<TripDto>> GetTrips()
    {
        return await _tripRepository.GetTrips();
    }

    public async Task<Errors> DeleteClient(int idClient)
    {
        return await _tripRepository.DeleteClient(idClient);
    }

    public async Task<Errors> AssignClientToTrip(ClientInputDTO clientInputDto)
    {
        return await _tripRepository.AssignClientToTrip(clientInputDto);
    }
}