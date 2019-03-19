using System;
using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;

namespace UnitTestProject
{
	[TestFixture]
	public class TestsPage
	{
		[Test]
		public void GET_ListUsers()
		{
			Helper<Users> restApi = new Helper<Users>();
			var restUrl = restApi.SetUrl("api/users?page=4");
			var restRequest = restApi.CreateGetRequest();
			var response = restApi.GetResponse(restUrl, restRequest);
			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
			Users content = restApi.GetContent<Users>(response);
			Assert.AreEqual(content.page,4);
			Assert.AreEqual(content.per_page, 3);
			Assert.AreEqual(content.total, 12);
			Assert.AreEqual(content.total_pages, 4);
			Assert.AreEqual(content.data[0].id, 10);
			Assert.AreEqual(content.data[1].id, 11);
			Assert.AreEqual(content.data[2].id, 12);
			Assert.AreEqual(content.data.Count,content.per_page);
		}
		
		
		[Test]
		public void POST_CreateUsers()
		{
			string jsonString = @"{
                                    ""name"": ""morpheus"",
                                    ""job"": ""leader""
                                }";
			Helper<CreateUser> restApi = new Helper<CreateUser>();

			var restUrl = restApi.SetUrl("api/users");
			var restRequest = restApi.CreatePostRequest(jsonString);
			var response = restApi.GetResponse(restUrl, restRequest);
			Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
			CreateUser content = restApi.GetContent<CreateUser>(response);
			Assert.AreEqual(content.name, "morpheus");
			Assert.AreEqual(content.job, "leader");
		}


		[Test]
		public void PUT_UpdateUser()
		{
			string jsonString = @"{
                                    ""name"": ""morpheus"",
                                    ""job"": ""zion resident""
                                }";
			Helper<CreateUser> restApi = new Helper<CreateUser>();

			var restUrl = restApi.SetUrl("api/users/2");
			var restRequest = restApi.CreatePutRequest(jsonString);
			var response = restApi.GetResponse(restUrl, restRequest);
			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
			CreateUser content = restApi.GetContent<CreateUser>(response);
			Assert.AreEqual(content.name, "morpheus");
			Assert.AreEqual(content.job, "zion resident");
		}

		[Test]
		public void PATCH_UpdateUser()
		{
			string jsonString = @"{
                                    ""name"": ""morpheus"",
                                    ""job"": ""zion resident123""
                                }";
			Helper<CreateUser> restApi = new Helper<CreateUser>();
			var restUrl = restApi.SetUrl("api/users/2");
			var restRequest = restApi.CreatePatchRequest(jsonString);
			var response = restApi.GetResponse(restUrl, restRequest);
			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
			CreateUser content = restApi.GetContent<CreateUser>(response);
			Assert.AreEqual(content.name, "morpheus");
			Assert.AreEqual(content.job, "zion resident123");
		}

		[Test]
		public void DELETE_DeleteUser()
		{
			Helper<CreateUser> restApi = new Helper<CreateUser>();
			var restUrl = restApi.SetUrl("api/users");
			var restRequest = restApi.CreateDeleteRequest();
			var response = restApi.GetResponse(restUrl, restRequest);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
		}

	}
}
