using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bindr
{
    public class NestPlanProcessor
    {

        public List<List<string>> ParseNestPlanFileFast(string filePath)
        {
            var results = new List<List<string>>();
            string planId = "";
            string fileName = System.IO.Path.GetFileName(filePath);
            DateTime fileCreationDate = File.GetCreationTime(filePath);
            string material = "";
            string thickness = "";
            string plannedTime = "";

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
                    else if (line.Contains("Material="))
                    {
                        material = ExtractQuotedValueSimple(line, "Material");
                    }
                    else if (line.Contains("Thickness="))
                    {
                        thickness = ExtractValue(line, "Thickness");
                    }
                    else if (line.Contains("PlannedTime="))
                    {
                        plannedTime = ExtractPlannedTimeValue(line, "PlannedTime=");
                        
                        
                    }
                    else if (line.Contains("PI_PartProgramQty=") && pendingPartInfo != null)
                    {
                        string qty = ExtractValue(line, "PI_PartProgramQty");
                        // Add all the requested fields
                        results.Add(new List<string> {
                    fileName,
                    planId,
                    pendingPartInfo,
                    qty,
                    material,
                    thickness,
                    plannedTime,
                    fileCreationDate.ToString("MM-dd-yyyy HH:mm:ss")
                });

                        // Reset part-specific values for next part
                        pendingPartInfo = null;
                        //material = "";
                        //thickness = "";
                        //plannedTime = "";
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


        private string ExtractPlannedTimeValue(string line, string key)
        {
            try
            {
                // Look for the pattern (PlannedTime=
                int startIndex = line.IndexOf("(" + key + "=");
                if (startIndex == -1) return "";

                // Move past the key name and equals sign
                startIndex += key.Length + 2; // +2 for the '(' and '='

                // Find the closing parenthesis
                int endIndex = line.IndexOf(')', startIndex);
                if (endIndex == -1) return line.Substring(startIndex); // fallback

                return line.Substring(startIndex, endIndex - startIndex);
            }
            catch
            {
                return "";
            }
        }


    }
}
