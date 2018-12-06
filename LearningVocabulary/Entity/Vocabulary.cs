using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningVocabulary.Entity
{
    class Vocabulary
    {
        public int stt { get; set; }
        public string vocabulary { get; set; }
        public string spelling { get; set; }
        public string means { get; set; }

        public Vocabulary()
        {
            
        }

        public Vocabulary(int stt, string vocabulary, string spelling, string means)
        {
            this.stt = stt;
            this.vocabulary = vocabulary;
            if (spelling != null)
            {

                this.spelling = spelling;
            }
            else
            {
                this.spelling = "";}

            if (means != null)
            {

                this.means = means;
            }
            else
            {
                this.means = "";
            }
        }
    }
}
