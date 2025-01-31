using CommonReg.BLL.Mappers;
using CommonReg.BLL.Services.Interfaces;
using CommonReg.Common.Models;
using CommonReg.Common.UIModels.User.Request;
using CommonReg.Common.UIModels.User.Response;
using CommonReg.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.BLL.Services
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddRoleForUser(Guid userId, int roleId)
        {
            await _unitOfWork.UserRoleRepository.AddRoleForUser(userId, roleId);
            _unitOfWork.Commit();
        }

        public async  Task DeleteRoleForUser(Guid userId, int roleId)
        {
            await _unitOfWork.UserRoleRepository.DeleteRoleForUser(userId, roleId);
            _unitOfWork.Commit();
        }

        public async Task DeleteRolesForUser(Guid userId, List<int> roleIds)
        {
            await _unitOfWork.UserRoleRepository.DeleteRolesForUser(userId, roleIds);
            _unitOfWork.Commit();
        }

        public async Task DeleteUser(Guid userId)
        {
            await _unitOfWork.UserRepository.DeleteUser(userId);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<RoleResponseModel>> GetAllRoles()
        {
            IEnumerable<RoleEntity> roles = _unitOfWork.UserRoleRepository.GetAllRoles().Result;
            return roles.Select(UserMapper.MapToUserRoleResponseModel);
        }

        public async  Task<IEnumerable<UserResponseModel>> GetAllUsers()
        {
            IEnumerable<AccountEntity> users = _unitOfWork.UserRepository.GetAllUsers().Result;
            return users.Select(UserMapper.MapToUserResponseModel);
        }

        public Task<AccountEntity> GetUserByEmail(string email)
        {
        return _unitOfWork.UserRepository.GetUserByEmail(email);
        }

        public Task<AccountEntity> GetUserById(Guid id)
        {
            return _unitOfWork.UserRepository.GetUserById(id);
        }

        public async Task<UserProfileResponseModel> GetUserProfileById(Guid userId)
        {
            AccountEntity user = _unitOfWork.UserRepository.GetUserById(userId).Result;
            IEnumerable<UserRoleModel> userRoles = await _unitOfWork.UserRoleRepository.GetUserRoles(userId);
            return UserMapper.MapToUserProfileResponseModel(user, userRoles);
        }

        public async Task<List<int>> GetUserRolePermissions(Guid userId)
        {
            return (await _unitOfWork.UserRoleRepository.GetUserRolePermissions(userId)).ToList();
        }

        public Task<IEnumerable<UserRoleModel>> GetUserRoles(Guid id)
        {
            return _unitOfWork.UserRoleRepository.GetUserRoles(id);
        }

        public async Task<bool> UpdateUser(UpdateUserRequestModel updateUserRequestModel)
        {
            AccountEntity existedUser = await _unitOfWork.UserRepository.GetUserById(updateUserRequestModel.UserId);

            if (existedUser == null) return false;

            existedUser.UpdatedDate = DateTime.UtcNow;
            existedUser.FirstName = updateUserRequestModel.FirstName;
            existedUser.LastName = updateUserRequestModel.LastName;

            await _unitOfWork.UserRepository.UpdateUser(existedUser);

            List<int> existingRoles = (await _unitOfWork.UserRoleRepository.GetUserRoles(updateUserRequestModel.UserId)).Select(role => role.RoleId).ToList();

            if (updateUserRequestModel.UserRoleIds != null)
            {
                List<int> roleIdsToDelete =
                    existingRoles.FindAll(roleId => !updateUserRequestModel.UserRoleIds.Contains(roleId));

                List<int> roleIdsToAdd =
                    updateUserRequestModel.UserRoleIds.FindAll(roleId => !existingRoles.Contains(roleId));

                if (roleIdsToDelete.Any())
                {
                    await _unitOfWork.UserRoleRepository.DeleteRolesForUser(updateUserRequestModel.UserId,
                        roleIdsToDelete);
                }

                if (roleIdsToAdd.Any())
                {
                    await _unitOfWork.UserRoleRepository.InsertRolesForUser(updateUserRequestModel.UserId,
                        roleIdsToAdd);
                }
            }

            _unitOfWork.Commit();

            return true;
        }
    }
}
