When deferring, the message will have similar headers compared to a _send_, but will be delivered later.

In NServiceBus version 7.7 and above, the `DeliverAt` header will also be added containing the time when the message was targeted to be delivered.
