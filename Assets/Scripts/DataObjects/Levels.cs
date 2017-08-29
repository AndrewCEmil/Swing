using System;
using System.Collections.Generic;

[Serializable]
public class Levels
{
	public Levels ()
	{
	}

	public Levels(List<Level> _levels) {
		levels = _levels;
	}

	public List<Level> levels;
}