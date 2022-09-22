using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoList.Api.Models;
using TodoList.Api.Repositories;
using TodoList.Api.Services;
using Xunit;

namespace TodoList.Api.UnitTests
{
    public class TodoItemsServiceTests
    {
        protected readonly Mock<ILogger<TodoItemsService>> _mockLogger;
        protected readonly Mock<IMapper> _mockMapper;
        protected readonly Mock<ITodoItemsRepository> _mockRepo;

        public TodoItemsServiceTests()
        {
            _mockLogger = new Mock<ILogger<TodoItemsService>>();
            _mockMapper = new Mock<IMapper>();
            _mockRepo = new Mock<ITodoItemsRepository>();
        }

        [Fact]
        public async void GetItems_Valid_ReturnsItemDTOs()
        {
            _mockRepo.Setup(m => m.GetItems())
                .ReturnsAsync(GetMockTodoItems());
            _mockMapper.Setup(m => m.Map<TodoItemsDTO>(It.IsAny<IEnumerable<TodoItem>>()))
                .Returns(new TodoItemsDTO()
                {
                    Items = GetMockTodoItemDTOs()
                });

            var service = new TodoItemsService(_mockLogger.Object, _mockMapper.Object, _mockRepo.Object);

            var expected = new TodoItemsDTO()
            {
                Items = GetMockTodoItemDTOs()
            };
            var actual = await service.GetItems();

            var expectedItems = expected.Items.ToList();
            var actualItems = actual.Items.ToList();
            for (int i = 0; i < expectedItems.Count(); i++)
            {
                Assert.Equal(expectedItems[i].Id, actualItems[i].Id);
                Assert.Equal(expectedItems[i].Description, actualItems[i].Description);
                Assert.Equal(expectedItems[i].IsCompleted, actualItems[i].IsCompleted);
            }

            _mockMapper.Verify(m => m.Map<TodoItemsDTO>(It.IsAny<IEnumerable<TodoItem>>()), Times.Once);
            _mockRepo.Verify(m => m.GetItems(), Times.Once);
        }

        [Fact]
        public async void GetItem_Valid_ReturnsItemDTO()
        {
            _mockRepo.Setup(m => m.GetItem(It.IsAny<Guid>()))
                .ReturnsAsync(GetMockTodoItems().First());
            _mockMapper.Setup(m => m.Map<TodoItemDTO>(It.IsAny<TodoItem>()))
                .Returns(GetMockTodoItemDTOs().First());

            var service = new TodoItemsService(_mockLogger.Object, _mockMapper.Object, _mockRepo.Object);

            var expected = GetMockTodoItemDTOs().First();
            var actual = await service.GetItem(It.IsAny<Guid>());

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.IsCompleted, actual.IsCompleted);

            _mockMapper.Verify(m => m.Map<TodoItemDTO>(It.IsAny<TodoItem>()), Times.Once);
            _mockRepo.Verify(m => m.GetItem(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void UpdateItem_Valid()
        {
            _mockRepo.Setup(m => m.UpdateItem(It.IsAny<UpdateTodoItemRequest>(), It.IsAny<Guid>()));

            var service = new TodoItemsService(_mockLogger.Object, _mockMapper.Object, _mockRepo.Object);

            await service.UpdateItem(It.IsAny<UpdateTodoItemRequest>(), It.IsAny<Guid>());

            _mockRepo.Verify(m => m.UpdateItem(It.IsAny<UpdateTodoItemRequest>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void CreateItem_Valid()
        {
            _mockRepo.Setup(m => m.CreateItem(It.IsAny<CreateTodoItemRequest>()));

            var service = new TodoItemsService(_mockLogger.Object, _mockMapper.Object, _mockRepo.Object);

            await service.CreateItem(It.IsAny<CreateTodoItemRequest>());

            _mockRepo.Verify(m => m.CreateItem(It.IsAny<CreateTodoItemRequest>()), Times.Once);
        }

        private IEnumerable<TodoItem> GetMockTodoItems()
        {
            return new List<TodoItem>
            {
                new TodoItem
                {
                    Id = new Guid("8F2E9176-35EE-4F0A-AE55-83023D2DB1A3"),
                    Description = "Test 1",
                    IsCompleted = false
                },
                new TodoItem
                {
                    Id = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3"),
                    Description = "Todo item 2",
                    IsCompleted = false
                },
                new TodoItem
                {
                    Id = new Guid("73EE57E2-7862-430D-9F18-8D289A599846"),
                    Description = "Sams tester description",
                    IsCompleted = true
                }
            };
        }

        private IEnumerable<TodoItemDTO> GetMockTodoItemDTOs()
        {
            return new List<TodoItemDTO>
            {
                new TodoItemDTO
                {
                    Id = new Guid("8F2E9176-35EE-4F0A-AE55-83023D2DB1A3"),
                    Description = "Test 1",
                    IsCompleted = false
                },
                new TodoItemDTO
                {
                    Id = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3"),
                    Description = "Todo item 2",
                    IsCompleted = false
                },
                new TodoItemDTO
                {
                    Id = new Guid("73EE57E2-7862-430D-9F18-8D289A599846"),
                    Description = "Sams tester description",
                    IsCompleted = true
                }
            };
        }
    }
}
