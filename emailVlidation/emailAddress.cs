using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emailVlidation
{
    public class emailAddress
    {
        public string emailName { get; set; }
        public string emailDomain { get; set; }


        public emailAddress(string emailAddress, ref string message)
        {
            if (checkEmailAddress(emailAddress))
            {
                var tempEmail = emailAddress.Split('@');
                this.emailName = tempEmail.First();
                this.emailDomain = tempEmail.Last();
            }
            else {
                message ="Invalide name";
            }
        }


        public bool checkEmailAddress(string emailAddress) {
            var result = true;
            //check regular expression
            RegexUtilities util = new RegexUtilities();
            if (util.IsValidEmail(emailAddress))
            {
                result = true;
            }else
            {
                result = false;
            }

            //check blacklist from here.

            return result;
        }

        public bool checkEmailName() {
            var result = true;
            if (this.emailName != null)
            {
                //check from the valid list, define new name rule here
            }
            return result;
        }

        public bool checkEmailDomain()
        {
            var result = true;
            if (this.emailDomain != null)
            {
                //check from the valid list, if meet strong domain, pass.
                //if meet weak domain return not sure code.
                //if meet wrong domain, return invalid domain code.

            }

            return result;
        }

    }
}
