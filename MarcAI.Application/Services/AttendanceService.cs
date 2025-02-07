using MarcAI.Application.Dtos.Attendances;
using MarcAI.Application.Dtos.Filters;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Aggregates.Attendances;

namespace MarcAI.Application.Services;
internal class AttendanceService : IAttendanceService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICostumerRepository _customerRepository;

    public AttendanceService(IServiceRepository serviceRepository,
                             IAttendanceRepository attendanceRepository,
                             IEmployeeRepository employeeRepository,
                             ICostumerRepository customerRepository)
    {
        _serviceRepository = serviceRepository;
        _attendanceRepository = attendanceRepository;
        _employeeRepository = employeeRepository;
        _customerRepository = customerRepository;
    }

    public async Task<Attendance> CreateAttenndance(CreateAttendanceDto data)
    {
        var costumerTask = _customerRepository.GetByIdAsync(data.CostumerId);
        var employeeTask = _employeeRepository.GetByIdAsync(data.EmployeeId);
        var servicesTask = _serviceRepository.GetListByIds(data.ServiceIds);

        await Task.WhenAll(costumerTask, employeeTask, servicesTask);

        var costumer = await costumerTask ?? throw new ArgumentException("Costumer not found"); 
        var employee = await employeeTask ?? throw new ArgumentException("Employee not found");
        var services = await servicesTask;

        var totalDuration = TimeSpan.Zero;
        var totalPrice = 0m;

        foreach (var service in services)
        {
            totalDuration += service.Duration;
            totalPrice += service.Price;
        }

        var newAttendance = Attendance.Create(data.Appointment, employee.Id, costumer.Id,totalDuration, totalPrice);

        newAttendance.AddServices(services);

        return await _attendanceRepository.AddAsync(newAttendance);
    }

    public Task<IEnumerable<AttendanceDto>> GetList(AttendanceFilterDto filter)
    {
        throw new NotImplementedException();
    }
}
