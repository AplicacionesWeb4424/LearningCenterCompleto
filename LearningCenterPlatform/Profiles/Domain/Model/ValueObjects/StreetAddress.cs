namespace LearningCenterPlatform.Profiles.Domain.Model.ValueObjects
{
    public class StreetAddress
    {
        public string Street;
        public string City;
        public string State;
        public string PostalCode;
        public string Country;
        public string FullAddress;
        public string Number;
        public StreetAddress(string street, string Number, string city, string state, string postalCode, string country)
        {
            this.Street = street;
            this.City = city;
            this.Number = Number;
            this.State = state;
            this.PostalCode = postalCode;
            this.Country = country;
            FullAddress = $"{Street}, {City}, {State}, {PostalCode}, {Country}, {Number}";

        }
        public StreetAddress()
        {  this.Street = string.Empty;
            this.City = string.Empty;
            this.State = string.Empty;
            this.PostalCode = string.Empty;
            this.Country = string.Empty;
            this.Number = string.Empty;
            FullAddress = $"{Street}, {City}, {State}, {PostalCode}, {Country}, {Number}";
        }
    }
}
