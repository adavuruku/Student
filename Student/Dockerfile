FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Student/Student.csproj", "Student/"]
RUN dotnet restore "Student/Student.csproj"
COPY . .
WORKDIR "/src/Student"
RUN dotnet build "Student.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Student.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Student.dll"]
