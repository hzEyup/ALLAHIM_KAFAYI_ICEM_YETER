using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace OgrenciKayit.Areas.Database.Controllers
{
    [Area("Db")]
    public class HomeController : Controller
    {
        private readonly StudentKayitContext _db;

        public HomeController(StudentKayitContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var students = _db.Students.ToList();
            _db.Students.RemoveRange(students);

            var classes = _db.Classes.ToList();
            _db.Classes.RemoveRange(classes);

            var lessons = _db.Lessons.ToList();
            _db.Lessons.RemoveRange(lessons);

            var users = _db.Users.ToList();
            _db.Users.RemoveRange(users);

            var roles = _db.Roles.ToList();
            _db.Roles.RemoveRange(roles);

            if (roles.Count > 0)
            {
                _db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('Roles', RESEED, 0)");
            }

            _db.Lessons.Add(new Lesson()
            {
                Name = "Matematik",
                IsNumeric = true
            });
            _db.Lessons.Add(new Lesson()
            {
                Name = "Turkce",
                IsNumeric = false
            });
            _db.Lessons.Add(new Lesson()
            {
                Name = "Fizik",
                IsNumeric = true
            });
            _db.Lessons.Add(new Lesson()
            {
                Name = "biyoloji",
                IsNumeric = true
            });
            _db.Lessons.Add(new Lesson()
            {
                Name = "felsefe",
                IsNumeric = false
            });
            _db.Lessons.Add(new Lesson()
            {
                Name = "tarih",
                IsNumeric = false
            });
            _db.SaveChanges();





            _db.Classes.Add(new Class()
            {
                Name = "10-C",
                Students = new List<Student>()
                {
                    new Student()
                    {
                        Name = "Eyup",
                        SurName = "Karaman",
                        SchoolNo = 163,
                        DateOfBirthday = new DateTime(2006, 12, 27),
                        studentLessons = new List<StudentLesson>()
                        {
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Felsefe").Id,

                            },
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Matematik").Id,
                            },
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Turkce").Id,
                            }
                        }
                    },
                    new Student()
                    {
                        Name = "Patrick",
                        SurName = "Bateman",
                        SchoolNo = 111,
                        DateOfBirthday = new DateTime(1996, 02, 11),
                        studentLessons = new List<StudentLesson>()
                        {
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Felsefe").Id,

                            },
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Matematik").Id,

                            },
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Turkce").Id,
                            },
                             new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Biyoloji").Id,
                            },



                        }
                    },
                    new Student()
                    {
                        Name = "Leonardo",
                        SurName = "DiCaprio",
                        SchoolNo = 12,
                        DateOfBirthday = new DateTime(2001, 03, 07),
                        studentLessons = new List<StudentLesson>()
                        {
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Felsefe").Id,

                            },
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Fizik").Id,

                            },
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Tarih").Id
                            },

                        },
                    },
                }
            });
            _db.Classes.Add(new Class()
            {
                Name = "11-A",
                Students = new List<Student>()
                {
                   new Student()
                        {
                            Name = "Quentin",
                            SurName = "Tarantino",
                            SchoolNo = 162,
                            DateOfBirthday = DateTime.Parse("05/12/1945", new CultureInfo("en-US")),
                             studentLessons = new List<StudentLesson>()
                        {
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Felsefe").Id,

                            },
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Matematik").Id,

                            },
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Turkce").Id,
                            },
                             new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Biyoloji").Id,
                            },

                        },
                             },

                   new Student()
                        {
                            Name = "Angelina",
                            SurName = "Jolie",
                            SchoolNo = 777,
                            DateOfBirthday = new DateTime(1979, 04, 12),
                             studentLessons = new List<StudentLesson>()
                        {
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Felsefe").Id,

                            },
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Matematik").Id,
                            },
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Turkce").Id,
                            }
                        }
                        },
                   new Student()
                        {
                            Name = "Johnny",
                            SurName = "Depp",
                            SchoolNo = 192,
                            DateOfBirthday = new DateTime(2012, 12, 07),
                             studentLessons = new List<StudentLesson>()
                        {
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Felsefe").Id,

                            },
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Matematik").Id,
                            },
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Turkce").Id,
                            }
                        },
                     },

                }
            });
            _db.Classes.Add(new Class()
            {
                Name = "10-D",
                Students = new List<Student>()
                {
                   new Student()
                        {
                            Name = "Dwayne",
                            SurName = "Johnson",
                            SchoolNo = 356,
                            DateOfBirthday = new DateTime(2010, 11, 13),
                            studentLessons = new List<StudentLesson>()
                        {
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Felsefe").Id,

                            },
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Biyoloji").Id,

                            },
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Tarih").Id
                            }
                        },
                        },
                   new Student()
                        {
                            Name = "Robert",
                            SurName = "jr",
                            SchoolNo = 767,
                            DateOfBirthday = new DateTime(1979, 04, 12),
                            studentLessons = new List<StudentLesson>()
                        {
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Turkce").Id,

                            },
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Matematik").Id,

                            },
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Tarih").Id
                            }
                        },
                        },
                   new Student()
                        {
                            Name = "Kemal",
                            SurName = "Sunal",
                            SchoolNo = 714,
                            DateOfBirthday = new DateTime(1979, 04, 12),
                            studentLessons = new List<StudentLesson>()
                        {
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Turkce").Id,

                            },
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Matematik").Id,

                            },
                            new StudentLesson()
                            {
                                LessonId = _db.Lessons.SingleOrDefault(l => l.Name == "Felsefe").Id
                            }
                        },
                        },


                }

            });
            _db.Roles.Add(new Role()
            {
                Name = "Admin",
                Users = new List<User>()
                {
                    new User()
                    {
                        IsActive = true,
                        Password = "eyup",
                        UserName = "eyup"
                    }
                }
            });
            _db.Roles.Add(new Role()
            {
                Name = "User",
                Users = new List<User>()
                {
                    new User()
                    {
                        IsActive = true,
                        Password = "eleyubbi",
                        UserName = "eleyubbi"
                    }
                }
            });

            _db.SaveChanges();
            return Content("<label style=\"color:red\";><b>Database basariyla doldu.</b></label>", "text/html", Encoding.UTF8);

        }
    }
}
