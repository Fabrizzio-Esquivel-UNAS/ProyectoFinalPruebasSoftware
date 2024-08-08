using System;
using System.Linq;
using System.Security.Claims;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using CleanArchitecture.Domain.DTOs;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain;

public sealed class ApiNotifications : IUserNotifications
{
    private readonly IOptionsSnapshot<MailSettings> _mailSettings;
    private static bool IsBitSet(byte b, int pos)
    {
        return (b & (1 << pos)) != 0;
    }

    public ApiNotifications(IOptionsSnapshot<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings;
    }

    public bool NotifyUser(User user, string titulo, string mensaje)
    {
        if (IsBitSet(user.Notificaciones, 0))
        {
            SendMail(new MailData
            {
                EmailToId = user.Email,
                EmailToName = user.FullName,
                EmailSubject = titulo,
                EmailBody = mensaje
            });
        }
        if (IsBitSet(user.Notificaciones, 1))
        {
            // SendPushNotification()
        }
        if (IsBitSet(user.Notificaciones, 2))
        {
            // SendSMS()
        }
        return true;
    }

    public bool SendMail(MailData mailData)
    {
        try
        {
            //MimeMessage - a class from Mimekit
            MimeMessage email_Message = new MimeMessage();
            MailboxAddress email_From = new MailboxAddress(_mailSettings.Value.Name, _mailSettings.Value.EmailId);
            email_Message.From.Add(email_From);
            MailboxAddress email_To = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
            email_Message.To.Add(email_To);
            email_Message.Subject = mailData.EmailSubject;
            BodyBuilder emailBodyBuilder = new BodyBuilder();
            emailBodyBuilder.TextBody = mailData.EmailBody;
            email_Message.Body = emailBodyBuilder.ToMessageBody();
            //this is the SmtpClient class from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
            SmtpClient MailClient = new SmtpClient();
            MailClient.Connect(_mailSettings.Value.Host, _mailSettings.Value.Port, _mailSettings.Value.UseSSL);
            MailClient.Authenticate(_mailSettings.Value.EmailId, _mailSettings.Value.Password);
            MailClient.Send(email_Message);
            MailClient.Disconnect(true);
            MailClient.Dispose();
            return true;
        }
        catch (Exception ex)
        {
            // Exception Details
            Console.WriteLine("ERROR SENDING EMAIL: ");
            Console.WriteLine(ex.ToString());
            return false;
        }
    }
}