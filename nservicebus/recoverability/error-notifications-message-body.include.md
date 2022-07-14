Since the message could have failed due to a deserialization exception, it is not possible for the API to provide the instance of the message. For the same reason is it recommended that consumers of this API do not attempt to deserialize the message instance. If the message contents are required for debugging purposes, convert the message body to a string using the .NET `Encoding` APIs.