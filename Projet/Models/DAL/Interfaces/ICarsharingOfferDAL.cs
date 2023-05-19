namespace Projet.Models.DAL.Interfaces
{
    public interface ICarsharingOfferDAL
    {
        public int Save(CarsharingOffer offer,int car_id);
        public void SavePath(int offer_id, int city_id, int order);
		public List<CarsharingOffer> TakeOffers();
	}
}
