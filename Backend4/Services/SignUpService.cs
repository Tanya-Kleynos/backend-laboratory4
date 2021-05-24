using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Backend4.Services
{
    public class SignUpService : ISignUpService
    {
        private readonly ILogger logger;
        private readonly List<Entry> personalInfo = new List<Entry>();

        public SignUpService(ILogger<IPasswordResetService> logger)
        {
            this.logger = logger;
        }

        public Boolean CheckPersonalInfo(String firstName, String lastName, Int32 birthdayDay, Int32 birthdayMonth, Int32 birthdayYear, String gender)
        {
            lock (this.personalInfo)
            {
                this.logger.LogInformation($"Check personal info for another the same: firstName:{firstName}, lastName:{lastName}, birthdayDay:{birthdayDay}, birthdayMonth:{birthdayMonth}, birthdayYear:{birthdayYear}, gender:{gender}");
                return this.personalInfo.Any(x => x.FirstName == firstName && x.LastName == lastName && x.BirthdayDay == birthdayDay && x.BirthdayMonth == birthdayMonth && x.BirthdayYear == birthdayYear && x.Gender == gender);
            }
        }

        public void SavePersonalInfo(String firstName, String lastName, Int32 birthdayDay, Int32 birthdayMonth, Int32 birthdayYear, String gender)
        {
            lock (this.personalInfo)
            {
                this.personalInfo.Add(new Entry(firstName, lastName, birthdayDay, birthdayMonth, birthdayYear, gender));
                this.logger.LogInformation($"Save personal info: firstName:{firstName}, lastName:{lastName}, birthdayDay:{birthdayDay}, birthdayMonth:{birthdayMonth}, birthdayYear:{birthdayYear}, gender:{gender}");
            }
        }



        private sealed class Entry
        {
            public Entry(String firstName, String lastName, Int32 birthdayDay, Int32 birthdayMonth, Int32 birthdayYear, String gender)
            {
                FirstName = firstName;
                LastName = lastName;                
                BirthdayDay = birthdayDay;
                BirthdayMonth = birthdayMonth;
                BirthdayYear = birthdayYear;
                Gender = gender;
            }

            public String FirstName { get; }
            public String LastName { get; }            
            public Int32 BirthdayDay { get; }
            public Int32 BirthdayMonth { get; }
            public Int32 BirthdayYear { get; }
            public String Gender { get; }
        }
    }
}
