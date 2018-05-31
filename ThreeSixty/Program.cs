using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeSixty
{
	class Program
	{
		private const int _sectorSize = 0x2400;

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
			if (!File.Exists(path))
			{
				Console.WriteLine("File '{0}' did not exist", path);
				return;
			}

			FileInfo fi = new FileInfo(path);
			if (fi.Length != 720 * 1024)
			{
				Console.WriteLine("File '{0}' was not 720k", path);
				return;
			}

			path = Path.GetFullPath(path);
			string newpath = path + ".360";

			using (BinaryReader br = new BinaryReader(File.OpenRead(path)))
			{
				br.ReadBytes(_sectorSize);
				byte[] buffer = br.ReadBytes(_sectorSize);

				if (buffer.Any(b => b != 0x00))
				{
					Console.WriteLine("File '{0}' was a valid 720k image", path);
					return;
				}
			}

			using (BinaryReader br = new BinaryReader(File.OpenRead(path)))
			using (BinaryWriter bw = new BinaryWriter(File.OpenWrite(newpath)))
			{
				bool even = true;
				while (br.BaseStream.Position < br.BaseStream.Length)
				{
					byte[] buffer = br.ReadBytes(_sectorSize);
					if (even)
					{
						bw.Write(buffer);
					}

					even = !even;
				}
			}

			Console.WriteLine("File '{0}' was converted to a 360k image", path);
		}
	}
}
