using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThreeSixty
{
	public class Program
	{
		static void Main(string[] args)
		{
			bool force = false;
			List<string> files = new List<string>();
			foreach (string arg in args)
			{
				if (arg == "-f" || arg == "--force")
				{
					force = true;
				}
				else if (File.Exists(arg))
				{
					files.Add(arg);
				}
				else if (Directory.Exists(arg))
				{
					files.AddRange(Directory.EnumerateFiles(arg, "*", SearchOption.AllDirectories));
				}
			}

			foreach (string file in files)
			{
				Convert(file, force);
			}

			Console.ReadLine();
		}

		private static void Convert(string path, bool force)
		{
			// Ensure the file exists
			if (!File.Exists(path))
			{
				Console.WriteLine("File '{0}' did not exist", path);
				return;
			}

			// Check that the file size matches an 80-track image
			FileInfo fi = new FileInfo(path);
			if (fi.Length != ThreeFiveDDDS.Capacity && !force)
			{
				Console.WriteLine("File '{0}' was not a valid 80-track file size: {1} {2}", path, ThreeFiveDDDS.Capacity, fi.Length);
				return;
			}

			// Get format-specific pieces
			string extension;
			int trackSize;
			if (fi.Length == ThreeFiveDDDS.Capacity)
			{
				extension = "." + FiveTwoFiveDDDS.Capacity.ToString();
				trackSize = FiveTwoFiveDDDS.TrackSize;
			}
			else if (force)
			{
				extension = ".forced";
				trackSize = FiveTwoFiveDDDS.TrackSize;
			}
			else
			{
				Console.WriteLine("File '{0}' was not a valid 80-track file size");
				return;
			}

			// Get the output path
			path = Path.GetFullPath(path);
			string newpath = path + extension;

			// Check to see if the image is truely the incorrect size (second sector should be null)
			using (BinaryReader br = new BinaryReader(File.OpenRead(path)))
			{
				br.ReadBytes(trackSize);
				byte[] buffer = br.ReadBytes(trackSize);

				if (buffer.Any(b => b != 0x00) && !force)
				{
					Console.WriteLine("File '{0}' was a valid 80-track image", path);
					return;
				}
			}
			
			// Create the output file
			using (BinaryReader br = new BinaryReader(File.OpenRead(path)))
			using (BinaryWriter bw = new BinaryWriter(File.OpenWrite(newpath)))
			{
				bool even = true;
				while (br.BaseStream.Position < br.BaseStream.Length)
				{
					byte[] buffer = br.ReadBytes(trackSize);
					if (even)
					{
						bw.Write(buffer);
					}

					even = !even;
				}
			}

			Console.WriteLine("File '{0}' was converted to a 40-track image", path);
		}
	}
}
