﻿INSERT INTO States (Id, StateName) VALUES (1, 'Alabama');
INSERT INTO States (Id, StateName) VALUES (2, 'Alaska');
INSERT INTO States (Id, StateName) VALUES (4, 'Arizona');
INSERT INTO States (Id, StateName) VALUES (5, 'Arkansas');
INSERT INTO States (Id, StateName) VALUES (6, 'California');
INSERT INTO States (Id, StateName) VALUES (8, 'Colorado');
INSERT INTO States (Id, StateName) VALUES (9, 'Connecticut');
INSERT INTO States (Id, StateName) VALUES (10, 'Delaware');
INSERT INTO States (Id, StateName) VALUES (11, 'District of Columbia');
INSERT INTO States (Id, StateName) VALUES (12, 'Florida');
INSERT INTO States (Id, StateName) VALUES (13, 'Georgia');
INSERT INTO States (Id, StateName) VALUES (15, 'Hawaii');
INSERT INTO States (Id, StateName) VALUES (16, 'Idaho');
INSERT INTO States (Id, StateName) VALUES (17, 'Illinois');
INSERT INTO States (Id, StateName) VALUES (18, 'Indiana');
INSERT INTO States (Id, StateName) VALUES (19, 'Iowa');
INSERT INTO States (Id, StateName) VALUES (20, 'Kansas');
INSERT INTO States (Id, StateName) VALUES (21, 'Kentucky');
INSERT INTO States (Id, StateName) VALUES (22, 'Louisiana');
INSERT INTO States (Id, StateName) VALUES (23, 'Maine');
INSERT INTO States (Id, StateName) VALUES (24, 'Maryland');
INSERT INTO States (Id, StateName) VALUES (25, 'Massachusetts');
INSERT INTO States (Id, StateName) VALUES (26, 'Michigan');
INSERT INTO States (Id, StateName) VALUES (27, 'Minnesota');
INSERT INTO States (Id, StateName) VALUES (28, 'Mississippi');
INSERT INTO States (Id, StateName) VALUES (29, 'Missouri');
INSERT INTO States (Id, StateName) VALUES (30, 'Montana');
INSERT INTO States (Id, StateName) VALUES (31, 'Nebraska');
INSERT INTO States (Id, StateName) VALUES (32, 'Nevada');
INSERT INTO States (Id, StateName) VALUES (33, 'New Hampshire');
INSERT INTO States (Id, StateName) VALUES (34, 'New Jersey');
INSERT INTO States (Id, StateName) VALUES (35, 'New Mexico');
INSERT INTO States (Id, StateName) VALUES (36, 'New York');
INSERT INTO States (Id, StateName) VALUES (37, 'North Carolina');
INSERT INTO States (Id, StateName) VALUES (38, 'North Dakota');
INSERT INTO States (Id, StateName) VALUES (39, 'Ohio');
INSERT INTO States (Id, StateName) VALUES (40, 'Oklahoma');
INSERT INTO States (Id, StateName) VALUES (41, 'Oregon');
INSERT INTO States (Id, StateName) VALUES (42, 'Pennsylvania');
INSERT INTO States (Id, StateName) VALUES (44, 'Rhode Island');
INSERT INTO States (Id, StateName) VALUES (45, 'South Carolina');
INSERT INTO States (Id, StateName) VALUES (46, 'South Dakota');
INSERT INTO States (Id, StateName) VALUES (47, 'Tennessee');
INSERT INTO States (Id, StateName) VALUES (48, 'Texas');
INSERT INTO States (Id, StateName) VALUES (49, 'Utah');
INSERT INTO States (Id, StateName) VALUES (50, 'Vermont');
INSERT INTO States (Id, StateName) VALUES (51, 'Virginia');
INSERT INTO States (Id, StateName) VALUES (53, 'Washington');
INSERT INTO States (Id, StateName) VALUES (54, 'West Virginia');
INSERT INTO States (Id, StateName) VALUES (55, 'Wisconsin');
INSERT INTO States (Id, StateName) VALUES (56, 'Wyoming');

INSERT INTO Contacts (FirstName, LastName, Email, Company, Title) VALUES ('Michael', 'Jordan', 'michael@bulls.com', 'Chicago Bulls', 'MVP');
INSERT INTO Contacts (FirstName, LastName, Email, Company, Title) VALUES ('LaBron', 'James', 'labron@heat.com', 'Miami Heat', 'King James');
INSERT INTO Contacts (FirstName, LastName, Email, Company, Title) VALUES ('Kobe', 'Bryant', 'kobe@lakers.com', 'Los Angeles Lakers', 'Guard');
INSERT INTO Contacts (FirstName, LastName, Email, Company, Title) VALUES ('Kevin', 'Durant', 'kevin@thunder.com', 'OKC Thunder', 'Durantula');
INSERT INTO Contacts (FirstName, LastName, Email, Company, Title) VALUES ('Kyrie', 'Irving', 'kyrie@cavs.com', 'Cleveland Cavaliers', 'Uncle Drew');;
INSERT INTO Contacts (FirstName, LastName, Email, Company, Title) VALUES ('Chris', 'Paul', 'chris@clippers.com', 'Los Angeles Clippers', 'CP3');

INSERT INTO Addresses (ContactId, AddressType, StreetAddress, City, StateId, PostalCode) VALUES(1, 'Home', '123 Main Street', 'Chicago', 17, '60290');
INSERT INTO Addresses (ContactId, AddressType, StreetAddress, City, StateId, PostalCode) VALUES(1, 'Work', '1901 W Madison St', 'Chicago', 17, '60612');
INSERT INTO Addresses (ContactId, AddressType, StreetAddress, City, StateId, PostalCode) VALUES(2, 'Home', '123 Main Street', 'Miami', 12, '33101');
INSERT INTO Addresses (ContactId, AddressType, StreetAddress, City, StateId, PostalCode) VALUES(3, 'Home', '123 Main Street', 'Los Angeles', 6, '90001');
INSERT INTO Addresses (ContactId, AddressType, StreetAddress, City, StateId, PostalCode) VALUES(4, 'Home', '123 Main Street', 'Oklahoma City', 40, '73101');
INSERT INTO Addresses (ContactId, AddressType, StreetAddress, City, StateId, PostalCode) VALUES(5, 'Home', '123 Main Street', 'Cleveland', 39, '44101');
INSERT INTO Addresses (ContactId, AddressType, StreetAddress, City, StateId, PostalCode) VALUES(6, 'Home', '456 Main Street', 'Los Angeles', 6, '90003');
