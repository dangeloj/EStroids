namespace EStroids.EntitySystem
{
	using System;
	using System.Collections.Concurrent;
	using System.Linq;
	using System.Windows.Forms;

	public class Keyboard
	{
		private readonly ConcurrentDictionary<Keys, bool> _keys;

		public bool IsKeyDown(Keys key)
		{
			return _keys[key];
		}

		public void KeyDown(Keys key)
		{
			_keys[key] = true;
		}

		public void KeyUp(Keys key)
		{
			_keys[key] = false;
		}

		public Keyboard()
		{
			var keys = Enum.GetValues(typeof(Keys))
						   .Cast<Keys>()
						   .Distinct()
						   .ToDictionary(x => x, x => false);

			_keys = new ConcurrentDictionary<Keys, bool>(keys);
		}
	}
}