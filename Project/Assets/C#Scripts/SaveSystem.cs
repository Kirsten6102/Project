using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//class to save the data and translate it to and from binary
//binary is being used to save the game as it is more secure due to the nature of the binary language

//public static class SaveSystem {

//	public static void SaveGame (PlayerMovement player, LevelManager lvlMnger)
//    {
//        BinaryFormatter formatter = new BinaryFormatter();
        
//        //Application.persistnetDataPath will find and use a system file path that is unlikely to change
//        string path = Application.persistentDataPath + "/Gamesave.binary";
//        FileStream file = new FileStream(path, FileMode.Create);

//        PlayerData data = new PlayerData(lvlMnger, player);

//        formatter.Serialize(file, data);
//        file.Close();

//    }

//    public static PlayerData LoadGame()
//    {
//        string path = Application.persistentDataPath + "/Gamesave.binary";

//        if (File.Exists(path))
//        {
//            BinaryFormatter formatter = new BinaryFormatter();
//            FileStream file = new FileStream(path, FileMode.Open);

//            PlayerData data = formatter.Deserialize(file) as PlayerData;
//            file.Close();

//            return data;

//        } else
//        {
//            Debug.LogError("Save file not found in " + path);
//            return null;
//        }
//    }

//}
