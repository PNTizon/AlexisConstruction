using AlexisConstruction.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexisConstruction.Classes
{
    public class Models
    {


    }
    public class Client
    {
        public int ClientID { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Fullname { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CountryCode { get; set; }
    }
   
    public class Bookings
    {
        public int BookingID { get; set; }
        public string CustomerName { get; set; }
        public int HoursRendered { get; set; }
        public Services Service { get; set; }
        public string ServiceName => Service?.ServiceName ?? ""; 
        public decimal HourlyRate => Service?.HourlyRate ?? 0;
        public decimal Amount => HoursRendered * HourlyRate;
        public DateTime BillingDate { get; set; }
        public DateTime BookedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; } = "Pending";
        public string PaymentMethod { get; set; } = "Cash";
        public Bookings ()
        {
            PaymentReceipt = new List<Orders>();
        }
        public List<Orders> PaymentReceipt { get; set; }
    }
    public class Services
    {
        public  int ServiceID { get; set; }
        public  string ServiceName { get; set; }
        public  decimal HourlyRate { get; set; }
        public decimal Amount { get; set; }
    }
    public class Inventory
    {
        public int InventoryID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
    }
    public class Connection
    {
        public static string Database { get; } = "Data Source=(local);Initial Catalog=AlexisConstruction;Integrated Security=True;TrustServerCertificate=True";
    }
    public class Orders
    {
        public string ServiceName { get; set; }
        public int HoursRendered { get; set; }
        public int HourlyRate { get; set; }
        public decimal TotalAmount
        {
            get
            {
                return HoursRendered * HourlyRate;
            }
        }
    }
    public class  BookingDetails
    {
        public BookingDetails()
        {
            BookingReceipt = new List<Orders>();
        }
        public List<Orders> BookingReceipt { get; set; }
        public static int ClientID { get; set; }
        public int BookingsID { get; set; }
        public string CustomerName { get; set; }
        public DateTime BillingDate { get; set; }
        public static  DateTime BookedDate { get; set; }
        public string MOP { get; set; }
    }
    public class BillingRecords
    {
        public BillingRecords()
        {
            BookingReceipt = new List<Orders>();
        }
        public List<Orders> BookingReceipt { get; set; }
        public int BookingsID { get; set; }
        public string CustomerName { get; set; }
        public DateTime BillingDate { get; set; }
        public  DateTime BookedDate { get; set; }
        public string MOP { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
 