using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using emailVlidation;

namespace emailTestCMD
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = "";
            var test = new emailAddress(" ", ref message);
            if (message == null)
            {
                Console.WriteLine("Email Name is " + test.emailName);
                Console.WriteLine("Email Domain is " + test.emailDomain);
            }
            else {
                Console.WriteLine(message);
            }
            
            
/*
            string[] emailAddresses = { "david.jones@proseware.com", "d.j@server1.proseware.com",
                                  "jones@ms1.proseware.com", "j.@server1.proseware.com",
                                  "j@proseware.com9", "js#internal@proseware.com",
                                  "j_9@[129.126.118.1]", "j..s@proseware.com",
                                  "js*@proseware.com", "js@proseware..com",
                                  "js@proseware.com9", "j.s@server1.proseware.com",
                                   "\"j\\\"s\\\"\"@proseware.com", "js@bücher.com" };
*/                           
            Console.ReadLine();
        }
    }
}
