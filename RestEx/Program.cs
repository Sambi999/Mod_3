using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestEx;
using RestSharp;
using System.ComponentModel.DataAnnotations;



APIwithEX apiexp = new APIwithEX();
apiexp.GetSingleUser();




//string baseUrl = "https://reqres.in/api";
//var client = new RestClient(baseUrl);
/*

 
var getUserRequest = new RestRequest("users/2", Method.Get);
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

//GetAllUsers(client);
//GetSingleUser(client);
//CreateUser(client);
//UpdateUser(client);
//DeleteUser(client);
/*
static void GetAllUsers(RestClient client)
{
    var getUserRequest = new RestRequest("users", Method.Get);
    getUserRequest.AddQueryParameter("page", "2"); //Adding query parameter

    var getUserResponse = client.Execute(getUserRequest);
    Console.WriteLine("GET response: \n" + getUserResponse.Content);

}
//static void UpdateUser()
static void CreateUser(RestClient client)
{

    var createUserRequest = new RestRequest("users", Method.Post);
    createUserRequest.AddHeader("Content-Type", "application/json"); //
    createUserRequest.AddJsonBody(new
    {
        name = "John Doe",
        job = "Software Developer"
    });


    var createUserResponse = client.Execute(createUserRequest);
    Console.WriteLine("POST Create User Response:");
    Console.WriteLine(createUserResponse.Content);
}
static void UpdateUser(RestClient client)
{

    var updateUserRequest = new RestRequest("users/2", Method.Put);

    updateUserRequest.AddHeader("Content-Type", "application/json");
    updateUserRequest.AddJsonBody(new
    { 
        name = "Updated John Doe", 
        job = "Senior Software Developer" 
    });

    var updateUserResponse = client.Execute(updateUserRequest);
    Console.WriteLine("PUT Update User Response:");
    Console.WriteLine(updateUserResponse.Content);
}
static void DeleteUser(RestClient client)
{
    var deleteUserRequest = new RestRequest("users/2", Method.Delete);
    var deleteUserResponse = client.Execute(deleteUserRequest);
    Console.WriteLine("DELETE User Response:");
    Console.WriteLine(deleteUserResponse.Content);

}



static void GetSingleUser(RestClient client)
{
    var getUserRequest = new RestRequest("users/2", Method.Get);
    //getUserRequest.AddQueryParameter("page", "2"); //Adding query parameter

    var getUserResponse = client.Execute(getUserRequest);

    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        // Parse JSON response content
        JObject? userJson = JObject.Parse(getUserResponse.Content);


        string? userName = userJson["data"]["first_name"].ToString();
        string? userLastName = userJson["data"]["last_name"].ToString();

        Console.WriteLine($"User Name: {userName} {userLastName} ");
    }
    else
    {
        Console.WriteLine($"Error: {getUserResponse.ErrorMessage}");
    }
}
*/
/*
static void GetSingleUser(RestClient client)
{
    var getUserRequest = new RestRequest("users/6", Method.Get);
    var getUserResponse = client.Execute(getUserRequest);

    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        // Deserialize JSON response content into C# object
        var response = JsonConvert.DeserializeObject<UserDataResponse>(getUserResponse.Content);
        

        UserData? user = response?.Data;


        // Access properties of the deserialized object
        Console.WriteLine($"User ID: {user?.Id} ");
        Console.WriteLine($"User Email: {user?.Email} ");
        Console.WriteLine($"User Name: {user?.FirstName} {user?.LastName} "); 
        Console.WriteLine($"User Avatar: {user?.Avatar} "); 

    }
    else
    {
        Console.WriteLine($"Error: {getUserResponse.ErrorMessage}");
    }
}
*/






