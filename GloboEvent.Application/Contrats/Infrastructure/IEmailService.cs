using GloboEvent.Application.Model.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GloboEvent.Application.Contrats.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendMail(Email email);
    }
}
