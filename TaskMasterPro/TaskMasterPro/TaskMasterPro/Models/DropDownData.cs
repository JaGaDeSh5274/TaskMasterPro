namespace TaskMasterPro.Models
{public class DropdownData
{
    public List<StatusResult> Statuses { get; set; }
    public List<PriorityResult> Priorities { get; set; }
}

public class StatusResult
{
    public int Id { get; set; }
    public string Status { get; set; }
}

public class PriorityResult
{
    public int Id { get; set; }
    public string Priority { get; set; }
}
}
