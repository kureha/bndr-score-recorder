using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bndr_score_recorder.common.entity
{
    class ScoreResult
    {
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
        public int rankCode;
    }
}
