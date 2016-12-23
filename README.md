# OnionArchitecture

### Build Status
[![Build Status](https://travis-ci.org/hpcsc/OnionArchitecture.png)](https://travis-ci.org/hpcsc/OnionArchitecture)

### Notes
I started this project a few years ago when trying to find a good .NET sample that made use of onion architecture. However due to several reasons, I was not able to lead this project to an acceptble state.

Having said that, I have used most of the ideas here in another work project with satisfactory result.

Below are some ideas that I want to improve this project further:

- Introduce persistence model (this was the last thing I was trying to do before the project was abandoned): for simple projects, domain model and persistence model are almost exactly the same. However, the more complicated the project is, the further and further those 2 models grow apart. Ideally I want to have a separate persistence model (which mapped directly to database tables) and leave domain model truly persistence-ignorance. But it's not as easy as it sounds. I have attempted several approaches in another personal project but haven't found a good but simple solution.
- Convert to .NET Core
- Tests, tests and more tests

This project is no longer maintained, however I still keep it here since it's still useful as reference for me in future projects

### Architecture

[![Architecture](https://dl.dropboxusercontent.com/u/55034418/OnionArchitectureDiagram.png)](https://dl.dropboxusercontent.com/u/55034418/OnionArchitectureDiagram.png)

#### Solution Structure

- `OnionArchitecture.Core`: contains domain models, interfaces for repositories or infrastructure services used by upper layers
- `OnionArchitecture.Services.Interface`: contains application service interfaces, DTO (request and response models), to be used by controllers
- `OnionArchitecture.Services`: contains implementation of application services
- `OnionArchitecture.UI.Web`: main UI application
- `OnionArchitecture.Infrastructure`: contains implementation of infrastructure services
- `OnionArchitecture.Repository.EntityFramework`: contains repositories implementation, using EF code first

#### Bootstrapping Process

The project uses [Sharpenter.BootstrapperLoader](https://github.com/hpcsc/Sharpenter.BootstrapperLoader) to perform bootstrapping of the application (configuration of IoC container, AutoMapper, etc)

One advantage (or downside, depends on your view) of this approach is that the UI project no longer has reference to lower layers that contains concrete implementation (like `OnionArchitecture.Services`, `OnionArchitecture.Infrastructure`). Those concrete implementation will be registered to IoC container and injected into controllers through their interfaces if needed. 

However no reference also means during building of the solution, Visual Studio will no longer copy output dlls from those concrete implementation projects to `bin` directory of `UI.Web` project. I work around this limitation by defining post-build task for `UI.Web` to copy those dlls manually:
```
<Target Name="AfterBuild">
    <ItemGroup>
        <SourceFiles Include="$(SolutionDir)OnionArchitecture.Services\bin\$(Configuration)\**\*.*" />
        <SourceFiles Include="$(SolutionDir)OnionArchitecture.Repository.EntityFramework\bin\$(Configuration)\**\*.*" />
        <SourceFiles Include="$(SolutionDir)OnionArchitecture.Infrastructure\bin\$(Configuration)\**\*.*" />
    </ItemGroup>
    <Copy SourceFiles="@(SourceFiles)" DestinationFolder="$(OutputPath)\%(RecursiveDir)" ContinueOnError="true" SkipUnchangedFiles="true" />
</Target>
```
