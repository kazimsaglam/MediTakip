namespace MediTakipApp.Utils
{
    public static class SelectedPatient
    {
        public static int Id { get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string TcNo { get; set; }
        public static string Insurance { get; set; }
        public static DateTime BirthDate { get; set; }
        public static string Gender { get; set; }
        public static string City { get; set; }
        public static string District { get; set; }
        public static string Phone { get; set; }

        public static string FullName => $"{FirstName} {LastName}";

        public static void Clear()
        {
            Id = 0;
            FirstName = LastName = TcNo = Insurance = Gender = City = District = Phone = string.Empty;
            BirthDate = DateTime.MinValue;
        }
    }
}
