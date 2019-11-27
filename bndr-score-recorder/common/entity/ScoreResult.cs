using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bndr_score_recorder.common.entity
{
    class ScoreResult
    {
        private static readonly string[] CHAR_RAW_SCORE_LIST_SPLITER = { "\n" };

        public static readonly string LITERAL_PERFECT = "PERFECT";
        public static readonly string LITERAL_GREAT = "GREAT";
        public static readonly string LITERAL_GOOD = "GOOD";
        public static readonly string LITERAL_BAD = "BAD";
        public static readonly string LITERAL_MISS = "MISS";

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
        /// <param name="scoreString">Tesseractで読み取ったScoreResult文字列</param>
        /// <returns>ScoreResultオブジェクト</returns>
        public static ScoreResult Parse(string scoreString)
        {
            // result
            ScoreResult scoreResult = new ScoreResult();

            // split raw string and convert to list
            string[] rawScoreArray = scoreString.Split(CHAR_RAW_SCORE_LIST_SPLITER, StringSplitOptions.RemoveEmptyEntries);
            List<string> rawScoreList = new List<string>();
            rawScoreList.AddRange(rawScoreArray);

            // scan rawScore and insert to return object
            rawScoreList.ForEach(rawScore => {
                if (rawScore.Contains(LITERAL_PERFECT) == true)
                {
                    scoreResult.perfect = int.Parse(rawScore.Replace(LITERAL_PERFECT, string.Empty).Trim());
                }
                else if (rawScore.Contains(LITERAL_GREAT) == true) {
                    scoreResult.great = int.Parse(rawScore.Replace(LITERAL_GREAT, string.Empty).Trim());
                }
                else if (rawScore.Contains(LITERAL_GOOD) == true)
                {
                    scoreResult.good = int.Parse(rawScore.Replace(LITERAL_GOOD, string.Empty).Trim());
                }
                else if (rawScore.Contains(LITERAL_BAD) == true)
                {
                    scoreResult.bad = int.Parse(rawScore.Replace(LITERAL_BAD, string.Empty).Trim());
                }
                else if (rawScore.Contains(LITERAL_MISS) == true)
                {
                    scoreResult.miss = int.Parse(rawScore.Replace(LITERAL_MISS, string.Empty).Trim());
                }
            });

            // check score is valid?
            if (scoreResult.IsValidScore() == false)
            {
                throw new FormatException("Input data is not valid for ScoreResult.");
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
