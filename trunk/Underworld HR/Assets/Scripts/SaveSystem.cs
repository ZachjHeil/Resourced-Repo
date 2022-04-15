using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    public static string savePath = Application.persistentDataPath + "/autosave.dat";
    
    public static void SaveGame(SaveData data){
	BinaryFormatter bf = new BinaryFormatter();
	//string path = Application.persistentDataPath + "autosave.dat";
	FileStream stream = new FileStream(SaveSystem.savePath,FileMode.Create);

	bf.Serialize(stream,data);

	stream.Close();
    }

    public static bool SaveExists(){
	return File.Exists(SaveSystem.savePath);
    }

    public static SaveData LoadGame(){
	//string path = Application.persistentDataPath + "autosave.dat";
	if(SaveSystem.SaveExists()){
	    BinaryFormatter bf = new BinaryFormatter();
	    FileStream stream = new FileStream(SaveSystem.savePath,FileMode.Open);

	    SaveData data = bf.Deserialize(stream) as SaveData;
	    stream.Close();
	    return data;
	}else{
	    Debug.LogError("Safe file not found in " + SaveSystem.savePath);
	    return null;
	}
    }

    public static string GetLevelName(LevelName index){
	if(index == LevelName.FINAL_LEVEL)
	    return "Final_Stage";
	return index.ToString();
    }
    
    
    public static LevelName GetLevelIndex(string levelName){
	if(levelName.Equals("Tutorial"))
	    return LevelName.TUTORIAL;
	if(levelName.Equals("Stage1_Level1"))
	    return LevelName.STAGE1_LEVEL1;
	if(levelName.Equals("Stage1_Level2"))
	    return LevelName.STAGE1_LEVEL2;
	if(levelName.Equals("Stage2_Level1"))
	    return LevelName.STAGE2_LEVEL1;
	if(levelName.Equals("Stage2_Level2"))
	    return LevelName.STAGE2_LEVEL2;
	if(levelName.Equals("Stage3_Level1"))
	    return LevelName.STAGE3_LEVEL1;
	if(levelName.Equals("Stage3_Level2"))
	    return LevelName.STAGE3_LEVEL2;
	if(levelName.Equals("Final_Stage"))
	    return LevelName.FINAL_LEVEL;
	return LevelName.TUTORIAL;
    }
}

// the data that will be saved to a file
[System.Serializable]
public class SaveData {
    public LevelName levelProgress;
    public StarRating[] levelStars = new StarRating[9];

    public SaveData(){
	levelProgress = LevelName.TUTORIAL;
	for(int i = 0; i < levelStars.Length;i++){
	    levelStars[i] = StarRating.NONE;
	}
    }
}
