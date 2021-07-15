using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LibraryGUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace LibraryGUI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Role")]
            public string Name { get; set; }

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
        }

        public void OnGet(string returnUrl = null)
        {
            //<font color = "#c0504d" > 
                ViewData["roles"] = _roleManager.Roles.ToList();
            //</font>
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            var role = _roleManager.FindByIdAsync(Input.Name).Result;
            if (ModelState.IsValid)
            {
                //using (var smtpClient = HttpContext.RequestServices.GetRequiredService<SmtpClient>())
                //{


                    var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");




                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        //var emailModel = new EmailModel();



                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { userId = user.Email, code },
                            protocol: Request.Scheme);

                        var callbackUrls = Url.Page(
                         "/Account/SuccessRegistration",
                         pageHandler: null
                             );

                        //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");


                        //var confirmationLink = Url.Page(nameof(ConfirmEmailModel), "Account", new { code, EmailModel = user.Email }, Request.Scheme);

                        //var message = new Message(new string[] { user.Email }, "Email Confirmation Link", confirmationLink, null);

                        //await _emailSender.SendEmailAsync(message);

                        //await smtpClient.SendMailAsync(new MailMessage(

                        //    from: "slaterzamani@gmail.com",

                        //    to: Input.Email,
                        //    subject: "User Registered Successfully",
                        //    body: $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."

                        //    ));

                        MailMessage msg = new MailMessage();
                     
                            msg.From = new MailAddress("slaterzamani@gmail.com");
                            msg.To.Add(Input.Email);
                            msg.Subject = "User Registered Successfully";
                            msg.Body = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
                            msg.IsBodyHtml = true;
                        
                        using (SmtpClient client = new SmtpClient())
                        {
                            client.EnableSsl = true;
                            client.UseDefaultCredentials = false;
                            client.Credentials = new NetworkCredential("slaterzamani@gmail.com", "micheal123");
                        client.Host = "smtp.gmail.com";
                        client.Port = 587;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;

                        client.Send(msg);
                        }
                      


                        await _emailSender.SendEmailAsync(Input.Email, "User Registered Successfully", code);

                        await _userManager.AddToRoleAsync(user, role.Name);
                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(callbackUrls);
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            //}
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
