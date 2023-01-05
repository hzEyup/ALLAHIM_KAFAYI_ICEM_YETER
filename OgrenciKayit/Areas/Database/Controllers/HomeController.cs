using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
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

            _db.Classes.Add(new Class()
            {
                Name = "10-C",
                Students = new List<Student>()
                {
                    new Student()
                    {
                        Name="Eyup",
                        SurName="Karaman",
                        SchoolNo=163,
                        DateOfBirthday= new DateTime(2006,12,27)
                    },
                    new Student()
                    {
                        Name="Patrick",
                        SurName="Bateman",
                        SchoolNo=111,
                        DateOfBirthday= new DateTime(1996,02,11)
                    },
                    new Student()
                    {
                        Name="abdul ",
                        SurName="Mehmud",
                        SchoolNo=12,
                        DateOfBirthday= new DateTime(2001,03,07)
                    },
                    new Student()
                    {
                        Name="Lucifer",
                        SurName="Morningstar",
                        SchoolNo=121,
                        DateOfBirthday= new DateTime(1978,06,06)
                    },
                    new Student()
                    {
                        Name="Jack",
                        SurName="Daniel",
                        SchoolNo=162,
                        DateOfBirthday=  DateTime.Parse("05/12/1945", new CultureInfo("en-US")),
                    }

                }
            });
            _db.Classes.Add(new Class()
            {
                Name = "11-A",
                Students = new List<Student>()
                {
                   new Student()
                   {
                       Name="Hit the Road",
                       SurName="jack",
                       SchoolNo=156,
                       DateOfBirthday = new DateTime(1999,02,13)

                   },
                   new Student()
                   {
                       Name="behlul",
                       SurName="Haznedar",
                       SchoolNo=65,
                       DateOfBirthday = new DateTime(1989,1,13)

                   },
                   new Student()
                   {
                       Name="Riza",
                       SurName="Baba",
                       SchoolNo=45,
                       DateOfBirthday = new DateTime(1969,02,23)

                   },
                }
            });
            _db.SaveChanges();
            return Content("<label style=\"color:red\";><b>Database basariyla doldu.</b></label>", "text/html", Encoding.UTF8);

        }
    }
}
