using System;
using System.Collections.Generic;
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
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CountryCode { get; set; }
    }
    public class Booking
    {
        public int BookingID { get; set; }
        public int ClientID { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; } 
    }

    public enum BookingStatus
    {
        Scheduled,
        Completed,
        Cancelled
    }

    public  class Services
    {
        public  int ServiceID { get; set; }
        public  string ServiceName { get; set; }
        public  decimal HourlyRate { get; set; }
    }
    public class BookingDetailsViewModel
    {
        public int ServiceID { get; set; }
        public int HoursRendered { get; set; }

        public Services Service { get; set; }
        public decimal Amount => HoursRendered * (Service?.HourlyRate ?? 0);
    }
    public class BookingDetails
    {
        public int BookingDetailID { get; set; }
        public int BookingID { get; set; }
        public int ServiceID { get; set; }
        public int HoursRendered { get; set; }
    }
    public class Billing
    {
        public  int BillingID { get; set; }
        public int BookingID { get; set; }
        public DateTime BillingDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public List<Payment> Payments { get; set; } = new List<Payment>();

        public void AddPayment(Payment payment)
        {
            Payments.Add(payment);
        }

        public decimal TotalAmount => Payments.Sum(p => p.AmountPaid);
    }

    public enum PaymentStatus
    {
        Pending,
        Paid
    }
    public class Payment
    {
        public int PaymentID { get; set; }
        public int BillingID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }
    }
    
    public class Inventory
    {
        public int InventoryID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
    }

    public class Connection
    {
        public static string Database { get; } = "Data Source=DESKTOP-NF4HS4J;Initial Catalog=ConstructionServices;Integrated Security=True;TrustServerCertificate=True";

    }
}
