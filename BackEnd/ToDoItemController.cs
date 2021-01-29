using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("items")]

public class ToDoItemController : ControllerBase {

    private readonly ToDoItemRepository _toDoItemRepository;
    public ToDoItemController (){
        _toDoItemRepository = new ToDoItemRepository ();
    }

    [HttpGet]
    public IEnumerable<ToDoItem> GetAllItems() {  //if it doesn't work add the id

        return _toDoItemRepository.GetAll();  
    }

    [HttpGet("{Id}")]
    public ToDoItem GetOneItem(long id) {  

        return _toDoItemRepository.GetOne(id);  
    }

    [HttpDelete]
    public void DeleteCompletedItem()
    {
        _toDoItemRepository.DeleteComplete();
    }

    [HttpDelete("{Id}")]
    public void DeleteItem(long id)
    {
        _toDoItemRepository.Delete(id);
    }

    [HttpPut("{Id}")]

    public ToDoItem UpdateItem(long id, [FromBody] ToDoItem toDoItem)
    {
        toDoItem.Id = id;
        return _toDoItemRepository.Update(toDoItem);
    }

    [HttpPost]

    public ToDoItem CreateItem([FromBody] ToDoItem toDoItem)
    {
        return _toDoItemRepository.Create(toDoItem);
    }



}