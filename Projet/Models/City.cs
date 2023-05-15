namespace Projet.Models
{
    public class City
    {
        private string name;
        private DateTime passageHour;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
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

        //public List<City> GetCitys()
        //{

        //}
    }
}
