using UnityEngine;
using System;


namespace Code
{
    public class UserRepository
    {
        private const string CONTRIBUTION_POINT_KEY = "ContributionPoint";
        private const string LEVEL_KEY = "Level";
        private const string EXPERIENCE_KEY = "Experience";
        private const string ENEMY_POINT_KEY = "Enemy Point";

        private const string ENEMY_NAME_KEY = "Enemy Name";

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

        public static int LoadEnemyPoint()
        {
            return PlayerPrefs.GetInt(ENEMY_POINT_KEY, 0);
        }

        public static void SaveEnemyPoint(int point)
        {
            PlayerPrefs.SetInt(EXPERIENCE_KEY, point);
            PlayerPrefs.Save();
        }
        
        
        public static string LoadEnemyName()
        {
            return PlayerPrefs.GetString(ENEMY_POINT_KEY, "ミュータント");
        }

        public static void SaveEnemyName(string name)
        {
            PlayerPrefs.SetString(EXPERIENCE_KEY, name);
            PlayerPrefs.Save();
        }
        
    }
}
