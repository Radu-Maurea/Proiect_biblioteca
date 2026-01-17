namespace ProiectPoo;

public class Imprumut
{
    public Carte CarteaImprumutata { get; set; }
    public DateTime DataImprumut { get; set; }
    public DateTime DataScadenta { get; set; } 
    public DateTime? DataReturnare { get; set; } 

    
    public Imprumut() { }

    public Imprumut(Carte carte, int durataZile)
    {
        CarteaImprumutata = carte;
        DataImprumut = DateTime.Now;
        DataScadenta = DateTime.Now.AddDays(durataZile);
        DataReturnare = null; 
    }

    public bool EsteIntarziata()
    {
        return DataReturnare == null && DateTime.Now > DataScadenta;
    }
}