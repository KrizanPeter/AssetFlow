using System;
using System.Collections.Generic;
using System.Text;

namespace AssetFlow.Application.Dtos
{
    public class AccountDto
    {
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public AccountSettingsDto AccountSettings { get; set; } = new AccountSettingsDto();
    }
}
