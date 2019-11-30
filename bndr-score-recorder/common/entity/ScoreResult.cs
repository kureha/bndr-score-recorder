using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BndrScoreRecorder.common.entity
{
    [DataContract]
    public class ScoreResult
    {
        // logger
        protected static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // raw score spliter
        private static readonly string[] CHAR_RAW_SCORE_LIST_SPLITER = { "\n" };

        // line number define
        private static readonly int LINE_NUM_PERFECT = 0;
        private static readonly int LINE_NUM_GREAT = 1;
        private static readonly int LINE_NUM_GOOD = 2;
        private static readonly int LINE_NUM_BAD = 3;
        private static readonly int LINE_NUM_MISS = 4;

        // initialize count value
        public static readonly long ERROR_COUNT = -1;

        // perfect
        [DataMember]
        public long perfect;

        // great
        [DataMember]
        public long great;

        // good
        [DataMember]
        public long good;

        // bad
        [DataMember]
        public long bad;

        // miss
        [DataMember]
        public long miss;

        // total notes
        [DataMember]
        public long totalNotes;

        // max combo
        [DataMember]
        public long maxCombo;

        // score
        [DataMember]
        public long score;

        // ex score
        [DataMember]
        public long exScore;

        // rank code : CodeMaster.RankCodeMaster
        [DataMember]
        public long rankCode;

        // screenshot image file path
        public string imageFilePath;

        /// <summary>
        /// 文字列をもとにScoreResultを生成
        /// </summary>
        /// <param name="rawScoreString">Tesseractで読み取ったScoreResult文字列</param>
        /// <param name="imageFilePath">読み取り元画像ファイルパス</param>
        /// <returns>ScoreResultオブジェクト</returns>
        public static ScoreResult Parse(string rawScoreString, string imageFilePath)
        {
            logger.Info("ScoreResult parse start. rawScoreString = " + rawScoreString);

            // result
            ScoreResult scoreResult = new ScoreResult();

            // split raw string and convert to list
            string[] rawScoreArray = rawScoreString.Split(CHAR_RAW_SCORE_LIST_SPLITER, StringSplitOptions.RemoveEmptyEntries);
            List<string> rawScoreList = new List<string>();
            rawScoreList.AddRange(rawScoreArray);

            logger.Info("RawString convert to RawList success. List size = " + rawScoreList.Count);

            scoreResult.perfect = ExtractScore(rawScoreArray[LINE_NUM_PERFECT]);
            scoreResult.great = ExtractScore(rawScoreArray[LINE_NUM_GREAT]);
            scoreResult.good = ExtractScore(rawScoreArray[LINE_NUM_GOOD]);
            scoreResult.bad = ExtractScore(rawScoreArray[LINE_NUM_BAD]);
            scoreResult.miss = ExtractScore(rawScoreArray[LINE_NUM_MISS]);

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

            // calculate ex score
            scoreResult.exScore = scoreResult.perfect * 2 + scoreResult.great;

            // calculate total notes
            scoreResult.totalNotes = scoreResult.perfect
                + scoreResult.great
                + scoreResult.good
                + scoreResult.bad
                + scoreResult.miss;

            // image file path
            scoreResult.imageFilePath = imageFilePath;

            // result
            return scoreResult;
        }

        /// <summary>
        /// 文字列を空白で分割し、末尾の文字列をスコアとみなしてLong型で返却する。
        /// </summary>
        /// <param name="rawScoreString">スコア文字列</param>
        /// <returns>スコア値</returns>
        private static long ExtractScore(string rawScoreString)
        {
            try
            {
                return long.Parse(rawScoreString.Trim());
            } catch (Exception e)
            {
                logger.Error("Score parse error. raw score string = " + rawScoreString);
                logger.Error(e);
                return ERROR_COUNT;
            }
        }

        /// <summary>
        /// 初期処理を実施し、エラー値を代入する。
        /// </summary>
        public ScoreResult()
        {
            perfect = ERROR_COUNT;
            great = ERROR_COUNT;
            good = ERROR_COUNT;
            bad = ERROR_COUNT;
            miss = ERROR_COUNT;

            maxCombo = ERROR_COUNT;
            score = ERROR_COUNT;
            rankCode = ERROR_COUNT;
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
            if (perfect == ERROR_COUNT || 
                great == ERROR_COUNT || 
                good == ERROR_COUNT || 
                bad == ERROR_COUNT || 
                miss == ERROR_COUNT)
            {
                result = false;
            }

            return result;
        }
    }
}
