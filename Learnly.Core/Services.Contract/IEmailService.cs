using Learnly.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Core.Services.Contract
{
    public interface IEmailService
    {
        Task SendEmailAsync(Email email);
    }
}
