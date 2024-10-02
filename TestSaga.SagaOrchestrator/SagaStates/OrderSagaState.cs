using MassTransit;

namespace TestSaga.SagaOrchestrator.SagaStates;

public class OrderSagaState : SagaStateMachineInstance
{
	public Guid CorrelationId { get; set; }  // Saga correlation ID
	public string CurrentState { get; set; }  // Current state of the saga
	public DateTime OrderDate { get; set; }   // Order date (can store other info)
	public string OrderId { get; set; }       // ID of the order being processed
}