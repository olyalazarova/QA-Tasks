using exam_210419.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace exam_210419
{
    public class DeleteTest
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
        public async Task DeleteAuthor_WithValidData_ShouldReturnSuccess()
        {
            //Arrange
            //id = 25320c5e-f58a-4b1f-b63a-8ee07a840bdf

            var delete = await _client.DeleteAsync($"/api/authors/25320c5e-f58a-4b1f-b63a-8ee07a840bdf");
            
            delete.EnsureSuccessStatusCode();
            var response = delete.IsSuccessStatusCode.ToString();



            //Assert
            Assert.AreEqual("200", response);

        }
    }
}
