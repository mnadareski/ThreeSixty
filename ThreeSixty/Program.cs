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

			// Get format-specific pieces
			long filesize = new FileInfo(path).Length;
			string extension;
			int trackSize;
			int tracks;

			if (filesize == EightSDDS.Capacity)
			{
				extension = "." + EightSDSS.Capacity;
				trackSize = EightSDDS.TrackSize;
				tracks = EightSDSS.TracksPerSide * EightSDSS.Sides;
			}
			else if (filesize == EightDDDS.Capacity)
			{
				extension = "." + EightDDSS.Capacity;
				trackSize = EightDDDS.TrackSize;
				tracks = EightDDSS.TracksPerSide * EightDDSS.Sides;
			}
			else if (filesize == FiveTwoFiveDDDS.Capacity)
			{
				extension = "." + FiveTwoFiveDDSS.Capacity;
				trackSize = FiveTwoFiveDDDS.TrackSize;
				tracks = FiveTwoFiveDDSS.TracksPerSide * FiveTwoFiveDDSS.Sides;
			}
			else if (filesize == FiveTwoFiveDDDS9S.Capacity)
			{
				extension = "." + FiveTwoFiveDDSS9S.Capacity;
				trackSize = FiveTwoFiveDDDS9S.TrackSize;
				tracks = FiveTwoFiveDDSS9S.TracksPerSide * FiveTwoFiveDDSS9S.Sides;
			}
			else if (filesize == FiveTwoFiveQDDS.Capacity)
			{
				extension = "." + FiveTwoFiveQDSS.Capacity;
				trackSize = FiveTwoFiveQDDS.TrackSize;
				tracks = FiveTwoFiveQDSS.TracksPerSide * FiveTwoFiveQDSS.Sides;
			}
			else if (filesize == ThreeFiveDDDS.Capacity)
			{
				extension = "." + FiveTwoFiveDDDS.Capacity;
				trackSize = ThreeFiveDDDS.TrackSize;
				tracks = FiveTwoFiveDDDS.TracksPerSide * FiveTwoFiveDDDS.Sides;
			}
			else if (filesize == ThreeFiveDDDS9S.Capacity)
			{
				extension = "." + FiveTwoFiveDDDS9S.Capacity;
				trackSize = ThreeFiveDDDS9S.TrackSize;
				tracks = FiveTwoFiveDDDS9S.TracksPerSide * FiveTwoFiveDDDS9S.Sides;
			}
			else
			{
				Console.WriteLine("File '{0}' was not a recognized file size: {1}", path, filesize);
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
