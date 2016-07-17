using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class_6_21;
using ECommerce.Data;

namespace AdminSignup
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your first name.");
            string firstname = Console.ReadLine();
            Console.WriteLine("Enter your last name.");
            string lastname = Console.ReadLine();
            Console.WriteLine("Enter your username.");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your password.");
            string password = Console.ReadLine();
            if (SignUp(firstname, lastname, username, password))
                Console.WriteLine("Success");
            else
                Console.WriteLine("Unsuccessful");
            Console.ReadKey(true);
        }

        public static bool SignUp(string firstname, string lastname, string username,string password)
        {
            if (firstname == null|| lastname == null|| username == null|| password == null)
            {
                return false;
            }
            AdminRepository repo = new AdminRepository(Properties.Settings.Default.ConnString);
            repo.SignUp(firstname, lastname, username, password);
            return true;
        }
    }

    
}
