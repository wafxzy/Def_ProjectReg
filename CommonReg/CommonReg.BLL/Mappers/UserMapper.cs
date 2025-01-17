using CommonReg.Common.Models;
using CommonReg.Common.UIModels.User.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonReg.BLL.Mappers
{
    public static class UserMapper
    {
        public static UserResponseModel MapToUserResponseModel(AccountEntity accountEntity)
        {
            if (accountEntity == null)
            {
                return null;
            }

            return new UserResponseModel
            {
                Id = accountEntity.Id,
                Email = accountEntity.Email,
                FirstName = accountEntity.FirstName,
                LastName = accountEntity.LastName,
                Age = accountEntity.Age,
                IsActive = accountEntity.IsActive,
                CreatedDate = accountEntity.CreatedAt,
                UpdatedDate = accountEntity.UpdatedAt,
                Avatar = accountEntity.Avatar
            };
        }

        public static UserProfileResponseModel MapToUserProfileResponseModel(AccountEntity accountEntity, IEnumerable<UserRoleModel> userRoleModels)
        {
            if (accountEntity == null)
            {
                return null;
            }

            return new UserProfileResponseModel
            {
                UserId = accountEntity.Id,
                Email = accountEntity.Email,
                FirstName = accountEntity.FirstName,
                LastName = accountEntity.LastName,
                Age = accountEntity.Age,
                Roles = userRoleModels.Select(MapToUserRoleResponseModel),
                Avatar = accountEntity.Avatar
            };
        }

        public static RoleResponseModel MapToUserRoleResponseModel(UserRoleModel userRoleModel)
        {
            if (userRoleModel == null)
            {
                return null;
            }

            return new RoleResponseModel()
            {
                RoleId = userRoleModel.RoleId,
                RoleName = userRoleModel.RoleName
            };
        }

        public static RoleResponseModel MapToUserRoleResponseModel(RoleEntity roleEntity)
        {
            if (roleEntity == null)
            {
                return null;
            }

            return new RoleResponseModel()
            {
                RoleId = roleEntity.Id,
                RoleName = roleEntity.Name
            };
        }
    }
}
