namespace gnisbdback.Models.Domain
{
    public class MeetingMInutesMaster
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public DateTime DateandTime { get; set; }

        public string MeetingPlace { get; set; }

        public string ClientSide { get; set; }

        public string HostSide { get; set; }
    }
}
