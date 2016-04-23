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
			next = 7;
			step = 7;
		}

		public void addExp(int value)
		{
			total += value;
			sub += value;
			if (sub <= next) {
				level++;
				sub -= next;

				step += (int)(step * 0.5f);
			}
		}

	}
}

