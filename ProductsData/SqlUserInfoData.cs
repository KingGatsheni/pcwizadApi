using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace UserInfoData
{
    public class SqlUserInfoData:IUserInfoData
    {
         private ApplicationDbContext _userContext;
        public SqlUserInfoData(ApplicationDbContext userContext)
        {
            _userContext = userContext;

        }
        public UserInfo AddUserInfo(UserInfo userInfo)
        {
            userInfo.StuffId = Guid.NewGuid();
            _userContext.userInfos.Add(userInfo);
            _userContext.SaveChanges();
            return userInfo;
        }

        public void DeleteUserInfo(UserInfo userInfo)
        {
            _userContext.userInfos.Remove(userInfo);
            _userContext.SaveChanges();
        }

        public UserInfo EditUserInfo(UserInfo userInfo)
        {
            var existing_UserInfo = _userContext.userInfos.Find(userInfo.StuffId);
            if (existing_UserInfo != null)
            {
                existing_UserInfo.UserName = userInfo.UserName;
                existing_UserInfo.Email = userInfo.Email;
                existing_UserInfo.Password = userInfo.Password;
                _userContext.userInfos.Update(existing_UserInfo);
                _userContext.SaveChanges();
            }
            return userInfo;

        }

        public UserInfo GetUserInfo(Guid id)
        {
            var UserInfo = _userContext.userInfos.Find(id);
            return UserInfo;
        }

        public List<UserInfo> GetUserInfo()
        {
            return _userContext.userInfos.ToList();
        }
    }
    }
