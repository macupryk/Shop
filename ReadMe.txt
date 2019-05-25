pplication Details:

1-it uses Identity Server for  authentication 
in this example the identity server use memory not db (in real applications it should use db with aspnet Identity for example )
 \ShopApi\ShopIdentityServer\Config.cs
http://localhost:5000/.well-known/openid-configuration

2- it uses entity framework to store items data  
 add-migration init -context ApplicationContext
 update-database  -context ApplicationContext

 3-it uses repository,unitofwork,automapper,depedency injection,base classses and filter to catch exceptions and validation  (CustomExceptionFilterAttribute,ValidateModelFilterAttribute)
 The application layers are in folders in shop api (in real applications it should be seperated in projects )


 4-Apis URL 
 (ShopApi) get:  http://localhost:5001/api/item
 (ShopApi) post: http://localhost:5001/api/item/buy/1 
 (ShopIdentityServer) identityserver : http://localhost:5000 


 5-Use mock test (ShopNUnitTestProject) for testing the service and use integration test to test apis  (ShopNUnitIntegrationTestProject)
 also adding conole app for integration test  (ShopClient)


 6-Use swagger to display and test apis

 Questions

 How do we know if a user is authenticated?
 we authenticate the api by adding [Authorize] tag on it then we get user id using
 if(User.Identity.IsAuthenticated)
 var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

- Is it always possible to buy an item?
No we have tho check that this item is exist on the stock first 

a. choice of data format. Include one example of a request and response.
json data 
http://localhost:5001/swagger/index.html

http://localhost:5001/api/item
{
  "data": [
    {
      "description": "Item1 Description",
      "price": 100,
      "stock": 1,
      "id": 1,
      "name": "Item1",
      "createdDate": "2019-05-24T23:34:24.5433852",
      "modifiedDate": "2019-05-24T23:34:24.5433852",
      "createdUserName": null,
      "modifiedUserName": null
    },
    {
      "description": "Item2 Description",
      "price": 200,
      "stock": 5,
      "id": 2,
      "name": "Item2",
      "createdDate": "2019-05-24T23:34:24.5433852",
      "modifiedDate": "2019-05-24T23:34:24.5433852",
      "createdUserName": null,
      "modifiedUserName": null
    },
    {
      "description": "Item3 Description",
      "price": 300,
      "stock": 50,
      "id": 3,
      "name": "Item3",
      "createdDate": "2019-05-24T23:34:24.5433852",
      "modifiedDate": "2019-05-24T23:34:24.5433852",
      "createdUserName": null,
      "modifiedUserName": null
    },
    {
      "description": "Item4 Description",
      "price": 250,
      "stock": 15,
      "id": 4,
      "name": "Item4",
      "createdDate": "2019-05-24T23:34:24.5433852",
      "modifiedDate": "2019-05-24T23:34:24.5433852",
      "createdUserName": null,
      "modifiedUserName": null
    },
    {
      "description": "Item5 Description",
      "price": 400,
      "stock": 105,
      "id": 5,
      "name": "Item5",
      "createdDate": "2019-05-24T23:34:24.5433852",
      "modifiedDate": "2019-05-24T23:34:24.5433852",
      "createdUserName": null,
      "modifiedUserName": null
    }
  ],
  "statusCode": 200,
  "message": null,
  "messageCode": null,
  "developerMessage": null,
  "validationErrors": null,
  "pageInfo": null
}
 

http://localhost:5001/api/Item/buy/1
 {
  "data": 200,
  "statusCode": 200,
  "message": null,
  "messageCode": "The order has been done",
  "developerMessage": null,
  "validationErrors": null,
  "pageInfo": null
}


b.what authentication mechanism was chosen, and why
Identity server to support single sign on 



