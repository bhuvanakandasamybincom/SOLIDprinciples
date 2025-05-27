/**** 1. Single responsibility ****/
// Documents class should only do document related things 
public class Documents()
{
     string text = "";
     string printModel = "";
    public string Text
    {
        get { return text; }
        set { text = value; }
    }
   public string PrintModel
    {
        get {
           return printModel ;
        }
        set {
            printModel = value ;
        }
    }


    public void Save()
    {
        //
    }
    public void DeleteFile(string filename)
    {
        //
    }
    //Print should NOT be allowed in document process
    //void print()
    //{ }

}
public interface IMachine
{
    public void Print(Documents documents);
    public void Scan(Documents documents);
    public void Fax(Documents documents);
}
public class MultiFunctionPrinter : IMachine
{
    public void Print(Documents d)
    {
        if (d.PrintModel == "2D")
        {
            Console.WriteLine("2d model");
        }
        else {
            Console.WriteLine("3d model");
        }
    }
    public void Scan(Documents d)
    {
        //
    }
    /**** 2. Open and Close principle***/
//Open for addition and close for modification in class/method
//We should not allow to change an existing code
public void Fax(Documents d)
    {
        //
    }
}
//Without implementation of Interface Segregation Principle in SOLID
//We're forcing this class to implement unwanted methods/members
public class OldFunctionedPrinter : IMachine
{
    public void Print(Documents d)
    {
        //
    }
    public void Scan(Documents d)
    {
        throw new NotImplementedException();
    }
    public void Fax(Documents d)
    {
        throw new NotImplementedException();
    }
}


public interface IPrinter
{
    public void Print(Documents d);
}
public interface IScaner
{
    public void Scan(Documents d);
}
public interface IFax
{
    public void Fax(Documents d);
}

/**** 4. Interface Segregation Principle in SOLID ****/
//Loosly coupled - Break into smaller parts
public class WithInterFaceSegregation : IPrinter, IScaner
{
    public void Print(Documents d)
    {
        throw new NotImplementedException();
    }

    public void Scan(Documents d)
    {
        throw new NotImplementedException();
    }
}
public interface IMultiFunctionedPrinter: IPrinter,IScaner
{

}
  public class MultiFunctionedPrinter : IMultiFunctionedPrinter
{
    IPrinter printer;
    IScaner scaner;

    public MultiFunctionedPrinter(IPrinter p, IScaner s)
    {
        this.printer = p;
        this.scaner = s;
    }

    public void Print(Documents d)
    {
        printer.Print(d);
    }

    public void Scan(Documents d)
    {
        scaner.Scan(d);
        //Decorator / Decorative pattern (Structural)
    }
}
//Singleton design pattern
public sealed class Singleton
{
    Singleton() { }
    private static Singleton instance=null;
    private static readonly object padlock = new object();
    public int count;//0
    public static Singleton Instance
    {
        get
        {
            lock (padlock)
            {
                //Instance will be created only time based on Singleton design pattern
                if (instance == null)
                {
                    instance = new Singleton();
                }

                return instance;
            }
        }
    }
    public int AddValue(int val1, int val2)
    {
        count++;
        return val1 + val2;
    }


}

public class SolidandPattern
{
    static public void Main(string[] args)
    {
        //Instance will be created only time and it will be accessed through the application.
        var db=Singleton.Instance;
       db.AddValue(1, 2);
        Console.WriteLine(db.count);
        var db2 = Singleton.Instance;
        db2.AddValue(2, 3);
        Console.WriteLine(db2.count);

        Documents d = new Documents();
        d.PrintModel = "2D";
        MultiFunctionPrinter multiPrinter = new();
        /**** 5. Dependency Inversion Principle ****/
        //High level model should not dependent on low level model, instead use abstraction.
        //Below should be implemented in Print 
        //if (d.PrintModel == "2d")
        //{
        //    Console.WriteLine("2d model");
        //}
        //else
        //{
        //    Console.WriteLine("3d model");
        //}
        multiPrinter.Print(d);
        MultiFunctionPrinter multiPrinter1 = new MultiFunctionPrinter();
        /*** 3. Liskov Substitution Principle***/
        //objects of a superclass should be replaceable with objects of its subclasses
        //without affecting the correctness of the program
        IMachine multiPrinter2 = new MultiFunctionPrinter();
        d.PrintModel = "3D";
        multiPrinter2.Print(d);
        Console.ReadLine();

    }
}


