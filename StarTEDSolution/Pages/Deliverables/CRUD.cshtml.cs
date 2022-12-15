using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using StarTEDSolution.Helpers;
using StarTEDSystem.BLL;
using StarTEDSystem.Entities;

namespace StarTEDSolution.Pages.Deliverables
{
    public class CRUDModel : PageModel
    {
        #region Private service fields & class constructor
        private readonly ILogger<IndexModel> _logger;
        private readonly EmployeeServices _employeeServices;
        private readonly ProgramServices _programServices;
        private readonly PositionServices _positionServices;

        public CRUDModel(ILogger<IndexModel> logger,
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

        public bool HasFeedback => !string.IsNullOrWhiteSpace(Feedback);

        public string ErrorMessage { get; set; }

        public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

        [BindProperty(SupportsGet = true)]

        public int? employeeid { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchArg { get; set; }

        [BindProperty]

        public Employee EmployeeInfo { get; set; }

        [BindProperty]
        public StarTEDSystem.Entities.Programs ProgramInfo { get; set; }

        [BindProperty]

        public List<StarTEDSystem.Entities.Programs> ProgramList { get; set; }  

        [BindProperty]

        public Position PositionInfo { get; set; }

        [BindProperty]

        public List <Position> PositionList { get; set; }

        #region Paginator
        //  Desired page size
        private const int PAGE_SIZE = 25;
        //  Hold an instance of the Paginator
        public Paginator Pager { get; set; }
        #endregion  


        public void OnGet()
        {

            PopulateLists(); 

            if (employeeid.HasValue && employeeid > 0)
            {
             
                EmployeeInfo = _employeeServices.GetByID(employeeid.Value);

                       
            }


        }

        public void PopulateLists()
        {
            ProgramList = _programServices.Program_List();
            PositionList = _positionServices.Position_List();
        }



        public IActionResult OnPostCancel()
        {
            Feedback = "Employee Table Information will be cleared and you will be returned to the Query page.";

            return RedirectToPage("/Deliverables/Query");


        }

        public IActionResult OnPostNew()
        {
            if (ModelState.IsValid)
            {
                try
                {
                  
                    int newemployeeID = _employeeServices.Employee_AddEmployee(EmployeeInfo);
                    
                    Feedback = $"Employee ({newemployeeID}) has been added to the system";
                    return RedirectToPage(new { employeeid = newemployeeID });

                }
                catch (ArgumentNullException ex)
                {
                    ErrorMessage = GetInnerException(ex).Message;
                  
                    PopulateLists();
                   
                    return Page();
                }
                catch (Exception ex)
                {
                    ErrorMessage = GetInnerException(ex).Message;
                    PopulateLists();
                    return Page();
                }
            }

            return Page();
        }

        public IActionResult OnPostDelete()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int employeeid = _employeeServices.Employee_DeleteEmployee(EmployeeInfo);
                    if (employeeid > 0)
                    {
                        Feedback = $" The new Employee Record ({employeeid}) was successfully created.";
                    }
                    else
                    {
                        Feedback = "No Employee was affected.  Refresh search and try again.";
                    }
                    return RedirectToPage(new { employeeid = employeeid });
                }
                catch (ArgumentNullException ex)
                {
                    ErrorMessage = GetInnerException(ex).Message;
                    PopulateLists();
                    return Page();
                }
                catch (Exception ex)
                {
                    ErrorMessage = GetInnerException(ex).Message;
                    PopulateLists();
                    return Page();
                }
            }

            return Page();
        }

        public IActionResult OnPostUpdate()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int employeeid = _employeeServices.Employee_UpdateEmployee(EmployeeInfo);
                    if (employeeid > 0)
                    {
                        Feedback = $"The Employee Record ({employeeid}) was successfully updated..";
                    }
                    else
                    {
                        Feedback = "No Employee Record was affected.  Refresh search and try again.";
                    }
                    return RedirectToPage(new { employeeid = employeeid });
                }
                catch (ArgumentNullException ex)
                {
                    ErrorMessage = GetInnerException(ex).Message;
                    PopulateLists();
                    return Page();
                }
                catch (Exception ex)
                {
                    ErrorMessage = GetInnerException(ex).Message;
                    PopulateLists();
                    return Page();
                }

                
            }
            return Page();
        }
        private Exception GetInnerException(Exception ex)
        {

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;

            }
            return ex;
        }
    }
}
