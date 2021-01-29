using System.Collections.Generic;
using Dapper;
using System;

public class ToDoItemRepository : BaseRepository
{
    public IEnumerable<ToDoItem> GetAll()
    {
        using var connection = CreateConnection();
        return connection.Query<ToDoItem> ("SELECT * FROM ToDoItems;");
    }

    public ToDoItem GetOne(long id)
    {
        using var connection = CreateConnection();
        return connection.QuerySingle<ToDoItem>("SELECT * FROM ToDoItems WHERE id=@Id;", new{Id = id});
    }

    public void Delete(long id)
    {
        using var connection = CreateConnection();
        connection.Execute("DELETE FROM ToDoItems WHERE id =@Id;", new{Id = id});
    }

    public ToDoItem Update(ToDoItem toDoItem)
    {
        using var connection = CreateConnection();
        connection.QuerySingle<ToDoItem>("UPDATE ToDoItems SET Title =@Title, Priority =@Prioirity IsComplete =@IsComplete WHERE id=@Id RETURNING Id, Title, Priority, IsComplete;", toDoItem);

    }

    public ToDoItem Create(ToDoItem toDoItem)
    {
        using var connection = CreateConnection();
        connection.QuerySingle<ToDoItem>("INSERT INTO ToDoItems (Title, Priority, IsComplete) VALUES (@Title, @Priority, @IsComplete) RETURNING Id, Title, Priority, IsComplete;", toDoItem);
    }

}

//GetAll
//GetOne
//Delete
//Update
//Create