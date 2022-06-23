using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Saving
{
    //creates a new file using binary to stop easy modification of the file
   public static void SavePlayer(PlayerController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData info = new PlayerData(player);

        formatter.Serialize(stream, info);
        stream.Close();
    }
   
    //loads a file based on the name created when saving
    public static PlayerData LoadSave()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();


            return data;
        }
        else
        {
            Debug.LogError("Save file not found at" + path);
            return null;
        }
    }

}
