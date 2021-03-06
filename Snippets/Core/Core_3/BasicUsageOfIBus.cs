namespace Core3
{
    using NServiceBus;

    class BasicUsageOfIBus
    {
        void Send(Configure configure)
        {
            #region BasicSend

            var configUnicastBus = configure.UnicastBus();
            var bus = configUnicastBus.CreateBus().Start();

            var myMessage = new MyMessage();
            bus.Send(myMessage);

            #endregion
        }

        #region SendFromHandler

        public class MyMessageHandler :
            IHandleMessages<MyMessage>
        {
            IBus bus;

            public MyMessageHandler(IBus bus)
            {
                this.bus = bus;
            }

            public void Handle(MyMessage message)
            {
                var otherMessage = new OtherMessage();
                bus.Send(otherMessage);
            }
        }

        #endregion

        void SendInterface(IBus bus)
        {
            #region BasicSendInterface

            bus.Send<IMyMessage>(
                messageConstructor: message =>
                {
                    message.MyProperty = "Hello world";
                });

            #endregion
        }

        void SetDestination(IBus bus)
        {
            #region BasicSendSetDestination

            var destination = Address.Parse("MyDestination");
            bus.Send(destination, new MyMessage());

            #endregion
        }

        void ThisEndpoint(IBus bus)
        {
            #region BasicSendToAnyInstance

            var myMessage = new MyMessage();
            bus.SendLocal(myMessage);

            #endregion
        }

        public class MyMessage
        {
        }

        public class OtherMessage
        {
        }

        interface IMyMessage
        {
            string MyProperty { get; set; }
        }
    }
}