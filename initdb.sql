CREATE DATABASE devices;

USE devices;

CREATE TABLE IF NOT EXISTS Users (
id INT NOT NULL AUTO_INCREMENT,
username VARCHAR(50) NOT NULL,
PRIMARY KEY (id, username)
);

CREATE TABLE IF NOT EXISTS Devices (
id INT NOT NULL AUTO_INCREMENT,
deviceId TEXT NOT NULL,
active TINYINT NOT NULL,
PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS UsersDevices (
userId INT NOT NULL,
deviceId INT NOT NULL,
PRIMARY KEY (userId,deviceId),
FOREIGN KEY (userId) REFERENCES Users(id),
FOREIGN KEY (deviceId) REFERENCES Devices(id)
);
 
 
SELECT 'user.id', 'user.username','device.id', 'device.deviceId', 'device.active' FROM UsersDevices udev 
LEFT JOIN Users user ON udev.userId = user.id
LEFT JOIN Devices device ON udev.deviceId = device.Id
WHERE user.username = @username



INSERT INTO Users (username) VALUES ('gerard');
INSERT INTO Devices (deviceId, active) VALUES ('pepetest', 0);
