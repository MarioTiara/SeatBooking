# SeatBooking Project

This repository contains the SeatBooking application, which allows users to manage seat bookings for flights. The application is built using ASP.NET Core, React, and Docker.

---

## **Getting Started**

Follow the steps below to clone the repository, build the project, and run it using Docker.

---

### **Prerequisites**

Ensure you have the following installed on your machine:
- **Git**: [Download Git](https://git-scm.com/downloads)
- **Docker**: [Download Docker](https://www.docker.com/products/docker-desktop)
- **Docker Compose**: Comes bundled with Docker Desktop.

---

### **Steps to Run the Project**

1. **Clone the Repository**
   ```bash
   git clone https://github.com/<your-repo-name>/SeatBooking.git
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

### **Project Structure**

- **SeatBooking.Domain**: Contains domain models and business logic.
- **SeatBooking.Infrastructure**: Handles database access and persistence.
- **SeatBooking.Web**: Contains the web application (frontend and backend).

---

### **Troubleshooting**

If you encounter issues:
- Ensure Docker is running.
- Check for port conflicts (default port is `8080`).
- Run `docker-compose down` to stop containers and try again.

---

### **License**

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

### **Contributing**

Feel free to submit issues or pull requests to improve the project.