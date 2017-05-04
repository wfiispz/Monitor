CREATE TABLE DummyData (
	Id integer PRIMARY KEY AUTOINCREMENT,
	string varchar,
	int integer
);

CREATE TABLE DummyData2 (
	Id integer PRIMARY KEY AUTOINCREMENT,
	string varchar,
	DummyDataId integer
);