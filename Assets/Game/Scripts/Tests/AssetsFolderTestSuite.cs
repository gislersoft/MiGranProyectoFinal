using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.IO;

public class AssetsFolderTestSuite {
    private string assetsRootDirectory = Application.dataPath;

    [Test]
    public void elRootFolderNoDebeTenerNingunArhivo() {
        // Use the Assert class to test conditions.
        DirectoryInfo dir = new DirectoryInfo(assetsRootDirectory);
        FileInfo[] allFiles = dir.GetFiles("*.*");
        foreach (FileInfo file in allFiles) 
        {
            if (file.Extension != ".meta") {
                Assert.Fail("El archivo '"+file.Name+"' no debe estar en el root folder.");
            }
        }
    }

    [Test]
    public void elRootFolderDebeContenerSoloLasCarpetasConfiguradas() {
        string[] carpetasValidas = {
            "Game",
            "Lens Flares",
            "PostProcessing",
            "Standard Assets"
        };
        // Use the Assert class to test conditions.
        DirectoryInfo dir = new DirectoryInfo(assetsRootDirectory);
        DirectoryInfo[] allDirectories = dir.GetDirectories();
        foreach (DirectoryInfo directory in allDirectories) 
        {
            int pos = System.Array.IndexOf(carpetasValidas, directory.Name);
            if (pos <= -1)
            {
               Assert.Fail("El directorio '"+directory.Name+"' no debe estar en el root.");
            }
        }
    }
}
