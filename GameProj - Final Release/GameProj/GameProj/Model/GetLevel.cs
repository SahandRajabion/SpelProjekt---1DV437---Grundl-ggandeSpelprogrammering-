using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameProj.Model
{
    class GetLevel
    {

        private const string FileLocationLevel1 = @"..\..\..\Level1.txt";
        private const string FileLocationLevel2 = @"..\..\..\Level2.txt";
        private const string FileLocationLevel3 = @"..\..\..\Level3.txt";



        /// <summary>
        /// Finds the textfile locations of the levels.
        /// </summary>
        /// <param name="currentLevel"></param>
        /// <returns></returns>
        private string FindLevelLocation(int currentLevel)
        {
            if (currentLevel == 1)
                return System.IO.Path.GetFullPath(FileLocationLevel1);
            else if (currentLevel == 2)
                return System.IO.Path.GetFullPath(FileLocationLevel2);
            else if (currentLevel == 3)
                return System.IO.Path.GetFullPath(FileLocationLevel3);


            throw new ArgumentException("Could not find the right path for current level Textfile!");
        }

        public string GetLevels(int level)
        {

            string FileLocation = FindLevelLocation(level);
            bool levelStrings = false;

            using (StreamReader reader = new StreamReader(FileLocation))
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
