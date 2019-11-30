using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BndrScoreRecorder.common.entity
{
    [DataContract]
    public class Music
    {
        // id
        [DataMember]
        public string id;

        // 曲名
        [DataMember]
        public string title;

        // Level
        [DataMember]
        public int level;

        // 難易度種別 : CodeMaster.DifficultCodeMaster
        [DataMember]
        public string difficult;

        // ScoreData
        [DataMember]
        public List<ScoreResult> scoreResultList = new List<ScoreResult>();

        /// <summary>
        /// 楽曲データ、スコアデータをJSON化する。
        /// </summary>
        /// <param name="music">対象オブジェクト</param>
        /// <returns>JSON文字列</returns>
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

        /// <summary>
        /// 曲名文字列と難易度文字列からIDを生成する。
        /// </summary>
        public void CreateIdFromTitle()
        {
            using (SHA512CryptoServiceProvider provider = new SHA512CryptoServiceProvider())
            {
                byte[] bytes = provider.ComputeHash(Encoding.UTF8.GetBytes(title + difficult));
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
                id = stringBuilder.ToString();
            }
        }
    }
}
