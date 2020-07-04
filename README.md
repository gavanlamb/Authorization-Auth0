# Authorization.Auth0  
[![Build Status](http://jenkins.cicd.haplo.com.au/buildStatus/icon?job=Haplo-Tech%2FHaplo.Authorization.Auth0%2Fmaster)](http://jenkins.cicd.haplo.com.au/job/Haplo-Tech/job/Haplo.Authorization.Auth0/job/master/)

## How to Use  
### Configuration  

| Property Name | Description                               |
|:--------------|:------------------------------------------|
| Authority     | Auth0 authority                           |
| Audience      | Auth0 audience                            |
| Permissions   | A list of permissions to add to the token |

Add Configuration

``` json
{
    "Haplo.Authorization.Auth" : {
        "Authority": "https://tenantname.au.auth0.com/",
        "Audience" : "https://servicename.domain.com/",
        "Permissions" : [
            "read:servicename",
            "delete:servicename",
            "create:servicename"
        ]
    }
}
```

Add Auth0
``` csharp
public void ConfigureServices(IServiceCollection services)
{
    ...
    var auth0Options = Configuration.GetAuth0Configuration()
    services.AddAuth0(auth0Options); 
    ...
}
```

Use Auth0
``` csharp
public void Configure(IServiceCollection services)
{
    ...
    app.UseAuth0() 
    ...
}
```

## Development
### Build, Package & Release
#### Locally
```
// Step 1: Authenticate
dotnet build --configuration release 

// Step 2: Pack
dotnet pack --configuration release 

// Step 3: Publish
dotnet nuget push "Haplo.Authorization.Auth0.nupkg" -Source "github"
```

#### Jenkins Blueocean Pipelines
Create GitHub organisation and scan all repos, the Jenkins file should be picked up automatically.


## Reading Material
[On The Nature of OAuth2’s Scopes](https://auth0.com/blog/on-the-nature-of-oauth2-scopes/)
