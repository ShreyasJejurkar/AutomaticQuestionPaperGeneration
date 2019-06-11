using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Controllers
{
    public class BaseController : Controller
    {
        [NonAction]
        public void Alert(string title, string message, Enums.NotificationType notificationType)
        {
            var msg = $"<script language='javascript'>swal('{title}','{message}','{notificationType}')" + "</script>";
            TempData["notification"] = msg;
        }

        [NonAction]
        public void Alert(string title, string message)
        {
            var msg = $"<script language='javascript'>swal('{title}','{message}')" + "</script>";
            TempData["notification"] = msg;
        }

        [NonAction]
        public void Alert(string message)
        {
            var msg = $"<script language='javascript'>swal('{message}')" + "</script>";
            TempData["notification"] = msg;
        }


        /// <summary>
        ///     Sets the information for the system notification.
        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="notifyType">The type of notification to display to the user: Success, Error or Warning.</param>
        [NonAction]
        public void Message(string message, Enums.NotificationType notifyType)
        {
            TempData["Notification2"] = message;

            switch (notifyType)
            {
                case Enums.NotificationType.success:
                    TempData["NotificationCSS"] = "alert-box success";
                    break;
                case Enums.NotificationType.error:
                    TempData["NotificationCSS"] = "alert-box errors";
                    break;
                case Enums.NotificationType.warning:
                    TempData["NotificationCSS"] = "alert-box warning";
                    break;

                case Enums.NotificationType.info:
                    TempData["NotificationCSS"] = "alert-box notice";
                    break;
            }
        }
    }
}