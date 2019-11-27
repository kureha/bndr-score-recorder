using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bndr_score_recorder.common.entity
{
    class ScoreResult
    {
        // logger
        protected static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // raw score spliter
        private static readonly string[] CHAR_RAW_SCORE_LIST_SPLITER = { "\n" };

        // literals
        public static readonly string LITERAL_PERFECT = "PERFECT";
        public static readonly string LITERAL_GREAT = "GREAT";
        public static readonly string LITERAL_GOOD = "GOOD";
        public static readonly string LITERAL_BAD = "BAD";
        public static readonly string LITERAL_MISS = "MISS";

        // initialize count value
        public static readonly long ERROR_COUNT = -1;

        // perfect
        public long perfect;

        // great
        public long great;

        // good
        public long good;

        // bad
        public long bad;

        // miss
        public long miss;

        // max combo
        public long maxCombo;

        // score
        public long score;

        // rank code : CodeMaster.RankCodeMaster
        public long rankCode;

        /// <summary>
        /// 文字列をもとにScoreResultを生成
        /// </summary>
        /// <param name="rawScoreString">Tesseractで読み取ったScoreResult文字列</param>
        /// <returns>ScoreResultオブジェクト</returns>
        public static ScoreResult Parse(string rawScoreString)
        {
            logger.Info("ScoreResult parse start. rawScoreString = " + rawScoreString);

            // result
            ScoreResult scoreResult = new ScoreResult();

            // split raw string and convert to list
            string[] rawScoreArray = rawScoreString.Split(CHAR_RAW_SCORE_LIST_SPLITER, StringSplitOptions.RemoveEmptyEntries);
            List<string> rawScoreList = new List<string>();
            rawScoreList.AddRange(rawScoreArray);

            logger.Info("RawString convert to RawList success. List size = " + rawScoreList.Count);

            // scan rawScore and insert to return object
            rawScoreList.ForEach(rawScore => {
                if (rawScore.Contains(LITERAL_PERFECT) == true)
                {
                    logger.Info("RawList contains perfect data. raw = " + rawScore);
                    scoreResult.perfect = int.Parse(rawScore.Replace(LITERAL_PERFECT, string.Empty).Trim());
                }
                else if (rawScore.Contains(LITERAL_GREAT) == true)
                {
                    logger.Info("RawList contains great data. raw = " + rawScore);
                    scoreResult.great = int.Parse(rawScore.Replace(LITERAL_GREAT, string.Empty).Trim());
                }
                else if (rawScore.Contains(LITERAL_GOOD) == true)
                {
                    logger.Info("RawList contains good data. raw = " + rawScore);
                    scoreResult.good = int.Parse(rawScore.Replace(LITERAL_GOOD, string.Empty).Trim());
                }
                else if (rawScore.Contains(LITERAL_BAD) == true)
                {
                    logger.Info("RawList contains bad data. raw = " + rawScore);
                    scoreResult.bad = int.Parse(rawScore.Replace(LITERAL_BAD, string.Empty).Trim());
                }
                else if (rawScore.Contains(LITERAL_MISS) == true)
                {
                    logger.Info("RawList contains miss data. raw = " + rawScore);
                    scoreResult.miss = int.Parse(rawScore.Replace(LITERAL_MISS, string.Empty).Trim());
                }
            });

            // check score is valid?
            if (scoreResult.IsValidScore())
            {
                logger.Info("ScoreResult parse succeeded.");
            } 
            else
            {
                string errorMessage = "ScoreResult parse failed.";
                logger.Error(errorMessage);
                throw new FormatException(errorMessage);
            }

            // result
            return scoreResult;
        }

        /// <summary>
        /// 初期処理を実施し、エラー値を代入する。
        /// </summary>
        public ScoreResult()
        {
            this.perfect = ERROR_COUNT;
            this.great = ERROR_COUNT;
            this.good = ERROR_COUNT;
            this.bad = ERROR_COUNT;
            this.miss = ERROR_COUNT;

            this.maxCombo = ERROR_COUNT;
            this.score = ERROR_COUNT;
            this.rankCode = ERROR_COUNT;
        }

        /// <summary>
        /// スコアにエラー値が残っていないか検査するメソッド。
        /// </summary>
        /// <returns>true:成功、false:エラー値が存在</returns>
        public bool IsValidScore()
        {
            // return value (initialize true)
            bool result = true;

            // if contains "-1", this is error object.
            if (this.perfect == ERROR_COUNT || 
                this.great == ERROR_COUNT || 
                this.good == ERROR_COUNT || 
                this.bad == ERROR_COUNT || 
                this.miss == ERROR_COUNT)
            {
                result = false;
            }

            return result;
        }
    }
}
