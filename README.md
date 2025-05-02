# myWebAPI

- created using `dotnet new webapi --use-controllers --output Visual.myWebAPI`
- run with `dotnet run --launch-profile https`

Packages added:
> `dotnet add package Microsoft.EntityFrameworkCore.InMemory`
> `code -r ../Visual.myWebAPI`
> `dotnet add package NSwag.AspNetCore`
> `dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design`
> `dotnet add package Microsoft.EntityFrameworkCore.Design`
> `dotnet add package Microsoft.EntityFrameworkCore.SqlServer`
> `dotnet add package Microsoft.EntityFrameworkCore.Tools`
> `dotnet tool uninstall -g dotnet-aspnet-codegenerator`
> `dotnet tool install -g dotnet-aspnet-codegenerator`
> `dotnet tool update -g dotnet-aspnet-codegenerator`

HTTPS certification:
> `dotnet dev-certs https --trust`

Controller generation:
- `dotnet aspnet-codegenerator controller -name *ThisController* -async -api -m *ThisItem* -dc *ThisContext* -outDir Controllers`

~~created using "dotnet new webapp --output Visual.myProject --no-https"
run by verifying terminal directory & entering "dotnet run"

database installed using "npm install -g xmysql"
REST APIs generated using "xmysql -h localhost -u root -p password -d mydb"

git stuff: git init | git config --global user.email "email@website.com" | git branch -M main | git remote add origin URL
git add . | git commit -m "commit details" | git push -u origin main
git fetch origin | git pull

to delete all: git rm -r .
to reverse: git reset --hard HEAD~~
