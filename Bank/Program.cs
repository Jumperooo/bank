namespace Bank
{
    class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool isLoggedIn = false;
            int userId = 0; // ID zalogowanego użytkownika
            List<Client> clients = new List<Client>(); // Lista na informacje o klientach

            // Initialize client data
            clients.Add(new Client { ID = 1, Name = "Jan Nowak", AccountNumber = "001", Balance = 1457.23m });
            clients.Add(new Client { ID = 2, Name = "Agnieszka Kowalska", AccountNumber = "002", Balance = 3600.18m });
            clients.Add(new Client { ID = 3, Name = "Robert Lewandowski", AccountNumber = "003", Balance = 2745.03m });
            clients.Add(new Client { ID = 4, Name = "Zofia Plucińska", AccountNumber = "004", Balance = 7344.00m });
            clients.Add(new Client { ID = 5, Name = "Grzegorz Braun", AccountNumber = "005", Balance = 455.38m });

            void DisplayClientInfo()
            {
                Console.WriteLine("ID | IMIĘ I NAZWISKO | NR KONTA | SALDO");
                foreach (var client in clients)
                {
                    Console.WriteLine($"{client.ID} | {client.Name} | {client.AccountNumber} | {client.Balance.ToString("N2")} zł");
                }
                Console.WriteLine("\n");
            }

            while (true)
            {
                if (!isLoggedIn)
                {
                    Console.WriteLine("WYBIERZ OPCJĘ:");
                    Console.WriteLine("1 => LISTA WSZYSTKICH KLIENTÓW BANKU");
                    Console.WriteLine("2 => LOGOWANIE");
                    Console.WriteLine("3 => ZAKOŃCZ PROGRAM");
                    Console.WriteLine("WYBIERZ 1, 2 LUB 3:");
                }
                else
                {
                    // Display client information if logged in
                    Console.WriteLine("\nZALOGOWANY KLIENT");
                    var loggedInClient = clients.Find(client => client.ID == userId);
                    Console.WriteLine($"ID: {loggedInClient.ID}");
                    Console.WriteLine($"IMIĘ I NAZWISKO: {loggedInClient.Name}");
                    Console.WriteLine($"NR KONTA: {loggedInClient.AccountNumber}");
                    Console.WriteLine($"SALDO: {loggedInClient.Balance.ToString("N2")} zł");
                }

                if (!isLoggedIn)
                {
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            DisplayClientInfo();
                            break;
                        case "2":
                            Console.WriteLine("ZALOGUJ SIĘ WYBIERAJĄC ID KLIENTA:");
                            string userIdInput = Console.ReadLine();
                            if (int.TryParse(userIdInput, out userId) && userId >= 1 && userId <= 5)
                            {
                                isLoggedIn = true;
                            }
                            else
                            {
                                Console.WriteLine("Nieprawidłowe ID klienta.");
                            }
                            break;
                        case "3":
                            Console.WriteLine("Wybrano opcję: ZAKOŃCZ PROGRAM");
                            return; // Zakańcza program i tyle
                        default:
                            Console.WriteLine("Nieprawidłowa opcja. Wpisz 1, 2 lub 3.");
                            break;
                    }
                }
                else
                {
                    // Menu zalogowanego użytkownika
                    Console.WriteLine("WPISZ NUMER KONTA NA KTÓRY CHCESZ WYKONAĆ PRZELEW:");
                    string accountNumber = Console.ReadLine();

                    Console.WriteLine("PODAJ KWOTĘ PRZELEWU:");
                    string transferAmountInput = Console.ReadLine();
                    if (decimal.TryParse(transferAmountInput, out decimal transferAmount))
                    {
                        var loggedInClient = clients.Find(client => client.ID == userId);
                        var recipientClient = clients.Find(client => client.AccountNumber == accountNumber);

                        if (recipientClient != null)
                        {
                            if (transferAmount <= loggedInClient.Balance)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("PRZELEW ZOSTAŁ WYKONANY");
                                Console.ResetColor();

                                // Update balansu konta dla odbiorcy i nadawcy
                                loggedInClient.Balance -= transferAmount;
                                recipientClient.Balance += transferAmount;

                                // Wyświetla informacje o klientach po transferach,
                                // Wylogowanie żeby można było działać jeszcze raz
                                DisplayClientInfo();
                                isLoggedIn = false;
                            }
                            else
                            {
                                Console.WriteLine("Nie masz wystarczających środków na koncie.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowy numer konta odbiorcy.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowa kwota przelewu.");
                    }
                }
            }
        }
    }
}