using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using WebService.ServiceModel.Types;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;

namespace WebService.ServiceModel.Operations
{
    [DataContract]
    [Route("/json/user")]
    public class Users
    {
    }

    [DataContract]
    [Route("/json/createUser", "POST")]
    public class CreateUser
    {
        [DataMember]
        public string nric { get; set; }
        [DataMember]
        public string pwd { get; set; }
        [DataMember]
        public string fullname { get; set; }
        [DataMember]
        public int age { get; set; }
        [DataMember]
        public DateTime dob { get; set; }
        [DataMember]
        public string gender { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public int hp { get; set; }
        [DataMember]
        public bool notification { get; set; }

        public CreateUser(string nric, string pwd, string fullname, int age, DateTime dob, string gender, string email, int hp, bool notification)
        {
            this.nric = nric;
            this.pwd = pwd;
            this.fullname = fullname;
            this.age = age;
            this.dob = dob;
            this.gender = gender;
            this.email = email;
            this.hp = hp;
            this.notification = notification;
        }
    }

    [DataContract]
    [Route("/json/user/{nric}")]
    public class GetUserDetails
    {
        [DataMember]
        public string nric { get; set; }
    }

    [DataContract]
    [Route("/json/updateUser", "POST")]
    public class UpdateUser
    {
        [DataMember]
        public string nric { get; set; }
        [DataMember]
        public string pwd { get; set; }
        [DataMember]
        public string fullname { get; set; }
        [DataMember]
        public int age { get; set; }
        [DataMember]
        public DateTime dob { get; set; }
        [DataMember]
        public string gender { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public int hp { get; set; }
        [DataMember]
        public bool notification { get; set; }

        public UpdateUser(string nric, string pwd, string fullname, int age, DateTime dob, string gender, string email, int hp, bool notification)
        {
            this.nric = nric;
            this.pwd = pwd;
            this.fullname = fullname;
            this.age = age;
            this.dob = dob;
            this.gender = gender;
            this.email = email;
            this.hp = hp;
            this.notification = notification;
        }
    }

    [DataContract]
    public class UserResult : IHasResponseStatus
    {
        public UserResult()
        {
            this.User = new List<User>();
        }

        [DataMember]
        public List<User> User { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    [DataContract]
    public class CreateUserResult : IHasResponseStatus
    {
        public CreateUserResult()
        {
            this.User = User;
        }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    [DataContract]
    public class UserDetailsResult : IHasResponseStatus
    {
        public UserDetailsResult()
        {
            this.User = User;
        }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    [DataContract]
    public class UpdateUserResult : IHasResponseStatus
    {
        public UpdateUserResult()
        {
            this.User = User;
        }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
}