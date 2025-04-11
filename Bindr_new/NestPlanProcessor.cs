using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bindr_new
{
    public class NestPlanProcessor
    {

        public List<List<string>> ParseNestPlanFileFast(string filePath)
        {
            
            var results = new List<List<string>>();
            string planId = "";
            string fileName = System.IO.Path.GetFileName(filePath);
            DateTime fileCreationDate = File.GetCreationTime(filePath);  // Get file's creation date

            using (var reader = new StreamReader(filePath))
            {
                string line;
                string pendingPartInfo = null;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("PlanId=") && string.IsNullOrEmpty(planId))
                    {
                        planId = ExtractQuotedValueSimple(line, "PlanId");
                    }
                    else if (line.Contains("PartInfo="))
                    {
                        pendingPartInfo = ExtractQuotedValueSimple(line, "PartInfo");
                    }
                    else if (line.Contains("PI_PartProgramQty=") && pendingPartInfo != null)
                    {
                        string qty = ExtractValue(line, "PI_PartProgramQty");

                        // Add the new row including the file creation date
                        results.Add(new List<string> { fileName, planId, pendingPartInfo, qty, fileCreationDate.ToString("MM-dd-yyyy HH:mm:ss") });
                        pendingPartInfo = null;
                    }
                    else if (line.Contains("G70")) break;
                }
            }

            return results;
        }






        private string ExtractValue(string line, string key)
        {
            try
            {
                int start = line.IndexOf($"{key}=");
                if (start == -1) return "";

                start += key.Length + 1;
                int end;

                if (line[start] == '"') // quoted string
                {
                    start++;
                    end = line.IndexOf('"', start);
                }
                else // numeric value or unquoted
                {
                    end = line.IndexOfAny(new char[] { ' ', ')', ']' }, start);
                }

                if (end == -1) return line.Substring(start); // fallback
                return line.Substring(start, end - start);
            }
            catch
            {
                return "";
            }
        }

        private string ExtractQuotedValueSimple(string line, string key)
        {
            try
            {
                var search = key + "=\\\"";
                var start = line.IndexOf(search);
                if (start == -1) return "";

                start += search.Length;
                var end = line.IndexOf("\\\"", start);
                if (end == -1) return "";

                return line.Substring(start, end - start);
            }
            catch
            {
                return "";
            }
        }



    }
}
