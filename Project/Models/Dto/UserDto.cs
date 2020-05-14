using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using Project.Models.Entities;
using Project.Models.ViewModels;
using Supports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Project.Models.Dto
{
    public class UserDto
    {
        private DBContext db;
        public int size = ConstantCuaSang.size;
        public string search = ConstantCuaSang.search;
        //int start = size * (page - 1);

        public UserDto()
        {
            db = new DBContext();
        }

        public List<UserView> GetData(int page)
        {
            int start = size * (page - 1);
            return db.User.AsNoTracking().Where(s => s.Status).OrderByDescending(s => s.Id).Skip(start).Take(size).Select(s => new UserView
            {
                Id = s.Id,
                Email = s.Email,
                Name = s.Name,
                Password = s.Password,
                Phone = s.Phone.Trim(),
                Address = s.Address,
                Photo = s.Photo,
                Birthday = (DateTime)s.Birthday,
                DayCreate = s.DayCreate,
                DayEdited = s.DayEdited,
                Gender = (byte)s.Gender,
                Status = s.Status,
                Role = s.Role,
                Active = s.Active,
                Description = s.Description,
            }).ToList();
        }

        public UserView GetDataById(int userId)
        {
            return db.User.AsNoTracking().Where(s => s.Id == userId).Select(s => new UserView
            {
                Id = s.Id,
                Email = s.Email,
                Name = s.Name,
                Password = s.Password,
                Phone = s.Phone.Trim(),
                Address = s.Address,
                Photo = s.Photo,
                Birthday = (DateTime)s.Birthday,
                DayCreate = s.DayCreate,
                DayEdited = s.DayEdited,
                Gender = (byte)s.Gender,
                Active = s.Active,
                Status = s.Status,
                Role = s.Role,
                Description = s.Description,
                EditerId = (int)s.EditerId
            }).SingleOrDefault();
        }

        public int Create(UserView userView)// thêm dữ liệu User
        {
            try
            {
                if (CheckExists(userView, db)) return -1; //check email, phone đã tồn tại chưa;

                User user = new User
                {
                    Name = userView.Name,
                    Email = userView.Email,
                    Photo = userView.Photo,
                    Address = userView.Address,
                    Birthday = userView.Birthday,
                    Gender = (byte)userView.Gender,
                    Password = MD5_Sang.Encrypt(userView.Password),
                    Phone = userView.Password,
                    Role = (byte)userView.Role,
                    DayCreate = userView.DayCreate,
                    DayEdited = userView.DayEdited,
                    Description = userView.Description,
                };

                db.User.Add(user);
                db.SaveChanges();
                return user.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return 0;
            }
        }

        public bool Active(int id)
        {
            try
            {
                User user = db.User.Find(id);
                if (user == null) return false;
                user.Active = !user.Active;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public bool Modify(UserView userView) //Update
        {
            try
            {
                if (CheckExists(userView, db)) return false; //lúc modify cũng phải check coi có trùng ko;
                User user_1 = db.User.Find(userView.Id);
                user_1.Name = userView.Name;
                user_1.Gender = (byte)userView.Gender;
                user_1.Phone = userView.Phone;
                user_1.Address = userView.Address;
                user_1.Photo = userView.Photo;
                user_1.Active = userView.Active;
                user_1.EditerId = userView.EditerId;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public bool SetStatus(int id)
        {
            try
            {
                User user = db.User.Find(id);
                user.Status = !user.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public List<UserView> GetDataRemoved(int page)
        {
            int start = size * (page - 1);
            return db.User.AsNoTracking().Where(s => !s.Status).Skip(start).Take(size).Select(s => new UserView
            {
                Id = s.Id,
                Email = s.Email,
                Name = s.Name,
                Password = s.Password,
                Phone = s.Phone.Trim(),
                Address = s.Address,
                Photo = s.Photo,
                Birthday = (DateTime)s.Birthday,
                DayCreate = s.DayCreate,
                DayEdited = s.DayEdited,
                Gender = (byte)s.Gender,
                Status = s.Status,
                Role = s.Role,
                Active = s.Active,
                Description = s.Description
            }).ToList();
        }

        public List<UserView> SearchAll(int page, string textsearch)
        {
            int start = size * (page - 1);
            string query = $"SELECT * FROM [user] WHERE " +
                            $"name {search} like '%{textsearch}%' {search}  OR" +
                            $"email {search} like '%{textsearch}%' {search} OR" +
                            $"phone {search} like '%{textsearch}%' {search} OR" +
                            $"address {search} like '%{textsearch}%' {search}";
            return db.User.FromSqlRaw(query).AsNoTracking().Where(s => s.Status).OrderByDescending(s => s.Id).Skip(start).Take(size).Select(s => new UserView
            {
                Id = s.Id,
                Email = s.Email,
                Name = s.Name,
                Password = s.Password,
                Phone = s.Phone.Trim(),
                Address = s.Address,
                Photo = s.Photo,
                Birthday = (DateTime)s.Birthday,
                DayCreate = s.DayCreate,
                DayEdited = s.DayEdited,
                Gender = (byte)s.Gender,
                Active = s.Active,
                Status = s.Status,
                Role = s.Role,
                Description = s.Description
            }).ToList();
        }

        public int GetRowsCountSearchAll(string textsearch)
        {
            string query = $"SELECT * FROM [user] WHERE " +
                            $"name {search} like '%{textsearch}%' {search}  OR" +
                            $"email {search} like '%{textsearch}%' {search} OR" +
                            $"phone {search} like '%{textsearch}%' {search} OR" +
                            $"address {search} like '%{textsearch}%' {search}";
            return db.User.FromSqlRaw(query).AsNoTracking().Where(s => s.Status).Count();
        }

        public List<UserView> SearchByName(int page, string textsearch)
        {
            int start = size * (page - 1);
            return db.User.FromSqlRaw($"SELECT * FROM [user] WHERE name {search} like '%{textsearch}%' {search}").AsNoTracking().Where(s => s.Status).OrderByDescending(s => s.Id).Skip(start).Take(size).Select(s => new UserView
            {
                Id = s.Id,
                Email = s.Email,
                Name = s.Name,
                Password = s.Password,
                Phone = s.Phone.Trim(),
                Address = s.Address,
                Photo = s.Photo,
                Birthday = (DateTime)s.Birthday,
                DayCreate = s.DayCreate,
                DayEdited = s.DayEdited,
                Gender = (byte)s.Gender,
                Active = s.Active,
                Status = s.Status,
                Role = s.Role,
                Description = s.Description
            }).ToList();
        }

        public int GetRowsCountSearchByName(string textsearch)
        {
            return db.User.FromSqlRaw($"SELECT * FROM [user] WHERE name {search} like '%{textsearch}%' {search}").AsNoTracking().Where(s => s.Status).Count();
        }

        public List<UserView> SearchByEmail(int page, string textsearch)
        {
            int start = size * (page - 1);
            return db.User.FromSqlRaw($"SELECT * FROM [user] WHERE email {search} like '%{textsearch}%' {search}").AsNoTracking().Where(s => s.Status).OrderByDescending(s => s.Id).Skip(start).Take(size).Select(s => new UserView
            {
                Id = s.Id,
                Email = s.Email,
                Name = s.Name,
                Password = s.Password,
                Phone = s.Phone.Trim(),
                Address = s.Address,
                Photo = s.Photo,
                Birthday = (DateTime)s.Birthday,
                DayCreate = s.DayCreate,
                DayEdited = s.DayEdited,
                Gender = (byte)s.Gender,
                Active = s.Active,
                Status = s.Status,
                Role = s.Role,
                Description = s.Description
            }).ToList();
        }

        public int GetRowsCountSearchByEmail(string textsearch)
        {
            return db.User.FromSqlRaw($"SELECT * FROM [user] WHERE email {search} like '%{textsearch}%' {search}").AsNoTracking().Where(s => s.Status).Count();
        }

        public List<UserView> SearchByPhone(int page, string textsearch)
        {
            int start = size * (page - 1);
            return db.User.FromSqlRaw($"SELECT * FROM [user] WHERE phone {search} like '%{textsearch}%' {search}").AsNoTracking().Where(s => s.Status).OrderByDescending(s => s.Id).Skip(start).Take(size).Select(s => new UserView
            {
                Id = s.Id,
                Email = s.Email,
                Name = s.Name,
                Password = s.Password,
                Phone = s.Phone.Trim(),
                Address = s.Address,
                Photo = s.Photo,
                Birthday = (DateTime)s.Birthday,
                DayCreate = s.DayCreate,
                DayEdited = s.DayEdited,
                Gender = (byte)s.Gender,
                Active = s.Active,
                Status = s.Status,
                Role = s.Role,
                Description = s.Description
            }).ToList();
        }

        public int GetRowsCountSearchByPhone(string textsearch)
        {
            return db.User.FromSqlRaw($"SELECT * FROM [user] WHERE phone {search} like '%{textsearch}%' {search}").AsNoTracking().Where(s => s.Status).Count();
        }

        public List<UserView> SearchByAddress(int page, string textsearch)
        {
            int start = size * (page - 1);
            return db.User.FromSqlRaw($"SELECT * FROM [user] WHERE address {search} like '%{textsearch}%' {search}").AsNoTracking().Where(s => s.Status).OrderByDescending(s => s.Id).Skip(start).Take(size).Select(s => new UserView
            {
                Id = s.Id,
                Email = s.Email,
                Name = s.Name,
                Password = s.Password,
                Phone = s.Phone.Trim(),
                Address = s.Address,
                Photo = s.Photo,
                Birthday = (DateTime)s.Birthday,
                DayCreate = s.DayCreate,
                DayEdited = s.DayEdited,
                Gender = (byte)s.Gender,
                Active = s.Active,
                Status = s.Status,
                Role = s.Role,
                Description = s.Description
            }).ToList();
        }

        public int GetRowsCountSearchByAddress(string textsearch)
        {
            return db.User.FromSqlRaw($"SELECT * FROM [user] WHERE address {search} like '%{textsearch}%' {search}").AsNoTracking().Where(s => s.Status).Count();
        }

        public UserView Login(UserView userView)
        {
            string passHash = MD5_Sang.Encrypt(userView.Password.Trim());
            return db.User.AsNoTracking().Where(s => s.Email.ToLower().Trim() == userView.Email.ToLower().Trim() && s.Password == passHash && s.Status && s.Active && s.Status).Select(s => new UserView
            {
                Id = s.Id,
                Email = s.Email,
                Name = s.Name,
                Phone = s.Phone,
                Address = s.Address,
                Photo = s.Photo,
                Birthday = (DateTime)s.Birthday,
                DayCreate = s.DayCreate,
                DayEdited = s.DayEdited,
                Gender = (byte)s.Gender,
                Role = s.Role,
                Description = s.Description
            }).SingleOrDefault();
        }

        public UserView LoginAdmin(UserView userView)
        {
            string passHash = MD5_Sang.Encrypt(userView.Password.Trim());
            return db.User.AsNoTracking().Where(s => s.Email.ToLower().Trim() == userView.Email.ToLower().Trim() && s.Password == passHash && s.Role == 0 && s.Status && s.Active && s.Status).Select(s => new UserView
            {
                Id = s.Id,
                Email = s.Email,
                Name = s.Name,
                Phone = s.Phone,
                Address = s.Address,
                Photo = s.Photo,
                Birthday = (DateTime)s.Birthday,
                DayCreate = s.DayCreate,
                DayEdited = s.DayEdited,
                Gender = (byte)s.Gender,
                Role = s.Role,
                Description = s.Description
            }).SingleOrDefault();
        }

        public string InsertCodeForgotPW(string email)
        {
            try
            {
                User user = db.User.Where(s => s.Email.ToLower() == email.Trim().ToLower()).SingleOrDefault();
                user.Forgotpw = Guid.NewGuid().ToString("n").Substring(0, 8);
                db.SaveChanges();
                return user.Email + "-" + user.Forgotpw;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public UserView CompareCodeChangePW(string email, string code)
        {
            UserView user = db.User.AsNoTracking().Where(s => s.Email.ToLower() == email.ToLower().Trim() && s.Forgotpw == code).Select(s => new UserView
            {
                Id = s.Id,
                Password = s.Password,
            }).SingleOrDefault();
            return user;
        }

        public bool UpdatePassword(UserView userView)
        {
            try
            {
                User user = db.User.Find(userView.Id);
                user.Password = MD5_Sang.Encrypt(userView.Password);
                user.Forgotpw = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        private bool CheckExists(UserView userView, DBContext db)
        {
            if (userView.Role == 0 || userView.Role != 1 || userView.Role != 2) return false;// Kiểm tra nếu quyền user là admin hoặc ko đúng với những quyền qui định thì false;
            User user = db.User.AsNoTracking().Where(s => s.Email.ToLower().Trim() == userView.Email.ToLower().Trim() && s.Phone.ToLower().Trim() == s.Phone.ToLower().Trim() && s.Id != userView.Id).SingleOrDefault();
            return user == null ? true : false;
        }
    }
}
