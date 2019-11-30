using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BndrScoreRecorder.common.bridge
{
    public class BridgeBase
    {
        // logger
        protected static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // process arg spliter
        protected static readonly string ARG_SPLIT_CHAR = " ";

        // is window is visible?
        protected static readonly bool IS_VISIBLE_WINDOW = false;
    }
}
