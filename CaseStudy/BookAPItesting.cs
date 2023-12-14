using CaseStudy.Utilities;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy
{
    [TestFixture]
    public class BookAPItesting : CoreCodes
    {
        [Test, Order(0)]
        
        public void CreateTokenTest()
        {
            test = extent.CreateTest("Create Token");
            Log.Information("Create Token Test Started");

            var createTokenrequest = new RestRequest("auth", Method.Post);
            createTokenrequest.AddHeader("Content-Type","application/json");
            createTokenrequest.AddJsonBody(new
            { 
                username = "admin",
                password = "password123" 
            });
            var createTokenresponse = client.Execute(createTokenrequest);

            try
            {
                Assert.That(createTokenresponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response:" + createTokenresponse.Content);
                Log.Information("Token Received");
                Log.Information("Create Token test passed all Asserts");
                test.Pass("Create Token Test passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("Create Token test failed");
            }
        }
        [Test, Order(1)]
        public void AllBooking()
        {
            test = extent.CreateTest("Get All booking");
            Log.Information("Get all booking Test Started");

            var getallbookingreq = new RestRequest("booking", Method.Get);
            var getallbookingres = client.Execute(getallbookingreq);
            try
            {
                Assert.That(getallbookingres.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + getallbookingres.Content);

                List<BookResponse> bookingdata = JsonConvert.DeserializeObject<List<BookResponse>>(getallbookingres.Content);
                Assert.NotNull(bookingdata);
                Log.Information("all booking Returned");
                Log.Information("Get booking test passed all asserts");
                test.Pass("Get booking test passed all asserts.");

            }
            catch(AssertionException)
            {
                test.Fail("Get all booking test failed");
            }
        }
        [Test, Order(2), TestCase(13)]
        public void GetOneBooking(int id)
        {
            test = extent.CreateTest("Get One booking");
            Log.Information("Get one booking Test Started");

            var getonebookingreq = new RestRequest("booking/"+id, Method.Get);
            getonebookingreq.AddHeader("Accept", "application/json");
            var getonebookingres = client.Execute(getonebookingreq);
            try
            {
                Assert.That(getonebookingres.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + getonebookingres.Content);

                var bookingdata = JsonConvert.DeserializeObject<BookDetails>(getonebookingres.Content);
                Assert.NotNull(bookingdata);
                Log.Information("booking Returned");
                Assert.IsNotEmpty(bookingdata.FirstName);
                Log.Information("Name is not empty");
                Log.Information("Get one booking test passed all asserts");
                test.Pass("Get one booking test passed all asserts.");

            }
            catch (AssertionException)
            {
                test.Fail("Get one booking test failed");
            }
        }
        [Test, Order(3)]
        public void CreateBooking()
        {
            test = extent.CreateTest("Create booking");
            Log.Information("Create booking Test Started");

            var createbookingreq = new RestRequest("booking", Method.Post);
            createbookingreq.AddHeader("Content-Type", "application/json");
            createbookingreq.AddHeader("Accept", "application/json");
            createbookingreq.AddJsonBody(new {
                firstname = "Jim",
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "2018-01-01",
                    checkout = "2019-01-01"

                },
                additionalneeds = "Breakfast"
            
    });
            var response = client.Execute(createbookingreq);
            try
            {
                Assert.That(response.Content, Is.Not.Null, "Response is null");
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + response.Content);

               
                
               
                Log.Information("Booking created and Returned");
                test.Pass("Booking creation test passed.");

            }
            catch (AssertionException)
            {
                test.Fail("Booking creation test failed");
            }
        }
        [Test, Order(4)]
        [TestCase(2)]
        public void UpdateBooking(int uid)
        {
            test = extent.CreateTest("Update booking");
            Log.Information("Update booking Test Started");

            var gettokenreq = new RestRequest("auth", Method.Post);
            gettokenreq.AddHeader("Content-Type", "application/json");
            //gettokenreq.AddHeader("Accept", "application/json");
            gettokenreq.AddJsonBody(new { username = "admin", password = "password123" });
            var gettokenresponse = client.Execute(gettokenreq);
            var token = JsonConvert.DeserializeObject<BookDetails>(gettokenresponse.Content);



            var updateuserrequest = new RestRequest("booking/" + uid, Method.Put);
            updateuserrequest.AddHeader("Content-Type", "application/json");
            updateuserrequest.AddHeader("Accept", "application/json");
            updateuserrequest.AddHeader("Cookie", "token="+token.Token);
            updateuserrequest.AddJsonBody(new
            {
                firstname = "Jim",
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "2018-01-01",
                    checkout = "2019-01-01"

                },
                additionalneeds = "Breakfast"

            });
            var response = client.Execute(updateuserrequest);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                
                Log.Information("API Response:" + response.Content);
                var user = JsonConvert.DeserializeObject<BookDetails>(response.Content);
                Console.WriteLine(response.Content);
                Assert.NotNull(user);



                Log.Information("User Updated and Returned");
                Log.Information("Update user test passed.");
                test.Pass("Update user passed.");

            }
            catch (AssertionException)
            {
                test.Fail("Update user test failed");
            }
        }
        [Test, Order(5)]
        [TestCase(8)]
        public void DeleteUser(int usrid)
        {

            test = extent.CreateTest("Delete user");
            Log.Information("Delete User Test Started");
            var gettokenrequest = new RestRequest("auth", Method.Post);
            gettokenrequest.AddHeader("Content-Type", "application/json");
            gettokenrequest.AddJsonBody(new { username = "admin", password = "password123" });
            var gettokenresponse = client.Execute(gettokenrequest);
            var token = JsonConvert.DeserializeObject<BookDetails>(gettokenresponse.Content);


            var deleteUserRequest = new RestRequest("booking/" + usrid, Method.Delete);
            deleteUserRequest.AddHeader("Content-Type", "application/json");
            deleteUserRequest.AddHeader("Cookie", "token=" + token?.Token);
            var deleteUserResponse = client.Execute(deleteUserRequest);
            try
            {
                Assert.That(deleteUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information("API Response:" + deleteUserResponse.Content);
                Log.Information("Booking deletion test passed .");
                test.Pass("Delete booking test passed .");
            }
            catch (AssertionException)
            {
                test.Fail("Delete booking test failed");

            }
        }








        /*
        [Test]
        [Order(0)]
        [TestCase(1)]
        public void GetSingleUserTest(int userId)
        {
            test = extent.CreateTest("Get Single User");
            Log.Information("GetSingleUser Test Started");

            var request = new RestRequest("posts/" + userId, Method.Get);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);

                Assert.NotNull(user);
                Log.Information("User returned");
                Assert.That(user.Id, Is.EqualTo(1));
                Log.Information("User Id matches with fetch");
                Assert.That(user.UserId, Is.EqualTo(1));
                Log.Information("User userId matches with fetch");
                Assert.IsNotEmpty(user.Title);
                Log.Information("User Title matches with fetch");
                Assert.IsNotEmpty(user.Body);
                Log.Information("User Body matches with fetch");

                test.Pass("GetSingleUserTest passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("GetSingleUser test failed");
            }
        }

        [Test]
        [Order(1)]
        public void CreateUserTest()
        {
            test = extent.CreateTest("Create User");
            Log.Information("CreateUser Test Started");

            var request = new RestRequest("posts", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = "10", title = "RestSharp API", body = "RestSharp" });

            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User created and returned");
                Assert.That(user.UserId, Is.EqualTo(10));
                Log.Information("User userId matches with fetch");

                Log.Information("CreateUser test passed all Asserts");

                test.Pass("CreateUserTest passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("Create User test failed");
            }
        }

        [Test]
        [Order(2)]
        [TestCase(1)]
        public void UpdateUserTest(int userId)
        {
            test = extent.CreateTest("Update User");
            Log.Information("UpdateUserTest Started");

            var request = new RestRequest("posts/" + userId, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = "11", title = "Updated RestSharp API", body = "RestSharp" });

            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User updated and returned");

                Log.Information("UpdateUser test passed all Asserts");

                test.Pass("UpdateUserTest passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("Update User test failed");
            }
        }

        [Test]
        [Order(3)]
        [TestCase(1)]
        public void DeleteUserTest(int userId)
        {
            test = extent.CreateTest("Delete User");
            Log.Information("DeleteUserTest Started");

            var request = new RestRequest("posts/" + userId, Method.Delete);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("User deleted");
                Log.Information("Delete User test passed");

                test.Pass("Delete User test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Delete User test failed");
            }
        }

        [Test]
        [Order(4)]
        public void GetAllUsers()
        {
            test = extent.CreateTest("Get All User");
            Log.Information("Get All User Test Started");

            var request = new RestRequest("posts", Method.Get);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                List<UserData> users = JsonConvert.DeserializeObject<List<UserData>>(response.Content);

                Assert.NotNull(users);
                Log.Information("Get All User test passed");

                test.Pass("Get All User test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Get Non-Existing User test failed");
            }
        }

        [Test]
        [Order(5)]
        [TestCase(987)]
        public void GetNonExistingUser(int userId)
        {
            test = extent.CreateTest("Get Non-Existing User");
            Log.Information("Get Non-Existing User Test Started");

            var request = new RestRequest("posts/" + userId, Method.Get);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
                Log.Information("Get Non-Existing User test passed");

                test.Pass("Get Non-Existing User test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Get Non-Existing User test failed");
            }
        }
        */
    }
}
