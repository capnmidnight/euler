<Query Kind="Program" />

int maken(int n, int k)
{
	var v = new List<int>();
	for(int i = 0; i < k; ++i)
		v.Add(i+1);
	var l = v.Count;
	var r = 0;
	for(int i = 0; i < l; ++i)
	{
		var t = n % (l - i);
		n /= (l - i);
		r += v[t] * (int)Math.Pow(10, i);
		v.RemoveAt(t);
	}
	return r;
}

bool isComp(int n)
{
	int max = (int)Math.Sqrt(n);
	for(int i = 2; i < max; ++i)
	{
		var t = (double)n / (double)i;
		if(Math.Floor(t) == t)
			return true;
	}
	return false;
}

void Main()
{				
	int start = 8*7*6*5*4*3*2 - 1;
	int max = 0;
	for(int j = 8; j >= 2; --j)
	{
		for(int i = 0; i <= start; ++i)
		{
			var n = maken(i, j);
			if(!isComp(n) && n > max)
			{
				max = n;
			}
		}
	}
	Console.WriteLine(max);
}