using BankApp.Core.Interfaces;
using BankApp.Data.DTO;
using BankApp.Data.Entities;
using BankApp.Data.Interfaces;

namespace BankApp.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IAccountService _accountService;

        public CustomerService(
            ICustomerRepo customerRepo,
            IAccountService accountService)
        {
            _customerRepo = customerRepo;
            _accountService = accountService;
        }

        public async Task<int> CreateCustomer(CreateCustomerDTO dto)
        {
            var customer = new Customer
            {
                Gender = dto.Gender,
                Givenname = dto.Givenname,
                Surname = dto.Surname,
                Streetaddress = dto.Streetaddress,
                City = dto.City,
                Zipcode = dto.Zipcode,
                Country = dto.Country,
                CountryCode = dto.CountryCode,
                Birthday = dto.Birthday,
                Telephonecountrycode = dto.Telephonecountrycode,
                Telephonenumber = dto.Telephonenumber,
                Emailaddress = dto.Emailaddress
            };
            await _customerRepo.AddAsync(customer);

            await _accountService.CreateAccount(
                customer.CustomerId,
                dto.AccountTypeId
            );

            return customer.CustomerId;

        }
    }
}
