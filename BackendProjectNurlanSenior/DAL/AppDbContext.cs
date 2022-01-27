using BackendProjectNurlanSenior.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Dal
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options ):base(options)
        {

        }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Hobby> Hobbies { get; set; }

        public DbSet<SocialMedia> SocialMedias { get; set; }

        public DbSet<TeacherHobbies> TeacherHobbies { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Features> Features { get; set; }

    }
}
