using System;
using System.Drawing;
using System.Web.Script.Serialization;
using System.IO;
using System.Threading;

namespace AnchorMapParser
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			string FilePath;

			Console.WriteLine("Enter Image File Name:");
			FilePath = Console.ReadLine();

			ln();
			Console.WriteLine("Loading Image...");
			Bitmap map = new Bitmap(FilePath);

			int width = map.Width;
			int height = map.Height;
			int squarewidth;
			int squareheight;

			Console.WriteLine($"Width: {width} Height: {height}");
			Console.WriteLine("Enter Grid Sizes:");
			ln();

			Console.WriteLine("Width:");
			int.TryParse(Console.ReadLine(), out squarewidth);
			Console.WriteLine("Height:");
			int.TryParse(Console.ReadLine(), out squareheight);

			ln();
			Console.WriteLine("Parsing Image");

			MapData mapData = new MapData(map.Width, map.Height, squareheight, squarewidth);

			for (int i = 0; i * squarewidth < map.Width; i++)
			{
				for (int j = 0; j * squareheight < map.Height; j++)
				{
					var sample = map.GetPixel(i * squarewidth, j * squareheight);
					string name = $"{sample.R},{sample.G},{sample.B}";

					if (sample.R == 238 && sample.G == 195 && sample.B == 154) //sand
					{
						mapData.Data[i, j] = 0;
					}
					else if (sample.R == 241 && sample.G == 188 && sample.B == 139) //wet sand
					{
						mapData.Data[i, j] = 1;
					}
					else if (sample.R == 91 && sample.G == 110 && sample.B == 225) //Shallow water
					{
						mapData.Data[i, j] = 2;
					}
					else if (sample.R == 91 && sample.G == 100 && sample.B == 234) //deep water
					{
						mapData.Data[i, j] = 3;
					}
					else if (sample.R == 75 && sample.G == 105 && sample.B == 47) //Grass
					{
						mapData.Data[i, j] = 4;
					}
					else if (sample.R == 255 && sample.G == 255 && sample.B == 255) //White
					{
						mapData.Data[i, j] = 5;
					}
					else
					{
						mapData.Data[i, j] = 999;
					}

					int tester = j;

					if (tester++  * squareheight  >= map.Height )
					{
						j = map.Height + squareheight;
					}


				}
			}
			Thread.Sleep(2000);
			ln();

			Console.WriteLine("Parsed Image");
			Console.WriteLine("Creating new MapData...");
			ln();

			Thread.Sleep(2000);
			Console.WriteLine("Serializing Data...");
			JavaScriptSerializer ser = new JavaScriptSerializer();
			string outputText = ser.Serialize(mapData);
			Thread.Sleep(2000);
			Console.WriteLine("Writing to JSON");
			Thread.Sleep(2000);
			File.WriteAllText("Output.json", outputText);
			Console.WriteLine("Map Parsed");
			ln();

			Console.WriteLine("Map Parser Finished");
			Console.ReadKey();
		}

		public static void ln()
		{
			Console.WriteLine();
		}
	}
}