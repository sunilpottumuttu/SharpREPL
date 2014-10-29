using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Roslyn.Compilers;
using Roslyn.Scripting.CSharp;
using Roslyn.Scripting;


namespace CSharpREPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = CONSTANTS.WINDOWTITLE;
            Console.SetWindowPosition(0, 0);
            Console.WindowHeight = Console.LargestWindowHeight/2;
            Console.WindowWidth = Console.LargestWindowWidth/2;
            Console.WriteLine(CONSTANTS.BANNERMESSAGE);

            var engine = new ScriptEngine();
            var session = engine.CreateSession();

            #region Add Assemblies SearchPaths
            string[] searchPaths = File.ReadAllLines("SearchPaths.txt");
            for (int i = 0; i < searchPaths.Length; i++)
            {
                if (searchPaths[i].Trim() == "")
                {
                    //Ignore empty lines
                }
                else
                {
                    try
                    {
                        session.SetReferenceSearchPaths(searchPaths[i]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            #endregion

            #region Reference Assemblies 
            string[] references = File.ReadAllLines("References.txt");
            for (int i = 0; i < references.Length; i++)
            {
                if (references[i].Trim() == "")
                {
                    //Ignore empty lines
                }
                else
                {
                    try
                    {
                        session.AddReference(references[i]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            #endregion

            #region Import Namespaces
            string[] namespaces = File.ReadAllLines("Namespaces.txt");
            for (int i = 0; i < namespaces.Length; i++)
            {
                if (namespaces[i].Trim() == "")
                {
                    //Ignore empty lines
                }
                else
                {
                    try
                    {
                        session.ImportNamespace(namespaces[i]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            #endregion
            
            for(;;)
            {
                try
                {
                    Console.Write(CONSTANTS.PROMPT);
                    string code = Console.ReadLine();
                    
                    session.Execute(code);
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }



        }
    }
}
