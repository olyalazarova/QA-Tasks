using exam_210419.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace exam_210419
{
    public class PostTest
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://libraryexam21.azurewebsites.net")
            };
        }

        [Test]
        public async Task PostAuthor_WithValidData_ShouldReturnSuccess()
        {
            //Arrange
            var expectedAuthor = new Author
            {
                Id = "75a78dce-0d81-459c-b13a-a045dbe8ec10",
                FirstName = "Peter",
                LastName = "Petrov",
                Genre = "History",

            };
            var requestContent = new StringContent(expectedAuthor.ToJson());

            //Act
            var response = await _client.PostAsync("/api/authors/", requestContent);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var actualAutor = Author.FromJson(responseContent);

            //Assert
            actualAutor.Equals(expectedAuthor);

        }
    }
}
