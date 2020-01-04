using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ManipulateProtocolTextFile : MonoBehaviour
{
    public static ManipulateProtocolTextFile MPTF;


    //use Application.dataPath because on every device accessing the Asset folder can work differently (e.g. laptop, mobile) just to be sure
    public static string pathToAssetFolder = Application.dataPath;
    public static string pathInsideAssetFolder = "/TextFile/";
    public static string fileName = "Protocol.txt";
    public static string fullPath = pathToAssetFolder + pathInsideAssetFolder + fileName;

    TextAsset protocol = new TextAsset(fullPath);

    //output from the file after reading it
    public static List<string> stringList = new List<string>();
    //what to write into the file
    public static List<string> writeList = new List<string>();

    public static int lineCount = 0;
    public static int getLineCount()
    {
        return lineCount;
    }

    public static void setLineCount(int num)
    {
        lineCount = num;
    }

    //read from file
    public static void readFile(string filePath)
    {
        StreamReader sReader = new StreamReader(filePath);
        while(!sReader.EndOfStream)
        {
            //look for every line because we want to work with the individually
            string line = sReader.ReadLine();

            //if we wanted the text as a whole
            //string text = sReader.ReadToEnd();

            stringList.Add(line);
        }
        sReader.Close();
    }

    //erase everything previously written in the file
    public static void writeFile(string filePath, string content)
    {
        StreamWriter sWriter;
        if (!File.Exists(filePath))
        {
            sWriter = File.CreateText(fullPath);
        }
        else 
        {
            sWriter = new StreamWriter(filePath);
        }

        sWriter.WriteLine(content);
        
        sWriter.Close();
    }

    // add to the end of the file while leaving everything previously written intact
    public static void appendFile(string filePath)
    {
        StreamWriter sWriter;
        if(!File.Exists(filePath))
        {
            sWriter = File.CreateText(fullPath);
        }
        else
        {
            sWriter = new StreamWriter(filePath, append: true);
        }

        for(int i = 0; i < writeList.Count; i++)
        {
            sWriter.WriteLine(writeList[i]);
        }

        sWriter.Close();
    }

    public static void clearTextFile()
    {
        File.WriteAllText(fullPath, string.Empty);
        setLineCount(0);
    }

    // get the current amount of lines in the file
    public static int getLineCountOfFile(string filePath)
    {
        int lineCount = 0;
        StreamReader sReader = new StreamReader(filePath);
        while (!sReader.EndOfStream)
        {
            sReader.ReadLine();
            lineCount++;
        }
        sReader.Close();
        return lineCount;
    }

    //add the parameter to the writeList and append it
    public static void addParameterToWriteList(string lineToAdd)
    {
        setLineCount(getLineCount() + 1);
        writeList.Add(getLineCount() + ". " + lineToAdd);
        
    }

    //print out the whole text file to the console once the heap is finished
    public static void printOutProtocolContent()
    {
        appendFile(fullPath);
        readFile(fullPath);
        foreach(string s in stringList)
        {
            print(s);
        }
    }
}
