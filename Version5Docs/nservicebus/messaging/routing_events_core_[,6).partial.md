Subscribe message routing can be configured in a configuration section by registering publishers for a given type, namespace or assembly containing events.

In the `UnicastBusConfig/MessageEndpointMappings` configuration section, publishers are registered in the same way as the command destinations are defined. If a given assembly or namespace contains both events and commands, the mapping will recognize that fact and configure the routing correctly (both commands and subscribe messages will be routed to the destination specified in the `Endpoint` attribute).

`MessageEndpointMappings` routing configuration can also take advantage of message inheritance: base types "inherit" routes from the derived types (opposite to member inheritance in .NET). If both `EventOne` and `EventTwo` inherit from `BaseEvent`, when subscribing to `BaseEvent` the subscription messages are sent to publishers of both `EventOne` and `EventTwo`.
