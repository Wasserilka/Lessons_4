using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using Timesheets.Repositories;
using Timesheets.Requests;
using Timesheets.Models;
using Timesheets.Controllers;
using AutoMapper;
using Timesheets.Validation.Requests;
using Timesheets.Responses;
using System;
using Timesheets;

namespace TimesheetsTests
{
    public class EmployeeControllerUnitTests
    {
        private Mock<IEmployeeRepository> mockEmployeeRepository;
        private Mock<ITaskRepository> mockTaskRepository;
        private EmployeeController controller;
        private Mock<IGetEmployeeByIdValidator> mockGetEmployeeByIdValidator;
        private Mock<ICreateEmployeeValidator> mockCreateEmployeeValidator;
        private Mock<IDeleteEmployeeValidator> mockDeleteEmployeeValidator;
        private Mock<IGetTaskByIdValidator> mockGetTaskByIdValidator;
        private Mock<ICreateTaskValidator> mockCreateTaskValidator;
        private Mock<IDeleteTaskValidator> mockDeleteTaskValidator;
        private Mock<ICloseTaskValidator> mockCloseTaskValidator;
        private Mock<IAddEmployeeToTaskValidator> mockAddEmployeeToTaskValidator;
        private Mock<IRemoveEmployeeFromTaskValidator> mockRemoveEmployeeFromTaskValidator;
        private IMapper mapper;

        public EmployeeControllerUnitTests()
        {
            mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockTaskRepository = new Mock<ITaskRepository>();
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            mapper = mapperConfiguration.CreateMapper();
            mockGetEmployeeByIdValidator = new Mock<IGetEmployeeByIdValidator>();
            mockCreateEmployeeValidator = new Mock<ICreateEmployeeValidator>();
            mockDeleteEmployeeValidator = new Mock<IDeleteEmployeeValidator>();
            mockGetTaskByIdValidator = new Mock<IGetTaskByIdValidator>();
            mockCreateTaskValidator = new Mock<ICreateTaskValidator>();
            mockDeleteTaskValidator = new Mock<IDeleteTaskValidator>();
            mockCloseTaskValidator = new Mock<ICloseTaskValidator>();
            mockAddEmployeeToTaskValidator = new Mock<IAddEmployeeToTaskValidator>();
            mockRemoveEmployeeFromTaskValidator = new Mock<IRemoveEmployeeFromTaskValidator>();

            controller = new EmployeeController(
                mockEmployeeRepository.Object,
                mockTaskRepository.Object,
                mapper,
                mockGetEmployeeByIdValidator.Object,
                mockCreateEmployeeValidator.Object,
                mockDeleteEmployeeValidator.Object,
                mockGetTaskByIdValidator.Object,
                mockCreateTaskValidator.Object,
                mockDeleteTaskValidator.Object,
                mockCloseTaskValidator.Object,
                mockAddEmployeeToTaskValidator.Object,
                mockRemoveEmployeeFromTaskValidator.Object);
        }

        [Fact]
        public void CreateEmployee_DoOnceTest()
        {
            var result = controller.Create("employee");

            mockEmployeeRepository.Verify(repository => repository.Create(
                It.IsAny<CreateEmployeeRequest>()), 
                Times.AtMostOnce);
        }

        [Fact]
        public void DeleteEmployee_DoOnceTest()
        {
            var result = controller.Delete(1);

            mockEmployeeRepository.Verify(repository => repository.Delete(
                It.IsAny<DeleteEmployeeRequest>()), 
                Times.AtMostOnce);
        }

        [Fact]
        public void GetAllEmployees_GetListTest()
        {
            mockEmployeeRepository.Setup(repository => repository.GetAll())
                .ReturnsAsync(GetEmployeeListForTest());

            var result = controller.GetAll().Result;

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<GetAllEmployeesResponse>(viewResult.Value);
            Assert.Equal(GetEmployeeListForTest().Count, model.Employees.Count);
        }

        [Fact]
        public void GetByIdEmployee_GetModelTest()
        {
            mockEmployeeRepository.Setup(repository => repository.GetById(It.IsAny<GetEmployeeByIdRequest>()))
                .ReturnsAsync(GetEmployeeForTest());

            var result = controller.GetById(1).Result;

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<GetEmployeeByIdResponse>(viewResult.Value);
            Assert.Equal(mapper.Map<EmployeeDto>(GetEmployeeForTest()).Id, model.Employee.Id);
        }

        [Fact]
        public void CreateTask_DoOnceTest()
        {
            var result = controller.CreateTask(300);

            mockTaskRepository.Verify(repository => repository.Create(
                It.IsAny<CreateTaskRequest>()),
                Times.AtMostOnce);
        }

        [Fact]
        public void DeleteTask_DoOnceTest()
        {
            var result = controller.DeleteTask(1);

            mockTaskRepository.Verify(repository => repository.Delete(
                It.IsAny<DeleteTaskRequest>()),
                Times.AtMostOnce);
        }

        [Fact]
        public void GetAllTasks_GetListTest()
        {
            var time = DateTime.UtcNow;
            mockTaskRepository.Setup(repository => repository.GetAll())
                .ReturnsAsync(GetTaskListForTest(time));

            var result = controller.GetAllTasks().Result;

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<GetAllTasksResponse>(viewResult.Value);
            Assert.Equal(GetTaskListForTest(time).Count, model.Tasks.Count);
            Assert.Equal(model.Tasks[0].Start, time);
        }

        [Fact]
        public void GetByIdTask_GetModelTest()
        {
            var time = DateTime.UtcNow;
            mockTaskRepository.Setup(repository => repository.GetById(It.IsAny<GetTaskByIdRequest>()))
                .ReturnsAsync(GetTaskForTest(time));

            var result = controller.GetTaskById(1).Result;

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<GetTaskByIdResponse>(viewResult.Value);
            Assert.Equal(mapper.Map<TaskDto>(GetTaskForTest(time)).Id, model.Task.Id);
            Assert.Equal(model.Task.Start, time);
        }

        [Fact]
        public void CloseTask_DoOnceTest()
        {
            var result = controller.CloseTask(1);

            mockTaskRepository.Verify(repository => repository.CloseTask(
                It.IsAny<CloseTaskRequest>()),
                Times.AtMostOnce);
        }

        [Fact]
        public void AddEmployeeToTask_DoOnceTest()
        {
            var result = controller.AddEmployeeToTask(1, 1);

            mockTaskRepository.Verify(repository => repository.AddEmployeeToTask(
                It.IsAny<AddEmployeeToTaskRequest>()),
                Times.AtMostOnce);
        }

        [Fact]
        public void RemoveEmployeeFromTask_DoOnceTest()
        {
            var result = controller.RemoveEmployeeFromTask(1, 1);

            mockTaskRepository.Verify(repository => repository.RemoveEmployeeFromTask(
                It.IsAny<RemoveEmployeeFromTaskRequest>()),
                Times.AtMostOnce);
        }

        private List<Employee> GetEmployeeListForTest()
        {
            var factory = new EmployeeFactory();
            var list = new List<Employee>
            {
                factory.Create(1, "employee_1"),
                factory.Create(2, "employee_2"),
                factory.Create(3, "employee_3"),
                factory.Create(4, "employee_4")
            };
            return list;
        }

        private Employee GetEmployeeForTest()
        {
            var factory = new EmployeeFactory();
            var employee = factory.Create(1, "employee_1");
            return employee;
        }

        private List<Task> GetTaskListForTest(DateTime time)
        {
            var factory = new TaskFactory();
            var list = new List<Task>
            {
                factory.Create(1, 300, new TimeForTests(time)),
                factory.Create(2, 100, new TimeForTests(time)),
                factory.Create(3, 200, new TimeForTests(time)),
                factory.Create(4, 500, new TimeForTests(time))
            };
            return list;
        }

        private Task GetTaskForTest(DateTime time)
        {
            var factory = new TaskFactory();
            var task = factory.Create(1, 300, new TimeForTests(time));
            return task;
        }
    }
}
