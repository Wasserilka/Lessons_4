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
using Timesheets;

namespace TimesheetsTests
{
    public class CustomerControllerUnitTests
    {
        private Mock<IContractRepository> mockContractRepository;
        private Mock<ICustomerRepository> mockCustomerRepository;
        private CustomerController controller;
        private IMapper mapper;
        private Mock<IGetContractByIdValidator> mockGetContractByIdValidator;
        private Mock<IGetAllContractsValidator> mockGetAllContractsValidator;
        private Mock<ICreateContractValidator> mockCreateContractValidator;
        private Mock<IDeleteContractValidator> mockDeleteContractValidator;
        private Mock<IGetCustomerByIdValidator> mockGetCustomerByIdValidator;
        private Mock<ICreateCustomerValidator> mockCreateCustomerValidator;
        private Mock<IDeleteCustomerValidator> mockDeleteCustomerValidator;

        public CustomerControllerUnitTests()
        {
            mockContractRepository = new Mock<IContractRepository>();
            mockCustomerRepository = new Mock<ICustomerRepository>();
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            mapper = mapperConfiguration.CreateMapper();
            mockGetContractByIdValidator = new Mock<IGetContractByIdValidator>();
            mockGetAllContractsValidator = new Mock<IGetAllContractsValidator>();
            mockCreateContractValidator = new Mock<ICreateContractValidator>();
            mockDeleteContractValidator = new Mock<IDeleteContractValidator>();
            mockGetCustomerByIdValidator = new Mock<IGetCustomerByIdValidator>();
            mockCreateCustomerValidator = new Mock<ICreateCustomerValidator>();
            mockDeleteCustomerValidator = new Mock<IDeleteCustomerValidator>();

            controller = new CustomerController(
                mockCustomerRepository.Object,
                mockContractRepository.Object,
                mapper,
                mockGetContractByIdValidator.Object,
                mockGetAllContractsValidator.Object,
                mockCreateContractValidator.Object,
                mockDeleteContractValidator.Object,
                mockGetCustomerByIdValidator.Object,
                mockCreateCustomerValidator.Object,
                mockDeleteCustomerValidator.Object);
        }

        [Fact]
        public void CreateContract_DoOnceTest()
        {
            var result = controller.CreateContract(1, "contract");

            mockContractRepository.Verify(repository => repository.Create(
                It.IsAny<CreateContractRequest>()), 
                Times.AtMostOnce);
        }

        [Fact]
        public void DeleteContract_DoOnceTest()
        {
            var result = controller.DeleteContract(1, 1);

            mockContractRepository.Verify(repository => repository.Delete(
                It.IsAny<DeleteContractRequest>()), 
                Times.AtMostOnce);
        }

        [Fact]
        public void GetAllContracts_GetListTest()
        {
            mockContractRepository.Setup(repository => repository.GetAll(It.IsAny<GetAllContractsRequest>()))
                .ReturnsAsync(GetContractListForTest());

            var result = controller.GetAllContracts(1).Result;

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<GetAllContractsResponse>(viewResult.Value);
            Assert.Equal(GetContractListForTest().Count, model.Contracts.Count);
        }

        [Theory]
        [InlineData("contract_1")]
        [InlineData("contract_2")]
        [InlineData("contract_3")]
        public void GetAllContracts_IsContainTest(string name)
        {
            mockContractRepository.Setup(repository => repository.GetAll(It.IsAny<GetAllContractsRequest>()))
                .ReturnsAsync(GetContractListForTest());

            var result = controller.GetAllContracts(1).Result;

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<GetAllContractsResponse>(viewResult.Value);
            Assert.Contains(name, GetNameListFromContractListForTest(model.Contracts));
        }

        [Fact]
        public void GetByIdContract_GetModelTest()
        {
            mockContractRepository.Setup(repository => repository.GetById(It.IsAny<GetContractByIdRequest>()))
                .ReturnsAsync(GetContractForTest());

            var result = controller.GetContractById(1, 1).Result;

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<GetContractByIdResponse>(viewResult.Value);
            Assert.Equal(mapper.Map<ContractDto>(GetContractForTest()).Id, model.Contract.Id);
        }

        [Fact]
        public void CreateCustomer_DoOnceTest()
        {
            var result = controller.Create("customer");

            mockCustomerRepository.Verify(repository => repository.Create(
                It.IsAny<CreateCustomerRequest>()),
                Times.AtMostOnce);
        }

        [Fact]
        public void DeleteCustomer_DoOnceTest()
        {
            var result = controller.Delete(1);

            mockCustomerRepository.Verify(repository => repository.Delete(
                It.IsAny<DeleteCustomerRequest>()),
                Times.AtMostOnce);
        }

        [Fact]
        public void GetAllCustomers_GetListTest()
        {
            mockCustomerRepository.Setup(repository => repository.GetAll())
                .ReturnsAsync(GetCustomerListForTest());

            var result = controller.GetAll().Result;

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<GetAllCustomersResponse>(viewResult.Value);
            Assert.Equal(GetCustomerListForTest().Count, model.Customers.Count);
        }

        [Fact]
        public void GetByIdCustomer_GetModelTest()
        {
            mockCustomerRepository.Setup(repository => repository.GetById(It.IsAny<GetCustomerByIdRequest>()))
                .ReturnsAsync(GetCustomerForTest());

            var result = controller.GetById(1).Result;

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<GetCustomerByIdResponse>(viewResult.Value);
            Assert.Equal(mapper.Map<CustomerDto>(GetCustomerForTest()).Id, model.Customer.Id);
        }

        private List<Contract> GetContractListForTest()
        {
            var factory = new ContractFactory();
            var list = new List<Contract>
            {
                factory.Create(1, "contract_1", 1),
                factory.Create(2, "contract_2", 1),
                factory.Create(3, "contract_3", 2),
                factory.Create(4, "contract_4", 3)
            };
            return list;
        }

        private Contract GetContractForTest()
        {
            var factory = new ContractFactory();
            var contract = factory.Create(1, "contract_1", 1);
            return contract;
        }

        private List<Customer> GetCustomerListForTest()
        {
            var factory = new CustomerFactory();
            var list = new List<Customer>
            {
                factory.Create(1, "customer_1"),
                factory.Create(2, "customer_2"),
                factory.Create(3, "customer_3"),
                factory.Create(4, "customer_4")
            };
            return list;
        }

        private Customer GetCustomerForTest()
        {
            var factory = new CustomerFactory();
            var customer = factory.Create(1, "customer_1");
            return customer;
        }

        private List<string> GetNameListFromContractListForTest(List<ContractDto> contracts)
        {
            var list = new List<string>();
            foreach (ContractDto contract in contracts)
            {
                list.Add(contract.Name);
            }
            return list;
        }
    }
}
