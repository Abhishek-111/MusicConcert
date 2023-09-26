using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicConcert.Models;
using MusicConcert.Repository;

namespace MusicConcert.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }



        [Route("signup")]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> Signup(SignUpUserModel userModel)
        {
            if(ModelState.IsValid)
            {
                // save to db 
                var result = await _accountRepository.CreateUserAsync(userModel);

                if(!result.Succeeded)
                {
                    foreach(var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    //ModelState.Clear();
                }

                // ModelState.Clear();
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // LOGIN FUNCTIONALITES

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(SignInModel signInModel)
        {
            if (ModelState.IsValid)
            {

                var result = await _accountRepository.PasswordSignInAsync(signInModel);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                    //return RedirectToAction("UserEvents", "Event");
                }
                ModelState.AddModelError("", "Invalid Credentials");
                //ModelState.Clear();
            }
            return View(signInModel);
        }

        // logout feature
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
           
        }

    }
}
