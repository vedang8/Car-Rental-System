using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    [DataContract]
    public class Booking
    {
        public int Id;
        public int user_id;
        public int car_id;
        public DateTime start_date;
        public DateTime end_date;
        public DateTime booking_date;
        public int price;
        public bool driver_required;

        [DataMember]
        public int BookingId
        {
            get { return Id; }
            set { Id = value; }
        }

        [DataMember]
        public int UserId
        {
            get { return user_id; }
            set { user_id = value; }
        }

        [DataMember]
        public int CarId
        {
            get { return car_id; }
            set { car_id = value; }
        }

        [DataMember]
        public DateTime StartDate
        {
            get { return start_date; }
            set { start_date = value; }
        }

        [DataMember]
        public DateTime EndDate
        {
            get { return end_date; }
            set { end_date = value; }
        }

        [DataMember]
        public DateTime BookingDate
        {
            get { return booking_date; }
            set { booking_date = value; }
        }

        [DataMember]
        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        [DataMember]
        public bool DriverRequired
        {
            get { return driver_required; }
            set { driver_required = value; }
        }
    }
}
