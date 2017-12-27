using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class CustomNode
    {
        public string Title;
        public CustomNode Parent;
        public List<CustomNode> Children;


        public CustomNode(string title, CustomNode parent)
        {
            Title = title;
            Parent = parent;
            Children = new List<CustomNode>();

            if (Parent != null)
                Parent.Children.Add(this);
        }

        public CustomNode Find(string path)
        {
            if (String.IsNullOrEmpty(path))
                return null;

            if (path == Title)
                return this;

            string[] pieces = path.Split(new char[] { '/' });

            foreach (var child in Children)
            {
                if (child.Title == pieces[1])
                    return child.Find(path.Remove(0, Title.Length + 1));
            }

            return null;
        }

        public List<String> FindAll(CustomNode nodeToBeSearched, string absolutePathSoFar)
        {
            if (LevelOfTheNode(this) == 0)
                absolutePathSoFar = this.Title;
            else
                absolutePathSoFar = absolutePathSoFar + "/" + this.Title;

            List<String> matchedNodes = new List<String>();

            if (nodeToBeSearched.Title == Title)
            {
                matchedNodes.Add(absolutePathSoFar);
            }

            foreach (var child in Children)
            {
                  List<String> temp = new List<String>();
                  temp = child.FindAll(nodeToBeSearched, absolutePathSoFar);

                    if (temp.Count > 0)
                    {
                        matchedNodes.AddRange(temp);

                    }
                
            }

            return matchedNodes;
        } 

        public int LevelOfTheNode(CustomNode target)
        {
            CustomNode parentNode = target.Parent;
            int level = 0;
            while (parentNode != null)
            {
                parentNode = parentNode.Parent;
                level++;
            }
            return level;
        }
    }

    public class Test
    {
        public static void Main()
        {
            //Build a test tree (matches the example)
            CustomNode root = new CustomNode("Root", null);
            CustomNode userData = new CustomNode("UserData", root);
            CustomNode ud_browser = new CustomNode("Browser", userData);
            CustomNode ud_word = new CustomNode("Word", userData);
            CustomNode priv = new CustomNode("Private", userData);
            CustomNode priv_word = new CustomNode("Word", priv);

            CustomNode windows = new CustomNode("Windows", root);
            CustomNode programs = new CustomNode("Programs", root);
            CustomNode notepad = new CustomNode("Notepad", programs);
            CustomNode prog_word = new CustomNode("Word", programs);
            CustomNode prog_browser = new CustomNode("Browser", programs);

            CustomNode custom1 = new CustomNode(Console.ReadLine(), root);
            CustomNode custom2 = new CustomNode(Console.ReadLine(), custom1);
            CustomNode custom3 = new CustomNode(Console.ReadLine(), custom2);
            CustomNode target = root.Find(Console.ReadLine());

            Console.WriteLine(GetShortestUniqueQualifier(root, target));

            //The following lines written to avoid compiler warnings in the given IDE. 
            // These are actuallly not required for my own IDE
            //getting waning like this from the default IDE: warning CS0219: The variable `ud_browser' is assigned but its value is never used
            string temp1 = ud_browser.Title + ud_word.Title + priv_word.Title + priv_word.Title;
            string temp2 = windows.Title + notepad.Title + prog_word.Title + prog_browser.Title;
            string temp3 = custom3.Title;
            temp3 = temp1 + temp2 + temp3;

            Console.ReadKey();
        }

        public static string GetShortestUniqueQualifier(CustomNode root, CustomNode target)
        {
            if (root == null)
                return "null";

            if (target == null)
                return "null";

            if (String.IsNullOrEmpty(target.Title))
                return "null";

            if (String.IsNullOrEmpty(root.Title))
                return "null";

        
            //reconstruct the path of the target node again. I do not want to touch the given functions
            CustomNode parentNode = target.Parent;
            string targetNodeAbsolutepath = target.Title;

  
            while (parentNode!=null)
            {
                targetNodeAbsolutepath = parentNode.Title + "/" + targetNodeAbsolutepath;
                parentNode = parentNode.Parent;
        
            }
       
            //Search and get all conflicting paths calling this method
            List <String> conflictingPaths = root.FindAll(target,"");

            int matchedIndex = -1;
            for (int i = 0; i < conflictingPaths.Count; i++)
                if (conflictingPaths[i].Equals(targetNodeAbsolutepath))
                    matchedIndex = i;

            //Console.WriteLine(conflictingPaths[i]);

            conflictingPaths.RemoveAt(matchedIndex);


            int index = -1;
            if(conflictingPaths.Count==0)
            {
                index = targetNodeAbsolutepath.LastIndexOf("/");

                if (index >= 0)
                    return targetNodeAbsolutepath.Substring(index + 1);
                else
                    return targetNodeAbsolutepath;
            }


            string probableIdentifier = "";
            List<String> conflictingNames = new List<String>();
            for (int i = 0; i < conflictingPaths.Count; i++)
            {
                conflictingNames.Add("");
            }

                string remainingTargetNodeAbsolutepath = targetNodeAbsolutepath;
            string remainingConflictingNodeAbsolutepath = "";

            int blankCount = 0;

            while (true)
            {
                index = remainingTargetNodeAbsolutepath.LastIndexOf("/");

                if (index >= 0)
                {
                    if (!String.IsNullOrEmpty(probableIdentifier))
                        probableIdentifier = remainingTargetNodeAbsolutepath.Substring(index + 1) + "/" + probableIdentifier;
                    else
                        probableIdentifier = remainingTargetNodeAbsolutepath.Substring(index + 1);

                    remainingTargetNodeAbsolutepath = targetNodeAbsolutepath.Substring(0,index);
                }
                else
                {
                    if (!String.IsNullOrEmpty(remainingTargetNodeAbsolutepath))
                    {
                        if (!String.IsNullOrEmpty(probableIdentifier))
                            probableIdentifier =  remainingTargetNodeAbsolutepath + "/" + probableIdentifier;
                        else
                            probableIdentifier = remainingTargetNodeAbsolutepath;

                        remainingTargetNodeAbsolutepath = "";
                    }

                }


                if (blankCount >= conflictingPaths.Count && blankCount > 0)
                {
                   // if (forwardSlashCount(targetNodeAbsolutepath)== forwardSlashCount(probableIdentifier))
                        //return "/"+ probableIdentifier;
                   // else
                        return probableIdentifier;
                }


                int index1 = -1;
                Boolean conflictFound = false;

                blankCount = 0;
                for (int i = 0; i < conflictingPaths.Count; i++)
                {
                    remainingConflictingNodeAbsolutepath = conflictingPaths[i];

                    index1 = remainingConflictingNodeAbsolutepath.LastIndexOf("/");

                    if (index1 >= 0)
                    {
                        if(!String.IsNullOrEmpty(conflictingNames[i]))
                            conflictingNames[i]= remainingConflictingNodeAbsolutepath.Substring(index1 + 1) + "/" + conflictingNames[i];
                        else
                            conflictingNames[i] = remainingConflictingNodeAbsolutepath.Substring(index1 + 1);

                        conflictingPaths[i] = conflictingPaths[i].Substring(0, index1);
                    }
                    else
                    {
                        if(!String.IsNullOrEmpty(conflictingPaths[i]))
                        {
                            if (!String.IsNullOrEmpty(conflictingNames[i]))
                                conflictingNames[i] =  remainingConflictingNodeAbsolutepath + "/" + conflictingNames[i];
                            else
                                conflictingNames[i] = remainingConflictingNodeAbsolutepath;

                            conflictingPaths[i] = "";
                            blankCount++;
                        }

                    }

                    if (conflictingNames[i].Equals(probableIdentifier))
                        conflictFound = true;
                }


                if (conflictFound == false)
                {
                  //  if (forwardSlashCount(targetNodeAbsolutepath) == forwardSlashCount(probableIdentifier))
                     //   return "/" + probableIdentifier;
                    //else
                        return probableIdentifier;
                }


            }


           // return "null";
        }

        public static int forwardSlashCount(string result)
        {
            int count = 0;

            for (int i = 0; i < result.Length; i++)
                if (result[i] == '/')
                    count++;

            return count++;
        }
    }

}
