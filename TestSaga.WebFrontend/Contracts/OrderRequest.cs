namespace TestSaga.WebFrontend.Contracts;

public class OrderRequest
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
}