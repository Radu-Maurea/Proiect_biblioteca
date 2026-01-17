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
        Console.WriteLine("Durata maxima de imprumut:");
        int durataMaxima = int.Parse(Console.ReadLine());
        Console.WriteLine("An:");
        if (int.TryParse(Console.ReadLine(), out int an))
        {
            Carte carte = new Carte(titlu, autor, an, numarCopii,durataMaxima);
            biblioteca.AdaugaCarte(carte); 
            Console.WriteLine("Cartea a fost adaugata cu succes!");
        }
        else Console.WriteLine("An invalid.");
    }
    
    public void CreareCategorieUI(Bibloteca biblioteca)
    {
        Console.WriteLine("Nume Categorie Noua:");
        string numeCat = Console.ReadLine();
        
        if(biblioteca.CategoriiPublic.Any(c => c.Nume == numeCat))
        {
            Console.WriteLine("Aceasta categorie exista deja!");
            return;
        }

        Categorie_Literara catNoua = new Categorie_Literara(numeCat);
        biblioteca.AdaugaCategorie(catNoua);
        Console.WriteLine($"Categoria '{numeCat}' a fost creata.");
    }
    
    public void AdaugaCarteInCategorieUI(Bibloteca biblioteca)
    {
        Console.WriteLine("Scrie numele cartii pe care vrei sa o incadrezi:");
        string numeCarte = Console.ReadLine();
        var carte = biblioteca.CartiPublic.FirstOrDefault(c => c.Nume == numeCarte);

        if (carte == null)
        {
            Console.WriteLine("Cartea nu exista.");
            return;
        }
        
        Console.WriteLine("Categoriile disponibile:");
        foreach(var c in biblioteca.CategoriiPublic) Console.WriteLine($"- {c.Nume}");

        Console.WriteLine("Scrie numele categoriei:");
        string numeCat = Console.ReadLine();
        var categorie = biblioteca.CategoriiPublic.FirstOrDefault(c => c.Nume == numeCat);

        if (categorie == null)
        {
            Console.WriteLine("Categoria nu exista.");
            return;
        }
        carte.Categorie = categorie.Nume;
        
        if(!categorie.Carti.Any(c => c.Nume == carte.Nume))
        {
            categorie.Carti.Add(carte);
        }

        biblioteca.SalveazaDate();
        Console.WriteLine($"Cartea '{carte.Nume}' a fost adaugata in categoria '{categorie.Nume}'.");
    }

    public void AfisareCarti(Bibloteca biblioteca) => biblioteca.AfisareCarti();

    public void StergeCarte(Bibloteca biblioteca)
    {
        Console.WriteLine("Nume Carte:");
        string numeCarte = Console.ReadLine();
        var carteCautata = biblioteca.CartiPublic.FirstOrDefault(c => c.Nume == numeCarte);
        if (carteCautata == null) Console.WriteLine("Cartea nu exista.");
        else biblioteca.StergeCarte(carteCautata);
    }

    public void ModificaCarte(Bibloteca biblioteca)
    {
        Console.WriteLine("Nume Carte:");
        string nume = Console.ReadLine();
        var carte = biblioteca.CartiPublic.FirstOrDefault(c => c.Nume == nume);
        if (carte == null)
            Console.WriteLine("Cartea nu a fost gasita!");
        else biblioteca.ModificaCarte(carte);
    }
}