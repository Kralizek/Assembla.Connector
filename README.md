This repository contains a series of tools to easily integrate the REST API offered by [Assembla](https://www.assembla.com/).
The first tool being worked on is a REST API wrapper that will act as foundation for future tools.

## Kralizek.Assembla.Connector
This library contains the REST API wrapper. The idea behind this package is to closely map the available methods.
In addition, this library won't validate the correctness of your requests. So, be sure you have the [API reference](http://api-docs.assembla.cc/content/api_reference.html) at hand.

### Authentication
As stated in the [documentation](http://api-docs.assembla.cc/content/authentication.html), Assembla API supports four type of authentication of the requests.

* Key/Secret credentials: suitable for development environment, small one-user applications.
* Client credentials suitable for applications that rely on public data and do not require user authentication.
* 3-step authentication flow: suitable for browser based applications.
* Pin Code suitable for desktop/mobile applications. 

This library aims at supporting the first two scenarios out-of-the-box.
The remaining scenarios will be supported by extensions packages.

### Functionalities
Here is a list of sections of the exposed functionalities.

#### Implemented
- [x] Users
- [x] Spaces
- [x] Tools
- [x] Tickets
- [x] Tags
- [x] Milestones
- [x] Documents

#### Next in line
- [ ] Stream
- [ ] Mentions
- [ ] User roles
- [ ] Wiki
- [ ] SSH keys

#### Later
- [ ] Merge requests
- [ ] StandUp reports
- [ ] Webhooks
- [ ] Tasks
- [ ] Portfolio

## Versioning
This repository uses the [Semantic Versioning 2.0.0](http://semver.org/spec/v2.0.0.html).
The packages whose major version is 0 are to be considered in their initial development phase, therefore their public programming interface cannot be considered stable.

## Future development
When the wrapper library is complete, additional tools can be built upon it.
Ideas I have but are not limited to are
* An ASP.NET Core drop-in middleware to handle authentication
* A PowerShell module
* A high-level library with a more cohesive and semantic richer programming interface.

## Support
Since I am not affiliated with Assembla in any way, this package might stop working at any moment due to unannounced API changes or similar causes. For these reasons, you use this package at your own risk.

Anyway, if anything breaks, feel free to send a PR with the solution ;)

## Disclaimer
All product and company names are trademarks™ or registered® trademarks of their respective holders. Use of them does not imply any affiliation with or endorsement by them. 
