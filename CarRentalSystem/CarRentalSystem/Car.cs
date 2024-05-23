using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CarRentalSystem
{
    [DataContract]
    public class Car
    {
        public int Id;
        public string car_model;
        public string car_number_plate;
        public int rental_price;
        public int capacity;
        public string fuel_type;
        public int mileage;
        public int driver_cost;
        public string image;

        [DataMember]
        public int CarId
        {
            get { return Id; }
            set { Id = value; }
        }
        [DataMember]
        public string CarModel
        {
            get { return car_model; }
            set { car_model = value; }
        }
        [DataMember]
        public string NumberPlate
        {
            get { return car_number_plate; }
            set { car_number_plate = value; }
        }
        [DataMember]
        public int RentPrice
        {
            get { return rental_price; }
            set { rental_price = value; }
        }
        [DataMember]
        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }
        [DataMember]
        public string FuelType
        {
            get { return fuel_type; }
            set { fuel_type = value; }
        }
        [DataMember]
        public int Mileage
        {
            get { return mileage; }
            set { mileage = value; }
        }

        [DataMember]
        public int DriverCost
        {
            get { return driver_cost; }
            set { driver_cost = value; }
        }

        [DataMember]
        public string Image
        {
            get { return image; }
            set { image = value; }
        }
    }
}
