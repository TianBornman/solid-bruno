# Bruno
## Backend
### Setup
#### EF Migrations
Instructions to create/run migrations:

1. Scope to the correct directory:

    `cd {repo-path}\solid-bruno\backend`

2. Create Migration
	
    `dotnet ef migrations add InitialCreate --project Bruno.API`

3. Run Update

	`dotnet ef database update --project Bruno.API`

### Architecture
#### Unit Of Work & Repositories
This project uses the repository patters which enforces that there is a repository per entity that handles any database commands & queries for that entity.

The project also uses a `IUnitOfWork` implimentation that holds all the repositories and instantiated repositories with a single database instance.