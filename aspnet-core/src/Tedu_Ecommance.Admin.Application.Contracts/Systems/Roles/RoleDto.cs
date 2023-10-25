﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tedu_Ecommance.Admin.Systems.Roles
{
    public class RoleDto : EntityDto<Guid>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
