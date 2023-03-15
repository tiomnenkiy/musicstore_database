CREATE TABLE "shop" (
	"id_shop" serial NOT NULL,
	"name" varchar NOT NULL,
	"opening_year" smallint NOT NULL,
	"phone" varchar(20) NOT NULL,
	"address" varchar(30) NOT NULL,
	"id_district" integer NOT NULL,
	"id_property_type" integer NOT NULL,
	"license_num" integer NOT NULL,
	"id_owner" integer NOT NULL,
	CONSTRAINT "shop_pk" PRIMARY KEY ("id_shop"),
    CONSTRAINT "valid_opening_year" CHECK (opening_year > 1799),
    CONSTRAINT "unique_shop" UNIQUE (name, address, id_district)
) WITH (
  OIDS=FALSE
);



CREATE TABLE "license" (
	"license_num" serial NOT NULL,
	"expiration_date" date NOT NULL,
	CONSTRAINT "license_pk" PRIMARY KEY ("license_num")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "owner" (
	"id_owner" serial NOT NULL,
	"surname" varchar(30) NOT NULL,
	"name" varchar(30) NOT NULL,
	"patronymic" varchar(30) NOT NULL,
	CONSTRAINT "owner_pk" PRIMARY KEY ("id_owner"),
	CONSTRAINT "unique_owner" UNIQUE (surname, name, patronymic)
) WITH (
  OIDS=FALSE
);



CREATE TABLE "district" (
	"id_district" serial NOT NULL,
	"name" varchar(30) NOT NULL,
	CONSTRAINT "district_pk" PRIMARY KEY ("id_district"),
	CONSTRAINT "unique_district" UNIQUE (name)
) WITH (
  OIDS=FALSE
);



CREATE TABLE "property_type" (
	"id_property_type" serial NOT NULL,
	"property_type" varchar(30) NOT NULL,
	CONSTRAINT "property_type_pk" PRIMARY KEY ("id_property_type"),
	CONSTRAINT "unique_property_type" UNIQUE (property_type)
) WITH (
  OIDS=FALSE
);



CREATE TABLE "album" (
	"id_album" serial NOT NULL,
	"name" varchar(30) NOT NULL,
	"release_date" DATE NOT NULL,
	"copies_total" bigint NOT NULL,
	"songs_total" smallint NOT NULL,
	"collection" BOOLEAN NOT NULL,
	"album_inf" varchar(200),
	"duration" TIME NOT NULL,
	"id_company" integer NOT NULL,
	"id_format" integer NOT NULL,
	"id_performer" integer NOT NULL,
	"id_genre" integer NOT NULL,
	"id_language" integer NOT NULL,
	"photo" varchar(100) NOT NULL,
	CONSTRAINT "album_pk" PRIMARY KEY ("id_album"),
	CONSTRAINT "valid_copies" CHECK (copies_total > 0),
	CONSTRAINT "valid_songs_total" CHECK (songs_total > 0),
	CONSTRAINT "unique_album" UNIQUE (name, release_date, id_performer)
) WITH (
  OIDS=FALSE
);



CREATE TABLE "supplies" (
    "id_supplies" serial NOT NULL,
	"id_album" integer NOT NULL,
	"id_shop" integer NOT NULL,
	"arrival_date" DATE NOT NULL,
	"quantity" bigint NOT NULL,
	CONSTRAINT "supplies_pk" PRIMARY KEY (id_supplies),
	CONSTRAINT "valid_quantity" CHECK (quantity > 0)
) WITH (
  OIDS=FALSE
);



CREATE TABLE "company" (
	"id_company" serial NOT NULL,
	"name" varchar(30) NOT NULL,
	"id_city" integer NOT NULL,
	CONSTRAINT "company_pk" PRIMARY KEY ("id_company"),
	CONSTRAINT "unique_company" UNIQUE (name, id_city)
) WITH (
  OIDS=FALSE
);



CREATE TABLE "city" (
	"id_city" serial NOT NULL,
	"name" varchar(30) NOT NULL,
	"id_country" integer NOT NULL,
	CONSTRAINT "city_pk" PRIMARY KEY ("id_city"),
	CONSTRAINT "unique_city" UNIQUE (name, id_country)
) WITH (
  OIDS=FALSE
);



CREATE TABLE "country" (
	"id_country" serial NOT NULL,
	"name" varchar(30) NOT NULL,
	CONSTRAINT "country_pk" PRIMARY KEY ("id_country"),
	CONSTRAINT "unique_country" UNIQUE (name)
) WITH (
  OIDS=FALSE
);



CREATE TABLE "format" (
	"id_format" serial NOT NULL,
	"type" varchar(30) NOT NULL,
	CONSTRAINT "format_pk" PRIMARY KEY ("id_format"),
	CONSTRAINT "unique_format" UNIQUE (type)
) WITH (
  OIDS=FALSE
);



CREATE TABLE "performer" (
	"id_performer" serial NOT NULL,
	"name" varchar(30) NOT NULL,
	"surname" varchar(30) NOT NULL,
	"patronymic" varchar(30) NOT NULL,
	CONSTRAINT "performer_pk" PRIMARY KEY ("id_performer"),
	CONSTRAINT "unique_performer" UNIQUE (surname, name, patronymic)
) WITH (
  OIDS=FALSE
);



CREATE TABLE "genre" (
	"id_genre" serial NOT NULL,
	"type" varchar(30) NOT NULL,
	CONSTRAINT "genre_pk" PRIMARY KEY ("id_genre"),
	CONSTRAINT "unique_genre" UNIQUE (type)
) WITH (
  OIDS=FALSE
);



CREATE TABLE "language" (
	"id_language" serial NOT NULL,
	"language" varchar(30) NOT NULL,
	CONSTRAINT "language_pk" PRIMARY KEY ("id_language"),
	CONSTRAINT "unique_language" UNIQUE (language)
) WITH (
  OIDS=FALSE
);



CREATE TABLE "catalog" (
    "id_catalog" serial NOT NULL,
	"id_album" integer NOT NULL,
	"id_shop" integer NOT NULL,
	"price" integer NOT NULL,
	"supplied_num" bigint NOT NULL,
	"sold_num" bigint NOT NULL,
	CONSTRAINT "catalog_pk" PRIMARY KEY ("id_catalog"),
	CONSTRAINT "valid_price" CHECK (price > 0),
	CONSTRAINT "valid_supplied_num" CHECK (supplied_num > 0),
	CONSTRAINT "valid_sold_num" CHECK (sold_num > 0 AND sold_num < supplied_num),
	CONSTRAINT "unique_catalog" UNIQUE (id_album, id_shop)
) WITH (
  OIDS=FALSE
);