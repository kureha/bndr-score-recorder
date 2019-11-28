using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace bndr_score_recorder.common.entity
{
    [DataContract]
    class Music
    {
        // 曲名
        [DataMember]
        public string title;

        // Level
        [DataMember]
        public int level;

        // 難易度種別 : CodeMaster.DifficultCodeMaster
        [DataMember]
        public int difficultCodes;

        // ScoreData
        [DataMember]
        public List<ScoreResult> scoreResultList = new List<ScoreResult>();

        public static string ToJsonString(Music music)
        {
            using (var memoryStream = new MemoryStream())
            using (var streamReader = new StreamReader(memoryStream))
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Music));
                jsonSerializer.WriteObject(memoryStream, music);
                memoryStream.Position = 0;

                return streamReader.ReadToEnd();
            }
        }
    }
}
