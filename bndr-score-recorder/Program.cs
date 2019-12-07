using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BndrScoreRecorder
{
    static class Program
    {
        // Exception message
        private static readonly string EXCEPTION_MESSAGE_FORMAT = "予期せぬ例外が発生したため、処理を中断しました。"
            + Environment.NewLine + "[Message]"
            + Environment.NewLine + "{0}"
            + Environment.NewLine + "[StackTrace]"
            + Environment.NewLine + "{1}";

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            } catch (Exception ex)
            {
                MessageBox.Show(String.Format(EXCEPTION_MESSAGE_FORMAT, ex.Message, ex.StackTrace));
            }
        }
    }
}
