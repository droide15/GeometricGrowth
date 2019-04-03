using System;

public class Program
{
	public static void Main()
	{
		int rows = 6;
		int cols = 6;
		Region[,] region = new Region[rows, cols];
		
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				region[i, j] = new Region(i*100+100, i*100+200, j*100+100, j*100+200);
			}
		}
		
		for (int i = 0; i < rows; i++)
		{
			Console.Write("\n");
			for (int j = 0; j < cols; j++)
			{
				Console.Write(region[i, j].upLim+"-"+region[i, j].downLim+"x"+region[i, j].leftLim+"-"+region[i, j].rightLim+", ");
			}
			Console.Write("\n");
		}
		
		Collage collage = new Collage(cols, rows, region);
		
		while (collage.grow())
		{
			collage.stitchNext(region);
		}
	}	
}

class Collage
{
	int width;
	int height;
	int up;
	int down;
	int left;
	int right;
	int middleH;
	int middleV;
	int lengthH;
	int lengthV;
	
	public Collage(int width, int height, Region[,] region)
	{
		this.width = width;
		this.height = height;
		
		middleH = width - (int)(width/2) - 1;
		left = middleH;
		right = middleH;
		if (width%2 == 0)
		{
			right++;
		}
		lengthH = right - left + 1;
		
		middleV = height - (int)(height/2) - 1;
		up = middleV;
		down = middleV;
		if (height%2 == 0)
		{
			down++;
		}
		lengthV = down - up + 1;
		
		Console.WriteLine("stitching");
		
		for (int i = 0; i < lengthV; i++)
		{
			for (int j = 0; j < lengthH; j++)
			{
				Console.WriteLine("index: "+(up+i)+", "+(left+j));
				region[up+i, left+j].stitchReady = true;
			}
		}
		
		doStitch(region);
		Console.WriteLine("width: "+width);
		Console.WriteLine("height: "+height);
		Console.WriteLine("up: "+up);
		Console.WriteLine("down: "+down);
		Console.WriteLine("left: "+left);
		Console.WriteLine("right: "+right);
		Console.WriteLine("middleH: "+middleH);
		Console.WriteLine("middleV: "+middleV);
		Console.WriteLine("lengthH: "+lengthH);
		Console.WriteLine("lengthV: "+lengthV);
	}
	
	public void stitchNext(Region[,] region)
	{
		int startH = middleH;
		int endH = middleH;
		int startV = middleV;
		int endV = middleV;
		int growArea = lengthH*lengthV;
		char growDir = '*';
		
		if (width >= height)
		{
			growDir = 'V';
		}
		else
		{
			growDir = 'H';
		}
		
		if (width%2 == 0)
		{
			endH++;
		}
		if (height%2 == 0)
		{
			endV++;
		}
		
		while ((endH-startH)*(endV-startV) < growArea)
		{
			if (growDir == 'V')
			{
				Console.WriteLine("up: "+(up-1)+" down: "+(down+1)+" startH: "+startH+" endH: "+endH);
				Console.WriteLine("index: "+(up-1)+", "+startH);
				region[up-1, startH].stitchReady = true;
				if (endH-startH > 0)
				{
					Console.WriteLine("index: "+(up-1)+", "+endH);
					region[up-1, endH].stitchReady = true;
				}
				Console.WriteLine("index: "+(down+1)+", "+startH);
				region[down+1, startH].stitchReady = true;
				if (endH-startH > 0)
				{
					Console.WriteLine("index: "+(down+1)+", "+endH);
					region[down+1, endH].stitchReady = true;
				}
			}
			else if (growDir == 'H')
			{
				Console.WriteLine("startV: "+startV+" endV: "+endV+" left: "+(left-1)+" right: "+(right+1));
				Console.WriteLine("index: "+startV+", "+(left-1));
				region[startV, left-1].stitchReady = true;
				Console.WriteLine("index: "+startV+", "+(right+1));
				region[startV, right+1].stitchReady = true;
				if (endV-startV > 0)
				{
					Console.WriteLine("index: "+endV+", "+(left-1));
					region[endV, left-1].stitchReady = true;
					Console.WriteLine("index: "+endV+", "+(right+1));
					region[endV, right+1].stitchReady = true;
				}
			}
			else
			{
				Console.WriteLine("startV: "+startV+" endV: "+endV+" startH: "+startH+" endH: "+endH);
				Console.WriteLine("index: "+startV+", "+startH);
				region[startV, startH].stitchReady = true;
				Console.WriteLine("index: "+startV+", "+endH);
				region[startV, endH].stitchReady = true;
				Console.WriteLine("index: "+endV+", "+startH);
				region[endV, startH].stitchReady = true;
				Console.WriteLine("index: "+endV+", "+endH);
				region[endV, endH].stitchReady = true;
				startV--;
				endV++;
				startH--;
				endH++;
			}
			
			doStitch(region);

			if (width >= height)
			{
				if (growDir == 'V')
				{
					growDir = 'H';
				}
				else if (growDir == 'H')
				{
					growDir = '*';
					if (startH > left)
					{
						growDir = 'V';
					}
					startH--;
					endH++;
					startV--;
					endV++;
				}
				else
				{
					growDir = 'V';
				}
			}
			else
			{
				if (growDir == 'V')
				{
					growDir = '*';
					if (startV > up)
					{
						growDir = 'H';
					}
					startH--;
					endH++;
					startV--;
					endV++;
				}
				else if (growDir == 'H')
				{
					growDir = 'V';
				}
				else
				{
					growDir = 'H';
				}
			}
		}
	}
	
	public bool grow()
	{
		if (lengthH*lengthV < width*height)
		{
			if (lengthH*lengthV > 4)
			{
				left--;
				right++;
				up--;
				down++;
			}
			lengthH += 2;
			lengthV += 2;
			return true;
		}
		
		return false;
	}
	
	private void doStitch(Region[,] region)
	{
		for (int i = 0; i < height; i++)
		{
			Console.Write("\n");
			for (int j = 0; j < width; j++)
			{
				if (region[i, j].stitchReady)
				{
					Console.Write("X ");
					region[i, j].stitchReady = false;
				}
				else
				{
					Console.Write("O ");
				}
			}
		}
		Console.Write("\n");
	}
}

class Region
{
	public int upLim;
	public int downLim;
	public int leftLim;
	public int rightLim;
	public bool stitchReady;
	
	public Region(int upLim, int downLim, int leftLim, int rightLim)
	{
		this.upLim = upLim;
		this.downLim = downLim;
		this.leftLim = leftLim;
		this.rightLim = rightLim;
		
		stitchReady = false;
	}
}
