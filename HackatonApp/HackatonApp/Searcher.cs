using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackatonApp
{
    class Searcher
    {
        private String filename;
        private String SearchWord;

        public Searcher(String fname, String word)
        {
            filename = fname;
            SearchWord = word;
        }

        public String searching()
        {
            StreamReader str = new StreamReader(filename);
            string line = "";
            string result = "";
            char[] delimiters = { ' ', ',', '.', ':', '\t', '\n' };
            string[] words;
            do
            {
                line = str.ReadLine();
                words = line.Split(delimiters);
                if (words.Contains(this.SearchWord) || words.Contains("bonus"))
                {
                    result = result + line + str.ReadLine() + "\n";

                }
            } while (str.Peek() != -1);
            return result;
        }

    }
}
