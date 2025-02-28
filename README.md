# Netcash API Integration

## Overview

This repository provides a **.NET 9.0 API integration** for **Netcash**, a South African payment and debit order processing gateway.  
The integration enables secure **eMandate requests, batch debit order processing, and transaction reporting**.

> ⚠️ **Work In Progress**: This repository is still under active development.  
> Use it **at your own risk** as features may change, and I will **not be held liable** for any issues resulting from its use.

## Features

- **Request Netcash eMandates** for debit orders.
- **Batch debit order file generation and submission**.
- **Retrieve file upload reports** for batch processing.
- **API versioning and OpenAPI documentation**.
- **Scalar UI available at** `https://localhost:7273/scalar/v1` **for enhanced API interaction**.

## Installation

### Prerequisites

- **.NET 9.0 SDK**
- **Visual Studio 2022 or later**

### Clone Repository

```sh
git clone https://github.com/Pierre35557/dotnet-netcash-integration.git
cd dotnet-netcash-integration
```

### Running the Application

```sh
dotnet run
```

The API will be accessible at `https://localhost:7273` (or another port if configured differently).

## API Endpoints

### 📌 **Request eMandate URL**

**Endpoint:** `POST /api/v1/netcash/mandate`

**Headers:**
```http
X-Netcash-Service-Key: your-service-key
```

**Request Body:**
```json
{
  "accountReference": "ACC123",
  "mandateName": "TestMandate",
  "mandateAmount": 100.00,
  "isConsumer": true,
  "firstName": "John",
  "surname": "Doe",
  "mobileNumber": "0712345678",
  "agreementDate": "20250228",
  "agreementReferenceNumber": "AGREE123"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Mandate URL received",
  "statusCode": 201,
  "data": "https://short.surf/..."
}
```

### 📌 **Batch Debit Order Processing**

**Endpoint:** `POST /api/v1/netcash/debit-order`

**Request Body:**
```json
{
  "batchReferenceNo": "BATCH001",
  "debitOrderDate": "20250301",
  "users": [
    {
      "accountNo": "123456789",
      "amount": 500.00
    },
    {
      "accountNo": "987654321",
      "amount": 300.00
    }
  ]
}
```

**Response:**
```json
{
  "fileToken": "TOKEN123",
  "fileUploadStatus": "SUCCESSFUL",
  "fileUploadReport": "File processed successfully."
}
```

### 📌 **Fetch Batch File Upload Report**

**Endpoint:** `GET /api/v1/netcash/debit-order/report`

**Query Parameters:**
```
?serviceKey=your-service-key&fileToken=TOKEN123
```

**Response:**
```json
{
  "fileUploadStatus": "SUCCESSFUL",
  "fileUploadReport": "File processed successfully."
}
```

## OpenAPI & Scalar UI

The API documentation is available via **Scalar UI** at:

```
https://localhost:7273/scalar/v1
```

This provides an **interactive UI** for testing and exploring the API.

## Contributing

1. **Fork the repository**.
2. **Create a new branch** (`feature-branch`).
3. **Commit your changes**.
4. **Push to the branch**.
5. **Open a pull request**.

## License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

## ⚠️ Disclaimer

This repository is **a work in progress** and provided **as-is**.  
By using this code, **you accept full responsibility for any consequences**.  
I am **not liable** for any issues, financial loss, or system failures resulting from its use.

---

> 💡 **Contributions & feedback are welcome!**  
> Feel free to submit **issues** or **pull requests**.

