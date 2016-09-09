Import-Module SPE

Write-Output "Logging into Sitecore"
$session = New-ScriptSession -Username "admin"  -Password "b" -ConnectionUri #{Url}
Write-Output "Starting Publish"
Invoke-RemoteScript -Session $session -ScriptBlock { 
    $publisher = New-Object -TypeName Sitecore.Tasks.PublishAgent -ArgumentList "master","web","incremental","en"
    $publisher.Run()
}
Write-Output "Publish Complete" 
Stop-ScriptSession  -Session $session 
