using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceTracker.Data.Context;
using AttendanceTracker.Data.Entity;

namespace AttendanceTracker.Data.Seed
{
    public class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Departments.Any())
            {
                context.Departments.AddRange(
                    new Department { Name = "HR", Code = "HRDE", Location = "Cairo" },
                    new Department { Name = "IT", Code = "ITDE", Location = "Alexandria" }
                );

                context.SaveChanges();
            }
        }
    }
}
