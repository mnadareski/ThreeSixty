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
			bool force = true; // We assume that there are corrupt sectors by default
			List<string> files = new List<string>();
			foreach (string arg in args)
			{
				if (arg == "-nf" || arg == "--no-force")
				{
					force = false;
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
			if (fi.Length != ThreeFiveDDDS9S.Capacity
				&& fi.Length != FiveTwoFiveDDDS.Capacity
				&& !force)
			{
				Console.WriteLine("File '{0}' was not a valid 80-track file size: {1} {2}", path, ThreeFiveDDDS.Capacity, fi.Length);
				return;
			}

			// Get format-specific pieces
			string extension;
			int trackSize;
			int tracks;
			if (fi.Length == ThreeFiveDDDS9S.Capacity)
			{
				extension = "." + FiveTwoFiveDDDS9S.Capacity;
				trackSize = FiveTwoFiveDDDS9S.TrackSize * 2;
				tracks = FiveTwoFiveDDDS9S.TracksPerSide * FiveTwoFiveDDDS9S.Sides;
			}
			else if (fi.Length == FiveTwoFiveDDDS.Capacity)
			{
				extension = "." + FiveTwoFiveDDSS.Capacity;
				trackSize = FiveTwoFiveDDSS.TrackSize;
				tracks = FiveTwoFiveDDSS.TracksPerSide * FiveTwoFiveDDSS.Sides;
			}
			else
			{
				Console.WriteLine("File '{0}' was not a recognized file size");
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
					Console.WriteLine("File '{0}' was already a valid image", path);
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

			Console.WriteLine("File '{0}' was converted to a {1}-track image", path, tracks);
		}
	}
}
