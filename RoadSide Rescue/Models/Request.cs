using System.ComponentModel.DataAnnotations;

namespace RoadSide_Rescue.Models
{
    class Request
    {
        [Key] public int RequestId { get; set; }
        public string DriverId { get; set; }
        public string? ServiceProviderId { get; set; }
        public string? ServiceProviderVehicleId { get; set; }
        public string? DriverVehicleId { get; set; }
        public string ProblemDescription { get; set; }
        public string LocationId { get; set; }
        public string Status { get; set; } 
        public DateTime TimeStamp { get; set; }
    }
}
