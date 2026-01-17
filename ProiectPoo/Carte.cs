
namespace ProiectPoo;

public class Carte
{
    public string Nume { get; set; }
    public string Autor { get; set; }
    public int An { get; set; }
    public int NumarCopii { get; set; }
    public int DurataMaxima { get; set; }
    public List<string> Categorie { get; set; } 
    public List<string> Recenzie { get; set; }
    public List<int> Note { get; set; }
    public List<string> Rezervari { get; set; }
    public Carte(string nume, string autor, int an, int numarCopii, int durataMaxima)
    {
        Nume = nume;
        Autor = autor;
        An = an;
        NumarCopii = numarCopii;
        DurataMaxima = durataMaxima;
        Categorie = new List<string>(); 
        Recenzie = new List<string>();
        Note = new List<int>();
        Rezervari = new List<string>();
    }

    public void AdaugaRecenzie(string recenzie)
    {
        Recenzie.Add(recenzie);
    }

    public void VeziRecenzii()
    {
        foreach (var recenzie in Recenzie)
        {
            Console.WriteLine(recenzie);
        }
    }
    public override string ToString()
    {
        string infoCat = (Categorie == null || Categorie.Count == 0) 
            ? "Fara Categorie" 
            : string.Join(", ", Categorie);
                         
        return $"{Nume} de ({Autor}, {An}), Copii: {NumarCopii}, Categorii: [{infoCat}]";
    }
}

public class Categorie_Literara
{
    public string Nume { get; set; }
    public List<Carte> Carti { get; set; }

    public Categorie_Literara(string nume)
    {
        Nume = nume;
        Carti = new List<Carte>();
    }
}