namespace TestSaga.OrderService.Requests;

public class OrderRequest
{
	public string ProductId { get; set; }
	public int Quantity { get; set; }
}