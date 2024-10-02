using MassTransit;
using TestSaga.Commands;
using TestSaga.Events;
using TestSaga.SagaOrchestrator.SagaStates;

namespace TestSaga.SagaOrchestrator.StateMachines;

public class OrderStateMachine : MassTransitStateMachine<OrderSagaState>
{
	public State? OrderSubmitted { get; private set; }
	public State? InventoryChecked { get; private set; }
	public State? PaymentProcessed { get; private set; }
	public State? OrderShipped { get; private set; }

	public State? InventoryFailed { get; private set; }
	public State? PaymentFailed { get; private set; }

	public Event<OrderSubmitted>? OrderSubmittedEvent { get; private set; }
	public Event<InventoryAvailable>? InventoryAvailableEvent { get; private set; }
	public Event<InventoryUnavailable>? InventoryUnavailableEvent { get; private set; }
	public Event<PaymentSucceeded>? PaymentSucceededEvent { get; private set; }
	public Event<PaymentFailed>? PaymentFailedEvent { get; private set; }
	public Event<OrderShipped>? OrderShippedEvent { get; private set; }

	public OrderStateMachine()
	{
		InstanceState(x => x.CurrentState); // Configuring the state field

		Event(() => OrderSubmittedEvent, x => x.CorrelateById(context => context.Message.OrderId));
		Event(() => InventoryAvailableEvent, x => x.CorrelateById(context => context.Message.OrderId));
		Event(() => InventoryUnavailableEvent, x => x.CorrelateById(context => context.Message.OrderId));
		Event(() => PaymentSucceededEvent, x => x.CorrelateById(context => context.Message.OrderId));
		Event(() => PaymentFailedEvent, x => x.CorrelateById(context => context.Message.OrderId));
		Event(() => OrderShippedEvent, x => x.CorrelateById(context => context.Message.OrderId));

		Initially(
			When(OrderSubmittedEvent)
				.Then(context => Console.WriteLine($"Order Saga initialized, CorrelationId: {context.Saga.CorrelationId}"))
				.TransitionTo(OrderSubmitted)
		);

		During(OrderSubmitted,
			When(InventoryAvailableEvent)
				.TransitionTo(InventoryChecked),
			When(InventoryUnavailableEvent)
				.TransitionTo(InventoryFailed)
				//.Then(context => Console.WriteLine($"Inventory failed for Order: {context.Message.OrderId}, Reason: {context.Message.Reason}"))
				.ThenAsync(CancelOrder)
		);

		During(InventoryChecked,
			When(PaymentSucceededEvent)
				.TransitionTo(PaymentProcessed),
			When(PaymentFailedEvent)
				.TransitionTo(PaymentFailed)
				//.Then(context => Console.WriteLine($"Payment failed for Order: {context.Message.OrderId}, Reason: {context.Message.FailureReason}"))
				.ThenAsync(CancelOrder)
		);

		During(PaymentProcessed,
			When(OrderShippedEvent)
				//.Then(context => Console.WriteLine($"Order OrderShipped: {context.Message.OrderId}"))
				.TransitionTo(OrderShipped)
				.Finalize()
		);

		SetCompletedWhenFinalized();
	}

	private async Task CancelOrder<TEvent>(BehaviorContext<OrderSagaState, TEvent> context) where TEvent : class
	{
		// Use context.Publish to publish events, since it will have the correct scoped dependencies
		await context.Publish(new CancelOrder
		{
			OrderId = context.Saga.CorrelationId
		});

		Console.WriteLine($"Published CancelOrder command for order: {context.Saga.CorrelationId}");
	}
}