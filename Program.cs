using System;

class Program
{
    readonly Person[] people = { new Person("Z", 9), new Person("X", 4), new Person("Y", 1) };
    readonly LinkedList llist = new LinkedList();
    static void Main()
    {
        Program p = new Program();
        p.Run();
    }
    void Run()
    {
        llist.Add(15);
        llist.Add(10);
        llist.Add(5);
        llist.Add(20);
        llist.Add(3);
        llist.PrintList();
        while (true)
        {
            string input = Console.ReadLine().ToLower();
            switch (input)
            {
                case "exit":
                    Environment.Exit(0);
                    break;
                case "selectsort": // Complexity O(n^2)
                case "s":
                case "name":
                    Person.SelectSort(people);
                    Person.PrintArray(people);
                    break;
                case "bubblesort": // Complexity worst: O(n^n) best: O(n)
                case "b":
                case "age":
                    Person.BubbleSort(people);
                    Person.PrintArray(people);
                    break;
                case "gnomesort": // Complexity O(n^2)
                case "g":
                case "gnome":
                    Person.GnomeSort(people, 3);
                    Person.PrintArray(people);
                    break;
                case "bogosort": //Complexity worst: O(∞) avarage: O(n*n!) best: O(n)
                case "bogo":
                    Person.BogoSort.Bogosort(people, people.Length);
                    Person.PrintArray(people);
                    break;
                case "mergesort": // Complexity O(n Log n)
                case "m":
                case "merge":
                    llist.head = llist.MergeSort(llist.head);
                    llist.PrintList();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Unknown command");
                    Console.ResetColor();
                    break;
            }
        }
    }
}
class Person
{
    private readonly string name;
    private readonly int age;
    public Person(string name, int age)
    {
        this.name = name;
        this.age = age;
    }
    public static void SelectSort(Person[] people)
    {
        int pos_min;
        Person temp;
        for (int i = 0; i < people.Length - 1; i++)
        {
            pos_min = i;
            for (int j = i + 1; j < people.Length; j++)
            {
                if (people[j].name.CompareTo(people[pos_min].name) < 0)
                {
                    pos_min = j;
                }
            }
            if (pos_min != i)
            {
                temp = people[i];
                people[i] = people[pos_min];
                people[pos_min] = temp;
            }
        }
    }
    public static void BubbleSort(Person[] people)
    {
        int n = people.Length;
        for (int i = 0; i < n - 1; i++)
            for (int j = 0; j < n - i - 1; j++)
                if (people[j].age > people[j + 1].age)
                {
                    Person temp = people[j];
                    people[j] = people[j + 1];
                    people[j + 1] = temp;
                }
    }
    public static void GnomeSort(Person[] people, int n)
    {
        int index = 0;

        while (index < n)
        {
            if (index == 0)
                index++;
            if (people[index].age >= people[index - 1].age)
                index++;
            else
            {
                Person temp;
                temp = people[index];
                people[index] = people[index - 1];
                people[index - 1] = temp;
                index--;
            }
        }
        return;
    }
    public static void PrintArray(Person[] people)
    {
        int n = people.Length;
        for (int i = 0; i < n; ++i)
            Console.WriteLine(people[i].name + "," + people[i].age);
    }
    public class BogoSort
    {
        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
        public static bool IsSorted(Person[] people, int length)
        {
            int i = 0;
            while (i < length - 1)
            {
                if (people[i].age > people[i + 1].age)
                    return false;
                i++;
            }
            return true;
        }
        public static void Shuffle(Person[] people, int length)
        {
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
                Swap(ref people[i], ref people[rnd.Next(0, length)]);
        }
        public static void Bogosort(Person[] people, int length)
        {
            while (!IsSorted(people, length))
                Shuffle(people, length);
        }
    }
}
public class LinkedList
{
    public Node head;
    public class Node
    {
        public int data;
        public Node next;
        public Node(int data)
        {
            this.data = data;
            next = null;

        }
    }
    public void Add(int new_data)
    {
        Node new_node = new Node(new_data)
        {
            next = head
        };

        head = new_node;
    }
    private Node SortedMerge(Node a, Node b)
    {
        if (a == null)
            return b;
        if (b == null)
            return a;

        Node result;
        if (a.data <= b.data)
        {
            result = a;
            result.next = SortedMerge(a.next, b);
        }
        else
        {
            result = b;
            result.next = SortedMerge(a, b.next);
        }
        return result;
    }
    public Node MergeSort(Node h)
    {
        if (h == null || h.next == null)
        {
            return h;
        }
        Node middle = GetMiddle(h);
        Node nextofmiddle = middle.next;

        middle.next = null;

        Node left = MergeSort(h);

        Node right = MergeSort(nextofmiddle);

        Node sortedlist = SortedMerge(left, right);
        return sortedlist;
    }
    private Node GetMiddle(Node h)
    {
        // Base case 
        if (h == null)
            return h;
        Node fastptr = h.next;
        Node slowptr = h;

        while (fastptr != null)
        {
            fastptr = fastptr.next;
            if (fastptr != null)
            {
                slowptr = slowptr.next;
                fastptr = fastptr.next;
            }
        }
        return slowptr;
    }
    public void PrintList()
    {
        Node n = head;
        while (n != null)
        {
            Console.WriteLine(n.data);
            n = n.next;
        }
    }
}