namespace ProiectPoo;

class Program
{
    static Bibloteca bibloteca = new Bibloteca();
    static Bibliotecar bibliotecar = new Bibliotecar("radu@gmail.com", "123456");
    static Utilizator? utilizatorCurent = null;

    static void AdaugaUtilizatr()
    {
        Console.WriteLine("========Adaugare utilizator========");
        Console.WriteLine("Email: ");
        string email = Console.ReadLine();
        Console.WriteLine("Nume: ");
        string nume = Console.ReadLine();
        Console.WriteLine("Parola: ");
        string paroala = Console.ReadLine();
        var clinet = new Client(email, paroala);
        clinet.SetNume(nume);
        bibloteca.AdaugaUtilizator(clinet);
    }

    static bool LogIn()
    {
        Console.WriteLine("========Log In========");
        Console.WriteLine("Email: ");
        string email = Console.ReadLine();
        Console.WriteLine("Parola: ");
        string parola = Console.ReadLine();

        var userGasit = bibloteca.Autentificare(email, parola);
        if (userGasit != null)
        {
            utilizatorCurent = userGasit;
            return true;   
        }
        else
        {
            Console.WriteLine("Email sau parola gresita!");
            return false;
        }
    }

    static void InitializareMeniu()
    {
        Console.WriteLine("========Menu========");
        Console.WriteLine("0. Exit");
        Console.WriteLine("1. Sign Up");
        Console.WriteLine("2. Log In");
    }

    static void MeniuBibilotecar()
    {
        while (true)
        {
            Console.WriteLine("\n=== Meniu Bibliotecar ===");
            Console.WriteLine("0. Log out");
            Console.WriteLine("1. Adauga Carte");
            Console.WriteLine("2. Afisare Carti");
            Console.WriteLine("3. Sterge Carte");
            Console.WriteLine("4. Modifica Carte");
            Console.WriteLine("5. Creare Categorie Noua");       
            Console.WriteLine("6. Incadreaza Carte in Categorie"); 
            Console.WriteLine("7. Vezi Categorii");                
            Console.WriteLine("8. Monitorizare Imprumuturi");
            Console.WriteLine("9. Configurare Reguli");

            try
            {
                int opt = int.Parse(Console.ReadLine());
                
                switch (opt)
                {
                    case 0:
                        return;
                    case 1:
                        bibliotecar.AdaugaCarteUI(bibloteca);
                        break;
                    case 2:
                        bibliotecar.AfisareCarti(bibloteca);
                        break;
                    case 3:
                        bibliotecar.StergeCarte(bibloteca);
                        break;
                    case 4:
                        bibliotecar.ModificaCarte(bibloteca);
                        break;
                    case 5:
                        bibliotecar.CreareCategorieUI(bibloteca);
                        break;
                    case 6:
                        bibliotecar.AdaugaCarteInCategorieUI(bibloteca);
                        break;
                    case 7:
                        bibloteca.AfisareCategorii();
                        break;
                    case 8: 
                        bibliotecar.MonitorizareImprumuturi(bibloteca);
                        break;
                    case 9:
                        bibliotecar.ConfigurareReguliUI(bibloteca);
                        break;
                    default:
                        Console.WriteLine("Optiune invalida.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Eroare: Introduceti doar cifre pentru a naviga in meniu!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A aparut o eroare neasteptata: {ex.Message}");
            }

            
        }
    }

    static void MeniuClient()
    {
        while (true)
        {
            Console.WriteLine("\n=== Meniu Client ===");
            Console.WriteLine("0. Log out");
            Console.WriteLine("1. Cauta Carte");
            Console.WriteLine("2. Imprumuta Carte");
            Console.WriteLine("3. Restituie Cartea");
            Console.WriteLine("4. Vezi carti imprumutate");
            Console.WriteLine("5. Lasati o recenzie");
            Console.WriteLine("6. Vezi recenziile unei carti");

            try
            {
                int opt = int.Parse(Console.ReadLine());
                
                switch (opt)
                {
                    case 0:
                        return; 
                    case 1:
                        if (utilizatorCurent is Client clientLogat)
                        {
                            clientLogat.CautareCarte(bibloteca);
                        }
                        break;
                    case 2:
                        if (utilizatorCurent is Client c)
                        {
                            c.ImprumutaCarte(bibloteca);
                        }
                        break;
                    case 3:
                        if (utilizatorCurent is Client c2)
                        {
                            c2.RestituireCarte(bibloteca);
                        }
                        break;
                    case 4:
                        if (utilizatorCurent is Client c3)
                        {
                            c3.VeziCartiImprumutate();
                        }
                        break;
                    case 5:
                        if (utilizatorCurent is Client c4)
                        {
                            c4.Recenzie(bibloteca);
                        }
                        break;
                    case 6:
                        if (utilizatorCurent is Client c5)
                        {
                            c5.VeziRecenzii(bibloteca);
                        }
                        break;
                    default:
                        Console.WriteLine("Optiune invalida.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Eroare: Introduceti doar cifre pentru a naviga in meniu!");
            }
            
        }
    }

    static void MeniuUtilizator(Utilizator utilizator)
    {
        Console.WriteLine("========Meniu Utilizator========");
        if(utilizator is Bibliotecar)
            MeniuBibilotecar();
        else if (utilizator is Client)
            MeniuClient();
            
    }
    
    static void Main(string[] args)
    {
        if (!bibloteca.ExistaEmail("radu@gmail.com"))
        {
            bibliotecar.SetNume("Radu");
            bibloteca.AdaugaUtilizator(bibliotecar);
        }
        bool ok = true;
        do
        {
            InitializareMeniu();
            try
            {
                int opt = int.Parse(Console.ReadLine());
                switch (opt)
                {
                    case 0:
                        ok = false;
                        break;
                    case 1:
                        AdaugaUtilizatr(); 
                        break;
                    case 2:
                        if (LogIn())
                        {
                            bibliotecar.AfisareMeniuSpecific();
                            MeniuUtilizator(utilizatorCurent);
                        }
                        break;
                    default:
                        Console.WriteLine("Optiune invalida.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Eroare: Introduceti doar cifre pentru a naviga in meniu!");
            }
            
        } while (ok);
    }
}