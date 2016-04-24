using System;

namespace AssemblyCSharp
{
	public class exp
	{
		public int total;
		public int level;
		public int sub;
		public int next;
		public int step;
	
		public exp ()
		{
			level = 1;
			next = 1;
			step = next;
		}

		public bool addExp(int value)
		{
			bool levelUp = false;

			total += value;
			sub += value;
			if (next <= sub) {
				level++;
				sub -= next;

				//step += (int)(next * 0.1f);
				next += step;

				levelUp = true;
			}

			return levelUp;
		}

	}
}

