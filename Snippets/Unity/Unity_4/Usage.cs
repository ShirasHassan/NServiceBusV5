﻿using Microsoft.Practices.Unity;
using NServiceBus;

class Usage
{
    Usage(Configure configure)
    {
        #region Unity

        configure.UnityBuilder();

        #endregion
    }

    void Existing(Configure configure)
    {
        #region Unity_Existing

        var container = new UnityContainer();
        container.RegisterInstance(new MyService());
        configure.UnityBuilder(container);

        #endregion
    }
    class MyService
    {
    }
}