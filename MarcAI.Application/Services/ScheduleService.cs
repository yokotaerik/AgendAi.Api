using MarcAI.Application.Dtos.Schedules;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Aggregates.Schedules;

namespace MarcAI.Application.Services;

internal class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public ScheduleService(IScheduleRepository scheduleRepository, IEmployeeRepository employeeRepository)
    {
        _scheduleRepository = scheduleRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task AddSchedule(RegisterSchedulesDto data)
    {
        var employee = await _employeeRepository.GetByIdAsync(data.EmployeeId)
           ?? throw new ArgumentException("Employee not found");

        var newSchedules = new List<Schedule>();

        foreach (var scheduleDto in data.Schedules)
        {
            var periods = scheduleDto.AvaiblePeriods.Select(dto => AvailablePeriod.Create(dto.Start, dto.End)).ToList();
            var schedule = Schedule.Create(scheduleDto.Date, periods, employee.Id);
            newSchedules.Add(schedule);
        }

        employee.ReplaceSchedules(newSchedules);

        await _employeeRepository.UpdateAsync(employee);
    }

    public async Task CreateDefaultSchedule(RegisterDefaultSchedulesDto data)
    {
        var employee = await _employeeRepository.GetByIdAsync(data.EmployeeId) 
            ?? throw new ArgumentException("Employee not found");

        foreach (var (dayOfWeek, periodsDto) in data.DefaultPeriods)
        {
            var periods = periodsDto.Select(dto => AvailablePeriod.Create(dto.Start, dto.End)).ToList();
            employee.AddDefaultSchedule(dayOfWeek, periods);
        }

        await _employeeRepository.UpdateAsync(employee);
    }

    public Task<bool> EditSchedule(UpdateScheduleDto data)
    {
        throw new NotImplementedException();
    }
}
