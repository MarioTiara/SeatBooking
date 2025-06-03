# SeatBooking Project

This repository contains the SeatBooking application, which allows users to manage seat bookings for flights. The application is built using ASP.NET Core, React, and Docker.

---

## **Getting Started**

Follow the steps below to clone the repository, build the project, and run it using Docker or locally.

---

### **Prerequisites**

Ensure you have the following installed on your machine:
- **Git**: [Download Git](https://git-scm.com/downloads)
- **Docker**: [Download Docker](https://www.docker.com/products/docker-desktop)
- **Docker Compose**: Comes bundled with Docker Desktop.
- **Node.js**: [Download Node.js](https://nodejs.org/)
- **.NET SDK**: [Download .NET SDK](https://dotnet.microsoft.com/download)

---

### **Steps to Run the Project**

#### **Option 1: Run via Docker Compose**

1. **Clone the Repository**
   ```bash
   git clone git@github.com:MarioTiara/SeatBooking.git
   cd SeatBooking
   ```

2. **Run the Project via Docker Compose**
   Build and start the containers using Docker Compose:
   ```bash
   docker-compose up --build
   ```

3. **Access the Application**
   Once the containers are running, you can access the application at:
   - **Frontend**: `http://localhost:8080/Home`
   - **Swagger API Documentation**: `http://localhost:8080/swagger`

---

#### **Option 2: Run Locally**

1. **Clone the Repository**
   ```bash
   git clone git@github.com:MarioTiara/SeatBooking.git
   cd SeatBooking
   ```

2. **Update the Connection String**
   Open `src/SeatBooking.Web/appsettings.json` and update the `ConnectionStrings` section with your database connection string:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "YourDatabaseConnectionStringHere"
   }
   ```

3. **Apply Database Migrations**
   Navigate to the `src/SeatBooking.Web` directory and run:
   ```bash
   dotnet ef database update
   ```

4. **Install Frontend Dependencies**
   Navigate to the `src/SeatBooking.Web/ClientApp` directory and run:
   ```bash
   npm install
   ```

5. **Build Frontend Assets**
   After installing dependencies, run:
   ```bash
   npx webpack --mode development
   ```

6. **Run the Application**
   Navigate back to the `src/SeatBooking.Web` directory and run:
   ```bash
   dotnet run
   ```

7. **Access the Application**
   Once the application is running, you can access it at:
   - **Frontend**: `http://localhost:5251/Home`
   - **Swagger API Documentation**: `http://localhost:5251/swagger`

---

### **Quick Start**

1. **Access Swagger UI**  
   Open your browser and go to:  
   - For Docker: `http://localhost:8080/swagger`
   - For local: `http://localhost:5251/swagger`

2. **Upload Seat Map**  
   In the Swagger UI, use the `POST /api/Seatmap` endpoint to upload your `seatmap.json` file.

3. **Access the Application**  
   After uploading the seat map, open:  
   - For Docker: `http://localhost:8080/Home`
   - For local: `http://localhost:5251/Home`

---

### **Project Structure**

- **SeatBooking.Domain**: Contains domain models and business logic.
- **SeatBooking.Infrastructure**: Handles database access and persistence.
- **SeatBooking.Web**: Contains the web application (frontend and backend).

---

### **Troubleshooting**

If you encounter issues:
- Ensure Docker is running (for Docker Compose).
- Ensure your database connection string is correct (for local setup).
- Check for port conflicts (default port is `8080` for Docker and `5251` for local).
- Run `docker-compose down` to stop containers and try again (for Docker Compose).
- Run `dotnet clean` and `dotnet build` to rebuild the project (for local setup).

---

### **License**

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

### **Contributing**

Feel free to submit issues or pull requests to improve the project.