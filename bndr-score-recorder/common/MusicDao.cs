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

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="dataSourcePath">接続先ファイル名を指定</param>
        public MusicDao(string dataSourcePath)
        {
            logger.Info("Dao datasource string = " + dataSourcePath);

            if (File.Exists(dataSourcePath) == false)
            {
                string errorMessage = "Datasource file not found";
                logger.Error(errorMessage);
                throw new FileNotFoundException(errorMessage);
            }

            builder = new SqliteConnectionStringBuilder
            {
                DataSource = dataSourcePath
            };
        }

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

                    // check file is exists
                    command.CommandText = "SELECT * FROM M_MUSIC WHERE id = @id";

                    // query to log
                    logger.Info(command.CommandText);

                    // prepared statement
                    command.Parameters.AddWithValue("id", music.id);

                    // check music is still exists?
                    bool isMusicExists = false;
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            isMusicExists = true;
                        }
                    }
                    // clear parameters
                    command.Parameters.Clear();

                    if (isMusicExists == true)
                    {
                        logger.Info("Music master data is exists, skip insert music master data.");
                    } else
                    {
                        // SQL
                        command.CommandText = "INSERT INTO M_MUSIC(id, title, level, insert_date, update_date) VALUES (@id, @title, @level, datetime('now', 'localtime'), datetime('now', 'localtime'));";

                        // query to log
                        logger.Info(command.CommandText);

                        // prepared statement
                        command.Parameters.AddWithValue("id", music.id);
                        command.Parameters.AddWithValue("title", music.title);
                        command.Parameters.AddWithValue("level", music.level);

                        // execute
                        command.ExecuteNonQuery();

                        // clear parameters
                        command.Parameters.Clear();
                    }

                    logger.Info("Section 1. music data insert/replace end.");

                    logger.Info("Section 2. score data insert start.");

                    // SQL
                    command.CommandText = "INSERT INTO T_SCORE_RECORD(music_id, perfect, great, good, bad, miss, max_combo, ex_score, insert_date, update_date) VALUES (@music_id, @perfect, @great, @good, @bad, @miss, @max_combo, @ex_score, datetime('now', 'localtime'), datetime('now', 'localtime'));";

                    // query to log
                    logger.Info(command.CommandText);

                    foreach(ScoreResult scoreResult in music.scoreResultList)
                    {
                        logger.Info("Target data = title : " + music.title + ", ex_score : " + scoreResult.exScore);
                        command.Parameters.AddWithValue("music_id", music.id);
                        command.Parameters.AddWithValue("perfect", scoreResult.perfect);
                        command.Parameters.AddWithValue("great", scoreResult.great);
                        command.Parameters.AddWithValue("good", scoreResult.good);
                        command.Parameters.AddWithValue("bad", scoreResult.bad);
                        command.Parameters.AddWithValue("miss", scoreResult.miss);
                        command.Parameters.AddWithValue("max_combo", scoreResult.maxCombo);
                        command.Parameters.AddWithValue("ex_score", scoreResult.exScore);

                        // execute
                        command.ExecuteNonQuery();
                    }

                    /** Section.2 - Score data**/
                    logger.Info("Section 2. score data insert end.");

                    // commit
                    transaction.Commit();

                    // successful all command
                    result = true;
                }
            }

            logger.Info("Music dao insert or replace end.");

            return result;
        }
    }
}
