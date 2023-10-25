using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tedu_Ecommance.Admin.Systems.Users
{
    public interface IUserAppService : ICrudAppService<
        UserDto,
        Guid,
        PagedResultRequestDto,
        CreateUserDto,
        UpdateUserDto
        >
    {
        Task DeleteMultipleAsync(IEnumerable<Guid> ids);
        Task<PagedResultDto<UserInlistDto>> GetListWithFilterAsync(BaseListFilterDto input);
        Task<List<UserInlistDto>> GetListAllAsync(string  filterKeyword);
        Task AssignRoleAsync(Guid userId, string[] roleName);
    }
}
