using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend4.Services
{
    public interface ISignUpService
    {
        Boolean CheckPersonalInfo(String firstName, String lastName, Int32 birthdayDay, Int32 birthdayMonth, Int32 birthdayYear, String gender);
        void SavePersonalInfo(String firstName, String lastName, Int32 birthdayDay, Int32 birthdayMonth, Int32 birthdayYear, String gender);

    }
}
