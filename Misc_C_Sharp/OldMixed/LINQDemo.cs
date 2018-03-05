using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Misc_C_Sharp
{

    public class LINQDemo
    {
        public LINQDemo()
        {
            //query to get all world champions from Brazil sorted by the highest number of wins
            //var query = from r in Formula1.GetChampions()
            //            where r.Country == "Brazil"
            //            orderby r.Wins descending
            //            select r;
            //foreach(var item in query)
            //{
            //    Console.WriteLine("{0:A}", item);
            //}

            //Select Many
            //var drivers = from r in Formula1.GetChampions()
            //              from c in r.Cars
            //                  //where c == "Ferrari"
            //              orderby r.FirstName
            //              select r.FirstName + " " + r.LastName + " : " + c;
            //foreach (var item in drivers)
            //{
            //    Console.WriteLine(item);
            //}

            //group by countery show country: count
            //var query = from r in Formula1.GetChampions()
            //            group r by r.Country into g
            //            where g.Count() > 1
            //            orderby g.Count() descending
            //            select new { Country = g.Key, Champions = g.Count() };
            //foreach (var item in query)
            //{
            //    Console.WriteLine("Country: {0} Champions: {1}", item.Country, item.Champions);
            //}

            //Nested groups
            //var query = from r in Formula1.GetChampions()
            //            group r by r.Country into g
            //            where g.Count() > 1
            //            orderby g.Count() descending
            //            select new {
            //                Country = g.Key,
            //                Champions = g.Count(),
            //                Racers = (from r1 in g
            //                         select r1)
            //            };
            //foreach (var item in query)
            //{
            //    Console.WriteLine("Country: {0} Champions: {1}", item.Country, item.Champions);
            //    Console.WriteLine("-------------------------------------");
            //    foreach (var racer in item.Racers)
            //    {
            //        Console.WriteLine("{0:A}", racer);
            //    }
            //    Console.WriteLine("*******************************************");
            //}

            // Compound Form (Select Many)
            // Select All racers who win with Ferrari
            //var racers = from r in Formula1.GetChampions()
            //             from c in r.Cars
            //             where c == "Ferrari"
            //             select r;
            //foreach (var racer in racers)
            //{
            //    Console.WriteLine("{0:A}", racer);
            //    Console.WriteLine("*****");
            //    foreach (var car in racer.Cars)
            //    {
            //        Console.WriteLine(car);
            //    }
            //    Console.WriteLine("*****");
            //}

            // Compound Form(Select Many)
            // Select All racers who win with Ferrari
            //var racers = Formula1.GetChampions()
            //    .SelectMany(r => r.Cars, (r, c) => new { Racer = r, Car = c })
            //    .Where(racer => racer.Car == "Ferrari")
            //    .Select(racer => racer.Racer);
            //foreach (var racer in racers)
            //{
            //    Console.WriteLine("{0:A}", racer);
            //    Console.WriteLine("*****");
            //    foreach (var car in racer.Cars)
            //    {
            //        Console.WriteLine(car);
            //    }
            //    Console.WriteLine("*****");
            //}

            // Join Racer and Team on Year
            //var racers = from r in Formula1.GetChampions()
            //            from y in r.Years
            //            select new { Year = y, Name = r.FirstName + ", " + r.LastName };
            //var teams = from t in Formula1.GetContructorChampions()
            //            from y in t.Years
            //            select new { Year = y, Name = t.Name };
            //var racerAndTeams = (from r in racers
            //                     join t in teams on r.Year equals t.Year
            //                     select new { Year = r.Year, Champion = r.Name, Team = t.Name }).Take(10);
            //Console.WriteLine("Year World Champion\t Constructur Title");
            //foreach (var item in racerAndTeams)
            //{
            //    Console.WriteLine("{0} {1,-20} {2}", item.Year, item.Champion, item.Team);
            //}

            // Left Join
            var racers = from r in Formula1.GetChampions()
                         from y in r.Years
                         select new { Year = y, Name = r.FirstName + ", " + r.LastName };
            var teams = from t in Formula1.GetContructorChampions()
                        from y in t.Years
                        select new { Year = y, Name = t.Name };
            var racerAndTeams = (from r in racers
                                 join t in teams on r.Year equals t.Year into rt
                                 from t in rt.DefaultIfEmpty()
                                 select new {
                                     Year = r.Year,
                                     Champion = r.Name,
                                     Team = t == null ? "No constructor" : t.Name
                                 }).Take(10);
            Console.WriteLine("Year World Champion\t Constructur Title");
            foreach (var item in racerAndTeams)
            {
                Console.WriteLine("{0} {1,-20} {2}", item.Year, item.Champion, item.Team);
            }

            //var racer = Formula1.GetChampions().FirstOrDefault();
            //PropertyInfo[] propInfo = racer.GetType().GetProperties();
            //foreach (var pi in propInfo)
            //{
            //    Console.WriteLine("{0}: {1}", pi.Name, pi.GetValue(racer, null));
            //}




        }
    }

    [Serializable]
    public class Racer : IComparable<Racer>, IFormattable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Wins { get; set; }
        public string Country { get; set; }
        public int Starts { get; set; }
        public IEnumerable<string> Cars { get; private set; }
        public IEnumerable<int> Years { get; private set; }

        public Racer(string firstName, string lastName, string country, int starts, int wins) 
            :this(firstName, lastName, country, starts, wins, null, null)
        {
        }
        public Racer(string firstName, string lastName, string country, int starts, int wins, IEnumerable<int> years, IEnumerable<string> cars)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Country = country;
            this.Starts = starts;
            this.Wins = wins;
            this.Years = new List<int>(years);
            this.Cars = new List<string>(cars);
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }

        public int CompareTo(Racer other)
        {
            if (other == null)
                return -1;
            return string.Compare(this.LastName, other.LastName);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case null:
                case "N":
                    return ToString();
                case "F":
                    return FirstName;
                case "L":
                    return LastName;
                case "C":
                    return Country;
                case "S":
                    return Starts.ToString();
                case "W":
                    return Wins.ToString();
                case "A":
                    return String.Format("{0} {1}, {2}; starts: {3}, wins: {4}",
                    FirstName, LastName, Country, Starts, Wins);
                default:
                    throw new FormatException(String.Format(
                    "Format {0} not supported", format));
            }
        }
    }

    [Serializable]
    public class Team
    {
        public Team(string name, params int[] years)
        {
            Name = name;
            Years = new List<int>(years);
        }
        public string Name { get; set; }
        public IEnumerable<int> Years { get; private set; }
    }

    public static class Formula1
    {
        private static List<Racer> racers;
        public static IList<Racer> GetChampions()
        {
            if (racers == null)
            {
                racers = new List<Racer>(40);
                racers.Add(new Racer("Nino", "Farina", "Italy", 33, 5, new int[] { 1950 }, new string[] { "Alfa Romeo" }));
                racers.Add(new Racer("Alberto", "Ascari", "Italy", 32, 10, new int[] { 1952, 1953 }, new string[] { "Ferrari" }));
                racers.Add(new Racer("Juan Manuel", "Fangio", "Argentina", 51, 24, new int[] { 1951, 1954, 1955, 1956, 1957 }, new string[] { "Alfa Romeo", "Maserati", "Mercedes", "Ferrari" }));
                racers.Add(new Racer("Mike", "Hawthorn", "UK", 45, 3, new int[] { 1958 }, new string[] { "Ferrari" }));
                racers.Add(new Racer("Phil", "Hill", "USA", 48, 3, new int[] { 1961 }, new string[] { "Ferrari" }));
                racers.Add(new Racer("John", "Surtees", "UK", 111, 6, new int[] { 1964 }, new string[] { "Ferrari" }));
                racers.Add(new Racer("Jim", "Clark", "UK", 72, 25, new int[] { 1963, 1965 }, new string[] { "Lotus" }));
                racers.Add(new Racer("Jack", "Brabham", "Australia", 125, 14, new int[] { 1959, 1960, 1966 }, new string[] { "Cooper", "Brabham" }));
                racers.Add(new Racer("Denny", "Hulme", "New Zealand", 112, 8, new int[] { 1967 }, new string[] { "Brabham" }));
                racers.Add(new Racer("Graham", "Hill", "UK", 176, 14, new int[] { 1962, 1968 }, new string[] { "BRM", "Lotus" }));
                racers.Add(new Racer("Jochen", "Rindt", "Austria", 60, 6, new int[] { 1970 }, new string[] { "Lotus" }));
                racers.Add(new Racer("Jackie", "Stewart", "UK", 99, 27, new int[] { 1969, 1971, 1973 }, new string[] { "Matra", "Tyrrell" }));
                racers.Add(new Racer("Emerson", "Fittipaldi", "Brazil", 143, 14, new int[] { 1972, 1974 }, new string[] { "Lotus", "McLaren" }));
                racers.Add(new Racer("James", "Hunt", "UK", 91, 10, new int[] { 1976 }, new string[] { "McLaren" }));
                racers.Add(new Racer("Mario", "Andretti", "USA", 128, 12, new int[] { 1978 }, new string[] { "Lotus" }));
                racers.Add(new Racer("Jody", "Scheckter", "South Africa", 112, 10, new int[] { 1979 }, new string[] { "Ferrari" }));
                racers.Add(new Racer("Alan", "Jones", "Australia", 115, 12, new int[] { 1980 }, new string[] { "Williams" }));
                racers.Add(new Racer("Keke", "Rosberg", "Finland", 114, 5, new int[] { 1982 }, new string[] { "Williams" }));
                racers.Add(new Racer("Niki", "Lauda", "Austria", 173, 25, new int[] { 1975, 1977, 1984 }, new string[] { "Ferrari", "McLaren" }));
                racers.Add(new Racer("Nelson", "Piquet", "Brazil", 204, 23, new int[] { 1981, 1983, 1987 }, new string[] { "Brabham", "Williams" }));
                racers.Add(new Racer("Ayrton", "Senna", "Brazil", 161, 41, new int[] { 1988, 1990, 1991 }, new string[] { "McLaren" }));
                racers.Add(new Racer("Nigel", "Mansell", "UK", 187, 31, new int[] { 1992 }, new string[] { "Williams" }));
                racers.Add(new Racer("Alain", "Prost", "France", 197, 51, new int[] { 1985, 1986, 1989, 1993 }, new string[] { "McLaren", "Williams" }));
                racers.Add(new Racer("Damon", "Hill", "UK", 114, 22, new int[] { 1996 }, new string[] { "Williams" }));
                racers.Add(new Racer("Jacques", "Villeneuve", "Canada", 165, 11, new int[] { 1997 }, new string[] { "Williams" }));
                racers.Add(new Racer("Mika", "Hakkinen", "Finland", 160, 20, new int[] { 1998, 1999 }, new string[] { "McLaren" }));
                racers.Add(new Racer("Michael", "Schumacher", "Germany", 287, 91, new int[] { 1994, 1995, 2000, 2001, 2002, 2003, 2004 }, new string[] { "Benetton", "Ferrari" }));
                racers.Add(new Racer("Fernando", "Alonso", "Spain", 177, 27, new int[] { 2005, 2006 }, new string[] { "Renault" }));
                racers.Add(new Racer("Kimi", "Räikkönen", "Finland", 148, 17, new int[] { 2007 }, new string[] { "Ferrari" }));
                racers.Add(new Racer("Lewis", "Hamilton", "UK", 90, 17, new int[] { 2008 }, new string[] { "McLaren" }));
                racers.Add(new Racer("Jenson", "Button", "UK", 208, 12, new int[] { 2009 }, new string[] { "Brawn GP" }));
                racers.Add(new Racer("Sebastian", "Vettel", "Germany", 81, 21, new int[] { 2010, 2011 }, new string[] { "Red Bull Racing" }));
            }

            return racers;
        }

        private static List<Team> teams;
        public static IList<Team> GetContructorChampions()
        {
            if (teams == null)
            {
                teams = new List<Team>()
                {
                    new Team("Vanwall", 1958),
                    new Team("Cooper", 1959, 1960),
                    new Team("Ferrari", 1961, 1964, 1975, 1976, 1977, 1979, 1982, 1983, 1999, 2000, 2001, 2002, 2003, 2004, 2007, 2008),
                    new Team("BRM", 1962),
                    new Team("Lotus", 1963, 1965, 1968, 1970, 1972, 1973, 1978),
                    new Team("Brabham", 1966, 1967),
                    new Team("Matra", 1969),
                    new Team("Tyrrell", 1971),
                    new Team("McLaren", 1974, 1984, 1985, 1988, 1989, 1990, 1991, 1998),
                    new Team("Williams", 1980, 1981, 1986, 1987, 1992, 1993, 1994, 1996, 1997),
                    new Team("Benetton", 1995),
                    new Team("Renault", 2005, 2006),
                    new Team("Brawn GP", 2009),
                    new Team("Red Bull Racing", 2010, 2011)
                };
            }
            return teams;
        }
    }
}
