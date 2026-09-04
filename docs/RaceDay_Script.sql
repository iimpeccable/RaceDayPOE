-- RaceDay Database 
CREATE DATABASE RaceDay;
GO

USE RaceDay;
GO
-- Drop existing tables
DROP TABLE IF EXISTS Results, Payment, Registration, Race, Category, Runner, Organizer;

-- Organizer Table
CREATE TABLE Organizer (
    organizer_id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(100) NOT NULL,
    contact VARCHAR(100) UNIQUE
);

-- Runner Table
CREATE TABLE Runner (
    runner_id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(100) NOT NULL,
    age INT CHECK (age > 0),
    email VARCHAR(100) UNIQUE NOT NULL
);

-- Category Table
CREATE TABLE Category (
    category_id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(50) NOT NULL,
    type VARCHAR(50) NOT NULL 
);

-- Race Table
CREATE TABLE Race (
    race_id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(100) NOT NULL,
    description VARCHAR(255),
    date DATE NOT NULL,
    location VARCHAR(100),
    distance DECIMAL(5,2),
    type VARCHAR(20) CHECK (type IN ('Run','Walk','Cycle')),
    category_id INT FOREIGN KEY REFERENCES Category(category_id),
    organizer_id INT FOREIGN KEY REFERENCES Organizer(organizer_id)
);

-- Registration Table
CREATE TABLE Registration (
    reg_id INT PRIMARY KEY IDENTITY(1,1),
    runner_id INT FOREIGN KEY REFERENCES Runner(runner_id),
    race_id INT FOREIGN KEY REFERENCES Race(race_id),
    category_id INT FOREIGN KEY REFERENCES Category(category_id),
    status VARCHAR(20) DEFAULT 'Pending'
);

-- Payment Table
CREATE TABLE Payment (
    payment_id INT PRIMARY KEY IDENTITY(1,1),
    reg_id INT FOREIGN KEY REFERENCES Registration(reg_id),
    amount DECIMAL(10,2) NOT NULL,
    method VARCHAR(20) CHECK (method IN ('Card','Cash','EFT'))
);

-- Results Table
CREATE TABLE Results (
    result_id INT PRIMARY KEY IDENTITY(1,1),
    runner_id INT FOREIGN KEY REFERENCES Runner(runner_id),
    race_id INT FOREIGN KEY REFERENCES Race(race_id),
    time TIME,
    position INT
);

-- Seed Data
INSERT INTO Organizer (name, contact) VALUES
('RaceDay SA', 'info@raceday.co.za'),
('Speed Events', 'contact@speedevents.com');

INSERT INTO Runner (name, age, email) VALUES
('Mpendulo Ngomane', 22, 'mpendulo@example.com'),
('Thabo Dlamini', 25, 'thabo@example.com');

INSERT INTO Category (name, type) VALUES
('Under 20', 'Age'),
('Senior', 'Age'),
('10km', 'Distance'),
('21km', 'Distance'),
('Marathon', 'Distance');

INSERT INTO Race (name, description, date, location, distance, type, category_id, organizer_id) VALUES
('Spring 10km', 'Community road race', '2026-09-15', 'Johannesburg', 10.00, 'Run', 3, 1),
('Autumn 21km', 'Half marathon event', '2026-10-01', 'Cape Town', 21.00, 'Run', 4, 2),
('City Marathon', 'Full marathon challenge', '2026-11-20', 'Durban', 42.20, 'Run', 5, 1);

INSERT INTO Registration (runner_id, race_id, category_id, status) VALUES
(1, 1, 3, 'Confirmed'),
(2, 2, 4, 'Confirmed');

INSERT INTO Payment (reg_id, amount, method) VALUES
(1, 250.00, 'Card'),
(2, 300.00, 'EFT');

INSERT INTO Results (runner_id, race_id, time, position) VALUES
(1, 1, '00:45:30', 1),
(2, 2, '01:50:10', 2);
