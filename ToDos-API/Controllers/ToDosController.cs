using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ToDos_API.Models;
using ToDos_API.Repository;

namespace ToDos_API.Controllers
{
    public class ToDosController : ODataController
    {
        private readonly IToDosRepository _toDosRepository;

        public ToDosController(IToDosRepository toDosRepository)
        {
            _toDosRepository = toDosRepository;
        }

        [EnableQuery]
        [HttpGet]
        public IQueryable<ToDo> Get()
        {
            return _toDosRepository.GetToDos();
        }

        [EnableQuery]
        public SingleResult<ToDo> Get([FromODataUri] int key)
        {
            var toDo = _toDosRepository.GetToDoById(key);
            return SingleResult.Create(toDo);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ToDo toDo)
        {
            var checkToDo = _toDosRepository.GetToDoById(toDo.Id).FirstOrDefault();
            if (checkToDo != null)
                return BadRequest("ToDo already exists");

            _toDosRepository.AddToDo(toDo);
            return Created(toDo);
        }

        [HttpPut]
        public IActionResult Put([FromODataUri] int key, [FromBody] ToDo toDo)
        {
            var checkToDo = _toDosRepository.GetToDoById(key).FirstOrDefault();
            if (checkToDo == null)
                return NotFound("ToDo not found");

            toDo.Id = key;
            _toDosRepository.UpdateToDo(toDo);
            return Updated(toDo);
        }

        [HttpDelete]
        public IActionResult Delete([FromODataUri] int key)
        {
            var checkToDo = _toDosRepository.GetToDoById(key).FirstOrDefault();
            if (checkToDo == null)
                return NotFound("ToDo not found");

            _toDosRepository.DeleteToDo(checkToDo);
            return NoContent();
        }
    }
}
