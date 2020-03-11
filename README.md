# Webapi .Net Core 3.1 + Angular

This is a test project to demonstrate using Angular 8 + WebApi Core 3.1.

## Get started

### Access the solution on production environment.

```shell
Access link: http://expenses2017.cfapps.io/
Inform username: adriano@gmail.com
Inform password: adriano
```

----------------------------------------------------------------------------

## Step by Step

### Clone the repo on Github

```shell
git clone https://github.com/adrianozovka/Expenses.git
cd Expenses
```

### Install npm packages ( Angular FrontEnd)

Install the `npm` packages described in the `package.json` and verify that it works:

```shell
npm install
```

To watches for changes to the source files, and runs localhost on port `4200`, execute ng serve.

```shell
ng serve
```

Case necessary, shut it down manually with `Ctrl-C`.

### Up WebAPI ( Backend )

change folder one level, build project and run project on localhost port 5000 via http protocol

```shell
cd..
dotnet build
dotnet run
```
Case necessary, shut it down manually with `Ctrl-C`.


## Access website local

With Backend and Frontend up, run browser on http://localhost:4200
Inform `Username`: adriano@gmail.com and `Password`: adriano

## Techniques

### WebAPI

In WebApi Core 3.1, to access the webmethod, it's necessary to use Bearer Token to authenticate.

To do it, It was created tokenservice.cs and that was injected on startup app.

To create the documentation, swagger was used that configured on startup app as well.

To access Rest API from Prefeitura do Recife, it was added RestClient file on Util folder.

To access the documentation by swagger, check in this link: https://webapiexpenses.cfapps.io/swagger/index.html

### Angular

In this solution, it was included somes components avaliable on site https://primefaces.org/primeng/showcase/#/

BlockUI
Charts
DataTables

To documentation, the compodoc has resource to create a great view to show how the app is working.

The documentation is avaliable on link:
http://expenses2017.cfapps.io/documentation/



## Deploying

To deploy using the Pivotal platform, its necessary to install the  client CF on link below:

https://tanzu.vmware.com/tutorials/getting-started/install-the-cf-cli

Command `cf` will be used.

-------------------------------------------------------------------------------

### Angular

```shell
To access the project root folder
dotnet publish -c Release -o ./publish
cd publish
cf login -a https://api.run.pivotal.io
put email account and password
cf push WebAPiExpenses
```

after completed, the url mentioned in the routes is your app url.

In this case, url created was webapiexpenses.cfapps.io

----------------------------------------------------------------------------

### WebApi
To access the project root folder

```shell
ng build --prod
To access dist/Expenses
create empty file "Staticfile.txt"
create file "manifest.yml"
write:
---
applications:
- name: expenses2017
  memory: 512M
  disk_space: 1024
  instance: 1
  buildpack: staticfile_buildpack
  
  
cf push
```

after completed, the url mentioned in the routes is your app url.

In this case, url created was expenses2017.cfapps.io as mentioned in manifest.yml

