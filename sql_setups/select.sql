CREATE OR REPLACE FUNCTION public.select_album_catalog(
	album_data integer,
	pagenum integer,
	rowsnum integer)
    RETURNS TABLE(id integer, "Альбом" character varying, "Магазин" character varying, "Цена" character varying, "Поставлено" character varying, "Продано" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select cat.id_catalog as ID, a.name as alb, s.name, cat.price, cat.supplied_num, cat.sold_num
from catalog cat right join album a on (a.id_album = album_data and cat.id_album = album_data)
    right join shop s on s.id_shop = cat.id_shop
ORDER BY ID OFFSET (pagenum*8)ROWS FETCH NEXT (rowsnum) ROWS ONLY;
$BODY$;

ALTER FUNCTION public.select_album_catalog(integer, integer, integer)
    OWNER TO postgres;



CREATE OR REPLACE FUNCTION public.selectalbum(
	pagenum integer)
    RETURNS TABLE(id integer, "Название" character varying, "Дата выпуска" date, "Тираж" character varying, "Количество песен" character varying, "Сборник" character varying, "Информация" character varying, "Продолжительность" character varying, "Название компании" character varying, "Формат" character varying, "Исполнитель" character varying, "Жанр" character varying, "Язык" character varying, "Фото" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select alb.id_album as ID, alb.name as название, alb.release_date as дата_выпуска, alb.copies_total as тираж,
       alb.songs_total, alb.collection, alb.album_inf, alb.duration, comp.name, form.type,
	   perf.surname || ' ' || perf.name || ' ' || perf.patronymic, genr.type,
       lang.language, alb.photo
from album alb
    left join company comp on(comp.id_company=alb.id_company)
    left join format form on(form.id_format=alb.id_format)
    left join performer perf on(perf.id_performer=alb.id_performer)
    left join genre genr on(genr.id_genre=alb.id_genre)
    left join language lang on(lang.id_language=alb.id_language)
ORDER BY ID OFFSET (pagenum*24)ROWS FETCH NEXT 24 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.selectalbum(integer)
    OWNER TO postgres;



CREATE OR REPLACE FUNCTION public.selectcatalog(
	pagenum integer)
    RETURNS TABLE(id integer, "Альбом" character varying, "Магазин" character varying, "Цена" character varying, "Поставлено" character varying, "Продано" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select cat.id_catalog as ID, a.name as alb, s.name, cat.price, cat.supplied_num, cat.sold_num
from catalog cat right join album a on a.id_album = cat.id_album
    right join shop s on s.id_shop = cat.id_shop
ORDER BY ID OFFSET (pagenum*24)ROWS FETCH NEXT 24 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.selectcatalog(integer)
    OWNER TO postgres;



CREATE OR REPLACE FUNCTION public.selectcity(
	pagenum integer)
    RETURNS TABLE(id integer, "Город" character varying, "Страна" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select city.id_city as ID, city.name, c.name from city left join country c on(city.id_country=c.id_country)
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.selectcity(integer)
    OWNER TO postgres;



CREATE OR REPLACE FUNCTION public.selectcompany(
	pagenum integer)
    RETURNS TABLE(id integer, "Компания" character varying, "Город" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select comp.id_company as ID, comp.name, c.name from company comp left join city c on(comp.id_city=c.id_city)
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.selectcompany(integer)
    OWNER TO postgres;



CREATE OR REPLACE FUNCTION public.selectcountry(
	pagenum integer)
    RETURNS TABLE(id integer, "Страна" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select country.id_country as ID, country.name from country
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.selectcountry(integer)
    OWNER TO postgres;



CREATE OR REPLACE FUNCTION public.selectdistrict(
	pagenum integer)
    RETURNS TABLE(id integer, "Район города" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select dist.id_district as ID, dist.name from district dist
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.selectdistrict(integer)
    OWNER TO postgres;



CREATE OR REPLACE FUNCTION public.selectformat(
	pagenum integer)
    RETURNS TABLE(id integer, "Формат" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select format.id_format as ID, format.type from format
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.selectformat(integer)
    OWNER TO postgres;



CREATE OR REPLACE FUNCTION public.selectgenre(
	pagenum integer)
    RETURNS TABLE(id integer, "Жанр" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select genre.id_genre as ID, genre.type from genre
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.selectgenre(integer)
    OWNER TO postgres;



CREATE OR REPLACE FUNCTION public.selectlanguage(
	pagenum integer)
    RETURNS TABLE(id integer, "Язык" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select lang.id_language as ID, lang.language from language lang
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.selectlanguage(integer)
    OWNER TO postgres;



CREATE OR REPLACE FUNCTION public.selectlicense(
	pagenum integer)
    RETURNS TABLE("Номер лицензии" integer, "Срок окончания" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select lic.license_num as ID, lic.expiration_date from license lic
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.selectlicense(integer)
    OWNER TO postgres;



CREATE OR REPLACE FUNCTION public.selectowner(
	pagenum integer)
    RETURNS TABLE(id integer, "Фамилия" character varying, "Имя" character varying, "Отчество" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select owner.id_owner ID, owner.surname, owner.name, owner.patronymic from owner
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.selectowner(integer)
    OWNER TO postgres;



CREATE OR REPLACE FUNCTION public.selectperformer(
	pagenum integer)
    RETURNS TABLE(id integer, "Фамилия" character varying, "Имя" character varying, "Отчество" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select perf.id_performer as ID, perf.surname, perf.name, perf.patronymic from performer perf
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.selectperformer(integer)
    OWNER TO postgres;




CREATE OR REPLACE FUNCTION public.selectproperty(
	pagenum integer)
    RETURNS TABLE(id integer, "Тип собственности" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select prop.id_property_type as ID, prop.property_type from property_type prop
ORDER BY ID OFFSET (pagenum*15)ROWS FETCH NEXT 15 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.selectproperty(integer)
    OWNER TO postgres;



CREATE OR REPLACE FUNCTION public.selectshop(
	pagenum integer)
    RETURNS TABLE(id integer, "Название" character varying, "Год открытия" character varying, "Телефон" character varying, "Адрес" character varying, "Район города" character varying, "Тип собственности" character varying, "Лицензия выдана до" character varying, "Владелец" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select shop.id_shop as ID, shop.name, shop.opening_year, shop.phone, shop.address, dist.name, pt.property_type,
       l.expiration_date, o.surname || ' ' || o.name || ' ' || o.patronymic
from shop left join district dist on(dist.id_district=shop.id_district)
    right join property_type pt on shop.id_property_type = pt.id_property_type
    right join license l on shop.license_num = l.license_num
    right join owner o on shop.id_owner = o.id_owner

ORDER BY ID OFFSET (pagenum*24)ROWS FETCH NEXT 24 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.selectshop(integer)
    OWNER TO postgres;




CREATE OR REPLACE FUNCTION public.selectsupplies(
	pagenum integer)
    RETURNS TABLE(id integer, "Альбом" character varying, "Магазин" character varying, "Дата поступления" character varying, "Количество" character varying)
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
select sup.id_supplies as ID, a.name as alb, s.name, sup.arrival_date, sup.quantity
from supplies sup
    right join album a on a.id_album = sup.id_album
    right join shop s on s.id_shop = sup.id_shop
ORDER BY ID OFFSET (pagenum*24)ROWS FETCH NEXT 24 ROWS ONLY;
$BODY$;

ALTER FUNCTION public.selectsupplies(integer)
    OWNER TO postgres;