namespace TestSaga.Events;

public class InventoryAvailable
{
	public Guid OrderId { get; set; }
}