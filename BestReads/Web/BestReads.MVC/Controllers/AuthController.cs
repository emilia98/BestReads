using BestReads.Data.Models;
using BestReads.Helpers;
using BestReads.InputModels.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BestReads.MVC.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserManager<ApplicationUser> userManager, 
                              SignInManager<ApplicationUser> signInManager,
                              RoleManager<ApplicationRole> roleManager,
                              IUserStore<ApplicationUser> userStore,
                              ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _logger = logger;
        }

        public IActionResult Index()
		{
			return View();
		}
        
        [HttpGet]
        public async Task<IActionResult> Login([FromRoute] string? returnUrl = null)
        {
            /*
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            } */

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            // await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            // ReturnUrl = returnUrl;
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model, [FromRoute] string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return string.IsNullOrEmpty(returnUrl) ? RedirectToAction("Index", "Home") : LocalRedirect(returnUrl);                
            }

            ModelState.AddModelError(string.Empty, "Invalid email/password.");
            ViewData["returnUrl"] = returnUrl;
            return View(model);
        }

        [HttpGet]
        public IActionResult Register([FromRoute] string? returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterInputModel model, [FromRoute] string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            

            (ApplicationUser? user, string? errorMessage) = await CreateUser(model);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty,
                    string.IsNullOrEmpty(errorMessage) ? "Възникна неочаквана грешка при регистрация." : errorMessage);
                return View(model);
            }
           // await _userStore.SetUserNameAsync(user, model.UserName, CancellationToken.None);
           // await _userStore.SetNormalizedUserNameAsync(user, model.UserName, CancellationToken.None);
           // await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
           // await _emailStore.SetNormalizedEmailAsync(user, model.UserName, CancellationToken.None);

            var result = await _userManager.CreateAsync(user!, model.Password);
            
            if (result.Succeeded)
            {
                _logger.LogInformation($"Successfully created user ${model.UserName}.");
                await _userManager.AddToRoleAsync(user, GlobalConstants.UserRole);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return string.IsNullOrEmpty(returnUrl) ? RedirectToRoute("/") : LocalRedirect(returnUrl);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            ViewData["returnUrl"] = returnUrl;
            return View(model);
        }

        private async Task<(ApplicationUser?, string?)> CreateUser(RegisterInputModel model)
        {
            var userWithUserName = await _userManager.FindByNameAsync(model.UserName);
            if (userWithUserName != null)
            {
                return (null, "Това потребителско име е заето. Изберете друго.");
            }

            var userWithEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithEmail != null)
            {
                return (null, "Този имейл е зает. Изберете друг.");
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                NormalizedUserName = model.UserName.ToUpper(),
                NormalizedEmail = model.Email.ToUpper(),
            };

            return (user, null);
        }
    }
}