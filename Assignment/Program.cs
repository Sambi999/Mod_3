using Newtonsoft.Json.Linq;
using RestSharp;
using System.ComponentModel.DataAnnotations;



string baseUrl = "https://jsonplaceholder.typicode.com/";
var client = new RestClient(baseUrl);
/*
var getUserRequest = new RestRequest("users/4", Method.Get);
var getUserResponse = client.Execute(getUserRequest);
Console.WriteLine("GET response : \n" + getUserResponse.Content);

var createUserRequest = new RestRequest("users", Method.Post);
createUserRequest.AddParameter("name", "John Doe");
createUserRequest.AddParameter("job", "Software Developer");

var createUserResponse = client.Execute(createUserRequest);
Console.WriteLine("POST Create User Response:");
Console.WriteLine(createUserResponse.Content);

var updateUserRequest = new RestRequest("users/2", Method.Put);
updateUserRequest.AddParameter("name", "Updated John Doe");
updateUserRequest.AddParameter("job", "Senior Software Developer");

var updateUserResponse = client.Execute(updateUserRequest);
Console.WriteLine("PUT Update User Response:");
Console.WriteLine(updateUserResponse.Content);

var deleteUserRequest = new RestRequest("users/2", Method.Delete);
var deleteUserResponse = client.Execute(deleteUserRequest);
Console.WriteLine("DELETE User Response:");
Console.WriteLine(deleteUserResponse.Content);
*/
GetAllUsers(client);
CreateUser(client);
updateUser(client);
DeleteUser(client);
GetSingleUser(client);

static void GetAllUsers(RestClient client)
{
    var getAllUserRequest = new RestRequest("posts", Method.Get);
    var getUserReponse = client.Execute(getAllUserRequest);
    Console.WriteLine("GET Response:" + getUserReponse.Content);
}

static void CreateUser(RestClient client)
{
    var createUserRequest = new RestRequest("posts", Method.Post);
    createUserRequest.AddHeader("Content-Type", "application/json");
    createUserRequest.AddJsonBody(new
    {
        title = "foo",
        body = "bar",
        userId = "100"
    });
    var createUserResponse = client.Execute(createUserRequest);
    Console.WriteLine("POST Response:" + createUserResponse.Content);
}

static void updateUser(RestClient client)
{
    var updateUserRequest = new RestRequest("posts/1", Method.Put);
    updateUserRequest.AddHeader("Content-Type", "application/json");
    updateUserRequest.AddJsonBody(new
    {
        userId = "500",
        body = "Update user",

    });
    var updateUserResponse = client.Execute(updateUserRequest);
    Console.WriteLine("PUT Response:" + updateUserResponse.Content);
}

static void DeleteUser(RestClient client)
{
    var deleteUserRequest = new RestRequest("posts/1", Method.Delete);
    var deleteUserResponse = client.Execute(deleteUserRequest);
    if (deleteUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        Console.WriteLine("DELETE Response:Item Deleted Successfully");
    }
    else
    {
        Console.WriteLine($"Error:{deleteUserResponse.ErrorMessage}");
    }


}
static void GetSingleUser(RestClient client)
{
    var getSingleUserRequest = new RestRequest("posts/1", Method.Get);
    var getSingeUserResponse = client.Execute(getSingleUserRequest);
    if (getSingeUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        JObject? data = JObject.Parse(getSingeUserResponse.Content);
        string? userId = data["userId"].ToString();
        string? id = data["id"].ToString();
        string? title = data["title"].ToString();
        string? body = data["body"].ToString();
        Console.WriteLine("GET Single User Response:");
        Console.WriteLine($"userId:{userId}\nId:{id}\nTitle:{title}\nBody:{body}");

    }
    else
    {
        Console.WriteLine($"Error:{getSingeUserResponse.ErrorMessage}");
    }
}

