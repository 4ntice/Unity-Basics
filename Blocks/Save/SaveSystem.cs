using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private const string saveFileName = "game_save.txt";
    private const string savePath = "Killdozer01_Data/SaveGame/";//=> Path.Combine("Assets/SaveGame/", saveFileName);
    public MonoBehaviour scriptToSave;

    [ContextMenu("Save Game")]
    public void SaveGame()
    {
        CreateFolderIfNotExists(savePath);

        GetAttributedFieldValue(scriptToSave);

        File.WriteAllText(savePath + saveFileName, currentSaveData);
        Debug.Log($"Game saved to {savePath + saveFileName}");
    }

    // Checks if a folder exists; if not, creates it
    private void CreateFolderIfNotExists(string path)
    {
        // Check if the directory exists
        if (!Directory.Exists(path))
        {
            // Create the directory (also creates any missing parent folders)
            Directory.CreateDirectory(path);
            Debug.Log("Folder created at: " + path);
        }
        else
        {
            Debug.Log("Folder already exists at: " + path);
        }
    }

    public string currentSaveData = "";
    // This method finds and logs the value of any field on a MonoBehaviour that has the [MyCustomAttribute]
    void GetAttributedFieldValue(MonoBehaviour obj)
    {
        // Get the runtime type (class) of the object passed in (e.g., MyComponent)
        Type type = obj.GetType();

        // Get all instance fields in the type, including private and public ones
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        // Loop through each field in the object
        foreach (FieldInfo field in fields)
        {
            // Check if the field has the MyCustomAttribute applied
            SaveThis attr = field.GetCustomAttribute<SaveThis>();

            // If the attribute is present...
            if (attr != null)
            {
                // Get the current value stored in the field on this instance of the object
                object value = field.GetValue(obj);
                currentSaveData = value.ToString();
                // Log the field name, value, and any extra info from the attribute
                Debug.Log($"Field: {field.Name}, Value: {value}");
            }
        }
    }

    [ContextMenu("Load Game")]
    public void LoadGame()
    {
        if (!File.Exists(savePath + saveFileName))
        {
            Debug.LogWarning("No save file found!");
            return;
        }

        // Read all text from the file
        string fileContents = File.ReadAllText(savePath + saveFileName);
        Debug.Log("File loaded successfully:\n" + fileContents);

        // Find the field with [SaveThis] attribute
        Type type = scriptToSave.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (FieldInfo field in fields)
        {
            SaveThis attr = field.GetCustomAttribute<SaveThis>();
            if (attr != null)
            {
                // Convert saved string back to the field's type
                object convertedValue = ConvertToFieldType(fileContents, field.FieldType);
                if (convertedValue != null)
                {
                    field.SetValue(scriptToSave, convertedValue);
                    Debug.Log($"Loaded value '{convertedValue}' set to field '{field.Name}'");
                }
                else
                {
                    Debug.LogWarning($"Failed to convert saved value to {field.FieldType}");
                }

                // Since you save only one field, break after first match
                break;
            }
        }
    }

    // Helper method to convert string back to the appropriate field type
    private object ConvertToFieldType(string value, Type targetType)
    {
        try
        {
            if (targetType == typeof(string))
                return value;

            if (targetType.IsEnum)
                return Enum.Parse(targetType, value);

            // Use Convert.ChangeType for common value types
            return Convert.ChangeType(value, targetType);
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"Conversion error: {ex.Message}");
            return null;
        }
    }

    [ContextMenu("Delete Save File")]
    public void CreateNewPlay()
    {
        string fullPath = Path.Combine(savePath, saveFileName);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            Debug.Log("Save file deleted.");
        }
        else
        {
            Debug.LogWarning("No save file to delete.");
        }
    }
}
