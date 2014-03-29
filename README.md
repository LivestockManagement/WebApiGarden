An example entity framework set out here...
http://www.asp.net/web-api/overview/creating-web-apis/using-web-api-with-entity-framework/using-web-api-with-entity-framework,-part-1

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

Friendly error handling.
- HttpResponseMessage wrapper.
- Leverate HttpStatusCodes in request handlers - when no data is found. HttpStatusCode.NotFound.
- Response Status. OK. Bad Request. Error. Created. ...

