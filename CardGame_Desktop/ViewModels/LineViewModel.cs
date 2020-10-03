using System.Windows;

namespace CardGame_Desktop.ViewModels
{
    public class LineViewModel
    {
        public Point Source { get; }
        public Point Target { get; }

        public LineViewModel(Point source, Point target)
        {
            Source = source;
            Target = target;
        }
    }
}