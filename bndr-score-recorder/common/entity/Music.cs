using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bndr_score_recorder.common.entity
{
    class Music
    {
        // ID
        public string id;

        // 曲名
        public string title;

        // Level
        public int level;

        // 難易度種別 : CodeMaster.DifficultCodeMaster
        public int difficultCode;

        // ScoreData
        List<ScoreResult> scoreResultList = new List<ScoreResult>();
    }
}
