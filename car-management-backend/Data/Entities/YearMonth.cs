namespace car_management_backend.Data.Entities
{
    public class YearMonth
    {
        public int Id { get; set; }
        public int Year {  get; set; }
        public int Month { get; set; }
        public bool LeapYear { get; set; }
        public int MonthValue { get; set; }
    }
}
