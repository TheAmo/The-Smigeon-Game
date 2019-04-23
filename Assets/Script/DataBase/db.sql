-- MATERIAL TABLE
DROP TABLE IF EXISTS material CASCADE;
CREATE TABLE material(
	id serial PRIMARY KEY,
	name varchar(255) NOT NULL,
	price int NOT NULL,
	damage int NOT NULL
);

-- INSERT INFO INTO MATERIAL TABLE 
INSERT INTO material (name, price, damage) VALUES ('bronze', 0, 1);
INSERT INTO material (name, price, damage) VALUES ('steel', 20, 2);
INSERT INTO material (name, price, damage) VALUES ('white', 100, 3);
INSERT INTO material (name, price, damage) VALUES ('mithril', 500, 4);
INSERT INTO material (name, price, damage) VALUES ('adamant', 1250, 5);
INSERT INTO material (name, price, damage) VALUES ('rune', 3000, 6);
INSERT INTO material (name, price, damage) VALUES ('draconic', 10000, 7);

--SHOW
SELECT name, price, damage FROM material;

--PLAYER INVENTORY TABLE
--DROP TABLE IF EXISTS player_inventory CASCADE;
--CREATE TABLE player_inventory(
	--id serial PRIMARY KEY,
	--weapons varchar(255) NOT NULL,
	--money int NOT NULL,
	--items varchar(255) NOT NULL	
--);

--MONSTER STATS TABLE 
DROP TABLE IF EXISTS monster_stats CASCADE;
CREATE TABLE monster_stats(
	id serial PRIMARY KEY,
	name varchar(255) NOT NULL,
	xp int NOT NULL,
	hitpoint int NOT NULL,
	armorclass int NOT NULL,
	attackbonus int NOT NULL,
	damagebonus int NOT NULL,
	damagedice int NOT NULL
);

-- INSERT INFO INTO MONSTER STATS TABLE 
INSERT INTO monster_stats (name, xp, hitpoint, armorclass, attackbonus, 
						   damagebonus, damagedice) 
VALUES ('Goblin', 50, 7, 15, 4, 2, 6);

-- SHOW 
SELECT name, xp, hitpoint, armorclass, attackbonus,damagebonus, damagedice
FROM monster_stats;

--PLAYER ENTRY TABLE
DROP TABLE IF EXISTS player_entry CASCADE;
CREATE TABLE player_entry(
	id serial PRIMARY KEY,
	name varchar(255) NOT NULL,
	class_name varchar(255) NOT NULL,
	experience int NOT NULL,
	positionx float NOT NULL,
	positiony float NOT NULL,
	gold int NOT NULL	
);
INSERT INTO player_entry (name, class_name, experience, positionx, positiony, gold) 
VALUES ('new player', 'allo2', 3, 0, 1, 4);

-- SHOW 
SELECT name, xp, hitpoint, armorclass, attackbonus,damagebonus, damagedice
FROM monster_stats;

-- CLASS PLAYER TABLE
DROP TABLE IF EXISTS class_player CASCADE;
CREATE TABLE class_player(
	id serial PRIMARY KEY,
	name varchar(255) NOT NULL,
	base_ability integer[6] NOT NULL,
	hitdice int NOT NULL,
	bba_each_level integer[20] NOT NULL,
	damagedice int NOT NULL
);

-- ARMOR TABLE
DROP TABLE IF EXISTS armor CASCADE;
CREATE TABLE armor(
	id serial PRIMARY KEY,
	name varchar(255) NOT NULL,
	price int NOT NULL,
	defense int NOT NULL
);

-- INSERT INFO INTO ARMOR TABLE
INSERT INTO armor (name, price, defense) VALUES ('armor1', 0, 0.15);
INSERT INTO armor (name, price, defense) VALUES ('armor2', 500, 0.3);
INSERT INTO armor (name, price, defense) VALUES ('armor3', 2500, 0.45);
INSERT INTO armor (name, price, defense) VALUES ('armor4', 5000, 0.60);
INSERT INTO armor (name, price, defense) VALUES ('armor5', 10000, 0.75);

-- SAVE PLAYER
DROP TABLE IF EXISTS save_player CASCADE;
CREATE TABLE save_player(
	id serial PRIMARY KEY,
	name varchar(255) NOT NULL,
	experience int NOT NULL,
	weapon varchar(255) NOT NULL,
	armor varchar(255) NOT NULL,
	positionx int NOT NULL,
	positiony int NOT NULL, 
	gold int NOT NULL
);
INSERT INTO save_player (name, experience, weapon, armor, positionx, positiony, gold) VALUES ('new', 1, 'bronze', 'bronze', 1,2, 100);
SELECT * FROM save_player;




			