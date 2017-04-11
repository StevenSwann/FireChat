using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FireChatCore;

namespace WCF2ndFireChat
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
        [OperationContract]
        List<FireChatCore.User> GetUsers();

        [OperationContract]
        void AddUser(FireChatCore.User user);

        [OperationContract]
        void EditUser(FireChatCore.User user);

        [OperationContract]
        void DeleteUser(FireChatCore.User user);

        [OperationContract]
        FireChatCore.User GetUserById(int id);

        [OperationContract]
        FireChatCore.User GetUserByUsername(string username);

        [OperationContract]
        bool UserExists(FireChatCore.User user);

        [OperationContract]
        bool EmailExists(FireChatCore.User user);

        [OperationContract]
        bool PasswordMatch(FireChatCore.User user);

        [OperationContract]
        bool IsAdmin(FireChatCore.User user);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    //[DataContract]
    //public class user
    //{
    //    [DataMember]
    //    public int user_id { get; set; }
    //    [DataMember (IsRequired=true)]
    //    public string username { get; set; }
    //    [DataMember(IsRequired = true)]
    //    public string password { get; set; }
    //    [DataMember(IsRequired = true)]
    //    public string email { get; set; }
    //    [DataMember(IsRequired = true)]
    //    public Nullable<bool> is_admin { get; set; }
    //}
}
