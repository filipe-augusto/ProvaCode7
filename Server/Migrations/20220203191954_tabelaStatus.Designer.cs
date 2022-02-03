﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProvaCode7.Server;

namespace ProvaCode7.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220203191954_tabelaStatus")]
    partial class tabelaStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15");

            modelBuilder.Entity("ProvaCode7.Shared.CategoriaProduto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("TEXT");

                    b.Property<int>("Descricao")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAtivo")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("CategoriaProduto");
                });

            modelBuilder.Entity("ProvaCode7.Shared.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Credito")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdEndereco")
                        .HasColumnName("id_endereco")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("IdStatus")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAtivo")
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IdEndereco")
                        .IsUnique();

                    b.HasIndex("IdStatus");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("ProvaCode7.Shared.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Complemento")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("TEXT");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAtivo")
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("ProvaCode7.Shared.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdCategoria")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAtivo")
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<decimal>("Valor")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IdCategoria");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("ProvaCode7.Shared.StatusCliente", b =>
                {
                    b.Property<byte>("IdStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsContabilizaVenda")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFinalizaCliente")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdStatus");

                    b.ToTable("StatusCliente");
                });

            modelBuilder.Entity("ProvaCode7.Shared.Cliente", b =>
                {
                    b.HasOne("ProvaCode7.Shared.Endereco", "Endereco")
                        .WithOne("Cliente")
                        .HasForeignKey("ProvaCode7.Shared.Cliente", "IdEndereco")
                        .HasConstraintName("ForeignKey_Cliente_Endereco")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProvaCode7.Shared.StatusCliente", "StatusCliente")
                        .WithMany("Clientes")
                        .HasForeignKey("IdStatus")
                        .HasConstraintName("ForeignKey_Cliente_StatusCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProvaCode7.Shared.Produto", b =>
                {
                    b.HasOne("ProvaCode7.Shared.CategoriaProduto", "CategoriaProduto")
                        .WithMany("Produtos")
                        .HasForeignKey("IdCategoria")
                        .HasConstraintName("ForeignKey_Produto_CategoriaProduto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
