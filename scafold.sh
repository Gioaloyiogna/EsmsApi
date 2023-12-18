using Microsoft.EntityFrameworkCore;
using ServiceManagerApi.Data;
using ServiceManagerApi.Models;

Scaffold - DbContext - Connection 'Server=173.248.129.77,1439;Database=Tarkwa;User ID=sa;Password=Pa$$w0rd@21;' - Provider Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models - context ServiceManagerContext - force
Scaffold - DbContext - Connection 'Server=208.117.44.15;Database=EnPDB;User ID=sa;Password=Admin@EnP;MultipleActiveResultSets=true' - Provider Microsoft.EntityFrameworkCore.SqlServer - OutputDir Data - context EnpDBContext - force
Scaffold - DbContext - Connection 'Server=DESKTOP-ROVQU4A\SQLEXPRESS;Database=EnpDB;User ID=sa;Password=password01;' - Provider Microsoft.EntityFrameworkCore.SqlServer - OutputDir Data - context EnpDBContext - force
 dotnet ef dbcontext scaffold --project .\ServiceManagerApi.csproj  'Server=208.117.44.15;Database=EnPDB;User ID=sa;Password=Admin@EnP;MultipleActiveResultSets=true' Microsoft.EntityFrameworkCore.SqlServer -o Data -c EnpDBContext  -f --no-build
########Scaff with certicificate windows
dotnet ef dbcontext scaffold --project .\ServiceManagerApi.csproj  'Server=208.117.44.15;Database=EnPDB;User ID=sa;Password=Admin@EnP;MultipleActiveResultSets=true; TrustServerCertificate=true;' Microsoft.EntityFrameworkCore.SqlServer -o Data -c EnpDBContext  -f --no-build
###Linux
dotnet ef dbcontext scaffold --project ./ServiceManagerApi.csproj  'Server=208.117.44.15;Database=EnPDB;User ID=sa;Password=Admin@EnP;MultipleActiveResultSets=true; TrustServerCertificate=true;' Microsoft.EntityFrameworkCore.SqlServer -o Data -c EnpDBContext  -f --no-build


Ben
  6:05 PM
Scaffold-DbContext -Connection 'Server=173.248.129.77,1439;Database=Tarkwa;User ID=sa;Password=Pa$$w0rd@21;' -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -context ServiceManagerContext  -force
Scaffold-DbContext -Connection 'Server=208.117.44.15;Database=EnPDB;User ID=sa;Password=Admin@EnP;MultipleActiveResultSets=true' -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data -context EnpDBContext  -force
Scaffold-DbContext -Connection 'Server=DESKTOP-ROVQU4A\SQLEXPRESS;Database=EnpDB;User ID=sa;Password=password01;' -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data -context EnpDBContext  -force
 dotnet ef dbcontext scaffold --project .\ServiceManagerApi.csproj  'Server=208.117.44.15;Database=EnPDB;User ID=sa;Password=Admin@EnP;MultipleActiveResultSets=true' Microsoft.EntityFrameworkCore.SqlServer -o Data -c EnpDBContext  -f --no-build
########Scaff with certicificate windows
dotnet ef dbcontext scaffold --project .\ServiceManagerApi.csproj  'Server=208.117.44.15;Database=EnPDB;User ID=sa;Password=Admin@EnP;MultipleActiveResultSets=true; TrustServerCertificate=true;' Microsoft.EntityFrameworkCore.SqlServer -o Data -c EnpDBContext  -f --no-build
###Linux
dotnet ef dbcontext scaffold --project ./ServiceManagerApi.csproj  'Server=208.117.44.15;Database=EnPDB;User ID=sa;Password=Admin@EnP;MultipleActiveResultSets=true; TrustServerCertificate=true;' Microsoft.EntityFrameworkCore.SqlServer -o Data -c EnpDBContext  -f --no-build
Scaffold-DbContext -Connection 'Server=173.248.129.77,1439;Database=Tarkwa;User ID=sa;Password=Pa$$w0rd@21;' -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -context ServiceManagerContext  -force
Scaffold-DbContext -Connection 'Server=208.117.44.15;Database=EnPDB;User ID=sa;Password=Admin@EnP;MultipleActiveResultSets=true' -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data -context EnpDBContext  -force
Scaffold-DbContext -Connection 'Server=DESKTOP-ROVQU4A\SQLEXPRESS;Database=EnpDB;User ID=sa;Password=password01;' -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data -context EnpDBContext  -force
 dotnet ef dbcontext scaffold --project .\ServiceManagerApi.csproj  'Server=208.117.44.15;Database=EnPDB;User ID=sa;Password=Admin@EnP;MultipleActiveResultSets=true' Microsoft.EntityFrameworkCore.SqlServer -o Data -c EnpDBContext  -f --no-build
########Scaff with certicificate windows
dotnet ef dbcontext scaffold --project .\ServiceManagerApi.csproj  'Server=208.117.44.15;Database=EnPDB;User ID=sa;Password=Admin@EnP;MultipleActiveResultSets=true; TrustServerCertificate=true;' Microsoft.EntityFrameworkCore.SqlServer -o Data -c EnpDBContext  -f --no-build
###Linux
dotnet ef dbcontext scaffold --project ./ServiceManagerApi.csproj  'Server=208.117.44.15;Database=EnPDB;User ID=sa;Password=Admin@EnP;MultipleActiveResultSets=true; TrustServerCertificate=true;' Microsoft.EntityFrameworkCore.SqlServer -o Data -c EnpDBContext  -f --no-build