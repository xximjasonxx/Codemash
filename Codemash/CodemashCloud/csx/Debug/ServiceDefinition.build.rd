<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="CodemashCloud" generation="1" functional="0" release="0" Id="eaf501ce-fb8e-4af1-881a-4bb56cbe621c" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="CodemashCloudGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="Codemash.DeltaApi:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/CodemashCloud/CodemashCloudGroup/LB:Codemash.DeltaApi:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="Codemash.DeltaApi:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/CodemashCloud/CodemashCloudGroup/MapCodemash.DeltaApi:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="Codemash.DeltaApiInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/CodemashCloud/CodemashCloudGroup/MapCodemash.DeltaApiInstances" />
          </maps>
        </aCS>
        <aCS name="Codemash.Poller:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/CodemashCloud/CodemashCloudGroup/MapCodemash.Poller:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="Codemash.PollerInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/CodemashCloud/CodemashCloudGroup/MapCodemash.PollerInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:Codemash.DeltaApi:Endpoint1">
          <toPorts>
            <inPortMoniker name="/CodemashCloud/CodemashCloudGroup/Codemash.DeltaApi/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapCodemash.DeltaApi:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/CodemashCloud/CodemashCloudGroup/Codemash.DeltaApi/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapCodemash.DeltaApiInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/CodemashCloud/CodemashCloudGroup/Codemash.DeltaApiInstances" />
          </setting>
        </map>
        <map name="MapCodemash.Poller:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/CodemashCloud/CodemashCloudGroup/Codemash.Poller/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapCodemash.PollerInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/CodemashCloud/CodemashCloudGroup/Codemash.PollerInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="Codemash.DeltaApi" generation="1" functional="0" release="0" software="C:\projects\Codemash\Codemash\CodemashCloud\csx\Debug\roles\Codemash.DeltaApi" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;Codemash.DeltaApi&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;Codemash.DeltaApi&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;Codemash.Poller&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/CodemashCloud/CodemashCloudGroup/Codemash.DeltaApiInstances" />
            <sCSPolicyFaultDomainMoniker name="/CodemashCloud/CodemashCloudGroup/Codemash.DeltaApiFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="Codemash.Poller" generation="1" functional="0" release="0" software="C:\projects\Codemash\Codemash\CodemashCloud\csx\Debug\roles\Codemash.Poller" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;Codemash.Poller&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;Codemash.DeltaApi&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;Codemash.Poller&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/CodemashCloud/CodemashCloudGroup/Codemash.PollerInstances" />
            <sCSPolicyFaultDomainMoniker name="/CodemashCloud/CodemashCloudGroup/Codemash.PollerFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyFaultDomain name="Codemash.DeltaApiFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="Codemash.PollerFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="Codemash.DeltaApiInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="Codemash.PollerInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="d71340ff-54a7-4a56-85c8-1d20155390ee" ref="Microsoft.RedDog.Contract\ServiceContract\CodemashCloudContract@ServiceDefinition.build">
      <interfacereferences>
        <interfaceReference Id="c6ebed8c-b246-4382-98d0-5ca86f07fb6c" ref="Microsoft.RedDog.Contract\Interface\Codemash.DeltaApi:Endpoint1@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/CodemashCloud/CodemashCloudGroup/Codemash.DeltaApi:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>