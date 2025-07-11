# Supermarket Customer Membership Point Tracker

A comprehensive customer loyalty management system built with ASP.NET Core Web API and React TypeScript frontend.

## Features

### Customer Features
- **Account Management**: Registration, login, profile management
- **Point Tracking**: Real-time point balance and transaction history
- **Tier System**: Bronze, Silver, Gold, Platinum membership levels
- **Rewards Catalog**: Browse and redeem rewards with points
- **Transaction History**: Detailed purchase and redemption records
- **Point Expiration**: Track expiring points with notifications

### Admin Features
- **Customer Management**: View, create, edit customer accounts
- **Product Management**: Manage product catalog with point values
- **Campaign Management**: Create promotional campaigns with bonus points
- **Analytics Dashboard**: Comprehensive reporting and insights
- **Reward Management**: Create and manage reward offerings
- **Transaction Monitoring**: View all customer transactions

### Technical Features
- **JWT Authentication**: Secure token-based authentication
- **Role-based Authorization**: Customer and Admin roles
- **Entity Framework Core**: Database operations with SQL Server
- **AutoMapper**: Object-to-object mapping
- **Swagger Documentation**: API documentation and testing
- **Responsive Design**: Mobile-friendly interface
- **Real-time Updates**: Live data synchronization

## Technology Stack

### Backend
- **ASP.NET Core 8.0**: Web API framework
- **Entity Framework Core**: ORM for database operations
- **SQL Server**: Database management system
- **ASP.NET Core Identity**: Authentication and authorization
- **JWT Bearer**: Token-based authentication
- **AutoMapper**: Object mapping
- **FluentValidation**: Input validation
- **Serilog**: Logging framework
- **Swagger/OpenAPI**: API documentation

### Frontend
- **React 18**: Frontend framework
- **TypeScript**: Type-safe JavaScript
- **Tailwind CSS**: Utility-first CSS framework
- **Axios**: HTTP client for API calls
- **React Router**: Client-side routing
- **Lucide React**: Icon library

## Project Structure

```
SupermarketLoyalty/
├── SupermarketLoyalty.sln
├── SupermarketLoyalty.API/          # Web API project
│   ├── Controllers/                 # API controllers
│   ├── Program.cs                   # Application entry point
│   └── appsettings.json            # Configuration
├── SupermarketLoyalty.Core/         # Domain layer
│   ├── Entities/                    # Domain entities
│   ├── DTOs/                        # Data transfer objects
│   └── Interfaces/                  # Service interfaces
├── SupermarketLoyalty.Infrastructure/ # Data access layer
│   ├── Data/                        # Database context and configuration
│   └── Services/                    # Service implementations
└── SupermarketLoyalty.Web/          # React frontend
    └── ClientApp/                   # React application
```

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- Node.js 18+ and npm
- SQL Server (LocalDB for development)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd SupermarketLoyalty
   ```

2. **Restore .NET packages**
   ```bash
   dotnet restore
   ```

3. **Install npm packages**
   ```bash
   cd SupermarketLoyalty.Web/ClientApp
   npm install
   ```

4. **Update database connection string**
   - Edit `SupermarketLoyalty.API/appsettings.json`
   - Update the `DefaultConnection` string for your SQL Server instance

5. **Run database migrations**
   ```bash
   cd SupermarketLoyalty.API
   dotnet ef database update
   ```

6. **Start the application**
   ```bash
   # Start API (from SupermarketLoyalty.API directory)
   dotnet run

   # Start React app (from SupermarketLoyalty.Web directory)
   dotnet run
   ```

### Default Accounts

**Customer Accounts:**
- Email: `john.doe@email.com` | Password: `Password123!` (Gold Member)
- Email: `jane.smith@email.com` | Password: `Password123!` (Silver Member)
- Email: `bob.johnson@email.com` | Password: `Password123!` (Bronze Member)

**Admin Account:**
- Email: `admin@supermart.com` | Password: `Admin123!`

## API Endpoints

### Authentication
- `POST /api/auth/login` - User login
- `POST /api/auth/register` - Customer registration

### Customers
- `GET /api/customers` - Get all customers (Admin)
- `GET /api/customers/{id}` - Get customer by ID
- `POST /api/customers` - Create customer (Admin)
- `PUT /api/customers/{id}` - Update customer
- `DELETE /api/customers/{id}` - Delete customer (Admin)

### Products
- `GET /api/products` - Get all products
- `POST /api/products` - Create product (Admin)
- `PUT /api/products/{id}` - Update product (Admin)
- `DELETE /api/products/{id}` - Delete product (Admin)

### Transactions
- `GET /api/transactions` - Get all transactions (Admin)
- `GET /api/transactions/customer/{id}` - Get customer transactions
- `POST /api/transactions` - Create transaction (Admin)

### Rewards
- `GET /api/rewards` - Get all rewards
- `GET /api/rewards/available/{customerId}` - Get available rewards
- `POST /api/rewards/redeem` - Redeem reward
- `POST /api/rewards` - Create reward (Admin)

### Analytics
- `GET /api/analytics` - Get analytics dashboard (Admin)
- `GET /api/analytics/top-products` - Get top products
- `GET /api/analytics/customers-by-tier` - Get tier distribution

## Database Schema

### Key Entities
- **Customer**: User accounts with membership details
- **Product**: Store inventory with point values
- **Transaction**: Purchase records with point calculations
- **Reward**: Available rewards for redemption
- **Campaign**: Promotional campaigns with bonus multipliers
- **ExpiringPoint**: Point expiration tracking

## Configuration

### JWT Settings
```json
{
  "JwtSettings": {
    "Secret": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "SupermarketLoyalty",
    "Audience": "SupermarketLoyalty",
    "ExpirationInMinutes": 60
  }
}
```

### Database Connection
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SupermarketLoyaltyDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

## Development

### Adding New Features
1. Create entities in `SupermarketLoyalty.Core/Entities`
2. Add DTOs in `SupermarketLoyalty.Core/DTOs`
3. Define interfaces in `SupermarketLoyalty.Core/Interfaces`
4. Implement services in `SupermarketLoyalty.Infrastructure/Services`
5. Create controllers in `SupermarketLoyalty.API/Controllers`
6. Update database context and run migrations

### Running Tests
```bash
dotnet test
```

### Building for Production
```bash
dotnet publish -c Release
```

## Deployment

### Database Setup
1. Create production database
2. Update connection string
3. Run migrations: `dotnet ef database update`

### Application Deployment
1. Build the application: `dotnet publish -c Release`
2. Deploy to IIS, Azure App Service, or Docker
3. Configure environment variables
4. Set up SSL certificates

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For support and questions, please create an issue in the repository or contact the development team.