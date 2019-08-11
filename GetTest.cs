using exam_210419.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace exam_210419
{
    public class GetTest
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            var test = Guid.Empty.ToString();
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://libraryexam21.azurewebsites.net")
            };
        }

        [Test]
        public async Task GetAuthor_WithValidId()
        {
            var expectedAutor = new Author
            {
                Id = "76053df4-6687-4353-8937-b45556748abe",
                FirstName = "Olya",
                LastName = "Lazarova",
                

            };

            var response = await _client.GetAsync($"/api/authors/{expectedAutor.Id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var actualAuthor = Author.FromJson(content);

            actualAuthor.Equals(expectedAutor);
        }

    }
}
