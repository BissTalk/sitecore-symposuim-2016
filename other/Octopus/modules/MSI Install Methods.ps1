function GetErrorMessage($exitCode) {
    $ErrorMessages = @{
                "0" = "Action completed successfully.";
                "13" = "The data is invalid.";
                "87" = "One of the parameters was invalid.";
                "1601" = "The Windows Installer service could not be accessed. Contact your support personnel to verify that the Windows Installer service is properly registered.";
                "1602" = "User cancel installation.";
                "1603" = "Fatal error during installation.";
                "1604" = "Installation suspended, incomplete.";
                "1605" = "This action is only valid for products that are currently installed.";
                "1606" = "Feature ID not registered.";
                "1607" = "Component ID not registered.";
                "1608" = "Unknown property.";
                "1609" = "Handle is in an invalid state.";
                "1610" = "The configuration data for this product is corrupt. Contact your support personnel.";
                "1611" = "Component qualifier not present.";
                "1612" = "The installation source for this product is not available. Verify that the source exists and that you can access it.";
                "1613" = "This installation package cannot be installed by the Windows Installer service. You must install a Windows service pack that contains a newer version of the Windows Installer service.";
                "1614" = "Product is uninstalled.";
                "1615" = "SQL query syntax invalid or unsupported.";
                "1616" = "Record field does not exist.";
                "1618" = "Another installation is already in progress. Complete that installation before proceeding with this install.";
                "1619" = "This installation package could not be opened. Verify that the package exists and that you can access it, or contact the application vendor to verify that this is a valid Windows Installer package.";
                "1620" = "This installation package could not be opened. Contact the application vendor to verify that this is a valid Windows Installer package.";
                "1621" = "There was an error starting the Windows Installer service user interface. Contact your support personnel.";
                "1622" = "Error opening installation log file. Verify that the specified log file location exists and is writable.";
                "1623" = "This language of this installation package is not supported by your system.";
                "1624" = "Error applying transforms. Verify that the specified transform paths are valid.";
                "1625" = "This installation is forbidden by system policy. Contact your system administrator.";
                "1626" = "Function could not be executed.";
                "1627" = "Function failed during execution.";
                "1628" = "Invalid or unknown table specified.";
                "1629" = "Data supplied is of wrong type.";
                "1630" = "Data of this type is not supported.";
                "1631" = "The Windows Installer service failed to start. Contact your support personnel.";
                "1632" = "The temp folder is either full or inaccessible. Verify that the temp folder exists and that you can write to it.";
                "1633" = "This installation package is not supported on this platform. Contact your application vendor.";
                "1634" = "Component not used on this machine.";
                "1635" = "This patch package could not be opened. Verify that the patch package exists and that you can access it, or contact the application vendor to verify that this is a valid Windows Installer patch package.";
                "1636" = "This patch package could not be opened. Contact the application vendor to verify that this is a valid Windows Installer patch package.";
                "1637" = "This patch package cannot be processed by the Windows Installer service. You must install a Windows service pack that contains a newer version of the Windows Installer service.";
                "1638" = "Another version of this product is already installed. Installation of this version cannot continue. To configure or remove the existing version of this product, use Add/Remove Programs on the Control Panel.";
                "1639" = "Invalid command line argument. Consult the Windows Installer SDK for detailed command line help.";
                "1640" = "Installation from a Terminal Server client session not permitted for current user.";
                "1641" = "The installer has started a reboot.";
                "1642" = "The installer cannot install the upgrade patch because the program being upgraded may be missing, or the upgrade patch updates a different version of the program. Verify that the program to be upgraded exists on your computer and that you have the correct upgrade patch.";
                "3010" = "A restart is required to complete the install. This does not include installs where the ForceReboot action is run. Note that this error will not be available until future version of the installer."
    };

       Write-Output $ErrorMessages[$exitCode.ToString()]
        return $ErrorMessages[$exitCode.ToString()]
}


function UninstallApplication($ApplicationName) { 
   
   $filter = "Name = "+"'"+$ApplicationName+"'"
   $Application = Get-WmiObject -Class Win32_Product -Filter "$filter"
   $outputInfo = ""
   if($Application -ne $null)
   {
        $Application
        $uninstallInfo = $null
      do{
		
		 $uninstallInfo = $Application.Uninstall() 
         #Out-File -FilePath $logfile -Append -InputObject $uninstallInfo
		 Write-Output  $uninstallInfo
		 if($uninstallInfo.ReturnValue -eq 0)
		 {  
          $outputInfo =  "application "+ $filter+ " uninstallation successfull"      
          #Out-File -FilePath $logfile -Append -InputObject $outputInfo
		  Write-Output $outputInfo
		  break;
		 }
         elseif($uninstallInfo.ReturnValue -ne 1618)
         {
            $outputInfo =  "uninstallation failed, application returnvalue: " + $uninstallInfo.ReturnValue 
            #Send-MailMessage -To $to -From $from -Subject ('FAILED - ' + $subject + ' - ' + $(Get-Date -format "MM/dd/yyyy-HH:mm:ss")) -Body ($outputInfo + ' - Package: ' + $package) -SmtpServer $mailServer
			Write-Output $outputInfo
			break;
         }
         $outputInfo =  "Another Windows installer is in progress, application returnValue: " + $uninstallInfo.ReturnValue + " sleeping for 30 seconds"    
         Write-Output $outputInfo
		 Start-Sleep -s 60
         #Out-File -FilePath $logfile -Append -InputObject $outputInfo
		}while($uninstallInfo.ReturnValue -eq 1618)

       if($uninstallInfo.ReturnValue -ne 0)
       {
         EXIT $uninstallInfo.ReturnValue
       }
    }
}

function InstallApplication($MSIArgs) { 
  $argsText = $MSIArgs -join " "
  $outputInfo =""
  $process= $null
  do{
        Write-Output "Execute msiexec.exe $argsText"
		$process = Start-Process -FilePath "msiexec.exe" -ArgumentList $MSIArgs -Wait -PassThru      
		if($process.ExitCode -eq 0)
        {
         $outputInfo =  "application "+ $package+ " installation successfull"  
         #Out-File -FilePath $logfile -Append -InputObject $process.ExitCode
         Write-Output $outputInfo
            break
        }
		#wait before retry		
       elseif($process.ExitCode -ne 1618)
       {
         $errorMessage = GetErrorMessage $process.ExitCode
         $outputInfo =  "installion unsucessful, application returnvalue: $errorMessage" 
         Write-Output  $outputInfo 
         break;
         #Send-MailMessage -To $to -From $from -Subject ('FAILED - ' + $subject + ' - ' + $(Get-Date -format "MM/dd/yyyy-HH:mm:ss")) -Body ($outputInfo + ' - Package: ' + $package) -SmtpServer $mailServer
       }
       $outputInfo =  "Another Windows installer is in progress, application returnValue " + $process.ExitCode + " sleeping for 30 seconds"    
       #Out-File -FilePath $logfile -Append -InputObject $outputInfo
       Write-Output  $outputInfo 
	    Start-Sleep -s 60
             
      } while ($process.ExitCode -eq 1618)

      if($process.ExitCode -ne 0)
      {
        $result = GetErrorMessage $process.ExitCode
        Write-Output $result
        EXIT $process.ExitCode
      }
}

function AppendParameters($parameters,$siteArguments) { 
    if($parameters -ne $null -and $parameters -ne '')
    {
        $argArray = $parameters -split [char][byte]10 | Where-Object { $_ -ne '' }
        if($argArray)
        {
            foreach($arg in $argArray)
            {                   
                $siteArguments+=$arg
            }
        } 
    }
    return $siteArguments
}

function ValidateCatalog($catalogPath,$destinationPath)
{

    if ([string]::IsNullOrEmpty($catalogPath)) {return $false}
    if (Test-Path $catalogPath) {
        
        Write-Output "Validating..... $catalogPath"

        if($ValidateOnlyCMSPatchBinaries -and $ValidateOnlyCMSPatchBinaries -eq $true)
        {
          Write-Output "Validating only binaries ... ValidateOnlyCMSPatchBinaries is enabled "
          $patchCatalog = Get-Content $catalogPath | ? { $_ -like "*.dll" }
        }
        else
        {    
            $patchCatalog = Get-Content $catalogPath
        }
        $conflicts = [System.Collections.ArrayList]@()
    
        foreach ($file in $patchCatalog)
        {
            $targetFile = [System.IO.Path]::Combine($destinationPath, $file)
        
            if (Test-Path $targetFile)
            {
                $conflicts.Add($file) | out-null
            }
        }
    
        if ($conflicts.Count -gt 0)
        {
            Write-Warning "Patch contains files which already exists at destination: $destinationPath"
            Write-Host $conflicts
            
            Write-Error "Conflicts are not allowed. Terminating."
        }
        return $true
    }
    else{
        return $false
    }
}


function AddNewInstance($instanceId) {
	if ([string]::IsNullOrEmpty($instanceId)) {
		return ''
	}
	$args+="MSINEWINSTANCE=`"1`""
	$args+="TRANSFORMS=`":$instanceId`""
	return $args
	
}
