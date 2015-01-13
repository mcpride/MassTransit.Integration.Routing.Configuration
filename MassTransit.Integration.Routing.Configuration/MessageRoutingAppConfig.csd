<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="210072ff-9542-452a-9483-5436a2a4b997" namespace="MassTransit.Integration.Routing.Configuration" xmlSchemaNamespace="urn:MassTransit.Integration.Routing.Configuration" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
  </typeDefinitions>
  <configurationElements>
    <configurationSectionGroup name="masstransit.routing">
      <configurationSectionProperties>
        <configurationSectionProperty>
          <containedConfigurationSection>
            <configurationSectionMoniker name="/210072ff-9542-452a-9483-5436a2a4b997/MessageRoutingSettings" />
          </containedConfigurationSection>
        </configurationSectionProperty>
      </configurationSectionProperties>
    </configurationSectionGroup>
    <configurationSection name="MessageRoutingSettings" codeGenOptions="XmlnsProperty" xmlSectionName="messageRoutings">
      <elementProperties>
        <elementProperty name="Routes" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="routes" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/210072ff-9542-452a-9483-5436a2a4b997/RouteToCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="RouteToCollection" xmlItemName="routeTo" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementCollectionMoniker name="/210072ff-9542-452a-9483-5436a2a4b997/MessageTypeCollection" />
      </itemType>
    </configurationElementCollection>
    <configurationElementCollection name="MessageTypeCollection" xmlItemName="messageType" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <attributeProperties>
        <attributeProperty name="Uri" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="uri" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/210072ff-9542-452a-9483-5436a2a4b997/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <itemType>
        <configurationElementMoniker name="/210072ff-9542-452a-9483-5436a2a4b997/MessageTypeElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="MessageTypeElement">
      <attributeProperties>
        <attributeProperty name="Type" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="type" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/210072ff-9542-452a-9483-5436a2a4b997/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>