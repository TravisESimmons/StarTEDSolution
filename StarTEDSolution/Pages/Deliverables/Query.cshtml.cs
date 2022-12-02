#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using StarTEDSystem.BLL;
using StarTEDSystem.Entities;
using StarTEDSolution.Helpers;

namespace StarTEDSolution.Pages.Deliverables
{
    public class QueryModel : PageModel
    {
        #region Private service fields & class constructor
        private readonly ILogger<IndexModel> _logger;
        private readonly EmployeeServices _employeeServices;
        private readonly ProgramServices _programServices;
        private readonly PositionServices _positionServices;
  
        public QueryModel (ILogger<IndexModel> logger,
                                            EmployeeServices employeeServices,
                                            ProgramServices programServices,
                                            PositionServices positionServices)
        {
            _logger = logger;
            _employeeServices = employeeServices;
            _positionServices = positionServices;   
            _programServices = programServices;
        }
        #endregion

        [TempData]
        public string Feedback { get; set; }

        [BindProperty(SupportsGet = true)]
        public int EmployeeID { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchArg { get; set; }

        [BindProperty]

        public List<Employee> EmployeeInfo { get; set; }

        [BindProperty]
        public List <StarTEDSystem.Entities.Programs> ProgramInfo { get; set; }

        [BindProperty]

        public List<Position> PositionInfo { get; set; }

        #region Paginator
        //  Desired page size
        private const int PAGE_SIZE = 25;
        //  Hold an instance of the Paginator
        public Paginator Pager { get; set; }
        #endregion


        public void OnGet(int? currentPage)
        {
            

            ProgramInfo = _programServices.Program_List();

            PositionInfo = _positionServices.Position_List();

            if (!string.IsNullOrWhiteSpace(SearchArg))
            {
                //  Setting up for using the Paginator only needs to be done if 
                //      a query is executing

                //  determine the current page number
                int pageNumber = currentPage.HasValue ? currentPage.Value : 1;

                //  Setup the current state of the pagomatpr (sizing)
                PageState current = new(pageNumber, PAGE_SIZE);

                //  Temporary local integer to hold the results of the query's total collection size
                //  This will be need by the Paginator during the paginator's execute
                int totalCount;

                EmployeeInfo = _employeeServices.GetByPartialName(SearchArg,
                                            pageNumber, PAGE_SIZE, out totalCount);
                Pager = new Paginator(totalCount, current);
            }

        }

        public IActionResult OnPostSearch()
        {
            

            if (string.IsNullOrWhiteSpace(SearchArg))
            {
                Feedback = "Required:  Search argument is empty";
            }
            return RedirectToPage(new { SearchArg = SearchArg });
        }

        public IActionResult OnPostClear()
        {
            Feedback = "";
            
            ModelState.Clear();
            return RedirectToPage(new { SearchArg = (string?)null });
        }

        
        
    }
}
