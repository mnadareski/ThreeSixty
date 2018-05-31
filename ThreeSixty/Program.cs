using System;
using System.IO;
using System.Linq;

namespace ThreeSixty
{
	public class Program
	{
		private const int _fiveTwoFiveFortyTrackFileSize = 360 * 1024;
		private const int _fiveTwoFiveEightyTrackFileSize = 720 * 1024;
		private const int _fiveTwoFiveSectorSize = 0x2400;
		private const string _fiveTwoFiveExtension = ".360";

		private const int _threeFiveFortyTrackFileSize = 720 * 1024;
		private const int _threeFiveEightyTrackFileSize = 1440 * 1024;
		private const int _threeFiveSectorSize = 0x2400; // TODO: Verify this
		private const string _threeFiveExtension = ".720";

		static void Main(string[] args)
		{
			foreach (string arg in args)
			{
				Convert(arg);
			}

			Console.ReadLine();
		}

		private static void Convert(string path)
		{
			// Ensure the file exists
			if (!File.Exists(path))
			{
				Console.WriteLine("File '{0}' did not exist", path);
				return;
			}

			// Check that the file size matches an 80-track image
			FileInfo fi = new FileInfo(path);
			if (fi.Length != _fiveTwoFiveEightyTrackFileSize
				&& fi.Length != _threeFiveEightyTrackFileSize)
			{
				Console.WriteLine("File '{0}' was not a valid 80-track file size", path);
				return;
			}

			// Get the output path
			path = Path.GetFullPath(path);
			string newpath = path;
			int sectorSize = 0;
			switch (fi.Length)
			{
				case _fiveTwoFiveEightyTrackFileSize:
					newpath += _fiveTwoFiveExtension;
					sectorSize = _fiveTwoFiveSectorSize;
					break;
				case _threeFiveEightyTrackFileSize:
					newpath += _threeFiveExtension;
					sectorSize = _threeFiveSectorSize;
					break;
				default:
					Console.WriteLine("File '{0}' was not a valid 80-track file size");
					return;
			}

			// Check to see if the image is truely the incorrect size (second sector should be null)
			using (BinaryReader br = new BinaryReader(File.OpenRead(path)))
			{
				br.ReadBytes(sectorSize);
				byte[] buffer = br.ReadBytes(sectorSize);

				if (buffer.Any(b => b != 0x00))
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
					byte[] buffer = br.ReadBytes(sectorSize);
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
