insert into "format" ("type") VALUES ('CD-DA'), ('SACD'), ('DVD-Audio'), ('DVD-Video'), ('Cassette');
insert into "country"("name") values ('Украина'),('Россия'),('Чехия'),('США'),('Греция');
insert into "city"("name", "id_country") values ('Афины', '5'),('Чикаго', '4'),('Прага', '3'),('Москва', '2'),('Киев', '1');
insert into "company"("name", "id_city") values ('Капитошка', '5'),('ДиньДон', '4'),('Слипнотики', '3'),('Радуга', '2'),('МиуМяу', '1');
insert into "genre"("type") values ('Рок'),('Поп'),('Соул'),('Кантри'),('Джаз');
insert into "language"("language") values ('Английский'),('Русский'),('Французский'),('Испанский'),('Китайский');
insert into "performer"("name", "surname", "patronymic") 
values ('ching', 'chong', 'ding'),('los', 'polos', 'huan'),('спать', 'спать', 'спать'),('skibidi','pun','dun'),('lu','kang','na_deviatke');


insert into "album"("name", "release_date", "copies_total", "songs_total", "collection", "album_inf", "duration", "id_company", "id_format", "id_performer", "id_genre", "id_language", "photo")
VALUES ('карп', 'January 8, 1999', 123, 10, false, '', '12:34:56', 1, 1, 1 ,1, 1, 'photo/hcing1'),
       ('Марк', 'December 11, 1997', 15, 3, true, 'А мой мальчик едет на девятке по автостраде вдоль ночных дорог.', '11:33:00', 3, 1, 2, 4, 5, 'photo/ng135'),
       ('Аврелий', 'August 23, 350', 1000, 7, false, '', '12:55:', 5, 4, 3, 2, 1, 'photo/greg');

insert into shop (name, opening_year, phone, address,id_district, id_property_type, license_num, id_owner)
values ('dgsdf', 2000, '+38(071)-123-4567', 'Rtr dfgdfg 34/15', 1, 1, 1, 1);