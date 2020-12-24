using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Media;
using System.Security.Policy;
using System.Deployment.Internal;
using System.Runtime.InteropServices;
using System;

public static class SystemSave
{
    private static string GetSaveDirectoryPath()
    {
        string mainDirectoryPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        string saveDirectoryPath = System.IO.Path.Combine(mainDirectoryPath, "PlayerSave");
        return saveDirectoryPath;
    }
    private static string GetSavePath()
    {
        string saveDirectoryPath = GetSaveDirectoryPath();
        string path = System.IO.Path.Combine(saveDirectoryPath, "player.bin");
        return path;
    }
    public static PlayerData SavePlayer(Player_Movement player)
    {
        if(PlayerPrefs.GetString("mode") == "singleplayer")
        {
            BinaryFormatter formatter = new BinaryFormatter();
            
            System.IO.Directory.CreateDirectory(GetSaveDirectoryPath());

            FileStream stream = new FileStream(GetSavePath(), FileMode.Create);
            Debug.Log("Do pliku " + GetSavePath());

            PlayerData data = new PlayerData(player);
            formatter.Serialize(stream, data);
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
    public static PlayerData LoadPlayer()
    {
        if (PlayerPrefs.GetString("mode") == "singleplayer")
        {
            if (File.Exists(GetSavePath()))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(GetSavePath(), FileMode.Open);
                Debug.Log("Z pliku " + GetSavePath());
                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();
                return data;
            }
            else
            {
                Console.WriteLine("Nie ma pliku");
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}