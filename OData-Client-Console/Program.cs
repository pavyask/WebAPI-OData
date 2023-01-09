using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using ToDos_API.Models;

Console.WriteLine("Console Client");

var baseUrl = @"https://localhost:7032/odata/ToDos";


while (true)
{
    var option = MainMenu();
    ProcessOption(option);
    Console.Clear();
}






Option MainMenu()
{
    Console.WriteLine("Select an option:");
    Console.WriteLine("1. Get ToDos");
    Console.WriteLine("2. Get ToDo by Id");
    Console.WriteLine("3. Add ToDo");
    Console.WriteLine("4. Update ToDo");
    Console.WriteLine("5. Delete ToDo");
    Console.WriteLine("6. Exit");

    var option = Console.ReadKey().KeyChar.ToString();

    switch (option)
    {
        case "1":
            return Option.GetToDos;
        case "2":
            return Option.GetToDoById;
        case "3":
            return Option.AddToDo;
        case "4":
            return Option.UpdateToDo;
        case "5":
            return Option.DeleteToDo;
        case "6":
            return Option.Exit;
        default:
            return Option.Invalid;
    }
}

async void ProcessOption(Option option)
{
    switch (option)
    {
        case Option.GetToDos:
            GetToDos();
            break;
        case Option.GetToDoById:
            GetToDoById();
            break;
        case Option.AddToDo:
            AddToDo();
            break;
        case Option.UpdateToDo:
            UpdateToDo();
            break;
        case Option.DeleteToDo:
            DeleteToDo();
            break;
        case Option.Exit:
            Environment.Exit(0);
            break;
        case Option.Invalid:
            Console.WriteLine("Invalid option");
            break;
    }
}

void GetToDos()
{
    Console.Clear();
    var query = baseUrl;
    Console.WriteLine("Select GET query:");
    Console.WriteLine("1. Get All ToDos with count");
    Console.WriteLine("2. Get All Not Done ToDos");
    Console.WriteLine("3. Get All ToDos ordered by Priority descending");
    Console.WriteLine("4. Get First 3 ToDos");
    Console.WriteLine("5. Get All ToDo's Title");

    var option = Console.ReadKey().KeyChar.ToString();

    switch (option)
    {
        case "1":
            query = query.SetQueryParam("$count", "true");
            break;
        case "2":
            query = query.SetQueryParam("$filter", "IsDone eq false");
            break;
        case "3":
            query = query.SetQueryParam("$orderby", "Priority desc");
            break;
        case "4":
            query = query.SetQueryParam("$top", 3);
            break;
        case "5":
            query = query.SetQueryParam("$select", "Title");
            break;
        default:
            break;
    }

    Console.WriteLine("\n\nGetting ToDos ...");
    var response = query.AllowAnyHttpStatus().GetStringAsync().Result;
    JObject json = JObject.Parse(response);
    var toDos = json["value"].ToObject<List<ToDo>>();

    foreach (var toDo in toDos)
    {
        Console.WriteLine(toDo);
    }
    Console.WriteLine(response);
    Console.WriteLine("\n\nPress any key to continue...");
    Console.ReadKey();
}

void GetToDoById()
{
    Console.Clear();
    Console.WriteLine("Enter Id of ToDo");
    var id = Console.ReadLine();
    Console.WriteLine($"Getting ToDo with Id={id} ...");

    var response = baseUrl.AllowAnyHttpStatus().AppendPathSegment($@"/{id}").GetStringAsync();

    Console.WriteLine(response.Result);
    Console.WriteLine("\n\nPress any key to continue...");
    Console.ReadKey();
}

void AddToDo()
{
    Console.Clear();

    Console.WriteLine("Enter Id of ToDo");
    var id = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter Title of ToDo");
    var title = Console.ReadLine();

    Console.WriteLine("Enter Description of ToDo");
    var descritprion = Console.ReadLine();

    Console.WriteLine("Choose Priority:");
    Console.WriteLine("1. Could");
    Console.WriteLine("2. Should");
    Console.WriteLine("3. Must");
    ToDoPriority priority;
    var num = Console.ReadKey().KeyChar.ToString();
    Console.WriteLine();
    switch (num)
    {
        case "1":
            priority = ToDoPriority.Could;
            break;
        case "2":
            priority = ToDoPriority.Should;
            break;
        case "3":
            priority = ToDoPriority.Must;
            break;
        default:
            priority = ToDoPriority.Could;
            break;
    }

    Console.WriteLine("Choose IsDone [y/n]:");
    bool isDone;
    num = Console.ReadKey().KeyChar.ToString();
    Console.WriteLine();
    switch (num)
    {
        case "y":
            isDone = false;
            break;
        case "n":
            isDone = true;
            break;
        default:
            isDone = false;
            break;
    }

    var toDo = new ToDo(id, title, descritprion, priority, isDone);
    Console.WriteLine($"Sending created toDo to the server ...");

    var response = baseUrl
        .WithHeaders(new { Content_Type = "application/json; charset=UTF-8" })
        .AllowAnyHttpStatus()
        .PostJsonAsync(toDo)
        .ReceiveString().Result;

    Console.WriteLine(response);
    Console.WriteLine("\n\nPress any key to continue...");
    Console.ReadKey();
}

void UpdateToDo()
{
    Console.Clear();

    Console.WriteLine("Enter Id of ToDo");
    var id = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter new Title of ToDo");
    var title = Console.ReadLine();

    Console.WriteLine("Enter new Description of ToDo");
    var descritprion = Console.ReadLine();

    Console.WriteLine("Choose new Priority:");
    Console.WriteLine("1. Could");
    Console.WriteLine("2. Should");
    Console.WriteLine("3. Must");
    ToDoPriority priority;
    var num = Console.ReadKey().KeyChar.ToString();
    Console.WriteLine();
    switch (num)
    {
        case "1":
            priority = ToDoPriority.Could;
            break;
        case "2":
            priority = ToDoPriority.Should;
            break;
        case "3":
            priority = ToDoPriority.Must;
            break;
        default:
            priority = ToDoPriority.Could;
            break;
    }

    Console.WriteLine("Choose new IsDone [y/n]:");
    bool isDone;
    num = Console.ReadKey().KeyChar.ToString();
    Console.WriteLine();
    switch (num)
    {
        case "y":
            isDone = false;
            break;
        case "n":
            isDone = true;
            break;
        default:
            isDone = false;
            break;
    }

    var toDo = new ToDo(id, title, descritprion, priority, isDone);
    Console.WriteLine($"Sending updated toDo to the server ...");

    var response = baseUrl
        .AppendPathSegment($"/{id}")
        .WithHeaders(new { Content_Type = "application/json; charset=UTF-8" })
        .AllowAnyHttpStatus()
        .PutJsonAsync(toDo)
        .ReceiveString().Result;

    Console.WriteLine(response);
    Console.WriteLine("\n\nPress any key to continue...");
    Console.ReadKey();
}

void DeleteToDo()
{
    Console.Clear();

    Console.WriteLine("Enter Id of ToDo");
    var id = int.Parse(Console.ReadLine());

    Console.WriteLine($"Deleting toDo with id={id} ...");

    var response = baseUrl
        .AppendPathSegment($"/{id}")
        .AllowAnyHttpStatus()
        .DeleteAsync()
        .ReceiveString().Result;

    Console.WriteLine(response);
    Console.WriteLine("\n\nPress any key to continue...");
    Console.ReadKey();
}

enum Option
{
    GetToDos,
    GetToDoById,
    AddToDo,
    UpdateToDo,
    DeleteToDo,
    Exit,
    Invalid,
};