using MarcAI.Application.Dtos.Schedules;
using MarcAI.Domain.Models.Aggregates.Schedules;

namespace MarcAI.Application.Interfaces;

public interface IScheduleService
{
    Task CreateDefaultScheduleAsync(RegisterDefaultSchedulesDto data);
    Task AddScheduleAsync(RegisterScheduleDto data);
    Task<bool> EditScheduleAsync(UpdateScheduleDto data);
}
