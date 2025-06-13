namespace LearningCenterPlatform.Profiles.Domain.Model.ValueObjects
{
    public class EmailAddress
    {
        public string email;
        public string Address;

        public EmailAddress(string email) { 
            this.email = email;
        }
        public EmailAddress() {
            email = "";
            Address = "";
        }
    }
}
