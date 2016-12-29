namespace AnchorMapParser
{
	public class MapData
	{
		private int _Width;
		private int _Height;
		public  int[,] Data;

		public int Width
		{
			get { return _Width; }
			set { _Width = value; }
		}

		public int Height
		{
			get { return _Height; }
			set { _Height = value; }
		}

		public MapData(int width, int height, int sqht, int sqwd)
		{
			Width = width;
			Height = height;
			Data = new int[Width / sqwd, Height / sqht];
		}
	}
}