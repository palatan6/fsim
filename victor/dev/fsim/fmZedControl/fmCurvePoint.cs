using System;
namespace fmZedGraph
{
    public struct fmCurvePoint:IComparable
    {
        public ZedGraph.CurveItem Curve;
        public int PointInx;
        
        public fmCurvePoint(ZedGraph.CurveItem curve, int index)
        {
            Curve = curve;
            PointInx = index;
        }      

        public int CompareTo(object obj)
        {
            return PointInx.CompareTo(((fmCurvePoint)obj).PointInx);
        }
    }    
}
