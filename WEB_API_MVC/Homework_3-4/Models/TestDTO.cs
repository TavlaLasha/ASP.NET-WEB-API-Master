using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Homework_3_4.Models
{
    [DataContract]
    public class TestDTO
    {
        [DataMember]
        public string Fullname;  // serialized

        [DataMember]
        public string PersonalNumber;

        // Not serialized (read-only)
        public string PhoneNumber { get { return PhoneNumber; } }
    }
}