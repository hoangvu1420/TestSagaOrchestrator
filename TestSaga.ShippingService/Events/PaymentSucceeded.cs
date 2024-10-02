namespace TestSaga.Events;

public class PaymentSucceeded
{
	public Guid OrderId { get; set; }
}
