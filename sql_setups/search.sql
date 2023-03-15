/*searchcountry*/
CREATE OR REPLACE FUNCTION public.searchcountry(
	pagenum integer,
	strstr1 character varying,
	strstr2 character varying)
    RETURNS TABLE(id integer, "Страна" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select country.id_country as ID, country.name
from country
where country.name ilike strstr1 or
country.name ilike strstr2
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.searchcountry(integer, character varying, character varying)
    OWNER TO postgres;

/*searchlanguage*/
CREATE OR REPLACE FUNCTION public.searchlanguage(
	pagenum integer,
	strstr1 character varying,
	strstr2 character varying)
    RETURNS TABLE(id integer, "Язык" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select language.id_language as ID, language.language
from language
where language.name ilike strstr1 or
language.name ilike strstr2
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.searchlanguage(integer, character varying, character varying)
    OWNER TO postgres;

/*searchformat*/
CREATE OR REPLACE FUNCTION public.searchformat(
	pagenum integer,
	strstr1 character varying,
	strstr2 character varying)
    RETURNS TABLE(id integer, "Формат" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select format.id_format as ID, format.type
from format
where format.name ilike strstr1 or
format.name ilike strstr2
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.searchformat(integer, character varying, character varying)
    OWNER TO postgres;

/*searchgenre*/
CREATE OR REPLACE FUNCTION public.searchgenre(
	pagenum integer,
	strstr1 character varying,
	strstr2 character varying)
    RETURNS TABLE(id integer, "Жанр" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select genre.id_genre as ID, genre.type
from genre
where genre.name ilike strstr1 or
genre.name ilike strstr2
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.searchgenre(integer, character varying, character varying)
    OWNER TO postgres;

/*searchperformer*/
CREATE OR REPLACE FUNCTION public.searchperformer(
	pagenum integer,
	strstr1 character varying,
	strstr2 character varying)
    RETURNS TABLE(id integer, "Фамилия" character varying, "Имя" character varying, "Отчество" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select perf.id_performer as ID, perf.surname, perf.name, perf.patronymic from performer perf
where perf.name ilike strstr1 or
perf.name ilike strstr2
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.searchperformer(integer, character varying, character varying)
    OWNER TO postgres;

/*searchproperty*/
CREATE OR REPLACE FUNCTION public.searchproperty(
	pagenum integer,
	strstr1 character varying,
	strstr2 character varying)
    RETURNS TABLE(id integer, "Тип собственности" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select prop.id_property_type as ID, prop.property_type from property_type prop
where prop.name ilike strstr1 or
prop.name ilike strstr2
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.searchproperty(integer, character varying, character varying)
    OWNER TO postgres;

/*searchowner*/
CREATE OR REPLACE FUNCTION public.searchowner(
	pagenum integer,
	strstr1 character varying,
	strstr2 character varying)
    RETURNS TABLE(id integer, "Фамилия" character varying, "Имя" character varying, "Отчество" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select owner.id_owner ID, owner.surname, owner.name, owner.patronymic from owner
where owner.surname ilike strstr1 or
owner.surname ilike strstr2
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.searchowner(integer, character varying, character varying)
    OWNER TO postgres;

/*searchdistrict*/
CREATE OR REPLACE FUNCTION public.searchdistrict(
	pagenum integer,
	strstr1 character varying,
	strstr2 character varying)
    RETURNS TABLE(id integer, "Район города" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select dist.id_district as ID, dist.name from district dist
where dist.name ilike strstr1 or
dist.name ilike strstr2
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.searchdistrict(integer, character varying, character varying)
    OWNER TO postgres;

/*searchlicense*/
CREATE OR REPLACE FUNCTION public.searchlicense(
	pagenum integer,
	strstr1 character varying,
	strstr2 character varying)
    RETURNS TABLE("Номер лицензии" integer, "Срок окончания" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select lic.license_num as ID, lic.expiration_date from license lic
where lic.expiration_date ilike strstr1 or
lic.expiration_date ilike strstr2
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.searchlicense(integer, character varying, character varying)
    OWNER TO postgres;

/*searchcatalog*/
CREATE OR REPLACE FUNCTION public.searchcatalog(
	pagenum integer,
	strstr1 character varying,
	strstr2 character varying,
	tabcol character varying)
    RETURNS TABLE("Альбом" character varying, "Магазин" character varying, "Цена" character varying,
    "Поставлено" character varying, "Продано" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select a.name as alb, s.name, cat.price as ID, cat.supplied_num, cat.sold_num
from catalog cat right join album a on a.id_album = cat.id_album
    right join shop s on s.id_shop = cat.id_shop
where tabcol ilike strstr1 or
tabcol ilike strstr2
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.searchcatalog(integer, character varying, character varying, character varying, character varying)
    OWNER TO postgres;

/*searchcountry*/
CREATE OR REPLACE FUNCTION public.searchcountry(
	pagenum integer,
	strstr1 character varying,
	strstr2 character varying)
    RETURNS TABLE(id integer, "Страна" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select country.id_country as ID, country.name
from country
where country.name ilike strstr1 or
country.name ilike strstr2
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.searchcountry(integer, character varying, character varying)
    OWNER TO postgres;

/*searchcountry*/
CREATE OR REPLACE FUNCTION public.searchcountry(
	pagenum integer,
	strstr1 character varying,
	strstr2 character varying)
    RETURNS TABLE(id integer, "Страна" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select country.id_country as ID, country.name
from country
where country.name ilike strstr1 or
country.name ilike strstr2
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.searchcountry(integer, character varying, character varying)
    OWNER TO postgres;