using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ThreeSixty
{
    /// <summary>
    /// Single floppy disk image
    /// </summary>
    public class FloppyImage : IEquatable<FloppyImage>
    {
        /// <summary>
        /// Physical format size
        /// </summary>
        public PhysicalSize PhysicalSize { get; set; }

        /// <summary>
        /// Reported disk density
        /// </summary>
        public Density Density { get; set; }

        /// <summary>
        /// Number of bytes per disk sector
        /// </summary>
        public int BytesPerSector { get; set; }

        /// <summary>
        /// Number of sectors per disk track
        /// </summary>
        public int SectorsPerTrack { get; set; }

        /// <summary>
        /// Number of tracks per disk side
        /// </summary>
        public int TracksPerSide { get; set; }

        /// <summary>
        /// Number of sides for the disk
        /// </summary>
        public int Sides { get; set; }

        /// <summary>
        /// Read/Write RPM
        /// </summary>
        public int RPM { get; set; }

        /// <summary>
        /// Disk format encoding
        /// </summary>
        public DiskEncoding Encoding { get; set; }

        /// <summary>
        /// Size of a track in bytes
        /// </summary>
        public int TrackSize => BytesPerSector * SectorsPerTrack;

        /// <summary>
        /// Total disk capacity in bytes
        /// </summary>
        public int Capacity => BytesPerSector * SectorsPerTrack * TracksPerSide * Sides;

        /// <summary>
        /// Determine equality between two images
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(FloppyImage other)
        {
            return this.PhysicalSize == other.PhysicalSize
                && this.Density == other.Density
                && this.BytesPerSector == other.BytesPerSector
                && this.SectorsPerTrack == other.SectorsPerTrack
                && this.TracksPerSide == other.TracksPerSide
                && this.Sides == other.Sides
                && this.RPM == other.RPM
                && this.Encoding == other.Encoding;
        }

        /// <summary>
        /// Get a list of standard floppy images that match a given file size
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public static List<FloppyImage> GetMatchingImages(long fileSize)
        {
            var floppyImageTypes = FindImageTypes();
            var floppyImages = new List<FloppyImage>();
            foreach (var imageType in floppyImageTypes)
            {
                var image = (FloppyImage)Activator.CreateInstance(imageType);
                if (image.Capacity == fileSize)
                    floppyImages.Add(image);
            }

            return floppyImages;
        }

        /// <summary>
        /// Find all derived floppy image types
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Type> FindImageTypes()
        {
            var baseType = typeof(FloppyImage);
            return Assembly.GetAssembly(baseType)
                .GetTypes()
                .Where(t => t != baseType && baseType.IsAssignableFrom(t));
        }
    }

    #region 8-inch

    /// <summary>
    /// IBM PC-compatible 8-inch, Single-Density, Single-Sided, 77-Track image
    /// </summary>
    public class IBMPC8SDSS77Track : FloppyImage
    {
        public IBMPC8SDSS77Track()
        {
            PhysicalSize = PhysicalSize.EightInch;
            Density = Density.Single;
            BytesPerSector = 128;
            SectorsPerTrack = 26;
            TracksPerSide = 77;
            Sides = 1;
            RPM = 360;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// IBM PC-compatible 8-inch, Single-Density, Double-Sided, 77-Track image
    /// </summary>
    public class IBMPC8SDDS77Track : FloppyImage
    {
        public IBMPC8SDDS77Track()
        {
            PhysicalSize = PhysicalSize.EightInch;
            Density = Density.Single;
            BytesPerSector = 128;
            SectorsPerTrack = 26;
            TracksPerSide = 77;
            Sides = 2;
            RPM = 360;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// IBM PC-compatible 8-inch, Double-Density, Single-Sided, 77-Track image
    /// </summary>
    public class IBMPC8DDSS77Track : FloppyImage
    {
        public IBMPC8DDSS77Track()
        {
            PhysicalSize = PhysicalSize.EightInch;
            Density = Density.Double;
            BytesPerSector = 1024;
            SectorsPerTrack = 8;
            TracksPerSide = 77;
            Sides = 1;
            RPM = 360;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// IBM PC-compatible 8-inch, Double-Density, Single-Sided, 77-Track image
    /// </summary>
    public class IBMPC8DDDS77Track : FloppyImage
    {
        public IBMPC8DDDS77Track()
        {
            PhysicalSize = PhysicalSize.EightInch;
            Density = Density.Double;
            BytesPerSector = 1024;
            SectorsPerTrack = 8;
            TracksPerSide = 77;
            Sides = 2;
            RPM = 360;
            Encoding = DiskEncoding.MFM;
        }
    }

    #endregion

    #region 5.25-inch

    /// <summary>
    /// Acorn 5.25-inch, Single-Density, Single-Sided, 40-Track image
    /// </summary>
    public class Acorn525SDSS40Track : FloppyImage
    {
        public Acorn525SDSS40Track()
        {
            PhysicalSize = PhysicalSize.FiveTwoFiveInch;
            Density = Density.Single;
            BytesPerSector = 256;
            SectorsPerTrack = 10;
            TracksPerSide = 40;
            Sides = 1;
            RPM = 300;
            Encoding = DiskEncoding.FM;
        }
    }

    /// <summary>
    /// Acorn 5.25-inch, Single-Density, Single-Sided, 80-Track image
    /// </summary>
    public class Acorn525SDSS80Track : FloppyImage
    {
        public Acorn525SDSS80Track()
        {
            PhysicalSize = PhysicalSize.FiveTwoFiveInch;
            Density = Density.Single;
            BytesPerSector = 256;
            SectorsPerTrack = 10;
            TracksPerSide = 80;
            Sides = 1;
            RPM = 300;
            Encoding = DiskEncoding.FM;
        }
    }

    /// <summary>
    /// Acorn 5.25-inch, Double-Density, Single-Sided, 40-Track image
    /// </summary>
    public class Acorn525DDSS40Track : FloppyImage
    {
        public Acorn525DDSS40Track()
        {
            PhysicalSize = PhysicalSize.FiveTwoFiveInch;
            Density = Density.Double;
            BytesPerSector = 256;
            SectorsPerTrack = 16;
            TracksPerSide = 40;
            Sides = 1;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// Acorn 5.25-inch, Double-Density, Single-Sided, 80-Track image
    /// </summary>
    public class Acorn525DDSS80Track : FloppyImage
    {
        public Acorn525DDSS80Track()
        {
            PhysicalSize = PhysicalSize.FiveTwoFiveInch;
            Density = Density.Double;
            BytesPerSector = 256;
            SectorsPerTrack = 16;
            TracksPerSide = 80;
            Sides = 1;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// Acorn 5.25-inch, Double-Density, Double-Sided, 80-Track image
    /// </summary>
    public class Acorn525DDDS80Track : FloppyImage
    {
        public Acorn525DDDS80Track()
        {
            PhysicalSize = PhysicalSize.FiveTwoFiveInch;
            Density = Density.Double;
            BytesPerSector = 256;
            SectorsPerTrack = 16;
            TracksPerSide = 80;
            Sides = 2;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// Agat 5.25-inch, Double-Density, Double-Sided, 80-Track image
    /// </summary>
    public class Agat625DDDS80Track : FloppyImage
    {
        public Agat625DDDS80Track()
        {
            PhysicalSize = PhysicalSize.FiveTwoFiveInch;
            Density = Density.Double;
            BytesPerSector = 256;
            SectorsPerTrack = 21;
            TracksPerSide = 80;
            Sides = 2;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// IBM PC-compatible 5.25-inch, Double-Density, Single-Sided, 8 SPT, 40-Track image
    /// </summary>
    public class IBMPC525DDSS8SPT40Track : FloppyImage
    {
        public IBMPC525DDSS8SPT40Track()
        {
            PhysicalSize = PhysicalSize.FiveTwoFiveInch;
            Density = Density.Double;
            BytesPerSector = 512;
            SectorsPerTrack = 8;
            TracksPerSide = 40;
            Sides = 1;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// IBM PC-compatible 5.25-inch, Double-Density, Double-Sided, 8 SPT, 40-Track image
    /// </summary>
    public class IBMPC525DDDS8SPT40Track : FloppyImage
    {
        public IBMPC525DDDS8SPT40Track()
        {
            PhysicalSize = PhysicalSize.FiveTwoFiveInch;
            Density = Density.Double;
            BytesPerSector = 512;
            SectorsPerTrack = 8;
            TracksPerSide = 40;
            Sides = 2;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// IBM PC-compatible 5.25-inch, Double-Density, Single-Sided, 9 SPT, 40-Track image
    /// </summary>
    public class IBMPC525DDSS9SPT40Track : FloppyImage
    {
        public IBMPC525DDSS9SPT40Track()
        {
            PhysicalSize = PhysicalSize.FiveTwoFiveInch;
            Density = Density.Double;
            BytesPerSector = 512;
            SectorsPerTrack = 9;
            TracksPerSide = 40;
            Sides = 1;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// IBM PC-compatible 5.25-inch, Double-Density, Double-Sided, 9 SPT, 40-Track image
    /// </summary>
    public class IBMPC525DDDS9SPT40Track : FloppyImage
    {
        public IBMPC525DDDS9SPT40Track()
        {
            PhysicalSize = PhysicalSize.FiveTwoFiveInch;
            Density = Density.Double;
            BytesPerSector = 512;
            SectorsPerTrack = 9;
            TracksPerSide = 40;
            Sides = 2;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// IBM PC-compatible 5.25-inch, Quad-Density, Single-Sided, 80-Track image
    /// </summary>
    public class IBMPC525QDSS80Track : FloppyImage
    {
        public IBMPC525QDSS80Track()
        {
            PhysicalSize = PhysicalSize.FiveTwoFiveInch;
            Density = Density.Quad;
            BytesPerSector = 512;
            SectorsPerTrack = 8;
            TracksPerSide = 80;
            Sides = 1;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// IBM PC-compatible 5.25-inch, Quad-Density, Double-Sided, 80-Track image
    /// </summary>
    public class IBMPC525QDDS80Track : FloppyImage
    {
        public IBMPC525QDDS80Track()
        {
            PhysicalSize = PhysicalSize.FiveTwoFiveInch;
            Density = Density.Quad;
            BytesPerSector = 512;
            SectorsPerTrack = 8;
            TracksPerSide = 80;
            Sides = 2;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// IBM PC-compatible 5.25-inch, High-Density, Double-Sided, 80-Track image
    /// </summary>
    public class IBMPC525HDDS80Track : FloppyImage
    {
        public IBMPC525HDDS80Track()
        {
            PhysicalSize = PhysicalSize.FiveTwoFiveInch;
            Density = Density.High;
            BytesPerSector = 512;
            SectorsPerTrack = 15;
            TracksPerSide = 80;
            Sides = 2;
            RPM = 360;
            Encoding = DiskEncoding.MFM;
        }
    }

    #endregion

    #region 3.5-inch

    /// <summary>
    /// Acorn 3.5-inch, Double-Density, Double-Sided, 80-Track, 256 BPS image
    /// </summary>
    public class Acorn35DDDS80Track256BPS : FloppyImage
    {
        public Acorn35DDDS80Track256BPS()
        {
            PhysicalSize = PhysicalSize.ThreeFiveInch;
            Density = Density.Double;
            BytesPerSector = 256;
            SectorsPerTrack = 16;
            TracksPerSide = 80;
            Sides = 2;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// Acorn 3.5-inch, Double-Density, Double-Sided, 80-Track, 1024 BPS image
    /// </summary>
    public class Acorn35DDDS80Track1024BPS : FloppyImage
    {
        public Acorn35DDDS80Track1024BPS()
        {
            PhysicalSize = PhysicalSize.ThreeFiveInch;
            Density = Density.Double;
            BytesPerSector = 1024;
            SectorsPerTrack = 5;
            TracksPerSide = 80;
            Sides = 2;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// Acorn 3.5-inch, High-Density, Double-Sided, 80-Track
    /// </summary>
    public class Acorn35HDDS80Track : FloppyImage
    {
        public Acorn35HDDS80Track()
        {
            PhysicalSize = PhysicalSize.ThreeFiveInch;
            Density = Density.High;
            BytesPerSector = 1024;
            SectorsPerTrack = 10;
            TracksPerSide = 80;
            Sides = 2;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// IBM PC-compatible 3.5-inch, Double-Density, Single-Sided, 8 SPT, 80-Track image
    /// </summary>
    public class IBMPC35DDSS8SPT80Track : FloppyImage
    {
        public IBMPC35DDSS8SPT80Track()
        {
            PhysicalSize = PhysicalSize.ThreeFiveInch;
            Density = Density.Double;
            BytesPerSector = 512;
            SectorsPerTrack = 8;
            TracksPerSide = 80;
            Sides = 1;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// IBM PC-compatible 3.5-inch, Double-Density, Single-Sided, 9 SPT, 80-Track image
    /// </summary>
    public class IBMPC35DDSS9SPT80Track : FloppyImage
    {
        public IBMPC35DDSS9SPT80Track()
        {
            PhysicalSize = PhysicalSize.ThreeFiveInch;
            Density = Density.Double;
            BytesPerSector = 512;
            SectorsPerTrack = 9;
            TracksPerSide = 80;
            Sides = 1;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// IBM PC-compatible 3.5-inch, Double-Density, Double-Sided, 8 SPT, 80-Track image
    /// </summary>
    public class IBMPC35DDDS8SPT80Track : FloppyImage
    {
        public IBMPC35DDDS8SPT80Track()
        {
            PhysicalSize = PhysicalSize.ThreeFiveInch;
            Density = Density.Double;
            BytesPerSector = 512;
            SectorsPerTrack = 8;
            TracksPerSide = 80;
            Sides = 2;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// IBM PC-compatible 3.5-inch, Double-Density, Double-Sided, 9 SPT, 80-Track image
    /// </summary>
    public class IBMPC35DDDS9SPT80Track : FloppyImage
    {
        public IBMPC35DDDS9SPT80Track()
        {
            PhysicalSize = PhysicalSize.ThreeFiveInch;
            Density = Density.Double;
            BytesPerSector = 512;
            SectorsPerTrack = 9;
            TracksPerSide = 80;
            Sides = 2;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// IBM PC-compatible 3.5-inch, High-Density, Double-Sided, 18SPT, 80-Track image
    /// </summary>
    public class IBMPC35HDDS18SPT80Track : FloppyImage
    {
        public IBMPC35HDDS18SPT80Track()
        {
            PhysicalSize = PhysicalSize.ThreeFiveInch;
            Density = Density.High;
            BytesPerSector = 512;
            SectorsPerTrack = 18;
            TracksPerSide = 80;
            Sides = 2;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// IBM PC-compatible 3.5-inch, High-Density, Double-Sided, 21SPT, 80-Track image
    /// </summary>
    public class IBMPC35HDDS21SPT80Track : FloppyImage
    {
        public IBMPC35HDDS21SPT80Track()
        {
            PhysicalSize = PhysicalSize.ThreeFiveInch;
            Density = Density.High;
            BytesPerSector = 512;
            SectorsPerTrack = 21;
            TracksPerSide = 80;
            Sides = 2;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// IBM PC-compatible 3.5-inch, High-Density, Double-Sided, 82-Track image
    /// </summary>
    public class IBMPC35HDDS82Track : FloppyImage
    {
        public IBMPC35HDDS82Track()
        {
            PhysicalSize = PhysicalSize.ThreeFiveInch;
            Density = Density.High;
            BytesPerSector = 512;
            SectorsPerTrack = 21;
            TracksPerSide = 82;
            Sides = 2;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    /// <summary>
    /// IBM PC-compatible 3.5-inch, Extended-Density, Double-Sided, 80-Track image
    /// </summary>
    public class IBMPC35EDDS80Track : FloppyImage
    {
        public IBMPC35EDDS80Track()
        {
            PhysicalSize = PhysicalSize.ThreeFiveInch;
            Density = Density.Extended;
            BytesPerSector = 512;
            SectorsPerTrack = 36;
            TracksPerSide = 80;
            Sides = 2;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    #endregion

    #region 3-inch

    /// <summary>
    /// Amstrad CPC 3-inch, Double-Density, Single-Sided, 40-Track
    /// </summary>
    public class AmstradCPC3DDSS40Track : FloppyImage
    {
        public AmstradCPC3DDSS40Track()
        {
            PhysicalSize = PhysicalSize.ThreeInch;
            Density = Density.Double;
            BytesPerSector = 512;
            SectorsPerTrack = 9;
            TracksPerSide = 40;
            Sides = 1;
            RPM = 300;
            Encoding = DiskEncoding.MFM;
        }
    }

    #endregion
}
