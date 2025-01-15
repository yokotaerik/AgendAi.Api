using MarcAI.Application.Dtos.Schedules;
using MarcAI.Domain.Models.Aggregates.Schedules;

namespace MarcAI.Application.Interfaces;

public interface IScheduleService
{
    Task CreateDefaultSchedule(RegisterDefaultSchedulesDto data);
    Task AddSchedule(RegisterSchedulesDto data);
    Task<bool> EditSchedule(UpdateScheduleDto data);
}
