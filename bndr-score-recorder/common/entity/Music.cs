using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bndr_score_recorder.common.entity
{
    class Music
    {
        // 曲名
        public string title;

        // Level
        public int level;

        // 難易度種別 : CodeMaster.DifficultCodeMaster
        public int difficultCodes;

        // ScoreData
        public List<ScoreResult> scoreResultList = new List<ScoreResult>();
    }
}
