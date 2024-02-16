

using Microsoft.Extensions.DependencyInjection;
using PlayGround;
using PlayGround.CSVFiles;

var services = new ServiceCollection();
services.AddSingleton<ICSVRearer, CSVRearer>();
services.AddSingleton<IApp, App>();

var serviceProvider =  services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();
app.Run();