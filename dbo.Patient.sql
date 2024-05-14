CREATE TABLE [dbo].[Patient] (
    [UserEmail]                   VARCHAR (50) NOT NULL,
    [Password]                    VARCHAR (20) NOT NULL,
    [SSN]                         INT          NOT NULL,
    [ElectronicHealthRecords]     VARCHAR (50) NULL,
    [emergencyContactInformation] VARCHAR (50) NULL,
    [allergies]                   VARCHAR (50) NULL,
    [chronicDiseases]             VARCHAR (50) NULL,
    [vaccines]                    VARCHAR (50) NULL,
    [prescribedDrugs]             VARCHAR (50) NULL,
    [results]                     VARCHAR (50) NULL,
    [Hospital_name]               VARCHAR (50) NULL,
    [companyid]                   INT          NULL,
    [nurseID]                     INT          NULL,
    PRIMARY KEY CLUSTERED ([SSN] ASC),
    UNIQUE NONCLUSTERED ([UserEmail] ASC),
    FOREIGN KEY ([nurseID]) REFERENCES [dbo].[Nurse] ([Nurse_ID]),
    FOREIGN KEY ([Hospital_name]) REFERENCES [dbo].[Hospital] ([Hospital_name])
);

