using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bndr_score_recorder.common.bridge
{
    class BridgeBase
    {
        // logger
        protected static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // process arg spliter
        protected static readonly string ARG_SPLIT_CHAR = " ";
    }
}
