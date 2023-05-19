using System.ComponentModel.DataAnnotations;

namespace Projet.Models
{
    public abstract class Carsharing
    {
        protected DateTime date;
        protected City departure;
        protected City arrival;

		[Required(ErrorMessage = "Field required")]
		[DataType(DataType.DateTime, ErrorMessage = "Incorrect Date")]
		public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public City Departure
        {
            get { return departure; }
            set { departure = value; }
        }
        public City Arrival
        {
            get { return departure; }
            set { departure = value; }
        }
        public Carsharing(City departure,City arrival)
        {
            this.departure = departure;
            this.arrival = arrival;
            date = DateTime.Now;
        }
        public Carsharing(){}
    }
}
