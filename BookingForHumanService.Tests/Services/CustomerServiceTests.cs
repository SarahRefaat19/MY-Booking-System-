using AutoMapper;
using BookingForHumanService.Application.DTOs.CustomerDtos;
using BookingForHumanService.Application.Interfaces;
using BookingForHumanService.Application.Services;
using BookingForHumanService.Domain.Entities;
using BookingForHumanService.Domain.Enums;
using BookingForHumanService.Domain.Interfaces;
using BookingForHumanService.Domain.ValueObjects;
using BookingForHumanService.Domain.ValueObjects.CustomerValueObjects;
using BookingForHumanService.Infrastructure.Repositories;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;


namespace BookingForHumanService.Tests.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitofwork;
        private readonly CustomerService _customerService;
        private readonly Mock<IMapper> _mappermock;


        public CustomerServiceTests()
        {
            _mockUnitofwork = new Mock<IUnitOfWork>();
            _mappermock = new Mock<IMapper>();
            _customerService = new CustomerService(_mockUnitofwork.Object, _mappermock.Object);

        }
        [Fact]

        public async Task CreateCustomer_ShouldReturnCustomerReadDto()
        {
            //Arrange
            var customerAddDto = new CustomerAddDto
            {
                Name = "Sarah",
                Email = "sararefaat@gmail.com",
                Phone = "0112233584"
            };

            var name = CustomerName.Create("Sarah");
            var email = CustomerEmail.Create("sararefaat@gmail.com");

            var customer = new Customer(

            user: new User { Id = 1 },
            name,
            email,
            phone: new CustomerPhone("0112233584"),
            status: CustomerStatus.Active
           );


            var mockrepo = new Mock<ICustomerRepository>();

            _mockUnitofwork.Setup(c => c.Customers).Returns(mockrepo.Object);


            mockrepo.Setup(u => u.AddAsync(It.IsAny<Customer>())).ReturnsAsync(customer);
            _mappermock.Setup(m => m.Map<Customer>(customerAddDto))
              .Returns(customer);


            var customerreaddto = new CustomerReadDto();

            _mappermock.Setup(m => m.Map<CustomerReadDto>(customer)).Returns(customerreaddto);


            // Act 


            var result = await _customerService.CreateCustomerAsync(customerAddDto);
            _mockUnitofwork.Setup(b => b.SaveChangesAsync()).ReturnsAsync(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(customerreaddto, result);


            mockrepo.Verify(c => c.AddAsync(It.IsAny<Customer>()), Times.Once);
            _mockUnitofwork.Verify(o => o.SaveChangesAsync(), Times.Once);

        }





        //-----------------------------------------------------------


        [Fact]

        public async Task GetCustomerByIdAsync_WhenIdExists_ShouldReturnCustomerReadDto()
        {
            //Arrange

            var someUser = new User { Id = 1 };
            var name = CustomerName.Create("Sarah");
            var email = CustomerEmail.Create("sararefaat@gmail.com");

            var customer = new Customer(
           user: someUser,
           name: name,
           email: email,
           phone: new CustomerPhone("0100122335"),
           status: CustomerStatus.Active);


            var mockrepo = new Mock<ICustomerRepository>();

            _mockUnitofwork.Setup(c => c.Customers).Returns(mockrepo.Object);


            mockrepo.Setup(u => u.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var customerreaddto = new CustomerReadDto();

            _mappermock.Setup(m => m.Map<CustomerReadDto>(customer)).Returns(customerreaddto);


            // Act 


            var result = await _customerService.GetCustomerByIdAsync(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(customerreaddto, result);


        }

        [Fact]


        public async Task GetCustomerByIdAsync_WhencustomerNotFound_ShouldthrowException()
        {
            //Arrange

            var mockrepos = new Mock<ICustomerRepository>();
            _mockUnitofwork.Setup(c => c.Customers).Returns(mockrepos.Object);

            mockrepos.Setup(c => c.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer?)null);




            //Act  & Assert


            await Assert.ThrowsAsync<Exception>(() => _customerService.GetCustomerByIdAsync(1));



        }

        [Fact]
        public async Task GetCustomerByIdAsync_WhencustomerNull_ShouldthrowException()
        {
            //Arrange

            var mockrepos = new Mock<ICustomerRepository>();
            _mockUnitofwork.Setup(c => c.Customers).Returns(mockrepos.Object);

            mockrepos.Setup(c => c.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer?)null);

            //Act  & Assert


            await Assert.ThrowsAsync<Exception>(() => _customerService.GetCustomerByIdAsync(1));


        }


        //------------------------
        [Fact]
        //Arrange
        public async Task UpdateCustomer_ShouldReturnUpdatedCustomer()
        {
            //mockData from Request
          var customerupdatedto = new CustomerUpdateDto { Id = 1,Name ="soo",Email ="sararefaa653@gmail.com",Phone ="0172626333" };

            //mockReal Data in System
            var name = CustomerName.Create("soo");
            var email = CustomerEmail.Create("sararefaat653@gmail.com");

            var customer = new Customer
          (
            user: new User { Id = 1 },
            name,
            email,
            phone: new CustomerPhone("0112233584"),
            status: CustomerStatus.Active
          );
            //makemockrepo
            var mockrepos = new Mock<ICustomerRepository>();
            // get cutomerfromuow
            _mockUnitofwork.Setup(x => x.Customers).Returns(mockrepos.Object);

            //GetUser
            mockrepos.Setup(o => o.GetByIdAsync(1)).ReturnsAsync(customer);
            //mapping

            _mappermock.Setup(o => o.Map(customerupdatedto, customer)).Callback(() => { customer.ChangeName(CustomerName.Create("sooo")); customer.ChangeName(CustomerEmail.Create("sararaeafaa")); });
            //Update
            var customerreaddto = new CustomerReadDto
            {
                Name = "soo",
                Email = "sararefaa653@gmail.com",
                Phone = "0172626333"
            };
            _mappermock.Setup(o => o.Map<CustomerReadDto>(customer)).Returns(customerreaddto);

            //ACT 
            var result = await _customerService.UpdateCustomerAsync(1, customerupdatedto);
            //Assert
            result.Should().NotBeNull();

            // 2. القيم اتحدثت فعلاً
            result.Name.Should().Be("soo");
            result.Email.Should().Be("sararefaa653@gmail.com");
            result.Phone.Should().Be("0172626333");

            // 3. التأكد إننا جبنا العميل من الداتا
            mockrepos.Verify(x => x.GetByIdAsync(1), Times.Once);

        }

    }
    }

