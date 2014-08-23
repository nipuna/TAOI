using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entities;

namespace DomainModel.Abstract
{
    public interface IUserRepository
    {
        IQueryable<User> getUsers();

        IQueryable<userDisplay> getUsersForDisplay(IQueryable<User> Users);

        IQueryable<User> getUser(Int32 UserId);

        void editUser(Int32 UserId);

        void saveUser(User user);

        void deleteUser(int UserId);

        void createUser(User newUser);

        Dictionary<string, List<details>> getAllUserEditDetails(User user);

        Dictionary<string, List<details>> getAllUserCreateDetails();

        User editedUserData(User userId, List<Int32> customerChoosen, List<Int32> checkboxtree, List<Int32> cultureChoosen, List<Int32> countryChoosen);

        User createdUserData(User user, List<Int32> customerChoosen, List<Int32> checkboxtree, List<Int32> cultureChoosen, List<Int32> countryChoosen);

        Boolean authentucateUser(string username, string password, out Int32 userId, out string nameOfUser);

        List<userRightdetails> getAllUserRightDetails(User user);
    
    }
}
