# passwordManager

## To use
First clone the repository
### To setup the server and database
You need to run these commands in the terminal. 
Change the -p value to a password of your choice.
```
1.	dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\server.pfx -p 1234
2.	dotnet dev-certs https –trust
```
These will create a certificate for the server and trust it so that the server can be accessed through https.
You also need to create .env file in the server folder and add following enviroment variables. 
The values are only examples and should be changed for more secure values.
```
CONNECTION_STRING=host=serverDP;
ENV_POSTGRES_DB=mydatabase
ENV_POSTGRES_USER=postgres
ENV_POSTGRES_PASSWORD=postgres
CONNECTION_STRING=host=serverDP;Database=mydatabase;User Id=postgres;Password=postgres;port=5432;
CERT_PASSWORD=1234
```
After that you need to add an appsettings.json file in the server folder with the following content. 
Change the ConnectionString value to match your database. Also change the jwt key value to be more secure. 
```
{
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
      }
    },
    "AllowedHosts": "*",
    "ConnectionStrings": {
      "Default": "Host=Localhost;Database=mydatabase;Username=postgres;Password=postgres;Port=5432"
    },
    "Jwt": {
      "Audience": "https://myapp.example.com",
      "Issuer": "https://myapp.example.com",
      "Key": "thisisaverylongkeythatshouldbeusedforjwt"
    }
  }
```
Finally run the following commands in the terminal to start the server. 
```
docker-compose up
```
Make sure that you are on the same level as the docker-compose.yml file.

### To setup the client
Run this command to install the required packages
```
npm install
```
After that you can run the following command to start the client
```
npm start
```
The client should now be running on localhost:3000.