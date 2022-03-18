using Microsoft.AspNetCore.Mvc;
using Notes_API.Controllers;
using Notes_API.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Notes_API.Tests
{
    public class NotesControllerTests
    {
        [Fact]
        public void GetNotes_Success()
        {
            //arrange
            var notesController = new NotesController();

            //act
            OkObjectResult response = (OkObjectResult)notesController.GetNotes();

            //assert
            Assert.IsType<List<Note>>(response.Value);

        }
    }
}
