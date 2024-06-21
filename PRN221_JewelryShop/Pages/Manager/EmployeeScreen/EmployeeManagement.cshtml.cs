using BLL.Interfaces;
using BLL.Services;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Manager.EmployeeScreen
{
    public class EmployeeManagementModel : PageModel
    {
        private readonly IEmployeeService _employeeService;
        private readonly IImageService _imageService;
        private readonly ICounterService _counterService;
        public List<Employee> EmpoyeeList { get; set; }
        public List<Counter> CounterList { get; set; }
        public LoginResponse LoginResponse { get; set; }

        [BindProperty]
        public EmployeeRequestDTO EmployeeRequestDTO { get; set; }

        [BindProperty]
        public IFormFile? EmployeeAvatar { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchValue { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 5;

        public EmployeeManagementModel(IEmployeeService employeeService, IImageService imageService,
            ICounterService counterService)
        {
            _employeeService = employeeService;
            _imageService = imageService;
            _counterService = counterService;
        }

        private void GetEmployeeList()
        {
            var listAll = _employeeService.GetAllEmployee();
            EmpoyeeList = PagingList(listAll);
        }

        private List<Employee> PagingList(List<Employee> list)
        {
            TotalPages = (int)Math.Ceiling(list.Count / (double)PageSize);
            return list.Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();
        }

        private void GetAllCounter()
        {
            CounterList = _counterService.GetAllCounter();
        }

        public IActionResult OnGet()
        {
            GetEmployeeList();
            GetAllCounter();

            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return RedirectToPage("/Login");
            };

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            if (LoginResponse.RoleName != "Manager")
            {
                return RedirectToPage("/Login");
            }
            return Page();
        }

        public IActionResult OnPostCreateEmployee()
        {
            GetEmployeeList();
            GetAllCounter();

            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return RedirectToPage("/Login");
            };

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            if (LoginResponse.RoleName != "Manager")
            {
                return RedirectToPage("/Login");
            }

            if (!ModelState.IsValid)
            {
                TempData["CreateModelError"] = "Model State Invalid";
                return Page();
            }

            if (_employeeService.CheckUserNameExist(EmployeeRequestDTO.UserName))
            {
                TempData["CreateError"] = "Username already exists";
                return Page();
            }

            if (_employeeService.CheckEmailExist(EmployeeRequestDTO.Email))
            {
                TempData["CreateError"] = "Email already exists";
                return Page();
            }

            if (_employeeService.CheckPhoneExist(EmployeeRequestDTO.PhoneNumber))
            {
                TempData["CreateError"] = "PhoneNumber already exists";
                return Page();
            }

            /*convert image file to base 64*/
            if (EmployeeAvatar != null)
            {
                EmployeeRequestDTO.AvatarImg = _imageService.ConvertToBase64(EmployeeAvatar);
            }
            

            var result = _employeeService.AddEmployee(EmployeeRequestDTO);
            if (result)
            {
                TempData["CreateMsg"] = "Create Customer Successfully";
                GetEmployeeList();
                return Page();
            }
            else
            {
                TempData["CreateMsg"] = "Create Customer Unsuccessfully";
                return Page();
            }
        }


        public IActionResult OnPostUpdateEmployee()
        {
            GetEmployeeList();
            GetAllCounter();

            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return RedirectToPage("/Login");
            };

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            if (LoginResponse.RoleName != "Manager")
            {
                return RedirectToPage("/Login");
            }

            if (!ModelState.IsValid)
            {
                TempData["UpdateModelError"] = "Model State Invalid";
                return Page();
            }

            var employee = _employeeService.GetEmployee(EmployeeRequestDTO.EmployeeId);

            if (_employeeService.CheckEmailExist(EmployeeRequestDTO.Email)
                && employee.Email != EmployeeRequestDTO.Email)
            {
                TempData["UpdateError"] = "Email already exists";
                return Page();
            }

            if (_employeeService.CheckEmailExist(EmployeeRequestDTO.PhoneNumber)
                 && employee.PhoneNumber != EmployeeRequestDTO.PhoneNumber )
            {
                TempData["UpdateError"] = "PhoneNumber already exists";
                return Page();
            }


            if (EmployeeAvatar != null)
            {
                EmployeeRequestDTO.AvatarImg = _imageService.ConvertToBase64(EmployeeAvatar);
            }

            var result = _employeeService.UpdateEmployee(EmployeeRequestDTO);
            if (result)
            {
                TempData["UpdateMsg"] = "Update Employee Successfully";
                GetEmployeeList();
                return Page();
            }
            else
            {
                TempData["UpdateMsg"] = "Update Employee Unsuccessfully";
                return Page();
            }
        }

        public IActionResult OnPostDeleteEmployee()
        {
            GetEmployeeList();
            GetAllCounter();

            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return RedirectToPage("/Login");
            };

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            if (LoginResponse.RoleName != "Manager")
            {
                return RedirectToPage("/Login");
            }


            var delete = _employeeService.DeleteEmployee(EmployeeRequestDTO.EmployeeId);
            if (delete)
            {
                TempData["DeleteMsg"] = "Delete Employee Successfully";
                GetEmployeeList();
                return Page();
            }
            else
            {
                TempData["DeleteMsg"] = "Delete Employee Unsuccessfully";
                return Page();
            }
        }

        public IActionResult OnGetPaging()
        {
            GetAllCounter();
            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return RedirectToPage("/Login");
            };

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            if (LoginResponse.RoleName != "Manager")
            {
                return RedirectToPage("/Login");
            }

            if (SearchValue.IsNullOrEmpty())
            {
                GetEmployeeList();
                return Page();
            }
            else
            {
                var searchList = _employeeService.SearchEmployees(SearchValue);
                var pagingList = PagingList(searchList);
                EmpoyeeList = pagingList;
                return Page();
            }
        }

        public IActionResult OnGetSearchEmployee()
        {
            GetAllCounter();

            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return RedirectToPage("/Login");
            };

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            if (LoginResponse.RoleName != "Manager")
            {
                return RedirectToPage("/Login");
            }

            if (SearchValue.IsNullOrEmpty())
            {
                GetEmployeeList();
                return Page();
            }
            var searchList = _employeeService.SearchEmployees(SearchValue);
            if (searchList.Count == 0)
            {
                TempData["SearchMsg"] = "No result";
                CurrentPage = 0;
                return Page();
            }
            var pagingList = PagingList(searchList);
            EmpoyeeList = pagingList;
            return Page();
        }
    }
}
