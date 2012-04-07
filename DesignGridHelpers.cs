using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace com.wp.helpers
{
    public static class DesignGridHelper
    {
        private static bool _visible;
        private static double _opacity = 0.15;
        private static Color _color = Colors.Red;
        private static List<Rectangle> _squares;
        private static Grid _grid;

        public static bool IsVisible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
                UpdateGrid();
            }
        }

        public static Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                UpdateGrid();
            }
        }

        public static double Opacity
        {
            get { return _opacity; }
            set
            {
                _opacity = value;
                UpdateGrid();
            }
        }

        private static void UpdateGrid()
        {
            if (_squares != null)
            {
                var brush = new SolidColorBrush(_color);
                foreach (var square in _squares)
                {
                    square.Fill = brush;
                }
                if (_grid != null)
                {
                    _grid.Visibility = _visible ? Visibility.Visible : Visibility.Collapsed;
                    _grid.Opacity = _opacity;
                }
            }
            else
            {
                BuildGrid();
            }
        }

        private static void BuildGrid()
        {
            _squares = new List<Rectangle>();

            var frame = Application.Current.RootVisual as Frame;
            if (frame == null || VisualTreeHelper.GetChildrenCount(frame) == 0)
            {
                Deployment.Current.Dispatcher.BeginInvoke(BuildGrid);
                return;
            }

            var child = VisualTreeHelper.GetChild(frame, 0);
            var childAsBorder = child as Border;
            var childAsGrid = child as Grid;
            if (childAsBorder != null)
            {
                var content = childAsBorder.Child;
                if (content == null)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(BuildGrid);
                    return;
                }
                childAsBorder.Child = null;
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    Grid newGrid = new Grid();
                    childAsBorder.Child = newGrid;
                    newGrid.Children.Add(content);
                    PrepareGrid(frame, newGrid);
                });
            }
            else if (childAsGrid != null)
            {
                PrepareGrid(frame, childAsGrid);
            }
            else
            {
                Debug.WriteLine("Dear developer:");
                Debug.WriteLine("Unfortunately the design overlay feature requires that the root frame visual");
                Debug.WriteLine("be a Border or a Grid. So the overlay grid just isn't going to happen.");
                return;
            }
        }

        private static void PrepareGrid(Frame frame, Grid parent)
        {
            var brush = new SolidColorBrush(_color);

            _grid = new Grid();
            _grid.IsHitTestVisible = false;

            double width = frame.ActualWidth;
            double height = frame.ActualHeight;
            double max = Math.Max(width, height);

            for (int x = 24; x < /*width*/ max; x += 37)
            {
                for (int y = 24; y < /*height*/ max; y += 37)
                {
                    var rect = new Rectangle
                    {
                        Width = 25,
                        Height = 25,
                        VerticalAlignment = System.Windows.VerticalAlignment.Top,
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                        Margin = new Thickness(x, y, 0, 0),
                        IsHitTestVisible = false,
                        Fill = brush,
                    };
                    _grid.Children.Add(rect);
                    _squares.Add(rect);
                }
            }

            _grid.Visibility = _visible ? Visibility.Visible : Visibility.Collapsed;
            _grid.Opacity = _opacity;

            _grid.CacheMode = new BitmapCache();

            parent.Children.Add(_grid);
        }
    }
}