
namespace ProiectPoo;

public class Client : Utilizator
{
    // MODIFICARE: Lista este acum de tip 'Imprumut' pentru a retine si datele
    public List<Imprumut> CartiImprumutate { get; set; }

    public Client(string email, string parola) : base(email, parola)
    {
        CartiImprumutate = new List<Imprumut>();
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
            var cautat = biblioteca.CartiPublic.FirstOrDefault(x => x.Nume.ToLower().Contains(titlu.ToLower()));
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
            Console.WriteLine("Autorul Cautat:");
            string autor = Console.ReadLine();
            var cautat = biblioteca.CartiPublic.FirstOrDefault(x => x.Autor.ToLower().Contains(autor.ToLower()));
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
            bool gasit = false;
            foreach (var g in biblioteca.CartiPublic)
            {
                // Verificam in lista de categorii (daca ai facut modificarea anterioara)
                // Daca nu ai facut modificarea cu lista, foloseste: if (g.Categorie == genul)
                if (g.Categorie != null && g.Categorie.Any(c => c.Equals(genul, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine(g);
                    gasit = true;
                }
            }
            if (!gasit) Console.WriteLine("Nu s-au gasit carti la acest gen.");
        }
    }

    public void ImprumutaCarte(Bibloteca biblioteca)
    {
        // Verificarea limitei de carti 
        int cartiActive = CartiImprumutate.Count(i => i.DataReturnare == null);
        if (cartiActive >= biblioteca.LimitaCartiPerUtilizator)
        {
            Console.WriteLine("Ai atins limita de carti imprumutate!");
            return;
        }

        Console.WriteLine("Titlul cartii cautate:");
        string titlu = Console.ReadLine();

        // Verificam daca clientul are deja cartea imprumutata
        if (CartiImprumutate.Any(g => g.CarteaImprumutata.Nume == titlu && g.DataReturnare == null))
        {
            Console.WriteLine("Ati imprumutat deja aceasta carte si nu ati returnat-o!");
            return;
        }

        var carte = biblioteca.CartiPublic.FirstOrDefault(x => x.Nume == titlu);

        if (carte == null)
        {
            Console.WriteLine("Cartea nu exista!");
            return;
        }

         // --- LOGICA NOUA PENTRU REZERVARI ---

        // CAZ 1: Cartea este disponibila fizic (stoc > 0)
        if (biblioteca.EDisponibilaCartea(carte))
        {
            // Verificam daca exista o lista de asteptare
            if (carte.Rezervari.Count > 0)
            {
                // Daca utilizatorul curent NU este primul pe lista
                if (carte.Rezervari[0] != this.Email)
                {
                    Console.WriteLine($"Cartea este disponibila, dar este REZERVATA pentru alt utilizator.");
                    Console.WriteLine("Trebuie sa asteptati pana cand acea persoana isi ridica rezervarea sau renunta la ea.");
                
                    // Optional: Se poate adauga la coada daca nu e deja
                    if (!carte.Rezervari.Contains(this.Email))
                    {
                        Console.WriteLine("Doriti sa va adaugati la coada de asteptare? (1.Da / 2.Nu)");
                        if (int.Parse(Console.ReadLine()) == 1)
                        {
                            carte.Rezervari.Add(this.Email);
                            Console.WriteLine("Ati fost adaugat in lista de asteptare.");
                            biblioteca.SalveazaDate();
                        }
                    }
                return;
                }
                else
                {
                // Utilizatorul ESTE primul pe lista, deci are prioritate
                Console.WriteLine("Rezervarea dumneavoastra a sosit! Puteti imprumuta cartea.");
                carte.Rezervari.RemoveAt(0); // Il scoatem din lista de rezervari
                }
        }

        // Procesul standard de imprumut [cite: 121-122]
        Imprumut imprumutNou = new Imprumut(carte, carte.DurataMaxima);
        CartiImprumutate.Add(imprumutNou);
        carte.NumarCopii--;
        biblioteca.SalveazaDate();
        Console.WriteLine($"Ai imprumutat cartea! Data scadenta: {imprumutNou.DataScadenta:dd/MM/yyyy}");
    }
    else
    {
        Console.WriteLine("Cartea nu mai este disponibila momentan.");
        
        // Verificam daca utilizatorul este deja pe lista
        if (carte.Rezervari.Contains(this.Email))
        {
            int pozitie = carte.Rezervari.IndexOf(this.Email) + 1;
            Console.WriteLine($"Sunteti deja pe lista de asteptare. Pozitia dumneavoastra: {pozitie}.");
        }
        else
        {
            Console.WriteLine($"Exista {carte.Rezervari.Count} persoane in asteptare.");
            Console.WriteLine("Doriti sa rezervati cartea? (1.Da / 2.Nu)");
            
            string optiune = Console.ReadLine();
            if (optiune == "1")
            {
                carte.Rezervari.Add(this.Email);
                biblioteca.SalveazaDate();
                Console.WriteLine("Ati rezervat cartea cu succes! Veti avea prioritate cand va fi returnata.");
            }
        }
    }
}

    public void RestituireCarte(Bibloteca biblioteca)
    {
        var active = CartiImprumutate.Where(x => x.DataReturnare == null).ToList();

        if (CartiImprumutate == null || active.Count == 0)
        {
            Console.WriteLine("Nu aveti carti active de restituit!");
        }
        else
        {
            Console.WriteLine("Cartile tale active:");
            foreach (var c in active) 
                Console.WriteLine($"{c.CarteaImprumutata.Nume} (Scadenta: {c.DataScadenta})");
        
            Console.WriteLine("Scrie titlul cartii pe care vrei sa o restitui:");
            string titlu = Console.ReadLine();

            // Cautam in lista de imprumuturi un imprumut activ cu acest titlu
            var imprumutGasit = CartiImprumutate.FirstOrDefault(x => x.CarteaImprumutata.Nume == titlu && x.DataReturnare == null);
            
            if (imprumutGasit == null)
            {
                Console.WriteLine("Nu aveti aceasta carte imprumutata sau ati introdus titlul gresit!");
            }
            else
            {
                imprumutGasit.DataReturnare = DateTime.Now;

                if (DateTime.Now > imprumutGasit.DataScadenta)
                {
                    TimeSpan intarziere = DateTime.Now - imprumutGasit.DataScadenta;
                    int zile = (int)Math.Ceiling(intarziere.TotalDays);
                    double suma = zile * biblioteca.PenalizarePeZi;
                    
                    Console.WriteLine($"Ai intarziat {zile} zile,Penalizare:{suma} RON");
                }
                else
                {
                    Console.WriteLine("Returant la timp");
                }
                // Cautam cartea in biblioteca globala pentru a reface stocul
                var carteDinBiblioteca = biblioteca.CartiPublic.FirstOrDefault(c => c.Nume == imprumutGasit.CarteaImprumutata.Nume);
                if (carteDinBiblioteca != null)
                {
                    carteDinBiblioteca.NumarCopii++;
                }

                biblioteca.SalveazaDate();
                Console.WriteLine("Cartea a fost restituita cu succes!");
            }
        }
    }

    public void VeziCartiImprumutate()
    {
        Console.WriteLine("\n====Carti imprumutate====");
        foreach (var c in CartiImprumutate)
            Console.WriteLine(c.CarteaImprumutata);
    }

    public void Recenzie(Bibloteca biblioteca)
    {
        Console.WriteLine("Titlul cartii pentru care doriti sa lasati o recenzie:");
        string titlu =  Console.ReadLine();
        var cautat = biblioteca.CartiPublic.FirstOrDefault(x => x.Nume == titlu);
        if (cautat != null)
        {
            Console.WriteLine("Recenzia:");
            string recenzie = Console.ReadLine();
            string recenzie_final = $"Utilizatorul {this.Nume} spune:" + recenzie;
            cautat.AdaugaRecenzie(recenzie_final);
            biblioteca.SalveazaDate();
            
            Console.WriteLine("Ofera o nota intre 1 si 5:");
            int nota =  int.Parse(Console.ReadLine());
            if (nota >= 0 && nota <= 5)
            {
                cautat.Note.Add(nota);
                biblioteca.SalveazaDate();
            }
            else
            {
                Console.WriteLine("Nota Introdusa gresit");
            }
        }
        else
        {
            Console.WriteLine("Cartea nu exista");
        }
    }

    public void VeziRecenzii(Bibloteca biblioteca)
    {
        Console.WriteLine("Titlul cartii pentru care doriti sa lasati o recenzie:");
        string titlu =  Console.ReadLine();
        var cautat = biblioteca.CartiPublic.FirstOrDefault(x => x.Nume == titlu);
        if (cautat != null)
        {
            cautat.VeziRecenzii();
        }
    }
}