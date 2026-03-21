# Bruno
## Setup
Instructions to create/run migrations:

1. Scope to the correct directory:

    `cd {repo-path}\solid-bruno\backend`

2. Create Migration
	
    `dotnet ef migrations add InitialCreate --project Bruno.API`

3. Run Update

	`dotnet ef database update --project Bruno.API`