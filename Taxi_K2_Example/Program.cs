using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace K2Taxi
{
    public interface IPropertySelector<T>
    {
        /// <summary>
        /// Implements the selection of the class property for the peak value search.
        /// </summary>
        T ValueForPeakSearch { get; }
    }

    public interface IBetween<T>
    {
        /// <summary>
        /// Indicates whether the value of the certain property of the current instance is in
        /// [<paramref name="from"/>, <paramref name="to"/>] range including range marginal values.
        /// <paramref name="from"/> should always precede <paramref name="to"/> in default sort order.
        /// </summary>
        /// <param name="from">The starting value of the range</param>
        /// <param name="to">The ending value of the range</param>
        /// <returns>true if the value of the current object property is in range; otherwise,
        /// false.</returns>
        bool MutuallyInclusiveBetween(T from, T to);

        /// <summary>
        /// Indicates whether the value of the certain property of the current instance is in
        /// [<paramref name="from"/>, <paramref name="to"/>] range excluding range marginal values.
        /// <paramref name="from"/> should always precede <paramref name="to"/> in default sort order.
        /// </summary>
        /// <param name="from">The starting value of the range</param>
        /// <param name="to">The ending value of the range</param>
        /// <returns>true if the value of the current object property is in range; otherwise,
        /// false.</returns>
        bool MutuallyExclusiveBetween(T from, T to);
    }

    /// <summary>
    /// Provides properties and implements interfaces for the storing and manipulating of the automobile data.
    /// THE STUDENT SHOULD DEFINE THE CLASS ACCORDING TO THE TASK.
    /// </summary>
    public class Automobile : IPropertySelector<double>, IBetween<string>, IBetween<double>, IComparable<Automobile>
    {
        public string NameAndSurname { get; private set; }
        public string CarNumber { get; private set; }
        public string CarMaker { get; private set; }
        public string CarModel { get; private set;}
        public int CarYear { get; private set; }
        public double CarMileage { get; private set;}
        public double ValueForPeakSearch { get; set;}

        public Automobile(string nameAndSurname, string carNumber, string carMaker, string carModel, int carYear, double carMileage, double valueForPeakSearch)
        {
            NameAndSurname = nameAndSurname;
            CarNumber = carNumber;
            CarMaker = carMaker;
            CarModel = carModel;
            CarYear = carYear;
            CarMileage = carMileage;
            ValueForPeakSearch = valueForPeakSearch;
        }


        public int CompareTo(Automobile other)
        {
            if((object)other==null)
            {
                return 1;
            }
            if(other.CarMileage.CompareTo(this.CarMileage)!=0)
            {
                return other.CarMileage.CompareTo(this.CarMileage);
            }
            else
            {
                return this.CarMaker.CompareTo(other.CarMaker);
            }
        }

        public bool MutuallyInclusiveBetween(string from, string to)
        {
            if(string.Compare(this.CarMaker,from,true)==0 || string.Compare(this.CarMaker, to,true) == 0)
            {
                return true;
            }
            return false;
        }

        public bool MutuallyExclusiveBetween(string from, string to)
        {
            if (string.Compare(this.CarMaker, from, true) != 0 || string.Compare(this.CarMaker, to, true) != 0)
            {
                return true;
            }
            return false;
        }

        public bool MutuallyInclusiveBetween(double from, double to)
        {
            if (this.CarMileage>=from && this.CarMileage<=to)
            {
                return true;
            }
            return false;
        }

        public bool MutuallyExclusiveBetween(double from, double to)
        {
            if (this.CarMileage <= from || this.CarMileage >= to)
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Provides generic container where the data are stored in the linked list.
    /// THE STUDENT SHOULD APPEND CONSTRAINTS ON TYPE PARAMETER <typeparamref name="T"/>
    /// IF THE IMPLEMENTATION OF ANY METHOD REQUIRES IT.
    /// </summary>
    /// <typeparam name="T">The type of the data to be stored in the list. Data 
    /// class should implement some interfaces.</typeparam>
    public class LinkList<T> where T : IComparable<T>
    {
        class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }
            public Node(T data, Node next)
            {
                Data = data;
                Next = next;
            }
        }

        /// <summary>
        /// All the time should point to the first element of the list.
        /// </summary>
        private Node begin;
        /// <summary>
        /// All the time should point to the last element of the list.
        /// </summary>
        private Node end;
        /// <summary>
        /// Shouldn't be used in any other methods except Begin(), Next(), Exist() and Get().
        /// </summary>
        private Node current;

        /// <summary>
        /// Initializes a new instance of the LinkList class with empty list.
        /// </summary>
        public LinkList()
        {
            begin = current = end = null;
        }
        /// <summary>
        /// One of four interface methods devoted to loop through a list and get value stored in it.
        /// Method should be used to move internal pointer to the first element of the list.
        /// </summary>
        public void Begin()
        {
            current = begin;
        }
        /// <summary>
        /// One of four interface methods devoted to loop through a list and get value stored in it.
        /// Method should be used to move internal pointer to the next element of the list.
        /// </summary>
        public void Next()
        {
            current = current.Next;
        }
        /// <summary>
        /// One of four interface methods devoted to loop through a list and get value stored in it.
        /// Method should be used to check whether the internal pointer points to the element of the list.
        /// </summary>
        /// <returns>true, if the internal pointer points to some element of the list; otherwise,
        /// false.</returns>
        public bool Exist()
        {
            return current != null;
        }
        /// <summary>
        /// One of four interface methods devoted to loop through a list and get value stored in it.
        /// Method should be used to get the value stored in the node pointed by the internal pointer.
        /// </summary>
        /// <returns>the value of the element that is pointed by the internal pointer.</returns>
        public T Get()
        {
            return current.Data;
        }

        /// <summary>
        /// Method appends new node to the end of the list and saves in it <paramref name="data"/>
        /// passed by the parameter.
        /// THE STUDENT SHOULD IMPLEMENT THIS METHOD ACCORDING TO THE TASK.
        /// </summary>
        /// <param name="data">The data to be stored in the list.</param>
        public void Add(T data)
        {
            var gotten = new Node(data, null);
            if(begin!=null)
            {
                end.Next = gotten;
                end = gotten;
            }
            else
            {
                begin = gotten;
                end = gotten;
            }
        }

        /// <summary>
        /// Method sorts data in the list. The data object class should implement IComparable
        /// interface though defining sort order.
        /// THE STUDENT SHOULD IMPLEMENT THIS METHOD ACCORDING TO THE TASK.
        /// </summary>
        public void Sort()
        {
            for(Node d1=begin; d1!=null; d1= d1.Next)
            {
                Node max = d1;
                for(Node d2 = d1; d2 != null; d2 = d2.Next)
                {
                    if (d1.Data.CompareTo(d2.Data)>0)
                    {
                        max = d2;
                        T temp = d1.Data;
                        d1.Data = max.Data;
                        max.Data= temp;
                    }
                }
            }
        }
    }

    public static class InOut
    {
        /// <summary>
        /// Creates the list containing data read from the text file.
        /// THE STUDENT SHOULD IMPLEMENT THIS METHOD ACCORDING TO THE TASK.
        /// </summary>
        /// <param name="fileName">The name of the text file</param>
        /// <returns>List with data from file</returns>
        public static LinkList<Automobile> ReadFromFile(string fileName)
        {
            LinkList<Automobile> list = new LinkList<Automobile>();
            string line;

            using (var file = new StreamReader(fileName, Encoding.UTF8))
            {
                while((line = file.ReadLine())!=null)
                {
                    var values = Regex.Split(line, "; ");
                    string nameAndSurname = values[0];
                    string carNumber = values[1];
                    string carMaker = values[2];
                    string carModel = values[3];
                    int carYear = int.Parse(values[4]);
                    double carMileage = double.Parse(values[5]);

                    Automobile current = new Automobile(nameAndSurname, carNumber, carMaker, carModel, carYear, carMileage, carMileage);
                    list.Add(current);
                }
            }
            return list;
        }

        /// <summary>
        /// Appends the table, built from data contained in the list and preceded by the header,
        /// to the end of the text file.
        /// THE STUDENT SHOULD IMPLEMENT THIS METHOD ACCORDING TO THE TASK.
        /// </summary>
        /// <param name="fileName">The name of the text file</param>
        /// <param name="header">The header of the table</param>
        /// <param name="list">The list from which the table to be formed</param>
        public static void PrintToFile(string fileName, string header, LinkList<Automobile> list)
        {
            using(var file = new StreamWriter(fileName, true)) 
            {
                file.WriteLine(new string('-', 100));
                file.WriteLine(header);
                file.WriteLine(new string('-', 100));
                file.WriteLine($"{"Name and surname",-20} | {"Car number",-15} | {"Car maker",-10} | {"Car model",-15} | {"Car year",-10} | {"Car mileage",-13} |");
                file.WriteLine(new string('-', 100));
                for (list.Begin(); list.Exist();list.Next())
                {
                    Automobile current = list.Get();
                    file.WriteLine($"{current.NameAndSurname,-20} | {current.CarNumber,-15} | {current.CarMaker,-10} | {current.CarModel,-15} | {current.CarYear,10} | {current.CarMileage,13} |");
                }
                file.WriteLine(new string('-', 100));
                file.WriteLine();
            }
        }
    }

    public static class Task
    {
        /// <summary>
        /// The method finds the biggest value in the given list. The object property for biggest
        /// value search can be selected by implementing interface IPropertySelector in the <typeparamref name="TData"/> class.
        /// THE STUDENT SHOULD IMPLEMENT THIS METHOD ACCORDING TO THE TASK.
        /// </summary>
        /// <typeparam name="TData">The type of the data objects stored in the list</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="list">The data list to be searched.</param>
        /// <returns>The biggest value found.</returns>
        public static TResult PeakValue<TResult, TData>(LinkList<TData> list) where TResult : IComparable<TResult> where TData : IPropertySelector<TResult>, IComparable<TData>
        {
            TResult max = default(TResult);
            for(list.Begin();list.Exist();list.Next())
            {
                TData current = list.Get();
                if(current.ValueForPeakSearch.CompareTo(max)>0)
                max = current.ValueForPeakSearch;
            }
            return max;
        }

        /// <summary>
        /// Filters data from the source list that meets filtering criteria and writes them
        /// into the new list.
        /// THE STUDENT SHOULD IMPLEMENT THIS METHOD ACCORDING TO THE TASK.
        /// THE STUDENT SHOULDN'T CHANGE THE SIGNATURE OF THE METHOD!
        /// </summary>
        /// <typeparam name="TData">The type of the data objects stored in the list</typeparam>
        /// <typeparam name="TCriteria">The type of criteria</typeparam>
        /// <param name="source">The source list from which the result would be created</param>
        /// <param name="from">Lower bound of the interval</param>
        /// <param name="to">Upper bound of the interval</param>
        /// <returns>The list that contains filtered data</returns>
        public static LinkList<TData> Filter<TData, TCriteria>(LinkList<TData> source, TCriteria from, TCriteria to) where TData : IComparable<TData>, IBetween<TCriteria>
        {
            LinkList<TData> list = new LinkList<TData>();
            for (source.Begin(); source.Exist(); source.Next())
            {
                TData current = source.Get();
                if(current.MutuallyInclusiveBetween(from,to))
                {
                    list.Add(current);
                }
            }
            return list;
        }

    }

    class Program
    {
        /// <summary>
        /// THE STUDENT SHOULD IMPLEMENT THIS METHOD ACCORDING TO THE TASK.
        /// </summary>
        static void Main()
        {
            LinkList<Automobile> FirstList = InOut.ReadFromFile("Duomenys.txt");
            LinkList<Automobile> SecondList = new LinkList<Automobile>();
            LinkList<Automobile> ThirdList = new LinkList<Automobile>();
            LinkList<Automobile> FourthList = new LinkList<Automobile>();

            if(File.Exists("Rezultatai.txt"))
            {
                File.Delete("Rezultatai.txt");
            }
            FirstList.Begin();
            if(FirstList.Exist())
            {

                InOut.PrintToFile("Rezultatai.txt", "First list", FirstList);

                Console.Write("Input first car maker: ");
                string FirstMaker = Console.ReadLine();

                Console.Write("Input second car maker: ");
                string SecondMaker = Console.ReadLine();

                SecondList = Task.Filter<Automobile, string>(FirstList, FirstMaker, null);
                ThirdList = Task.Filter<Automobile, string>(FirstList, null, SecondMaker);
                SecondList.Begin();
                ThirdList.Begin();
                if(SecondList.Exist() && ThirdList.Exist())
                {

                    InOut.PrintToFile("Rezultatai.txt", "Second list", SecondList);
                    InOut.PrintToFile("Rezultatai.txt", "Third list", ThirdList);

                    double MaxFromSecond = Task.PeakValue<double, Automobile>(SecondList);
                    double MaxFromThird = Task.PeakValue<double, Automobile>(ThirdList);
                    if (MaxFromSecond > MaxFromThird)
                    {
                        FourthList = Task.Filter<Automobile, double>(FirstList, MaxFromThird, MaxFromSecond);
                    }
                    else
                    {
                        FourthList = Task.Filter<Automobile, double>(FirstList, MaxFromSecond, MaxFromThird);
                    }
                    FourthList.Begin();
                    if(FourthList.Exist())
                    {
                        FourthList.Sort();
                        InOut.PrintToFile("Rezultatai.txt", "Fourth list", FourthList);
                    }

                    File.AppendAllText("Rezultatai.txt", $"The biggest mileage from the second list: {MaxFromSecond} made by {FirstMaker}\n\r");
                    File.AppendAllText("Rezultatai.txt", $"The biggest mileage from the third list: {MaxFromThird} made by {SecondMaker}\n\r");
                }
                else
                {
                    File.AppendAllText("Rezultatai.txt", $"The second or third list lacks data\n\r");
                }
            }
            else
            {
                File.AppendAllText("Rezultatai.txt", $"The first list lacks data\n\r");
            }
        }
    }
}
