namespace ByteBank1
{
    public class Program
    {
        
        static void ShowMenu()
        {
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Listar todas as contas registradas");
            Console.WriteLine("4 - Detalhes de um usuário");
            Console.WriteLine("5 - Quantia armazenada no banco");
            Console.WriteLine("6 - Manipular a conta");
            Console.WriteLine("0 - Para sair do programa");
            Console.Write("Digite a opção desejada: ");
        }





        static void RegistrarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Digite o nome: ");
            titulares.Add(Console.ReadLine()); //o Add faz com que não seja necessario criar uma variavel para receber o cpf
            Console.Write("Digite a senha: ");
            senhas.Add(Console.ReadLine());
            saldos.Add(0);
            
            
        }





        static void DeletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = cpfs.FindIndex(d => d == cpfParaDeletar); // retorna true ou false; o d serve de parametro de função (pode ser qualquer letra ou nome)

            if (indexParaDeletar == -1)
            {
                Console.WriteLine("Não foi possível deletar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }

            cpfs.Remove(cpfParaDeletar);
            titulares.RemoveAt(indexParaDeletar);
            senhas.RemoveAt(indexParaDeletar);
            saldos.RemoveAt(indexParaDeletar);

            Console.WriteLine("Conta deletada com sucesso.");
        }






        static void ListarTodasAsContas(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            for (int i = 0; i < cpfs.Count; i++)
            {
                ApresentaConta(i, cpfs, titulares, saldos);
            }
        }







        static void ApresentaConta(int index, List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo = {saldos[index]:f2}");
        }







        static void ApresentarUsuario(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaApresentar = cpfs.FindIndex(d => d == cpfParaApresentar);

            if (indexParaApresentar == -1)
            {
                Console.WriteLine("Não foi possível apresentar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }

            ApresentaConta(indexParaApresentar, cpfs, titulares, saldos);
        }







        static void ApresentarValorAcumulado(List<double> saldos)
        {
            Console.WriteLine($"Total acumulado no banco: {saldos.Sum()}");
            //.Agregatte (0.0, (x, y) =>  + y)
        }







        static void MenuTransicoes()
        {
            Console.WriteLine("Operações disponíveis:");
            Console.WriteLine("1 - Saque");
            Console.WriteLine("2 - Despósito");
            Console.WriteLine("3 - Transferência");
            Console.WriteLine("0 - Para voltar");
            Console.Write("Digite o número da opção desejada: ");
        }





        static void Login(List<string> cpfs, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            string cpfLogin = Console.ReadLine();
            Console.Write("Digite a senha: ");
            string senhaLogin = Console.ReadLine();
            int indexLoginCpf = cpfs.FindIndex(d => d == cpfLogin);
            int indexLoginSenha = senhas.FindIndex(e => e == senhaLogin);
            Console.Clear();

            if (indexLoginCpf != indexLoginSenha)
            {
                Console.WriteLine("Não foi possível entrar na conta.");
                Console.WriteLine("MOTIVO: CPF ou senha inválidos");
            }
            else
            {

                int opcao;

                do
                {
                    Console.WriteLine();
                    Console.WriteLine("=========================================");
                    MenuTransicoes();
                    opcao = int.Parse(Console.ReadLine());
                    Console.WriteLine("=========================================");
                    Console.WriteLine();
                    Console.Clear();

                    switch (opcao)
                    {
                        case 0:
                            break;

                        case 1:
                            Console.WriteLine($"Seu saldo atual é de: R${saldos[indexLoginCpf]:f2}");
                            Console.Write("Quanto deseja sacar? R$");
                            double qSaque = double.Parse(Console.ReadLine());

                            if (saldos[indexLoginCpf] <= 0 | saldos[indexLoginCpf] < qSaque)
                            {
                                Console.WriteLine("Saldo insuficiente para realizar o saque.");
                            }
                            else
                            {
                                double restoSaldo = saldos[indexLoginCpf] - qSaque;
                                saldos[indexLoginCpf] = restoSaldo;
                                Console.WriteLine();
                                Console.WriteLine("Saque realizado com sucesso!");
                                Console.WriteLine($"Seu saldo restante é de: R${saldos[indexLoginCpf]:f2}");
                            }
                            break;

                        case 2:
                            Console.WriteLine($"Seu saldo atual é de: R${saldos[indexLoginCpf]:f2}");
                            Console.Write("Quanto deseja depositar? R$");
                            double dep = double.Parse(Console.ReadLine());
                            double saldoFinal = saldos[indexLoginCpf] + dep;
                            saldos[indexLoginCpf] = saldoFinal;
                            Console.WriteLine();
                            Console.WriteLine("Depósito realizado com sucesso!");
                            Console.WriteLine($"Seu novo saldo é de: R${saldos[indexLoginCpf]:f2}");
                            break;


                        case 3:
                            Console.WriteLine($"Seu saldo atual é de: R${saldos[indexLoginCpf]:f2}");
                            Console.Write("Quanto deseja tranferir? R$");
                            double transf = double.Parse(Console.ReadLine());

                            if (saldos[indexLoginCpf] < transf)
                            {
                                Console.WriteLine("Saldo insuficiente para realizar a transferência.");
                            }
                            else
                            {
                                Console.Write("Digite o CPF do titular da conta que receberá a transferência: ");
                                string receberTransf = Console.ReadLine();
                                int indexReceberTransf = cpfs.FindIndex(a => a == receberTransf);

                                if (indexReceberTransf == -1)
                                {
                                    Console.WriteLine("Não foi possível tranferir para esta conta.");
                                    Console.WriteLine("MOTIVO: conta não encontrada.");
                                }
                                else
                                {
                                    double saldoDaTransf = saldos[indexReceberTransf] + transf;
                                    saldos[indexReceberTransf] = saldoDaTransf;
                                    double saldoF = saldos[indexLoginCpf] - transf;
                                    saldos[indexLoginCpf] = saldoF;
                                    Console.WriteLine();
                                    Console.WriteLine("Transferência realizada com sucesso!");
                                    Console.WriteLine($"Seu saldo restante é de: R${saldos[indexLoginCpf]:f2}");
                                }
                            }

                            break;
                    }
                   

                } while (opcao != 0);
            }
        }








        public static void Main(string[] args)
        {
            Console.WriteLine("Antes de começar a usar, vamos configurar alguns valores: ");



            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();

            int option;

            do
            {
                Console.WriteLine();
                ShowMenu();
                option = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (option)
                {
                    case 0:
                        Console.WriteLine("Estou encerrando o programa...");
                        break;
                    case 1:
                        RegistrarNovoUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 2:
                        DeletarUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 3:
                        ListarTodasAsContas(cpfs, titulares, saldos);
                        break;
                    case 4:
                        ApresentarUsuario(cpfs, titulares, saldos);
                        break;
                    case 5:
                        ApresentarValorAcumulado(saldos);
                        break;
                    case 6:
                        Login(cpfs, senhas, saldos);
                        break;

                        
                }

                

            } while (option != 0);
        }


    }
}
