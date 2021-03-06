﻿using System.Threading.Tasks;

namespace InstaDemo.Identity.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
