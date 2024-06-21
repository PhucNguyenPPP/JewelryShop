using BLL.Interfaces;
using BOL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace PRN221_JewelryShop.Pages.Staff
{
    public class CustomerManagementModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IImageService _imageService;
        public List<Customer> CustomerList { get; set; }
        public LoginResponse LoginResponse { get; set; }

        [BindProperty]
        public CustomerResquestDTO CustomerResquestDTO { get; set; }

        [BindProperty]
        public IFormFile? CustomerAvatar { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchValue { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 5;

        public CustomerManagementModel(ICustomerService customerService, IImageService imageService)
        {
            _customerService = customerService;
            _imageService = imageService;

        }

        private void GetCustomerList()
        {
            var listAll = _customerService.GetAllCustomers();
            CustomerList = PagingList(listAll);
        }

        private List<Customer> PagingList(List<Customer> list)
        {
            TotalPages = (int)Math.Ceiling(list.Count / (double)PageSize);
            return list.Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();
        }

        public IActionResult OnGet()
        {
            GetCustomerList();

            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return RedirectToPage("/Login");
            };

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            if (LoginResponse.RoleName != "Staff")
            {
                return RedirectToPage("/Login");
            }
            return Page();
        }

        public IActionResult OnPostCreateCustomer()
        {
            GetCustomerList();

            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return RedirectToPage("/Login");
            };

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            if (LoginResponse.RoleName != "Staff")
            {
                return RedirectToPage("/Login");
            }

            if (!ModelState.IsValid)
            {
                TempData["CreateModelError"] = "Model State Invalid";
                return Page();
            }

            if (_customerService.CheckEmailAlreadyExists(CustomerResquestDTO.Email))
            {
                TempData["CreateError"] = "Email already exists";
                return Page();
            }

            if (_customerService.CheckPhoneAlreadyExists(CustomerResquestDTO.PhoneNumber))
            {
                TempData["CreateError"] = "PhoneNumber already exists";
                return Page();
            }

            /*convert image file to base 64*/
            if(CustomerAvatar != null)
            {
                CustomerResquestDTO.AvatarImg = _imageService.ConvertToBase64(CustomerAvatar);
            }
            CustomerResquestDTO.EmployeeId = LoginResponse.EmployeeId.ToString();

            var result = _customerService.AddCustomer(CustomerResquestDTO);
            if (result)
            {
                TempData["CreateMsg"] = "Create Customer Successfully";
                GetCustomerList();
                return Page();
            }
            else
            {
                TempData["CreateMsg"] = "Create Customer Unsuccessfully";
                return Page();
            }
        }


        public IActionResult OnPostUpdateCustomer()
        {
            GetCustomerList();

            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return RedirectToPage("/Login");
            };

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            if (LoginResponse.RoleName != "Staff")
            {
                return RedirectToPage("/Login");
            }

            if (!ModelState.IsValid)
            {
                TempData["UpdateModelError"] = "Model State Invalid";
                return Page();
            }


            var customer = _customerService.GetCustomer(CustomerResquestDTO.CustomerId);
            if (_customerService.CheckEmailAlreadyExists(CustomerResquestDTO.Email) && customer.Email != CustomerResquestDTO.Email)
            {
                TempData["UpdateError"] = "Email already exists";
                return Page();
            }

            if (_customerService.CheckPhoneAlreadyExists(CustomerResquestDTO.PhoneNumber) && customer.PhoneNumber != CustomerResquestDTO.PhoneNumber)
            {
                TempData["UpdateError"] = "PhoneNumber already exists";
                return Page();
            }


            if (CustomerAvatar != null)
            {
                CustomerResquestDTO.AvatarImg = _imageService.ConvertToBase64(CustomerAvatar);
            }


            var createCustomer = _customerService.UpdateCustomer(CustomerResquestDTO);
            if (createCustomer)
            {
                TempData["UpdateMsg"] = "Update Customer Successfully";
                GetCustomerList();
                return Page();
            }
            else
            {
                TempData["UpdateMsg"] = "Update Customer Unsuccessfully";
                return Page();
            }
        }

        public IActionResult OnPostDeleteCustomer()
        {
            GetCustomerList();

            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return RedirectToPage("/Login");
            };

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            if (LoginResponse.RoleName != "Staff")
            {
                return RedirectToPage("/Login");
            }


            var delete = _customerService.DeleteCustomer(CustomerResquestDTO.CustomerId);
            if (delete)
            {
                TempData["DeleteMsg"] = "Delete Successfully";
                GetCustomerList();
                return Page();
            }
            else
            {
                TempData["DeleteMsg"] = "Delete Unsuccessfully";
                return Page();
            }
        }

        public IActionResult OnGetPaging()
        {
            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return RedirectToPage("/Login");
            };

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            if (LoginResponse.RoleName != "Staff")
            {
                return RedirectToPage("/Login");
            }

            if (SearchValue.IsNullOrEmpty())
            {
                GetCustomerList();
                return Page();
            }
            else
            {
                var searchList = _customerService.SearchCustomers(SearchValue);
                var pagingList = PagingList(searchList);
                CustomerList = pagingList;
                return Page();
            }
        }

        public IActionResult OnGetSearchCustomer()
        {
            var loginResponseString = HttpContext.Session.GetString("LoginResponse");
            if (loginResponseString == null)
            {
                return RedirectToPage("/Login");
            };

            LoginResponse = JsonSerializer.Deserialize<LoginResponse>(loginResponseString);
            if (LoginResponse.RoleName != "Staff")
            {
                return RedirectToPage("/Login");
            }

            if (SearchValue.IsNullOrEmpty())
            {
                GetCustomerList();
                return Page();
            }
            var searchList = _customerService.SearchCustomers(SearchValue);
            if (searchList.Count == 0)
            {
                TempData["SearchMsg"] = "No result";
                CurrentPage = 0;
                return Page();
            }
            var pagingList = PagingList(searchList);
            CustomerList = pagingList;
            return Page();
        }
    }
}
