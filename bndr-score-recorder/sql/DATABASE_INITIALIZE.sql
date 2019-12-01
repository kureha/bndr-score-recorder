DROP TABLE IF EXISTS "M_MUSIC";
CREATE TABLE "M_MUSIC" (
	"id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"title"	TEXT,
	"difficult"	TEXT,
	"level"	INTEGER,
	"insert_date"	TEXT,
	"update_date"	TEXT
);

DROP TABLE IF EXISTS "T_SCORE_RECORD";
CREATE TABLE "T_SCORE_RECORD" (
	"id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"music_id"	INTEGER NOT NULL,
	"perfect"	INTEGER,
	"great"	INTEGER,
	"good"	INTEGER,
	"bad"	INTEGER,
	"miss"	INTEGER,
	"total_notes"	INTEGER,
	"max_combo"	INTEGER,
	"ex_score"	INTEGER,
	"image_file_path"	TEXT,
	"insert_date"	TEXT,
	"update_date"	TEXT
);

DROP TABLE IF EXISTS "T_OCRREAD_MUSIC_LINK";
CREATE TABLE "T_OCRREAD_MUSIC_LINK" (
	"music_id"	TEXT NOT NULL,
	"hashed_ocr_data"	TEXT NOT NULL
);
