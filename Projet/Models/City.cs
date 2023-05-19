using Projet.Models.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Projet.Models
{
    public class City
    {
        private string name;
        private DateTime passageHour;
		[Required(ErrorMessage = "Field required")]
		[DataType(DataType.Text, ErrorMessage = "Incorrect city name")]
		public string Name
        {
            get { return name; }
            set { name = value; }
        }
		[Required(ErrorMessage = "Field required")]
		[DataType(DataType.DateTime, ErrorMessage = "Incorrect passage hour")]
		public DateTime PassageHour
        {
            get { return passageHour; }
            set { passageHour = value; }
        }
        public City(string name, DateTime passageHour)
        {
            Name = name;
            PassageHour = passageHour;
        }
        public City(string name)
        {
            Name = name;
            passageHour = DateTime.Now;
        }
        public City(){}

        //public List<City> GetCitys()
        //{

        //}
        public static City GetDepartureCity(ICityDAL CityDal,int carsharing_id)
        {
            return CityDal.GetDepartureCity(carsharing_id);
        }
        public static City GetArrivalCity(ICityDAL CityDal, int carsharing_id)
        {
            return CityDal.GetDepartureCity(carsharing_id);
        }
        public int Save(ICityDAL cityDAl)
        {
            return cityDAl.Save(this);
        }
		public int Get(ICityDAL cityDAL)
		{
			return cityDAL.TakeCityId(this);
		}
	}
}
