using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApp3
{
    class XMLDataSourceExample
    {
        public static void Main()
        {
           string fileName = "employee.xml";

           XmlDocument doc = new XmlDocument();
           doc.Load(fileName);

            //get all nodes with "emp" xml tags with XPath
            // string path = "employee/emp";
            // XmlNodeList nodeList = doc.SelectNodes(path);

            Console.WriteLine("Using XmlDocument Class");
            //get all nodes with "emp" xml tags (alternative approach)
            XmlNodeList nodeList = doc.GetElementsByTagName("emp");

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlNode node = nodeList[i];
               
                //access attributes of each node
                string empName = node.Attributes["empname"].Value;
                string deptid = node.Attributes["deptid"].Value;

               // if (deptid.Equals("10"))
                //{
                    sb.Append(empName + "\n");
                    sb.Append(deptid + "\n");
                //}
                //access immediate child node values
                XmlNodeList childNode = node.ChildNodes;
                sb.Append(childNode[0].InnerText + "\n");
                sb.Append(childNode[1].InnerText + "\n");
                sb.Append("\n");

            }

            Console.Write(sb.ToString());

           Console.WriteLine();
           Console.WriteLine("Using XElement Class (LINQTOXML)");
            //using LINQTOXML
            //using System.Xml.Linq.XElement class
            XElement elm = XElement.Load(fileName);
            if (elm != null)
            {
                foreach (var emp in elm.Elements("emp"))
                {
                   // foreach (var item in emp.Attributes())
                   // {
                     //   Console.WriteLine(item.Name + ": " + item.Value);
                    //}

                    //how to access child nodes
                    string streetTemp = emp.Element("street").Value;
                    string cityTemp = emp.Element("city").Value;
          
                    //how to access attributes
                    string empName = emp.Attribute("empname").Value;
                    string deptid = emp.Attribute("deptid").Value;

                    Console.WriteLine(empName);
                    Console.WriteLine(deptid);
                    Console.WriteLine(streetTemp);
                    Console.WriteLine(cityTemp);
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
        } 
    }
}
