using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BudgetFlow.Db;

namespace BudgetFlow.Migrations
{
    [DbContext(typeof(BudgetFlowContext))]
    [Migration("20161104170937_Add balances")]
    partial class Addbalances
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BudgetFlow.Db.BalanceDBO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("Date");

                    b.HasKey("Id");

                    b.ToTable("Balances");
                });

            modelBuilder.Entity("BudgetFlow.Db.PaymentDBO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Frequency");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Payments");
                });
        }
    }
}
