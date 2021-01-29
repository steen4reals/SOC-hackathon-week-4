using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("Items")]

public class ToDoItemController : ControllerBase {

    private readonly ToDoItemRepository _toDoItemRepository;
    public ToDoItemController (){
        _toDoItemRepository = new ToDoItemRepository ();
    }

    [HTTPGet]
    public IEnumerable<ToDoItem> GetAllItems() {  //if it doesn't work add the id

        return _toDoItemRepository.GetAll();  
    }

    [HttpGet("{Id}")]
    public ToDoItem GetAllItems(long id) {  

        return _toDoItemRepository.GetOne(id);  
    }

}