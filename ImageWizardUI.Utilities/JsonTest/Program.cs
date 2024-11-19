// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json.Linq;

Console.WriteLine("Testing JSON!");

var rootPAth = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "test.json";

JObject root = new JObject();

root["Animation File Name"] = "Some animation file name";
root["img. count"] = 20;
root["Use Offsets"] = true;
root["Offsets"] = new { Vertical = 8, Horizontal = 3 }.ToString();
root["Use Resize"] = false;
root["Img. size"] = new { Width = 200, Height = 200 }.ToString();

string str = root.ToString();

File.WriteAllText(rootPAth, str);

Console.WriteLine("Finish!");
