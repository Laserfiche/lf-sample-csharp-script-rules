# Sample Laserfiche C# Rule script project

Sample C# script that can be invoked from Laserfiche workflow or business process.

Scripts are invoked by **Laserfiche Remote Agent** which is a service installed on a Windows PC for this purpose.

The script method invoked in this example is `StringJoiner.JoinTokensAsync()`

NOTE: .NET rule script must be Dlls targeting netstandard2.0 or net48

## Prerequisite

- [.Net Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)
- [Visual Studio Code](https://code.visualstudio.com/download) or other IDE
- [A Laserfiche Cloud Account](https://www.laserfiche.com/signon/)
- [Documentation](https://doc.laserfiche.com/laserfiche.documentation/en-us/Default.htm#../Subsystems/ProcessAutomation/Content/Resources/Rules/csharpscript.htm?TocPath=Process%2520Automation%257CRules%257CGetting%2520Started%2520With%2520Scripts%257C_____1)

## Build and Test script

Open a PS terminal window:

- To build project: `dotnet build`
- To run tests: `dotnet run --framework net48`

## Configure a remote agent

- [Documentation](https://doc.laserfiche.com/laserfiche.documentation/en-us/Default.htm#../Subsystems/ProcessAutomation/Content/Resources/Integrations/Remote-Agents/Remote-Agents.htm?TocPath=Process%2520Automation%257CIntegrations%257CRemote%2520Agents%257C_____0)

## Deploy script to your remote agent

- Copy the content of folder `bin\Debug\netstandard2.0` folder under one of the three LFPALocalAgent folder e.g. `C:\Program Files\Laserfiche\ProcessAutomationWorkerAgent\LFPALocalAgent\myscriptbin`

## Configure and test this script rule in your Laserfiche Cloud Account -> Process Automation -> Rules

- Create a new Script rule
  - Script location: `myscriptbin/lf-sample-csharp-script-rule.dll`
  - Class name: `StringJoiner`
  - Method name: `JoinTokensAsync`
