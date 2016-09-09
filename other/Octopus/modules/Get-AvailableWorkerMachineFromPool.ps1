################################################################
# This module identifies a single machine using Round-robin 
# from a pool of machines based on provide Machine Role name. 
#
################################################################

function Get-AvailableWorkerMachineFromPool($machineRole)
{
    
    Write-Host ("Machine role provided: $machineRole")

    #Extract each one and pull machines assigned to each role
    $machinesInRole = $OctopusParameters["Octopus.Environment.MachinesInRole[$machineRole]"]
  
    $servers = $machinesInRole -Split ","
    if(!$servers)
    {
        throw ("Unable to find any machines in provided machine role: $machineRole or machine role is not provided.")
    }
    
    $releaseNumber = Get-NumericReleaseNumber

    Write-Host "Identifying a server for running this process..."
    $currentServer = $OctopusParameters["Octopus.Machine.Id"] 
    
    Write-Host ("Current server: $env:COMPUTERNAME, machine: $currentServer")
    $calculatedServerIndex = ($releaseNumber % $servers.Length)
    $executingServerName = $servers[$calculatedServerIndex]
    Write-Host ("Executing machine should be: $executingServerName")
    if(!$currentServer.StartsWith($executingServerName)){
        Write-Host ("$currentServer is not a matching machine, exiting...")
        return $null
    }
    else{
        Write-Host ("Process will be executed on $executingServerName machine.")
        return $executingServerName
    }
}

function Get-NumericReleaseNumber(){
    $releaseNumber = $OctopusParameters["Octopus.Release.Number"] -replace '[.]'
    $releaseNumber = $releaseNumber -replace "[a-zA-Z_]*"
    $releaseNumber = ($releaseNumber | select-string "\d+" -allmatches).matches
    if(!$releaseNumber) {
        return 1
    }
    return $releaseNumber -replace "\s"
}
