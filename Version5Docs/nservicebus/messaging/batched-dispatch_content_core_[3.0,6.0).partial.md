Batched dispatch isn't available for Versions 5 and below. It is necessary to pay more attention to the ordering of outgoing operations when using transports other than MSMQ and SQLServer because they lack support for cross queue transactions. For those transports, messages will be dispatched immediately to the transport as soon as the call to `.Send` or `.Publish` completes. This means that there is a risk of a "ghost" message being emitted if all database operations are not performed prior to bus operations. One example would of this would be to call `.Publish(new OrderPlaced())` before calling `DB.Store(new Order())` since that would cause the `OrderPlaced` event to be sent even if the order could not be stored in the database.

To avoid ghost messages there are the following options:

 * Send/publish messages only after all storage operations have completed. This would have to be enforced through code reviews, and it can be hard to detect the problem when there are multiple message handlers for the same message. See [message handler ordering](/nservicebus/handlers/handler-ordering.md) for more details on how to make sure handlers are called in a deterministic way.
 * Turn on the [Outbox](/nservicebus/outbox) feature to ensure that outgoing operations are not dispatched until all handlers have completed successfully.
 * Switch to either the [MSMQ Transport](/transports/msmq/) or [SqlServer Transport](/transports/sql/).