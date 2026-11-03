using Comex;

ComexService service = new ComexService();
Menu menu = new Menu(service);
menu.Registrar(10, new MenuGerarRelatorioCsv(service));

await menu.ExibirMenu();
