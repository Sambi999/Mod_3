using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json;
using RestExNunit.Utilities;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestExNunit
{
    [TestFixture]
    public class ReqResTests : CoreCodes
    {
       
       


       


        [Test]
        public void GetSingleUser()
        
        {
            test = extent.CreateTest("Get Single User");
            Log.Information("GetSingleUser Test started");

            var request = new RestRequest("users/2", Method.Get);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var userdata = JsonConvert.DeserializeObject<UserDataResponse>(response.Content);
                UserData? user = userdata?.Data;

                Assert.NotNull(user);
                Log.Information("User returned");
                Assert.That(user.Id, Is.EqualTo(2));
                Log.Information("User Id matches with fetch");
                Assert.IsNotEmpty(user.Email);
                Log.Information("Email is not empty");
                Log.Information("Get Single User test passed all Asserts.");

                test.Pass("GetSingleUser test passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("FetSingleUser test failed");
            }
        }


        [Test]
        [Order(1)]
        public void CreateUser()
        {
            test = extent.CreateTest("Create User");
            Log.Information("CreateUser Test started");

            var request = new RestRequest("users", Method.Post);
            try
            {
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new { name = "John Doe", job = "Software Developer" });
                var response = client.Execute(request);
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User created and returned");

                Assert.IsNotEmpty(user.Email);
                Log.Information("Email is not empty");
                Log.Information("Create User test passed all Asserts.");
                test.Pass("Create User test passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("Create User test failed");
            }
        }





        [Test]
        [Order(2)]
        public void UpdateUser()
        {
            test = extent.CreateTest("Update User");
            Log.Information("Update User Test started");

            var request = new RestRequest("users/2", Method.Put);
            
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { name = "Updated John Doe", job = "Updated Software Developer" });
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User updated and returned");
                Log.Information("Update User test passed all Asserts.");
                test.Pass("Update User test passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("Update User test failed");
            }
            
            
        }
            
        [Test]
        [Order(3)]
        public void DeleteUser()
        {
            test = extent.CreateTest("Delete User");
            Log.Information("Delete User Test started");
            var request = new RestRequest("users/2", Method.Delete);
            var response = client.Execute(request);
            try
            { 
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NoContent));
                Log.Information("User deleted");
                Log.Information("Delete User test passed.");
                test.Pass("Delete User test passed.");
            }
            catch(AssertionException)
            {
                test.Fail("Delete User test failed");
            }


        }
        [Test]
        [Order(4)]
        public void GetNonExistingUser()
        {
            test = extent.CreateTest("Get NonExisting User");
            Log.Information("Get NonExisting User Test started");
            var request = new RestRequest("users/999", Method.Get);
            var response = client.Execute(request);
            try
            {

                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
                Log.Information("User NotExisted");
                Log.Information("NotExisted User test passed.");
                test.Pass("NotExisted User test passed.");
            }
            catch (AssertionException)
            {
                test.Fail("NotExisted User test failed");
            }
        }
    }
}
