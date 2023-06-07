using EventPlatform.Entities.Interfaces;

namespace EventPlatform.Entities.DTO;

public class TaskDto
    : IDto<TaskDto>
{
    public string Description { get; set; } = null!;

    public EventDto Event { get; set; } = null!;

    public TaskDto(ECP.Task task)
    {
        Description = task.Description;
        Event = new(task.Event);
    }
}
