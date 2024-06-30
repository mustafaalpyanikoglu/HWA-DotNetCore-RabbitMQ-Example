# Ticketing Service Application

## Overview

This is a simple ticketing service application built with .NET Core and RabbitMQ. The application demonstrates how to integrate RabbitMQ for messaging between a .NET API and a console application.

## Components

- **Console Application**: Consumes messages from a RabbitMQ queue and processes them.
- **API Application**: Provides an endpoint to create bookings and sends them as messages to the RabbitMQ queue.

## Prerequisites

- .NET 6 SDK
- Docker

## Setup and Running the Applications

### 1. Set Up RabbitMQ Using Docker

Create a `docker-compose.yaml` file with the following content:

```yaml
version: '3.8'
services:
  rabbitmq:
    container_name: "rabbitmq"
    image: rabbitmq:3-management-alpine
    environment:
      - RABBITMQ_DEFAULT_USER=guest
      - RABBITMQ_DEFAULT_PASS=guest
    ports:
      - '5672:5672'     # RabbitMQ instance
      - '15672:15672'   # Web interface
```

Run the following command in the directory where docker-compose.yaml is located:
```
docker-compose up -d
```

This will start RabbitMQ and make the management interface available at [http://localhost:15672](http://localhost:15672).

## 2. Run the Console Application

The console application listens for messages from the RabbitMQ queue.

**File:** `Program.cs`  
**Key Actions:** Create a connection to RabbitMQ, declare the queue, and consume messages.

To run the console application:
```sh
dotnet run
```

## 3. Run the API Application
The API application exposes an endpoint to create bookings and sends messages to the RabbitMQ queue.

**File:** Program.cs
**Key Actions:** Configure services, add IMessageProducer implementation, and start the API.

To run the API application:
```sh
dotnet run
```

## 4. Test the Application

### Send a Booking Request
Use Postman or curl to send a POST request to the API.

**Example curl command:**

```sh
curl -X POST https://localhost:5001/api/bookings -H "Content-Type: application/json" -d '{"Id":1,"CustomerName":"John Doe","BookingDate":"2024-06-30T00:00:00"}'
```

## Check the Console Application

The console application should display the received booking message.

## Troubleshooting

### Common Issue: RESOURCE_LOCKED Error

If you encounter the `RESOURCE_LOCKED` error, it is likely due to the exclusive property on the queue declaration.

**Solution:** Ensure the `exclusive` parameter is set to `false` in both the console application and API application.

- **Console Application:** `exclusive: false`
- **API Application:** `exclusive: false`

## Verify RabbitMQ Setup

- **Access the RabbitMQ Management Interface:** [http://localhost:15672](http://localhost:15672)
- **Check the bookings Queue:** Ensure that the queue is declared and messages are being sent and received correctly.

## Conclusion

This application demonstrates basic RabbitMQ usage in a .NET Core environment. It includes an API for creating bookings and a console application for processing these bookings.

Feel free to extend the application for more advanced use cases.

## Additional Resources

- [RabbitMQ Documentation](https://www.rabbitmq.com/documentation.html)
- [.NET 6 Documentation](https://learn.microsoft.com/en-us/dotnet/core/)
