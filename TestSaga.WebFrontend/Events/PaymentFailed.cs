namespace TestSaga.Events;

public class PaymentFailed
{
	public Guid OrderId { get; set; }
	public string FailureReason { get; set; }
}