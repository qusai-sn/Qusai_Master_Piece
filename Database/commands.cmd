Open the Package Manager Console from Tools > NuGet Package Manager > Package Manager Console.

Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Design
Install-Package Microsoft.EntityFrameworkCore.Tools


then use scafolding command 

Scaffold-DbContext "Server=DESKTOP-GIEQN5M;Database=MasterPiece;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context MasterPieceContext -force

PC Server : "DESKTOP-GIEQN5M"
Database : "MasterPiece"
Labtop Server : "DESKTOP-AD5CUR3"

add this to program.cs 
// Add services to the container.
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MasterPiece_Connection_string")));


add this to appsetting 
"ConnectionStrings": {
  "MasterPiece_Connection_string": "Server=DESKTOP-GIEQN5M;Database=MasterPiece;Trusted_Connection=True;TrustServerCertificate=True;"
}

