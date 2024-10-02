namespace TestSaga.Events;

public class InventoryUnavailable
{
	public Guid OrderId { get; set; }
	public string Reason { get; set; }
}