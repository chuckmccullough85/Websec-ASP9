drop table if exists transactions;
drop table if exists accounts;
Drop table if exists users;

CREATE TABLE [dbo].[Users] (
	[Id] INT NOT NULL identity(1,1), 
	[email] VARCHAR(50) NOT NULL, 
	[password]  VARCHAR(50) NOT NULL,
	[firstname] VARCHAR(50) NOT NULL,
	[lastname]  VARCHAR(50) NOT NULL,
	[phone]     VARCHAR(20) NULL,
	PRIMARY KEY CLUSTERED([Id] ASC));

CREATE TABLE [dbo].[Accounts] (
    [Id]      INT NOT NULL identity(1,1),
    [balance] SMALLMONEY NOT NULL,
    [type] INT NOT NULL,
    [userId] INT DEFAULT((0)) NOT NULL,
    PRIMARY KEY CLUSTERED([Id] ASC),
    CONSTRAINT[FK_Accounts_ToUser] FOREIGN KEY([userId]) REFERENCES[dbo].[Users] ([Id]));

CREATE TABLE [dbo].[Transactions] (
    [Id]        INT identity(1,1) NOT NULL,
    [TransDate] DATE NOT NULL,
    [AcctId] INT NOT NULL,
    [Amount] MONEY NOT NULL,
    [Payee] VARCHAR(50) NOT NULL,
    [Type]      INT NOT NULL,
    PRIMARY KEY CLUSTERED([Id] ASC),
    CONSTRAINT[FK_TransactionsTo_Accounts]
    FOREIGN KEY([AcctId]) REFERENCES[dbo].[Accounts] ([Id]));

declare @userid as int
declare @acctid as int

-- peggy
Insert into Users(email, firstname,
    lastname, phone,[password])
	values
    ('peggy@yahoo.com', 'Peggy', 'Hill',
    '111-222-0112', 'boggle');
select @userid = scope_identity()

INSERT INTO [dbo].[Accounts]
    ([balance], [type], [userId]) VALUES 
    (CAST(431.2200 AS SmallMoney), 1, @userid);
select @acctid = SCOPE_IDENTITY()

INSERT INTO [dbo].[Transactions] 
    ([TransDate], [AcctId], [Amount], [Payee], [Type]) 
    VALUES (N'2019-02-14', @acctid, CAST(100.0000 AS Money), N'Lulys',1);
INSERT INTO[dbo].[Transactions] 
    ([TransDate], [AcctId], [Amount], [Payee], [Type])
    VALUES(N'2019-02-08', @acctid, CAST(200.0000 AS Money), N'Strickland', 2);

INSERT INTO[dbo].[Accounts] ([balance], [type], [userId]) VALUES
    (CAST(3555.0100 AS SmallMoney), 2, @userid);
select @acctid = SCOPE_IDENTITY()

INSERT INTO [dbo].[Transactions] 
    ([TransDate], [AcctId], [Amount], [Payee], [Type]) 
    VALUES (N'2019-02-14', @acctid, CAST(100.0000 AS Money), N'Boggle store',1);
INSERT INTO[dbo].[Transactions] 
    ([TransDate], [AcctId], [Amount], [Payee], [Type])
    VALUES(N'2019-02-08', @acctid, CAST(200.0000 AS Money), N'Transfer', 2);


--hank
Insert into Users(email, firstname,
    lastname, phone,[password]) values
    ('hank@propane.com', 'Hank', 'Hill',
    '986-222-0012', 'propane');
select @userid = SCOPE_IDENTITY();
INSERT INTO [dbo].[Accounts]
        ([balance], [type], [userId]) VALUES 
        (CAST(881.2200 AS SmallMoney), 1, @userid);
select @acctid = SCOPE_IDENTITY();

INSERT INTO [dbo].[Transactions] 
    ([TransDate], [AcctId], [Amount], [Payee], [Type]) 
    VALUES (N'2019-02-14', @acctid, CAST(190.0000 AS Money), N'Ace Hardware', 1);
INSERT INTO[dbo].[Transactions] 
   ([TransDate], [AcctId], [Amount], [Payee], [Type]) 
    VALUES (N'2019-02-18', @acctid, CAST(90.0000 AS Money), N'Tom Landry', 1);
INSERT INTO[dbo].[Transactions] 
    ([TransDate], [AcctId], [Amount], [Payee], [Type])
     VALUES(N'2019-02-16', @acctid, CAST(2500.0000 AS Money), N'Strickland', 2);


INSERT INTO[dbo].[Accounts]
         ([balance], [type], [userId]) VALUES
        (CAST(8975.0100 AS SmallMoney), 2, @userid);
select @acctid = SCOPE_IDENTITY();
INSERT INTO[dbo].[Transactions] 
    ([TransDate], [AcctId], [Amount], [Payee], [Type])
     VALUES(N'2019-02-16', @acctid, CAST(1000.0000 AS Money), N'Transfer from Chk', 2);

--luann
Insert into Users(email, firstname,
    lastname, phone,[password]) values
    ('luann@yahoo.com', 'Luann', 'Platter',
    '321-222-0889', 'blondie');
select @userid = SCOPE_IDENTITY();

INSERT INTO [dbo].[Accounts]
        ([balance], [type], [userId]) VALUES 
        (CAST(8981.2200 AS SmallMoney), 1, 3);
select @acctid = SCOPE_IDENTITY();

INSERT INTO [dbo].[Transactions] 
    ([TransDate], [AcctId], [Amount], [Payee], [Type]) 
    VALUES (N'2019-02-01', @acctid, CAST(109.0000 AS Money), N'Nail Salon', 1);
INSERT INTO[dbo].[Transactions] 
    ([TransDate], [AcctId], [Amount], [Payee], [Type]) 
    VALUES (N'2019-02-11', @acctid, CAST(90.0000 AS Money), N'Hair Salon', 1);

INSERT INTO[dbo].[Accounts]
        ([balance], [type], [userId]) VALUES
        (CAST(98975.0100 AS SmallMoney), 2, 3);
select @acctid = SCOPE_IDENTITY();

INSERT INTO [dbo].[Transactions] 
    ([TransDate], [AcctId], [Amount], [Payee], [Type]) 
    VALUES (N'2019-02-01', @acctid, CAST(239.0000 AS Money), N'Interest', 2);


