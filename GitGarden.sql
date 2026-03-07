Create Database GitGardensDB;

/* Roles Table */
Create Table Roles(
RoleID int Primary Key Identity(1,1),
RoleName Nvarchar(75) not null Unique
);

Insert into Roles (RoleName) Values
('Admin'),
('User');

/* Users Table */
Create Table Users(
UserID Int Primary Key Identity(1,1),
FullName Nvarchar(175) Not Null,
Email Nvarchar(150) Not Null Unique,
Password Nvarchar(250) Not Null,
RoleID int not Null Foreign Key References Roles(RoleID),
);

/* Gardens Table - Owned by a Users */
/* One User - Many Gardens */
Create Table Gardens (
    GardenID Int Primary Key Identity(1,1),
    GardenName Nvarchar(150) not Null,
    Description Nvarchar(250) null,

    -- Timestamps
    CreatedAt DateTime2 Not Null Default GetDate(),
    UpdatedAt DateTime2 Null,

    -- Owner of the Garden
    UserID int not Null Foreign Key References Users(UserID),
);

/* Garden Metrics - Stores user inputted Measurments */
/* One Garden - Many Metric Records */

Create Table GardenMetrics (
    MetricID Int Primary Key Identity(1,1),

    -- Metrics
    Moisture Decimal(5,2) not Null,
    PH Decimal(4,2) not Null,
    Temperature Decimal(5,2) not Null,
    Humidity Decimal(5,2) not Null,
    Sunlight Decimal(5,2) not Null,
    Nitrogen Decimal(5,2)not Null,

    -- Timestamp
    RecordedAt DateTime2 not Null Default GetDate(),

    GardenID int not Null Foreign Key References Gardens(GardenID),      -- Reference to the garden
);