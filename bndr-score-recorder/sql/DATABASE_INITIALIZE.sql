DROP TABLE IF EXISTS "M_MUSIC";
CREATE TABLE "M_MUSIC" (
	"id"	TEXT NOT NULL,
	"title"	TEXT,
	"difficult"	TEXT,
	"level"	INTEGER,
	"insert_date"	TEXT,
	"update_date"	TEXT,
	PRIMARY KEY("id")
)
;

DROP TABLE IF EXISTS "T_SCORE_RECORD";
CREATE TABLE "T_SCORE_RECORD" (
	"id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"music_id"	TEXT NOT NULL,
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
)
;
