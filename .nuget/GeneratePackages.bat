if not exist "..\build-packages" mkdir ..\build-packages
nuget pack ..\Elmah.EF\Elmah.EF.csproj -IncludeReferencedProjects -Build -OutputDirectory ..\build-packages
nuget pack ..\Elmah.EF6\Elmah.EF6.csproj -IncludeReferencedProjects -Build -OutputDirectory ..\build-packages
nuget pack ..\Elmah.EF.AspNet.Mvc\Elmah.EF.AspNet.Mvc.csproj -IncludeReferencedProjects -Build -OutputDirectory ..\build-packages
nuget pack ..\Elmah.EF.AspNet.WebApi\Elmah.EF.AspNet.WebApi.csproj -IncludeReferencedProjects -Build -OutputDirectory ..\build-packages
