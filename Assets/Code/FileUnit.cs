using System;
using System.IO;
using UnityEngine;

public class FileUnit : MonoBehaviour
{
    public String _FilePath;
    public static bool CheckFile(String FilePath)
    {
        String SongName = "";
        for(int i = FilePath.Length - 1;i >= 0; i--)
        {
            if(FilePath[i] == '/')
            {
                SongName = FilePath.Substring(i + 1);
                break;
            }
        }
        String SongPath = FilePath + "/" + SongName + ".mp3";
        String MapsPath = FilePath + "/" + SongName + ".mps";
        String ChkPath = FilePath + "/" + SongName + ".chk";
        if (!File.Exists(SongPath) || !File.Exists(MapsPath) || !File.Exists(ChkPath))
            return false;
        FileInfo Song = new FileInfo(SongPath);
        FileInfo Maps = new FileInfo(MapsPath);
        long Number = Song.Length * 233 + Maps.Length * 8848;
        StreamReader Chk = new StreamReader(ChkPath);
        //FileStream Chk = new FileStream(ChkPath, FileMode.Open);
        long ChkNumber = long.Parse(Chk.ReadLine().Split(new char[] { ' ', '\n' })[0]);
        if (Number != ChkNumber) return false;
        return true;
    }
    string getURL()
    {
        string root = @"./";
        return root;
    }
    void Start()
    {
        CheckFile(_FilePath);
    }
    public FileUnit()
    {
    }
}
