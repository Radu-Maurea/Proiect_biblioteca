namespace ProiectPoo;

public class Bibliotecar : Utilizator
{
    public Bibliotecar(string email, string parola) : base(email, parola){}
    
    public override string Rol() => "Manager";

    public void AdaugaCarteUI(Bibloteca biblioteca)
    {
        Console.WriteLine("--- Adaugare Carte Noua ---");
        Console.WriteLine("Titlu:");
        string titlu = Console.ReadLine();
        
        Console.WriteLine("Autor:");
        string autor = Console.ReadLine();
        
        Console.WriteLine("Numar Copii:");
        int numarCopii = int.Parse(Console.ReadLine());
        
        Console.WriteLine("An:");
        
        if (int.TryParse(Console.ReadLine(), out int an))
        {
            Carte carte = new Carte(titlu, autor, an,numarCopii);
            
            
            biblioteca.AdaugaCarte(carte); 
            Console.WriteLine("Cartea a fost adaugata cu succes!");
        }
        else
        {
            Console.WriteLine("Anul introdus nu este valid.");
        }
    }

    public void AfisareCarti(Bibloteca biblioteca)
    {
        biblioteca.AfisareCarti();
    }
}