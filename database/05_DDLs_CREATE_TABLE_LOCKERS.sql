-- LockerDB.Lockers definition

CREATE TABLE `Lockers` (
  `id` int NOT NULL AUTO_INCREMENT,
  `serial_number` int NOT NULL,
  `id_location` int NOT NULL,
  `id_price` int NOT NULL,
  `rented` bigint NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `Lockers_UNIQUE` (`serial_number`,`id_location`)
) ENGINE=InnoDB AUTO_INCREMENT=64 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;