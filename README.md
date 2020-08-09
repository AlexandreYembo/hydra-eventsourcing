# Hydra Eventsourcing
This project will be use to implement event store to an specific bounded context by using mediator.

### Package references:
```EventStore.Client```

### Project references:
```Hydra.Core```

### Connection string configuration:
Add the follow line to your appsettings.json
```json
{
    "ConnectionStrings":{
        "EventStoreConnection": "ConnectTo: tcp://admin:yourpassword@localhost:1113; HeartBeatTimeout=500"
    }
}
```


### Register Dependency Injection to your project
This configuration will follow the Eventstore documentation that suggest this instance as Singleton.

```c#
services.AddSingleton<IEventStoreService, EventStoreService>();
```
