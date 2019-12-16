using BndrScoreRecorder.common.entity;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BndrScoreRecorder.common
{
    class MusicDao
    {
        // logger
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // 接続文字列
        public SqliteConnectionStringBuilder builder;

        // Error int value
        public static readonly int ERROR_INTEGER = -1;

        // Error long value
        public static readonly long ERROR_LONG = -1L;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="dataSourcePath">接続先ファイル名を指定</param>
        public MusicDao(string dataSourcePath)
        {
            logger.Info("Dao datasource string = " + dataSourcePath);

            builder = new SqliteConnectionStringBuilder
            {
                DataSource = dataSourcePath
            };

            // Check file exists
            if (File.Exists(dataSourcePath) == false)
            {
                logger.Info("Datasource file not found, create new database.");

                // database access section
                using (SqliteConnection connection = new SqliteConnection(builder.ToString()))
                {
                    // connection open
                    connection.Open();

                    // enable transaction
                    using (SqliteTransaction transaction = connection.BeginTransaction())
                    using (SqliteCommand command = connection.CreateCommand())
                    {
                        // SQL
                        command.CommandText = "CREATE TABLE M_MUSIC ("
                            + "	id	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,"
                            + "	title	TEXT,"
                            + "	difficult	TEXT,"
                            + "	level	INTEGER,"
                            + "	insert_date	TEXT,"
                            + "	update_date	TEXT"
                            + ")";

                        // query to log
                        logger.Info(command.CommandText);

                        // execute
                        command.ExecuteNonQuery();

                        // SQL
                        command.CommandText = "CREATE TABLE T_OCRREAD_MUSIC_LINK ("
                            + "	music_id	TEXT NOT NULL,"
                            + "	hashed_ocr_data	TEXT NOT NULL"
                            + ")";

                        // query to log
                        logger.Info(command.CommandText);

                        // execute
                        command.ExecuteNonQuery();

                        // SQL
                        command.CommandText = "CREATE TABLE T_SCORE_RECORD ("
                            + "	id	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,"
                            + "	music_id	INTEGER NOT NULL,"
                            + "	perfect	INTEGER,"
                            + "	great	INTEGER,"
                            + "	good	INTEGER,"
                            + "	bad	INTEGER,"
                            + "	miss	INTEGER,"
                            + "	total_notes	INTEGER,"
                            + "	max_combo	INTEGER,"
                            + "	ex_score	INTEGER,"
                            + " score	    INTEGER,"
                            + "	image_file_path	TEXT,"
                            + "	insert_date	TEXT,"
                            + "	update_date	TEXT"
                            + ")";

                        // query to log
                        logger.Info(command.CommandText);

                        // execute
                        command.ExecuteNonQuery();

                        // commit
                        transaction.Commit();
                    }
                }

                logger.Info("Create table complete.");
            }
        }

        /// <summary>
        /// 楽曲一覧を検索し、返却する。スコアデータは含まれない。
        /// </summary>
        /// <returns>楽曲一覧</returns>
        public List<Music> selectMusicList()
        {
            List<Music> musicList = new List<Music>();

            logger.Info("Music dao select music list start.");

            // database access section
            using (SqliteConnection connection = new SqliteConnection(builder.ToString()))
            {
                // connection open
                connection.Open();

                // enable transaction
                using (SqliteTransaction transaction = connection.BeginTransaction())
                using (SqliteCommand command = connection.CreateCommand())
                {
                    // Section.1 - select M_MUSIC
                    // SQL
                    command.CommandText = "SELECT id, title, difficult, level FROM M_MUSIC ORDER BY level desc";

                    // query to log
                    logger.Info(command.CommandText);

                    // check music is still exists?
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            Music music = new Music
                            {
                                id = GetInt(0, reader),
                                title = GetString(1, reader),
                                difficult = GetString(2, reader),
                                level = GetInt(3, reader)
                            };
                            musicList.Add(music);
                        }
                    }
                    // clear parameters
                    command.Parameters.Clear();
                }
            }

            logger.Info("Music dao select music list end.");

            return musicList;
        }

        /// <summary>
        /// OCRデータをもとに楽曲マスタと、それに紐づくOCRデータ、および、スコアデータを検索
        /// </summary>
        /// <param name="musicId">楽曲マスタのID</param>
        /// <returns>IDに一致するMusicオブジェクト</returns>
        public Music selectByHashedOcrData(string hashedOcrData)
        {
            // return value
            Music music = null;

            // Music id value
            int? musicId = null;

            logger.Info("Music dao select by Hashed OCR Data start.");
            logger.Info("Hashed OCR Dat = " + hashedOcrData);

            // database access section
            using (SqliteConnection connection = new SqliteConnection(builder.ToString()))
            {
                // connection open
                connection.Open();

                // enable transaction
                using (SqliteTransaction transaction = connection.BeginTransaction())
                using (SqliteCommand command = connection.CreateCommand())
                {
                    // Select Music ID from Hashed OCR Data
                    // SQL
                    command.CommandText = "SELECT music_id, hashed_ocr_data FROM T_OCRREAD_MUSIC_LINK WHERE hashed_ocr_data = @hashed_ocr_data";

                    // query to log
                    logger.Info(command.CommandText);

                    // prepared statement
                    command.Parameters.AddWithValue("hashed_ocr_data", hashedOcrData);

                    // check music is still exists?
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            musicId = GetInt(0, reader);
                            logger.Info("Selected music id = " + musicId);
                        }
                    }
                    // clear parameters
                    command.Parameters.Clear();
                }
            }

            // if no selected record, return null
            if (musicId == null)
            {
                logger.Info("Music ID is not selected, return null.");
            }
            else
            {
                logger.Info("Music ID is selected, search data. Call another function.");
                music = selectByMusicId(musicId);
            }

            logger.Info("Music dao select by id end.");

            return music;
        }

        /// <summary>
        /// 曲名、Level、難易度をもとに楽曲マスタと、それに紐づくOCRデータ、および、スコアデータを検索
        /// </summary>
        /// <param name="musicId">楽曲マスタのID</param>
        /// <returns>IDに一致するMusicオブジェクト</returns>
        public Music selectByTitleDifficultLevel(string title, int level, string difficult)
        {
            // return value
            Music music = null;

            // Music id value
            int? musicId = null;

            logger.Info("Music dao select by title & level & difficult start.");
            logger.Info("Title = " + title + ", Level = " + level + ", Difficult = " + difficult);

            // database access section
            using (SqliteConnection connection = new SqliteConnection(builder.ToString()))
            {
                // connection open
                connection.Open();

                // enable transaction
                using (SqliteTransaction transaction = connection.BeginTransaction())
                using (SqliteCommand command = connection.CreateCommand())
                {
                    // Select Music ID from Hashed OCR Data
                    // SQL
                    command.CommandText = "SELECT id, title, level, difficult FROM M_MUSIC WHERE title = @title AND level = @level AND difficult = @difficult";

                    // query to log
                    logger.Info(command.CommandText);

                    // prepared statement
                    command.Parameters.AddWithValue("title", title);
                    command.Parameters.AddWithValue("level", level);
                    command.Parameters.AddWithValue("difficult", difficult);

                    // check music is still exists?
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            musicId = GetInt(0, reader);
                            logger.Info("Selected music id = " + musicId);
                        }
                    }
                    // clear parameters
                    command.Parameters.Clear();
                }
            }

            // if no selected record, return null
            if (musicId == null)
            {
                logger.Info("Music ID is not selected, return null.");
            }
            else
            {
                logger.Info("Music ID is selected, search data. Call another function.");
                music = selectByMusicId(musicId);
            }

            logger.Info("Music dao select by title & level & difficult end.");

            return music;
        }

        /// <summary>
        /// Music IDをもとに楽曲マスタと、それに紐づくOCRデータ、および、スコアデータを検索
        /// </summary>
        /// <param name="musicId">楽曲マスタのID</param>
        /// <returns>IDに一致するMusicオブジェクト</returns>
        public Music selectByMusicId(int? musicId)
        {
            // return value
            Music music = null;

            logger.Info("Music dao select by music id start.");
            logger.Info("Id = " + musicId);

            // Null check
            if (musicId == null)
            {
                logger.Info("Music id is null. No data returned.");
                return music;
            }

            // database access section
            using (SqliteConnection connection = new SqliteConnection(builder.ToString()))
            {
                // connection open
                connection.Open();

                // enable transaction
                using (SqliteTransaction transaction = connection.BeginTransaction())
                using (SqliteCommand command = connection.CreateCommand())
                {
                    // Section.1 - select M_MUSIC
                    // SQL
                    command.CommandText = "SELECT title, difficult, level FROM M_MUSIC WHERE id = @id";

                    // query to log
                    logger.Info(command.CommandText);

                    // prepared statement
                    command.Parameters.AddWithValue("id", musicId);

                    // check music is still exists?
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            music = new Music
                            {
                                id = musicId,
                                title = GetString(0, reader),
                                difficult = GetString(1, reader),
                                level = GetInt(2, reader)
                            };
                        }
                    }
                    // clear parameters
                    command.Parameters.Clear();

                    // if no selected record, return null
                    if (music == null)
                    {
                        return music;
                    }

                    // Section.2 - hashed OCR data list
                    // SQL
                    command.CommandText = "SELECT MM.id, TOML.hashed_ocr_data FROM M_MUSIC MM INNER JOIN T_OCRREAD_MUSIC_LINK TOML ON MM.id = TOML.music_id WHERE MM.id = @id;";

                    // query to log
                    logger.Info(command.CommandText);

                    // prepared statement
                    command.Parameters.AddWithValue("id", musicId);

                    // check music is still exists?
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            music.hashedOcrDataList.Add(GetString(1, reader));
                        }
                    }
                    // clear parameters
                    command.Parameters.Clear();

                    // Section.3 - select T_SCORE_RECORD
                    // SQL
                    command.CommandText = "SELECT id, perfect, great, good, bad, miss, total_notes, max_combo, ex_score, score, image_file_path FROM T_SCORE_RECORD WHERE music_id = @id ORDER BY update_date desc;";

                    // query to log
                    logger.Info(command.CommandText);

                    // prepared statement
                    command.Parameters.AddWithValue("id", music.id);

                    // check music is still exists?
                    ScoreResult scoreReuslt;
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            scoreReuslt = new ScoreResult();


                            scoreReuslt.id = GetString(0, reader);
                            scoreReuslt.perfect = GetLong(1, reader);
                            scoreReuslt.great = GetLong(2, reader);
                            scoreReuslt.good = GetLong(3, reader);
                            scoreReuslt.bad = GetLong(4, reader);
                            scoreReuslt.miss = GetLong(5, reader);
                            scoreReuslt.totalNotes = GetLong(6, reader);
                            scoreReuslt.maxCombo = GetLong(7, reader);
                            scoreReuslt.exScore = GetLong(8, reader);
                            scoreReuslt.score = GetLong(9, reader);
                            scoreReuslt.imageFilePath = GetString(10, reader);

                            scoreReuslt.CalculateInfos();

                            music.scoreResultList.Add(scoreReuslt);
                        }
                    }
                    // clear parameters
                    command.Parameters.Clear();
                }
            }

            logger.Info("Music dao select by music id end.");

            return music;
        }

        /// <summary>
        /// Musicオブジェクトが既に登録済みかを確認する。
        /// </summary>
        /// <param name="music">Musicオブジェクト</param>
        /// <returns>true:登録済み、false:未登録</returns>
        public bool IsMusicExists(Music music)
        {
            // return value initialized with false
            bool result = false;

            logger.Info("Music dao exists check start.");
            logger.Info("Target music data = " + Music.ToJsonString(music));

            // database access section
            using (SqliteConnection connection = new SqliteConnection(builder.ToString()))
            {
                // connection open
                connection.Open();

                // enable transaction
                using (SqliteTransaction transaction = connection.BeginTransaction())
                using (SqliteCommand command = connection.CreateCommand())
                {
                    // check file is exists
                    command.CommandText = "SELECT * FROM M_MUSIC WHERE id = @id";

                    // query to log
                    logger.Info(command.CommandText);

                    // prepared statement
                    command.Parameters.AddWithValue("id", music.id);

                    // check music is still exists?
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            result = true;
                        }
                    }
                    // clear parameters
                    command.Parameters.Clear();
                }
            }

            logger.Info("Check result = " + result);
            logger.Info("Music dao exists check end.");

            return result;
        }

        /// <summary>
        /// 楽曲マスタとスコアデータを登録する。
        /// </summary>
        /// <param name="music">Musicオブジェクト</param>
        /// <returns>true:登録成功、false：登録失敗</returns>
        public bool InsertOrReplace(Music music)
        {
            // return value initialized with false
            bool result = false;

            logger.Info("Music dao insert or replace start.");
            logger.Info("Target music data = " + Music.ToJsonString(music));

            // database access section
            using (SqliteConnection connection = new SqliteConnection(builder.ToString()))
            {
                // connection open
                connection.Open();

                // enable transaction
                using (SqliteTransaction transaction = connection.BeginTransaction())
                using (SqliteCommand command = connection.CreateCommand())
                {
                    /** Section.1 - Music data **/
                    logger.Info("Section.1 music data insert/replace start.");

                    // If music.id == null -> insert, else -> update
                    if (music.id == null)
                    {
                        logger.Info("Music master data is exists, insert music master data.");

                        // SQL
                        command.CommandText = "INSERT INTO M_MUSIC(title, difficult, level, insert_date, update_date) VALUES (@title, @difficult, @level, datetime('now', 'localtime'), datetime('now', 'localtime'));";

                        // query to log
                        logger.Info(command.CommandText);

                        // prepared statement
                        command.Parameters.AddWithValue("difficult", music.difficult);
                        command.Parameters.AddWithValue("title", music.title);
                        command.Parameters.AddWithValue("level", music.level);

                        // execute
                        command.ExecuteNonQuery();

                        // clear parameters
                        command.Parameters.Clear();

                        logger.Info("Get music id.");

                        // SQL
                        command.CommandText = "SELECT id FROM M_MUSIC WHERE rowid = last_insert_rowid();";

                        // query to log
                        logger.Info(command.CommandText);

                        // check music is still exists?
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                music.id = GetInt(0, reader);
                            }
                        }

                        // Inserted Music id check
                        if (music.id == null)
                        {
                            logger.Error("Can't get inserted music id!");
                            result = false;
                            return result;
                        }
                        else
                        {
                            logger.Info("Inserted music id = " + music.id);
                        }

                        // clear parameters
                        command.Parameters.Clear();
                    }
                    else
                    {
                        logger.Info("Music master data is exists, update music master data. Music id = " + music.id);

                        // SQL
                        command.CommandText = "UPDATE M_MUSIC SET title = @title, difficult = @difficult, level = @level, update_date = datetime('now', 'localtime') WHERE id = @id;";

                        // query to log
                        logger.Info(command.CommandText);

                        // prepared statement
                        command.Parameters.AddWithValue("id", music.id);
                        command.Parameters.AddWithValue("difficult", music.difficult);
                        command.Parameters.AddWithValue("title", music.title);
                        command.Parameters.AddWithValue("level", music.level);

                        // execute
                        command.ExecuteNonQuery();

                        // clear parameters
                        command.Parameters.Clear();
                    }

                    logger.Info("Section 1. music data insert/replace end.");

                    /** Section.2 - Score data**/
                    logger.Info("Section 2. score data insert start.");

                    foreach (ScoreResult scoreResult in music.scoreResultList)
                    {
                        // If scoreReuslt.id == null -> insert, else == update
                        if (scoreResult.id == null)
                        {
                            logger.Info("Score result data is exists, insert score result data.");

                            // SQL
                            command.CommandText = "INSERT INTO T_SCORE_RECORD(music_id, perfect, great, good, bad, miss, total_notes, max_combo, ex_score, score, image_file_path, insert_date, update_date) VALUES (@music_id, @perfect, @great, @good, @bad, @miss, @total_notes, @max_combo, @ex_score, @score, @image_file_path, datetime('now', 'localtime'), datetime('now', 'localtime'));";

                            // query to log
                            logger.Info(command.CommandText);

                            logger.Info("Target data = title : " + music.title + ", ex_score : " + scoreResult.exScore);
                            command.Parameters.AddWithValue("music_id", music.id);
                            command.Parameters.AddWithValue("perfect", scoreResult.perfect);
                            command.Parameters.AddWithValue("great", scoreResult.great);
                            command.Parameters.AddWithValue("good", scoreResult.good);
                            command.Parameters.AddWithValue("bad", scoreResult.bad);
                            command.Parameters.AddWithValue("miss", scoreResult.miss);
                            command.Parameters.AddWithValue("total_notes", scoreResult.totalNotes);
                            command.Parameters.AddWithValue("max_combo", scoreResult.maxCombo);
                            command.Parameters.AddWithValue("ex_score", scoreResult.exScore);
                            command.Parameters.AddWithValue("score", scoreResult.score);
                            command.Parameters.AddWithValue("image_file_path", scoreResult.imageFilePath);

                            // execute
                            command.ExecuteNonQuery();

                            // clear parameters
                            command.Parameters.Clear();
                        }
                        else
                        {
                            logger.Info("Score result data is exists, update Score result data. Score Result id = " + scoreResult.id);

                            // SQL
                            command.CommandText = "UPDATE T_SCORE_RECORD SET perfect = @perfect, great = @great, good = @good, bad = @bad, miss = @miss, total_notes = @total_notes, max_combo = @max_combo, ex_score = @ex_score, score = @score, image_file_path = @image_file_path WHERE id = @id;";

                            // query to log
                            logger.Info(command.CommandText);
                            logger.Info("Target data = id : " + scoreResult.id + ", ex_score : " + scoreResult.exScore);
                            command.Parameters.AddWithValue("perfect", scoreResult.perfect);
                            command.Parameters.AddWithValue("great", scoreResult.great);
                            command.Parameters.AddWithValue("good", scoreResult.good);
                            command.Parameters.AddWithValue("bad", scoreResult.bad);
                            command.Parameters.AddWithValue("miss", scoreResult.miss);
                            command.Parameters.AddWithValue("total_notes", scoreResult.totalNotes);
                            command.Parameters.AddWithValue("max_combo", scoreResult.maxCombo);
                            command.Parameters.AddWithValue("ex_score", scoreResult.exScore);
                            command.Parameters.AddWithValue("score", scoreResult.score);
                            command.Parameters.AddWithValue("image_file_path", scoreResult.imageFilePath);
                            command.Parameters.AddWithValue("id", scoreResult.id);

                            // execute
                            if (command.ExecuteNonQuery() == 1)
                            {
                                logger.Info("Update result is ok.");
                            }
                            else
                            {
                                logger.Info("Update result NG!");
                                throw new SqliteException("Update count is ng, not 1.", 5000);
                            }

                            // clear parameters
                            command.Parameters.Clear();
                        }
                    }

                    logger.Info("Section 2. score data insert end.");

                    /** Section.3 Update Hashed OCR data **/
                    logger.Info("Section 3. hashed ocr data insert start.");

                    if (music.hashedOcrData == null ||
                        music.hashedOcrData == string.Empty ||
                        music.hashedOcrDataList.Contains(music.hashedOcrData) == true)
                    {
                        logger.Info("No need to insert hashed ocr data.");
                    }
                    else
                    {
                        // SQL
                        command.CommandText = "INSERT INTO T_OCRREAD_MUSIC_LINK(music_id, hashed_ocr_data) VALUES (@music_id, @hashed_ocr_data);";

                        // query to log
                        logger.Info(command.CommandText);

                        // prepared statement
                        command.Parameters.AddWithValue("music_id", music.id);
                        command.Parameters.AddWithValue("hashed_ocr_data", music.hashedOcrData);

                        // execute
                        command.ExecuteNonQuery();

                        // clear parameters
                        command.Parameters.Clear();

                        // update music object
                        music.hashedOcrDataList.Add(music.hashedOcrData);
                    }

                    // commit
                    transaction.Commit();

                    // successful all command
                    result = true;
                }
            }

            logger.Info("Music dao insert or replace end.");

            return result;
        }

        /// <summary>
        /// 対象のスコアリザルトを削除する。
        /// </summary>
        /// <param name="scoreResult">削除対象のスコアリザルト</param>
        /// <returns>true:登録成功、false：登録失敗</returns>
        public bool DeleteScoreResult(ScoreResult scoreResult)
        {
            // return value initialized with false
            bool result = false;

            logger.Info("Music dao insert or replace start.");
            logger.Info("Target score data id = " + scoreResult.id);

            // database access section
            using (SqliteConnection connection = new SqliteConnection(builder.ToString()))
            {
                // connection open
                connection.Open();

                // enable transaction
                using (SqliteTransaction transaction = connection.BeginTransaction())
                using (SqliteCommand command = connection.CreateCommand())
                {
                    // SQL
                    command.CommandText = "DELETE FROM T_SCORE_RECORD WHERE @id = id;";

                    // query to log
                    logger.Info(command.CommandText);

                    // prepared statement
                    command.Parameters.AddWithValue("id", scoreResult.id);

                    // execute
                    if (command.ExecuteNonQuery() == 1)
                    {
                        logger.Info("Delete result is ok.");
                    }
                    else
                    {
                        logger.Info("Delete result NG!");
                        throw new SqliteException("Delete count is ng, not 1.", 5000);
                    }

                    // clear parameters
                    command.Parameters.Clear();

                    // commit
                    transaction.Commit();

                    // successful all command
                    result = true;
                }
            }

            return result;
        }

        public string GetString(int index, SqliteDataReader reader)
        {
            return reader.IsDBNull(index) ? string.Empty : reader.GetString(index);
        }

        public int GetInt(int index, SqliteDataReader reader)
        {
            return reader.IsDBNull(index) ? ERROR_INTEGER : reader.GetInt32(index);
        }

        public long GetLong(int index, SqliteDataReader reader)
        {
            return reader.IsDBNull(index) ? ERROR_LONG : reader.GetInt64(index);
        }
    }
}
