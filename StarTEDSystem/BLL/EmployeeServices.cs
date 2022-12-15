#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StarTEDSystem.DAL;
using StarTEDSystem.Entities;

namespace StarTEDSystem.BLL
{
    public class EmployeeServices
    {
        private readonly StarTEDContext _context;

        internal EmployeeServices(StarTEDContext regContext)
        {
            _context = regContext;
        }
        #region Services: Query

        public List<Employee> Employees()
        {

            IEnumerable<Employee> info = _context.Employees.OrderBy(item => item.LastName);
            return info.ToList();

        }


        public List<Employee> GetByPartialName(string partialname, int pageNumber,
                                                        int pageSize,
                                                        out int totalCount)
        {

            IEnumerable<Employee> info = _context.Employees
        .Where(x => x.LastName.Contains(partialname)
                || x.FirstName.Contains(partialname))
        .OrderBy(x => x.LastName)
        .ThenBy(x => x.FirstName);

            totalCount = info.Count();

            int skipRows = (pageNumber - 1) * pageSize;

            return info.Skip(skipRows).Take(pageSize).ToList();


        }

        public Employee GetByID(int employeeid)
        {
            return _context.Employees
         .Where(x => x.EmployeeID.Equals(employeeid))
         .FirstOrDefault();
            
        }
        #endregion

        public int Employee_AddEmployee(Employee item)
        {

            bool exists = _context.Employees.Any(x => x.EmployeeID == item.EmployeeID);

            if (exists != null)
            {
                throw new Exception($"Employee: {item.FullName} is already on file with the entered information.");
            }


            _context.Employees.Add(item);


            _context.SaveChanges();

           
            return item.EmployeeID;
        }

        public int Employee_DeleteEmployee(Employee item)
        {

            bool exists = _context.Employees.Any(x => x.EmployeeID == item.EmployeeID);

     
            if (!exists)
            {
                throw new Exception($"Existing Employee Data can NOT be deleted from the database.");
            }


            item.ReleaseDate = DateTime.Now;
            EntityEntry<Employee> updating = _context.Entry(item);
           
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();
            return item.EmployeeID;
        }

        public int Employee_UpdateEmployee(Employee item)
        {
           
            bool exists = _context.Employees.Any(x => x.EmployeeID == item.EmployeeID);
            
            if (!exists)
            {
                throw new Exception($"");
            }

           
            EntityEntry<Employee> updating = _context.Entry(item);
            
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
          
            _context.SaveChanges();
            return item.EmployeeID;
        }
    }
}
