# OnionArchitecture

### Notes
I started this project a few years ago when trying to find a good .NET sample that made use of onion architecture. However due to several reasons, I was not able to lead this project to an acceptble state.

Having said that, I have used most of the ideas here in another work project with satisfactory result.

Below are some ideas that I want to improve this project further:

- Remove completely project OnionArchitecture.Bootstrapper. Instead, let each project be responsible for its own initialization (using my other little library: [Sharpenter.BootstrapperLoader](https://github.com/hpcsc/Sharpenter.BootstrapperLoader) )
- Introduce persistence model (this was the last thing I was trying to do before the project was abandoned): for simple projects, domain model and persistence model are almost exactly the same. However, the more complicated the project is, the further and further those 2 models grow apart. Ideally I want to have a separate persistence model (which mapped directly to database tables) and leave domain model truly persistence-ignorance. But it's not as easy as it sounds. I have attempted several approaches in another personal project but haven't found a good but simple solution.
- Convert to .NET Core
- Tests, tests and more tests

This project is no longer maintained, however I still keep it here since it's still useful as reference for me in future projects

### Build Status
[![Build Status](https://travis-ci.org/hpcsc/OnionArchitecture.png)](https://travis-ci.org/hpcsc/OnionArchitecture)
