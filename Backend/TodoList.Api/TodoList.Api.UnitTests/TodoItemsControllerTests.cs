using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using TodoList.Api.Controllers;
using TodoList.Api.Models;
using TodoList.Api.Services;
using Xunit;

namespace TodoList.Api.UnitTests
{
    public class TodoItemsControllerTests
    {
        protected readonly Mock<ILogger<TodoItemsController>> _mockLogger;
        protected readonly Mock<ITodoItemsService> _mockService;

        public TodoItemsControllerTests()
        {
            _mockLogger = new Mock<ILogger<TodoItemsController>>();
            _mockService = new Mock<ITodoItemsService>();
        }

        [Fact]
        public async void GetTodoItems_Valid_ReturnsItems()
        {
            _mockService.Setup(m => m.GetItems())
                .ReturnsAsync(It.IsAny<TodoItemsDTO>());

            var controller = new TodoItemsController(_mockLogger.Object, _mockService.Object);

            var expected = new OkObjectResult(new TodoItemsDTO());
            var actual = await controller.GetTodoItems() as ObjectResult;

            _mockService.Verify(m => m.GetItems(), Times.Once);
            Assert.Equal(expected.StatusCode, actual.StatusCode);
        }

        [Fact]
        public async void GetTodoItems_LogsAndThrowsException()
        {
            _mockService.Setup(m => m.GetItems())
                .Throws(new Exception("An error has occurred"));

            var controller = new TodoItemsController(_mockLogger.Object, _mockService.Object);

            var expected = (int)HttpStatusCode.InternalServerError;
            var actual = await controller.GetTodoItems() as StatusCodeResult;

            _mockService.Verify(m => m.GetItems(), Times.Once);
            Assert.Equal(expected, actual.StatusCode);
            _mockLogger.VerifyLog(m => m.LogError(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void GetTodoItem_Valid_ReturnsItem()
        {
            _mockService.Setup(m => m.GetItem(It.IsAny<Guid>()))
                .ReturnsAsync(new TodoItemDTO());

            var controller = new TodoItemsController(_mockLogger.Object, _mockService.Object);

            var expected = new OkObjectResult(new TodoItemDTO());
            var actual = await controller.GetTodoItem(It.IsAny<Guid>()) as ObjectResult;

            _mockService.Verify(m => m.GetItem(It.IsAny<Guid>()), Times.Once);
            Assert.Equal(expected.StatusCode, actual.StatusCode);
        }

        [Fact]
        public async void GetTodoItem_Valid_ReturnsNull()
        {
            _mockService.Setup(m => m.GetItem(It.IsAny<Guid>()))
                .ReturnsAsync(It.IsAny<TodoItemDTO>());

            var controller = new TodoItemsController(_mockLogger.Object, _mockService.Object);

            var expected = (int)HttpStatusCode.BadRequest;
            var actual = await controller.GetTodoItem(It.IsAny<Guid>()) as StatusCodeResult;

            _mockService.Verify(m => m.GetItem(It.IsAny<Guid>()), Times.Once);
            Assert.Equal(expected, actual.StatusCode);
        }

        [Fact]
        public async void GetTodoItem_LogsAndThrowsException()
        {
            _mockService.Setup(m => m.GetItem(It.IsAny<Guid>()))
                .Throws(new Exception("An error has occurred"));

            var controller = new TodoItemsController(_mockLogger.Object, _mockService.Object);

            var expected = (int)HttpStatusCode.InternalServerError;
            var actual = await controller.GetTodoItem(It.IsAny<Guid>()) as StatusCodeResult;

            _mockService.Verify(m => m.GetItem(It.IsAny<Guid>()), Times.Once);
            Assert.Equal(expected, actual.StatusCode);
            _mockLogger.VerifyLog(m => m.LogError(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void UpdateTodoItem_Valid_ReturnsNoContent()
        {
            _mockService.Setup(m => m.UpdateItem(It.IsAny<UpdateTodoItemRequest>(), It.IsAny<Guid>()));

            var controller = new TodoItemsController(_mockLogger.Object, _mockService.Object);

            var expected = new StatusCodeResult((int)HttpStatusCode.NoContent);
            var actual = await controller.UpdateTodoItem(It.IsAny<Guid>(), It.IsAny<UpdateTodoItemRequest>()) as StatusCodeResult;

            _mockService.Verify(m => m.UpdateItem(It.IsAny<UpdateTodoItemRequest>(), It.IsAny<Guid>()), Times.Once);
            Assert.Equal(expected.StatusCode, actual.StatusCode);
        }

        [Fact]
        public async void UpdateTodoItem_Error_LogsAndThrowsBadRequest()
        {
            _mockService.Setup(m => m.UpdateItem(It.IsAny<UpdateTodoItemRequest>(), It.IsAny<Guid>()))
                .Throws(new KeyNotFoundException("Key not found exception"));

            var controller = new TodoItemsController(_mockLogger.Object, _mockService.Object);

            var expected = (int)HttpStatusCode.BadRequest;
            var actual = await controller.UpdateTodoItem(It.IsAny<Guid>(), It.IsAny<UpdateTodoItemRequest>()) as StatusCodeResult;

            _mockService.Verify(m => m.UpdateItem(It.IsAny<UpdateTodoItemRequest>(), It.IsAny<Guid>()), Times.Once);
            Assert.Equal(expected, actual.StatusCode);
            _mockLogger.VerifyLog(m => m.LogWarning(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void UpdateTodoItem_Error_LogsAndThrowsException()
        {
            _mockService.Setup(m => m.UpdateItem(It.IsAny<UpdateTodoItemRequest>(), It.IsAny<Guid>()))
                .Throws(new Exception("An error has occurred"));

            var controller = new TodoItemsController(_mockLogger.Object, _mockService.Object);

            var expected = (int)HttpStatusCode.InternalServerError;
            var actual = await controller.UpdateTodoItem(It.IsAny<Guid>(), It.IsAny<UpdateTodoItemRequest>()) as StatusCodeResult;

            _mockService.Verify(m => m.UpdateItem(It.IsAny<UpdateTodoItemRequest>(), It.IsAny<Guid>()), Times.Once);
            Assert.Equal(expected, actual.StatusCode);
            _mockLogger.VerifyLog(m => m.LogError(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void CreateTodoItem_Valid_ReturnsNoContent()
        {
            _mockService.Setup(m => m.CreateItem(It.IsAny<CreateTodoItemRequest>()));

            var controller = new TodoItemsController(_mockLogger.Object, _mockService.Object);

            var expected = new StatusCodeResult((int)HttpStatusCode.NoContent);
            var actual = await controller.CreateTodoItem(It.IsAny<CreateTodoItemRequest>()) as StatusCodeResult;

            _mockService.Verify(m => m.CreateItem(It.IsAny<CreateTodoItemRequest>()), Times.Once);
            Assert.Equal(expected.StatusCode, actual.StatusCode);
        }

        [Fact]
        public async void CreateTodoItem_Error_LogsAndThrowsBadRequest()
        {
            _mockService.Setup(m => m.CreateItem(It.IsAny<CreateTodoItemRequest>()))
                .Throws(new BadHttpRequestException("Bad Request exception"));

            var controller = new TodoItemsController(_mockLogger.Object, _mockService.Object);

            var expected = (int)HttpStatusCode.BadRequest;
            var actual = await controller.CreateTodoItem(It.IsAny<CreateTodoItemRequest>()) as StatusCodeResult;

            _mockService.Verify(m => m.CreateItem(It.IsAny<CreateTodoItemRequest>()), Times.Once);
            Assert.Equal(expected, actual.StatusCode);
            _mockLogger.VerifyLog(m => m.LogWarning(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void CreateTodoItem_Error_LogsAndThrowsException()
        {
            _mockService.Setup(m => m.CreateItem(It.IsAny<CreateTodoItemRequest>()))
                .Throws(new Exception("An error has occurred"));

            var controller = new TodoItemsController(_mockLogger.Object, _mockService.Object);

            var expected = (int)HttpStatusCode.InternalServerError;
            var actual = await controller.CreateTodoItem(It.IsAny<CreateTodoItemRequest>()) as StatusCodeResult;

            _mockService.Verify(m => m.CreateItem(It.IsAny<CreateTodoItemRequest>()), Times.Once);
            Assert.Equal(expected, actual.StatusCode);
            _mockLogger.VerifyLog(m => m.LogError(It.IsAny<string>()), Times.Once);
        }
    }
}
