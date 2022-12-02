#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            //  Determine the number of rows to skip
            //  THis skipped count reflects the rows of the previous pages
            //  Remember the pagenumber is a natural number (1,2,3,....)
            //  This needs to be treated as an index (natural number -1)  Zero base
            //  The number of rows to skip is index * pagesize
            int skipRows = (pageNumber - 1) * pageSize;
            //  Return only the required number of rows.
            //  This will be done using filters belonging to LINQ
            //  Use the filter .Skip(n) to skip over n rows from the beginning of a collection
            //  Use the filter .Take(n) to take the next n rows from a collection
            return info.Skip(skipRows).Take(pageSize).ToList();

            //  This is the return statement that would be used IF no paging is being implemented
        }

        public Employee GetByID(int employeeid)
        {
            return _context.Employees
         .Where(x => x.EmployeeID.Equals(employeeid))
         .FirstOrDefault();
            
        }
        #endregion
    }
}
