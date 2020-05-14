using Project.Models.Dto;
using Project.Models.ViewModels;
using System.Collections.Generic;

namespace Project.Models.Business
{
    public class UserBus
    {
        public static List<UserView> GetData(int page) => new UserDto().GetData(page);

        public static UserView GetDataById(int userId) => new UserDto().GetDataById(userId);

        public static int Create(UserView userView) => new UserDto().Create(userView);

        public static bool Modify(UserView userView) => new UserDto().Modify(userView);

        public static bool Active(int id) => new UserDto().Active(id);

        public static bool SetStatus(int id) => new UserDto().SetStatus(id);

        public static List<UserView> GetDataRemoved(int page) => new UserDto().GetDataRemoved(page);

        public static List<UserView> SearchAll(int page, string textsearch) => new UserDto().SearchAll(page, textsearch);
        public static int GetRowCountSearchAll(string textsearch) => new UserDto().GetRowsCountSearchAll(textsearch);

        public static List<UserView> SearchByName(int page, string textsearch) => new UserDto().SearchByName(page, textsearch);
        public static int GetRowCountSearchByName(string textsearch) => new UserDto().GetRowsCountSearchByName(textsearch);

        public static List<UserView> SearchByEmail(int page, string textsearch) => new UserDto().SearchByEmail(page, textsearch);
        public static int GetRowCountSearchByEmail(string textsearch) => new UserDto().GetRowsCountSearchByEmail(textsearch);

        public static List<UserView> SearchByPhone(int page, string textsearch) => new UserDto().SearchByPhone(page, textsearch);
        public static int GetRowCountSearchByPhone(string textsearch) => new UserDto().GetRowsCountSearchByPhone(textsearch);

        public static List<UserView> SearchByAddress(int page, string textsearch) => new UserDto().SearchByAddress(page, textsearch);
        public static int GetRowCountSearchByAddress(string textsearch) => new UserDto().GetRowsCountSearchByAddress(textsearch);

        public static UserView Login(UserView userView) => new UserDto().Login(userView);
        public static UserView LoginAdmin(UserView userView) => new UserDto().LoginAdmin(userView);

        public static string ForgorPassword(string email) => new UserDto().InsertCodeForgotPW(email);

        public static UserView CompareCodeChangePW(string email, string code) => new UserDto().CompareCodeChangePW(email, code);

        public static bool UpdatePassword(UserView userView) => new UserDto().UpdatePassword(userView);
    }
}
