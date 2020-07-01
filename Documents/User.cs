using System;

namespace StrengthAndHonor.Documents
{
    [BsonCollection("users")]
    public class User : Document
    {
        public string Alias { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
