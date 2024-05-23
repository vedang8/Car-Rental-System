using System;
using System.ServiceModel;
using System.Data;

namespace CarRentalSystem
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        DataSet GetUser();

        [OperationContract]
        User GetUserByEmail(string email);

        [OperationContract]
        bool AddUser(User user);

        [OperationContract]
        bool RemoveUser(int userId);

        [OperationContract]
        string GetUserName(int userId);
    }
}
