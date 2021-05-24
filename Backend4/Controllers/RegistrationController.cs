using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Globalization;
using Backend4.Models.Controls;
using Backend4.Models;
using Backend4.Services;

namespace Backend4.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ISignUpService signUpService;

        public RegistrationController(ISignUpService signUpService)
        {
            this.signUpService = signUpService;
        }

        public IActionResult SignUp()
        {
            this.ViewBag.AllMonths = this.GetAllMonths();
            return this.View(new SignUpPersonalInfoViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(SignUpPersonalInfoViewModel model)
        {
            this.ViewBag.AllMonths = this.GetAllMonths();

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            
            if (signUpService.CheckPersonalInfo(model.FirstName, model.LastName, model.BirthdayDay, model.BirthdayMonth, model.BirthdayYear, model.Gender))
            {
                model.Existed = true;
                model.Months = this.GetAllMonths();
                return this.View("SignUpAlreadyExists", model);
            }

            return this.View("SignUpCredentials", new SignUpPasswordViewModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthdayDay = model.BirthdayDay,
                BirthdayMonth = model.BirthdayMonth,
                BirthdayYear = model.BirthdayYear,
                Gender = model.Gender,
                Existed = false
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUpAlreadyExists(SignUpPersonalInfoViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }


            return this.View("SignUpCredentials", new SignUpPasswordViewModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthdayDay = model.BirthdayDay,
                BirthdayMonth = model.BirthdayMonth,
                BirthdayYear = model.BirthdayYear,
                Gender = model.Gender,
                Existed = true
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUpCredentials(SignUpPasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            signUpService.SavePersonalInfo(model.FirstName, model.LastName, model.BirthdayDay, model.BirthdayMonth, model.BirthdayYear, model.Gender);
            model.Months = this.GetAllMonths();
            return this.View("SignUpResult", model);
        }

        public IActionResult SignUpResult(SignUpPasswordViewModel model)
        {
            this.ViewBag.AllMonths = this.GetAllMonths();
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            return this.View(model);
        }

        private Month[] GetAllMonths()
        {
            return CultureInfo.InvariantCulture.DateTimeFormat.MonthNames
                .Select((x, i) => new Month { Id = i + 1, Name = x })
                .Where(x => !String.IsNullOrEmpty(x.Name))
                .ToArray();
        }

    }
}
