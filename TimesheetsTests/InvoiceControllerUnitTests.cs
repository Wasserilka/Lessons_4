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
    public class InvoiceControllerUnitTests
    {
        private Mock<IInvoiceRepository> mockInvoiceRepository;
        private InvoiceController controller;
        private IMapper mapper;
        private Mock<IGetInvoiceByIdValidator> mockGetInvoiceByIdValidator;
        private Mock<ICreateInvoiceValidator> mockCreateInvoiceValidator;
        private Mock<IDeleteInvoiceValidator> mockDeleteInvoiceValidator;

        public InvoiceControllerUnitTests()
        {
            mockInvoiceRepository = new Mock<IInvoiceRepository>();
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            mapper = mapperConfiguration.CreateMapper();
            mockGetInvoiceByIdValidator = new Mock<IGetInvoiceByIdValidator>();
            mockCreateInvoiceValidator = new Mock<ICreateInvoiceValidator>();
            mockDeleteInvoiceValidator = new Mock<IDeleteInvoiceValidator>();


            controller = new InvoiceController(
                mockInvoiceRepository.Object,
                mapper,
                mockGetInvoiceByIdValidator.Object,
                mockCreateInvoiceValidator.Object,
                mockDeleteInvoiceValidator.Object);
        }

        [Fact]
        public void CreateInvoice_DoOnceTest()
        {
            var result = controller.CreateInvoice(1, 1);

            mockInvoiceRepository.Verify(repository => repository.Create(
                It.IsAny<CreateInvoiceRequest>()), 
                Times.AtMostOnce);
        }

        [Fact]
        public void DeleteInvoice_DoOnceTest()
        {
            var result = controller.DeleteInvoice(1);

            mockInvoiceRepository.Verify(repository => repository.Delete(
                It.IsAny<DeleteInvoiceRequest>()), 
                Times.AtMostOnce);
        }

        [Fact]
        public void GetAllInvoices_GetListTest()
        {
            mockInvoiceRepository.Setup(repository => repository.GetAll())
                .ReturnsAsync(GetInvoicesListForTest());

            var result = controller.GetAll().Result;

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<GetAllInvoicesResponse>(viewResult.Value);
            Assert.Equal(GetInvoicesListForTest().Count, model.Invoices.Count);
        }

        [Fact]
        public void GetByIdInvoice_GetModelTest()
        {
            mockInvoiceRepository.Setup(repository => repository.GetById(It.IsAny<GetInvoiceByIdRequest>()))
                .ReturnsAsync(GetInvoiceForTest());

            var result = controller.GetInvoiceById(1).Result;

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<GetInvoiceByIdResponse>(viewResult.Value);
            Assert.Equal(mapper.Map<InvoiceDto>(GetInvoiceForTest()).Id, model.Invoice.Id);
        }

        private List<Invoice> GetInvoicesListForTest()
        {
            var factory = new InvoiceFactory();
            var list = new List<Invoice>
            {
                factory.Create(1, It.IsAny<Contract>(), 300),
                factory.Create(2, It.IsAny<Contract>(), 100),
                factory.Create(3, It.IsAny<Contract>(), 200),
                factory.Create(4, It.IsAny<Contract>(), 500)
            };
            return list;
        }

        private Invoice GetInvoiceForTest()
        {
            var factory = new InvoiceFactory();
            var contract = factory.Create(1, It.IsAny<Contract>(), 300);
            return contract;
        }
    }
}
