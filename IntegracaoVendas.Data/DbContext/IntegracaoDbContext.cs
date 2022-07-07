using System;
using System.Collections.Generic;
using System.Text;
using IntegracaoVendas.Dominio.Models;
using IntegracaoVendas.Dominio.Models.PeditosTxt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IntegracaoVendas.Data.DbContext
{
    public class IntegracaoDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IConfiguration _configuration;
        public IntegracaoDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BillingAddress> BillingAddresses { get; set; }
        public DbSet<LineItem> LineItens { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<INFORMACOES_PEDIDO> INFORMACOES_PEDIDOS { get; set; }
        public DbSet<INFORMACOES_CORREIO> INFORMACOES_CORREIOS { get; set; }
        public DbSet<VENDA_CANCELADA> VENDAS_CANCELADAS { get; set; }
        public DbSet<PedidoKeeper> PedidosKeeper { get; set; }
        public DbSet<ProdutoKeeper> ProdutosKeeper { get; set; }

        public DbSet<InfosPedidosDTO> InfosPedidosDTOs { get; set; }

        public DbSet<PEDIDOS_ENVIADOS> PEDIDOS_ENVIADOS { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasKey(t => new { t.OrderNumber });

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Shipment)
                .WithOne(s => s.Order)
                .HasForeignKey<Shipment>(s => s.OrderNumber);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderNumber);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.BillingAddress)
                .WithOne(b => b.Order)
                .HasForeignKey<BillingAddress>(b => b.OrderNumber);

            modelBuilder.Entity<LineItem>()
                .HasOne(o => o.Order)
                .WithMany(b => b.LineItem)
                .HasForeignKey(l => l.OrderNumber);

            modelBuilder.Entity<INFORMACOES_PEDIDO>()
                .HasKey(p => new { p.PEDIDO });

            modelBuilder.Entity<INFORMACOES_CORREIO>()
                .HasKey(p => new { p.OBJETO });

            modelBuilder.Entity<VENDA_CANCELADA>()
            .HasKey(p => new { p.ORDERNUMBER });

            modelBuilder.Entity<PedidoKeeper>().ToTable("SPL_VW_GET_ORDERS");
            modelBuilder.Entity<ProdutoKeeper>().ToTable("SPL_VW_GET_KEEPERS_ITEMS_FILE");

            modelBuilder.Entity<InfosPedidosDTO>()
                .HasNoKey();

            modelBuilder.Entity<PEDIDOS_ENVIADOS>()
                .HasKey(p => new { p.Pedido });
        }
    }
}
