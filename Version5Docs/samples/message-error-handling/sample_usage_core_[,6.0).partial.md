The `IManageMessageFailures` interface allows managing of both serialization exceptions as well as other unhandled exceptions thrown when handling a message. To ensure message loss does not happen, any unhandled exception thrown in the `IManageMessageFailures` interface will cause process hosting NServiceBus to terminate and effectively rollback the transactions.