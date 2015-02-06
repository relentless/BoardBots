using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BoardBots {
    internal class TypeLoader {
        public static List<Type> LoadTypesFromAssembliesIn(string folder) {
            var files = Directory.GetFiles(folder, "*.dll").Where( file => !file.EndsWith("BoardBots.Shared.dll"));

            var typeList = new List<Type>();

            foreach (string file in files) {
                try {
                    typeList.AddRange(Assembly.LoadFile(file).GetTypes());
                }
                catch (ReflectionTypeLoadException ex) {
                    foreach (var error in ex.LoaderExceptions) {
                        Console.WriteLine(error);
                    }

                    throw ex;
                }
                
            }

            return typeList;
        }
    }
}
