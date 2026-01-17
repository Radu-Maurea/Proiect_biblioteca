using System.Text.Json.Serialization;
namespace ProiectPoo;

// [JsonDerivedType(typeof(Bibliotecar), typeDiscriminator: "Bibliotecar")]
// [JsonDerivedType(typeof(Client), typeDiscriminator: "Client")]
public class Client : Utilizator
{
    public List<Carte> CartiImprumutate;

    public Client(string email, string parola) : base(email, parola)
    {
        CartiImprumutate = new List<Carte>();
    }
    public override string Rol() => "Client";


    public void CautareCarte(Bibloteca biblioteca)
    {
        Console.WriteLine("Doriti sa cautati dupa titlu?\n1.Da\n2.Nu");
        int opt = int.Parse(Console.ReadLine());
        if (opt == 1)
        {
            Console.WriteLine("Titlul Cautat:");
            string titlu = Console.ReadLine();
            var cautat = biblioteca.CartiPublic.FirstOrDefault(x=>x.Nume==titlu);
            if (cautat == null)
            {
                Console.WriteLine("Cartea nu exista!");
            }
            else Console.WriteLine(cautat);
        }
        Console.WriteLine("Doriti sa cautati dupa autor?\n1.Da\n2.Nu");
        opt = int.Parse(Console.ReadLine());
        if (opt == 1)
        {
            Console.WriteLine("Titlul Cautat:");
            string autor = Console.ReadLine();
            var cautat = biblioteca.CartiPublic.FirstOrDefault(x=>x.Autor==autor);
            if (cautat == null)
            {
                Console.WriteLine("Cartea nu exista!");
            }
            else Console.WriteLine(cautat);
        }
        Console.WriteLine("Doriti sa cautati dupa gen?\n1.Da\n2.Nu");
        opt = int.Parse(Console.ReadLine());
        if (opt == 1)
        {
            Console.WriteLine("Genul cautat:");
            string genul = Console.ReadLine();
            foreach (var g in biblioteca.CartiPublic)
            {
                if (g.Categorie == genul)
                {
                    Console.WriteLine(g);
                }
            }

        }
    }

    public void ImprumutaCarte(Bibloteca biblioteca)
    {
        bool ok = true;
        Console.WriteLine("Titlul cartii cautate:");
        string titlu = Console.ReadLine();

        foreach (var g in CartiImprumutate)
        {
            if (titlu == g.Nume)
            {
                Console.WriteLine("Ati imprumutat deja aceasta carte!");
                ok = false;
            }
                
        }
        if (ok)
        {
            var cautat =  biblioteca.CartiPublic.FirstOrDefault(x => x.Nume==titlu);
            if (cautat == null)
            {
                Console.WriteLine("Cartea nu exista!");
            }
            else
            {
                if (biblioteca.EDisponibilaCartea(cautat))
                {
                    CartiImprumutate.Add(cautat);
                    cautat.NumarCopii--;
                    biblioteca.SalveazaDate();
                }
                else
                {
                    Console.WriteLine("Cartea nu mai e disponibila!");
                }
            }
        }
        
    }

    public void RestituireCarte(Bibloteca biblioteca)
    {
        if (CartiImprumutate == null || CartiImprumutate.Count == 0)
        {
            Console.WriteLine("Nu aveti carti imprumutate!");
        }
        else
        {
            Console.WriteLine("Titlul Imprumutate:");
            foreach (var c in CartiImprumutate) Console.WriteLine(c);
        
            Console.WriteLine("Titlul cartii cautate:");
            string titlu = Console.ReadLine();
            var cautat =  biblioteca.CartiPublic.FirstOrDefault(x => x.Nume==titlu);
            if (cautat == null)
            {
                Console.WriteLine("Cartea nu exista!");
            }
            else
            {
                CartiImprumutate.Remove(cautat);
                cautat.NumarCopii++;
                biblioteca.SalveazaDate();
            }
        }
        
    }
}