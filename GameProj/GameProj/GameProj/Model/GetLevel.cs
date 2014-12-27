using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameProj.Model
{
    class GetLevel
    {

        // <summary>
        /// Gets the strings from the needed textfile for Levels.
        /// </summary>
        /// <param name="level"></param>
        public static string GetLevels(int level)
        {
            
            string levelSource = String.Format(@"..\..\..\Level{0}.txt", level);
            string fullSource = System.IO.Path.GetFullPath(levelSource);
            bool levelStrings = false;

            using (StreamReader reader = new StreamReader(fullSource))
            {
                string levelString = reader.ReadToEnd();

                while (!levelStrings)
                {
                    int n = levelString.IndexOf("\n");
                    int r = levelString.IndexOf("\r");

                    if (n != -1)
                    {
                        levelString = levelString.Remove(n, 1);
                    }

                    if (r != -1)
                    {
                        levelString = levelString.Remove(r, 1);
                    }

                    if (n == -1 && r == -1)
                    {
                        levelStrings = true;
                    }
                }

                return levelString;
            }
        }
    }
}
