CREATE TABLE DummyData (
	Id integer PRIMARY KEY AUTOINCREMENT,
	string varchar,
	int integer
);

CREATE TABLE resource (
	id integer PRIMARY KEY AUTOINCREMENT,
	guid varchar,
	name varchar,
	description varchar
);

CREATE TABLE sensor (
	id integer PRIMARY KEY AUTOINCREMENT,
	guid varchar,
	metric varchar,
	unit varchar,
	complex integer,
	resourceid integer
);

CREATE TABLE complexmetrics (
	id integer PRIMARY KEY AUTOINCREMENT,
	sensorid integer,
	frequency integer,
	windowsize integer,
	timestart integer
);

CREATE TABLE measurements (
	id integer PRIMARY KEY AUTOINCREMENT,
	value varchar,
	timestamp integer,
	sensorid integer
);

