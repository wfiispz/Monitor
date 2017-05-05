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

CREATE VIEW resourcesview AS
SELECT resource.guid id, resource.name name, resource.description description, sensor.guid sensorid FROM resource JOIN sensor ON resource.id=sensor.resourceid;



INSERT INTO resource (guid,name, description) values ('53eb765a-55cf-4553-9e5c-09d2e0aec392', 'resource1', 'description1');
INSERT INTO resource (guid,name, description) values ('98a7674f-db0b-4804-921a-419d9ab26815', 'resource2', 'description2');

insert into sensor (guid, metric, unit, complex, resourceId) values ('6da245b2-69e2-47bb-8f85-c366c034ba31', 'CPU', '%',0,1);
insert into sensor (guid, metric, unit, complex, resourceId) values ('bb5fed62-9acb-4807-8528-09c360a83825', 'CPU', '%',0,2);
insert into sensor (guid, metric, unit, complex, resourceId) values ('0b9de2a5-2220-429e-8740-3b63fdef949a', 'TEMP', 'C',0,2);