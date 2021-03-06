using System;
using System.Web.Mvc;
using NServiceBus;

public class SendAndBlockController :
    Controller
{
    IEndpointInstance endpoint;

    public SendAndBlockController(IEndpointInstance endpoint)
    {
        this.endpoint = endpoint;
    }

    [HttpGet]
    public ActionResult Index()
    {
        ViewBag.Title = "SendAndBlock";
        return View();
    }

    [HttpPost]
    public ActionResult Index(string textField)
    {
        ViewBag.Title = "SendAndBlock";

        if (!int.TryParse(textField, out var number))
        {
            return View();
        }

        #region SendAndBlockController

        var command = new Command
        {
            Id = number
        };


        var sendOptions = new SendOptions();
        sendOptions.SetDestination("Samples.Mvc.Server");
        var status = endpoint.Request<ErrorCodes>(command, sendOptions).GetAwaiter().GetResult();

        return IndexCompleted(Enum.GetName(typeof(ErrorCodes), status));

        #endregion
    }

    public ActionResult IndexCompleted(string errorCode)
    {
        ViewBag.Title = "SendAsync";
        ViewBag.ResponseText = errorCode;
        return View("Index");
    }
}
