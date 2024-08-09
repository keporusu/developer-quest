using UnityEngine;
using System;


namespace Code.Menu
{
    public class UserRepository
    {
        private const string CONTRIBUTION_POINT_KEY = "ContributionPoint";
        private const string LEVEL_KEY = "Level";
        private const string EXPERIENCE_KEY = "Experience";

        public static int LoadContributionPoint()
        {
            return PlayerPrefs.GetInt(CONTRIBUTION_POINT_KEY, 0);
        }

        public static void SaveContributionPoint(int points)
        {
            PlayerPrefs.SetInt(CONTRIBUTION_POINT_KEY, points);
            PlayerPrefs.Save();
        }

        public static int LoadLevel()
        {
            return PlayerPrefs.GetInt(LEVEL_KEY, 0);
        }

        public static void SaveLevel(int level)
        {
            PlayerPrefs.SetInt(LEVEL_KEY, level);
            PlayerPrefs.Save();
        }

        public static int LoadExperience()
        {
            return PlayerPrefs.GetInt(EXPERIENCE_KEY, 0);
        }

        public static void SaveExperience(int experience)
        {
            PlayerPrefs.SetInt(EXPERIENCE_KEY, experience);
            PlayerPrefs.Save();
        }
    }
}
