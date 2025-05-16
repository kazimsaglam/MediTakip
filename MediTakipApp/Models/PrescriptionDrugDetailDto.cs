public class PrescriptionDrugDetailDto
{
    public int DrugId { get; set; }
    public string Name { get; set; } = "";
    public int Quantity { get; set; }
    public string Dosage { get; set; } = "";
    public string UsagePeriod { get; set; } = "";
}