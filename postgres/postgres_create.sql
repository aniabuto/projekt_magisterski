CREATE DATABASE "safe_music_store";

\connect "safe_music_store"

DROP TABLE IF EXISTS "Genres" CASCADE;

CREATE TABLE "Genres" (
    "id" SERIAL PRIMARY KEY NOT NULL,
    "name" varchar(120) NULL,
    "description" varchar(4000) NULL
);

DROP TABLE IF EXISTS "Artists" CASCADE;

CREATE TABLE "Artists" (
    "id" SERIAL PRIMARY KEY NOT NULL,
    "name" varchar(120) NULL,
    "bio" varchar(4000) NULL
);

DROP TABLE IF EXISTS "Albums" CASCADE;

CREATE TABLE "Albums"(
	"id" SERIAL PRIMARY KEY NOT NULL,
	"genre_id" int NOT NULL,
	"artist_id" int NOT NULL,
	"title" varchar(160) NOT NULL,
	"price" numeric(10, 2) NOT NULL,
	"thumbnail" varchar(1024) NULL CONSTRAINT DF_Album_Thumbnail  DEFAULT ('/placeholder.gif')
);

DROP TABLE IF EXISTS "Orders";

CREATE TABLE "Orders"(
	"id" SERIAL PRIMARY KEY NOT NULL,
	"date" timestamptz NOT NULL,
	"username" varchar(256) NULL,
	"firstname" varchar(160) NULL,
	"lastname" varchar(160) NULL,
	"address" varchar(70) NULL,
	"city" varchar(40) NULL,
	"state" varchar(40) NULL,
	"postal_code" varchar(10) NULL,
	"country" varchar(40) NULL,
	"phone" varchar(24) NULL,
	"email" varchar(160) NULL,
	"total" numeric(10, 2) NOT NULL
);

DROP TABLE IF EXISTS "OrderDetails";

CREATE TABLE "OrderDetails"(
	"id" SERIAL PRIMARY KEY NOT NULL,
	"order_id" int NOT NULL,
	"album_id" int NOT NULL,
	"quantity" int NOT NULL,
	"unit_price" numeric(10, 2) NOT NULL
);

DROP TABLE IF EXISTS "Carts";

CREATE TABLE "Carts"(
	"id" SERIAL PRIMARY KEY NOT NULL,
	"cart_id" varchar(50) NOT NULL,
	"album_id" int NOT NULL,
	"count" int NOT NULL,
	"date_created" timestamptz NOT NULL
);

DROP TABLE IF EXISTS "Users";

CREATE TABLE "Users"(
	"id" SERIAL PRIMARY KEY NOT NULL,
	"username" varchar(200) NOT NULL,
	"email" varchar(200) NOT NULL,
	"password" varchar(200) NOT NULL,
	"role" varchar(50) NOT NULL
);




CREATE USER suave WITH ENCRYPTED Password '1234';
GRANT USAGE ON SCHEMA public to suave;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT ON TABLES TO suave;

GRANT CONNECT ON DATABASE "safe_music_store" to suave;
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO suave;
GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public TO suave;
