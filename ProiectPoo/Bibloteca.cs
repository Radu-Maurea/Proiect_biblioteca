using System.Text.Json;
namespace ProiectPoo;

public class Bibloteca
{
    private List<Utilizator> User;
    private List<Carte> Carti;
    private List<Categorie_Literara> Categorii;
    
    public IReadOnlyList<Utilizator> Utilizatori => User.AsReadOnly();
    public IReadOnlyList<Carte> CartiPublic => Carti.AsReadOnly();
    public IReadOnlyList<Categorie_Literara> CategoriiPublic => Categorii.AsReadOnly();
    
    private const string FisierUtilizatori = @"C:\Users\radum\Desktop\New folder (2)\ProiectPoo\ProiectPoo\Fisiere\utilizatori.json";
    private const string FisierCarti = @"C:\Users\radum\Desktop\New folder (2)\ProiectPoo\ProiectPoo\Fisiere\carti.json";
    private const string FisierCategorii = @"C:\Users\radum\Desktop\New folder (2)\ProiectPoo\ProiectPoo\Fisiere\categorii.json";
    private const string FisierSetari = @"C:\Users\radum\Desktop\New folder (2)\ProiectPoo\ProiectPoo\Fisiere\setari.json";
    
    public int LimitaCartiPerUtilizator { get; set; } = 3;
    public int DurataImprumutStandard { get; set; } = 14;
    public double PenalizarePeZi { get; set; } = 5.0;
    

    public Bibloteca()
    {
        User = new List<Utilizator>();
        Carti = new List<Carte>();
        Categorii = new List<Categorie_Literara>();
        IncarcaDate();
    }

    public void AdaugaUtilizator(Utilizator utilizator)
    {
        User.Add(utilizator);
        SalveazaDate();
    }

    public void AdaugaCarte(Carte carte)
    {
        Carti.Add(carte);
        SalveazaDate();
    }
    
    public void AdaugaCategorie(Categorie_Literara categorie)
    {
        Categorii.Add(categorie);
        SalveazaDate();
    }

    public void StergeCarte(Carte carte)
    {
        Carti.Remove(carte);
        foreach(var cat in Categorii)
        {
            var carteInCat = cat.Carti.FirstOrDefault(c => c.Nume == carte.Nume);
            if(carteInCat != null) cat.Carti.Remove(carteInCat);
        }
        SalveazaDate();
    }

    public void AfisareCarti()
    {
        IncarcaDate(); 
        Console.WriteLine("\n--- Toate Cartile ---");
        foreach (var carte in Carti)
            Console.WriteLine(carte);
    }

    public void ModificaCarte(Carte carte)
    {
        Console.WriteLine("Doriti sa schimbati titlul ? \n1.Da\n2.Nu\n");
        int opt = int.Parse(Console.ReadLine());
        if (opt == 1)
        {
            Console.WriteLine("Titlu nou:");
            string titlu = Console.ReadLine();
            carte.Nume = titlu;
        }
        Console.WriteLine("Doriti sa schimbati autor ? \n1.Da\n2.Nu\n");
        opt = int.Parse(Console.ReadLine());
        if (opt == 1)
        {
            Console.WriteLine("Autor nou:");
            string autor = Console.ReadLine();
            carte.Autor = autor;
        }
        Console.WriteLine("Doriti sa schimbati anul publicarii ? \n1.Da\n2.Nu");
        opt = int.Parse(Console.ReadLine());
        if (opt == 1)
        {
            Console.WriteLine("An nou:");
            int an =  int.Parse(Console.ReadLine());
            carte.An = an;
        }
        Console.WriteLine("Doriti sa schimbati numarul de exemplare ? \n1.Da\n2.Nu");
        opt = int.Parse(Console.ReadLine());
        if (opt == 1)
        {
            Console.WriteLine("Numar nou de exemplare:");
            int exemplare =  int.Parse(Console.ReadLine());
            carte.NumarCopii = exemplare;
        }
        SalveazaDate();
    }
    
    public void AfisareCategorii()
    {
        Console.WriteLine("\n--- Categorii Literare ---");
        foreach (var cat in Categorii)
        {
            Console.WriteLine($"Categoria: {cat.Nume} (Contine {cat.Carti.Count} carti)");
        }
    }

    public void SalveazaDate()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            // Salvare Utilizatori
            File.WriteAllText(FisierUtilizatori, JsonSerializer.Serialize(User, options));

            // Salvare Carti
            File.WriteAllText(FisierCarti, JsonSerializer.Serialize(Carti, options));

            // Salvare Categorii
            File.WriteAllText(FisierCategorii, JsonSerializer.Serialize(Categorii, options));
            
            var setari = new 
            { 
                Limita = LimitaCartiPerUtilizator, 
                Durata = DurataImprumutStandard, 
                Penalizare = PenalizarePeZi 
            };
            File.WriteAllText(FisierSetari, JsonSerializer.Serialize(setari, options));
            
        }
        catch (Exception e)
        {
            Console.WriteLine($"Eroare la salvare: {e.Message}");
        }
    }

    public void IncarcaDate()
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        // Incarcare Utilizatori
        if (File.Exists(FisierUtilizatori))
        {
            try { User = JsonSerializer.Deserialize<List<Utilizator>>(File.ReadAllText(FisierUtilizatori), options) ?? new List<Utilizator>(); }
            catch { }
        }

        // Incarcare Carti
        if (File.Exists(FisierCarti))
        {
            try { Carti = JsonSerializer.Deserialize<List<Carte>>(File.ReadAllText(FisierCarti), options) ?? new List<Carte>(); }
            catch { }
        }

        //Incarcare Categorii
        if (File.Exists(FisierCategorii))
        {
            try { Categorii = JsonSerializer.Deserialize<List<Categorie_Literara>>(File.ReadAllText(FisierCategorii), options) ?? new List<Categorie_Literara>(); }
            catch { }
        }

        if (File.Exists(FisierSetari))
        {
            try
            {
                string json = File.ReadAllText(FisierSetari);
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    var root = doc.RootElement;
                    if(root.TryGetProperty("Limita", out var lim)) LimitaCartiPerUtilizator = lim.GetInt32();
                    if(root.TryGetProperty("Durata", out var dur)) DurataImprumutStandard = dur.GetInt32();
                    if(root.TryGetProperty("Penalizare", out var pen)) PenalizarePeZi = pen.GetDouble();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Eroare la salvare: {e.Message}");
            }
            
        }
    }
    
    public bool ExistaEmail(string email)
    {
        return User.Any(u => u.Email.ToLower().Trim() == email.ToLower().Trim());
    }

    public bool EDisponibilaCartea(Carte carte)
    {
        if (carte.NumarCopii > 0) return true;
        return false;
    }
    
    public Utilizator? Autentificare(string email, string parola)
    {
        return User.FirstOrDefault(u => u.Email.ToLower().Trim() == email.ToLower().Trim() && u.Parola == parola);
    }

    
    
}