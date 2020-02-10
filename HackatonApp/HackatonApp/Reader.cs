using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackatonApp
{
    class Reader
    {
        private static String fname;
        private String name;

        public Reader(String aname)
        {
            fname = aname;
        }
        public string sorting()
        {
            StreamReader str = new StreamReader(fname);
            string line = "";
            name = "Not found yet";
            line = str.ReadToEnd();
            char[] delimiters = { ' ', ',', '.', ':', '\t', '\n' };
            string[] words = line.Split(delimiters);
            Boolean stop = false;
            int pos = 0;
            while (!stop && pos < words.Length / 2)
            {
                if (words[pos] == "between")
                {
                    pos++;
                    while (words[pos] != "and")
                    {
                        pos++;
                    }
                    name = words[pos + 1] + " " + words[pos + 2];
                    stop = true;
                }
                pos++;
            }
            return name;
        }

    }
}
