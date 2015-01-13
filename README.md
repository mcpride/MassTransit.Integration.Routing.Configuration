# Configuration based message routing support for MassTransit

See also MassTransit http://masstransit-project.com

## Example: App.config based usage

* Define in applications configuration file the needed configuration sections and some routing rules:

```xml
<?xml version="1.0"?>
<configuration>
	<configSections>
		<sectionGroup name="masstransit.routing" type="System.Configuration.ConfigurationSectionGroup, System.Configuration">
			<section name="myMessageRoutings" type="MassTransit.Integration.Routing.Configuration.MessageRoutingSettings, MassTransit.Integration.Routing.Configuration"/>
      <section name="otherMessageRoutings" type="MassTransit.Integration.Routing.Configuration.MessageRoutingSettings, MassTransit.Integration.Routing.Configuration"/>
    </sectionGroup>
	</configSections>
	<masstransit.routing xmlns="urn:MassTransit.Integration.Routing.Configuration">
    <myMessageRoutings>
      <routes>
        <routeTo uri="msmq://remote_server/my_queue">
          <messageType type="MyMessages.CustomerCreated, MyMessages"/>
          <messageType type="MyMessages.CustomerChanged, MyMessages"/>
          <messageType type="MyMessages.CustomerDeleted, MyMessages"/>
        </routeTo>
        <routeTo uri="msmq://remote_server/my_second_queue">
          <messageType type="MyMessages.CustomerCreated, MyMessages"/>
          <messageType type="MyMessages.CustomerDeleted, MyMessages"/>
        </routeTo>
      </routes>
		</myMessageRoutings>
    <otherMessageRoutings>
      <routes>
        <routeTo uri="msmq://other_remote_server/other_queue">
          <messageType type="OtherMessages.MessageA, OtherMessages"/>
        </routeTo>
      </routes>
    </otherMessageRoutings>
  </masstransit.routing>
</configuration>
```

* Run the "RouteByConfiguration" extension method for a routing configurator:

```C#
var bus = ServiceBusFactory.New(sbc =>
{
	sbc.UseMsmq(mqc =>
	{
		mqc.UseMulticastSubscriptionClient();
		mqc.VerifyMsmqConfiguration();
	});
	sbc.ReceiveFrom("msmq://localhost/myReceiveQueue");
    sbc.ConfigureService<RoutingConfigurator>(BusServiceLayer.Session, routingConfigurator =>
            {
                routingConfigurator.RouteByConfiguration("myMessageRoutings");
            });
    // ...
});
```