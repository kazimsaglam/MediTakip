public class PrescriptionDto
{
    public int PrescriptionId { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public string Diagnosis { get; set; }
    public DateTime PrescriptionDate { get; set; }
}
