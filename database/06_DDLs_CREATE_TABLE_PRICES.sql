-- LockerDB.Prices definition

CREATE TABLE `Prices` (
  `id` int NOT NULL AUTO_INCREMENT,
  `Value` decimal(9,2) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;