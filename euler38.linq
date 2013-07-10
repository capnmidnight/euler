<Query Kind="Program" />

int maken(int n)
{
	var v = new List<int>(new int[]{1,2,3,4,5,6,7,8});
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

void Main()
{
	int start = 8*7*6*5*4*3*2 - 1;
	int max = 0;
	for(int i = 0; i <= start; ++i)
	{
		var n = 900000000+maken(i);
		for(int b = 1; b <= 4; ++b)
		{
			var s = n.ToString();
			var parts = new List<int>();
			for(int a = b; a < s.Length - 1; ++a)
			{
				var tempS = s.Substring(0, a);
				if(tempS.Length > 0)
				{
					var tempI = int.Parse(tempS);
					if(parts.Count == 0)
					{
						parts.Add(tempI);
						a--;
						s = s.Substring(a+1);						
					}
					else if (tempI > parts[parts.Count - 1])
					{
						parts.Add(tempI);
						a--;
						s = s.Substring(a+1);
					}
				}
			}
			if(s.Length > 0)
			{
				var tempI = int.Parse(s);
				if (tempI > parts[parts.Count - 1])
				{
					parts.Add(tempI);
					bool good = true;
					for(int q = 1; q < parts.Count && good;++q)
						good &= (double)parts[0] == (parts[q] / (1.0 + q));
					if(good && n > max)
					{
						max = n;
					}
				}
			}
		}
	}
	Console.WriteLine(max);
}

// Define other methods and classes here
