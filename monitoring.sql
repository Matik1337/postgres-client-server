--
-- PostgreSQL database dump
--

-- Dumped from database version 16.2
-- Dumped by pg_dump version 16.2

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: employee; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.employee (
    id integer NOT NULL,
    first_name character varying(50),
    last_name character varying(50)
);


ALTER TABLE public.employee OWNER TO postgres;

--
-- Name: loader; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.loader (
    id integer NOT NULL,
    machine_name character varying(50)
);


ALTER TABLE public.loader OWNER TO postgres;

--
-- Name: shift_employee; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.shift_employee (
    id integer NOT NULL,
    employee_id integer,
    pos_x real,
    pos_y real,
    moment timestamp without time zone
);


ALTER TABLE public.shift_employee OWNER TO postgres;

--
-- Name: shift_loader; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.shift_loader (
    id integer NOT NULL,
    employee_id integer,
    loader_id integer,
    is_loading boolean,
    pos_x real,
    pos_y real,
    moment timestamp without time zone
);


ALTER TABLE public.shift_loader OWNER TO postgres;

--
-- Name: shift_tipper; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.shift_tipper (
    id integer NOT NULL,
    employee_id integer,
    tipper_id integer,
    ore_type character varying,
    ore_count integer,
    pos_x real,
    pos_y real,
    moment timestamp without time zone
);


ALTER TABLE public.shift_tipper OWNER TO postgres;

--
-- Name: tipper; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tipper (
    id integer NOT NULL,
    machine_name character varying(50),
    payload integer
);


ALTER TABLE public.tipper OWNER TO postgres;

--
-- Data for Name: employee; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.employee (id, first_name, last_name) FROM stdin;
2	Vince	Lenchenko
3	Raynard	Shapcott
4	Alleen	Plank
5	Harmonie	Wenden
6	Coriss	Eykel
7	Dixie	Huckleby
8	Val	Sighard
9	Ealasaid	Crofts
10	Celie	Wealleans
11	Alex	Cundy
12	Danna	Chaunce
13	Adella	Jinkins
14	Lazarus	Audenis
15	Zolly	De Roberto
16	Nadia	Pahler
17	Drona	McKinie
18	Malcolm	McLarnon
19	Jaime	Steward
20	Catie	Poone
21	Xylina	Lowdiane
22	Derrek	Stebles
23	Pepillo	Tedorenko
24	Gerald	Churching
25	Constancia	Olfert
26	Lemuel	Beyn
27	Charmane	Janic
28	Henri	Plowright
29	Jasen	O'Loughnan
30	Bernita	Devas
31	Shaughn	Epinay
32	Phyllis	Macrae
33	Karney	Chipping
34	Melli	McIsaac
35	Garrick	Clackers
36	Jessi	Puckrin
37	Carl	Chinery
38	L;urette	McKeady
39	Christi	Jesper
40	Martie	Lantuff
41	Renato	Doget
42	Winonah	Labern
43	Caryl	Brusle
44	Trista	Buller
45	Gal	Elph
46	Ephraim	Nolleau
47	Tildi	Rizzone
48	Kally	Belden
49	Arvie	Giovanni
50	Pace	Terris
51	Zara	Bridgen
52	Kaycee	Meiklem
53	Arlena	Carlon
54	Levey	Terne
55	Even	Greville
56	Peggie	Heinemann
57	Huey	Cunnell
58	Bram	Pavolillo
59	Pauletta	Posselow
60	Maribelle	Leve
61	Lorry	Mc Gaughey
62	Otha	Winyard
63	Lavinia	Mundall
64	Virgie	Cordeix
65	Dorice	Cerith
66	Allister	Strangeway
67	Dyann	Bradneck
68	Claiborne	Pheasant
69	Claribel	Flaubert
70	Riane	Satch
71	Toni	Trinbey
72	Stacy	Ondra
73	Darsie	Simms
74	Zora	MacTerrelly
75	Kerr	Tildesley
76	Dora	Robb
77	Emlynne	Ziehm
78	Nanon	Dericot
79	Ingunna	Abelwhite
80	Sal	Allix
81	Julianna	Vater
82	Lyle	Andreia
83	Lorine	Twoohy
84	Boycie	Ferrettini
85	Romola	Reeson
86	Jody	Bresman
87	Jacquenette	Standage
88	Barbee	Teale
89	Franciskus	Daintry
90	Ellette	Huntingdon
91	Roma	Boschmann
92	Shandie	Gerson
93	Mack	Ricker
94	Dodi	Burnage
95	Odetta	Roskrug
96	Ludovika	Bassett
97	Grethel	Bucknill
98	Donielle	Flintoft
99	Sydel	Ventom
100	Zerk	Cripwell
101	Philbert	Dimbleby
102	Merline	Bewick
103	Danny	Pache
104	Hans	Spillard
105	Reece	Garlick
106	Adriano	Hutsby
107	Jaclin	MacLure
108	Marty	Yakushkin
109	Bebe	Croft
110	Starlene	Camus
111	Marleah	Orsman
112	Zaccaria	Laurentin
113	Dahlia	Duffit
114	Julie	Soar
115	Gianni	Collaton
116	Neille	Ferrick
117	Sabine	Ashleigh
118	Gerhardt	Franz-Schoninger
119	Anna-maria	Halwill
120	Bonnie	Hardingham
121	Brandie	Fortnum
122	Adelaide	Atlee
123	Alvie	Larciere
124	Jerry	Chattoe
125	Darsie	Coldbreath
126	Erhard	Toseland
127	Tara	McCrie
128	Cyrus	Bennetts
129	Karine	Boal
130	Hildy	Garrow
131	Richy	Faulconer
132	Marice	Boultwood
133	Florian	McPhilip
134	Estella	Jerzycowski
135	Lodovico	Houchen
136	Avis	Trunkfield
137	Nonie	Sandilands
138	Sebastian	Eassom
139	Bernadina	Kleuer
140	Rudy	Karlolak
141	Lydie	Stuchbery
142	Kelly	Althorpe
143	Aksel	Whetnell
144	Colas	Himpson
145	Sherry	Pinney
146	Efrem	Ruane
147	Willey	Baldack
148	Graig	Duddin
149	Karole	Footitt
150	Denise	Brunini
151	Nefen	Mayberry
152	Fay	Edsell
153	Sayres	O'Cannon
154	Adelind	Withey
155	Arlee	Vezey
156	Charles	Yendall
157	Malcolm	Pierrepont
158	Kizzie	Radmer
159	Kristi	Grange
160	Debra	Wibrow
161	Otes	Swayte
162	Donnie	Huebner
163	Lissi	Kinnen
164	Louella	O'Dyvoie
165	Marlo	Quimby
166	Florence	Westmacott
167	Billy	Stilgo
168	Alic	Bedells
169	Sharia	Bonanno
170	Marget	Carpenter
171	Griswold	Fellgate
172	Web	De Filippis
173	Lowrance	Sambrok
174	Ainslie	Karolyi
175	Sheilakathryn	Weir
176	Donielle	Stuckes
177	Claretta	Ortzen
178	Mirilla	Room
179	Gerik	Milne
180	Zenia	Mongenot
181	Lindy	Hargreaves
182	Davon	Tinker
183	Shea	Lavis
184	Marguerite	Barensen
185	Atalanta	D'Enrico
186	Margot	Decker
187	Lettie	Frankiewicz
188	Vina	Caron
189	Arte	MacGoun
190	Alyson	Marquot
191	Janela	Ech
192	Harriett	Whether
193	Jerrylee	Joriot
194	Gardie	Tedder
195	Forrester	Spellard
196	Marja	Suermeier
197	Francklin	Melbert
198	Collin	Wigan
199	Paulina	Studd
200	Malorie	Trustrie
201	Cam	Burnett
202	Gerhard	Romain
203	Gib	Crease
204	Caz	Lunam
205	Mommy	Steely
206	Tonnie	Fant
207	Monique	Harbin
208	Woody	Ceaplen
209	Evanne	McRill
210	Erinna	Grubb
211	Gwenny	Wadman
212	Oneida	Girardez
213	Val	Kimberly
214	Albertine	Raggles
215	Yard	Huckin
216	Heinrick	Olijve
217	Maddy	Kunneke
218	Sheffie	Tweedell
219	Bob	Kohrs
220	Kristy	Storrie
221	Shane	Hassen
222	Horatio	Oldred
223	Trevor	Taig
224	Loydie	Garrioch
225	Lezlie	Noseworthy
226	Royal	Shinn
227	Bobbee	Tanby
228	Sansone	McAlindon
229	Jacquie	Himsworth
230	Thatch	Hanvey
231	Dewey	Axelby
232	Carlynn	Espadate
233	Beatriz	Fairburn
234	Markus	Gunter
235	Lanita	Malkie
236	Auria	Comelli
237	Atalanta	Abraham
238	Jane	Pywell
239	Cristal	Martinec
240	Moreen	Rowat
241	Fayina	Claige
242	Wayne	Chant
243	Davis	Meeke
244	Vachel	Broddle
245	Christan	Dowbiggin
246	L;urette	Cadreman
247	Frankie	Dibden
248	Tate	Whitehorn
249	Ludvig	Vallens
250	Noe	Hutchence
251	Clemens	Dilkes
252	Marven	Gravener
253	Emmerich	Wadly
254	Hillard	Eddisforth
255	Fitz	Goalby
256	Maritsa	Ausello
257	Anatollo	Philip
258	Kenton	Mobbs
259	Xever	Larciere
260	Vivi	Gocke
261	Carlynne	Brigshaw
262	Casie	Haryngton
263	Clarisse	Galilee
264	Nahum	Talks
265	Julina	Ghelardoni
266	Augustine	Twittey
267	Myrwyn	Cannop
268	Margy	Rosita
269	Ailbert	Brabyn
270	Sayre	Dangerfield
271	Hilde	Hall-Gough
272	Elane	Dinesen
273	Sasha	Sarfati
274	Lawry	Ferier
275	Free	McGlashan
276	Helga	Blaymires
277	Thurstan	Morena
278	Sutton	Flinn
279	D'arcy	Buffery
280	Hobey	Elwin
281	Hilario	McIan
282	Jessalyn	Eliasson
283	Binky	Minihan
284	Freddie	Eadon
285	Noellyn	Peete
286	Sollie	Troughton
287	Jonathan	McNeice
288	Bertrando	Janodet
289	Pamela	Mallinder
290	Orel	Kulicke
291	Kay	Vedenisov
292	Reinaldo	Sherland
293	Barron	Coull
294	Julissa	Street
295	Raina	McPaike
296	Cly	Bremeyer
297	Charles	Leason
298	Hieronymus	Danilowicz
299	Caresa	Cristofaro
300	Barris	Caldwall
301	Pippo	Signorelli
302	Evelyn	Kencott
303	Chen	Wholesworth
304	Hamish	Surgey
305	Carlene	Clowsley
306	Sean	Labro
307	Fanya	Praten
308	Teresa	Zannolli
309	Lemar	Aland
310	Iago	Headford
311	Inna	Eyles
312	Rhett	Lowdes
313	Carlin	Greggs
314	Noellyn	Hill
315	Kimmi	Feige
316	Slade	Grimble
317	Minne	Swalough
318	Yancey	Water
319	Janela	Hassall
320	Heddi	McClymond
321	Rodie	Comoletti
322	Gaynor	Burge
323	Jessika	Lidgley
324	Mariam	Clavey
325	Bibbye	Billingsley
326	Ginny	Esterbrook
327	Marita	Sommerly
328	Izaak	Vignaux
329	Ronni	Polleye
330	Tuck	Balbeck
331	Lewiss	Medendorp
332	Skipton	Benka
333	Winnifred	Mackrell
334	Jamie	Robens
335	Tanney	Garbert
336	Marcella	Stead
337	Carol-jean	Clixby
338	Rolando	Bixley
339	Astra	Merioth
340	Rollo	Kinnen
341	Audrie	Czaja
342	Cathee	Trahmel
343	Lucita	Yarrall
344	Siward	Annett
345	Cassy	Leonard
346	Felipe	Lyddiard
347	Renelle	Izsak
348	Chalmers	Northcote
349	Crichton	Gue
350	Brok	Gresswell
351	Niels	Inseal
352	Rebeka	Popland
353	Maxwell	Teenan
354	Adele	Butterly
355	Kissiah	Hawkins
356	Tristam	Gooble
357	Oates	McMurdo
358	Heidi	Moralas
359	Iggie	MacMenemy
360	Gunter	Hogben
361	Gottfried	Radwell
362	Solomon	Padefield
363	Pauly	Morphey
364	Fannie	Larrington
365	Trixie	Gerhts
366	Ranice	Creelman
367	Ly	Halloran
368	Christie	Pratt
369	Hercule	Maroney
370	Alex	Eamer
371	Dena	Circuit
372	Ed	Prescot
373	Stacee	Coo
374	Mel	Tuckett
375	Gweneth	Bernardt
376	Rorke	Roderigo
377	Roselia	Dessent
378	Malinde	Arnefield
379	Lottie	Catherall
380	Monika	Perico
381	Mona	Punt
382	Corbett	Chamberlayne
383	Imogene	Dake
384	Salome	Bleasdale
385	Elfrida	Eskell
386	Sile	Braiden
387	Alayne	Schimpke
388	Farlay	Large
389	Josefina	Marvell
390	Valentin	Dalgetty
391	Edy	Rioch
392	Analiese	Assaf
393	Meagan	Shalloo
394	Perice	Zanazzi
395	Andra	Valde
396	Bria	Nobes
397	Sandye	Milella
398	Alayne	Britton
399	Corrinne	Liptrod
400	Deeyn	Valencia
401	Deni	Theseira
402	Ashleigh	Scaplehorn
403	Stu	Micheli
404	Ynes	Dallicoat
405	Florella	Hartegan
406	Merilee	Faulo
407	Carola	Brislane
408	Tedra	Wasylkiewicz
409	Shena	Dredge
410	Far	Fredi
411	Rennie	Ruffell
412	Jackqueline	Bogeys
413	Farlie	Wetherald
414	Pietra	Eddies
415	Konstantine	Giacomoni
416	Bernita	Piper
417	Harald	Swaffield
418	Brandtr	Hiom
419	Peder	Lade
420	Dewie	Rockliffe
421	Tulley	Climie
422	Nichole	Bonwell
423	Cristobal	Gasgarth
424	Kirstyn	Stanwix
425	Jermaine	Rudman
426	Ransell	Treleaven
427	Christel	Corlett
428	Pavlov	Mosdell
429	Maude	Holberry
430	Alf	Hannant
431	Carlota	Lyttle
432	Beatrisa	Berthot
433	Annabal	Verrick
434	Mae	Gavigan
435	Diena	Cranidge
436	Cortney	Simoni
437	Randall	Wimsett
438	Guthrie	Kinny
439	Trstram	Tembridge
440	Jodie	Cattermull
441	Keslie	Ivens
442	Gustaf	Marklew
443	Arch	Lampitt
444	Brunhilda	Dowry
445	Sosanna	Ivancevic
446	Hildegaard	Davidovsky
447	Selinda	Hafford
448	Fredrick	Autin
449	Rosco	Hesse
450	Stavro	Startin
451	Ximenes	Stuttard
452	Malvin	Quaif
453	Andreana	Kirvell
454	Linda	Herreran
455	Jacklin	Birley
456	Nolana	Beeken
457	Jessee	Dalbey
458	Sinclair	Gyorgy
459	Jaye	Cordel
460	Claire	McKelloch
461	Payton	Prettjohn
462	Velvet	Cesco
463	Bidget	Duchasteau
464	Alfons	Gallamore
465	Sheila-kathryn	Bourgeois
466	Lynette	Engelmann
467	Gillan	Scrange
468	Alexis	Bever
469	Dom	Stennett
470	Boony	Attfield
471	Francine	Barsam
472	Cindy	MacAnulty
473	Anastassia	Attewill
474	Gillie	Odde
475	Lewie	Mercer
476	Lavinie	Macilhench
477	Hilton	Mival
478	Hyacinthe	Paraman
479	Debor	Reddick
480	Stanislaw	Sackur
481	Barret	Greenig
482	Hazel	Hunsworth
483	Valera	Poole
484	Keeley	Moreside
485	Rollie	Vacher
486	Janelle	Mantione
487	Falito	Ottee
488	Bucky	Lougheed
489	Amalita	MacFayden
490	Fulvia	De Michetti
491	Shaylah	Scarlon
492	Antonietta	Carson
493	Link	Perazzo
494	Sharleen	Iannazzi
495	Ewart	Braycotton
496	Inna	Swancott
497	Matthew	Polotti
498	Lorin	Purdom
499	Angie	Geldeard
500	Easter	Middlehurst
\.


--
-- Data for Name: loader; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.loader (id, machine_name) FROM stdin;
1	РџР”Рњ CAT
2	РџР”Рњ CAT
3	РџР”Рњ CAT
4	РџР”Рњ CAT
5	РџР”Рњ CAT
6	РџР”Рњ CAT
7	РџР”Рњ CAT
8	РџР”Рњ CAT
9	РџР”Рњ CAT
10	РџР”Рњ CAT
11	РџР”Рњ CAT
12	РџР”Рњ CAT
13	РџР”Рњ CAT
14	РџР”Рњ CAT
15	РџР”Рњ CAT
16	РџР”Рњ Р‘РµР»РђР·
17	РџР”Рњ Р‘РµР»РђР·
18	РџР”Рњ Р‘РµР»РђР·
19	РџР”Рњ Р‘РµР»РђР·
20	РџР”Рњ Р‘РµР»РђР·
21	РџР”Рњ Р‘РµР»РђР·
22	РџР”Рњ Р‘РµР»РђР·
23	РџР”Рњ Р‘РµР»РђР·
24	РџР”Рњ Р‘РµР»РђР·
25	РџР”Рњ Р‘РµР»РђР·
26	РџР”Рњ Р‘РµР»РђР·
27	РџР”Рњ Р‘РµР»РђР·
28	РџР”Рњ Р‘РµР»РђР·
29	РџР”Рњ Р‘РµР»РђР·
30	РџР”Рњ Р‘РµР»РђР·
\.


--
-- Data for Name: shift_employee; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.shift_employee (id, employee_id, pos_x, pos_y, moment) FROM stdin;
1	321	765	954	2024-04-24 11:30:35
2	123	456	366	2024-04-24 11:30:35
3	333	543	436	2024-04-24 11:30:35
4	444	736	347	2024-04-24 11:30:35
5	222	257	857	2024-04-24 11:30:35
6	111	456	97	2024-04-24 11:30:35
7	112	523	365	2024-04-24 11:30:35
8	113	456	436	2024-04-24 11:30:35
9	114	642	876	2024-04-24 11:30:35
10	115	984	45	2024-04-24 11:30:35
11	116	377	567	2024-04-24 11:30:35
12	117	246	24	2024-04-24 11:30:35
13	118	265	436	2024-04-24 11:30:35
14	119	926	367	2024-04-24 11:30:35
15	221	156	870	2024-04-24 11:30:35
16	223	894	234	2024-04-24 11:30:35
\.


--
-- Data for Name: shift_loader; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.shift_loader (id, employee_id, loader_id, is_loading, pos_x, pos_y, moment) FROM stdin;
1	321	1	t	563	865	2024-04-24 11:30:35
2	314	2	f	957	285	2024-04-24 11:30:35
3	144	3	f	185	447	2024-04-24 11:30:35
4	155	5	t	836	954	2024-04-24 11:30:35
5	166	18	f	186	954	2024-04-24 11:30:35
6	177	19	f	437	465	2024-04-24 11:30:35
7	188	21	t	849	154	2024-04-24 11:30:35
\.


--
-- Data for Name: shift_tipper; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.shift_tipper (id, employee_id, tipper_id, ore_type, ore_count, pos_x, pos_y, moment) FROM stdin;
2	321	2	SomeOreType	12	957	285	2024-04-24 11:30:35
3	322	3	SomeOreType	11	185	447	2024-04-24 11:30:35
4	323	5	SomeOreType	0	836	954	2024-04-24 11:30:35
5	324	18	SomeOreType	0	186	954	2024-04-24 11:30:35
6	325	19	SomeOreType	0	437	465	2024-04-24 11:30:35
7	426	21	SomeOreType	16	849	154	2024-04-24 11:30:35
\.


--
-- Data for Name: tipper; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tipper (id, machine_name, payload) FROM stdin;
1	Р‘РµР»РђР·	90
2	Р‘РµР»РђР·	90
3	Р‘РµР»РђР·	90
4	Р‘РµР»РђР·	90
5	Р‘РµР»РђР·	90
6	Р‘РµР»РђР·	90
7	Р‘РµР»РђР·	90
8	Р‘РµР»РђР·	90
9	Р‘РµР»РђР·	90
10	Р‘РµР»РђР·	70
11	Р‘РµР»РђР·	70
12	Р‘РµР»РђР·	70
13	Р‘РµР»РђР·	70
14	Р‘РµР»РђР·	70
15	Р‘РµР»РђР·	70
16	Р‘РµР»РђР·	70
17	Р‘РµР»РђР·	70
18	Р‘РµР»РђР·	40
19	Р‘РµР»РђР·	40
20	Р‘РµР»РђР·	40
21	Р‘РµР»РђР·	40
22	Р‘РµР»РђР·	40
23	Р‘РµР»РђР·	40
24	Р‘РµР»РђР·	40
25	Р‘РµР»РђР·	40
26	Р‘РµР»РђР·	40
27	Р‘РµР»РђР·	40
28	Р‘РµР»РђР·	40
29	Р‘РµР»РђР·	40
30	Р‘РµР»РђР·	40
\.


--
-- Name: employee employee_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.employee
    ADD CONSTRAINT employee_pkey PRIMARY KEY (id);


--
-- Name: loader loader_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.loader
    ADD CONSTRAINT loader_pkey PRIMARY KEY (id);


--
-- Name: shift_employee shift_employee_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shift_employee
    ADD CONSTRAINT shift_employee_pkey PRIMARY KEY (id);


--
-- Name: shift_loader shift_loader_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shift_loader
    ADD CONSTRAINT shift_loader_pkey PRIMARY KEY (id);


--
-- Name: shift_tipper shift_tipper_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shift_tipper
    ADD CONSTRAINT shift_tipper_pkey PRIMARY KEY (id);


--
-- Name: tipper tipper_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tipper
    ADD CONSTRAINT tipper_pkey PRIMARY KEY (id);


--
-- Name: shift_employee shift_employee_employee_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shift_employee
    ADD CONSTRAINT shift_employee_employee_id_fkey FOREIGN KEY (employee_id) REFERENCES public.employee(id);


--
-- Name: shift_loader shift_loader_employee_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shift_loader
    ADD CONSTRAINT shift_loader_employee_id_fkey FOREIGN KEY (employee_id) REFERENCES public.employee(id);


--
-- Name: shift_loader shift_loader_loader_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shift_loader
    ADD CONSTRAINT shift_loader_loader_id_fkey FOREIGN KEY (loader_id) REFERENCES public.loader(id);


--
-- Name: shift_tipper shift_tipper_employee_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shift_tipper
    ADD CONSTRAINT shift_tipper_employee_id_fkey FOREIGN KEY (employee_id) REFERENCES public.employee(id);


--
-- Name: shift_tipper shift_tipper_tipper_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shift_tipper
    ADD CONSTRAINT shift_tipper_tipper_id_fkey FOREIGN KEY (tipper_id) REFERENCES public.tipper(id);


--
-- PostgreSQL database dump complete
--

