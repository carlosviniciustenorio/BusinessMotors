-- Adminer 4.8.1 MySQL 9.0.1 dump

SET NAMES utf8;
SET time_zone = '+00:00';
SET foreign_key_checks = 0;
SET sql_mode = 'NO_AUTO_VALUE_ON_ZERO';

DROP TABLE IF EXISTS `Anuncio`;
CREATE TABLE `Anuncio` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Placa` longtext CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `ModeloId` int NOT NULL,
  `VersaoId` int NOT NULL,
  `Portas` int NOT NULL,
  `Cambio` int NOT NULL,
  `AnoVeiculo` int NOT NULL,
  `AnoFabricacao` int NOT NULL,
  `Cor` int NOT NULL,
  `Km` longtext CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Estado` longtext CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Preco` decimal(18,2) NOT NULL,
  `UserId` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  `DataAtualizacao` datetime(6) DEFAULT CURRENT_TIMESTAMP(6),
  `ExibirTelefone` tinyint(1) NOT NULL,
  `ExibirEmail` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Anuncio_ModeloId` (`ModeloId`),
  KEY `IX_Anuncio_VersaoId` (`VersaoId`),
  CONSTRAINT `FK_Anuncio_Modelo_ModeloId` FOREIGN KEY (`ModeloId`) REFERENCES `Modelo` (`Id`),
  CONSTRAINT `FK_Anuncio_Versao_VersaoId` FOREIGN KEY (`VersaoId`) REFERENCES `Versao` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

INSERT INTO `Anuncio` (`Id`, `Placa`, `ModeloId`, `VersaoId`, `Portas`, `Cambio`, `AnoVeiculo`, `AnoFabricacao`, `Cor`, `Km`, `Estado`, `Preco`, `UserId`, `DataCriacao`, `DataAtualizacao`, `ExibirTelefone`, `ExibirEmail`) VALUES
('08db46b1-62dd-4a49-81f4-43590331e006',	'CCC8I86',	3,	3,	4,	2,	2024,	2024,	3,	'12',	'RJ',	11111.00,	'ad0905e9-8c87-49a1-9b01-46416f6b08b2',	'2024-03-27 00:16:18.534376',	'2024-03-27 00:16:18.534376',	0,	0),
('08db46b1-da0d-400e-8755-41c56a2d16a5',	'CCC8I86',	3,	3,	4,	2,	2023,	2023,	3,	'12',	'RJ',	11111.00,	'ad0905e9-8c87-49a1-9b01-46416f6b08b2',	'2024-03-27 00:16:18.534376',	'2024-03-27 00:16:18.534376',	0,	0),
('08dc8f1f-db40-464b-8478-b73ee8622dcf',	'CCC8f88',	3,	3,	2,	2,	2021,	2021,	3,	'12',	'RJ',	121000.00,	'ad0905e9-8c87-49a1-9b01-46416f6b08b2',	'2024-06-17 22:50:15.637772',	NULL,	0,	0),
('08dc8f21-d0a2-4d6d-87ca-468a7b21c3ac',	'LTO8F54',	3,	3,	4,	0,	2023,	2023,	2,	'48',	'RJ',	45000.00,	'ad0905e9-8c87-49a1-9b01-46416f6b08b2',	'2024-06-17 23:04:17.657808',	NULL,	1,	1),
('08dc8f21-f6cc-411c-84ca-a75c5febbfab',	'LTO8F54',	1,	2,	4,	0,	2023,	2023,	2,	'48',	'RJ',	45000.00,	'ad0905e9-8c87-49a1-9b01-46416f6b08b2',	'2024-06-17 23:05:21.702424',	NULL,	1,	1);

DROP TABLE IF EXISTS `AnuncioCaracteristica`;
CREATE TABLE `AnuncioCaracteristica` (
  `AnuncioId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CaracteristicasId` int NOT NULL,
  PRIMARY KEY (`AnuncioId`,`CaracteristicasId`),
  KEY `IX_AnuncioCaracteristica_CaracteristicasId` (`CaracteristicasId`),
  CONSTRAINT `FK_AnuncioCaracteristica_Anuncio_AnuncioId` FOREIGN KEY (`AnuncioId`) REFERENCES `Anuncio` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AnuncioCaracteristica_Caracteristica_CaracteristicasId` FOREIGN KEY (`CaracteristicasId`) REFERENCES `Caracteristica` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

INSERT INTO `AnuncioCaracteristica` (`AnuncioId`, `CaracteristicasId`) VALUES
('08db46b1-62dd-4a49-81f4-43590331e006',	1),
('08db46b1-da0d-400e-8755-41c56a2d16a5',	1),
('08dc8f1f-db40-464b-8478-b73ee8622dcf',	1),
('08dc8f21-d0a2-4d6d-87ca-468a7b21c3ac',	1),
('08dc8f21-f6cc-411c-84ca-a75c5febbfab',	2);

DROP TABLE IF EXISTS `AnuncioOpcional`;
CREATE TABLE `AnuncioOpcional` (
  `AnuncioId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `OpcionaisId` int NOT NULL,
  PRIMARY KEY (`AnuncioId`,`OpcionaisId`),
  KEY `IX_AnuncioOpcional_OpcionaisId` (`OpcionaisId`),
  CONSTRAINT `FK_AnuncioOpcional_Anuncio_AnuncioId` FOREIGN KEY (`AnuncioId`) REFERENCES `Anuncio` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AnuncioOpcional_Opcional_OpcionaisId` FOREIGN KEY (`OpcionaisId`) REFERENCES `Opcional` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

INSERT INTO `AnuncioOpcional` (`AnuncioId`, `OpcionaisId`) VALUES
('08db46b1-62dd-4a49-81f4-43590331e006',	1),
('08db46b1-da0d-400e-8755-41c56a2d16a5',	1),
('08dc8f1f-db40-464b-8478-b73ee8622dcf',	1),
('08dc8f21-d0a2-4d6d-87ca-468a7b21c3ac',	2),
('08dc8f21-f6cc-411c-84ca-a75c5febbfab',	2);

DROP TABLE IF EXISTS `AnuncioTipoCombustivel`;
CREATE TABLE `AnuncioTipoCombustivel` (
  `AnuncioId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TiposCombustiveisId` int NOT NULL,
  PRIMARY KEY (`AnuncioId`,`TiposCombustiveisId`),
  KEY `IX_AnuncioTipoCombustivel_TiposCombustiveisId` (`TiposCombustiveisId`),
  CONSTRAINT `FK_AnuncioTipoCombustivel_Anuncio_AnuncioId` FOREIGN KEY (`AnuncioId`) REFERENCES `Anuncio` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AnuncioTipoCombustivel_TipoCombustivel_TiposCombustiveisId` FOREIGN KEY (`TiposCombustiveisId`) REFERENCES `TipoCombustivel` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

INSERT INTO `AnuncioTipoCombustivel` (`AnuncioId`, `TiposCombustiveisId`) VALUES
('08dc8f21-d0a2-4d6d-87ca-468a7b21c3ac',	2),
('08dc8f21-f6cc-411c-84ca-a75c5febbfab',	2),
('08db46b1-62dd-4a49-81f4-43590331e006',	4),
('08db46b1-da0d-400e-8755-41c56a2d16a5',	4),
('08dc8f1f-db40-464b-8478-b73ee8622dcf',	4);

DROP TABLE IF EXISTS `AspNetRoleClaims`;
CREATE TABLE `AspNetRoleClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;


DROP TABLE IF EXISTS `AspNetRoles`;
CREATE TABLE `AspNetRoles` (
  `Id` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

INSERT INTO `AspNetRoles` (`Id`, `Name`, `NormalizedName`, `ConcurrencyStamp`) VALUES
('1',	'Admin',	'Admin',	'Admin');

DROP TABLE IF EXISTS `AspNetUserClaims`;
CREATE TABLE `AspNetUserClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

INSERT INTO `AspNetUserClaims` (`Id`, `UserId`, `ClaimType`, `ClaimValue`) VALUES
(1,	'ad0905e9-8c87-49a1-9b01-46416f6b08b2',	'Usuarios',	'Create'),
(2,	'ad0905e9-8c87-49a1-9b01-46416f6b08b2',	'Usuarios',	'Read'),
(3,	'ad0905e9-8c87-49a1-9b01-46416f6b08b2',	'Usuarios',	'Update'),
(4,	'ad0905e9-8c87-49a1-9b01-46416f6b08b2',	'Usuarios',	'Delete');

DROP TABLE IF EXISTS `AspNetUserLogins`;
CREATE TABLE `AspNetUserLogins` (
  `LoginProvider` varchar(128) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `ProviderKey` varchar(128) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `ProviderDisplayName` longtext CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci,
  `UserId` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;


DROP TABLE IF EXISTS `AspNetUserRoles`;
CREATE TABLE `AspNetUserRoles` (
  `UserId` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

INSERT INTO `AspNetUserRoles` (`UserId`, `RoleId`) VALUES
('ad0905e9-8c87-49a1-9b01-46416f6b08b2',	'1');

DROP TABLE IF EXISTS `AspNetUserTokens`;
CREATE TABLE `AspNetUserTokens` (
  `UserId` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `LoginProvider` varchar(128) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Name` varchar(128) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;


DROP TABLE IF EXISTS `AspNetUsers`;
CREATE TABLE `AspNetUsers` (
  `Id` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci,
  `SecurityStamp` longtext CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci,
  `PhoneNumber` longtext CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

INSERT INTO `AspNetUsers` (`Id`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`) VALUES
('ad0905e9-8c87-49a1-9b01-46416f6b08b2',	'carlos.tenorio@gmail.com',	'CARLOS.TENORIO@GMAIL.COM',	'carlos.tenorio@gmail.com',	'CARLOS.TENORIO@GMAIL.COM',	1,	'AQAAAAIAAYagAAAAEF/dNHu2g+srIRAQunPSLpl/2/413g5yTKdPFbyDDgl0MEbpT7o+lQIvYVwVkQ7mfg==',	'7WBMNKNSCOB67ZM73RKFNLGKO5D2GDCL',	'63ff31f1-0552-4942-b84f-bac44cd24c0d',	'21999999999',	0,	0,	NULL,	0,	0);

DROP TABLE IF EXISTS `Caracteristica`;
CREATE TABLE `Caracteristica` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descricao` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

INSERT INTO `Caracteristica` (`Id`, `Descricao`) VALUES
(1,	'Todas as revisões em concessionária'),
(2,	'Blindado'),
(3,	'adaptado pra PCD'),
(4,	'Garantia de Fábrica'),
(5,	'IPVA pago'),
(6,	'Licenciado'),
(7,	'Não aceito troca'),
(8,	'Único Dono'),
(9,	'Veículo de Colecionador'),
(10,	'Veículo financiado');

DROP TABLE IF EXISTS `Imagem`;
CREATE TABLE `Imagem` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UrlS3` longtext CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `AnuncioId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Imagem_AnuncioId` (`AnuncioId`),
  CONSTRAINT `FK_Imagem_Anuncio_AnuncioId` FOREIGN KEY (`AnuncioId`) REFERENCES `Anuncio` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

INSERT INTO `Imagem` (`Id`, `UrlS3`, `AnuncioId`) VALUES
(1,	'https://cdn.appdealersites.com.br/aguia-branca/vm-jeep/2023/06-junho/2192844-c3mu1sd5em.jpg',	'08db46b1-62dd-4a49-81f4-43590331e006'),
(2,	'https://autoentusiastas.com.br/ae/wp-content/uploads/2022/04/AE-Jeep-Renegade-S-T270-4x4-002.jpeg',	'08db46b1-da0d-400e-8755-41c56a2d16a5'),
(3,	'https://www.media.stellantis.com/cache/5/0/e/3/b/50e3bac2628a8662d485560bdf54ef7c61cdac0b.jpeg',	'08dc8f1f-db40-464b-8478-b73ee8622dcf'),
(4,	'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTpW83chZNPcmxogBvijnczsMKOhd-a_s7ofA&usqp=CAU',	'08dc8f21-d0a2-4d6d-87ca-468a7b21c3ac'),
(5,	'https://conteudo.imguol.com.br/c/entretenimento/bf/2017/05/11/novo-fiat-palio-1494541137037_v2_4x3.png',	'08dc8f21-f6cc-411c-84ca-a75c5febbfab'),
(6,	'https://lh4.googleusercontent.com/proxy/L0KBbBxm3y73g03mg3-ZtOgHL218ZNyuclM3CgUHpw15mycyAEe3XWtuSIipSR0Fl1o3oVWi2VXGNdrjq2ALqTZay3G-bYTUsdFVvXNlHJaluI_6NG1h8JIG4OqeLJsULbcn2Vi0tYgPiIp5bjOC',	'08dc8f21-f6cc-411c-84ca-a75c5febbfab');

DROP TABLE IF EXISTS `Marca`;
CREATE TABLE `Marca` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descricao` longtext CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

INSERT INTO `Marca` (`Id`, `Descricao`) VALUES
(1,	'Jeep'),
(2,	'Chevrolet'),
(3,	'Fiat'),
(4,	'Volkswagem'),
(5,	'Kia'),
(6,	'Hyundai'),
(7,	'Honda'),
(8,	'Toyota'),
(9,	'Cherry'),
(10,	'Renault'),
(11,	'Volvo'),
(12,	'Jaguar');

DROP TABLE IF EXISTS `Modelo`;
CREATE TABLE `Modelo` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descricao` longtext CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `AnoModelo` int NOT NULL,
  `AnoFabricacao` int NOT NULL,
  `MarcaId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Modelo_MarcaId` (`MarcaId`),
  CONSTRAINT `FK_Modelo_Marca_MarcaId` FOREIGN KEY (`MarcaId`) REFERENCES `Marca` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

INSERT INTO `Modelo` (`Id`, `Descricao`, `AnoModelo`, `AnoFabricacao`, `MarcaId`) VALUES
(1,	'Renegade',	2018,	2017,	1),
(2,	'Compass',	2020,	2019,	1),
(3,	'Wrangler',	2021,	2020,	1),
(4,	'Cherokee',	2019,	2018,	1),
(5,	'Cruze',	2019,	2018,	2),
(6,	'Onix',	2021,	2020,	2),
(7,	'Tracker',	2020,	2019,	2),
(8,	'S10',	2021,	2020,	2),
(9,	'Camaro',	2021,	2020,	2),
(10,	'Palio',	2015,	2014,	3),
(11,	'Uno',	2020,	2019,	3),
(12,	'Toro',	2021,	2020,	3),
(13,	'Strada',	2021,	2020,	3),
(14,	'Argo',	2021,	2020,	3),
(15,	'Gol',	2020,	2019,	4),
(16,	'Polo',	2021,	2020,	4),
(17,	'Virtus',	2021,	2020,	4),
(18,	'T-Cross',	2021,	2020,	4),
(19,	'Tiguan',	2021,	2020,	4),
(20,	'Sportage',	2020,	2019,	5),
(21,	'Cerato',	2021,	2020,	5),
(22,	'Sorento',	2021,	2020,	5),
(23,	'Seltos',	2021,	2020,	5),
(24,	'Soul',	2020,	2019,	5),
(25,	'Tucson',	2021,	2020,	6),
(26,	'Creta',	2021,	2020,	6),
(27,	'HB20',	2021,	2020,	6),
(28,	'Santa Fe',	2020,	2019,	6),
(29,	'Elantra',	2021,	2020,	6),
(30,	'Civic',	2019,	2018,	7),
(31,	'Fit',	2020,	2019,	7),
(32,	'HR-V',	2021,	2020,	7),
(33,	'CR-V',	2021,	2020,	7),
(34,	'City',	2021,	2020,	7),
(35,	'Corolla',	2021,	2020,	8),
(36,	'Hilux',	2021,	2020,	8),
(37,	'Yaris',	2021,	2020,	8),
(38,	'RAV4',	2021,	2020,	8),
(39,	'Etios',	2020,	2019,	8),
(40,	'Tiggo',	2021,	2020,	9),
(41,	'Arrizo',	2021,	2020,	9),
(42,	'QQ',	2020,	2019,	9),
(43,	'Tiggo 8',	2021,	2020,	9),
(44,	'Kwid',	2020,	2019,	10),
(45,	'Duster',	2021,	2020,	10),
(46,	'Sandero',	2021,	2020,	10),
(47,	'Logan',	2021,	2020,	10),
(48,	'Captur',	2021,	2020,	10),
(49,	'XC60',	2020,	2019,	11),
(50,	'XC90',	2021,	2020,	11),
(51,	'S60',	2021,	2020,	11),
(52,	'V60',	2021,	2020,	11),
(53,	'F-Pace',	2019,	2018,	12),
(54,	'XE',	2021,	2020,	12),
(55,	'XF',	2021,	2020,	12),
(56,	'E-Pace',	2021,	2020,	12);

DROP TABLE IF EXISTS `Opcional`;
CREATE TABLE `Opcional` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descricao` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

INSERT INTO `Opcional` (`Id`, `Descricao`) VALUES
(1,	'bancos dianteiros com aquecimento'),
(2,	'Air bag'),
(3,	'alarme'),
(4,	'ar condicionado'),
(5,	'ar quente'),
(6,	'banco motorista com ajuste de altura'),
(7,	'bancos em couro'),
(8,	'controle de tração'),
(9,	'DVD Player'),
(10,	'Desembaçador traseiro');

DROP TABLE IF EXISTS `TipoCombustivel`;
CREATE TABLE `TipoCombustivel` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descricao` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

INSERT INTO `TipoCombustivel` (`Id`, `Descricao`) VALUES
(1,	'Benzina'),
(2,	'Gasolina'),
(3,	'Álcool'),
(4,	'Elétrico'),
(5,	'GNV');

DROP TABLE IF EXISTS `Versao`;
CREATE TABLE `Versao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descricao` longtext CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `ModeloId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Versao_ModeloId` (`ModeloId`),
  CONSTRAINT `FK_Versao_Modelo_ModeloId` FOREIGN KEY (`ModeloId`) REFERENCES `Modelo` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

INSERT INTO `Versao` (`Id`, `Descricao`, `ModeloId`) VALUES
(1,	'1.0 lxs 8v',	1),
(2,	'1.4 lxs 16v',	1),
(3,	'1.8 lxs 16v',	3);

-- 2024-08-12 22:27:58