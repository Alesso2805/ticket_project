FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/TicketSupport.API/TicketSupport.API.csproj", "src/TicketSupport.API/"]
COPY ["src/TicketSupport.Application/TicketSupport.Application.csproj", "src/TicketSupport.Application/"]
COPY ["src/TicketSupport.Domain/TicketSupport.Domain.csproj", "src/TicketSupport.Domain/"]
COPY ["src/TicketSupport.Infrastructure/TicketSupport.Infrastructure.csproj", "src/TicketSupport.Infrastructure/"]

RUN dotnet restore "src/TicketSupport.API/TicketSupport.API.csproj"

COPY . .
WORKDIR "/src/src/TicketSupport.API"
RUN dotnet build "TicketSupport.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicketSupport.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketSupport.API.dll"]
