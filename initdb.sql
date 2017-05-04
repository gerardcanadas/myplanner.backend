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

CREATE TABLE IF NOT EXISTS Notifications (
id INT NOT NULL AUTO_INCREMENT,
name TEXT NOT NULL,
message TEXT NULL,
notificationTypeId INT NOT NULL,
cron TEXT NOT NULL,
active TINYINT NOT NULL,
PRIMARY KEY (id),
FOREIGN KEY (notificationTypeId) REFERENCES NotificationType(id)
);

CREATE TABLE IF NOT EXISTS NotificationType (
id INT NOT NULL AUTO_INCREMENT,
name TEXT NOT NULL,
description TEXT NULL,
PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS UsersNotifications (
userId INT NOT NULL,
notificationId INT NOT NULL,
PRIMARY KEY (userId,notificationId),
FOREIGN KEY (userId) REFERENCES Users(id),
FOREIGN KEY (notificationId) REFERENCES Notifications(id)
);
 
--get collection of deviceId of a user by username 
SELECT user.id, user.username,device.id, device.deviceId, device.active FROM UsersDevices udev 
INNER JOIN Users user ON udev.userId = user.id
INNER JOIN Devices device ON udev.deviceId = device.Id
WHERE user.username = @username

--get collection of deviceId of a user by username 
SELECT user.id, user.username,device.id, device.deviceId, device.active FROM UsersDevices udev 
INNER JOIN Users user ON udev.userId = user.id
INNER JOIN Devices device ON udev.deviceId = device.Id
WHERE user.username = @username


--DUMMY DATA
INSERT INTO Users (username) VALUES ('gerard');
INSERT INTO Devices (deviceId, active) VALUES ('pepetest', 0);