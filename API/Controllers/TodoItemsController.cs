﻿namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using BusinessLayer.DTOs;
    using BusinessLayer.Interfaces;
    using DataLayer.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly IService<TodoItemDTO, TodoItem> _todoItemService;

        public TodoItemsController(IService<TodoItemDTO, TodoItem> todoItemService)
        {
            _todoItemService = todoItemService;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            var todoItems = await _todoItemService.GetAll();
            return Ok(todoItems);
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(long id)
        {
            var todoItem = await _todoItemService.Get(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task <IActionResult> PutTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            await _todoItemService.Update(todoItemDTO, id);
            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostTodoItem(TodoItemDTO todoItemDTO)
        {
            await _todoItemService.Add(todoItemDTO);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItemDTO.Id },
                todoItemDTO);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _todoItemService.Get(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            await _todoItemService.Delete(id);
            return NoContent();
        }
    }
}
