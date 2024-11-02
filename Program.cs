using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Linq;
using System.Diagnostics;

namespace Prac_4
{
    interface IComparable
    {
        int CompareTo(object o);
    }

    //часть 1
    public class Task1
    {

        string[] months = {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"
        };

        public void getMonthsOfLen(int n)
        {

            var selectedMonths = from month in months
                                 where (month.Length == n)
                                 orderby month
                                 select month;

            foreach (string item in selectedMonths)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\n");

        }

        public void getSummerWinterMonths()
        {

            var selectedMonths = from month in months
                                 where ((month == "January")
                                         || (month == "February")
                                         || (month == "December")
                                         || (month == "June")
                                         || (month == "July")
                                         || (month == "August"))
                                 orderby month
                                 select month;

            foreach (string item in selectedMonths)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\n");
        }

        public void printAlphabetically()
        {

            var selectedMonths = from month in months
                                 orderby month
                                 select month;

            foreach (string item in selectedMonths)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\n");

        }

        public void getMonthsWithUandLenOfFour()
        {

            var selectedMonths = from month in months
                                 where ((month.Length == 4) && (month.ToLower().Contains('u')))
                                 orderby month
                                 select month;

            foreach (string item in selectedMonths)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\n");

        }

    }

    //конец части 1




    public class Data : IComparable<Data>
    {
        private int day { get; set; }
        private int month { get; set; }
        private int year { get; set; }

        public Data() { }

        //проверяка на вводимые числа и  присловение полямт значений
        public Data(int d, int m, int y)
        {
            year = y;
            if (m < 1 && m > 12)
            {
                throw new Exception("Month should be between 1 and 12 ");
            }
            else month = m;

            if (m == 2 && y % 4 == 0)
            {
                if (d < 1 || d > 29)
                {
                    throw new Exception("In february should be between 1 and 29 ");
                }
                else day = d;
            }
            else if (m == 2 && y % 4 != 0)
            {
                if (d < 1 || d > 28)
                {
                    Console.WriteLine("In february should be between 1 and 28");
                    //throw new Exception("In february should be between 1 and 28 ");
                }
                else day = d;
            }
            else if (m == 1 || m == 3 || m == 5 || m == 7 || m == 8 || m == 10 || m == 12)
            {
                if (d < 1 || d > 31)
                {
                    throw new Exception("Day should be between 1 and 31 ");
                }
                else day = d;
            }
            else
            {
                if (d < 1 || d > 30)
                {
                    throw new Exception("Day should be between 1 and 30 ");
                }
                else day = d;
            }
        }

        public int getYear()
        {
            return year;
        }
        public int getDay()
        {
            return day;
        }
        public int getMonth()
        {
            return month;
        }

        //печать экземпляра класса в виде ДДММГГ
        public void printData()
        {
            Console.WriteLine("Data :");
            Console.WriteLine($"{day}.{month}.{year}\n");
        }

        //сравнение
        public int CompareTo(Data other)
        {
            if (other == null) return 1; // Текущий объект больше null

            int yearComparison = year.CompareTo(other.year);
            if (yearComparison != 0) return yearComparison; 

            int monthComparison = year.CompareTo(other.month);
            if (monthComparison != 0) return monthComparison; 

            return day.CompareTo(other.day); 
        }



    }

    public class List
    {
        //печатает лист
        public void printData(List<Data>datalist)
        {
            Console.WriteLine("List:\n");
            foreach(Data d in datalist)
            {
               d.printData();
            }
        }

        //добавляет экзмемпляр класска в лист
        public void Add(List<Data> datalist, Data d)
        {
            datalist.Add(d);
        }

        //выводит все даты с заданным годом
        public void Year_data(List<Data> datalist,int y)
        {
            var  year_data  =from Data d in datalist 
                             where(d.getYear()==y)
                             select d;

            Console.WriteLine($"In {y} year data:");
            foreach (Data a in year_data)
            {
                Console.WriteLine($"{a.getDay()}.{a.getMonth()}.{a.getYear()}\n");

            }
        }

        //выводит все даты с заданным месяцем
        public void Month_data(List<Data> datalist, int m)
        {
            var year_data = from Data d in datalist
                            where (d.getMonth() == m)
                            select d;

            Console.WriteLine($"In {m} month data:");
            foreach (Data a in year_data)
            {
                Console.WriteLine($"{a.getDay()}.{a.getMonth()}.{a.getYear()}\n");

            }
        }


        //выводит количество дат в диапазонпе от a до b
        public void data_in_range(List<Data> datalist, Data a, Data b)
        {
            int count = 0;
            var data = from Data d in datalist
                       where (d.getDay() >= a.getDay() && d.getDay() <=b.getDay()
                       && d.getMonth() >= a.getMonth() && d.getMonth() <= b.getMonth()
                       && d.getYear() >= a.getYear() && d.getYear() <= b.getYear())
                       select d;
            foreach(var i in data)
            {
                count++;
            }
            Console.WriteLine("From:");
            a.printData();
            Console.WriteLine("to:");
            b.printData();
            Console.WriteLine($"The number of dates in the range {count}");

        }

        //выводит максимальную дату
        public void max_data(List<Data> datalist)
        {
            Data d_max = new Data();
            d_max = datalist.Max();
            Console.WriteLine("Max data in list:");
            d_max.printData();
        }

        // выводит первую дату для заданного дня
        public void earliest_date(List<Data> datalist, int day) 
        {
            Data temp_date = new Data(31, 12, 99999);
            foreach(var d in datalist)
            {
                if(d.getDay() == day && d.getYear() <= temp_date.getYear())
                {
                    if (d.getMonth() <= temp_date.getMonth())
                    {
                        temp_date = d;
                    }
                }
            }
            Console.WriteLine($"Earliest data with day {day}:");
            temp_date.printData();
        }


    }


    public class Program
    {
        public static void Main(string[] args)
        {
            Task1 monthsTask = new Task1();
            monthsTask.getMonthsOfLen(7);
            monthsTask.getMonthsOfLen(8);
            monthsTask.getSummerWinterMonths();
            monthsTask.printAlphabetically();
            monthsTask.getMonthsWithUandLenOfFour();


            Data data_1 = new Data(7, 2, 2022);
            Data data_2 = new Data(8, 2, 2022);
            Data data_3 = new Data(6, 7, 2022);
            Data data_4 = new Data(4, 2, 2022);
            Data data_5 = new Data(8, 7, 2022);
            data_1.printData();
            Console.WriteLine("------------------------------------------------------------------");

            List A = new List();
            List<Data> datalist = new List<Data>();
            A.Add(datalist,data_1);
            A.Add(datalist, data_2);
            A.Add(datalist, data_3);  
            A.Add(datalist, data_4);
            A.Add(datalist, data_5);
            A.Year_data( datalist,2022);
            Console.WriteLine("------------------------------------------------------------------");
            A.Month_data(datalist, 2);
            Console.WriteLine("------------------------------------------------------------------");
            A.data_in_range(datalist, data_1, data_5);
            Console.WriteLine("------------------------------------------------------------------");
            int day = 8;
            A.max_data(datalist);
            Console.WriteLine("------------------------------------------------------------------");
            A.earliest_date(datalist, day);
            Console.WriteLine("------------------------------------------------------------------");
            A.printData(datalist);
            Console.WriteLine("------------------------------------------------------------------");

            // Сортируем список по возрастанию дат
            datalist.Sort((x, y) =>
            {
                if (x.getYear() != y.getYear()) return (x.getYear()).CompareTo(y.getYear());
                if (x.getMonth() != y.getMonth()) return (x.getMonth()).CompareTo(y.getMonth());
                return x.getDay().CompareTo(y.getDay());
            });

            // Выводим отсортированный список
            Console.WriteLine("The ascending list");
            foreach (var date in datalist)
            {
                date.printData();
            }
            Console.WriteLine("------------------------------------------------------------------");



        }
    }
}