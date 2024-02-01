DROP TABLE IF EXISTS Transactions;
DROP TABLE IF EXISTS Accounts;
DROP TABLE IF EXISTS Users;
drop table if exists temp;

CREATE TABLE temp
(
	name text primary key,
	id int
);

CREATE TABLE Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    email TEXT NOT NULL,
    password TEXT NOT NULL,
    firstname TEXT NOT NULL,
    lastname TEXT NOT NULL,
    phone TEXT
);

CREATE TABLE Accounts (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    balance REAL NOT NULL,
    type INTEGER NOT NULL,
    userId INTEGER NOT NULL DEFAULT 0,
    FOREIGN KEY
(userId) REFERENCES Users
(Id)
);

CREATE TABLE Transactions (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    TransDate TEXT NOT NULL,
    AcctId INTEGER NOT NULL,
    Amount REAL NOT NULL,
    Payee TEXT NOT NULL,
    Type INTEGER NOT NULL,
    FOREIGN KEY
(AcctId) REFERENCES Accounts
(Id)
);

-- peggy
INSERT INTO Users
	(email, firstname, lastname, phone, password)
VALUES
	('peggy@yahoo.com', 'Peggy', 'Hill', '111-222-0112', 'boggle');

insert into temp
	(name, id)
values
	('peggy', last_insert_rowid());

INSERT INTO Accounts
	(balance, type, userId)
VALUES
	(431.22, 1, (select id
		from temp
		where name='peggy'));

insert into temp
	(name,id)
values
	('pacct', last_insert_rowid());

INSERT INTO Accounts
	(balance, type, userId)
VALUES
	(431.22, 2, (select id
		from temp
		where name='peggy'));

insert into temp
	(name,id)
values
	('pacct2', last_insert_rowid());

INSERT INTO Transactions
	(TransDate, AcctId, Amount, Payee, Type)
VALUES
	('2019-02-14', (select id
		from temp
		where name='pacct'), 100.00, 'Lulys', 1);
INSERT INTO Transactions
	(TransDate, AcctId, Amount, Payee, Type)
VALUES
	('2019-02-08', (select id
		from temp
		where name='pacct2'), 200.00, 'Strickland', 2);

-- hank
INSERT INTO Users
	(email, firstname, lastname, phone, password)
VALUES
	('hank@propane.com', 'Hank', 'Hill', '986-222-0012', 'propane');

insert into temp (name, id) values ('hank', last_insert_rowid());

INSERT INTO Accounts
	(balance, type, userId)
VALUES
	(881.22, 1, (select id from temp where name='hank'));

insert into temp (name, id) values ('hacct', last_insert_rowid());

INSERT INTO Accounts
	(balance, type, userId)
VALUES
	(881.22, 2, (select id from temp where name='hank'));

insert into temp (name, id) values ('hacct2', last_insert_rowid());

INSERT INTO Transactions
	(TransDate, AcctId, Amount, Payee, Type)
VALUES
	('2019-02-14', (select id from temp where name='hacct'), 190.00, 'Ace Hardware', 1);
INSERT INTO Transactions
	(TransDate, AcctId, Amount, Payee, Type)
VALUES
	('2019-02-18', (select id
from temp
where name='hacct'), 90.00, 'Tom Landry', 1);
INSERT INTO Transactions
	(TransDate, AcctId, Amount, Payee, Type)
VALUES
	('2019-02-16', (select id
from temp
where name='hacct2'), 2500.00, 'Strickland', 2);


drop table temp;