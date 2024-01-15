namespace ClassesLibrary
{
    public class AppUser
    {
        /// <summary>
        /// The main fields of the class that define the user.
        /// </summary>
        private int _customer_id;
        private string _name;
        private string _email;
        private int _age;
        private string _city;
        private bool _is_premium;
        private List<double> _orders;

        /// <summary>
        /// Properties for interacting with class fields.
        /// </summary>
        public int CustomerId { get { return _customer_id; } }
        public string Name { get { return _name; } } 
        public string Email { get { return _email; } }
        public int Age { get { return _age; } }
        public string City { get { return _city; } }
        public bool IsPremium { get { return _is_premium; } }
        public List<double> Orders { get { return _orders; } }

        public AppUser() { }

        /// <summary>
        /// constructor for fields initializing.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="age"></param>
        /// <param name="city"></param>
        /// <param name="is_premium"></param>
        /// <param name="orders"></param>
        public AppUser(int customer_id, string name, string email, int age, string city, bool is_premium, List<double> orders)
        {
            _customer_id = customer_id;
            _name = name;
            _email = email;
            _age = age;
            _city = city;
            _is_premium = is_premium;
            _orders = orders;          
        }
    }
}