using Homework_8_9.Helpers;
using Homework_8_9.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Homework_8_9.Controllers
{
    [CustomAuthorize]
    public class AccountController : WebsiteControllerBase
    {
        static HttpClient client = new HttpClient();
        static string BaseURL = ConfigurationManager.AppSettings["ShopService"];

        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                User LoginResult = null;
                var dict = new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "username", model.Email },
                    { "password", model.Password }
                };
                var req = new HttpRequestMessage(HttpMethod.Post, $"{BaseURL}token") { Content = new FormUrlEncodedContent(dict) };
                var response = await client.SendAsync(req);

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
                }
                var Content = await response.Content.ReadAsStringAsync();
                var Result = JsonConvert.DeserializeObject<TokenResponse>(Content);

                LoginResult = new User { access_token = Result.access_token, Email = Result.userName };

                if (!string.IsNullOrWhiteSpace(Result.userName))
                {
                    var UserReq = new HttpRequestMessage(HttpMethod.Get, $"{BaseURL}api/Account/UserInfo?Email={Result.userName}");
                    var UserResponse = await client.SendAsync(UserReq);
                    var UserContent = await UserResponse.Content.ReadAsStringAsync();
                    var UserResult = JsonConvert.DeserializeObject<User>(UserContent);
                    LoginResult.Roles = UserResult.Roles;
                }

                SessionAssistance.SetUser(Session, LoginResult);

                return Redirect("/");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string output = JsonConvert.SerializeObject(model);
                    var stringContent = new StringContent(output, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"{BaseURL}api/Account/Register", stringContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "Invalid register attempt.");
                        return View(model);
                    }

                    return Redirect("/Account/Login");
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(500);
                }
            }

            return View(model);
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = await UserManager.FindByNameAsync(model.Email);
                //if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                //{
                //    // Don't reveal that the user does not exist or is not confirmed
                //    return View("ForgotPasswordConfirmation");
                //}
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //var user = await UserManager.FindByNameAsync(model.Email);
            //if (user == null)
            //{
            //    // Don't reveal that the user does not exist
            //    return RedirectToAction("ResetPasswordConfirmation", "Account");
            //}
            //var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            //if (result.Succeeded)
            //{
            //    return RedirectToAction("ResetPasswordConfirmation", "Account");
            //}
            //AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            SessionAssistance.Clear(Session);

            return RedirectToAction("Index", "Home");
        }


        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";


        private void AddErrors(List<string> result)
        {
            foreach (var error in result)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                //var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                //if (UserId != null)
                //{
                //    properties.Dictionary[XsrfKey] = UserId;
                //}
                //context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}