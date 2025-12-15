using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace BelajarEntityFramework.Models
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                    new Product
                    {
                        ProductId = 1,
                        Name = "Alpha Watch",
                        Description = "A smart and stylish watch from AlphaTech.",
                        Brand = "AlphaTech",
                        Price = 199.99m,
                        Discount = 5
                    },
                    new Product
                    {
                        ProductId = 2,
                        Name = "Alpha Mobile",
                        Description = "A high-performance mobile device by AlphaTech.",
                        Brand = "AlphaTech",
                        Price = 499.99m,
                        Discount = 10
                    },
                    new Product
                    {
                        ProductId = 3,
                        Name = "Alpha Laptop",
                        Description = "A lightweight and powerful laptop from AlphaTech.",
                        Brand = "AlphaTech",
                        Price = 999.99m,
                        Discount = 15
                    },
                    // BetaWorks products
                    new Product
                    {
                        ProductId = 4,
                        Name = "Beta Watch",
                        Description = "An elegant watch featuring modern functionalities by BetaWorks.",
                        Brand = "BetaWorks",
                        Price = 149.99m,
                        Discount = 5
                    },
                    new Product
                    {
                        ProductId = 5,
                        Name = "Beta Mobile",
                        Description = "An advanced mobile device with cutting-edge technology from BetaWorks.",
                        Brand = "BetaWorks",
                        Price = 599.99m,
                        Discount = 10
                    },
                    new Product
                    {
                        ProductId = 6,
                        Name = "Beta Laptop",
                        Description = "A powerful laptop built for both gaming and professional use by BetaWorks.",
                        Brand = "BetaWorks",
                        Price = 1099.99m,
                        Discount = 20
                    },
                    // GammaCorp products
                    new Product
                    {
                        ProductId = 7,
                        Name = "Gamma Watch",
                        Description = "A sporty watch with fitness tracking features from GammaCorp.",
                        Brand = "GammaCorp",
                        Price = 129.99m,
                        Discount = 5
                    },
                    new Product
                    {
                        ProductId = 8,
                        Name = "Gamma Mobile",
                        Description = "A compact mobile device with excellent battery life by GammaCorp.",
                        Brand = "GammaCorp",
                        Price = 399.99m,
                        Discount = 5
                    },
                    new Product
                    {
                        ProductId = 9,
                        Name = "Gamma Laptop",
                        Description = "A versatile laptop with long-lasting battery performance from GammaCorp.",
                        Brand = "GammaCorp",
                        Price = 899.99m,
                        Discount = 10
                    }
                );

            // Seed ProofType master table.
            modelBuilder.Entity<ProofType>().HasData(
                new ProofType { Id = 1, Name = "Passport" },
                new ProofType { Id = 2, Name = "Driver License" },
                new ProofType { Id = 3, Name = "National ID" },
                new ProofType { Id = 4, Name = "Voter ID" },
                new ProofType { Id = 5, Name = "Aadhar" },
                new ProofType { Id = 6, Name = "Pan" },
                new ProofType { Id = 7, Name = "Other" }
            );
            // Seed VerificationStatus master table.
            modelBuilder.Entity<VerificationStatus>().HasData(
                new VerificationStatus { Id = 1, Name = "Pending" },
                new VerificationStatus { Id = 2, Name = "Verified" },
                new VerificationStatus { Id = 3, Name = "Rejected" }
            );

            modelBuilder.Entity<DepartmentExcel>().HasData(
                new DepartmentExcel { DepartmentId = 1, Name = "HR" },
                new DepartmentExcel { DepartmentId = 2, Name = "Finance" },
                new DepartmentExcel { DepartmentId = 3, Name = "IT" }
            );
            // Seed Employee Types
            modelBuilder.Entity<EmployeeType>().HasData(
                new EmployeeType { EmployeeTypeId = 1, TypeName = "Full Time" },
                new EmployeeType { EmployeeTypeId = 2, TypeName = "Part Time" },
                new EmployeeType { EmployeeTypeId = 3, TypeName = "Contractor" }
            );
            // Seed Employees (10 employees, each on a single row)
            modelBuilder.Entity<EmployeeExcel>().HasData(
                new EmployeeExcel { EmployeeId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", DepartmentId = 1, EmployeeTypeId = 1, JoinDate = new DateTime(2020, 1, 15), Salary = 60000, IsActive = true },
                new EmployeeExcel { EmployeeId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", DepartmentId = 2, EmployeeTypeId = 2, JoinDate = new DateTime(2021, 3, 1), Salary = 40000, IsActive = true },
                new EmployeeExcel { EmployeeId = 3, FirstName = "Bob", LastName = "Johnson", Email = "bob.johnson@example.com", DepartmentId = 3, EmployeeTypeId = 3, JoinDate = new DateTime(2022, 7, 10), Salary = 50000, IsActive = false },
                new EmployeeExcel { EmployeeId = 4, FirstName = "Alice", LastName = "Williams", Email = "alice.williams@example.com", DepartmentId = 1, EmployeeTypeId = 1, JoinDate = new DateTime(2019, 5, 20), Salary = 65000, IsActive = true },
                new EmployeeExcel { EmployeeId = 5, FirstName = "Michael", LastName = "Brown", Email = "michael.brown@example.com", DepartmentId = 2, EmployeeTypeId = 2, JoinDate = new DateTime(2018, 8, 15), Salary = 55000, IsActive = true },
                new EmployeeExcel { EmployeeId = 6, FirstName = "Linda", LastName = "Davis", Email = "linda.davis@example.com", DepartmentId = 3, EmployeeTypeId = 3, JoinDate = new DateTime(2023, 2, 1), Salary = 48000, IsActive = false },
                new EmployeeExcel { EmployeeId = 7, FirstName = "David", LastName = "Miller", Email = "david.miller@example.com", DepartmentId = 1, EmployeeTypeId = 1, JoinDate = new DateTime(2020, 9, 10), Salary = 62000, IsActive = true },
                new EmployeeExcel { EmployeeId = 8, FirstName = "Susan", LastName = "Wilson", Email = "susan.wilson@example.com", DepartmentId = 2, EmployeeTypeId = 2, JoinDate = new DateTime(2021, 1, 5), Salary = 43000, IsActive = true },
                new EmployeeExcel { EmployeeId = 9, FirstName = "Robert", LastName = "Moore", Email = "robert.moore@example.com", DepartmentId = 3, EmployeeTypeId = 3, JoinDate = new DateTime(2022, 11, 1), Salary = 51000, IsActive = false },
                new EmployeeExcel { EmployeeId = 10, FirstName = "Karen", LastName = "Taylor", Email = "karen.taylor@example.com", DepartmentId = 1, EmployeeTypeId = 1, JoinDate = new DateTime(2019, 12, 12), Salary = 70000, IsActive = true }
            );
            // Seed Attendance records (2 per employee, each on a single row)
            modelBuilder.Entity<Attendance>().HasData(
                new Attendance { AttendanceId = 1, EmployeeId = 1, Date = DateTime.Today.AddDays(-1), IsPresent = true },
                new Attendance { AttendanceId = 2, EmployeeId = 1, Date = DateTime.Today, IsPresent = true },
                new Attendance { AttendanceId = 3, EmployeeId = 2, Date = DateTime.Today.AddDays(-1), IsPresent = true },
                new Attendance { AttendanceId = 4, EmployeeId = 2, Date = DateTime.Today, IsPresent = false },
                new Attendance { AttendanceId = 5, EmployeeId = 3, Date = DateTime.Today.AddDays(-1), IsPresent = false },
                new Attendance { AttendanceId = 6, EmployeeId = 3, Date = DateTime.Today, IsPresent = true },
                new Attendance { AttendanceId = 7, EmployeeId = 4, Date = DateTime.Today.AddDays(-1), IsPresent = true },
                new Attendance { AttendanceId = 8, EmployeeId = 4, Date = DateTime.Today, IsPresent = true },
                new Attendance { AttendanceId = 9, EmployeeId = 5, Date = DateTime.Today.AddDays(-1), IsPresent = true },
                new Attendance { AttendanceId = 10, EmployeeId = 5, Date = DateTime.Today, IsPresent = false },
                new Attendance { AttendanceId = 11, EmployeeId = 6, Date = DateTime.Today.AddDays(-1), IsPresent = false },
                new Attendance { AttendanceId = 12, EmployeeId = 6, Date = DateTime.Today, IsPresent = true },
                new Attendance { AttendanceId = 13, EmployeeId = 7, Date = DateTime.Today.AddDays(-1), IsPresent = true },
                new Attendance { AttendanceId = 14, EmployeeId = 7, Date = DateTime.Today, IsPresent = true },
                new Attendance { AttendanceId = 15, EmployeeId = 8, Date = DateTime.Today.AddDays(-1), IsPresent = true },
                new Attendance { AttendanceId = 16, EmployeeId = 8, Date = DateTime.Today, IsPresent = false },
                new Attendance { AttendanceId = 17, EmployeeId = 9, Date = DateTime.Today.AddDays(-1), IsPresent = false },
                new Attendance { AttendanceId = 18, EmployeeId = 9, Date = DateTime.Today, IsPresent = true },
                new Attendance { AttendanceId = 19, EmployeeId = 10, Date = DateTime.Today.AddDays(-1), IsPresent = true },
                new Attendance { AttendanceId = 20, EmployeeId = 10, Date = DateTime.Today, IsPresent = true }
            );

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Electronics", Description = "Electronic gadgets and devices" },
                new Category { CategoryId = 2, Name = "Apparel", Description = "Clothing and fashion accessories" },
                new Category { CategoryId = 3, Name = "Home Goods", Description = "Furniture and home decor" }
            );
            // Seed Suppliers
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { SupplierId = 1, Name = "TechSource", ContactEmail = "contact@techsource.com", PhoneNumber = "555-1234" },
                new Supplier { SupplierId = 2, Name = "FashionHub", ContactEmail = "sales@fashionhub.com", PhoneNumber = "555-5678" }
            );
            // Assume the Year of Creating is 2025 for seed data.
            // The SKU is computed as:
            // [First 3 letters of Category]-[First 3 letters of Brand]-[First 3 letters of Product Name]-[First 3 letters of Supplier]-Year (2025)
            // If Brand is missing, "BRD" is used.
            // (For these seeds, all Categories and Suppliers are available)
            modelBuilder.Entity<ProductExcel>().HasData(
                new ProductExcel
                {
                    ProductId = 1,
                    Name = "Wireless Headphones",
                    Description = "Noise-cancelling over-ear headphones",
                    Price = 99.99m,
                    Quantity = 50,
                    Brand = "SoundMax",
                    DiscountPercentage = 10,
                    SKU = "ELE-SOU-WIR-TEC-2025", // Electronics (ELE) - SoundMax (SOU) - Wireless (WIR) - TechSource (TEC) - 2025
                    IsActive = true,
                    CategoryId = 1, // Electronics
                    SupplierId = 1  // TechSource
                },
                new ProductExcel
                {
                    ProductId = 2,
                    Name = "Designer T-Shirt",
                    Description = "100% cotton, trendy design",
                    Price = 29.99m,
                    Quantity = 150,
                    Brand = "FashionCo",
                    DiscountPercentage = 5,
                    SKU = "APP-FAS-DES-FAS-2025", // Apparel (APP) - FashionCo (FAS) - Designer (DES) - FashionHub (FAS) - 2025
                    IsActive = true,
                    CategoryId = 2, // Apparel
                    SupplierId = 2  // FashionHub
                },
                new ProductExcel
                {
                    ProductId = 3,
                    Name = "Smart LED TV",
                    Description = "50-inch 4K Ultra HD Smart LED TV",
                    Price = 499.99m,
                    Quantity = 30,
                    Brand = "VisionTech",
                    DiscountPercentage = 15,
                    SKU = "ELE-VIS-SMA-TEC-2025", // Electronics (ELE) - VisionTech (VIS) - Smart (SMA) - TechSource (TEC) - 2025
                    IsActive = true,
                    CategoryId = 1, // Electronics
                    SupplierId = 1  // TechSource
                },
                new ProductExcel
                {
                    ProductId = 4,
                    Name = "Modern Sofa",
                    Description = "Comfortable 3-seater sofa",
                    Price = 299.99m,
                    Quantity = 20,
                    Brand = "HomeComfort",
                    DiscountPercentage = 8,
                    SKU = "HOM-HOM-MOD-FAS-2025", // Home Goods (HOM) - HomeComfort (HOM) - Modern (MOD) - FashionHub (FAS) - 2025
                    IsActive = true,
                    CategoryId = 3, // Home Goods
                    SupplierId = 2  // FashionHub
                },
                new ProductExcel
                {
                    ProductId = 5,
                    Name = "Bluetooth Speaker",
                    Description = "Portable wireless speaker with deep bass",
                    Price = 49.99m,
                    Quantity = 80,
                    Brand = "SoundMax",
                    DiscountPercentage = 12,
                    SKU = "ELE-SOU-BLU-TEC-2025", // Electronics (ELE) - SoundMax (SOU) - Bluetooth (BLU) - TechSource (TEC) - 2025
                    IsActive = true,
                    CategoryId = 1, // Electronics
                    SupplierId = 1  // TechSource
                },
                new ProductExcel
                {
                    ProductId = 6,
                    Name = "Casual Sneakers",
                    Description = "Comfortable and stylish sneakers",
                    Price = 59.99m,
                    Quantity = 100,
                    // Brand is missing (empty), so default value "BRD" is used in the SKU.
                    Brand = "",
                    DiscountPercentage = 0,
                    SKU = "APP-BRD-CAS-FAS-2025", // Apparel (APP) - Missing Brand => (BRD) - Casual (CAS) - FashionHub (FAS) - 2025
                    IsActive = true,
                    CategoryId = 2, // Apparel
                    SupplierId = 2  // FashionHub
                }
            );

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId);
            // One-to-many relationship between Order and OrderItems.
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);
            // Seed data for Products.
            modelBuilder.Entity<ProductPdf>().HasData(
                new ProductPdf { ProductId = 1, Name = "Laptop", Description = "High performance laptop", Price = 1200.00m },
                new ProductPdf { ProductId = 2, Name = "Smartphone", Description = "Latest model smartphone", Price = 800.00m },
                new ProductPdf { ProductId = 3, Name = "Headphones", Description = "Noise-cancelling headphones", Price = 150.00m }
            );
            // Seed data for Customers.
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, Name = "Alice Johnson", Email = "alice@example.com", Phone = "555-1234", Address = "123 Main St, City A" },
                new Customer { CustomerId = 2, Name = "Bob Smith", Email = "bob@example.com", Phone = "555-5678", Address = "456 Elm St, City B" }
            );
            // Seed data for Orders.
            // For OrderId 1: 
            //   Items: 1 × Laptop (Price 1200, TaxPercent 10) and 2 × Headphones (Price 150, TaxPercent 10).
            //   SubTotal = (1200*1)+(150*2) = 1500, TotalTax = (1200*1*0.10)+(150*2*0.10)=120+30=150, GrandTotal = 1650.
            // For OrderId 2:
            //   Items: 1 × Smartphone (Price 800, TaxPercent 10).
            //   SubTotal = 800, TotalTax = 800*0.10=80, GrandTotal = 880.
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    OrderId = 1,
                    OrderNumber = "ORD1001",
                    OrderDate = DateTime.Today.AddDays(-5),
                    CustomerId = 1,
                    SubTotal = 1500.00m,
                    TotalTax = 150.00m,
                    GrandTotal = 1650.00m
                },
                new Order
                {
                    OrderId = 2,
                    OrderNumber = "ORD1002",
                    OrderDate = DateTime.Today.AddDays(-2),
                    CustomerId = 2,
                    SubTotal = 800.00m,
                    TotalTax = 80.00m,
                    GrandTotal = 880.00m
                }
            );
            // Seed data for OrderItems (using TaxPercent instead of Tax amount).
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { OrderItemId = 1, OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 1200.00m, TaxPercent = 10.00m },
                new OrderItem { OrderItemId = 2, OrderId = 1, ProductId = 3, Quantity = 2, UnitPrice = 150.00m, TaxPercent = 10.00m },
                new OrderItem { OrderItemId = 3, OrderId = 2, ProductId = 2, Quantity = 1, UnitPrice = 800.00m, TaxPercent = 10.00m }
            );
            // Seed data for Payments with PaymentStatus.
            modelBuilder.Entity<Payment>().HasData(
                new Payment
                {
                    PaymentId = 1,
                    OrderId = 1,
                    PaymentMethod = "Credit Card",
                    PaymentAmount = 1650.00m,
                    PaymentDate = DateTime.Today.AddDays(-4),
                    PaymentStatus = "Paid"
                },
                new Payment
                {
                    PaymentId = 2,
                    OrderId = 2,
                    PaymentMethod = "PayPal",
                    PaymentAmount = 880.00m,
                    PaymentDate = DateTime.Today.AddDays(-1),
                    PaymentStatus = "Paid"
                }
            );

            // SEED CUSTOMERS (if not already seeded)
            modelBuilder.Entity<CustomerChart>().HasData(
                new CustomerChart { CustomerId = 1, Name = "Alice", Email = "alice@example.com" },
                new CustomerChart { CustomerId = 2, Name = "Bob", Email = "bob@example.com" }
            );
            // SEED PRODUCTS (with Category property assumed to be added)
            modelBuilder.Entity<ProductChart>().HasData(
                new ProductChart { ProductId = 1, Name = "Laptop", Price = 1200m, StockQuantity = 10, Category = "Electronics" },
                new ProductChart { ProductId = 2, Name = "Phone", Price = 800m, StockQuantity = 15, Category = "Electronics" },
                new ProductChart { ProductId = 3, Name = "Headphones", Price = 100m, StockQuantity = 30, Category = "Accessories" }
            );
            // SEED 18 ORDERS with OrderNumber and various statuses/dates
            modelBuilder.Entity<OrderChart>().HasData(
                new OrderChart { OrderId = 1, OrderNumber = "ORD-0001", CustomerId = 1, OrderDate = DateTime.Today.AddDays(-1), TotalAmount = 1300m, OrderStatus = OrderStatus.Completed },
                new OrderChart { OrderId = 2, OrderNumber = "ORD-0002", CustomerId = 2, OrderDate = DateTime.Today.AddDays(-1), TotalAmount = 2200m, OrderStatus = OrderStatus.Pending },
                new OrderChart { OrderId = 3, OrderNumber = "ORD-0003", CustomerId = 1, OrderDate = DateTime.Today.AddDays(-3), TotalAmount = 500m, OrderStatus = OrderStatus.Cancelled },
                new OrderChart { OrderId = 4, OrderNumber = "ORD-0004", CustomerId = 2, OrderDate = DateTime.Today.AddDays(-3), TotalAmount = 1600m, OrderStatus = OrderStatus.Completed },
                new OrderChart { OrderId = 5, OrderNumber = "ORD-0005", CustomerId = 1, OrderDate = DateTime.Today.AddDays(-3), TotalAmount = 900m, OrderStatus = OrderStatus.Pending },
                new OrderChart { OrderId = 6, OrderNumber = "ORD-0006", CustomerId = 2, OrderDate = DateTime.Today.AddDays(-4), TotalAmount = 1800m, OrderStatus = OrderStatus.Completed },
                new OrderChart { OrderId = 7, OrderNumber = "ORD-0007", CustomerId = 1, OrderDate = DateTime.Today.AddDays(-5), TotalAmount = 1100m, OrderStatus = OrderStatus.Completed },
                new OrderChart { OrderId = 8, OrderNumber = "ORD-0008", CustomerId = 2, OrderDate = DateTime.Today.AddDays(-5), TotalAmount = 2500m, OrderStatus = OrderStatus.Cancelled },
                new OrderChart { OrderId = 9, OrderNumber = "ORD-0009", CustomerId = 1, OrderDate = DateTime.Today.AddDays(-5), TotalAmount = 1700m, OrderStatus = OrderStatus.Pending },
                new OrderChart { OrderId = 10, OrderNumber = "ORD-0010", CustomerId = 2, OrderDate = DateTime.Today.AddDays(-5), TotalAmount = 1400m, OrderStatus = OrderStatus.Completed },
                new OrderChart { OrderId = 11, OrderNumber = "ORD-0011", CustomerId = 1, OrderDate = DateTime.Today.AddDays(-25), TotalAmount = 2000m, OrderStatus = OrderStatus.Completed },
                new OrderChart { OrderId = 12, OrderNumber = "ORD-0012", CustomerId = 2, OrderDate = DateTime.Today.AddDays(-25), TotalAmount = 2100m, OrderStatus = OrderStatus.Pending },
                new OrderChart { OrderId = 13, OrderNumber = "ORD-0013", CustomerId = 1, OrderDate = DateTime.Today.AddDays(-25), TotalAmount = 1300m, OrderStatus = OrderStatus.Completed },
                new OrderChart { OrderId = 14, OrderNumber = "ORD-0014", CustomerId = 2, OrderDate = DateTime.Today.AddDays(-35), TotalAmount = 1900m, OrderStatus = OrderStatus.Cancelled },
                new OrderChart { OrderId = 15, OrderNumber = "ORD-0015", CustomerId = 1, OrderDate = DateTime.Today.AddDays(-35), TotalAmount = 1500m, OrderStatus = OrderStatus.Completed },
                new OrderChart { OrderId = 16, OrderNumber = "ORD-0016", CustomerId = 1, OrderDate = DateTime.Today, TotalAmount = 300m, OrderStatus = OrderStatus.Pending },
                new OrderChart { OrderId = 17, OrderNumber = "ORD-0017", CustomerId = 2, OrderDate = DateTime.Today, TotalAmount = 800m, OrderStatus = OrderStatus.Completed },
                new OrderChart { OrderId = 18, OrderNumber = "ORD-0018", CustomerId = 1, OrderDate = DateTime.Today, TotalAmount = 150m, OrderStatus = OrderStatus.Cancelled }
            );
            // SEED ORDER ITEMS
            modelBuilder.Entity<OrderItemChart>().HasData(
                new OrderItemChart { OrderItemId = 1, OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 1200m },
                new OrderItemChart { OrderItemId = 2, OrderId = 1, ProductId = 3, Quantity = 1, UnitPrice = 100m },
                new OrderItemChart { OrderItemId = 3, OrderId = 2, ProductId = 1, Quantity = 1, UnitPrice = 1200m },
                new OrderItemChart { OrderItemId = 4, OrderId = 2, ProductId = 2, Quantity = 1, UnitPrice = 1000m },
                new OrderItemChart { OrderItemId = 5, OrderId = 3, ProductId = 3, Quantity = 5, UnitPrice = 100m },
                new OrderItemChart { OrderItemId = 6, OrderId = 4, ProductId = 2, Quantity = 2, UnitPrice = 800m },
                new OrderItemChart { OrderItemId = 7, OrderId = 5, ProductId = 3, Quantity = 3, UnitPrice = 300m },
                new OrderItemChart { OrderItemId = 8, OrderId = 6, ProductId = 1, Quantity = 1, UnitPrice = 1200m },
                new OrderItemChart { OrderItemId = 9, OrderId = 6, ProductId = 3, Quantity = 1, UnitPrice = 600m },
                new OrderItemChart { OrderItemId = 10, OrderId = 7, ProductId = 2, Quantity = 1, UnitPrice = 1100m },
                new OrderItemChart { OrderItemId = 11, OrderId = 8, ProductId = 1, Quantity = 1, UnitPrice = 2500m },
                new OrderItemChart { OrderItemId = 12, OrderId = 9, ProductId = 3, Quantity = 2, UnitPrice = 850m },
                new OrderItemChart { OrderItemId = 13, OrderId = 10, ProductId = 2, Quantity = 1, UnitPrice = 1400m },
                new OrderItemChart { OrderItemId = 14, OrderId = 11, ProductId = 1, Quantity = 1, UnitPrice = 2000m },
                new OrderItemChart { OrderItemId = 15, OrderId = 12, ProductId = 2, Quantity = 1, UnitPrice = 2100m },
                new OrderItemChart { OrderItemId = 16, OrderId = 13, ProductId = 3, Quantity = 1, UnitPrice = 1300m },
                new OrderItemChart { OrderItemId = 17, OrderId = 14, ProductId = 1, Quantity = 1, UnitPrice = 1900m },
                new OrderItemChart { OrderItemId = 18, OrderId = 15, ProductId = 2, Quantity = 1, UnitPrice = 1500m },
                new OrderItemChart { OrderItemId = 19, OrderId = 16, ProductId = 3, Quantity = 3, UnitPrice = 100m },
                new OrderItemChart { OrderItemId = 20, OrderId = 17, ProductId = 2, Quantity = 1, UnitPrice = 800m },
                new OrderItemChart { OrderItemId = 21, OrderId = 18, ProductId = 3, Quantity = 1, UnitPrice = 150m }
            );
        }
        public DbSet<CustomerChart> CustomersChart { get; set; }
        public DbSet<OrderChart> OrdersChart { get; set; }
        public DbSet<OrderItemChart> OrderItemsChart { get; set; }
        public DbSet<ProductChart> ProductsChart { get; set; }

        // DbSets for each entity.
        public DbSet<ProductPdf> ProductsPdf { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ProductExcel> ProductsExcel { get; set; }
        public DbSet<DepartmentExcel> DepartmentsExcel { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<EmployeeExcel> EmployeesExcel { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<IdentityProof> IdentityProofs { get; set; }
        public DbSet<ProofType> ProofTypes { get; set; }
        public DbSet<VerificationStatus> VerificationStatuses { get; set; }
    }
}