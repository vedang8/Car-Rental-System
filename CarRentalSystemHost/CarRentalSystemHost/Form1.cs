using CarRentalSystem;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows.Forms;

namespace CarRentalSystemHost
{
    public partial class Form1 : Form
    {
        ServiceMetadataBehavior metadataBehavior = new ServiceMetadataBehavior();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Uri tcpa = new Uri("net.tcp://localhost:8000/TcpBinding");
                 
                //Add user service endpoint
                ServiceHost userHost = new ServiceHost(typeof(UserService), tcpa);
                NetTcpBinding tcpb = new NetTcpBinding();
                userHost.Description.Behaviors.Add(metadataBehavior);
                userHost.AddServiceEndpoint(typeof(IMetadataExchange),
                MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
                userHost.AddServiceEndpoint(typeof(IUserService), tcpb, tcpa);

                //Add car service endpoint
                Uri carTcpUri = new Uri("net.tcp://localhost:8001/CarTcpBinding");
                ServiceHost carHost = new ServiceHost(typeof(CarService), carTcpUri);
                carHost.Description.Behaviors.Add(metadataBehavior);
                carHost.AddServiceEndpoint(typeof(IMetadataExchange),
                    MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
                carHost.AddServiceEndpoint(typeof(ICarService), tcpb, carTcpUri);

                //Add booking service endpoint
                Uri bookingTcpUri = new Uri("net.tcp://localhost:8002/BookingTcpBinding");
                ServiceHost bookingHost = new ServiceHost(typeof(BookingService), bookingTcpUri);
                bookingHost.Description.Behaviors.Add(metadataBehavior);
                bookingHost.AddServiceEndpoint(typeof(IMetadataExchange),
                    MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
                bookingHost.AddServiceEndpoint(typeof(IBookingService), tcpb, bookingTcpUri);

                //Open all service hosts
                userHost.Open();
                carHost.Open();
                bookingHost.Open();

                label1.Text = "Server Running";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
