using System;
using CleanArchitecture.Domain.DTOs;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interfaces;

public interface IUserNotifications
{
    bool NotifyUser(User user, string titulo, string mensaje);
    bool SendMail(MailData mailData);
}