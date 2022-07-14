A logical subscriber is defined by its endpoint name. In a data distribution scenario, each physical subscriber has to act as if it were a unique logical subscriber. That is, it has to be given a unique endpoint name. Usually that name consists of the logical name e.g. `WebFrontend` and a suffix which can be either read from a configuration file or obtained from the environment (e.g. an Azure role instance ID). This pattern is described in more detail in the [data distribution sample](/samples/routing/data-distribution/).