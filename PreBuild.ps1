# Set appSettings values

param (
    [string]$OctopusServer_Url,
    [string]$OctopusServer_ApiKey,
    [string]$Snapshot_Environment,
    [string]$Snapshot_FileName
 )

$config = "Tasks.OctopusDeploy.CrossProjectRelease.Take\app.config"
$doc = New-Object System.Xml.XmlDocument
$doc.Load($config)

$doc.SelectSingleNode('configuration/appSettings/add[@key="UDocx365.OctopusServer.Url"]').Attributes['value'].Value = $OctopusServer_Url
$doc.SelectSingleNode('configuration/appSettings/add[@key="UDocx365.OctopusServer.ApiKey"]').Attributes['value'].Value = $OctopusServer_ApiKey
$doc.SelectSingleNode('configuration/appSettings/add[@key="UDocx365.Snapshot.Environment"]').Attributes['value'].Value = $Snapshot_Environment
$doc.SelectSingleNode('configuration/appSettings/add[@key="UDocx365.Snapshot.FileName"]').Attributes['value'].Value = $Snapshot_FileName

$doc.Save($config)
