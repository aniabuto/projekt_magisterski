CREATE DATABASE "safe_music_store";

\connect "safe_music_store"

DROP TABLE IF EXISTS "Genres" CASCADE;

CREATE TABLE Genres (
    "id"  SERIAL PRIMARY KEY NOT NULL,
    "name" varchar(120) NULL,
    "description" varchar(4000) NULL
);

INSERT INTO Genres ( "name", "description") VALUES ('Rock', 'Rock and Roll is a form of rock music developed in the 1950s and 1960s. Rock music combines many kinds of music from "the" United States, such as Country music, folk music, church music, work songs, blues and jazz.');
INSERT INTO Genres ("name", "description") VALUES ('Jazz', 'Jazz is a type of music which was invented in the United States. Jazz music combines African-American music with European music. Some common jazz instruments include the saxoPhone, trumpet, piano, double bass, and drums.');
INSERT INTO Genres ("name", "description") VALUES ('Metal', 'Heavy Metal is a loud, aggressive style of Rock music. The bands who play heavy-metal music usually have one or two guitars, a bass guitar and drums. In some bands, electronic keyboards, organs, or other instruments are used. Heavy metal songs are loud and powerful-sounding, and have strong rhythms that are repeated. There are many different types of Heavy Metal, some of which are described below. Heavy metal bands sometimes dress in jeans, leather jackets, and leather boots, and have long hair. Heavy metal bands sometimes behave in a dramatic way when they play their instruments or sing. However, many heavy metal bands do not like to do this.');
INSERT INTO Genres ("name", "description") VALUES ('Alternative', 'Alternative rock is a type of rock music that became popular in the 1980s and became widely popular in the 1990s. Alternative rock is made up of various subgenres that have come out of the indie music scene since the 1980s, such as grunge, indie rock, Britpop, gothic rock, and indie pop. These genres are sorted by their collective types of punk, which laid the groundwork for alternative music in the 1970s.');
INSERT INTO Genres ("name", "description") VALUES ('Disco', 'Disco is a style of pop music that was popular in the mid-1970s. Disco music has a strong beat that people can dance to. People usually dance to disco music at bars called disco clubs. The word "disco" is also used to refer to the style of dancing that people do to disco music, or to the style of clothes that people wear to go disco dancing. Disco was at its most popular in the United States and Europe in the 1970s and early 1980s. Disco was brought into the mainstream by the hit movie Saturday Night Fever, which was released in 1977. This movie, which starred John Travolta, showed people doing disco dancing. Many radio stations played disco in the late 1970s.');
INSERT INTO Genres ("name", "description") VALUES ('Blues', 'The blues is a form of music that started in the United States during the start of the 20th century. It was started by former African slaves from "spirituals", praise songs, and chants. The first blues songs were called Delta blues. These songs came from "the" area near the mouth of the Mississippi River.');
INSERT INTO Genres ("name", "description") VALUES ('Latin', 'Latin American music is the music of all Countries in Latin America (and the Caribbean) and comes in many varieties. Latin America is home to musical styles such as the simple, rural conjunto music of northern Mexico, the sophisticated habanera of Cuba, the rhythmic sounds of the Puerto Rican plena, the symphonies of Heitor Villa-Lobos, and the simple and moving Andean flute. Music has played an important part recently in Latin America''s politics, the nueva canción movement being a prime example. Latin music is very diverse, with the only truly unifying thread being the use of Latin-derived languages, predominantly the Spanish language, the Portuguese language in Brazil, and to a lesser extent, Latin-derived creole languages, such as those found in Haiti.');
INSERT INTO Genres ("name", "description") VALUES ('Reggae', 'Reggae is a music genre first developed in Jamaica in the late 1960s. While sometimes used in a broader sense to refer to most types of Jamaican music, the term reggae more properly denotes a particular music style that originated following on the development of ska and rocksteady.');
INSERT INTO Genres ("name", "description") VALUES ('Pop', 'Pop music is a music genre that developed from "the" mid-1950s as a softer alternative to rock ''n'' roll and later to rock music. It has a focus on commercial recording, often oriented towards a youth market, usually through the medium of relatively short and simple love songs. While these basic elements of the genre have remained fairly constant, pop music has absorbed influences from "most" other forms of popular music, particularly borrowing from "the" development of rock music, and utilizing key technological innovations to produce new variations on existing themes.');
INSERT INTO Genres ("name", "description") VALUES ('Classical', 'Classical music is a very general term which normally refers to the standard music of Countries in the Western world. It is music that has been composed by musicians who are trained in the art of writing music (composing) and written down in music notation so that other musicians can play it. Classical music can also be described as "art music" because great art (skill) is needed to compose it and to perform it well. Classical music differs from "pop" music because it is not made just in order to be popular for a short time or just to be a commercial success.');



DROP TABLE IF EXISTS "Artists" CASCADE;

CREATE TABLE Artists (
    "id" SERIAL PRIMARY KEY NOT NULL,
    "name" varchar(120) NULL,
    "bio" varchar(4000) NULL
);

INSERT INTO Artists ("id", "name", "bio") VALUES (1, 'AC/DC', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (2, 'Accept', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (3, 'Aerosmith', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (4, 'Alanis Morissette', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (5, 'Alice In Chains', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (6, 'Antônio Carlos Jobim', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (7, 'Apocalyptica', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (8, 'Audioslave', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (10, 'Billy Cobham', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (11, 'Black Label Society', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (12, 'Black Sabbath', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (14, 'Bruce Dickinson', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (15, 'Buddy Guy', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (16, 'Caetano Veloso', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (17, 'Chico Buarque', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (18, 'Chico Science & Nação Zumbi', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (19, 'Cidade Negra', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (20, 'Cláudio Zoli', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (21, 'Various Artists', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (22, 'Led Zeppelin', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (23, 'Frank Zappa & Captain Beefheart', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (24, 'Marcos Valle', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (27, 'Gilberto Gil', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (37, 'Ed Motta', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (41, 'Elis Regina', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (42, 'Milton Nascimento', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (46, 'Jorge Ben', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (50, 'Metallica', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (51, 'Queen', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (52, 'Kiss', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (53, 'Spyro Gyra', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (55, 'David Coverdale', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (56, 'Gonzaguinha', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (58, 'Deep Purple', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (59, 'Santana', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (68, 'Miles Davis', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (72, 'Vinícius De Moraes', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (76, 'Creedence Clearwater Revival', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (77, 'Cássia Eller', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (79, 'Dennis Chambers', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (80, 'Djavan', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (81, 'Eric Clapton', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (82, 'Faith No More', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (83, 'Falamansa', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (84, 'Foo Fighters', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (86, 'Funk Como Le Gusta', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (87, 'Godsmack', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (88, 'Guns N'' Roses', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (89, 'Incognito', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (90, 'Iron Maiden', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (92, 'Jamiroquai', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (94, 'Jimi Hendrix', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (95, 'Joe Satriani', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (96, 'Jota Quest', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (98, 'Judas Priest', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (99, 'Legião Urbana', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (100, 'Lenny Kravitz', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (101, 'Lulu Santos', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (102, 'Marillion', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (103, 'Marisa Monte', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (105, 'Men At Work', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (106, 'Motörhead', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (109, 'Mötley Crüe', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (110, 'Nirvana', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (111, 'O Terço', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (112, 'Olodum', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (113, 'Os Paralamas Do Sucesso', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (114, 'Ozzy Osbourne', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (115, 'Page & Plant', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (117, 'Paul D''Ianno', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (118, 'Pearl Jam', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (120, 'Pink Floyd', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (124, 'R.E.M.', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (126, 'Raul Seixas', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (127, 'Red Hot Chili Peppers', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (128, 'Rush', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (130, 'Skank', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (132, 'Soundgarden', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (133, 'Stevie Ray Vaughan & Double Trouble', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (134, 'Stone Temple Pilots', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (135, 'System Of A Down', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (136, 'Terry Bozzio, Tony Levin & Steve Stevens', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (137, 'The Black Crowes', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (139, 'The Cult', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (140, 'The Doors', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (141, 'The Police', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (142, 'The Rolling Stones', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (144, 'The Who', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (145, 'Tim Maia', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (150, 'U2', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (151, 'UB40', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (152, 'Van Halen', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (153, 'Velvet Revolver', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (155, 'Zeca Pagodinho', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (157, 'Dread Zeppelin', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (179, 'Scorpions', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (196, 'Cake', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (197, 'Aisha Duo', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (200, 'The Posies', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (201, 'Luciana Souza/Romero Lubambo', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (202, 'Aaron Goldberg', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (203, 'Nicolaus Esterhazy Sinfonia', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (204, 'Temple of the Dog', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (205, 'Chris Cornell', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (206, 'Alberto Turco & Nova Schola Gregoriana', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (208, 'English Concert & Trevor Pinnock', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (211, 'Wilhelm Kempff', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (212, 'Yo-Yo Ma', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (213, 'Scholars Baroque Ensemble', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (217, 'Royal Philharmonic Orchestra & Sir Thomas Beecham', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (219, 'Britten Sinfonia, Ivor Bolton & Lesley Garrett', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (221, 'Sir Georg Solti & Wiener Philharmoniker', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (223, 'London Symphony Orchestra & Sir Charles Mackerras', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (224, 'Barry Wordsworth & BBC Concert Orchestra', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (226, 'Eugene Ormandy', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (229, 'Boston Symphony Orchestra & Seiji Ozawa', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (230, 'Aaron Copland & London Symphony Orchestra', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (231, 'Ton Koopman', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (232, 'Sergei Prokofiev & Yuri Temirkanov', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (233, 'Chicago Symphony Orchestra & Fritz Reiner', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (234, 'Orchestra of The Age of Enlightenment', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (236, 'James Levine', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (237, 'Berliner Philharmoniker & Hans Rosbaud', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (238, 'Maurizio Pollini', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (240, 'Gustav Mahler', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (242, 'Edo de Waart & San Francisco Symphony', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (244, 'Choir Of Westminster Abbey & Simon Preston', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (245, 'Michael Tilson Thomas & San Francisco Symphony', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (247, 'The King''s Singers', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (248, 'Berliner Philharmoniker & Herbert Von Karajan', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (250, 'Christopher O''Riley', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (251, 'Fretwork', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (252, 'Amy Winehouse', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (253, 'Calexico', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (255, 'Yehudi Menuhin', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (258, 'Les Arts Florissants & William Christie', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (259, 'The 12 Cellists of The Berlin Philharmonic', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (260, 'Adrian Leaper & Doreen de Feis', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (261, 'Roger Norrington, London Classical Players', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (264, 'Kent Nagano and Orchestre de l''Opéra de Lyon', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (265, 'Julian Bream', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (266, 'Martin Roscoe', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (267, 'Göteborgs Symfoniker & Neeme Järvi', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (270, 'Gerald Moore', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (271, 'Mela Tenenbaum, Pro Musica Prague & Richard Kapp', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (274, 'Nash Ensemble', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (276, 'Chic', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (277, 'Anita Ward', 'some bio description');
INSERT INTO Artists ("id", "name", "bio") VALUES (278, 'Donna Summer', 'some bio description');

DROP TABLE IF EXISTS "Albums" CASCADE;

CREATE TABLE Albums(
	"id" SERIAL PRIMARY KEY NOT NULL,
	genre_id int NOT NULL,
	artist_id int NOT NULL,
	"title" varchar(160) NOT NULL,
	"price" numeric(10, 2) NOT NULL,
	"thumbnail" varchar(1024) NULL CONSTRAINT DF_Album_Thumbnail  DEFAULT ('/placeholder.webp')
);

INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 1, 'For Those About To Rock We Salute You', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 1, 'Let There Be Rock', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 100, 'Greatest Hits', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 102, 'Misplaced Childhood', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 105, 'The Best Of Men At Work', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 110, 'Nevermind', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 111, 'Compositores', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 114, 'Bark at the Moon (Remastered)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 114, 'Blizzard of Ozz', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 114, 'Diary of a Madman (Remastered)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 114, 'No More Tears (Remastered)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 114, 'Speak of the Devil', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 115, 'Walking Into Clarksdale', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 117, 'The Beast Live', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 118, 'Live On Two Legs [Live]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 118, 'Riot Act', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 118, 'Ten', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 118, 'Vs.', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 120, 'Dark Side Of The Moon', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 124, 'New Adventures In Hi-Fi', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 126, 'Raul Seixas', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 127, 'By The Way', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 127, 'Californication', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 128, 'Retrospective I (1974-1980)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 130, 'Maquinarama', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 130, 'O Samba Poconé', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 132, 'A-Sides', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 134, 'Core', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 136, '[1997] Black Light Syndrome', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 139, 'Beyond Good And Evil', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 140, 'The Doors', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 141, 'The Police Greatest Hits', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 142, 'Hot Rocks, 1964-1971 (Disc 1)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 142, 'No Security', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 142, 'Voodoo Lounge', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 144, 'My Generation - The Very Best Of The Who', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 150, 'Achtung Baby', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 150, 'B-Sides 1980-1990', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 150, 'How To Dismantle An Atomic Bomb', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 150, 'Pop', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 150, 'Rattle And Hum', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 150, 'The Best Of 1980-1990', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 150, 'War', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 150, 'Zooropa', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 152, 'Diver Down', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 152, 'The Best Of Van Halen, Vol. I', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 152, 'Van Halen III', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 152, 'Van Halen', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 153, 'Contraband', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 157, 'Un-Led-Ed', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 2, 'Balls to the Wall', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 2, 'Restless and Wild', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 200, 'Every Kind of Light', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 22, 'BBC Sessions [Disc 1] [Live]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 22, 'BBC Sessions [Disc 2] [Live]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 22, 'Coda', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 22, 'Houses Of The Holy', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 22, 'In Through The Out Door', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 22, 'IV', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 22, 'Led Zeppelin I', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 22, 'Led Zeppelin II', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 22, 'Led Zeppelin III', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 22, 'Physical Graffiti [Disc 1]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 22, 'Physical Graffiti [Disc 2]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 22, 'Presence', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 22, 'The Song Remains The Same (Disc 1)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 22, 'The Song Remains The Same (Disc 2)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 23, 'Bongo Fury', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 3, 'Big Ones', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 4, 'Jagged Little Pill', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 5, 'Facelift', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 51, 'Greatest Hits I', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 51, 'Greatest Hits II', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 51, 'News Of The World', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 52, 'Greatest Kiss', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 52, 'Unplugged [Live]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 55, 'Into The Light', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 58, 'Come Taste The Band', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 58, 'Deep Purple In Rock', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 58, 'Fireball', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 58, 'Machine Head', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 58, 'MK III The Final Concerts [Disc 1]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 58, 'Purpendicular', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 58, 'Slaves And Masters', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 58, 'Stormbringer', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 58, 'The Battle Rages On', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 58, 'The Final Concerts (Disc 2)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 59, 'Santana - As Years Go By', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 59, 'Santana Live', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 59, 'Supernatural', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 76, 'Chronicle, Vol. 1', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 76, 'Chronicle, Vol. 2', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 8, 'Audioslave', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 82, 'King For A Day Fool For A Lifetime', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 84, 'In Your Honor [Disc 1]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 84, 'In Your Honor [Disc 2]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 84, 'The Colour And The Shape', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 88, 'Appetite for Destruction', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 88, 'Use Your Illusion I', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 90, 'A Matter of Life and Death', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 90, 'Brave New World', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 90, 'Fear Of The Dark', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 90, 'Live At Donington 1992 (Disc 1)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 90, 'Live At Donington 1992 (Disc 2)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 90, 'Rock In Rio [CD2]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 90, 'The Number of The Beast', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 90, 'The X Factor', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 90, 'Virtual XI', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 92, 'Emergency On Planet Earth', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 94, 'Are You Experienced?', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (1, 95, 'Surfing with the Alien (Remastered)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 203, 'The Best of Beethoven', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 208, 'Pachelbel: Canon & Gigue', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 211, 'Bach: Goldberg Variations', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 212, 'Bach: The Cello Suites', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 213, 'Handel: The Messiah (Highlights)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 217, 'Haydn: Symphonies 99 - 104', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 219, 'A Soprano Inspired', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 221, 'Wagner: Favourite Overtures', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 223, 'Tchaikovsky: The Nutcracker', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 224, 'The Last Night of the Proms', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 226, 'Respighi:Pines of Rome', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 226, 'Strauss: Waltzes', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 229, 'Carmina Burana', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 230, 'A Copland Celebration, Vol. I', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 231, 'Bach: Toccata & Fugue in D Minor', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 232, 'Prokofiev: Symphony No.1', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 233, 'Scheherazade', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 234, 'Bach: The Brandenburg Concertos', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 236, 'Mascagni: Cavalleria Rusticana', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 237, 'Sibelius: Finlandia', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 242, 'Adams, John: The Chairman Dances', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 245, 'Berlioz: Symphonie Fantastique', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 245, 'Prokofiev: Romeo & Juliet', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 247, 'English Renaissance', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 248, 'Mozart: Symphonies Nos. 40 & 41', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 250, 'SCRIABIN: Vers la flamme', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 255, 'Bartok: Violin & Viola Concertos', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 259, 'South American Getaway', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 260, 'Górecki: Symphony No. 3', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 261, 'Purcell: The Fairy Queen', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 264, 'Weill: The Seven Deadly Sins', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 266, 'Szymanowski: Piano Works, Vol. 1', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 267, 'Nielsen: The Six Symphonies', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (10, 274, 'Mozart: Chamber Music', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (2, 10, 'The Best Of Billy Cobham', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (2, 197, 'Quiet Songs', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (2, 202, 'Worlds', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (2, 27, 'Quanta Gente Veio ver--Bônus De Carnaval', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (2, 53, 'Heart of the Night', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (2, 53, 'Morning Dance', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (2, 6, 'Warner 25 Anos', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (2, 68, 'Miles Ahead', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (2, 68, 'The Essential Miles Davis [Disc 1]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (2, 68, 'The Essential Miles Davis [Disc 2]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (2, 79, 'Outbreak', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (2, 89, 'Blue Moods', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 100, 'Greatest Hits', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 106, 'Ace Of Spades', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 109, 'Motley Crue Greatest Hits', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 11, 'Alcohol Fueled Brewtality Live! [Disc 1]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 11, 'Alcohol Fueled Brewtality Live! [Disc 2]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 114, 'Tribute', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 12, 'Black Sabbath Vol. 4 (Remaster)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 12, 'Black Sabbath', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 135, 'Mezmerize', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 14, 'Chemical Wedding', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 50, '...And Justice For All', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 50, 'Black Album', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 50, 'Garage Inc. (Disc 1)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 50, 'Garage Inc. (Disc 2)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 50, 'Load', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 50, 'Master Of Puppets', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 50, 'ReLoad', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 50, 'Ride The Lightning', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 50, 'St. Anger', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 7, 'Plays Metallica By Four Cellos', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 87, 'Faceless', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 88, 'Use Your Illusion II', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 90, 'A Real Dead One', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 90, 'A Real Live One', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 90, 'Live After Death', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 90, 'No Prayer For The Dying', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 90, 'Piece Of Mind', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 90, 'Powerslave', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 90, 'Rock In Rio [CD1]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 90, 'Rock In Rio [CD2]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 90, 'Seventh Son of a Seventh Son', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 90, 'Somewhere in Time', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 90, 'The Number of The Beast', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (3, 98, 'Living After Midnight', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (4, 196, 'Cake: B-Sides and Rarities', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (4, 204, 'Temple of the Dog', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (4, 205, 'Carry On', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (4, 253, 'Carried to Dust (Bonus Track Version)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (4, 8, 'Revelations', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (6, 133, 'In Step', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (6, 137, 'Live [Disc 1]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (6, 137, 'Live [Disc 2]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (6, 81, 'The Cream Of Clapton', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (6, 81, 'Unplugged', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (6, 90, 'Iron Maiden', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 103, 'Barulhinho Bom', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 112, 'Olodum', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 113, 'Acústico MTV', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 113, 'Arquivo II', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 113, 'Arquivo Os Paralamas Do Sucesso', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 145, 'Serie Sem Limite (Disc 1)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 145, 'Serie Sem Limite (Disc 2)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 155, 'Ao Vivo [IMPORT]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 16, 'Prenda Minha', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 16, 'Sozinho Remix Ao Vivo', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 17, 'Minha Historia', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 18, 'Afrociberdelia', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 18, 'Da Lama Ao Caos', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 20, 'Na Pista', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 201, 'Duos II', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 21, 'Sambas De Enredo 2001', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 21, 'Vozes do MPB', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 24, 'Chill: Brazil (Disc 1)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 27, 'Quanta Gente Veio Ver (Live)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 37, 'The Best of Ed Motta', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 41, 'Elis Regina-Minha História', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 42, 'Milton Nascimento Ao Vivo', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 42, 'Minas', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 46, 'Jorge Ben Jor 25 Anos', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 56, 'Meus Momentos', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 6, 'Chill: Brazil (Disc 2)', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 72, 'Vinicius De Moraes', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 77, 'Cássia Eller - Sem Limite [Disc 1]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 80, 'Djavan Ao Vivo - Vol. 02', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 80, 'Djavan Ao Vivo - Vol. 1', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 81, 'Unplugged', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 83, 'Deixa Entrar', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 86, 'Roda De Funk', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 96, 'Jota Quest-1995', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (7, 99, 'Mais Do Mesmo', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (8, 100, 'Greatest Hits', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (8, 151, 'UB40 The Best Of - Volume Two [UK]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (8, 19, 'Acústico MTV [Live]', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (8, 19, 'Cidade Negra - Hits', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (9, 21, 'Axé Bahia 2001', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (9, 252, 'Frank', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (5, 276, 'Le Freak', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (5, 278, 'MacArthur Park Suite', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');
INSERT INTO Albums ("genre_id", "artist_id", "title", "price", "thumbnail") VALUES (5, 277, 'Ring My Bell', CAST(8.99 AS Numeric(10, 2)), '/placeholder.webp');

DROP TABLE IF EXISTS "Orders";

CREATE TABLE Orders(
	"id" SERIAL PRIMARY KEY NOT NULL,
	"date" timestamptz NOT NULL,
	"username" varchar(50) NULL,
	"firstname" varchar(160) NULL,
	"lastname" varchar(160) NULL,
	"address" varchar(70) NULL,
	"city" varchar(40) NULL,
	"state" varchar(40) NULL,
	"postal_code" varchar(10) NULL,
	"country" varchar(40) NULL,
	"phone" varchar(24) NULL,
	"email" varchar(200) NULL,
	"total" numeric(10, 2) NOT NULL
);

DROP TABLE IF EXISTS "OrderDetails";

CREATE TABLE OrderDetails(
	"id" SERIAL PRIMARY KEY NOT NULL,
	"order_id" int NOT NULL,
	"album_id" int NOT NULL,
	"quantity" int NOT NULL,
	"unit_price" numeric(10, 2) NOT NULL
);

DROP TABLE IF EXISTS "Carts";

CREATE TABLE Carts(
	"id" SERIAL PRIMARY KEY NOT NULL,
	"cart_id" varchar(50) NOT NULL,
	"album_id" int NOT NULL,
	"count" int NOT NULL,
	"date_created" timestamptz NOT NULL
);

DROP TABLE IF EXISTS "Users";

CREATE TABLE Users(
	"id" SERIAL PRIMARY KEY NOT NULL,
	"username" varchar(50) NOT NULL,
	"email" varchar(200) NOT NULL,
	"password" varchar(200) NOT NULL,
	"role" varchar(50) NOT NULL
);

--Password is admin
INSERT INTO Users ("username", "email", "password", "role") VALUES ('admin', 'admin@example@com', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 'admin');



-- views
CREATE VIEW AlbumDetails
as
select a."id", a."thumbnail", a."price", a."title", at."name" as "artist", g."name" as "genre"
from Albums a
         join Artists at on at."id" = a."artist_id"
join "genres" g on g."id" = a."genre_id";

CREATE VIEW CartDetails
as
select c."cart_id", c."count", a."title" as "album_title", a."id" as "album_id", a."price"
from Carts c
         join Albums a on c."album_id" = a."id";

CREATE VIEW Bestsellers
as
select a."id", a."title", a."thumbnail", Count(*) as "count"
from Albums as a
         inner join OrderDetails as o on a."id" = o."album_id"
group by a."id", a."title", a."thumbnail"
order by "count" DESC
    LIMIT 5;