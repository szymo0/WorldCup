dotnet restore
dotnet build
$plugins = ".\Plugin1", ".\Plugin2"
$netcorePathForBin = "\bin\Debug\netcoreapp2.1"
$copyToPath =".\PluginTester"+$netcorePathForBin+"\Plugins"



ForEach($item in $plugins){
    Write-Output $item
    $folderName= Split-Path $item  -Leaf
    Write-Output $folderName    
    $source = $item + $netcorePathForBin+"\*"
    $destination =$copyToPath+"\"+$folderName+"\"
    if(!(Test-Path -Path $destination -PathType Container)){
        New-Item -ItemType Directory -Force -Path $destination
    }

    
    Write-Output $source
    Write-Output $destination
    Copy-Item -Path $source -Destination $destination  -Recurse -Force -Verbose
    
}