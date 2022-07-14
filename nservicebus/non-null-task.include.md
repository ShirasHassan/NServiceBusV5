NOTE: In NServiceBus Versions 6 and above, and all integrations that target those versions, all extension points that return [`Task`](https://msdn.microsoft.com/en-AU/library/system.threading.tasks.task.aspx) cannot return a null Task. These APIs must return an instance of a Task, i.e. a pending Task or a [`CompletedTask`](https://msdn.microsoft.com/en-au/library/system.threading.tasks.task.completedtask.aspx), or be marked [`async`](https://msdn.microsoft.com/en-us/library/hh191443.aspx). For extension points that return a [`Task<T>`](https://msdn.microsoft.com/en-us/library/dd321424.aspx), return the value directly (for async methods) or wrap the value in a [`Task.FromResult(value)`](https://msdn.microsoft.com/en-us/library/hh194922.aspx).