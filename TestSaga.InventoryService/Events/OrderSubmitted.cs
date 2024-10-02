namespace TestSaga.Events;

public class OrderSubmitted
{
	public Guid OrderId { get; set; }
	public string ProductId { get; set; }
	public int Quantity { get; set; }
	public DateTime OrderDate { get; set; }
}
