#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Beis.HelpToGrow.Console.Web/Beis.HelpToGrow.Console.Web.csproj", "Beis.HelpToGrow.Console.Web/"]
COPY ["Beis.HelpToGrow.Core/Beis.HelpToGrow.Core.csproj", "Beis.HelpToGrow.Core/"]
RUN dotnet restore "Beis.HelpToGrow.Console.Web/Beis.HelpToGrow.Console.Web.csproj"
COPY . .
WORKDIR "/src/Beis.HelpToGrow.Console.Web"
RUN dotnet build "Beis.HelpToGrow.Console.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Beis.HelpToGrow.Console.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Beis.HelpToGrow.Console.Web.dll"]