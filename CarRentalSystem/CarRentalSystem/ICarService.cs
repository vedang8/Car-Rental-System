using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data;
using System.Text;
using System.Runtime.ConstrainedExecution;

namespace CarRentalSystem
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICarService
    {
        [OperationContract]
        DataSet GetCars();

        [OperationContract]
        Car GetCar(int id);

        [OperationContract]
        bool AddCar(Car car);

        [OperationContract]
        bool EditCar(Car car);

        [OperationContract]
        bool DeleteCar(int id);

        [OperationContract]
        string GetCarName(int id);

        [OperationContract]
        DataSet GetCarsByModel(string model);

        [OperationContract]
        DataSet GetCarsByFuelType(string fuel_type);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "CarRentalSystem.ContractType".

}
