using System.IO;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public string folderName = "SafeFiles";
    public string fileName = "gameSave.txt";

    string filePath;

    private void Start()
    {
        if (CheckForSaveData())
        {
            Debug.Log($"Found {folderName} and {fileName}");

            LoadSaveData();
        }
        else
        {
            Debug.Log($"Find {folderName} or {fileName}. Are they set?");
        }
    }

    public bool CheckForSaveData()
    {
        //Create path from combined given names
        filePath = Path.Combine(folderName, fileName);

        //Check for folder
        bool isFolderThere = Directory.Exists(folderName);

        //Check for file
        bool isFileThere = File.Exists(filePath);

        // Log the status
        Debug.Log($"Folder exists: {isFolderThere}, File exists: {isFileThere}");

        // Return true if both folder and file exist
        return isFolderThere && isFileThere;
    }

    void SaveAsFile(string[] variableToSave, string saveName)
    {
        //string path = $"SaveFiles/{saveName}.txt";
        //System.IO.File.WriteAllText(path, variableToSave);
    }

    public bool firstStartUp = false;
    void CreateSaveFile()
    {
        /**
         * TODO: Create a relative path from knowl location and save stuff files there.
         * We know where we are and what we put there.
         * so if we creat it and never expect it to change
         * we simply save and load from the expected path.
         * Why would you bother finding out where you are if you never leafe?
        **/

        //Create Subfolder
        Directory.CreateDirectory(folderName);

        //Creat File in Subfolder
        filePath = Path.Combine(folderName, fileName);   //Combine will work with more platforms 
                                                                //string filePath = folderName + "/" + fileName;  

        //Create Stuff to write in lines
        //string[] lines =
        //{
        //    "1",
        //    "2",
        //    "3"
        //};
        //File.WriteAllLines(filePath, lines);

        //Dynamic lines generation
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = 1; i <= 5; i++)
            {
                writer.WriteLine($"{i} --- Line {i}");
            }
        }

        Debug.Log($"Files Saved in : {filePath}");
    }

    public void LoadSaveData()
    {
        using (StreamReader reader = new StreamReader(filePath))
        {

        }
    }
}
