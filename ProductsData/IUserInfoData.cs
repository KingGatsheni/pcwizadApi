using System;
using System.Collections.Generic;

namespace UserInfoData
{
    public interface IUserInfoData
    {
        
        List<Models.UserInfo> GetUserInfo();

        Models.UserInfo GetUserInfo(Guid id);

        Models.UserInfo AddUserInfo(Models.UserInfo userInfo);

        void DeleteUserInfo(Models.UserInfo userInfo);

        Models.UserInfo EditUserInfo(Models.UserInfo userInfo);   
    }
}