using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeamlessHr.Service.Services
{
    public class ScriptLoader
    {
        public static string GetString(string name, Assembly assembly)
        {
            string data = null;
            var res = assembly.GetManifestResourceNames();
            string resName = res.FirstOrDefault(mf => mf.EndsWith(name));
            if (resName != null)
            {
                using (StreamReader sr = new StreamReader(assembly.GetManifestResourceStream(resName)))
                {
                    data = sr.ReadToEnd();
                    sr.Close();
                }
            }

            return data;
        }
        public static string GetString(string name)
        {
            return ScriptLoader.GetString(name, typeof(ScriptLoader).Assembly);
        }
    }
}
