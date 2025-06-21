--CREATE DATABASE PRUEBACSF

USE PRUEBACSF;
GO
CREATE TABLE Productos (
    Id_producto INT PRIMARY KEY IDENTITY(1,1),
    Nombre_producto NVARCHAR(100) NOT NULL,
    NroLote NVARCHAR(50) NOT NULL,
    Fec_registro DATETIME NOT NULL DEFAULT GETDATE(),
    Costo DECIMAL(10, 2) NOT NULL,
    PrecioVenta DECIMAL(10, 2) NOT NULL
);

CREATE TABLE CompraCab (
    Id_CompraCab INT PRIMARY KEY IDENTITY(1,1),
    FecRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    SubTotal DECIMAL(10,2) NOT NULL,
    Igv DECIMAL(10,2) NOT NULL,
    Total DECIMAL(10,2) NOT NULL
);

CREATE TABLE CompraDet (
    Id_CompraDet INT PRIMARY KEY IDENTITY(1,1),
    Id_CompraCab INT NOT NULL,
    Id_Producto INT NOT NULL,
    Cantidad INT NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Sub_Total DECIMAL(10,2) NOT NULL,
    Igv DECIMAL(10,2) NOT NULL,
    Total DECIMAL(10,2) NOT NULL,

    FOREIGN KEY (Id_CompraCab) REFERENCES CompraCab(Id_CompraCab),
    FOREIGN KEY (Id_Producto) REFERENCES Productos(Id_Producto)
);


CREATE TABLE VentaCab (
    Id_VentaCab INT PRIMARY KEY IDENTITY(1,1),
    FecRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    SubTotal DECIMAL(10,2) NOT NULL,
    Igv DECIMAL(10,2) NOT NULL,
    Total DECIMAL(10,2) NOT NULL
);


CREATE TABLE VentaDet (
    Id_VentaDet INT PRIMARY KEY IDENTITY(1,1),
    Id_VentaCab INT NOT NULL,
    Id_Producto INT NOT NULL,
    Cantidad INT NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Sub_Total DECIMAL(10,2) NOT NULL,
    Igv DECIMAL(10,2) NOT NULL,
    Total DECIMAL(10,2) NOT NULL,

    FOREIGN KEY (Id_VentaCab) REFERENCES VentaCab(Id_VentaCab),
    FOREIGN KEY (Id_Producto) REFERENCES Productos(Id_Producto)
);

CREATE TABLE MovimientoCab (
    Id_MovimientoCab INT PRIMARY KEY IDENTITY(1,1),
    Fec_Registro DATETIME NOT NULL DEFAULT GETDATE(),
    Id_TipoMovimiento INT NOT NULL, -- (1) Entrada, (2) Salida
    Id_DocumentoOrigen INT NOT NULL,
   
);


CREATE TABLE MovimientoDet (
    Id_MovimientoDet INT PRIMARY KEY IDENTITY(1,1),
    Id_MovimientoCab INT NOT NULL,
    Id_Producto INT NOT NULL,
    Cantidad INT NOT NULL,

    FOREIGN KEY (Id_MovimientoCab) REFERENCES MovimientoCab(Id_MovimientoCab),
    FOREIGN KEY (Id_Producto) REFERENCES Productos(Id_Producto)
);


