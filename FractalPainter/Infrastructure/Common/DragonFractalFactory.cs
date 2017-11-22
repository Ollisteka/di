using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FractalPainting.App.Fractals;

namespace FractalPainting.Infrastructure.Common
{
	public interface IDragonPainterFactory
	{
		DragonPainter CreateDragonPainter(DragonSettings settings);
	}
}
