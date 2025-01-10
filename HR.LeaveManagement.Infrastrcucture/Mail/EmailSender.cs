﻿using HR.LeaveManagement.Application.Contracts.Infrastrcuture;
using HR.LeaveManagement.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Infrastrcucture.Mail
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSetting _emailSettings;

        public EmailSender(EmailSetting setting)
        {
            _emailSettings = setting;
        }
        public async Task<bool> SendEmailAsync(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);
            var to = new EmailAddress(email.To);
            var from = new EmailAddress 
            { 
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromAddress
            };
            var message = MailHelper.CreateSingleEmail(from,to, email.Subject, email.Body, email.Body);
            var response = await client.SendEmailAsync(message);

            return response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }
    }
}