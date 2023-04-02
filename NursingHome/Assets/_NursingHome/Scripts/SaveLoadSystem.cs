using UnityEngine;

namespace NursingHome
{
    public interface ISaveLoad
    {
        public void SavePoints(int amount);
        public int GetSavedPoints();
    }

    public class SaveLoadSystem : ISaveLoad
    {
        const string PointSaveName = "Points";

        public int GetSavedPoints()
        {
            if(PlayerPrefs.HasKey(PointSaveName))
            {
                return PlayerPrefs.GetInt(PointSaveName);
            }

            return 0;
        }

        public void SavePoints(int amount)
        {
            Debug.Log($"Saving score: {amount}");
            PlayerPrefs.SetInt(PointSaveName, amount);
        }
    }
}