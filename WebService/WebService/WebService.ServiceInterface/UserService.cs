using System;
using System.Net;
using System.Collections.Generic;
using WebService.ServiceModel.Types;
using WebService.ServiceModel.Operations;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;
using ServiceStack.Common.Web;

namespace WebService.ServiceInterface
{
    public class UserService : Service
    {
        //Get all user data
        public UserResult Get(Users request)
        {
            return new UserResult { User = Db.Select<User>() };
        }

        //Insert 1 user object
        public CreateUserResult Post(CreateUser request)
        {
            Db.ExecuteSql("INSERT INTO user (nric, pwd, fullname, age, dob, gender, email, hp, notification) VALUES ('" + request.nric + "', '" + request.pwd + "', '" + request.fullname + "', '" + request.age + "', '" + request.dob + "', '" + request.gender + "', '" + request.email + "', '" + request.hp + "', '" + request.notification + "')");
            User e = Db.GetByIdOrDefault<User>(request.nric);
            if (e == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, new ArgumentException("User does not exist: " + request.nric.ToString()));
            }
            return new CreateUserResult { User = e };
        }

        //Retrieve 1 user object by id
        public UserDetailsResult Get(GetUserDetails request)
        {
            var eve = Db.GetByIdOrDefault<User>(request.nric);
            if (eve == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, new ArgumentException("User does not exist: " + request.nric));
            }
            return new UserDetailsResult { User = eve };
        }

        //Insert 1 user object
        public UpdateUserResult Post(UpdateUser request)
        {
            Db.ExecuteSql("UPDATE user SET pwd='" + request.pwd + "', fullname='" + request.fullname + "', age='" + request.age + "', dob='" + request.dob + "', gender='" + request.gender + "', email='" + request.email + "', hp='" + request.hp + "', notification='" + request.notification + "' WHERE nric='" + request.nric + "'");
            User e = Db.GetByIdOrDefault<User>(request.nric);
            if (e == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, new ArgumentException("User does not exist: " + request.nric.ToString()));
            }
            return new UpdateUserResult { User = e };
        }
    }
}
