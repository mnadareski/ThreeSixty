﻿using System;
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
					force = true;
				else if (File.Exists(arg))
					files.Add(arg);
				else if (Directory.Exists(arg))
					files.AddRange(Directory.EnumerateFiles(arg, "*", SearchOption.AllDirectories));
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

            // Get the full file path, in case it was not provided
            path = Path.GetFullPath(path);

            // Get a list of all images that match the existing file size
            long filesize = new FileInfo(path).Length;
            var matchingImages = FloppyImage.GetMatchingImages(filesize);			

            // If we got no matches, we can do nothing
			if (matchingImages.Count == 0)
			{
				Console.WriteLine("File '{0}' was not a recognized file size: {1}", path, filesize);
				return;
			}

            // Loop through the returned images and create files based on those
            foreach (var image in matchingImages)
            {
                string extension = $".{image.PhysicalSize}.{image.Density}.{image.Sides}sides";
                string outpath = path + extension;

                // Check to see if the image is truely the incorrect size (second track should be null)
                using (BinaryReader br = new BinaryReader(File.OpenRead(path)))
                {
                    br.ReadBytes(image.TrackSize);
                    byte[] buffer = br.ReadBytes(image.TrackSize);

                    // TODO: Improve detection of corrupt, supposed-to-be-blank tracks
                    if (buffer.Any(b => b != 0x00) && !force)
                    {
                        Console.WriteLine("File '{0}' was already a valid image", path);
                        return;
                    }
                }

                // Create the output file
                using (BinaryReader br = new BinaryReader(File.OpenRead(path)))
                using (BinaryWriter bw = new BinaryWriter(File.OpenWrite(outpath)))
                {
                    bool even = true;
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        byte[] buffer = br.ReadBytes(image.TrackSize);
                        if (even)
                        {
                            bw.Write(buffer);
                        }

                        even = !even;
                    }
                }

                Console.WriteLine($"File '{path}' was converted to a {image.TracksPerSide}-track image");
            }
		}
	}
}
