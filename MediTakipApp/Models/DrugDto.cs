public class DrugDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Barcode { get; set; } = "";
    public string ActiveIngredient { get; set; } = "";
    public string UsageAge { get; set; } = "";
    public decimal Price { get; set; }
    public bool IsPrescription { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }


    public int TotalStock { get; set; }
}
