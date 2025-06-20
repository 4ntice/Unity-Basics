using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Saver : MonoBehaviour
{
    public string saveFileName = "SaveFile.txt";
    public string currentFile = null;

    public string saveFolderName = "SaveFolder";
    public string saveFolderPath = "./Assets/";

    string fullPath = null;

    private void Start()
    {
        if(fullPath == null) //Will usualy be null, so it creates the full path.
        {
            fullPath = saveFolderPath + saveFolderName + "/" + saveFileName;
        }

        if (CheckForFileExist(fullPath)) //we need the local path from here on now, if it is missing we can stop or create a new, because someone has deleted it.
        {
            return;
        }
        else
        {
            CreateFile(); //Now we create the folder structure and first save file.
        }
    }

    void CreateFile()
    {
        try
        {
            // Specify the file path and name
            string path = fullPath;//@"./Assets/SaveFolder/"+ saveFileName;//script executes from the assets folder, a relative path exits there
                                   // @ makes the program treat \ as a character and not a escape sequence. if you remove it you need to do \\
            // Create a StreamWriter instance to write to the file
            SaveFile();

            Debug.Log("File created successfully. At " + path);
        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e.Message);
        }
        finally
        {
            Debug.Log("Executing finally block.");
        }
    }

    bool CheckForFileExist(string filePath)
    {
        if (File.Exists(filePath))
        {
            Debug.Log("Save File Found " + filePath);
            return true;
        }
        Debug.Log("No Save File " + filePath);
        return false;
    }

    public void ReadFile()
    {
        string filePath = fullPath;

        if (!CheckForFileExist(filePath))
        {
            return;
        }


        using (StreamReader reader = new StreamReader(filePath)) // \r\n r = to beginnin of next line, n = down a line
        {
            string line;
            while ((line = reader.ReadLine()) != null) //reader.Peek() returns a number or -1 when at end   
            {
                Debug.Log(line);  
            }
        }
    }
    public int saves = 0;
    public void SaveFile()
    {
        string path = fullPath;

        saves++;

        using (StreamWriter sw = new StreamWriter(path))
        {
            // Write text to the file
            sw.WriteLine($"This is Save {saves} of (add total ammount of saves)"); //Debug to test this
            sw.WriteLine("Hello World!!"); //Write line creates a new line after the last or \n and write
            sw.WriteLine("From the StreamWriter class");
        }
    }

    public void ChangeFile()
    {
        string filePath = fullPath;

        if (!CheckForFileExist(filePath))
        {
            return;
        }


        try
        {
            // Step 1: Read the file content
            string fileContent = File.ReadAllText(filePath);
            Debug.Log("Original Content:");
            Debug.Log(fileContent);

            // Step 2: Modify the content (example: append text)
            string modifiedContent = fileContent + Environment.NewLine + "This is the new line added.";

            // Step 3: Write the modified content back to the file
            File.WriteAllText(filePath, modifiedContent);

            Debug.Log("\nFile updated successfully!");
        }
        catch (Exception ex)
        {
            Debug.Log($"An error occurred: {ex.Message}");
        }
    }
}
