using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Bson;
using NServiceBus;
using NServiceBus.Jil;
using NServiceBus.MessageMutator;
using NServiceBus.Wire;

static class Program
{
    static async Task Main()
    {
        Console.Title = "Samples.MultipleDeserializers.ReceivingEndpoint";

        #region configAll

        var endpointConfiguration = new EndpointConfiguration("Samples.MultipleDeserializers.ReceivingEndpoint");

        // Xml
        endpointConfiguration.UseSerialization<XmlSerializer>();

        // External Newtonsoft Json
        var externalNewtonsoftJson = endpointConfiguration.AddDeserializer<NewtonsoftSerializer>();
        externalNewtonsoftJson.ContentTypeKey("NewtonsoftJson");

        // External Newtonsoft Bson
        var externalNewtonsoftBson = endpointConfiguration.AddDeserializer<NewtonsoftSerializer>();
        externalNewtonsoftBson.ReaderCreator(stream => new BsonDataReader(stream));
        externalNewtonsoftBson.WriterCreator(stream => new BsonDataWriter(stream));
        externalNewtonsoftBson.ContentTypeKey("NewtonsoftBson");

        // Jil
        var jil = endpointConfiguration.AddDeserializer<JilSerializer>();
        jil.ContentTypeKey("Jil");

        // Wire
        endpointConfiguration.AddDeserializer<WireSerializer>();

        // register the mutator so the the message on the wire is written
        endpointConfiguration.RegisterMessageMutator(new IncomingMessageBodyWriter());

        #endregion

        endpointConfiguration.UseTransport<LearningTransport>();

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}