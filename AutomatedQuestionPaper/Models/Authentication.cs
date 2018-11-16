using System.Linq;

namespace AutomatedQuestionPaper.Models
{
    /// <summary>
    /// Performs authentication for users and admin
    /// </summary>
    public class Authentication
    {
        // EF database context
        private static readonly DatabaseContext Context = new DatabaseContext();

        public static (int status , string authenticatedUserName) Authenticate(Admin user)
        {
            //Check if user is admin or not!
            var dbUser =
                Context.Admins.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if (dbUser == null)
            {
                //Check for staff member
                var staffUser =
                    Context.Staffs.FirstOrDefault(T => T.Email == user.Username && T.Password == user.Password);

                if (staffUser != null)
                {
                    return (1, staffUser.Name);
                }
            }

            if (dbUser != null)
            {
                return (2, dbUser.Username);
            }

            return (0, null);
        }
    }
}