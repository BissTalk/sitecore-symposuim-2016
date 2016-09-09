#############################################################################################################################
# Methods to connect to URL provided, retries $NumberOfRetries and waits $secomdsToWaitBetweenRetries between retries#
#############################################################################################################################



function AttemptWebRequest($url, $numberOfRetries=5, $secondsToWaitBetweenRetries=10){
    $currentTry=[int]1;
    Do{
        Write-Output "Trying to connect to site $url. Attempt $currentTry of $numberOfRetries."
        $request = $null 
        $exception = $null
        $time = try 
        { 
            $result = Measure-Command { 
                $request = [system.Net.WebRequest]::Create($url)
                $response = $request.GetResponse()
            } 
            $result.TotalMilliseconds 
        } 
        catch 
        { 
            ## If the request generated an exception (i.e.: 500 server 
            ## error or 404 not found), we can pull the status code from the 
            ## Exception.Response property 
            $response = $_.Exception.Response 
            if($response -eq $null){
                $exception = $_.Exception
            }
            $time = -1 
        } 

         if($exception -ne $null){
            $responseDescripion = $exception
         }
         else{
            $responseDescripion = $response.StatusDescription
         }
         
         $result = [PSCustomObject] @{ 
            Time = Get-Date; 
            Uri = $url; 
            StatusCode = $response.StatusCode; 
            StatusDescription = $responseDescripion; 
            ResponseLength = $response.ContentLength; 
            TimeTaken = $time; 
        } 

        
        $currentTry = $currentTry + 1
        $keepRetrying = ($result.StatusCode -ne "OK") -and ($currentTry -le $numberOfRetries);
        if($keepRetrying){
            Start-Sleep $secondsToWaitBetweenRetries
        }
    }While($keepRetrying)

    Write-Output $result
    if($result.StatusCode -ne "OK"){
        Write-Output "Failed"
        Exit 1  
    }
    Exit 0
}
