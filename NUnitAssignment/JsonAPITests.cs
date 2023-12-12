using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitAssignment
{
    [TestFixture]
    internal class JsonAPITests
    {
        private RestClient client;
        private string baseurl = "https://jsonplaceholder.typicode.com/";

        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseurl);


        }

        [Test]
        [Order(1)]
        public void GetSingleUser()
        {
            var request = new RestRequest("posts/2", Method.Get);
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var userdata = JsonConvert.DeserializeObject<UserData>(response.Content);



            Assert.NotNull(userdata);
            Assert.That(userdata.Id, Is.EqualTo(2));
            Assert.IsNotEmpty(userdata.Body);




        }

        [Test, Order(2)]

        public void CreateUser()
        {
            var createUserRequest = new RestRequest("posts", Method.Post);
            createUserRequest.AddHeader("Content-Type", "application/json");
            createUserRequest.AddJsonBody(new
            {
                userId = "43",
                id = "43",
                title = "Home",
                body = "architecto"
            });

            var createUserResponse = client.Execute(createUserRequest);
            Assert.That(createUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));

            var userdata = JsonConvert.DeserializeObject<UserData>(createUserResponse.Content);

            Assert.NotNull(userdata);




        }

        [Test, Order(3)]

        public void UpdateUser()
        {


            var updateUserRequest = new RestRequest("posts/2", Method.Put);


            updateUserRequest.AddHeader("Content-Type", "application/json");
            updateUserRequest.AddJsonBody(new
            {
                userId = "143",
                id = "43",
                title = "Homes",
                body = "architecto"
            });


            var updateUserResponse = client.Execute(updateUserRequest);

            Assert.That(updateUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var userdata = JsonConvert.DeserializeObject<UserData>(updateUserResponse.Content);

            Assert.NotNull(userdata);

        }

        [Test, Order(4)]

        public void DeleteUser()
        {
            var deleteUserRequest = new RestRequest("posts/2", Method.Delete);

            var deleteUserResponse = client.Execute(deleteUserRequest);
            Assert.That(deleteUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        }


        [Test, Order(5)]
        public void GetnonExsistingUser()
        {
            var request = new RestRequest("posts/245", Method.Get);
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));


        }

    }
}
