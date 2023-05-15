using Projet.Models.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Projet.Models
{
    public class Car
    {
        private string modelName;
        private int nbrPlaces;
        [Required(ErrorMessage = "Field required")]
        [DataType(DataType.Text, ErrorMessage = "Incorrect Model name")]
        public string ModelName
        {
            get { return modelName; }
            set { modelName = value; }
        }
        [Required(ErrorMessage = "Field required")]
        [Range(0, int.MaxValue, ErrorMessage = "Your car cannot have less than 0 seats.")]
        public int NbrPlaces
        {
            get { return nbrPlaces; }
            set { nbrPlaces = value; }
        }
        //public Car(string modelName, int nbrPlaces)
        //{
        //    ModelName = modelName;
        //    NbrPlaces = nbrPlaces;
        //}

        //public int GetSeats()
        //{
        //    return nbrPlaces;
        //}
        //public static List<Car> GetUserCars()
        //{

        //}
        public void Save(ICarDAL carDAL,int user_id)
        {
            carDAL.InsertInto(this, user_id);
        }
    }
}
