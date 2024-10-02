namespace TestSaga.Events;

public class OrderShipped
{
	public Guid OrderId { get; set; }
	public DateTime ShippedDate { get; set; }
}
