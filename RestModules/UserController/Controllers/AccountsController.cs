using UserController.Models.Users;
using AuctionHub.Data.Mssql.Dao.Interfaces;
using AuctionHub.Data.Mssql.Dto;
using Microsoft.AspNetCore.Mvc;
using SrvCornet.Http;
using SrvCornet.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SrvCornet.Http.Exceptions;
using AuctionHub.Common;
using AuctionHub.Common.Messages;

namespace UserController.Controllers
{
    [Route("admin/accounts")]
    public class AccountsController : BaseRestController
    {
        private readonly IAccountDao accountDao;
        public AccountsController(
            IAccountDao userAccountDao
        )
        {
            this.accountDao = userAccountDao;
        }
        [HttpPost]
        public async Task<AccountModel> Register([FromBody] AddAccountModel model)
        {
            ValidateInputModel();

            var accountFilter = new AccountFilter()
            {
                UserName = model.UserName
            };
            var accounts = await accountDao.Filter(accountFilter);
            if (accounts.Count > 0)
                throw new DomainValidateException(new List<string>() { ErrorMessages.REGISTER_EXIST_USERNAME });

            var passwordSalt = "-" + model.UserName + "-AuctionHub-" + model.UserName;
            string passwordHash = StringUtils.ComputeSha256Hash(model.Password + passwordSalt);

            var userDto = new Account()
            {
                UserName = model.UserName,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            var result = await accountDao.InsertAsync_EF(userDto);
            var userAccountResponse = GetFromAccount(userDto);

            return userAccountResponse;
        }

        [HttpGet]
        [Route("count")]
        public async Task<object> CountUser(AccountFilterModel filterModel)
        {
            var accountFilter = Mapper.Map<AccountFilter>(filterModel);
            var accounts = await accountDao.Filter(accountFilter);
            return new { Count = accounts.Count };
        }

        [HttpPost]
        [Route("login")]
        public async Task<AccountModel> Login([FromBody] LoginModel model)
        {
            ValidateInputModel();

            var accounts = await accountDao.GetForLogin(model.LoginName);
            if (accounts.Count == 0)
                throw new NotFoundException(ErrorMessages.LOGIN_INVALID_USER);
            var account = accounts[0];

            var passwordHash = StringUtils.ComputeSha256Hash(model.Password + account.PasswordSalt);
            if (passwordHash.Equals(account.PasswordHash))
                return GetFromAccount(account);
            else
                throw new AuthenticationException(ErrorMessages.LOGIN_FAILED);
        }

        private AccountModel GetFromAccount(Account account)
        {
            var accountModel = Mapper.Map<AccountModel>(account);
            return accountModel;
        }
    }
}
