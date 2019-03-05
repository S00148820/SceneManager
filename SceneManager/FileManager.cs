using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace SceneManager
{
    /*Using the file manager to load in files
     * Using a notepad file to load them in there are two thinggs the atributes and the content
     * the attributes will tell us what content is this helps us know what game elements we are loading
     * When the file says Load=[Image] it knows we will be loading in an image
     * */

    public class FileManager
    {
        enum LoadType {Attributes, Contents};

        //Here we are having a list within a list for attributes and contents
        //we have two dimensional lists is because 2D lists is basically a list of a list to add to a 2d list you have to add another list
        //Think of it as an array. Lists also dont have a defined length so that is why we are working with them
        //List<List<string>> attributes = new List<List<string>>();
        //List<List<string>> contents = new List<List<string>>();

        //Signel Dimension lists
        List<string> tempAttributes, tempContents;
        bool identifierFound = false;

        LoadType type;

        //Method taking in two 2D lists and the filename
       public void LoadContent(string filename, List<List<string>> attributes,List<List<string>> contents)
        {
            //This is going to read a line within the text file
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    //if the line in the text file contains the value load then we know we are loading in an attribute
                    if (line.Contains("Load="))
                    {
                        tempAttributes = new List<string>();
                        line = line.Remove(0, line.IndexOf("=") + 1);//Going to remove the load equals
                        type = LoadType.Attributes;
                    }
                    else
                    {
                        //tempContents = new List<string>();
                        type = LoadType.Contents;
                    }

                    //If you notice we seperate the text file by opening and closing the square brackets the type of symbol is up to you
                    tempContents = new List<string>();

                    string[] lineArray = line.Split(']');//Having a line array equals to line.Split and then split the ] so we have an array of split lines

                    foreach (string li in lineArray)
                    {
                        string newLine = li.Trim('[', ' ', ']');//trimming out the [] and spaces
                        if (newLine != String.Empty)
                        {
                            if (type == LoadType.Contents)
                                tempContents.Add(newLine);
                            else
                                 tempAttributes.Add(newLine);
                        }
                    }

                    //if the tempcontents is equal to zero we will add them to temp contents
                    //looking at the text file ifthe attribute is image it knows that we are loading in images 
                    //once we load an attribute it adds it to temp attribute.
                    if(type == LoadType.Contents && tempContents.Count > 0)
                    {
                        contents.Add(tempContents);
                        attributes.Add(tempAttributes); 
                    }
                }
            }
        }

        //Overload for the above method the identifier will let us know when we start and end loading
        //For most ot eh code we copied the above method its just a slight modification
        public void LoadContent(string filename, List<List<string>> attributes, List<List<string>> contents, string identifier)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    //If the line in the text file says Endload= we set the identifier to falser and exit the while loop
                    //we check for endload before load because the word load is in endload 
                    if (line.Contains("EndLoad=") && line.Contains(identifier))
                    {
                        identifierFound = false;
                        break;
                    }
                    
                    else if (line.Contains("Load=") && line.Contains(identifier))
                    {
                        identifierFound = true;
                        continue;
                    }

                    if (identifierFound)
                    {
                        if (line.Contains("Load="))
                        {
                            tempAttributes = new List<string>();
                            line = line.Remove(0, line.IndexOf("=") + 1);
                            type = LoadType.Attributes;
                        }
                        else
                        {
                            tempContents = new List<string>();
                            type = LoadType.Contents;
                        }

                        string[] lineArray = line.Split(']');
                        foreach (string li in lineArray)
                        {
                            string newLine = li.Trim('[', ' ', ']');
                            if (newLine != String.Empty)
                            {
                                if (type == LoadType.Contents)
                                    tempContents.Add(newLine);
                                else
                                    tempAttributes.Add(newLine);
                            }
                        }

                        if (type == LoadType.Contents && tempContents.Count > 0)
                        {
                            contents.Add(tempContents);
                            attributes.Add(tempAttributes);
                        }
                    }
                }
            }
        }
    }
}
