using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using ThemeParkDatabase.Services;
using ThemeParkDatabase.Models;
using Microsoft.Extensions.Logging;
using ThemeParkDatabase.Data;
using Microsoft.AspNetCore.Authorization;


namespace ThemeParkDatabase.Pages.Account
{

   [Authorize(Roles = "Admin")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceProvider _service;
        private readonly ILogger<LoginModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IServiceProvider service,
            ILogger<LoginModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _service = service;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [StringLength(20, ErrorMessage = "Account Type canot be longer than 20 characters")]
            [Display(Name = "Account Type")]
            public string AccountType { get; set; }
            

        }

        public void OnGet(string role, string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
           
           
            if (ModelState.IsValid)
            {
                //create the user and get the roles
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var roleManager = _service.GetRequiredService<RoleManager<IdentityRole>>();
                
                IdentityResult userVerified;

                //if the role exists adds in the user and their role into the database, if not then reload the page
                if (!await roleManager.RoleExistsAsync(Input.AccountType))
                {
                    _logger.LogInformation("Role doesn't exist");
                    return Page();
                }
                else
                {
                    userVerified = await _userManager.CreateAsync(user, Input.Password);
                    await _userManager.AddToRoleAsync(user, Input.AccountType);
                }
                
                //if the user was added successfully then return them back to whatever page they were on before
                if (userVerified.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(Input.Email, callbackUrl);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(Url.GetLocalUrl(returnUrl));
                }
                foreach (var error in userVerified.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
