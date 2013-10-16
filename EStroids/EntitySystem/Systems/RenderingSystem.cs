namespace EStroids.EntitySystem.Systems
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Windows.Forms;
	using EStroids.EntitySystem.Components;

	public class RenderingSystem : SystemBase
	{
		private readonly Pen _pen = new Pen(Color.LimeGreen);
		private readonly Brush _backBrush = new SolidBrush(Color.Black);

		private readonly Form _form;
		private readonly float _halfWidth;
		private readonly float _halfHeight;
		private BufferedGraphicsContext _context;
		private BufferedGraphics _buffer;

		private volatile bool _formOpen = false;

		public override void Frame(double dt)
		{
			if (_context == null || _buffer == null || _formOpen == false) return;

			_buffer.Graphics.FillRectangle(_backBrush, _form.DisplayRectangle);

			var count = 0L;
			foreach (var entity in Manager.GetEntitiesWithComponent<Model>())
			{
				var position = entity.GetComponent<Position>();
				var model = entity.GetComponent<Model>();

				if (position == null || model == null) continue;

				var points = new List<PointF>();
				Matrix4 scale;
				Matrix4 rotation;
				Matrix4 translation;
				Matrix4 screen;

				Matrix4.Scaling(model.Scale, out scale);
				Matrix4.Rotation(position.XRot, position.YRot, position.ZRot, out rotation);
				Matrix4.Translation(position.X, position.Y, position.Z, out translation);
				Matrix4.Translation(_halfWidth, _halfHeight, 0, out screen);
				var final = translation * screen * rotation * scale;
				var translated = model.Vertices.Select(x =>
				{
					Vector3 result;
					Matrix4.Multiply(ref final, ref x, out result);
					return result;
				}).ToArray();

				foreach (var indice in model.Indices)
				{
					var vertex = translated[indice];
					points.Add(new PointF(vertex.X, vertex.Y));
				}

				_buffer.Graphics.DrawPolygon(_pen, points.ToArray());
				++count;
			}

			_form.Invoke(new Action(() =>
			{
				try
				{
					if (_form.IsDisposed == false && _formOpen)
					{
						_buffer.Render();
					}
				}
				catch { }
			}));
		}

		public override void Initialize()
		{
			_form.Invoke(new Action(() =>
			{
				_context = BufferedGraphicsManager.Current;
				_buffer = _context.Allocate(_form.CreateGraphics(), _form.DisplayRectangle);
				_formOpen = true;
			}));

			_form.FormClosing += (s, e) => _formOpen = false;
			_form.Disposed += (s, e) => _formOpen = false;
		}

		public override void Shutdown()
		{
			using (_backBrush)
			using (_pen)
			using (_backBrush)
			using (_buffer)
			using (_context)
			{
				_buffer = null;
				_context = null;
				_formOpen = false;
			}
		}

		public RenderingSystem(Form form, EntityManager manager)
			: base(manager)
		{
			_form = form;
			_halfWidth = form.ClientRectangle.Width / 2;
			_halfHeight = form.ClientRectangle.Height / 2;
		}
	}
}