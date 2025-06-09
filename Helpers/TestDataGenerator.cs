using Bogus;


namespace AE_extensive_project.TestData
{
    public static class TestDataGenerator
    {
        public class UserTestData
        {
            public static readonly Faker faker = new Faker();
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Title {  get; set; }
            public int BirthYear { get; set; }
            public int BirthMonth {  get; set; }
            public int BirthDay { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Company { get; set; }
            public string MobileNumber { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Country { get; set; }
            public string State {get; set;}
            public string City { get; set; }
            public string Zipcode { get; set; }

        }
        public static UserTestData GenerateUser()
        {
            var faker = new Faker("en");

            // Step 1: Generate the BirthDate first
            DateTime birthDate = faker.Date.Past(60, DateTime.Today.AddYears(-18));

            return new UserTestData
            {
                BirthYear = birthDate.Year,
                BirthMonth = birthDate.Month,
                BirthDay = birthDate.Day,
                FirstName = faker.Name.FirstName(),
                LastName = faker.Name.LastName(),
                Title = faker.Name.Prefix(),
                Email = faker.Internet.Email(),
                Password = faker.Internet.Password(10, true),
                Company = faker.Company.CompanyName(),
                MobileNumber = faker.Phone.PhoneNumber("555-###-####"),
                Address1 = faker.Address.Locale,
                Address2 = faker.Address.Locale,
                Country = "US",
                Zipcode = "77036",
                State = "aRIZZona",
                City = "Phoenix",
            };
        }
    }
}
