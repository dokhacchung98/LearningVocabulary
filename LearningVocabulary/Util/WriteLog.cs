using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningVocabulary.Util
{
    class WriteLog
    {
        public static void WriteLogException(string tag, string methodName, string error)
        {
            Console.WriteLine(tag + " -> " + methodName + " Error Exception: " + error);
        }

        public static void WriteLogSuccess(string tag, string methodName, string value)
        {
            Console.WriteLine(tag + " -> " + methodName + " :" + value);
        }
    }
}
