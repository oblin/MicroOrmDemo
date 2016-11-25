CREATE TABLE Addresses (
    Id            serial primary key NOT NULL,
    ContactId     INT          NOT NULL,
    AddressType   VARCHAR (10) NOT NULL,
    StreetAddress VARCHAR (50) NOT NULL,
    City          VARCHAR (50) NOT NULL,
    StateId       INT          NOT NULL,
    PostalCode    VARCHAR (20) NOT NULL
);

CREATE TABLE Contacts (
    Id        serial primary key NOT NULL,
    FirstName VARCHAR (50) NULL,
    LastName  VARCHAR (50) NULL,
    Email     VARCHAR (50) NULL,
    Company   VARCHAR (50) NULL,
    Title     VARCHAR (50) NULL
);

CREATE TABLE States (
    Id        INT  primary key        NOT NULL,
    StateName VARCHAR (50) NOT NULL
);

ALTER TABLE Addresses ADD CONSTRAINT FK_Addresses_Contacts foreign KEY (ContactId) REFERENCES public.Contacts (Id) ON DELETE CASCADE; 
ALTER TABLE Addresses ADD CONSTRAINT FK_Addresses_States foreign KEY (StateId) REFERENCES public.States (Id) ON DELETE CASCADE; 