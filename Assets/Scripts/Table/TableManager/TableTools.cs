using UnityEngine;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

public class TableTools
{
    public static string TablePath = Application.streamingAssetsPath;

    public static byte[] GetZipBuffer(string resource)
    {
        return GetNormalBufferFromZip(Application.streamingAssetsPath + "/" + resource + ".txt");
    }

    public static byte[] GetNormalBufferFromZip(string zipFileName)
    {
        try
        {
            if (!File.Exists(zipFileName))
            {
                Debug.Log("file not exist");
                return null;
            }
            ZipInputStream zipStream = new ZipInputStream(File.OpenRead(zipFileName));
            //zipStream.Password = "abcwave_dreamheaven_game_2b";
            zipStream.GetNextEntry();

            long length = zipStream.Length;
            byte[] buffer = new byte[length];
            zipStream.Read(buffer, 0, (int) length);
            zipStream.Close();
            return buffer;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format("GetNormalBufferFromZip is error : {0}", ex.ToString()));
        }

        return null;
    }

    public static string ReadString(BinaryReader reader)
    {
        string str = "";
        char ch;
        while ((ch = reader.ReadChar()) != 0)
            str += ch;
        return str;
    }
}