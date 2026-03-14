using System.IO;
using UnityEngine;

public class StickerUtility : MonoBehaviour
{
    [Header("Camera & Settings")]
    public Camera targetCamera;       // Camera to capture from
    public int imageWidth = 64;
    public int imageHeight = 64;
    public string fileName = "CapturedImage.png";
    public bool saveAsJPG = false;    // If false, saves as PNG

    [Header("Save Location")]
    public string saveFolder = "Screenshots"; // Relative to Application.persistentDataPath

    private void Start()
    {
        CaptureAndSave();
    }

    // Call this to capture and save
    public void CaptureAndSave()
    {
        if (targetCamera == null)
        {
            Debug.LogError("No camera assigned for RenderTexture capture.");
            return;
        }

        // Create a temporary RenderTexture
        RenderTexture rt = new RenderTexture(imageWidth, imageHeight, 24);
        targetCamera.targetTexture = rt;

        // Render the camera's view
        targetCamera.Render();

        // Activate the RenderTexture and read pixels into a Texture2D
        RenderTexture.active = rt;
        Texture2D tex = new Texture2D(imageWidth, imageHeight, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, imageWidth, imageHeight), 0, 0);
        tex.Apply();

        // Reset camera and cleanup
        targetCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // Encode to PNG or JPG
        byte[] bytes = saveAsJPG ? tex.EncodeToJPG(100) : tex.EncodeToPNG();
        Destroy(tex);

        // Ensure save folder exists
        string folderPath = Path.Combine(Application.dataPath, saveFolder);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        // Save file
        string filePath = Path.Combine(folderPath, fileName);
        File.WriteAllBytes(filePath, bytes);

        Debug.Log($"Image saved to: {filePath}");
    }

    // Example: Capture when pressing space
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CaptureAndSave();
        }
    }
}
