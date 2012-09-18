<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="CodemashCloud" generation="1" functional="0" release="0" Id="c6bbe072-b286-4b9b-9e4c-ab84a0c79703" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="CodemashCloudGroup" generation="1" functional="0" release="0">
      <settings>
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
      <maps>
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
          <role name="Codemash.Poller" generation="1" functional="0" release="0" software="C:\projects\Codemash\Codemash\CodemashCloud\csx\Debug\roles\Codemash.Poller" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;Codemash.Poller&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;Codemash.Poller&quot; /&gt;&lt;/m&gt;" />
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
        <sCSPolicyFaultDomain name="Codemash.PollerFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="Codemash.PollerInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
</serviceModel>