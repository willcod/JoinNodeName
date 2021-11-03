using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace JoinNodeNameConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            JoinNodeName(@"Test.xml");
        }

        private static void JoinNodeName(string xmlFile)
        {
            if (!File.Exists(xmlFile)) return;

            using var fs = new FileStream(xmlFile, FileMode.Open);
            var xmlDoc = XDocument.Load(fs);

            var root = xmlDoc.Root;

            var joinedStrs = new List<string>();
            var paths = new List<string>() {root.Name.LocalName};
            Traverse(root, paths, joinedStrs);

            joinedStrs.ForEach(Console.WriteLine);

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }

        private static void Traverse(XElement root, List<string> paths, List<string> joinedStrs)
        {
            if (root == null) return;
            joinedStrs.Add(string.Join(@"\", paths));
            foreach (var sub in root.Elements())
            {
                paths.Add(sub.Name.LocalName);
                Traverse(sub, paths, joinedStrs);
                paths.RemoveAt(paths.Count-1);
            }
        }

    }
}
