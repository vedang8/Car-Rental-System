using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    [ServiceContract]
    public interface IBookingService
    {
        [OperationContract]
        DataSet GetAllBooking();

        [OperationContract]
        int AddBooking(Booking booking);

        [OperationContract]
        Booking GetBookingById(int bookingId);

        [OperationContract]
        bool RemoveBooking(int bookingId);

        [OperationContract]
        Booking[] GetBookingByUserId(int userId);

        [OperationContract]
        Booking[] GetBookingByCarId(int carId);

        [OperationContract]
        DataSet FindAvailableCars(DateTime startDate, DateTime endDate);

        [OperationContract]
        DataSet GetBookingsByFuelType(string fuelType);
    }
}
