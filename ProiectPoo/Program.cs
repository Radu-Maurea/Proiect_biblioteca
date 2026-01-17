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
            Console.WriteLine("0.Logout");
            Console.WriteLine("1. Adauga Carte");
            Console.WriteLine("2. Afisare Carti");
            
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
                default:
                    break;
            }
        }
}
    static void MeniuUtilizator(Utilizator utilizator)
    {
        Console.WriteLine("========Meniu Utilizator========");
        if(utilizator is Bibliotecar)
            MeniuBibilotecar();
            
    }
    
    static void Main(string[] args)
    {
        bibliotecar.SetNume("Radu");
        bibloteca.AdaugaUtilizator(bibliotecar);
        bool ok = true;
        do
        {
            InitializareMeniu();
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
                    break;
            }
        } while (ok);
    }
}