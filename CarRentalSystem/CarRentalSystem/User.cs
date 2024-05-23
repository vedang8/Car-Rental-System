using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CarRentalSystem
{
    [DataContract]
    public class User
    {
        public int Id;
        public string FirstName;
        public string MiddleName;
        public string LastName;
        public string Email;
        public string Phone;
        public string Address;
        public string Password;
        public bool isAdmin;

        [DataMember]
        public int UserId
        {
            get { return Id; }
            set { Id = value; }
        }

        [DataMember]
        public string firstname
        {
            get { return FirstName; }
            set { FirstName = value; }
        }

        [DataMember]
        public string middlename
        {
            get { return MiddleName; }
            set { MiddleName = value; }
        }

        [DataMember]
        public string lastname
        {
            get { return LastName; }
            set { LastName = value; }
        }

        [DataMember]
        public string email
        {
            get { return Email; }
            set { Email = value; }
        }

        [DataMember]
        public string phone
        {
            get { return Phone; }
            set { Phone = value; }
        }

        [DataMember]
        public string address
        {
            get { return Address; }
            set { Address = value; }
        }

        [DataMember]
        public string password
        {
            get { return Password; }
            set { Password = value; }
        }

        [DataMember]
        public bool isadmin
        {
            get { return isAdmin; }
            set { isAdmin = value; }
        }
    }
}
