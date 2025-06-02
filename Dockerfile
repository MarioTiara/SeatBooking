# Stage 1: Build React app
FROM node:20 AS client-build
WORKDIR /app
COPY ./src/SeatBooking.Web ./
RUN npm install
RUN npm run build

# Stage 2: Build .NET app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ./src/SeatBooking.Web/*.csproj ./SeatBooking.Web/
COPY ./src/SeatBooking.Domain/*.csproj ./SeatBooking.Domain/
COPY ./src/SeatBooking.Infrastructure/*.csproj ./SeatBooking.Infrastructure/
RUN dotnet restore ./SeatBooking.Web/SeatBooking.Web.csproj
COPY ./src .
WORKDIR /src/SeatBooking.Web
RUN dotnet publish -c Release -o /app/publish

# Stage 3: Final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=client-build /app/wwwroot ./wwwroot
COPY ./src/SeatBooking.Web ./
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "SeatBooking.Web.dll"]