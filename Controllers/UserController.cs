using BudgettingApp.Data;
using BudgettingApp.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgettingApp.Controllers
{

    //enpoint should be api/user
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Importing data context so we have our users to work with
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }


        //Getting our users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserEntity>>> GetUsers()
        {
            return await _context.UserEntities
                .Include(user => user.TransactionHistories)
                .ToListAsync();
            
            //return await _context.UserEntities.ToListAsync();
        }

        //will be getting a specific user by id
        [HttpGet("{username}")]
        public async Task<ActionResult<UserEntity>> GetUserById(string username)
        {


            var specificUser = await _context.UserEntities
                .Include(user => user.TransactionHistories)
                .SingleOrDefaultAsync(user => user.UserName.ToLower() == username.ToLower());

            if (specificUser == null) return NotFound("Specific user not found");

            return specificUser;
        }

        //api/user/add
        [HttpPost("add")]
        //when a user is added we want their username and amountStart
        public async Task<ActionResult<UserEntity>> AddUser(UserEntity user)
        {
            if (await UserExists(user.UserName)) return BadRequest("Username has already been taken");

            _context.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }


        //In this case we are not using a repository so we will get the user by their id and
        //then edit the user from there
        [HttpPut("{username}")]
        public async Task<ActionResult> UpdateUser(string username, UserEntity user)
        {

            //It will be similar to a post method
            //First find the specific user to update

            var userfound = await _context.UserEntities
                .Include(u => u.TransactionHistories)
                .FirstAsync(user => user.UserName.ToLower() == username.ToLower());

            if (userfound == null)
            {
                return NotFound("User is not found");
            } else
            {
                userfound.UserName = user.UserName;
                userfound.AmountStart = user.AmountStart;
                userfound.TransactionHistories = user.TransactionHistories;
                await _context.SaveChangesAsync();
                return Ok(userfound);
            }
        }

        [HttpDelete("{username}")]
        public async Task<ActionResult<UserEntity>> DeleteUser(string username)
        {
            var userToDelete = await _context.UserEntities
                .Include(userTrans => userTrans.TransactionHistories)
                .FirstAsync(user => user.UserName.ToLower() == username.ToLower());

            if (userToDelete == null) return NotFound("Sorry this is not a user!");

            _context.UserEntities.Remove(userToDelete);

            await _context.SaveChangesAsync();

            return NoContent();
        }


        //Adding transactions
        [HttpPost("transaction-add")]
        public TransactionHistory addTranasactions(TransactionHistory transaction)
        {
            var createdTransaction = new TransactionHistory();


            createdTransaction.AmountOfTransaction = transaction.AmountOfTransaction;
            createdTransaction.CreatedTransaction = transaction.CreatedTransaction;
            createdTransaction.TransactionDescription = transaction.TransactionDescription;
            createdTransaction.TransactionType = transaction.TransactionType;

            return createdTransaction;
        }


        //Returning a boolean value to see if a user is already created ijnside the db
        private async Task<bool> UserExists(string username)
        {
            //this will return either true or false if we have a user in the db with the same name
            return await _context.UserEntities
                .AnyAsync(doesUserExist => doesUserExist.UserName.ToLower() == username.ToLower());

        }
    }
}
