CREATE FUNCTION trigger_delete_language() RETURNS trigger AS '
BEGIN
if(select count(*) from album where album.id_language=OLD.id_language)>0
then delete from album where album.id_language=OlD.id_language;
end if;
return OLD;
END;
' LANGUAGE plpgsql;


CREATE TRIGGER delete_language BEFORE DELETE ON language FOR EACH ROW EXECUTE PROCEDURE trigger_delete_language();

CREATE FUNCTION trigger_delete_format() RETURNS trigger AS '
BEGIN
if(select count(*) from album where album.id_format=OLD.id_format)>0
then delete from album where album.id_format=OlD.id_format;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER delete_format BEFORE DELETE ON format FOR EACH ROW EXECUTE PROCEDURE trigger_delete_format();

CREATE FUNCTION trigger_delete_genre() RETURNS trigger AS '
BEGIN
if(select count(*) from album where album.id_genre=OLD.id_genre)>0
then delete from album where album.id_genre=OlD.id_genre;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER delete_genre BEFORE DELETE ON genre FOR EACH ROW EXECUTE PROCEDURE trigger_delete_genre();

CREATE FUNCTION trigger_delete_performer() RETURNS trigger AS '
BEGIN
if(select count(*) from album where album.id_performer=OLD.id_performer)>0
then delete from album where album.id_performer=OlD.id_performer;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER delete_performer BEFORE DELETE ON performer FOR EACH ROW EXECUTE PROCEDURE trigger_delete_performer();

CREATE FUNCTION trigger_delete_company() RETURNS trigger AS '
BEGIN
if(select count(*) from album where album.id_company=OLD.id_company)>0
then delete from album where album.id_company=OlD.id_company;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER delete_company BEFORE DELETE ON company FOR EACH ROW EXECUTE PROCEDURE trigger_delete_company();

CREATE FUNCTION trigger_delete_city() RETURNS trigger AS '
BEGIN
if(select count(*) from company where company.id_city=OLD.id_city)>0
then delete from company where company.id_city=OlD.id_city;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER delete_city BEFORE DELETE ON city FOR EACH ROW EXECUTE PROCEDURE trigger_delete_city();

CREATE FUNCTION trigger_delete_country() RETURNS trigger AS '
BEGIN
if(select count(*) from city where city.id_country=OLD.id_country)>0
then delete from city where city.id_country=OlD.id_country;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER delete_country BEFORE DELETE ON country FOR EACH ROW EXECUTE PROCEDURE trigger_delete_country();

CREATE FUNCTION trigger_delete_license() RETURNS trigger AS '
BEGIN
if(select count(*) from shop where shop.license_num=OLD.license_num)>0
then delete from shop where shop.license_num=OlD.license_num;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER delete_license BEFORE DELETE ON license FOR EACH ROW EXECUTE PROCEDURE trigger_delete_license();

CREATE FUNCTION trigger_delete_district() RETURNS trigger AS '
BEGIN
if(select count(*) from shop where shop.id_district=OLD.id_district)>0
then delete from shop where shop.id_district=OlD.id_district;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER delete_district BEFORE DELETE ON district FOR EACH ROW EXECUTE PROCEDURE trigger_delete_district();


CREATE FUNCTION trigger_delete_owner() RETURNS trigger AS '
BEGIN
if(select count(*) from shop where shop.id_owner=OLD.id_owner)>0
then delete from shop where shop.id_owner=OlD.id_owner;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER delete_owner BEFORE DELETE ON owner FOR EACH ROW EXECUTE PROCEDURE trigger_delete_owner();


CREATE FUNCTION trigger_delete_property_type() RETURNS trigger AS '
BEGIN
if(select count(*) from shop where shop.id_property_type=OLD.id_property_type)>0
then delete from shop where shop.id_property_type=OlD.id_property_type;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER delete_property_type BEFORE DELETE ON property_type FOR EACH ROW EXECUTE PROCEDURE trigger_delete_property_type();


CREATE FUNCTION trigger_delete_album() RETURNS trigger AS '
BEGIN
if(select count(*) from supplies where supplies.id_album=OLD.id_album)>0
then delete from supplies where supplies.id_album=OlD.id_album;
end if;
if(select count(*) from catalog where catalog.id_album=OLD.id_album)>0
then delete from catalog where catalog.id_album=OlD.id_album;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER delete_album BEFORE DELETE ON album FOR EACH ROW EXECUTE PROCEDURE trigger_delete_album();


CREATE FUNCTION trigger_delete_shop() RETURNS trigger AS '
BEGIN
if(select count(*) from supplies where supplies.id_shop=OLD.id_shop)>0
then delete from supplies where supplies.id_shop=OlD.id_shop;
end if;
if(select count(*) from catalog where catalog.id_shop=OLD.id_shop)>0
then delete from catalog where catalog.id_shop=OlD.id_shop;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER delete_shop BEFORE DELETE ON shop FOR EACH ROW EXECUTE PROCEDURE trigger_delete_shop();

CREATE FUNCTION trigger_insert_supply() RETURNS trigger AS '
BEGIN
IF (SELECT count(*) FROM catalog AS c WHERE c.id_album = NEW.id_album AND c.id_shop = NEW.id_shop) > 0
THEN UPDATE catalog
SET supplied_num = supplied_num + NEW.quantity
WHERE id_album = NEW.id_album AND id_shop = NEW.id_shop;
END IF;
RETURN NEW;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER insert_supply BEFORE INSERT ON supplies FOR EACH ROW EXECUTE PROCEDURE trigger_insert_supply();
/*
CREATE FUNCTION trigger_insert_catalog() RETURNS trigger AS '
BEGIN
IF (SELECT count(*) FROM supplies AS s WHERE s.id_album = NEW.id_album AND s.id_shop = NEW.id_shop) > 0
THEN NEW.supplied_num = NEW.supplied_num + supplies.quantity FROM supplies
WHERE supplies.id_album = NEW.id_album AND supplies.id_shop = NEW.id_shop;
END IF;
RETURN NEW;
END;
' LANGUAGE plpgsql;


CREATE TRIGGER insert_catalog BEFORE INSERT ON catalog FOR EACH ROW EXECUTE PROCEDURE trigger_insert_catalog();
*/
CREATE FUNCTION trigger_truncate_language() RETURNS trigger AS '
BEGIN
if(select count(*) from album where album.id_language=OLD.id_language)>0
then truncate album;
end if;
return OLD;
END;
' LANGUAGE plpgsql;


CREATE TRIGGER truncate_language BEFORE truncate ON language EXECUTE PROCEDURE trigger_truncate_language();

CREATE FUNCTION trigger_truncate_format() RETURNS trigger AS '
BEGIN
if(select count(*) from album where album.id_format=OLD.id_format)>0
then truncate album;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER truncate_format BEFORE truncate ON format EXECUTE PROCEDURE trigger_truncate_format();

CREATE FUNCTION trigger_truncate_genre() RETURNS trigger AS '
BEGIN
if(select count(*) from album where album.id_genre=OLD.id_genre)>0
then truncate album;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER truncate_genre BEFORE truncate ON genre EXECUTE PROCEDURE trigger_truncate_genre();

CREATE FUNCTION trigger_truncate_performer() RETURNS trigger AS '
BEGIN
if(select count(*) from album where album.id_performer=OLD.id_performer)>0
then truncate album;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER truncate_performer BEFORE truncate ON performer EXECUTE PROCEDURE trigger_truncate_performer();

CREATE FUNCTION trigger_truncate_company() RETURNS trigger AS '
BEGIN
if(select count(*) from album where album.id_company=OLD.id_company)>0
then truncate album;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER truncate_company BEFORE truncate ON company EXECUTE PROCEDURE trigger_truncate_company();

CREATE FUNCTION trigger_truncate_city() RETURNS trigger AS '
BEGIN
if(select count(*) from company where company.id_city=OLD.id_city)>0
then truncate company;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER truncate_city BEFORE truncate ON city EXECUTE PROCEDURE trigger_truncate_city();

CREATE FUNCTION trigger_truncate_country() RETURNS trigger AS '
BEGIN
if(select count(*) from city where city.id_country=OLD.id_country)>0
then truncate city;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER truncate_country BEFORE truncate ON country EXECUTE PROCEDURE trigger_truncate_country();

CREATE FUNCTION trigger_truncate_license() RETURNS trigger AS '
BEGIN
if(select count(*) from shop where shop.license_num=OLD.license_num)>0
then truncate shop;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER truncate_license BEFORE truncate ON license EXECUTE PROCEDURE trigger_truncate_license();

CREATE FUNCTION trigger_truncate_district() RETURNS trigger AS '
BEGIN
if(select count(*) from shop where shop.id_district=OLD.id_district)>0
then truncate shop;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER truncate_district BEFORE truncate ON district EXECUTE PROCEDURE trigger_truncate_district();


CREATE FUNCTION trigger_truncate_owner() RETURNS trigger AS '
BEGIN
if(select count(*) from shop where shop.id_owner=OLD.id_owner)>0
then truncate shop;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER truncate_owner BEFORE truncate ON owner EXECUTE PROCEDURE trigger_truncate_owner();


CREATE FUNCTION trigger_truncate_property_type() RETURNS trigger AS '
BEGIN
if(select count(*) from shop where shop.id_property_type=OLD.id_property_type)>0
then truncate shop;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER truncate_property_type BEFORE truncate ON property_type EXECUTE PROCEDURE trigger_truncate_property_type();


CREATE FUNCTION trigger_truncate_album() RETURNS trigger AS '
BEGIN
if(select count(*) from supplies where supplies.id_album=OLD.id_album)>0
then truncate supplies;
end if;
if(select count(*) from catalog where catalog.id_album=OLD.id_album)>0
then truncate catalog;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER truncate_album BEFORE truncate ON album EXECUTE PROCEDURE trigger_truncate_album();


CREATE FUNCTION trigger_truncate_shop() RETURNS trigger AS '
BEGIN
if(select count(*) from supplies where supplies.id_shop=OLD.id_shop)>0
then truncate supplies;
end if;
if(select count(*) from catalog where catalog.id_shop=OLD.id_shop)>0
then truncate catalog;
end if;
return OLD;
END;
' LANGUAGE plpgsql;

CREATE TRIGGER truncate_shop BEFORE truncate ON shop EXECUTE PROCEDURE trigger_truncate_shop();