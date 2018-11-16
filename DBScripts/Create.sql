CREATE SCHEMA `dfc` ;


CREATE TABLE `dfc`.`UserDetails` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `EMail` VARCHAR(100) NULL,
  `Mobile` VARCHAR(20) NULL,
  `AlternateMobile` VARCHAR(20) NULL,
  `HomePhone` VARCHAR(20) NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC) );
  
CREATE TABLE `dfc`.`Passwords` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `PasswordHash` VARCHAR(255) NULL,
  `PasswordSalt` VARCHAR(255) NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC) );

CREATE TABLE `dfc`.`Users` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(50) DEFAULT NULL,
  `FirstName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `Active` tinyint NOT NULL DEFAULT '1',
  `DetailID` int(11) NOT NULL,
  `PasswordID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`),
  UNIQUE KEY `UserName_UNIQUE` (`UserName`),
  KEY `DetailID_idx` (`DetailID`),
  KEY `PasswordID_idx` (`PasswordID`),
  CONSTRAINT `DetailID` FOREIGN KEY (`DetailID`) REFERENCES `userdetails` (`id`) ON DELETE CASCADE,
  CONSTRAINT `PasswordID` FOREIGN KEY (`PasswordID`) REFERENCES `passwords` (`id`) ON DELETE CASCADE
) ;

CREATE TABLE `dfc`.`ScreenGroups` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(100) NOT NULL,
  `Description` VARCHAR(100) NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC)
);
	
CREATE TABLE `dfc`.`Screens` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(100) NOT NULL,
  `Description` VARCHAR(100) NULL,
  `ScreenGroupID` INT NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC),
  CONSTRAINT `Screens_ScreenGroupID` FOREIGN KEY (`ScreenGroupID`) REFERENCES `ScreenGroups` (`id`) ON DELETE CASCADE
);

CREATE TABLE `dfc`.`ScreenElements` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `ControlID` VARCHAR(50) NOT NULL,
  `Name` VARCHAR(50) NOT NULL,
  `Description` VARCHAR(100) NULL,
  `ScreenID` INT NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC) VISIBLE,
  CONSTRAINT `ScreenElements_ScreenID` FOREIGN KEY (`ScreenID`) REFERENCES `Screens` (`id`) ON DELETE CASCADE
);
	
CREATE TABLE `dfc`.`Roles` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(100) NOT NULL,
  `Description` VARCHAR(255) NULL,
  `Active` tinyint NOT NULL DEFAULT '1',
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC)
);

CREATE TABLE `dfc`.`RolePermissions` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `RoleID` INT NOT NULL,
  `ScreenGroupID` INT NOT NULL,
  `ScreenID` INT NOT NULL,
  `ScreenElementID` INT NULL,
  `Allowed` tinyint NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC),
  CONSTRAINT `RolePermissions_RoleID` FOREIGN KEY (`RoleID`) REFERENCES `Roles` (`id`), 
  CONSTRAINT `RolePermissions_ScreenGroupID` FOREIGN KEY (`ScreenGroupID`) REFERENCES `ScreenGroups` (`id`),
  CONSTRAINT `RolePermissions_ScreenID` FOREIGN KEY (`ScreenID`) REFERENCES `Screens` (`id`), 
  CONSTRAINT `RolePermissions_ScreenElementID` FOREIGN KEY (`ScreenElementID`) REFERENCES `ScreenElements` (`id`) 
);
	
CREATE TABLE `dfc`.`UserRoles` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `UserID` INT NOT NULL,
  `RoleID` INT NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC),
  CONSTRAINT `UserRoles_RoleID` FOREIGN KEY (`RoleID`) REFERENCES `Roles` (`id`), 
  CONSTRAINT `UserRoles_UserID` FOREIGN KEY (`UserID`) REFERENCES `Users` (`id`) 
);
	
	
	
CREATE TABLE `dfc`.`Countries` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(100) NULL,
  `CreatedByUserID` INT NULL,
  `CreatedDateTime` DATETIME NULL,
  `UpdatedByUserID` INT NULL,
  `UpdatedDateTime` DATETIME NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC),
  CONSTRAINT `Countries_CreatedByUserID` FOREIGN KEY (`CreatedByUserID`) REFERENCES `Users` (`id`),
  CONSTRAINT `Countries_UpdatedByUserID` FOREIGN KEY (`UpdatedByUserID`) REFERENCES `Users` (`id`)
);

CREATE TABLE `dfc`.`States` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(100) NULL,
  `CountryID` INT NOT NULL,
  `CreatedByUserID` INT NULL,
  `CreatedDateTime` DATETIME NULL,
  `UpdatedByUserID` INT NULL,
  `UpdatedDateTime` DATETIME NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC),
  CONSTRAINT `States_CountryID` FOREIGN KEY (`CountryID`) REFERENCES `Countries` (`id`),
  CONSTRAINT `States_CreatedByUserID` FOREIGN KEY (`CreatedByUserID`) REFERENCES `Users` (`id`),
  CONSTRAINT `States_UpdatedByUserID` FOREIGN KEY (`UpdatedByUserID`) REFERENCES `Users` (`id`)
);

CREATE TABLE `dfc`.`Districts` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(100) NULL,
  `StateID` INT NOT NULL,
  `CreatedByUserID` INT NULL,
  `CreatedDateTime` DATETIME NULL,
  `UpdatedByUserID` INT NULL,
  `UpdatedDateTime` DATETIME NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC),
  CONSTRAINT `Districts_StateID` FOREIGN KEY (`StateID`) REFERENCES `States` (`id`),
  CONSTRAINT `Districts_CreatedByUserID` FOREIGN KEY (`CreatedByUserID`) REFERENCES `Users` (`id`),
  CONSTRAINT `Districts_UpdatedByUserID` FOREIGN KEY (`UpdatedByUserID`) REFERENCES `Users` (`id`)
);


CREATE TABLE `dfc`.`Cities` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(100) NULL,
  `CityType` VARCHAR(10) NULL,
  `Active` tinyint NOT NULL DEFAULT '1',
  `DistrictID` INT NOT NULL,
  `CreatedByUserID` INT NULL,
  `CreatedDateTime` DATETIME NULL,
  `UpdatedByUserID` INT NULL,
  `UpdatedDateTime` DATETIME NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC),
  CONSTRAINT `Cities_DistrictID` FOREIGN KEY (`DistrictID`) REFERENCES `Districts` (`id`),
  CONSTRAINT `Cities_CreatedByUserID` FOREIGN KEY (`CreatedByUserID`) REFERENCES `Users` (`id`),
  CONSTRAINT `Cities_UpdatedByUserID` FOREIGN KEY (`UpdatedByUserID`) REFERENCES `Users` (`id`)
);


INSERT INTO `dfc`.`UserDetails` (`EMail`, `Mobile`) VALUES ('admin@dfc.com', '1234567890');

INSERT INTO `dfc`.`Passwords` (`PasswordHash`, `PasswordSalt`) VALUES ('RaSQ1sthyXMMsVB4w6KamTYx/MHnG7vcAOOaZvhWhUe9J1PymDjtaorD+0c3m6z9sGDWgh82rIgykE2c9G9uvg==', '2/9a6IxXi1BForaZSpc2kDfKEzXy2Em74noXFJhnlr41gfa4G4456oNMYZaPrhawtxTQkEydutMc9vo1CDxpRm5IgogrgDY/Hl8KnX4FsKRsmOqT1jmwVaPayJSBGMaPGfAGiltID8mEJdTCevxPAhJP5uinratp82O1XeDMmIo=');
  
INSERT INTO `dfc`.`Users` (`UserName`, `FirstName`, `LastName`, `Active`, `DetailID`, `PasswordID`) VALUES ('admin', 'Administrator', 'Admin', '1', 1, 1);
  
INSERT INTO `dfc`.`Countries` (`Name`, `CreatedByUserID`, `CreatedDateTime`) VALUES ('India', '1', '2018-09-18');
INSERT INTO `dfc`.`countries` (`Name`, `CreatedByUserID`, `CreatedDateTime`) VALUES ('Sri Lanka', '1', '2018-09-18 00:00:00');
INSERT INTO `dfc`.`countries` (`Name`, `CreatedByUserID`, `CreatedDateTime`) VALUES ('Pakistan', '1', '2018-09-18 00:00:00');
INSERT INTO `dfc`.`countries` (`Name`, `CreatedByUserID`, `CreatedDateTime`) VALUES ('USA', '1', '2018-09-18 00:00:00');
INSERT INTO `dfc`.`countries` (`Name`, `CreatedByUserID`, `CreatedDateTime`) VALUES ('England', '1', '2018-09-18 00:00:00');
INSERT INTO `dfc`.`countries` (`Name`, `CreatedByUserID`, `CreatedDateTime`) VALUES ('Bangladesh', '1', '2018-09-18 00:00:00');
INSERT INTO `dfc`.`countries` (`Name`, `CreatedByUserID`, `CreatedDateTime`) VALUES ('Nepal', '1', '2018-09-18 00:00:00');
INSERT INTO `dfc`.`countries` (`Name`, `CreatedByUserID`, `CreatedDateTime`) VALUES ('Bhutan', '1', '2018-09-18 00:00:00');
INSERT INTO `dfc`.`countries` (`Name`, `CreatedByUserID`, `CreatedDateTime`) VALUES ('China', '1', '2018-09-18 00:00:00');
INSERT INTO `dfc`.`countries` (`Name`, `CreatedByUserID`, `CreatedDateTime`) VALUES ('Myanmar', '1', '2018-09-18 00:00:00');
INSERT INTO `dfc`.`countries` (`Name`, `CreatedByUserID`, `CreatedDateTime`) VALUES ('Malaysia', '1', '2018-09-18 00:00:00');
INSERT INTO `dfc`.`countries` (`Name`, `CreatedByUserID`, `CreatedDateTime`) VALUES ('Singapore', '1', '2018-09-18 00:00:00');

INSERT INTO `dfc`.`states` (`Name`, `CountryID`) VALUES ('Tamil Nadu', '1');
INSERT INTO `dfc`.`states` (`Name`, `CountryID`) VALUES ('Kerala', '1');
INSERT INTO `dfc`.`states` (`Name`, `CountryID`) VALUES ('Karnataka', '1');
INSERT INTO `dfc`.`states` (`Name`, `CountryID`) VALUES ('Andra Pradesh', '1');
INSERT INTO `dfc`.`states` (`Name`, `CountryID`) VALUES ('Telangana', '1');
INSERT INTO `dfc`.`ScreenGroups` (`Name`, `Description`) VALUES ('MasterData', 'Manage Master Data');

INSERT INTO `dfc`.`screens` (`Name`, `Description`, `ScreenGroupID`) VALUES ('Projects', 'Project List/Details','1');
INSERT INTO `dfc`.`screens` (`Name`, `Description`, `ScreenGroupID`) VALUES ('Agents', 'Agent List/Details','1');

INSERT INTO `dfc`.`screenelements` (`Name`, `Description`, `ScreenID`, `ControlID`) VALUES ('Save', 'Add New Record', '1', 'save');
INSERT INTO `dfc`.`screenelements` (`Name`, `Description`, `ScreenID`, `ControlID`) VALUES ('Update', 'Edit Record', '1', 'update');
INSERT INTO `dfc`.`screenelements` (`Name`, `Description`, `ScreenID`, `ControlID`) VALUES ('Delete', 'Delete single record', '1', 'delete');
INSERT INTO `dfc`.`screenelements` (`Name`, `Description`, `ScreenID`, `ControlID`) VALUES ('Delete Selected', 'Delete multipe records', '1', 'deleteMultiple');

INSERT INTO `dfc`.`screenelements` (`Name`, `Description`, `ScreenID`, `ControlID`) VALUES ('Save', 'Add New Record', '2', 'save');
INSERT INTO `dfc`.`screenelements` (`Name`, `Description`, `ScreenID`, `ControlID`) VALUES ('Update', 'Edit Record', '2', 'update');
INSERT INTO `dfc`.`screenelements` (`Name`, `Description`, `ScreenID`, `ControlID`) VALUES ('Delete', 'Delete single record', '2', 'delete');
INSERT INTO `dfc`.`screenelements` (`Name`, `Description`, `ScreenID`, `ControlID`) VALUES ('Delete Selected', 'Delete multipe records', '2', 'deleteMultiple');

INSERT INTO `dfc`.`roles` (`Name`, `Description`, `Active`) VALUES ('Administrator', 'Administrate the site', '1');

INSERT INTO `dfc`.`rolepermissions` (`RoleID`, `ScreenGroupID`, `ScreenID`, `ScreenElementID`, `Allowed`) VALUES ('1', '1', '1', '1', '1');
INSERT INTO `dfc`.`rolepermissions` (`RoleID`, `ScreenGroupID`, `ScreenID`, `ScreenElementID`, `Allowed`) VALUES ('1', '1', '1', '2', '1');
INSERT INTO `dfc`.`rolepermissions` (`RoleID`, `ScreenGroupID`, `ScreenID`, `ScreenElementID`, `Allowed`) VALUES ('1', '1', '1', '3', '0');
INSERT INTO `dfc`.`rolepermissions` (`RoleID`, `ScreenGroupID`, `ScreenID`, `ScreenElementID`, `Allowed`) VALUES ('1', '1', '1', '4', '0');
INSERT INTO `dfc`.`rolepermissions` (`RoleID`, `ScreenGroupID`, `ScreenID`, `ScreenElementID`, `Allowed`) VALUES ('1', '1', '2', '1', '1');
INSERT INTO `dfc`.`rolepermissions` (`RoleID`, `ScreenGroupID`, `ScreenID`, `ScreenElementID`, `Allowed`) VALUES ('1', '1', '2', '2', '1');
INSERT INTO `dfc`.`rolepermissions` (`RoleID`, `ScreenGroupID`, `ScreenID`, `ScreenElementID`, `Allowed`) VALUES ('1', '1', '2', '3', '1');
INSERT INTO `dfc`.`rolepermissions` (`RoleID`, `ScreenGroupID`, `ScreenID`, `ScreenElementID`, `Allowed`) VALUES ('1', '1', '2', '4', '1');
