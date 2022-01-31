using BackendProjectNurlanSenior.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProjectNurlanSenior.Dal
{
    public class AppDbContext : IdentityDbContext<AppUser>
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
        public DbSet<Tag> Tags { get; set; }
        public DbSet<CourseTag> CourseTags { get; set; }

        public DbSet<CCategory> CCategories { get; set; }

        public DbSet<EventSpeaker> EventSpeakers { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        public DbSet<WelcomeEdu> WelcomeEdus { get; set; }

        public DbSet<NoticeBoard> NoticeBoards { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Settings> Settings { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<ContactMessage> ContactMessages { get; set; }

        public DbSet<Blog> Blogs { get; set; }



    }
}
