using System;
using System.Windows;
using System.Windows.Controls;

namespace UI.Screen.Game.Core.Map
{
    public class HexagonGrid : Grid
    {
        /**
         * If the length of 1 side of the hexagon = S, then:
         * Width = 2 x S
         * Height = S x SQRT(3)
         * Column C starts at C x (0.75 x Width)
         * Row R starts at R x Height
         * A row's uneven columns have an vertical offset of 0.5 x Height 
         **/
        #region HexagonSideLength

        /// <summary>
        /// HexagonSideLength Dependency Property
        /// </summary>
        public static readonly DependencyProperty HexagonSideLengthProperty =
            DependencyProperty.Register("HexagonSideLength", typeof(double), typeof(HexagonGrid),
                new FrameworkPropertyMetadata((double)0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the HexagonSideLength property. This dependency property 
        /// represents the length of 1 side of the hexagon.
        /// </summary>
        public double HexagonSideLength
        {
            get { return (double)GetValue(HexagonSideLengthProperty); }
            set { SetValue(HexagonSideLengthProperty, value); }
        }

        #endregion

        #region Rows

        /// <summary>
        /// Rows Dependency Property
        /// </summary>
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(HexagonGrid),
                new FrameworkPropertyMetadata(1,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the Rows property.
        /// </summary>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        #endregion

        #region Columns

        /// <summary>
        /// Columns Dependency Property
        /// </summary>
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(int), typeof(HexagonGrid),
                new FrameworkPropertyMetadata(1,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the Columns property.
        /// </summary>
        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        #endregion

        public String Clipping
        {
            get
            {
                var side = HexagonSideLength;
                var width = side * Math.Sqrt(3.0);
                var height = 2 * side;
                var semiWidth = Math.Round(width/2);
                var fullWidth = Math.Round(width);
                var oneQuarterHeight = Math.Round(height / 4);
                var threeQuarterHeight = Math.Round(3 * (height / 4));
                var fullHeight = Math.Round(height);
                return "M " + semiWidth + ",0 L " + fullWidth + "," + oneQuarterHeight + " L " + fullWidth + "," + threeQuarterHeight + " L " + semiWidth + "," + fullHeight + " L 0," + threeQuarterHeight + "  L 0," + oneQuarterHeight + "  Z";
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            var side = HexagonSideLength;
            var width = side * Math.Sqrt(3.0);
            var height = 2 * side;
            var rowHeight = 0.75 * height;

            var availableChildSize = new Size(width, height);

            foreach (FrameworkElement child in InternalChildren)
            {
                child.Measure(availableChildSize);
            }

            var totalHeight = Rows * rowHeight;
            if (Columns > 1)
                totalHeight += (0.5 * rowHeight);
            var totalWidth = Columns + (0.5 * side);

            var totalSize = new Size(totalWidth, totalHeight);

            return totalSize;
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var side = HexagonSideLength;
            var width = side * Math.Sqrt(3.0);
            var height = 2 * side;
            var colWidth = width - 1;
            var rowHeight = 0.75 * height;

            var childSize = new Size(width, height);

            foreach (FrameworkElement child in InternalChildren)
            {
                var row = GetRow(child);
                var col = GetColumn(child);

                var left = col * colWidth;
                var top = row * rowHeight;
                var isUnevenRow = (row % 2 != 0);
                if (isUnevenRow)
                    left += (0.5 * colWidth);

                child.Arrange(new Rect(new Point(left, top), childSize));
            }

            return arrangeSize;
        }
    }
}
