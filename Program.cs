using System;
using System.Collections.Generic;

class Proprietario
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string CPF { get; set; }

    public Proprietario(string nome, string telefone, string cpf)
    {
        Nome = nome;
        Telefone = telefone;
        CPF = cpf;
    }
}

abstract class Imovel
{
    private static int contadorIds = 1;
    public int Id { get; }
    protected string endereco;
    protected int numero;
    protected bool alugado;
    protected Proprietario proprietario;

    public Imovel(string endereco, int numero, Proprietario proprietario)
    {
        Id = contadorIds++;
        this.endereco = endereco;
        this.numero = numero;
        this.proprietario = proprietario;
        this.alugado = false;
    }

    public string Endereco => endereco;
    public int Numero => numero;
    public bool Alugado
    {
        get => alugado;
        set => alugado = value;
    }

    public string ContatoProprietario() => $"{proprietario.Nome} - {proprietario.Telefone}";

    public abstract string EstaAlugado();
    public abstract int CalcularAluguel(int dias);
}

class Casa : Imovel
{
    public Casa(string endereco, int numero, Proprietario proprietario)
        : base(endereco, numero, proprietario) {}

    public override string EstaAlugado()
    {
        string status = alugado ? "alugada" : "disponível";
        return $"A casa de id {Id} localizada em {endereco} está {status}.";
    }

    public override int CalcularAluguel(int dias) => dias * 100;
}

class Apartamento : Imovel
{
    public Apartamento(string endereco, int numero, Proprietario proprietario)
        : base(endereco, numero, proprietario) {}

    public override string EstaAlugado()
    {
        string status = alugado ? "alugado" : "disponível";
        return $"O apartamento de id {Id} localizado no {endereco} está {status}.";
    }

    public override int CalcularAluguel(int dias) => dias * 80;
}

class Program
{
    static List<Imovel> imoveis = new List<Imovel>();

    static void Main()
    {
        int opcao = -1;
        do
        {
            MostrarMenu();
            try
            {
                opcao = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Entrada inválida! Por favor, insira um número.");
                opcao = -1;
            }

            switch (opcao)
            {
                case 1: GerenciarImovel(); break;
                case 2: ListarImoveis(); break;
                case 3: AlternarStatusAluguel(); break;
                case 4: CalcularValorAluguel(); break;
                case 5: DeletarImovelPorId(); break;
                case 0: Console.WriteLine("Saindo..."); break;
                default:
                    if (opcao != -1)
                        Console.WriteLine("Opção inválida!");
                    break;
            }

            if (opcao != 0)
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }

        } while (opcao != 0);
    }

    static void MostrarMenu()
    {
        Console.Clear();
        Console.WriteLine("=== SISTEMA DE IMÓVEIS ===");
        Console.WriteLine("1 - Registrar / Deletar Imóvel");
        Console.WriteLine("2 - Listar Imóveis");
        Console.WriteLine("3 - Alugar / Disponibilizar Imóvel");
        Console.WriteLine("4 - Calcular Valor do Aluguel");
        Console.WriteLine("5 - Deletar Imóvel por ID");
        Console.WriteLine("0 - Sair");
        Console.Write("Escolha uma opção: ");
    }

    static void GerenciarImovel()
    {
        Console.WriteLine("1 - Registrar Casa");
        Console.WriteLine("2 - Registrar Apartamento");
        Console.WriteLine("3 - Deletar Imóvel pelo ID");
        Console.Write("Escolha uma opção: ");
        if (!int.TryParse(Console.ReadLine(), out int opcao))
        {
            Console.WriteLine("Entrada inválida!");
            return;
        }

        switch (opcao)
        {
            case 1:
                CadastrarCasa();
                break;
            case 2:
                CadastrarApartamento();
                break;
            case 3:
                DeletarImovelPorId();
                break;
            default:
                Console.WriteLine("Opção inválida!");
                break;
        }
    }

    static void CadastrarCasa()
    {
        var (endereco, numero, proprietario) = ObterDadosImovel();
        imoveis.Add(new Casa(endereco, numero, proprietario));
        Console.WriteLine("Casa cadastrada com sucesso!");
    }

    static void CadastrarApartamento()
    {
        var (endereco, numero, proprietario) = ObterDadosImovel();
        imoveis.Add(new Apartamento(endereco, numero, proprietario));
        Console.WriteLine("Apartamento cadastrado com sucesso!");
    }

    static (string endereco, int numero, Proprietario proprietario) ObterDadosImovel()
    {
        Console.WriteLine("Vamos cadastrar o endereço do imóvel.");
        Console.Write("Nome do bairro: ");
        string bairro = Console.ReadLine();

        Console.Write("Tipo de logradouro (Rua, Avenida, Travessa etc.): ");
        string tipoLogradouro = Console.ReadLine();

        Console.Write("Nome do logradouro: ");
        string nomeLogradouro = Console.ReadLine();

        string endereco = $"{tipoLogradouro} {nomeLogradouro}, Bairro {bairro}";

        Console.Write("Número do imóvel: ");
        int numero = int.Parse(Console.ReadLine());

        Proprietario proprietario = CriarProprietario();

        return (endereco, numero, proprietario);
    }

    static Proprietario CriarProprietario()
    {
        Console.Write("Nome do proprietário: ");
        string nome = Console.ReadLine();

        Console.Write("Telefone: ");
        string telefone = Console.ReadLine();

        Console.Write("CPF: ");
        string cpf = Console.ReadLine();

        return new Proprietario(nome, telefone, cpf);
    }

    static void ListarImoveis()
    {
        Console.Write("Deseja listar todos os imóveis ou só os alugados? (T/A): ");
        string escolha = Console.ReadLine().Trim().ToUpper();

        if (escolha == "T")
            ListarTodosImoveis();
        else if (escolha == "A")
            ListarImoveisAlugados();
        else
        {
            Console.WriteLine("Opção inválida. Mostrando apenas imóveis alugados.");
            ListarImoveisAlugados();
        }
    }

    static void ListarTodosImoveis()
    {
        Console.WriteLine("\n=== TODOS OS IMÓVEIS ===");
        if (imoveis.Count == 0)
        {
            Console.WriteLine("Nenhum imóvel cadastrado.");
            return;
        }

        foreach (var imovel in imoveis)
        {
            Console.WriteLine(imovel.EstaAlugado());
        }
    }

    static void ListarImoveisAlugados()
    {
        Console.WriteLine("\n=== IMÓVEIS ALUGADOS ===");
        var alugados = imoveis.FindAll(i => i.Alugado);

        if (alugados.Count == 0)
        {
            Console.WriteLine("Nenhum imóvel está alugado no momento.");
            return;
        }

        foreach (var imovel in alugados)
        {
            Console.WriteLine(imovel.EstaAlugado());
        }
    }

    static void AlternarStatusAluguel()
    {
        Console.Write("Informe o ID do imóvel: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido!");
            return;
        }

        var imovel = imoveis.Find(i => i.Id == id);
        if (imovel == null)
        {
            Console.WriteLine("Imóvel não encontrado!");
            return;
        }

        imovel.Alugado = !imovel.Alugado;
        Console.WriteLine(imovel.EstaAlugado());
    }

    static void CalcularValorAluguel()
    {
        Console.Write("Informe o ID do imóvel: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido!");
            return;
        }

        var imovel = imoveis.Find(i => i.Id == id);
        if (imovel == null)
        {
            Console.WriteLine("Imóvel não encontrado!");
            return;
        }

        Console.Write("Digite quantos dias deseja alugar: ");
        if (!int.TryParse(Console.ReadLine(), out int dias))
        {
            Console.WriteLine("Número de dias inválido!");
            return;
        }

        int valorTotal = imovel.CalcularAluguel(dias);
        Console.WriteLine($"Valor total do aluguel: R$ {valorTotal}");
    }

    static void DeletarImovelPorId()
    {
        Console.Write("Informe o ID do imóvel a ser deletado: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido!");
            return;
        }

        var imovelDeletar = imoveis.Find(i => i.Id == id);
        if (imovelDeletar == null)
        {
            Console.WriteLine("Imóvel não encontrado!");
            return;
        }

        imoveis.Remove(imovelDeletar);
        Console.WriteLine("Imóvel deletado com sucesso!");
    }
}
