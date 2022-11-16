using AebTestProject.Controllers;
using AebTestProject.Services;
using AebTestProject.Models.Response;
using AutoMapper;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using AebTestProject.Models.Request;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
namespace AebTestProjectTests
{
    public class TaskControllerUnitTests
    {
        private TaskController _controller;
        private Mock<ITaskRepository> _mock;
        private Mock<IMapper> _mockMapper;
        public TaskControllerUnitTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mock = new Mock<ITaskRepository>();
            _controller = new TaskController(_mock.Object, _mockMapper.Object);
        }

        [Fact]
        public void ShouldCall_GetTasksList_From_Repository()
        {
            _mock.Setup(repository => repository.GetTasksList()).Returns(new List<AebTestProject.Models.Task>());
            var result = _controller.GetAll();
            _mock.Verify(repository => repository.GetTasksList());
        }

        [Fact]
        public void ShouldCall_Add_From_Repository()
        {
            CreateTask create = new CreateTask
            {
                CompleteBeforeDate = new DateTime(),
                CompletionDate = new DateTime(),
                Description = "义耱",
                Title = "123",
                Status = AebTestProject.Models.Status.Created

            };
            var result = _controller.Add(create);
            _mock.Verify(repository => repository.CreateTask(_mockMapper.Object.Map<AebTestProject.Models.Task>(create)));
        }

        [Fact]
        public void ShouldCall_GetTaskAsync_From_Repository()
        {
            CancellationToken cts = new CancellationToken();
            _mock.Setup(repository => repository.GetTaskAsync(new Guid(), cts)).Returns(Task.FromResult(new AebTestProject.Models.Task()));
            var result = _controller.GetTaskAsync(new Guid());
            _mock.Verify(repository => repository.GetTaskAsync(new Guid(), cts));
        }

        [Fact]
        public void ShouldCall_UpdateTask_From_Repository()
        {
            UpdateTask update = new UpdateTask
            {
                CompleteBeforeDate = new DateTime(),
                CompletionDate = new DateTime(),
                Description = "义耱",
                Title = "123",
                Status = AebTestProject.Models.Status.Created

            };
            CancellationToken cts = new CancellationToken();
            _mock.Setup(repository => repository.UpdateTask(_mockMapper.Object.Map<AebTestProject.Models.Task>(update)));
            var result = _controller.UpdateTask(new Guid(), update);
            _mock.Verify(repository => repository.UpdateTask(_mockMapper.Object.Map<AebTestProject.Models.Task>(update)));
        }

        [Fact]
        public void ShouldCall_GetTasksPage_From_Repository()
        {
            _mock.Setup(repository => repository.GetTasksPage(0,10)).Returns(new List<AebTestProject.Models.Task>());
            var result = _controller.GetTasksPage(0, 10);
            _mock.Verify(repository => repository.GetTasksPage(0, 10));
        }

        [Fact]
        public void ShouldCall_GetFilter_From_Repository()
        {
            GetTaskByFilter taskByFilter = new GetTaskByFilter
            {
                CompleteBeforeDate = new DateTime(),
                CompletionDate = new DateTime(),
                Title = "义耱"
            };
            _mock.Setup(repository => repository.GetFilter(new DateTime(), new DateTime(), "义耱")).Returns(new List<AebTestProject.Models.Task>());
            var result = _controller.GetFilterTask(taskByFilter);
            _mock.Verify(repository => repository.GetFilter(new DateTime(), new DateTime(), "义耱"));
        }
    }
}