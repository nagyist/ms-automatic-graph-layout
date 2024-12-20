using System;

namespace Microsoft.Msagl.Core.Geometry {
    /// <summary>
    /// A class for keeping polyline points in a double linked list
    /// </summary>
    [Serializable]
    public class CornerSite{
        /// <summary>
        /// the coeffiecient used to calculate the first and the second control points of the 
        /// Bezier segment for the fillet at the site
        /// </summary>
         double previouisBezierCoefficient = 0.5;
        /// <summary>
        /// used to calculate the first control points: the formula is kPrev * a + (1 - kPrev) * b
        /// </summary>
        public double PreviousBezierSegmentFitCoefficient {
            get { return previouisBezierCoefficient; }
            set { previouisBezierCoefficient = value; }
        }
        /// <summary>
        /// the coeffiecient used to calculate the third and the fourth control points of the 
        /// Bezier segment for the fillet at the site
        /// </summary>
         double nextBezierCoefficient = 0.5;
        /// <summary>
        /// the coefficient tells how tight the segment fits to the segment after the site; the formula is kNext * c + (1 - kNext) * b
        /// </summary>
        public double NextBezierSegmentFitCoefficient {
            get { return nextBezierCoefficient; }
            set { nextBezierCoefficient = value; }
        }

        double previousTangentCoefficient=1.0/3;

        ///<summary>
        ///used to calculate the second control point
        ///</summary>
        public double PreviousTangentCoefficient {
            get { return previousTangentCoefficient; }
            set { previousTangentCoefficient = value; }
        }
        double nextTangentCoefficient = 1.0 / 3;

        ///<summary>
        ///used to calculate the third control point
        ///</summary>
        public double NextTangentCoefficient
        {
            get { return nextTangentCoefficient; }
            set { nextTangentCoefficient = value; }
        }

        //   internal double par;
         Point point;

        /// <summary>
        /// gets the site point
        /// </summary>
        public Point Point {
            get { return point; }
            set { point = value; }
        }

         CornerSite prev;
/// <summary>
/// gets the previous site
/// </summary>
		public CornerSite Previous
		{
            get { return prev; }
            set { prev = value; }
        }

         CornerSite next;
/// <summary>
/// gets the next site
/// </summary>
		public CornerSite Next
		{
            get { return next; }
            set { next = value; }
        }
        internal CornerSite() { }
        /// <summary>
        /// the constructor
        /// </summary>
        /// <param name="sitePoint"></param>
        public CornerSite(Point sitePoint) {
            point = sitePoint;
        }
        /// <summary>
        /// a constructor
        /// </summary>
        /// <param name="previousSite"></param>
        /// <param name="sitePoint"></param>
		public CornerSite(CornerSite previousSite, Point sitePoint )
		{
            ValidateArg.IsNotNull(previousSite, "pr");
            point = sitePoint;
            prev = previousSite;
            previousSite.next = this;
        }
        /// <summary>
        /// a constructor
        /// </summary>
        /// <param name="previousSite"></param>
        /// <param name="sitePoint"></param>
        /// <param name="nextSite"></param>
		public CornerSite(CornerSite previousSite, Point sitePoint, CornerSite nextSite )
		{
            ValidateArg.IsNotNull(previousSite, "pr");
            ValidateArg.IsNotNull(nextSite, "ne");
            prev = previousSite;
            point = sitePoint;
            next = nextSite;

            previousSite.next = this;
            next.prev = this;
        }
        
        internal double Turn {
            get {
                if (this.Next == null || this.Previous == null)
                    return 0;
                return Point.SignedDoubledTriangleArea(Previous.Point, Point, Next.Point);
            }
        }

        internal CornerSite Clone() {
            CornerSite s = new CornerSite();
            s.PreviousBezierSegmentFitCoefficient = PreviousBezierSegmentFitCoefficient;
            s.Point = Point;
            return s;
        }
/// <summary>
/// 
/// </summary>
/// <returns></returns>
        public override string ToString() {
            return Point.ToString();
        }

    }
}
