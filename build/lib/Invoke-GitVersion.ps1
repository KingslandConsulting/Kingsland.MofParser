function Invoke-GitVersion
{

    param
    (

        [Parameter(Mandatory=$true)]
        [string] $GitVersion

    )

    write-host "*****************";
    write-host "Invoke-GitVersion";
    write-host "*****************";
    write-host "gitversion = $GitVersion";

    # check gitversion.exe exists
    if( -not [System.IO.File]::Exists($GitVersion) )
    {
        throw new-object System.IO.FileNotFoundException("GitVersion.exe not found.");
    }

    $stdOut = & $GitVersion;

    $exitcode = $LASTEXITCODE;
    if( $exitcode -ne 0 )
    {
        write-host ($stdOut | fl * | out-string);
        throw new-object System.InvalidOperationException("GitVersion.exe failed with exit code $exitcode");
    }

    $versionText = [string]::Join("`r`n", $stdOut);
    $versionJson = ConvertFrom-Json $versionText;

    return $versionJson;

}