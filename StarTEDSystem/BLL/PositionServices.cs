#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarTEDSystem.DAL;
using StarTEDSystem.Entities;

namespace StarTEDSystem.BLL
{
    public class PositionServices
    {
        private readonly StarTEDContext _context;

        internal PositionServices(StarTEDContext regContext)
        {
            _context = regContext;
        }
        #region Services: Query

        public List<Position> Position_List()
        {

            return _context.Positions
        .OrderBy(x => x.Description)
         .ToList();
        }


        #endregion
    }
}
