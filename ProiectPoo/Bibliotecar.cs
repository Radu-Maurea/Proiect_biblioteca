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

        Console.WriteLine("Scrie numele categoriei pe care vrei sa o adaugi:");
        string numeCat = Console.ReadLine();
        var categorie = biblioteca.CategoriiPublic.FirstOrDefault(c => c.Nume == numeCat);

        if (categorie == null)
        {
            Console.WriteLine("Categoria nu exista.");
            return;
        }
        if (!carte.Categorie.Contains(categorie.Nume))
        {
            carte.Categorie.Add(categorie.Nume); 
            Console.WriteLine($"Categoria '{categorie.Nume}' a fost adaugata cartii.");
        }
        else
        {
            Console.WriteLine("Cartea face deja parte din aceasta categorie.");
        }
        if(!categorie.Carti.Any(c => c.Nume == carte.Nume))
        {
            categorie.Carti.Add(carte);
        }

        biblioteca.SalveazaDate();
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
    
    public void MonitorizareImprumuturi(Bibloteca biblioteca)
    {
        Console.WriteLine("\n=== Monitorizare Imprumuturi ===");
        bool existaImprumuturi = false;

        foreach (var user in biblioteca.Utilizatori)
        {
            if (user is Client client)
            {
                var active = client.CartiImprumutate.Where(i => i.DataReturnare == null).ToList();
                if (active.Count > 0)
                {
                    existaImprumuturi = true;
                    Console.WriteLine($"\nClient: {client.Nume} ({client.Email})");
                
                    foreach (var imp in active)
                    {
                        string status = "In termen";
                        if (imp.EsteIntarziata())
                        {
                            status = "!!! INTARZIAT !!!";
                        }

                        Console.WriteLine($"   - Carte: {imp.CarteaImprumutata.Nume}");
                        Console.WriteLine($"     Data Imprumut: {imp.DataImprumut:dd/MM/yyyy}");
                        Console.WriteLine($"     Scadenta: {imp.DataScadenta:dd/MM/yyyy} -> Status: {status}");
                    }
                }
            }
        }

        if (!existaImprumuturi)
        {
            Console.WriteLine("Nu exista imprumuturi active in acest moment.");
        }
    }
    
    public void ConfigurareReguliUI(Bibloteca biblioteca)
    {
        Console.WriteLine("\n=== Configurare Reguli Imprumut ===");
        Console.WriteLine($"1. Limita carti per utilizator (Curent: {biblioteca.LimitaCartiPerUtilizator})");
        Console.WriteLine($"2. Durata standard imprumut (Curent: {biblioteca.DurataImprumutStandard} zile)");
        Console.WriteLine($"3. Penalizare intarziere (Curent: {biblioteca.PenalizarePeZi} RON/zi)");
        Console.WriteLine("0. Inapoi");
        
        int opt = int.Parse(Console.ReadLine());
        
        switch (opt)
        {
            case 1:
                Console.WriteLine("Noua limita de carti:");
                biblioteca.LimitaCartiPerUtilizator = int.Parse(Console.ReadLine());
                break;
            case 2:
                Console.WriteLine("Noua durata standard (zile):");
                biblioteca.DurataImprumutStandard = int.Parse(Console.ReadLine());
                break;
            case 3:
                Console.WriteLine("Noua penalizare (RON/zi):");
                biblioteca.PenalizarePeZi = double.Parse(Console.ReadLine());
                break;
            case 0:
                return;
        }
        biblioteca.SalveazaDate(); 
        Console.WriteLine("Regulile au fost actualizate!");
    }
    
}