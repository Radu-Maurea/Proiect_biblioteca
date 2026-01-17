using System.Text.Json;
namespace ProiectPoo;

public class Bibloteca
{
    private List<Utilizator> User;
    private List<Carte> Carti;
    
    public IReadOnlyList<Utilizator> Utilizatori => User.AsReadOnly();
    public IReadOnlyList<Carte> CartiPublic => Carti.AsReadOnly();
    
    
    private const string FisierUtilizatori = @"C:\Users\radum\Desktop\New folder (2)\ProiectPoo\ProiectPoo\Fisiere\utilizatori.json";
    private const string FisierCarti = @"C:\Users\radum\Desktop\New folder (2)\ProiectPoo\ProiectPoo\Fisiere\carti.json";

    public Bibloteca()
    {
        User = new List<Utilizator>();
        Carti = new List<Carte>();
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

    public void AfisareCarti()
    {
        IncarcaDate();
        foreach (var carte in Carti)
            Console.WriteLine(carte);
    }

    public void SalveazaDate()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            // Salvare Utilizatori
            string jsonUser = JsonSerializer.Serialize(User, options);
            File.WriteAllText(FisierUtilizatori, jsonUser);

            // Salvare Carti
            string jsonCarti = JsonSerializer.Serialize(Carti, options);
            File.WriteAllText(FisierCarti, jsonCarti);
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
            try
            {
                string jsonString = File.ReadAllText(FisierUtilizatori);
                User = JsonSerializer.Deserialize<List<Utilizator>>(jsonString, options) ?? new List<Utilizator>();
            }
            catch { Console.WriteLine("Eroare la incarcare utilizatori."); }
        }

        // Incarcare Carti
        if (File.Exists(FisierCarti))
        {
            try
            {
                string jsonString = File.ReadAllText(FisierCarti);
                Carti = JsonSerializer.Deserialize<List<Carte>>(jsonString, options) ?? new List<Carte>();
            }
            catch { Console.WriteLine("Eroare la incarcare carti."); }
        }
    }
    
    public bool ExistaEmail(string email)
    {
        email = email.ToLower().Trim();
        return User.Any(u => u.Email.ToLower().Trim() == email);
    }

    public Utilizator? Autentificare(string email, string parola)
    {
        email = email.ToLower().Trim();
        return User.FirstOrDefault(u => u.Email.ToLower().Trim() == email && u.Parola == parola);
    }
}