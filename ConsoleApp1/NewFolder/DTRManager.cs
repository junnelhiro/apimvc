using ConsoleApp1.Data;

namespace ConsoleApp1.NewFolder;

public class EmployeeDTR
{
    public int EmployeeId { get; set; }
    public DateTime WorkDate { get; set; }
    public DateTime? AM_IN { get; set; }
    public DateTime? AM_OUT { get; set; }
    public DateTime? PM_IN { get; set; }
    public DateTime? PM_OUT { get; set; }
}

public class DTRManager
{
    private readonly AppDbContext _context;
    public DTRManager(AppDbContext context)
    {
        _context = context;
    }

    public AppDbContext Context { get; }

    public List<EmployeeDTR> GetAppo(DateTime startDate, DateTime endDate)
    {
        var rawData = _context.TimeLogs
        .Where(t => t.WorkDate >= startDate && t.WorkDate <= endDate)
        .ToList();

        return rawData
              .GroupBy(t => new { t.EmployeeId, t.WorkDate })
              .Select(g => new EmployeeDTR
              {
                  EmployeeId = g.Key.EmployeeId,
                  WorkDate = g.Key.WorkDate,
                  AM_IN = g.OrderBy(t => t.LogTime).FirstOrDefault(t => t.LogType == "TimeIn")?.LogTime,
                  AM_OUT = g.OrderBy(t => t.LogTime).FirstOrDefault(t => t.LogType == "BreakOut")?.LogTime,
                  PM_IN = g.OrderBy(t => t.LogTime).FirstOrDefault(t => t.LogType == "BreakIn")?.LogTime,
                  PM_OUT = g.OrderBy(t => t.LogTime).FirstOrDefault(t => t.LogType == "Timeout")?.LogTime,
              }).ToList();

    }
}
