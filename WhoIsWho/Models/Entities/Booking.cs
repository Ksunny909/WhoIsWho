namespace WhoIsWho.Models.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime Date { get; set; }
        public string PhoneNumber { get; set; }
        public string Text { get; set; }
    }
}
