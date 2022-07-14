﻿using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NServiceBus;

class Usage
{
    Usage(BusConfiguration busConfiguration)
    {
        #region CastleWindsor

        busConfiguration.UseContainer<WindsorBuilder>();

        #endregion
    }

    void Existing(BusConfiguration busConfiguration)
    {
        #region CastleWindsor_Existing

        var container = new WindsorContainer();
        var registration = Component.For<MyService>()
            .Instance(new MyService());
        container.Register(registration);
        busConfiguration.UseContainer<WindsorBuilder>(
            customizations: customizations =>
            {
                customizations.ExistingContainer(container);
            });

        #endregion
    }

    class MyService
    {
    }
}