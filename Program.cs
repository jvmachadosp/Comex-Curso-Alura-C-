using Comex;

ComexService service = new ComexService();
Menu menu = new Menu(service);

await menu.ExibirMenu();
