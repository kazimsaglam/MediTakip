public class PatientDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TcNo { get; set; }
    public string Insurance { get; set; }
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Phone { get; set; }

    // Yeni alan (sadece frontend için kullanıyoruz)
    public string? LastPrescriptionDate { get; set; }
}
