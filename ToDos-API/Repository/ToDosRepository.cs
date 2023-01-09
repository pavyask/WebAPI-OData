using ToDos_API.Models;

namespace ToDos_API.Repository
{
    public class ToDosRepository : IToDosRepository
    {
        private ICollection<ToDo> _toDos = new List<ToDo>()
        {
            new ToDo()
            {
                Id = 1,
                Title = "Learn OData",
                Description = "Learn OData and build a simple OData API",
                Priority = ToDoPriority.Must,
                IsDone = false
            },
            new ToDo()
            {
                Id = 2,
                Title = "title2",
                Description = "description2",
                Priority = ToDoPriority.Could,
                IsDone = true,
            },
            new ToDo()
            {
                Id = 3,
                Title = "title3",
                Description = "description3",
                Priority = ToDoPriority.Should,
                IsDone = false,
            },
            new ToDo()
            {
                Id = 4,
                Title = "title4",
                Description = "description4",
                Priority = ToDoPriority.Must,
                IsDone = true,
            },
            new ToDo()
            {
                Id = 5,
                Title = "title5",
                Description = "description5",
                Priority = ToDoPriority.Could,
                IsDone = false,
            },

        };

        public IQueryable<ToDo> GetToDos()
        {
            return _toDos.AsQueryable();
        }

        public IQueryable<ToDo> GetToDoById(int id)
        {
            var toDo = _toDos.FirstOrDefault(t => t.Id == id);
            return toDo != null ? new List<ToDo>() { toDo }.AsQueryable() : new List<ToDo>().AsQueryable();
        }

        public void AddToDo(ToDo toDo)
        {
            _toDos.Add(toDo);
        }

        public void UpdateToDo(ToDo toDo)
        {
            var existingToDo = _toDos.FirstOrDefault(t => t.Id == toDo.Id);
            if (existingToDo != null)
            {
                existingToDo.Title = toDo.Title;
                existingToDo.Description = toDo.Description;
                existingToDo.Priority = toDo.Priority;
                existingToDo.IsDone = toDo.IsDone;
            }
        }
        public void DeleteToDo(ToDo toDo)
        {
            _toDos.Remove(toDo);
        }
    }
}
