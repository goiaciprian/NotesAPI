using Microsoft.AspNetCore.Mvc;
using Notes_API.Controllers;
using Notes_API.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Notes_API.Tests
{
    public class CategoriesControllerTests
    {
        private readonly List<Category> expectedData = new List<Category>()
        {
            new Category(){ Name="To do", Color = "#f0f0f0", Id = 1},
            new Category(){ Name="Doing", Color = "#F1ED95", Id = 2},
            new Category(){ Name="Done", Color = "#A0D582", Id = 3},

        };

        //arrange
        private readonly CategoriesController controller = new CategoriesController();

        [Fact]
        public void GetCategories_Success()
        {
            //act
            OkObjectResult actual = (OkObjectResult)controller.GetCategories();

            //assert
            Assert.IsType<List<Category>>(actual.Value);
            Assert.NotEmpty((List<Category>)actual.Value);

        }

        [Fact]
        public void GetCategoryById_Success()
        {
            //act
            OkObjectResult actual = (OkObjectResult)controller.GetCategoryById(1);

            //assert
            Assert.IsType<Category>(actual.Value);
            Assert.Equal(expectedData[0].Id, ((Category)actual.Value).Id);
        }

        [Fact]
        public void GetCategoryById_Fail()
        {
            //act
            var actual = controller.GetCategoryById(10);

            //assert
            Assert.IsType<NotFoundResult>(actual);
        }

        [Fact]
        public void PostCategory_Success()
        {
            //arrange
            var newCategory = new Category()
            {
                Name = "xUnitTest",
                Color = "xUnitColor"
            };

            //act
            CreatedAtRouteResult actual = (CreatedAtRouteResult)controller.PostCategory(newCategory);

            //assert
            Assert.IsType<Category>(actual.Value);
            Assert.NotNull(((Category)actual.Value).Id);
        }

        [Fact]
        public void UpdateCategory_Success()
        {
            //arrange
            var newCategory = new Category()
            {
                Name = "xUnitUpdate",
                Color = "xUnitUpdateColor"
            };
            var idToUpdate = 1;

            //act
            OkObjectResult actual = (OkObjectResult)controller.UpdateCategory(idToUpdate, newCategory);

            //assert
            Assert.IsType<Category>(actual.Value);
            Assert.Equal(idToUpdate, ((Category)actual.Value).Id);
            Assert.Equal(newCategory.Name, ((Category)actual.Value).Name);
        }

        [Fact]
        public void UpdateCategory_Fail()
        {
            //arrange
            var newCategory = new Category()
            {
                Name = "xUnitUpdate",
                Color = "xUnitUpdateColor"
            };
            var idToUpdate = 10;

            //act
            var actual = controller.UpdateCategory(idToUpdate, newCategory);

            //assert
            Assert.IsType<NotFoundResult>(actual);
        }

        [Fact]
        public void DeleteCategory_Success()
        {
            //arrange
            var idToDelete = 2;

            //act
            OkObjectResult actual = (OkObjectResult)controller.DeleteCategory(idToDelete);

            //assert
            Assert.IsType<Category>(actual.Value);
            Assert.Equal(idToDelete, ((Category)actual.Value).Id);
        }

        [Fact]
        public void DeleteCategory_Fail()
        {
            //arange 
            var idToDelete = 100;

            //act
            var actual = controller.DeleteCategory(idToDelete);

            //assert
            Assert.IsType<NotFoundResult>(actual);
        }
    }
}
