-- LockerDB.Locations definition

CREATE TABLE `Locations` (
  `id` int NOT NULL AUTO_INCREMENT,
  `address` varchar(50) NOT NULL,
  `postal_code` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;