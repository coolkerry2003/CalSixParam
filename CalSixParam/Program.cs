using PilotGaea.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalSixParam
{
    class Program
    {
        static void Main(string[] args)
        {
            List<GeoPoint> mPoints1 = new List<GeoPoint>();
            mPoints1.Add(new GeoPoint(0,0));
            mPoints1.Add(new GeoPoint(0, 1));
            mPoints1.Add(new GeoPoint(1, 0));
            
            List<GeoPoint> mPoints2 = new List<GeoPoint>();
            mPoints2.Add(new GeoPoint(0, 0));
            mPoints2.Add(new GeoPoint(5, 0));
            mPoints2.Add(new GeoPoint(0, -5));

            SixParam sp = new SixParam(mPoints1,mPoints2);
            sp.Cal();
        }
    }
    class SixParam
    {
        double P4R, B, P4X, D, P4S, P4Y; // 六參數
        List<GeoPoint> sPoints;
        List<GeoPoint> tPoints;
        public SixParam(List<GeoPoint> asPoints, List<GeoPoint> atPoints)
        {
            P4R = B = P4X = D = P4S = P4Y = 0; // 先初始化
            sPoints = asPoints;
            tPoints = atPoints;
        }
        public void Cal()
        {
            double dbase = det3x3(tPoints[0].x, tPoints[1].x, tPoints[2].x, tPoints[0].y, tPoints[1].y, tPoints[2].y, 1.0, 1.0, 1.0);
            P4R = det3x3(sPoints[0].x, sPoints[1].x, sPoints[2].x, tPoints[0].y, tPoints[1].y, tPoints[2].y, 1.0, 1.0, 1.0) / dbase;
            B = det3x3(tPoints[0].x, tPoints[1].x, tPoints[2].x, sPoints[0].x, sPoints[1].x, sPoints[2].x, 1.0, 1.0, 1.0) / dbase;
            P4X = det3x3(tPoints[0].x, tPoints[1].x, tPoints[2].x, tPoints[0].y, tPoints[1].y, tPoints[2].y, sPoints[0].x, sPoints[1].x, sPoints[2].x) / dbase;
            D = det3x3(sPoints[0].y, sPoints[1].y, sPoints[2].y, tPoints[0].y, tPoints[1].y, tPoints[2].y, 1.0, 1.0, 1.0) / dbase;
            P4S = det3x3(tPoints[0].x, tPoints[1].x, tPoints[2].x, sPoints[0].y, sPoints[1].y, sPoints[2].y, 1.0, 1.0, 1.0) / dbase;
            P4Y = det3x3(tPoints[0].x, tPoints[1].x, tPoints[2].x, tPoints[0].y, tPoints[1].y, tPoints[2].y, sPoints[0].y, sPoints[1].y, sPoints[2].y) / dbase;
        }
        /// <summary>
        /// 計算 2x2 行列式
        /// | a c |
        /// | b d |
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        double det2x2(double a, double b, double c, double d) // determinate 2x2
        {
            return (a * d - b * c);
        }
        /// <summary>
        /// 計算 3x3 行列式
        /// | a1 b1 c1 |
        /// | a2 b2 c2 | 
        /// | a3 b3 c3 | 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="a3"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <param name="b3"></param>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="c3"></param>
        /// <returns></returns>
        double det3x3(double a1, double a2, double a3, double b1, double b2, double b3, double c1, double c2, double c3)
        {
            double ans =
                a1 * det2x2(b2, b3, c2, c3) -
                b1 * det2x2(a2, a3, c2, c3) +
                c1 * det2x2(a2, a3, b2, b3);
            return ans;
        }
    }
}
