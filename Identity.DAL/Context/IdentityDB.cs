using Identity.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DAL.Context
{
    public class IdentityDB:IdentityDbContext<User,Role,string> /*если мы еще используем наши класс то добавляем <users,(если еще и роль есть то), Role, string> string это первичный ключь */
    {
        public IdentityDB(DbContextOptions<IdentityDB> options):base(options)//это контекст базы данных для авторизации  
        {

        }
    }
}
