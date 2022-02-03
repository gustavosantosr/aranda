# prueba de selección aranda

para conexion con una nueva instancia de base de datos cambiar el connectionstring en el archivo appsettings.Development.json

```json
 "ConnectionStrings": {
    "defaultConnection": "Data Source=pruebasaranda.database.windows.net,1433;Initial Catalog=productos;Persist Security Info=False;User ID=AdminAranda;Password=Admin360!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
```

de igual forma se puede restaurar por medio del comando de Dotnet EntityFramework
```shell
s$ dotnet ef database update
```

para acceder al swagger de la aplicación dirigirse a /swagger/index.html
