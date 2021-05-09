using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web_Game_Project.Models;

namespace Web_Game_Project.Context
{
    public class usersInfo : DbContext
    {
        public usersInfo() : base(@"Data Source=192.168.0.85;Initial Catalog=myDB;Persist Security Info=True;User ID=kosta;Password=kosta")
        { }

        public DbSet<user> users { get; set; }  // 테이블 명과 같아야

    }
}