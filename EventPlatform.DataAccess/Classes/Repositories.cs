using EventPlatform.DataAccess.Repositories;
using EventPlatform.Entities.Contexts;

namespace EventPlatform.DataAccess.Classes
{
    public class Repositories
    {
        private static readonly EventContext _context = new();

        private EventRepository? _eventRepository;
        private TaskRepository? _taskRepository;
        private UserRepository? _userRepository;

        public EventRepository EventRepository 
            => _eventRepository ??= new(_context);
        
        public TaskRepository TaskRepository 
            => _taskRepository ??= new(_context);
        
        public UserRepository UserRepository 
            => _userRepository ??= new(_context);
    }
}
