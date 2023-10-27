using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tedu_Ecommance.Public
{
    public class BaseListFilterDto : PagedResultRequestDto
    {
        public string? keyword { get; set; }
    }
}
