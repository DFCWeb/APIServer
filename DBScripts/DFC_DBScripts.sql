CREATE SCHEMA `dfc` ;


CREATE TABLE `dfc`.`UserDetails` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `EMail` VARCHAR(100) NULL,
  `Mobile` VARCHAR(20) NULL,
  `AlternateMobile` VARCHAR(20) NULL,
  `HomePhone` VARCHAR(20) NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC) 
);
  
CREATE TABLE `dfc`.`Passwords` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `PasswordHash` VARCHAR(255) NULL,
  `PasswordSalt` VARCHAR(255) NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC) 
);

CREATE TABLE `dfc`.`Users` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(50) DEFAULT NULL,
  `FirstName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `Active` tinyint NOT NULL DEFAULT '1',
  `UserDetailID` int(11) NOT NULL,
  `PasswordID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`),
  UNIQUE KEY `UserName_UNIQUE` (`UserName`),
  KEY `DetailID_idx` (`DetailID`),
  KEY `PasswordID_idx` (`PasswordID`)  
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
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC)  
);

CREATE TABLE `dfc`.`ScreenElements` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `ControlID` VARCHAR(50) NOT NULL,
  `Name` VARCHAR(50) NOT NULL,
  `Description` VARCHAR(100) NULL,
  `ScreenID` INT NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC) VISIBLE  
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
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC)  
);
	
CREATE TABLE `dfc`.`UserRoles` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `UserID` INT NOT NULL,
  `RoleID` INT NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC)  
);
		
CREATE TABLE `dfc`.`Countries` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(100) NULL,
  `CreatedByUserID` INT NULL,
  `CreatedDateTime` DATETIME NULL,
  `UpdatedByUserID` INT NULL,
  `UpdatedDateTime` DATETIME NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC)  
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
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC)  
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
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC)    
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
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC)
);


CREATE TABLE `dfc`.`Currency` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`Name` varchar(50) NOT NULL,
	`CountryID` INT NOT NULL,
	`CreatedByUserID` INT NULL,
	`CreatedDateTime` DATETIME NULL,
	`UpdatedByUserID` INT NULL,
	`UpdatedDateTime` DATETIME NULL,
	PRIMARY KEY (`ID`)
	UNIQUE INDEX `ID_UNIQUE` (`ID` ASC)
);

CREATE TABLE `dfc`.`ProjectInfo` (
	`ID` INT NOT NULL AUTO_INCREMENT,
	`Name` varchar NOT NULL,
	`Description` varchar NOT NULL,
	`StartDate` DATE NOT NULL,
	`EndDate` DATE NOT NULL,
	`TotalArea` FLOAT NOT NULL,
	`NoofUnits` INT NOT NULL,
	`BasePrice` DECIMAL,
	`CurrentPrice` DECIMAL,
	`BuyLimit` FLOAT,
	`SellLimit` FLOAT,
	`CurrencyId` INT NOT NULL,
	`ApprovalStatus` varchar NOT NULL,
	`AvailableDiscount` varchar NOT NULL,
	`IsAdminApproved` BOOLEAN,
	`IsAgentVisible` BOOLEAN,
	`IsVendorVisible` BOOLEAN,
	`IsCustomerVisible` BOOLEAN,
	`IsPrivateVisible` BOOLEAN,
	`CreatedByUserID` INT NULL,
	`CreatedDateTime` DATETIME NULL,
	`UpdatedByUserID` INT NULL,
	`UpdatedDateTime` DATETIME NULL,
	PRIMARY KEY (`ProjectId`)
);

CREATE TABLE `dfc`.`DiscountInfo` (
	`DiscountId` INT NOT NULL AUTO_INCREMENT,
	`DiscountName` varchar NOT NULL,
	`StartDate` DATETIME NOT NULL,
	`EndDate` DATETIME NOT NULL,
	`DiscountPercenatge` INT NOT NULL,
	`MinPurchaseAmount` DECIMAL NOT NULL,
	`MaxDiscountAmount` DECIMAL NOT NULL,
	`Status` varchar NOT NULL,
	`CreatedByUserID` INT NULL,
    `CreatedDateTime` DATETIME NULL,
    `UpdatedByUserID` INT NULL,
    `UpdatedDateTime` DATETIME NULL,
	PRIMARY KEY (`DiscountId`)
);

CREATE TABLE `dfc`.`ProjectDocuments` (
	`DocumentId` INT NOT NULL AUTO_INCREMENT,
	`ProjectId` INT NOT NULL,
	`DocumentName` varchar NOT NULL,
	`DocumentType` varchar NOT NULL,
	`DocumentDescription` varchar NOT NULL,
	`DocumentPath` varchar NOT NULL,
	`CreatedByUserID` INT NULL,
    `CreatedDateTime` DATETIME NULL,
    `UpdatedByUserID` INT NULL,
    `UpdatedDateTime` DATETIME NULL,
	PRIMARY KEY (`DocumentId`)
);

CREATE TABLE `dfc`.`CustomerInfo` (
	`CustomerId` INT NOT NULL AUTO_INCREMENT,
	`FirstName` varchar NOT NULL,
	`MiddleName` varchar,
	`LastName` varchar NOT NULL,
	`Username` varchar NOT NULL UNIQUE,
	`Password` varchar NOT NULL,
	`EmailAddress` varchar NOT NULL,
	`ContactNo` varchar NOT NULL,
	`FaxNo` varchar,
	`Address1` varchar,
	`Address2` varchar,
	`District` varchar,
	`City` varchar,
	`State` varchar,
	`Country` varchar,
	`Zipcode` varchar,
	`WalletBalance` DECIMAL,
	`CurrencyId` INT NOT NULL,
	`VendorId` INT NOT NULL,
	`AgentId` INT NOT NULL,
	`AvailableDiscount` varchar NOT NULL,
	`CreatedByUserID` INT NULL,
    `CreatedDateTime` DATETIME NULL,
    `UpdatedByUserID` INT NULL,
    `UpdatedDateTime` DATETIME NULL,
	PRIMARY KEY (`CustomerId`)
);

CREATE TABLE `dfc`.`CustomerDocuments` (
	`DocumentId` INT NOT NULL AUTO_INCREMENT,
	`CusomerId` INT NOT NULL,
	`DocumentName` varchar NOT NULL,
	`DocumentType` varchar NOT NULL,
	`DocumentDescription` varchar NOT NULL,
	`DocumentPath` varchar NOT NULL,
	`CreatedByUserID` INT NULL,
    `CreatedDateTime` DATETIME NULL,
    `UpdatedByUserID` INT NULL,
    `UpdatedDateTime` DATETIME NULL,
	PRIMARY KEY (`DocumentId`)
);

CREATE TABLE `dfc`.`CustomerWalletHistory` (
	`TransactionId` INT NOT NULL AUTO_INCREMENT,
	`CustomerId` INT NOT NULL,
	`TransactionDate` DATETIME NOT NULL,
	`TransactionAmount` DECIMAL NOT NULL,
	`TransactionStatus` varchar NOT NULL,
	`CreatedByUserID` INT NULL,
    `CreatedDateTime` DATETIME NULL,
    `UpdatedByUserID` INT NULL,
    `UpdatedDateTime` DATETIME NULL,
	PRIMARY KEY (`TransactionId`)
);

CREATE TABLE `dfc`.`CustomerLinkProject` (
	`CustomerProjectId` INT NOT NULL AUTO_INCREMENT,
	`CustomerId` INT NOT NULL,
	`ProjectId` INT NOT NULL,
	`LinkedDate` DATE NOT NULL,
	`AllocationUnit` INT NOT NULL,
	`CreatedByUserID` INT NULL,
    `CreatedDateTime` DATETIME NULL,
    `UpdatedByUserID` INT NULL,
    `UpdatedDateTime` DATETIME NULL,
	PRIMARY KEY (`CustomerProjectId`)
);

CREATE TABLE `dfc`.`TradeInfo` (
	`TradeId` INT NOT NULL AUTO_INCREMENT,
	`CustomerProjectId` INT NOT NULL,
	`TradeType` varchar NOT NULL,
	`Quantity` INT NOT NULL,
	`TradePrice` DECIMAL NOT NULL,
	`TradeDate` DATETIME NOT NULL,
	`Status` varchar NOT NULL,
	`TokenNo` INT NOT NULL,
	`ExpiryDate` DATETIME NOT NULL,
	`CreatedDateTime` DATETIME NULL,
	`UpdatedDateTime` DATETIME NULL,
	PRIMARY KEY (`TradeId`)
);

CREATE TABLE `dfc`.`OrderStatus` (
	`OrderId` INT NOT NULL AUTO_INCREMENT,
	`TradeId` INT NOT NULL,
	`TradedQuantity` INT NOT NULL,
	`TradedDateTime` DATETIME NOT NULL,
	`TradedPrice` DECIMAL NOT NULL,
	`Status` varchar NOT NULL,
	PRIMARY KEY (`OrderId`)
);

CREATE TABLE `dfc`.`OrderHistory` (
	`OrderId` INT NOT NULL AUTO_INCREMENT,
	`TradeId` INT NOT NULL,
	`TradedQuantity` INT NOT NULL,
	`TradedDateTime` DATETIME NOT NULL,
	`TradedPrice` DECIMAL NOT NULL,
	`Status` varchar NOT NULL,
	`CreatedDate` DATETIME NOT NULL,
	PRIMARY KEY (`OrderId`)
);

ALTER TABLE `dfc`.`Users` ADD CONSTRAINT `Users_UserDetailID` FOREIGN KEY (`UserDetailID`) REFERENCES `UserDetails` (`id`) ON DELETE CASCADE;

ALTER TABLE `dfc`.`Users` ADD CONSTRAINT `Users_PasswordID` FOREIGN KEY (`PasswordID`) REFERENCES `Passwords` (`id`) ON DELETE CASCADE;

ALTER TABLE `dfc`.`Screens` ADD CONSTRAINT `Screens_ScreenGroupID` FOREIGN KEY (`ScreenGroupID`) REFERENCES `ScreenGroups` (`id`) ON DELETE CASCADE;

ALTER TABLE `dfc`.`ScreenElements` ADD CONSTRAINT `ScreenElements_ScreenID` FOREIGN KEY (`ScreenID`) REFERENCES `Screens` (`id`) ON DELETE CASCADE;

ALTER TABLE `dfc`.`RolePermissions` ADD CONSTRAINT `RolePermissions_RoleID` FOREIGN KEY (`RoleID`) REFERENCES `Roles` (`id`);

ALTER TABLE `dfc`.`RolePermissions` ADD CONSTRAINT `RolePermissions_ScreenGroupID` FOREIGN KEY (`ScreenGroupID`) REFERENCES `ScreenGroups` (`id`);

ALTER TABLE `dfc`.`RolePermissions` ADD CONSTRAINT `RolePermissions_ScreenID` FOREIGN KEY (`ScreenID`) REFERENCES `Screens` (`id`);

ALTER TABLE `dfc`.`RolePermissions` ADD CONSTRAINT `RolePermissions_ScreenElementID` FOREIGN KEY (`ScreenElementID`) REFERENCES `ScreenElements` (`id`);

ALTER TABLE `dfc`.`UserRoles` ADD CONSTRAINT `UserRoles_RoleID` FOREIGN KEY (`RoleID`) REFERENCES `Roles` (`id`);

ALTER TABLE `dfc`.`UserRoles` ADD CONSTRAINT `UserRoles_UserID` FOREIGN KEY (`UserID`) REFERENCES `Users` (`id`);

ALTER TABLE `dfc`.`Countries` ADD CONSTRAINT `Countries_CreatedByUserID` FOREIGN KEY (`CreatedByUserID`) REFERENCES `Users` (`id`);

ALTER TABLE `dfc`.`Countries` ADD CONSTRAINT `Countries_UpdatedByUserID` FOREIGN KEY (`UpdatedByUserID`) REFERENCES `Users` (`id`);

ALTER TABLE `dfc`.`States` ADD CONSTRAINT `States_CountryID` FOREIGN KEY (`CountryID`) REFERENCES `Countries` (`id`);

ALTER TABLE `dfc`.`States` ADD CONSTRAINT `States_CreatedByUserID` FOREIGN KEY (`CreatedByUserID`) REFERENCES `Users` (`id`);

ALTER TABLE `dfc`.`States` ADD CONSTRAINT `States_UpdatedByUserID` FOREIGN KEY (`UpdatedByUserID`) REFERENCES `Users` (`id`);

ALTER TABLE `dfc`.`Districts` ADD CONSTRAINT `Districts_StateID` FOREIGN KEY (`StateID`) REFERENCES `States` (`id`);

ALTER TABLE `dfc`.`Districts` ADD CONSTRAINT `Districts_CreatedByUserID` FOREIGN KEY (`CreatedByUserID`) REFERENCES `Users` (`id`);

ALTER TABLE `dfc`.`Districts` ADD CONSTRAINT `Districts_UpdatedByUserID` FOREIGN KEY (`UpdatedByUserID`) REFERENCES `Users` (`id`);

ALTER TABLE `dfc`.`Cities` ADD CONSTRAINT `Cities_DistrictID` FOREIGN KEY (`DistrictID`) REFERENCES `Districts` (`id`);

ALTER TABLE `dfc`.`Cities` ADD CONSTRAINT `Cities_CreatedByUserID` FOREIGN KEY (`CreatedByUserID`) REFERENCES `Users` (`id`);

ALTER TABLE `dfc`.`Cities` ADD CONSTRAINT `Cities_UpdatedByUserID` FOREIGN KEY (`UpdatedByUserID`) REFERENCES `Users` (`id`);

ALTER TABLE `dfc`.`ProjectInfo` ADD CONSTRAINT `ProjectInfo_CurrencyId` FOREIGN KEY (`CurrencyId`) REFERENCES `Currency`(`id`);

ALTER TABLE `dfc`.`ProjectInfo` ADD CONSTRAINT `ProjectInfo_DiscountId` FOREIGN KEY (`AvailableDiscount`) REFERENCES `DiscountInfo`(`id`);

ALTER TABLE `dfc`.`ProjectDocuments` ADD CONSTRAINT `ProjectDocuments_fk0` FOREIGN KEY (`ProjectId`) REFERENCES `ProjectInfo`(`id`);

ALTER TABLE `dfc`.`CustomerInfo` ADD CONSTRAINT `CustomerInfo_fk0` FOREIGN KEY (`CurrencyId`) REFERENCES `Currency`(`id`);

ALTER TABLE `dfc`.`CustomerInfo` ADD CONSTRAINT `CustomerInfo_fk1` FOREIGN KEY (`AvailableDiscount`) REFERENCES `DiscountInfo`(`id`);

ALTER TABLE `dfc`.`CustomerDocuments` ADD CONSTRAINT `CustomerDocuments_fk0` FOREIGN KEY (`CusomerId`) REFERENCES `CustomerInfo`(`id`);

ALTER TABLE `dfc`.`CustomerWalletHistory` ADD CONSTRAINT `CustomerWalletHistory_fk0` FOREIGN KEY (`CustomerId`) REFERENCES `CustomerInfo`(`id`);

ALTER TABLE `dfc`.`CustomerLinkProject` ADD CONSTRAINT `CustomerLinkProject_fk0` FOREIGN KEY (`CustomerId`) REFERENCES `CustomerInfo`(`id`);

ALTER TABLE `dfc`.`CustomerLinkProject` ADD CONSTRAINT `CustomerLinkProject_fk1` FOREIGN KEY (`ProjectId`) REFERENCES `ProjectInfo`(`id`);

ALTER TABLE `dfc`.`TradeInfo` ADD CONSTRAINT `TradeInfo_fk0` FOREIGN KEY (`CustomerProjectId`) REFERENCES `CustomerLinkProject`(`id`);

ALTER TABLE `dfc`.`OrderStatus` ADD CONSTRAINT `OrderStatus_fk0` FOREIGN KEY (`TradeId`) REFERENCES `TradeInfo`(`id`);

ALTER TABLE `dfc`.`OrderHistory` ADD CONSTRAINT `OrderHistory_fk0` FOREIGN KEY (`TradeId`) REFERENCES `TradeInfo`(`id`);




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
