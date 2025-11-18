namespace OnlineOrdering
{
    public class Address
    {
        private string _streetAddress;
        private string _city;
        private string _stateProvince;
        private string _country;

        public Address(string street, string city, string state, string country)
        {
            _streetAddress = street;
            _city = city;
            _stateProvince = state;
            _country = country;
        }

        public bool IsInUSA()
        {
            return _country.ToLower() == "usa";
        }

        public string GetFullAddressString()
        {
            return $"{_streetAddress}\n{_city}, {_stateProvince} {_country}";
        }
    }
}