using Projet.Models.DAL;
using Projet.Models.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Security.Claims;

namespace Projet.Models
{
    public class User
    {
        private string? lastname;
        private string? firstname;
        private string? street_name;
        private string? postal_code;
        private string? city;
        private int? house_number;
        private string email;
        private string password;
        public List<Car> cars;
        [Required(ErrorMessage = "Field required")]
        [DataType(DataType.Text, ErrorMessage = "Incorect lastname")]
        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }
        [Required(ErrorMessage = "Field required")]
        [DataType(DataType.Text, ErrorMessage = "Incorect firstname")]
        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }
        [Required(ErrorMessage = "Field required")]
        [DataType(DataType.Text, ErrorMessage = "Incorect street name")]
        public string Street_name
        {
            get { return street_name; }
            set { street_name = value; }
        }
        [Required(ErrorMessage = "Field required")]
        [DataType(DataType.Text, ErrorMessage = "Incorect postal code")]
        public string Postal_code
        {
            get { return postal_code; }
            set { postal_code = value; }
        }
        [Required(ErrorMessage = "Field required")]
        [DataType(DataType.Text, ErrorMessage = "Incorect city")]
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        [Required(ErrorMessage = "Field required")]
        [Range(0, int.MaxValue, ErrorMessage = "Your house number cannot be under 0")]
        public int? House_number
        {
            get { return house_number; }
            set { house_number = value; }
        }
        [Required(ErrorMessage = "Field required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Incorect email")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        [Required(ErrorMessage = "Field required")]
        [DataType(DataType.Password, ErrorMessage = "Incorect password")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public List<Car> Cars
        {
            get { return cars; }
            //set { cars = value; }
        }
        public User()
        {
            cars = new List<Car>();
        }
        public User(string password,string email)
        {
            this.password = password;
            this.email = email;
        }
        public User(string lastname,string firstname,string streetname,string postalcode,string city,int housenumber,string email,string password) 
        {
            this.lastname = lastname;
            this.firstname = firstname;
            this.street_name = streetname;
            this.postal_code= postalcode;
            this.city = city;
            this.house_number= housenumber;
            this.email = email;
            this.password = password;
        }
        //public User(string lastname, string firstname, string adresse, Car car) : this(lastname,firstname,adresse)
        //{
        //    //cars = new List<Car>();
        //    cars.Add(car);
        //}
        public int VerifyIdentifiers(IUserDAL dal)
        {
            try
            {
                return dal.VerifyIdentifiers(this);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void WarnDriver()
        //{

        //}
        //public List<Car> RequestUserCars()
        //{

        //}
        public void AddCar(Car car)
        {
            cars.Add(car);
        }
        public static User LoadUser(IUserDAL userDAL, int user_id)
        {
            return userDAL.GetUser(user_id);
        }
        public List<Car> RequestUserCars(ICarDAL carDAL,int user_id)
        {
            List<Car> cars = carDAL.GetUserCar(user_id);
            this.cars = cars;
            return cars;
        }
        public int Save(IUserDAL userDal,ICarDAL carDal)
        {
            
            userDal.InsertInto(this);
            int user_id = userDal.TakeUserId(this);
            foreach(Car car in cars)
            {
                car.Save(carDal,user_id);
            }
            return user_id;
        }
    }
}
