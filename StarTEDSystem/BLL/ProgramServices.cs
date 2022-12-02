using StarTEDSystem.DAL;
using StarTEDSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarTEDSystem.BLL
{
    public class ProgramServices
    {
        private readonly StarTEDContext _context;

        internal ProgramServices(StarTEDContext regContext)
        {
            _context = regContext;
        }
        #region Services: Query
        public List<Programs> Program_List()
        {

           return _context.Programs
        .OrderBy(x => x.ProgramName)
        .ToList();
        }

        #endregion
    }
}
