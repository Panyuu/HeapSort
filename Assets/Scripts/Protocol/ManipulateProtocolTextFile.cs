using System.Collections.Generic;
using UnityEngine;
using System.IO;

// author: Leon Portius

public class ManipulateProtocolTextFile : MonoBehaviour {
    // singleton
    public static ManipulateProtocolTextFile MPTF;

    //use Application.dataPath because on every device accessing the Asset folder can work differently (e.g. laptop, mobile) just to be sure
    public static string pathToAssetFolder = Application.dataPath;
    // the path inside the AssetFolder to the Protocol text file
    public static string pathInsideAssetFolder = "/TextFile/";
    // the name of the protocol text file
    public static string fileName = "Protocol.txt";
    // the full path from root to the protocol text file
    public static string fullPath = pathToAssetFolder + pathInsideAssetFolder + fileName;

    //output from the file after reading it
    public static List<string> stringList = new List<string>();
    //what to write into the file
    public static List<string> writeList = new List<string>();

    // how many lines are currently in the protocol text file
    public static int lineCount = 0;

    //getter method for lineCount
    public static int getLineCount() {
        return lineCount;
    }

    //setter method for lineCount
    public static void setLineCount(int num) {
        lineCount = num;
    }

    //read from file
    public static void readFile(string filePath) {
        // open reading stream
        StreamReader sReader = new StreamReader(filePath);
        // i the end isn't reached yet
        while (!sReader.EndOfStream) {
            //look for every line because we want to work with the individually
            string line = sReader.ReadLine();

            // add line to the protocol
            stringList.Add(line);
        }
        // close stream again to free up resources
        sReader.Close();
    }

    //erase everything previously written in the file
    public static void writeFile(string filePath, string content) {
        //allocate writer stream
        StreamWriter sWriter;
        // if the file we want to write into doesn't exist yet 
        if (!File.Exists(filePath)) {
            //make new file
            sWriter = File.CreateText(fullPath);
        }
        else {
            //write into existing file
            sWriter = new StreamWriter(filePath);
        }
        //write line into file
        sWriter.WriteLine(content);
        // close stream again to free up resources
        sWriter.Close();
    }

    // add to the end of the file while leaving everything previously written intact
    public static void appendFile(string filePath) {
        StreamWriter sWriter;
        // if the file we want to write into doesn't exist yet 
        if (!File.Exists(filePath)) {
            // make new file
            sWriter = File.CreateText(fullPath);
        }
        else {
            //write into existing file
            sWriter = new StreamWriter(filePath, append: true);
        }

        // add all lines contained into writeList
        for (int i = 0; i < writeList.Count; i++) {
            sWriter.WriteLine(writeList[i]);
        }

        sWriter.Close();
    }

    //content of text file is ""
    public static void clearTextFile() {
        File.WriteAllText(fullPath, string.Empty);
        // line count is independent of content is gets reset maunally
        setLineCount(0);
    }

    // get the current amount of lines in the file
    public static int getLineCountOfFile(string filePath) {
        int lineCount = 0;
        StreamReader sReader = new StreamReader(filePath);
        // if the end hasn't been reached yet
        while (!sReader.EndOfStream) {
            sReader.ReadLine();
            lineCount++;
        }
        sReader.Close();

        return lineCount;
    }

    //add the parameter to the writeList and append it
    public static void addParameterToWriteList(string lineToAdd) {
        setLineCount(getLineCount() + 1);
        writeList.Add(getLineCount() + ". " + lineToAdd);
    }

    //print out the whole text file to the console once the heap is finished
    public static void printOutProtocolContent() {
        // add everything at once to file once all the lines are collected in list
        appendFile(fullPath);
        // read from file into stringList
        readFile(fullPath);
    }
}
