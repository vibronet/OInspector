# OInspector:  OpenID Connect/OAuth2 Inspector for Fiddler4

OInspector is a simple Fiddler4 inspector extension meant to facilitate analysis and troubleshooting of Fiddler traces representing OpenId Connect and/or OAuth2 network exchanges.
OInspector highlights request parameters and reveal claim values from JWT tokens, making it easier to visually inspect messages and spot anomalies.

OInspector comes "as is", with no guarantees. It has been designed to help troubleshooting Azure AD solutions and will likely not work as expected against other providers at this time. 

## System Requirements

OInspector requires [Fiddler](http://www.telerik.com/download/fiddler) for .NET4 (Fiddler4.5.0.0+). It will NOT work on Fiddler2. 
You can verify what version you have by clicking on Help->About Fiddler.
 

## Setup

- Download and expand [the ZIP](https://github.com/vibronet/OInspector/archive/master.zip) or [clone the repo](github-windows://openRepo/https://github.com/vibronet/OInspector). 
- Open a command prompt and navigate to the local root
- Launch `envy.cmd` to set up the build environment
- Run `build`
- Run `deploy`

aaand you are done.

## Getting Started

OInspector works on simple premises. Once you install it, following the setup instructions above, it adds to Fiddler's inspector tab a new inspector - named OIDC. That is not entirely correct - the inspector works with both OpenId Connect and OAuth2 - but it is short and recognizable. If you have a better idea, contribute! :)

The inspector in the OIDC tab verifies whether the request and/or response in the selected frame is an OpenId Connect message. If it is, it displays its salient parameters in a format that is easier on the eye than a querystring or encoded form. Furthermore: if the message contains an id_token or an access_token, the inspector assumes it is a JWT and it expands its claims.

Here there's an example of an authorization code grant request:

![](http://www.cloudidentity.com/blog/wp-content/uploads/2015/04/0request.png)

Below you can see the response. Note the code and the expanded id_token claim content.

![](http://www.cloudidentity.com/blog/wp-content/uploads/2015/04/0response.png)

## Credits & Next Steps

OInspector's development is 100% the work of Pavel Turbeleu. Vittorio contributed some ideas, but did exactly 0% of the development :-) 

At this point in time, OInspector is entirely unofficial and maintained as a 'best effort' project. Feel free to file issues and make feature request, but please be aware of the fact that there is no guarantee we will act upon them in timely fashion (or at all). That said: we hope OInspector will help you to navigate more easily through your traces!