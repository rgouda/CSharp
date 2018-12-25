using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Interview
{
    public class CostOptimization
    {
        // List of tuples of NY and SF costs.
        private List<(int SF, int NY)> costs;

        public CostOptimization()
        {
            costs = new List<(int SF, int NY)>();
        }

        public void InsertCandidateCosts(int costSF, int costNY)
        {
            costs.Add((SF: costSF, NY: costNY));
        }

        public int CandidateCount()
        {
            return costs.Count;
        }

        public void Clear()
        {
            costs.Clear();
        }

        public int CalculateCost()
        {
            var n = costs.Count;
            if (n % 2 != 0)
                throw new Exception($"Nr. of elements {n} are not even.");

            var minCostResults =
                (from c in this.costs
                 let minCity = (c.NY < c.SF ? "NY" : "SF")
                 let minCost = (c.NY < c.SF ? c.NY : c.SF)
                 select (minCity: minCity, minCost: minCost, NY: c.NY, SF: c.SF));
                 //.OrderBy(o => o.minCost);

            var cityCounter = new Dictionary<string, int> { { "NY", 0 }, { "SF", 0 } };
            var optimizedCost = 0;
            foreach(var (minCity, minCost, NY, SF) in minCostResults)
            {
                if (cityCounter[minCity] >= n/2)
                {
                    optimizedCost += minCity == "SF" ? NY : SF;
                    cityCounter[minCity == "SF" ? "NY" : "SF"]++;
                }
                else
                {
                    optimizedCost += minCost;
                    cityCounter[minCity]++;
                }
            }

            return optimizedCost;
        }
    }
}
