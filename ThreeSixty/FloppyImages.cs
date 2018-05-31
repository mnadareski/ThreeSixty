using System.Text;

namespace ThreeSixty
{
	#region 8-inch

	public class EightSDSS
	{
		public static PhysicalSize PhysicalSize = PhysicalSize.EightInch;
		public static Density Density = Density.Single;
		public static int BytesPerSector = 128;
		public static int SectorsPerTrack = 26;
		public static int TracksPerSide = 77;
		public static int Sides = 1;
		public static int RPM = 360;
		public static DiskEncoding Encoding = DiskEncoding.MFM;

		public static int TrackSize => BytesPerSector * SectorsPerTrack;
		public static int Capacity => BytesPerSector * SectorsPerTrack * TracksPerSide * Sides;
	}

	public class EightSDDS
	{
		public static PhysicalSize PhysicalSize = PhysicalSize.EightInch;
		public static Density Density = Density.Single;
		public static int BytesPerSector = 128;
		public static int SectorsPerTrack = 26;
		public static int TracksPerSide = 77;
		public static int Sides = 2;
		public static int RPM = 360;
		public static DiskEncoding Encoding = DiskEncoding.MFM;

		public static int TrackSize => BytesPerSector * SectorsPerTrack;
		public static int Capacity => BytesPerSector * SectorsPerTrack * TracksPerSide * Sides;
	}

	public class EightDDSS
	{
		public static PhysicalSize PhysicalSize = PhysicalSize.EightInch;
		public static Density Density = Density.Double;
		public static int BytesPerSector = 1024;
		public static int SectorsPerTrack = 8;
		public static int TracksPerSide = 77;
		public static int Sides = 1;
		public static int RPM = 360;
		public static DiskEncoding Encoding = DiskEncoding.MFM;

		public static int TrackSize => BytesPerSector * SectorsPerTrack;
		public static int Capacity => BytesPerSector * SectorsPerTrack * TracksPerSide * Sides;
	}

	public class EightDDDS
	{
		public static PhysicalSize PhysicalSize = PhysicalSize.EightInch;
		public static Density Density = Density.Double;
		public static int BytesPerSector = 1024;
		public static int SectorsPerTrack = 8;
		public static int TracksPerSide = 77;
		public static int Sides = 2;
		public static int RPM = 360;
		public static DiskEncoding Encoding = DiskEncoding.MFM;

		public static int TrackSize => BytesPerSector * SectorsPerTrack;
		public static int Capacity => BytesPerSector * SectorsPerTrack * TracksPerSide * Sides;
	}

	#endregion

	#region 5.25-inch

	public class FiveTwoFiveDDSS
	{
		public static PhysicalSize PhysicalSize = PhysicalSize.FiveTwoFiveInch;
		public static Density Density = Density.Double;
		public static int BytesPerSector = 512;
		public static int SectorsPerTrack = 8; // Variant: 9
		public static int TracksPerSide = 40;
		public static int Sides = 1;
		public static int RPM = 300;
		public static DiskEncoding Encoding = DiskEncoding.MFM;

		public static int TrackSize => BytesPerSector * SectorsPerTrack;
		public static int Capacity => BytesPerSector * SectorsPerTrack * TracksPerSide * Sides;
	}

	public class FiveTwoFiveDDDS
	{
		public static PhysicalSize PhysicalSize = PhysicalSize.FiveTwoFiveInch;
		public static Density Density = Density.Double;
		public static int BytesPerSector = 512;
		public static int SectorsPerTrack = 8; // Variant: 9
		public static int TracksPerSide = 40;
		public static int Sides = 2;
		public static int RPM = 300;
		public static DiskEncoding Encoding = DiskEncoding.MFM;

		public static int TrackSize => BytesPerSector * SectorsPerTrack;
		public static int Capacity => BytesPerSector * SectorsPerTrack * TracksPerSide * Sides;
	}

	public class FiveTwoFiveQDSS
	{
		public static PhysicalSize PhysicalSize = PhysicalSize.FiveTwoFiveInch;
		public static Density Density = Density.Quad;
		public static int BytesPerSector = 512;
		public static int SectorsPerTrack = 8;
		public static int TracksPerSide = 80;
		public static int Sides = 1;
		public static int RPM = 300;
		public static DiskEncoding Encoding = DiskEncoding.MFM;

		public static int TrackSize => BytesPerSector * SectorsPerTrack;
		public static int Capacity => BytesPerSector * SectorsPerTrack * TracksPerSide * Sides;
	}

	public class FiveTwoFiveQDDS
	{
		public static PhysicalSize PhysicalSize = PhysicalSize.FiveTwoFiveInch;
		public static Density Density = Density.Quad;
		public static int BytesPerSector = 512;
		public static int SectorsPerTrack = 8;
		public static int TracksPerSide = 80;
		public static int Sides = 2;
		public static int RPM = 300;
		public static DiskEncoding Encoding = DiskEncoding.MFM;

		public static int TrackSize => BytesPerSector * SectorsPerTrack;
		public static int Capacity => BytesPerSector * SectorsPerTrack * TracksPerSide * Sides;
	}

	public class FiveTwoFiveHDDS
	{
		public static PhysicalSize PhysicalSize = PhysicalSize.FiveTwoFiveInch;
		public static Density Density = Density.High;
		public static int BytesPerSector = 512;
		public static int SectorsPerTrack =	15;
		public static int TracksPerSide = 80;
		public static int Sides = 2;
		public static int RPM = 360;
		public static DiskEncoding Encoding = DiskEncoding.MFM;

		public static int TrackSize => BytesPerSector * SectorsPerTrack;
		public static int Capacity => BytesPerSector * SectorsPerTrack * TracksPerSide * Sides;
	}

	#endregion

	#region 3.5-inch

	public class ThreeFiveDDSS
	{
		public static PhysicalSize PhysicalSize = PhysicalSize.ThreeFiveInch;
		public static Density Density = Density.Double;
		public static int BytesPerSector = 512;
		public static int SectorsPerTrack = 8; // Variant: 9
		public static int TracksPerSide = 80;
		public static int Sides = 1;
		public static int RPM = 300;
		public static DiskEncoding Encoding = DiskEncoding.MFM;

		public static int TrackSize => BytesPerSector * SectorsPerTrack;
		public static int Capacity => BytesPerSector * SectorsPerTrack * TracksPerSide * Sides;
	}

	public class ThreeFiveDDDS
	{
		public static PhysicalSize PhysicalSize = PhysicalSize.ThreeFiveInch;
		public static Density Density = Density.Double;
		public static int BytesPerSector = 512;
		public static int SectorsPerTrack = 8; // Variant: 9
		public static int TracksPerSide = 80;
		public static int Sides = 2;
		public static int RPM = 300;
		public static DiskEncoding Encoding = DiskEncoding.MFM;

		public static int TrackSize => BytesPerSector * SectorsPerTrack;
		public static int Capacity => BytesPerSector * SectorsPerTrack * TracksPerSide * Sides;
	}

	public class ThreeFiveHDDS
	{
		public static PhysicalSize PhysicalSize = PhysicalSize.ThreeFiveInch;
		public static Density Density = Density.High;
		public static int BytesPerSector = 512;
		public static int SectorsPerTrack = 18; // Variant: 21
		public static int TracksPerSide = 80; // Variant: 82 (only with 21 SPT)
		public static int Sides = 2;
		public static int RPM = 300;
		public static DiskEncoding Encoding = DiskEncoding.MFM;

		public static int TrackSize => BytesPerSector * SectorsPerTrack;
		public static int Capacity => BytesPerSector * SectorsPerTrack * TracksPerSide * Sides;
	}

	public class ThreeFiveEHDDS
	{
		public static PhysicalSize PhysicalSize = PhysicalSize.ThreeFiveInch;
		public static Density Density = Density.ExtraHigh;
		public static int BytesPerSector = 512;
		public static int SectorsPerTrack = 36;
		public static int TracksPerSide = 80;
		public static int Sides = 2;
		public static int RPM = 300;
		public static DiskEncoding Encoding = DiskEncoding.MFM;

		public static int TrackSize => BytesPerSector * SectorsPerTrack;
		public static int Capacity => BytesPerSector * SectorsPerTrack * TracksPerSide * Sides;
	}

	#endregion
}
