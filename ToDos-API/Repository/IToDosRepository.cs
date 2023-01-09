using Microsoft.VisualBasic;
using ToDos_API.Models;

namespace ToDos_API.Repository
{
    public interface IToDosRepository
    {
        public IQueryable<ToDo> GetToDos();

        public IQueryable<ToDo> GetToDoById(int id);

        public void AddToDo(ToDo toDo);

        public void UpdateToDo(ToDo toDo);
        
        public void DeleteToDo(ToDo toDo);
    }
}
