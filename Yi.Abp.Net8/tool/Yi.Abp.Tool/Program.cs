﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yi.Abp.Tool;
using Yi.Abp.Tool.Application.Contracts;

class Program
{
    static async Task Main(string[] args)
    {

#if DEBUG
        //args = ["v"];
        //args = ["-v"];
        //args = ["h"];
        //args = ["-h"];
        //args = [];
        //args = ["12312"];
        args = ["new", "Acme.Book", "-t", "module", "-csf"];
        //args = ["new", "Acme.Book", "-t", "module"];
#endif
        try
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices(async (host, service) =>
                {
                    await service.AddApplicationAsync<YiAbpToolModule>();
                })
                //.ConfigureAppConfiguration(configurationBuilder =>
                //{
                //    configurationBuilder.AddJsonFile("appsettings.json");
                //})
                .UseAutofac()
                .Build();
            var commandSelector = host.Services.GetRequiredService<CommandSelector>();
            await commandSelector.SelectorAsync(args);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }
    }

}