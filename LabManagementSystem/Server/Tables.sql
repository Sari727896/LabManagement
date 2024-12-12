CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    GUID UNIQUEIDENTIFIER DEFAULT NEWID(),
    FullName NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(50) NOT NULL,
    Email NVARCHAR(255) NULL,
    Comments NVARCHAR(MAX) NULL,
    IsActive BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0,
    DateCreated DATETIME DEFAULT GETDATE(),
    DateModified DATETIME NULL,
    CONSTRAINT CHK_Customer_IsActive CHECK (IsActive IN (0, 1)),
    CONSTRAINT CHK_Customer_IsDeleted CHECK (IsDeleted IN (0, 1))
);

CREATE TABLE DeviceType (
    DeviceTypeID INT PRIMARY KEY IDENTITY(1,1),
    GUID UNIQUEIDENTIFIER DEFAULT NEWID(),
    TypeName NVARCHAR(100) NOT NULL,
    IsActive BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0,
    DateCreated DATETIME DEFAULT GETDATE(),
    DateModified DATETIME NULL,
    CONSTRAINT CHK_DeviceType_IsActive CHECK (IsActive IN (0, 1)),
    CONSTRAINT CHK_DeviceType_IsDeleted CHECK (IsDeleted IN (0, 1))
);

CREATE TABLE Device (
    DeviceID INT PRIMARY KEY IDENTITY(1,1),
    GUID UNIQUEIDENTIFIER DEFAULT NEWID(),
    DeviceType_Tbl_ID INT NOT NULL,
    Model NVARCHAR(255) NOT NULL,
    IssueDescription NVARCHAR(MAX) NULL,
    UnlockCode NVARCHAR(100) NULL,
    IsActive BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0,
    DateCreated DATETIME DEFAULT GETDATE(),
    DateModified DATETIME NULL,
    CONSTRAINT FK_Device_DeviceType_Tbl_ID FOREIGN KEY (DeviceType_Tbl_ID) REFERENCES DeviceType(DeviceTypeID),
    CONSTRAINT CHK_Device_IsActive CHECK (IsActive IN (0, 1)),
    CONSTRAINT CHK_Device_IsDeleted CHECK (IsDeleted IN (0, 1))
);

CREATE TABLE Status (
    StatusID INT PRIMARY KEY IDENTITY(1,1),
    GUID UNIQUEIDENTIFIER DEFAULT NEWID(),
    StatusName NVARCHAR(100) NOT NULL,
    IsActive BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0,
    DateCreated DATETIME DEFAULT GETDATE(),
    DateModified DATETIME NULL,
    CONSTRAINT CHK_Status_IsActive CHECK (IsActive IN (0, 1)),
    CONSTRAINT CHK_Status_IsDeleted CHECK (IsDeleted IN (0, 1))
);

CREATE TABLE [Order] (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    GUID UNIQUEIDENTIFIER DEFAULT NEWID(),
    Customer_Tbl_ID INT NOT NULL,
    Device_Tbl_ID INT NOT NULL,
    Status_Tbl_ID INT NOT NULL,
    EstimatedPrice DECIMAL(18,2) NULL,
    FinalPrice DECIMAL(18,2) NULL,
    Comments NVARCHAR(MAX) NULL,
    IsActive BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0,
    DateCreated DATETIME DEFAULT GETDATE(),
    DateModified DATETIME NULL,
    CONSTRAINT FK_Order_Customer_Tbl_ID FOREIGN KEY (Customer_Tbl_ID) REFERENCES Customer(CustomerID),
    CONSTRAINT FK_Order_Device_Tbl_ID FOREIGN KEY (Device_Tbl_ID) REFERENCES Device(DeviceID),
    CONSTRAINT FK_Order_Status_Tbl_ID FOREIGN KEY (Status_Tbl_ID) REFERENCES Status(StatusID),
    CONSTRAINT CHK_Order_IsActive CHECK (IsActive IN (0, 1)),
    CONSTRAINT CHK_Order_IsDeleted CHECK (IsDeleted IN (0, 1))
);


