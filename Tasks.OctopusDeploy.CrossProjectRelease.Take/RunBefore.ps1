# Set appSettings values

param (
    [string]$OctopusDeploy_Url,
    [string]$OctopusDeploy_ApiKey,
    [string]$FileName,
    [string]$Environment
 )

$config = "Tasks.OctopusDeploy.CrossProjectRelease.Take.exe.config"
$doc = New-Object System.Xml.XmlDocument
$doc.Load($config)

$doc.SelectSingleNode('configuration/appSettings/add[@key="OctopusDeploy.Url"]').Attributes['value'].Value = $OctopusDeploy_Url
$doc.SelectSingleNode('configuration/appSettings/add[@key="OctopusDeploy.ApiKey"]').Attributes['value'].Value = $OctopusDeploy_ApiKey
$doc.SelectSingleNode('configuration/appSettings/add[@key="Tasks.OctopusDeploy.CrossProjectRelease.FileName"]').Attributes['value'].Value = $FileName
$doc.SelectSingleNode('configuration/appSettings/add[@key="Tasks.OctopusDeploy.CrossProjectRelease.Take.Environment"]').Attributes['value'].Value = $Environment

$doc.Save($config)
