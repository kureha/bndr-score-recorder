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
        private const int LINE_NUM_PERFECT = 0;
        private const int LINE_NUM_GREAT = 1;
        private const int LINE_NUM_GOOD = 2;
        private const int LINE_NUM_BAD = 3;
        private const int LINE_NUM_MISS = 4;

        // initialize count value
        public static readonly long ERROR_COUNT = -1;

        // clear type define
        public static readonly string CLEAR_TYPE_FULL_COMBO = "FULL COMBO";
        public static readonly string CLEAR_TYPE_HARD_CLEAR = "HARD CLEAR";
        public static readonly string CLEAR_TYPE_NORMAL_CLEAR = "NORMAL CLEAR";
        public static readonly string CLEAR_TYPE_EASY_CLEAR = "EASY CLEAR";

        // id
        [DataMember]
        public string id;

        // ex score
        [DataMember]
        public long exScore { set; get; }

        // max combo
        [DataMember]
        public long maxCombo { set; get; }

        // perfect
        [DataMember]
        public long perfect { set; get; }

        // great
        [DataMember]
        public long great { set; get; }

        // good
        [DataMember]
        public long good { set; get; }

        // bad
        [DataMember]
        public long bad { set; get; }

        // miss
        [DataMember]
        public long miss { set; get; }

        // total notes
        [DataMember]
        public long totalNotes { set; get; }

        // score
        [DataMember]
        public long score { set; get; }

        // rank code : CodeMaster.RankCodeMaster
        [DataMember]
        public long rankCode;

        // screenshot image file path
        public string imageFilePath;

        // clear type
        [DataMember]
        public string clearType { set; get; }

        /// <summary>
        /// 文字列をもとにScoreResultを生成する。
        /// </summary>
        /// <param name="rawResultNotesString">Tesseractで読み取ったResultNotes文字列</param>
        /// <param name="imageFilePath">読み取り元画像ファイルパス</param>
        /// <returns>ScoreResultオブジェクト</returns>
        public static ScoreResult Parse(string rawResultNotesString, string imageFilePath)
        {
            logger.Info("ScoreResult parse start. rawResultNotesString = " + rawResultNotesString);

            // result
            ScoreResult scoreResult = new ScoreResult();

            // split raw string and convert to list
            string[] rawScoreArray = rawResultNotesString.Split(CHAR_RAW_SCORE_LIST_SPLITER, StringSplitOptions.RemoveEmptyEntries);
            List<string> rawScoreList = new List<string>();
            rawScoreList.AddRange(rawScoreArray);

            logger.Info("RawString convert to RawList success. List size = " + rawScoreList.Count);

            // insert data if line is enable
            try
            {
                scoreResult.perfect = ExtractResultNotes(rawScoreArray[LINE_NUM_PERFECT]);
                scoreResult.great = ExtractResultNotes(rawScoreArray[LINE_NUM_GREAT]);
                scoreResult.good = ExtractResultNotes(rawScoreArray[LINE_NUM_GOOD]);
                scoreResult.bad = ExtractResultNotes(rawScoreArray[LINE_NUM_BAD]);
                scoreResult.miss = ExtractResultNotes(rawScoreArray[LINE_NUM_MISS]);
            } catch (IndexOutOfRangeException)
            {
                logger.Error("RawString is not containes all data.");
            }

            // image file path
            scoreResult.imageFilePath = imageFilePath;

            // calculate ex score, total notes, clear type
            scoreResult.CalculateInfos();

            // result
            return scoreResult;
        }

        /// <summary>
        /// EX Score, Total notes, Clear typeを計算する。
        /// </summary>
        public void CalculateInfos()
        {
            // If data is invalid, nothing to do.
            if (IsValidResultNotes() == false)
            {
                return;
            }

            // ex score
            exScore = perfect * 2 + great;

            // total notes
            totalNotes = perfect + great + good + bad + miss;

            // clear type
            if ((good + bad + miss) == 0)
            {
                clearType = CLEAR_TYPE_FULL_COMBO;
            }
            else if ((bad + miss) <= 10)
            {
                clearType = CLEAR_TYPE_HARD_CLEAR;
            }
            else if ((bad + miss) <= 20)
            {
                clearType = CLEAR_TYPE_NORMAL_CLEAR;
            }
            else
            {
                clearType = CLEAR_TYPE_EASY_CLEAR;
            }

        }

        /// <summary>
        /// 文字列を空白で分割し、末尾の文字列をノーツとみなしてLong型で返却する。
        /// </summary>
        /// <param name="rawResultNotesString">ノーツ文字列</param>
        /// <returns>スコア値</returns>
        private static long ExtractResultNotes(string rawResultNotesString)
        {
            try
            {
                return long.Parse(rawResultNotesString.Trim());
            } catch (Exception e)
            {
                logger.Error("Score parse error. raw result notes string = " + rawResultNotesString);
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
        /// ResultNotesにエラー値が残っていないか検査する。
        /// </summary>
        /// <returns>true:成功、false:エラー値が存在</returns>
        public bool IsValidResultNotes()
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
