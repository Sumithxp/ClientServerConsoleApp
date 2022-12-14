# Client-Server Console App

Client Server Console App using SignalR

SignalR Server creation in Web Application and Client and Server applications in console app. All the applications run in docker contianers.


 <img src="https://www.docker.com/wp-content/uploads/2022/03/vertical-logo-monochromatic.png" width=20% height=20%>
 <img src="https://miro.medium.com/max/1200/0*ILbItnzDfSZhZwSn.png" width=20% height=20%>


 

**Server Console App** : Accept the text typed by the user in the console (interactive mode).

**Client Console App** : printout text from the server to the console as it is being typed (soft real time).

## Reuirements ##
Docker container 

## Easy Steps ##

1. **Build SignalRService** -   Goto to the SignalRService folder and build docker image and run


```docker
 docker build . -t signalrserver
 
 docker run signalrserver -d -p 8080:80
 
```
SignalR Service will be exposed to <IP>:8080  
  
2. **Build Server Console App**-   
  * Goto the ServerApp and find appsetting.json file and replace SingnaRServiceUrl (SignalRService container IP)
  * build docker image and run
   
```docker
 docker build . -t serverapp
 
 docker run -it serverapp
 
```
  
 3. **Build Client Console App**-  
  * Goto the ClientApp and find appsetting.json file and replace SingnaRServiceUrl (SignalRService container IP)
  * build docker image and run

  
```docker
 docker build . -t clientapp
 
 docker run -it clientapp
 
```
  
Happy coding... :D  
