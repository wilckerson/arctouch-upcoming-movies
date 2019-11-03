# ArcTouch Upcoming Movies Web App

https://arctouch-upcoming-movies.azurewebsites.net/

## Archtecture Description

This application uses ASP.NET Core 2.2 on backend and VueJS on frontend.

As this is a simple application, the backend was organized using a Business Logic Layer (BLL) that are invoked by a request controller API.
This BLL layer calls the services abstractions that can be injected with the correspondent implementation, in this case, the service of getting data from TheMovieDb. 
To avoid too much calls on TheMovieDb, the BLL are using a MemoryCache to provide results that have been stored by a period of time before making another external request. 

The frontend archtecture was kept simple as a default VueCLI project, withou using routes, vuex or any initial layout template. 
The search and pagination data is handled by the backend, the frontend just orchestrate it.

## Running the application

1) Install all dependencies and build the frontend project. This will create the bundle files inside "wwwroot" folder on backend project.
```
cd ./frontend
npm install
npm run build
```
2) Run the backend application
```
cd ./backend
dotnet run
```

## List of third-party libraries

- Bootstrap 4 CSS: to provide an easy way to build the interface experience.
- MomentJS: to provide an easy way to deal with dates.
- Axios: to provide an easy way to make web requests.
- SweetAlert: to provide an easy way to display message alerts.
- vuejs-paginate: to provide an easy way to deal with pagination.
