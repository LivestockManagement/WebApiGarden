An example entity framework set out here...
http://www.asp.net/web-api/overview/creating-web-apis/using-web-api-with-entity-framework/using-web-api-with-entity-framework,-part-1

WebApi Notes and Design.

Dependency Injection
- Unity WebApi

Models & Entities.
- Model
- Model Factory rather than auto mapper, more flexible.
- Entities / Business Objects

Routes.
- route paths.
- constraints.
- parameters.

Identifiers
- Associations / Actions (e.g. Controller.Get(int id), Controller.Get(int id, int secondid - optional...))
- Identifiers. Individual object identifier rather than using parameters...?! useful when versioning. (model urls)

Filters
- Filter dependency injection.
- Unity WebApi Bootstrapper.

Friendly error handling.
- HttpResponseMessage wrapper.
- Leverate HttpStatusCodes in request handlers - when no data is found. HttpStatusCode.NotFound.
- Response Status. OK. Bad Request. Error. Created. ...

Security.
- SSL
- WebAPI SSL validation. Using Filters. RequireHttpsAttribute. Flexible so easy to run on local / dev machines WITHOUT SSL.
- Cross Origin Security. We gotta allow calls from other domains? Other websites. By default, with JavaScript - other websites won't be able to access our WebApi.
  * JSONP. Simpler. Downloadable executable JS file. Callback provided.
  * Enable CORS. Cross Origin Resource Sharing.
    * Per method?
- Authentication.
	* App Authentication. a mobile app used by many people. App key / Secret pair.
	* User Authentication. granting individual users access to certain features of a WebApi. Roles. User specific data. Using Basic Authentication, OAuth and or Integrated Auth (web forms).
	* Typically Basic authentication is used to track the user, Token auth is used to track the 3rd party application.
	* Implementations.
		> ASP.NET Authentication. Good enough for a base website. [Authorize]. Simply implemented with webforms and sessions. (most likely not useful). Difficult to use with .Net clients.
		> Basic Authentication. Allows authentication without webforms or online interface. Useful for a 3rd party app. Credentials. Headers.
			- Filter. AuthoriseFilterAttribute
			- No encryption. Straight username and password. Requires SSL.
			- built in? WebMatrix.WebData.WebSecurity.Login()... or custom. Relies on standard asp.net roles and membership database.
			- minimal effort when authenticating JS client.
			- Base64 encoded username / password. 
			- Example header - [ Authorization: Basic SmFzb25BY2Nlc3M6UGFzc3dvcmQwMQ== ]
		> Token Authentication.
			- Usage sequence.
				1. [Client] Request Key. (once off request, 3rd party app developer request on registration).
				2. [API] Supply Key and Secret.
				3. [Client] Request Token. Regular daily process.
				4. [API] Review Token request and return Token.
				5. [Client] Use Token with each request. Tokens eventually expire. 2 hrs. 1 day.
			- Each client, on registration is given an ApiKey and a Signature which they can use to retrieve a token when needed.
			- Example Token Request. { "apikey" : "TXlGaXJzdEFwcElk", "signature" : "TXlGaXJzdFNlY3JldA==" }




Authentication vs Authorisation
The difference between these two when it comes to granting access to a WebApi is important.

Authentication revolves around identifying someone. Trying to figure out how someone is based on a provided set of details. Credentials. A client certificate. A token. A username / password. Who are you?

Authorisation takes an identity and determines your rights. Takes a request that has been authenticated and then decides what the request is allowed and not allowed to do.