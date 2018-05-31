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
		static void Main(string[] args)
		{
			foreach (string arg in args)
			{
				Convert(arg);
			}
		}

		private static void Convert(string path)
		{
			if (!File.Exists(path))
			{
				return;
			}

			path = Path.GetFullPath(path);
			string newpath = path + ".360";

			// TODO: Currently no checking to see if this is a valid 360k floppy file
			using (BinaryReader br = new BinaryReader(File.OpenRead(path)))
			using (BinaryWriter bw = new BinaryWriter(File.OpenWrite(newpath)))
			{
				bool even = true;
				while (br.BaseStream.Position < br.BaseStream.Length)
				{
					byte[] buffer = br.ReadBytes(0x2400);
					if (even)
					{
						bw.Write(buffer);
					}
					
					even = !even;
				}
			}
		}
	}
}
