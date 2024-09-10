using UnityEngine;

public class SaveManager 
{
    public void SaveData(string key, int score)
    {
        PlayerPrefs.SetInt(key, score);
        PlayerPrefs.Save();
    }

    public int LoadData(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            int score = PlayerPrefs.GetInt(key, 0);
            return score;
        }
        else
        {
            SaveData(key, 0);
            return 0;
        }
    }
}
