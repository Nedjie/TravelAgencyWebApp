Travel Agency System for SoftUni-WebProject
Welcome to the Travel Agency System – a dynamic web application developed as part of the ASP.NET Advanced course at SoftUni.

Overview
The Travel Agency System is a comprehensive and interactive web application designed to showcase the principles and capabilities of ASP.NET. This project focuses on providing a user-friendly platform for customers looking to book travel packages, demonstrating modern web development technologies and best practices in the process.

Key Features
User-Friendly Interface: A simple and intuitive design that allows users to easily browse, search, and book travel packages with minimal effort.

Travel Offers: Explore a diverse catalog of exciting travel packages, complete with detailed specifications and pricing information.

Booking and Reservations: A seamless process for booking travel packages for specific dates. Users can review their bookings and manage reservations efficiently.

Admin Dashboard: A dedicated dashboard for administrators to manage travel packages, user accounts, reservations, and overall system settings.

Responsive Design: The application is optimized for a variety of devices, ensuring an excellent user experience whether on desktops, tablets, or smartphones.

Purpose
The primary aim of this project is to serve as a practical learning resource for individuals interested in ASP.NET web development. By examining the code, exploring the project structure, and understanding the implemented features, developers can gain valuable insights into creating dynamic web applications.

How to Start the Project
To get started with the Travel Agency System, please follow these steps:

Clone the Repository: Begin by cloning this repository to your local machine.

Set Startup Project: Open the solution in Visual Studio and set the startup project to TravelAgencySystem.Web located in the Web folder.

Update Databases:

For the Data Database: Open the Package Manager Console, set the default project to Data\TravelAgencySystem.Data, and execute the command:
text
Copy
update-database -context TravelAgencyDbContext
For the Service Database: Set the default project to Web\TravelAgencySystem.Web and run:
text
Copy
update-database -context ServiceDbContext
Start the Project: After the databases are updated, you can run the project.

Login Credentials
Admin Profile: 

Email: admin@denitravel.com
Password: admin123

Common User Profile:

Email: user@gmail.com
Password: 123456

Usage
Feel free to explore the codebase by cloning or downloading this repository. This project provides an excellent hands-on learning experience for understanding ASP.NET concepts, database integration, user authentication, and building interactive web applications.

Contributing
We welcome contributions to this project. If you identify any bugs, security vulnerabilities, or have suggestions for enhancements, please feel free to open an issue or submit a pull request.

Thank you for exploring the Travel Agency System!
