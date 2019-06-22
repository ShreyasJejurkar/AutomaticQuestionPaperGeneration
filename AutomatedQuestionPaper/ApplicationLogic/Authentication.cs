using System.Linq;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.ApplicationLogic
{
    public static class Authentication
    {
        private static readonly DatabaseContext Context = new DatabaseContext();

        public static (int status, string authenticatedUserName) Authenticate(Admin user)
        {
            var dbUser =
                Context.Admins.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if (dbUser == null)
            {
                var staffUser =
                    Context.Staffs.FirstOrDefault(T => T.Email == user.Username && T.Password == user.Password);

                if (staffUser != null)
                {
                    return (1, staffUser.Name);
                }
            }

            return dbUser != null ? (2, dbUser.Username) : (0, null);
        }
    }
}