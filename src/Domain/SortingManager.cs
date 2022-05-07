using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public static class SortingManager
    {
        public static IEnumerable<Lead> SortedByPropertyAndProjectParams(this IEnumerable<Lead> leads)
        {
            return leads.OrderBy(l => l.PropertyType).ThenBy(l => l.Project);
        }
        
        public static IEnumerable<Lead> SortedByStartDateAsc(this IEnumerable<Lead> leads)
        {
            return leads.OrderBy(l => DateTime.Parse(l.StartDate));
        }
        
        
        public static IEnumerable<Lead> SortedByLastNameDesc(this IEnumerable<Lead> leads)
        {
            return leads.OrderByDescending(l => l.LastName);
        }
        
        public static IEnumerable<Lead> SortedByProjects(this IEnumerable<Lead> leads)
        {
            return leads.OrderBy(l => l.Project);
        }
    }
}