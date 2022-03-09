using BudgettingApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgettingApp.Data
{
    public class DataContext : DbContext 
    {
        //adds the constructor so we can use connect to the db and add tables to it
        public DataContext(DbContextOptions options) :base(options)
        {

        }

        public DbSet<UserEntity> UserEntities { get; set; }


        //Do i need to go to api specifically to get individual transactions?
        //I do want each transaction to be geared towards its specific user
    }
}
